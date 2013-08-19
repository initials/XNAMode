using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace org.flixel
{

    /// <summary>
    /// 
    /// </summary>
    public class FlxCaveGenerator
    {
        static public int _numTilesCols = 50;
	    static public int _numTilesRows = 50;


        /// <summary>
        /// How many times do you want to "smooth" the cave.
        /// The higher number the smoother.
        /// </summary>
	    public int numSmoothingIterations = 5;
		
        /// <summary>
        /// During initial state, how percent of matrix are walls?
        /// The closer the value is to 1.0, more wall-e the area is
        /// Values 0-1.
        /// </summary>
	    public float initWallRatio = 0.5f;


        public FlxCaveGenerator(int nCols, int nRows)
        {

    		_numTilesCols = nCols;
	    	_numTilesRows = nRows;

        }

        /// <summary>
        /// Generate a matrix of zeroes.
        /// </summary>
        /// <param name="rows">Number of rows for the matrix</param>
        /// <param name="cols">Number of cols for the matrix</param>
        /// <returns>Spits out a matrix that is cols x rows, zero initiated</returns>
        public int[,] genInitMatrix(int rows, int cols)
        {
            // Build array of 1s
            int[,] mat = new int[rows,cols];

            for (int _y = 0; _y < rows; _y++)
            {
                for (int _x = 0; _x < cols; _x++)
                {
                    mat[_y, _x] = 0;
                }

            }

            return mat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns a matrix of a cave!</returns>
	    public int[,] generateCaveLevel()
        {
		    // Initialize random array
		
		    int[,] mat = new int[_numTilesRows,_numTilesCols];
 
		    mat = this.genInitMatrix(_numTilesRows, _numTilesCols);

            int floor = _numTilesRows - 2;

            for (int _y = 0; _y < _numTilesRows; _y++) {
                for (int _x = 0; _x < _numTilesCols; _x++){
                    

                    //Throw in a random assortment of ones and zeroes.
                    if (FlxU.random() < initWallRatio)
                    {
                        mat[_y, _x] = 1;
                    }
                    else
                    {
                        mat[_y,_x] = 0;
                    }
                }
            }

		    // Secondary buffer
		    int[,] mat2 = genInitMatrix( _numTilesRows, _numTilesCols );
		
		    // Run automata

            for (int i = 0; i <= numSmoothingIterations; i++)
            {

                runCelluarAutomata(mat, mat2);

                int[,] temp = new int[mat.GetLength(0), mat.GetLength(1)];
                mat = mat2;
                mat2 = temp;

            }

            return mat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns a matrix of a cave!</returns>
        public int[,] generateCaveLevel(int floor, int ceiling, int leftWall, int rightWall, int floorPermanent, int ceilingPermanent, int leftWallPermanent, int rightWallPermanent)
        {
            // Initialize random array

            int[,] mat = new int[_numTilesRows, _numTilesCols];

            mat = this.genInitMatrix(_numTilesRows, _numTilesCols);

            for (int _y = 0; _y < _numTilesRows; _y++)
            {
                for (int _x = 0; _x < _numTilesCols; _x++)
                {
                    //Throw in a random assortment of ones and zeroes.
                    if (FlxU.random() < initWallRatio)
                    {
                        mat[_y, _x] = 1;
                    }
                    else
                    {
                        mat[_y, _x] = 0;
                    }

                    //If you need a floor or walls or ceiling, throw them in here.
                    //Note: These will be smoothed.

                    // Floor
                    if (_y >= _numTilesRows - floor)
                    {
                        mat[_y, _x] = 1;
                    }

                    // Ceiling
                    if (_y < ceiling)
                    {
                        mat[_y, _x] = 1;
                    }

                    // Left wall

                    if (_x < leftWall)
                    {
                        mat[_y, _x] = 1;
                    }

                    if (_x >= _numTilesCols - rightWall)
                    {
                        mat[_y, _x] = 1;
                    }
                }
            }

            // Secondary buffer
            int[,] mat2 = genInitMatrix(_numTilesRows, _numTilesCols);

            // Run automata

            for (int i = 0; i <= numSmoothingIterations; i++)
            {

                runCelluarAutomata(mat, mat2);

                int[,] temp = new int[mat.GetLength(0), mat.GetLength(1)];
                mat = mat2;
                mat2 = temp;

            }

            // This step puts some walls in after the smooth. Important only to run if needed

            if (floorPermanent != 0 || ceilingPermanent != 0 || leftWallPermanent != 0 || rightWallPermanent != 0)
            {
                for (int _y = 0; _y < _numTilesRows; _y++)
                {
                    for (int _x = 0; _x < _numTilesCols; _x++)
                    {
                        // Floor
                        if (_y >= _numTilesRows - floorPermanent)
                        {
                            mat[_y, _x] = 1;
                        }

                        // Ceiling
                        if (_y < ceilingPermanent)
                        {
                            mat[_y, _x] = 1;
                        }

                        // Left wall

                        if (_x < leftWallPermanent)
                        {
                            mat[_y, _x] = 1;
                        }

                        if (_x >= _numTilesCols - rightWallPermanent)
                        {
                            mat[_y, _x] = 1;
                        }
                    }
                }
            }

            return mat;
        }


        /// <summary>
        /// Runs 
        /// </summary>
        /// <param name="inMat"></param>
        /// <param name="outMat"></param>
        public void runCelluarAutomata(int[,] inMat, int[,] outMat)
        {
            int numRows = inMat.GetLength(0);
            int numCols = inMat.GetLength(1);

            //Console.WriteLine(numRows + "< r - c > " + numCols);

            for (int _y = 0; _y < numRows; _y++)
            {
                for (int _x = 0; _x < numCols; _x++)
                {
                    int numWalls = countNumWallsNeighbors(inMat, _x, _y, 1);

                    if (numWalls >= 5)
                    {
                        outMat[_y, _x] = 1;
                    }
                    else
                    {
                        outMat[_y, _x] = 0;
                    }
                }
            }

        }

        /// <summary>
        /// Counts number of walls around neighbours
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public int countNumWallsNeighbors(int[,] mat, int xPos, int yPos, int dist)
        {
            int count = 0;

            var numbers = Enumerable.Range(-dist , dist + 1);

            for ( int _y = -dist; _y <= dist; ++_y )
			{
				for ( int _x = -dist; _x <= dist; ++_x )
				{

                    // Boundary
                    if ( xPos + _x < 0 || xPos + _x > _numTilesCols - 1 || yPos + _y < 0 || yPos + _y > _numTilesRows - 1 )
                    {
                        continue;
                    }

                    // Neighbor is non-wall
                    if (mat[yPos + _y, xPos + _x] != 0)
                        count += 1;

                }
            }

            return count;
        }

        public int[] findRandomSolid(int[,] inMat)
        {
            int numRows = inMat.GetLength(0);
            int numCols = inMat.GetLength(1);

            int n = 0;
            int rx = 0;
            int ry = 0;

            while (n != 1)
            {
                rx = (int)(FlxU.random() * numRows);
                ry = (int)(FlxU.random() * numCols);
                
                if(inMat[rx,ry] == 1) {
                    //Console.WriteLine(rx + "<-should be 1-> " + ry);
                    n=1;
                    
                }

            }

            return new int[] {rx, ry};


        }

        /// <summary>
        /// Looks for any tiles that are "ground tiles"
        /// ! = new tile.
        /// !00!
        /// 1!!1
        /// 1111
        /// 0000
        /// </summary>
        /// <param name="inMat">multi array to analyse</param>
        /// <returns>multi array that has decorations only</returns>
        public int[,] createDecorationsMap(int[,] inMat)
        {
            int numRows = inMat.GetLength(0);
            int numCols = inMat.GetLength(1);

            int[,] outMat = this.genInitMatrix(_numTilesRows, _numTilesCols);

            for (int _y = 1; _y < numRows - 2; _y++)
            {
                for (int _x = 1; _x < numCols - 1; _x++)
                {
                    // test for flat surface with empty above 
                    if (inMat[_y, _x] == 0 && inMat[_y + 1, _x] == 1 && inMat[_y + 1, _x - 1] == 1 && inMat[_y + 1, _x + 1] == 1)
                    {
                        outMat[_y, _x] = 1;
                    }
                    else
                    {
                        outMat[_y, _x] = 0;
                    }
                }
            }
            return outMat;
        }




        public int[,] createHangingDecorationsMap(int[,] inMat)
        {
            int numRows = inMat.GetLength(0);
            int numCols = inMat.GetLength(1);

            int[,] outMat = this.genInitMatrix(_numTilesRows, _numTilesCols);

            for (int _y = 1; _y < numRows - 2; _y++)
            {
                for (int _x = 1; _x < numCols - 1; _x++)
                {
                    // test for flat surface with empty above 
                    if (inMat[_y, _x] == 0 && inMat[_y - 1, _x] == 1 && inMat[_y + 1, _x - 1] == 1 && inMat[_y + 1, _x + 1] == 1)
                    {
                        outMat[_y, _x] = 1;
                    }
                    else
                    {
                        outMat[_y, _x] = 0;
                    }
                }
            }
            return outMat;
        }




        /// <summary>
        /// Returns a string that is comma separated for use with FlxTilemap
        /// </summary>
        /// <param name="multiArray"></param>
        /// <returns></returns>
        public string convertMultiArrayToString(int[,] multiArray)
        {
            string newMap = "";

            for (int i = 0; i < multiArray.GetLength(0); i++)
            {
                for (int j = 0; j < multiArray.GetLength(1); j++)
                {
                    string s = multiArray[i, j].ToString();

                    newMap += s;
                    if (j != multiArray.GetLength(1) - 1)
                    {
                        newMap += ","; 
                    }
                        
                }
                newMap += "\n";
            }
            return newMap;

        }



        //end

    }

}


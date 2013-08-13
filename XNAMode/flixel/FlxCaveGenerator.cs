using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace org.flixel
{


    public class FlxCaveGenerator
    {
        static public int _numTilesCols = 50;
	    static public int _numTilesRows = 50;

	    /*
		    * How many times do you want to "smooth" the cave.
		    * The higher number the smoother.
	    */

	    static public int numSmoothingIterations = 5;
		
	    /*
		    * During initial state, how percent of matrix are walls?
		    * The closer the value is to 1.0, more wall-e the area is
	    */

	    static public float initWallRatio = 0.5f;


        public FlxCaveGenerator(int nCols, int nRows)
        {

    		_numTilesCols = nCols;
	    	_numTilesRows = nRows;

        }

        /*
	    * 
	    * @param	rows 	Number of rows for the matrix
	    * @param	cols	Number of cols for the matrix
	    * 
	    * @return Spits out a matrix that is cols x rows, zero initiated
         * 
         * 		for y in range(rows):
			mat.append([])
			for x in range(cols):
				mat[y].append(0)
		
		return mat
         * 
         * 
	    */

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

         /*
	     * 
	     * @return Returns a matrix of a cave!
	     */

	    public int[,] generateCaveLevel()
        {
		    // Initialize random array
		
		    //print 'Generating new Cave', self._numTilesCols, self._numTilesRows
		    int[,] mat = new int[_numTilesRows,_numTilesCols];
 
		    mat = this.genInitMatrix(_numTilesRows, _numTilesCols);
		
            for (int _y = 0; _y < _numTilesRows; _y++) {
                for (int _x = 0; _x < _numTilesCols; _x++){
                    //mat[y,x] = 1 if random.random() < self.initWallRatio else 0 
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

                //string x = convertMultiArrayToString(mat);
                //Console.WriteLine(x);

                //string xy = convertMultiArrayToString(mat2);
                //Console.WriteLine(xy);

                runCelluarAutomata(mat, mat2);

                int[,] temp = new int[mat.GetLength(0), mat.GetLength(1)];
                mat = mat2;
                mat2 = temp;

            }

            return mat;
        }


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

                    //Console.WriteLine(numWalls);

                    if (numWalls >= 5)
                    {
                        //Console.WriteLine("num of walls is greater than 5" + numWalls);

                        outMat[_y, _x] = 1;
                    }
                    else
                    {
                        //Console.WriteLine(_y + " num walls counter " + _x);
                        outMat[_y, _x] = 0;
                    }
                }
            }

            //

            //string x = convertMultiArrayToString(outMat);
            //Console.WriteLine(x);



        }

        public int countNumWallsNeighbors(int[,] mat, int xPos, int yPos, int dist)
        {
            int count = 0;

            var numbers = Enumerable.Range(-dist , dist + 1);

            //foreach (int _y in numbers)
            //{
            //    foreach (int _x in numbers)
            //    {

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





/*
import os
import re
import random
from PIL import Image

class PyCaveGenerator:
	"""
		PyCaveGenerator
		based on Eddie Lee's FlxCaveGenerator
		
	"""
	
	def __init__(self):
		self._numTilesCols = 50;
		self._numTilesRows = 50;
		'''
		 * How many times do you want to "smooth" the cave.
		 * The higher number the smoother.
		'''
		self.numSmoothingIterations = 5;
		
		'''
		 * During initial state, how percent of matrix are walls?
		 * The closer the value is to 1.0, more wall-e the area is
		'''
		self.initWallRatio = 0.5;

		self.color = (10, 200, 10)
		self.color2 = (10,200,10)
		
		
		self.C_ocean = (103,183,255)
		self.C_sand = (255, 237,153)
		self.C_land = (120,217,112)
		
	'''
	 * 
	 * @param	nCols	Number of columns in the cave tilemap
	 * @param	nRows	Number of rows in the cave tilemap
	'''
	def CaveGenerator( self, nCols, nRows ):
		self._numTilesCols = nCols
		self._numTilesRows = nRows
		


	'''
	* 
	* @param	rows 	Number of rows for the matrix
	* @param	cols	Number of cols for the matrix
	* 
	* @return Spits out a matrix that is cols x rows, zero initiated
	'''
	def genInitMatrix( self, rows, cols ):
		# Build array of 1s
		mat = []
		
		for y in range(rows):
			mat.append([])
			for x in range(cols):
				mat[y].append(0)
		
		return mat

		


	'''
	 * Use the 4-5 rule to smooth cells
	'''
	def runCelluarAutomata( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(numRows):
			for x in range(numCols):
				numWalls = self.countNumWallsNeighbors( mat=inMat, xPos=x, yPos=y, dist=1 );
				
				if ( numWalls >= 5 ):
					outMat[y][x] = 1;
				else:
					outMat[y][x] = 0;
					
	
	
	'''
	 * Use the 4-5 rule to smooth cells
	'''
	def runCelluarAutomataColor( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(numRows):
			for x in range(numCols):
				numWalls = self.countNumWallsNeighbors( mat=inMat, xPos=x, yPos=y, dist=1 );

				outMat[y][x] = numWalls;

					
					
	def erode( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(numRows):
			for x in range(numCols):
				numWalls = self.countNumWallsNeighbors( mat=inMat, xPos=x, yPos=y, dist=1 );
				if ( numWalls <= 3 ):
					outMat[y][x] = 0;
					
	def gain( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(numRows):
			for x in range(numCols):
				numWalls = self.countNumWallsNeighbors( mat=inMat, xPos=x, yPos=y, dist=1 );
				if ( numWalls == 1 ):
					outMat[y][x] = 1;	
					try:				
						outMat[y-1][x] = 1;
						outMat[y+1][x] = 1;
						outMat[y][x-1] = 1;
						outMat[y-1][x+1] = 1;
						outMat[y-1][x-1] = 1;
						outMat[y+1][x+1] = 1;
						outMat[y-1][x-1] = 1;
						outMat[y+1][x+1] = 1;	
					except:
						pass				
					
					
	def wave( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(100):
			outMat[int(random.random() * len(inMat))][int(random.random() * len(inMat))] = 0;	
				
										
	def addBlobs( self, inMat, outMat ):
		numRows = len(inMat);
		numCols = len(inMat[0]);

		#print numRows, numCols
		
		for y in range(numRows):
			for x in range(numCols):
				numWalls = self.countNumWallsNeighbors( mat=inMat, xPos=x, yPos=y, dist=1 );
				if ( numWalls >= 3 ):
					for i in range(2):
						for j in range(2):
							try:
								outMat[y+i][x+j] = 1;	
							except:
								pass
				else:
					outMat[y][x] = 0;
				
				if (random.random() < 0.01):				
					for i in range(-2, 3):
						for j in range(-3, 3):
							try:
								outMat[y+i][x+j] = 0;
							except:
								pass
						
				if (random.random() > 0.99):				
					for i in range(-2, 3):
						for j in range(-3, 3):
							try:
								outMat[y+i][x+j] = 1;
							except:
								pass						
	
		
	'''
	 * 
	 * @param	mat		Matrix of data (0=empty, 1 = wall)
	 * @param	xPos	Column we are examining
	 * @param	yPos	Row we are exampining
	 * @param	dist	Radius of how far to check for neighbors
	 * 
	 * @return	Number of walls around the target, including itself
	'''
	def countNumWallsNeighbors( self, mat, xPos, yPos, dist = 1 ):
		count = 0;
		
		for y in range(-dist, dist+1):
			for x in range(-dist, dist+1):
				# Boundary
				if ( xPos + x < 0 or xPos + x > self._numTilesCols - 1 or yPos + y < 0 or yPos + y > self._numTilesRows - 1 ):
					 continue
					
				# Neighbor is non-wall
				if ( mat[yPos + y][xPos + x] != 0 ):
					count += 1;

		return count			
		


	'''
	 * 
	 * @return Returns a matrix of a cave!
	'''
	def generateCaveLevel(self):
		# Initialize random array
		
		#print 'Generating new Cave', self._numTilesCols, self._numTilesRows
		
		mat = self.genInitMatrix(self._numTilesRows, self._numTilesCols)
		
		for y in range(self._numTilesRows):
			for x in range(self._numTilesCols):
				mat[y][x] = 1 if random.random() < self.initWallRatio else 0 
		
		# Secondary buffer
		mat2 = self.genInitMatrix( self._numTilesRows, self._numTilesCols );
		
		# Run automata
		for i in range(self.numSmoothingIterations):
			self.runCelluarAutomata(inMat=mat,outMat=mat2)
				
			# Swap
			temp = mat;
			mat = mat2;
			mat2 = temp;	

		return mat	
		
		
	'''
	 * 
	 * @return Returns a matrix of a cave!
	'''
	def generateCaveLevel_withSaveImage(self):
		# Initialize random array
		
		#print 'Generating new Cave', self._numTilesCols, self._numTilesRows
		
		mat = self.genInitMatrix(self._numTilesRows, self._numTilesCols)
		
		for y in range(self._numTilesRows):
			for x in range(self._numTilesCols):
				mat[y][x] = 1 if random.random() < self.initWallRatio else 0 
		
		# Secondary buffer
		mat2 = self.genInitMatrix( self._numTilesRows, self._numTilesCols );
		
		# Run automata
		for i in range(self.numSmoothingIterations):
			
			
			
			if i % 10 == 9 or i % 10 == 8 :
				self.gain(inMat=mat,outMat=mat2)
			else:
				self.runCelluarAutomata(inMat=mat,outMat=mat2)
			
			self.wave(inMat=mat,outMat=mat2)		
#			if i % 15 == 14:
#				print 'eroding', i
#				self.erode(inMat=mat, outMat=mat2)
#			elif i % 15 == 5:
#				print 'gain', i
#				self.gain(inMat=mat, outMat=mat2)				
#			else:
#				self.runCelluarAutomata(inMat=mat,outMat=mat2)
			

			
			# Swap
			temp = mat;
			mat = mat2;
			mat2 = temp;	
			
			mat3 = self.genInitMatrix( self._numTilesRows, self._numTilesCols );			
			self.runCelluarAutomataColor(inMat=mat,outMat=mat3)	
			self.saveCaveAsImage(mat=mat3, iteration=i)
			
			
		return mat					

	def saveCaveAsImage(self, mat, iteration=1):
		r = self.color[0] + 3;
		b = self.color[2] + 4;
		self.color = (r, 200, b)
		
		
		
		im = Image.new('RGB', (self._numTilesCols, self._numTilesRows))
		rgb_im = im.convert('RGB')

		
		for i in range(len(mat)):
			for j in range(len(mat[i])):
				if mat[j][i]==0:
					im.putpixel((j, i) , self.C_ocean)
				elif mat[j][i]==1 or mat[j][i]==2 or mat[j][i]==3:
					im.putpixel((j, i) , self.C_sand)
				else:
					im.putpixel((j, i) , self.C_land)				
#				if mat[j][i]==1:
#					count = self.countNumWallsNeighbors(mat=mat, xPos=j, yPos=i, dist=1)
#					if (count <= 4):
#						im.putpixel((j, i) , self.color)
#					else:
#						im.putpixel((j, i) ,  self.color2)
#				else:
#					im.putpixel((j, i) , 0)
		im.save('C:\_Files\image\\bits\\cells_fff' + str(iteration) +'.png', "PNG")
		
		
'''
			for j in len(mat[i]):
				print j,i
				if j==1:
					#im.putpixel((i,j) , 255)
					pass
		im.save('C:\_Files\image\hello' + iteration +'.jpg', "JPEG")
'''

cave = PyCaveGenerator()
cave.CaveGenerator(150,150)
matrix = cave.generateCaveLevel_withSaveImage()	
cave.saveCaveAsImage(matrix, iteration=1)	
print 'complete'
*/
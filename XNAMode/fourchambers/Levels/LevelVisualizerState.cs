using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class LevelVisualizerState : FlxState
    {
        private FlxCaveGenerator cave;
        private int[,] mainTilemapArray;
        private FlxTilemap mainTilemap;

        private int size = 100;

        override public void create()
        {
            base.create();
            
            cave = new FlxCaveGenerator(size, size);

            mainTilemap = new FlxTilemap();
            mainTilemap.auto = FlxTilemap.AUTO;
            
            mainTilemap.boundingBoxOverride = true;
            add(mainTilemap);




            regenCave();



        }

        public void regenCave()
        {
            
            cave.initWallRatio = 0.5f;
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(size, size);

            int[] solidColumnsBeforeSmooth = new int[] { 0, 1, size - 1, size - 2 };
            int[] solidRowsBeforeSmooth = new int[] { 0, 1, size - 1, size - 2 };

            int[] emptyColumnsBeforeSmooth = new int[] { 10, 20, 30 };
            int[] emptyRowsBeforeSmooth = new int[] { 10, 20, 30 };

            int[] solidColumnsAfterSmooth = new int[] { 0, 1, size - 1, size - 2 };
            int[] solidRowsAfterSmooth = new int[] { 0, 1, size - 1, size - 2 };

            int[] emptyColumnsAfterSmooth = new int[] { 10, 20, 30 };
            int[] emptyRowsAfterSmooth = new int[] { 10, 20, 30 };

            mainTilemapArray = cave.generateCaveLevel(solidRowsBeforeSmooth, solidColumnsBeforeSmooth, solidRowsAfterSmooth, solidColumnsAfterSmooth, emptyRowsBeforeSmooth, emptyColumnsBeforeSmooth, emptyRowsAfterSmooth, emptyColumnsAfterSmooth);
            string newMap = cave.convertMultiArrayToString(mainTilemapArray);

            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/palette"), 1, 1);
        
        }

        override public void update()
        {


            if (FlxG.mouse.pressed())
            {
                regenCave();

            }

            base.update();
        }


    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class LevelVisualizerState : FlxState
    {
        private FlxTilemap mainTilemap;

        private int sizex = 23;
        private int sizey = 23;

        private FlxCaveGeneratorExt caveExt;
        private string[,] tiles;

        Dictionary<string, string> destructableAttrs;
        private FlxTilemap destructableTilemap;

        private FlxSprite collider;

        override public void create()
        {
            FlxG.backColor = Color.DarkGray;
            base.create();

            // 32, 4
            //flip x and y here.
            caveExt = new FlxCaveGeneratorExt(sizey, sizex);
            caveExt.numSmoothingIterations = 5;
            caveExt.initWallRatio = 0.500f;
            tiles = caveExt.generateCaveLevel();
            caveExt.printCave(tiles);
            Console.WriteLine("\n");
            //tiles = caveExt.addDecorations(tiles);
            //caveExt.printCave(tiles);
            string newMap = caveExt.convertMultiArrayStringToString(tiles);

            mainTilemap = new FlxTilemap();
            //mainTilemap.auto = FlxTilemap.STRING;
            //mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("diagnostic/testpalette"), 1, 1);
            //mainTilemap.boundingBoxOverride = true;
            add(mainTilemap);

            destructableAttrs = new Dictionary<string, string>();
            destructableAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/levelTutorial.oel", "level/IndestructableTerrain");

            string baseMap = destructableAttrs["IndestructableTerrain"];

            string addedMap = caveExt.addStrings(baseMap, newMap, 12, 4, sizex, sizey);
            //Console.WriteLine(addedMap);

            //destructableTilemap = new FlxTilemap();
            //destructableTilemap.auto = FlxTilemap.STRING;
            //destructableTilemap.loadMap(addedMap, FlxG.Content.Load<Texture2D>("flixel/autotiles"), 2, 2);
            //destructableTilemap.boundingBoxOverride = true;
            //add(destructableTilemap);
            ////destructableTilemap.collideIndex = 16;
            //destructableTilemap.collideMin = 1;
            //destructableTilemap.collideMax = 12;

            //Console.WriteLine(newMap);

            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;
            destructableTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("flixel/autotiles"), 8, 8);
            destructableTilemap.boundingBoxOverride = true;
            add(destructableTilemap);
            //destructableTilemap.collideIndex = 16;
            destructableTilemap.collideMin = 1;
            destructableTilemap.collideMax = 20;




            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"), 0, 0);

            collider = new FlxSprite(40, 40).createGraphic(2,2,new Color(255,0,0));
            add(collider);
            collider.acceleration.Y = 49;

            FlxG.follow(collider, 1.0f);


        }

        

        public void regenCave()
        {
            caveExt = new FlxCaveGeneratorExt(sizex, sizey);
            tiles = caveExt.generateCaveLevel();
            //caveExt.printCave(tiles);
            string newMap = caveExt.convertMultiArrayStringToString(tiles);
            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("diagnostic/testpalette"), 1, 1);

        }

        override public void update()
        {
            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            if (FlxG.keys.R)
            {
                regenCave();

                FlxG.quake.start(0.2f, 2.0f);


            }

            if (FlxG.keys.SPACE)
            {
                FlxG.state = new BasePlayState();
            }

            //FlxU.overlap(FlxG.mouse.cursor, destructableTilemap, overlaps);
            
            FlxU.collide(collider, destructableTilemap);

            if (FlxG.mouse.justPressedLeftButton())
            {
                collider.reset(FlxG.mouse.x, FlxG.mouse.y);

            }





            if (FlxG.keys.Y)
            {
                Vector2 v = new Vector2();
                bool red = destructableTilemap.ray(8, 8, (int)collider.x, (int)collider.y, v, 1);
                Console.WriteLine(red + " {0} {1} -> {2} {3} ", (int)collider.x, (int)collider.y, v.X, v.Y);
            }

            base.update();

        }

        protected bool overlaps(object Sender, FlxSpriteCollisionEvent e)
        {
            Console.WriteLine("overlappwith ", e.Object2);
            return true;
        }

    }
}









/*
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

        private int sizex = 10;
        private int sizey = 40;

        private FlxCaveGeneratorExt caveExt;
        private string[,] tiles;
        Dictionary<string, string> levelAttrs;


        override public void create()
        {
            base.create();
            
            cave = new FlxCaveGenerator(sizex, sizey);

            mainTilemap = new FlxTilemap();
            //mainTilemap.auto = FlxTilemap.AUTO;
            //mainTilemap.boundingBoxOverride = true;
            //add(mainTilemap);

            //regenCave();

            caveExt = new FlxCaveGeneratorExt(sizex, sizey);

            tiles = caveExt.generateCaveLevel();

            //for (int i = 0; i < tiles.GetLength(1); i++)
            //{
            //    for (int y = 0; y < tiles.GetLength(0); y++)
            //    {
            //        Console.Write(tiles[y, i]);
            //    }

            //    Console.WriteLine();
            //}

            caveExt.printCave(tiles);

            //First build a dictionary of levelAttrs
            //This will determine how the level is built.

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            levelAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/level1.oel", "level");

            Console.WriteLine("----------------------------------" + levelAttrs);

            //foreach (KeyValuePair<string, string> kvp in levelAttrs)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}",
            //        kvp.Key, kvp.Value);
            //}

            List<Dictionary<string,string>> levelNodes = FlxXMLReader.readNodesFromOelFile("ogmoLevels/level1.oel", "level/ActorsLayer");

            //foreach (Dictionary<string,string> nodes in levelNodes)
            //{
            //    foreach (KeyValuePair<string, string> kvp in nodes)
            //    {
            //        Console.Write("Key = {0}, Value = {1}, ",
            //            kvp.Key, kvp.Value);
            //    }
            //    Console.Write("\r\n");
            //}

            levelAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/level1.oel", "level/TilesLayer");

            //Console.WriteLine("----------------------------------" + levelAttrs);

            //foreach (KeyValuePair<string, string> kvp in levelAttrs)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}",
            //        kvp.Key, kvp.Value);
            //}

            mainTilemap = new FlxTilemap();
            mainTilemap.auto = FlxTilemap.STRING;
            mainTilemap.loadMap(cave.dataAsString, FlxG.Content.Load<Texture2D>("diagnostic/testpalette"), 1, 1);
            mainTilemap.boundingBoxOverride = true;
            add(mainTilemap);

        }

        public void regenCave()
        {
            cave.initWallRatio = 0.5f;
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(sizex, sizey);

            int[] solidColumnsBeforeSmooth = new int[] { 0, 1, sizex - 1, sizex - 2, 1000 };
            int[] solidRowsBeforeSmooth = new int[] { 0, 1, sizey - 1, sizey - 2, sizey - 4, sizey - 5, sizey - 6,  1000 };

            int[] emptyColumnsBeforeSmooth = new int[] { 10, 20, 30, 1000 };
            int[] emptyRowsBeforeSmooth = new int[] { 10, 20, 30, 1000 };

            int[] solidColumnsAfterSmooth = new int[] { 0, 1, sizex - 1, sizex - 2, sizex - 3, sizex - 4, sizex - 5, 1000 };
            int[] solidRowsAfterSmooth = new int[] { 0, 1, sizey - 1, sizey - 2, sizey - 3, sizey - 4, sizey - 5, sizey - 6, 1000 };

            int[] emptyColumnsAfterSmooth = new int[] { 10, 20, 30, 1000 };
            int[] emptyRowsAfterSmooth = new int[] { 10, 20, 30, 1000 };

            mainTilemapArray = cave.generateCaveLevel(solidRowsBeforeSmooth, solidColumnsBeforeSmooth, solidRowsAfterSmooth, solidColumnsAfterSmooth, emptyRowsBeforeSmooth, emptyColumnsBeforeSmooth, emptyRowsAfterSmooth, emptyColumnsAfterSmooth);
            
            mainTilemapArray = cave.addChunks(mainTilemapArray, 10, 10, 30, 1);

            mainTilemapArray = cave.addChunks(mainTilemapArray, 10, 10, 30, 0);

            mainTilemapArray = cave.drawPath(mainTilemapArray, sizex - 5, 5, 5, sizey - 15, 0);

            string newMap = cave.convertMultiArrayToString(mainTilemapArray);

            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("diagnostic/testpalette"), 1, 1);
        
        }

        override public void update()
        {


            if (FlxG.keys.R )
            {
                //regenCave();

                caveExt.initWallRatio = 0.5f;
                caveExt.numSmoothingIterations = 5;
                caveExt.genInitMatrix(sizex, sizey);
                tiles = caveExt.generateCaveLevel();
                
                caveExt.printCave(tiles);
            }

            if (FlxG.keys.SPACE)
            {
                FlxG.state = new BasePlayState();
            }

            base.update();
        }


    }
}
*/
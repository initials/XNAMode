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
    public class BasePlayState : FlxState
    {
        Dictionary<string, string> levelAttrs;

        /// <summary>
        /// Holds the back clouds, gradient etc.
        /// </summary>
        private FlxTileblock bgTiles;

        
        /// <summary>
        /// The main tile map. Collisions etc happen on this.
        /// </summary>
        private FlxTilemap mainTilemap;

        /// <summary>
        /// Decorations tile map
        /// </summary>
        /// 
        private FlxTilemap decorationsTilemap;

        override public void create()
        {
            base.create();

            //important to reset the hud to get the text, gamepad buttons out.
            FlxG.resetHud();
            FlxG.showHud();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            //First build a dictionary of levelAttrs
            //This will determine how the level is built.

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level
            string levelname = "level_" + FlxG.level.ToString();

            XElement xelement = XElement.Load("levelDetails.xml");

            foreach (XElement xEle in xelement.Descendants(levelname).Elements())
            {
                if (xEle.Value.ToString() == "")
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                }
                else
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Value.ToString());
                }
            }

            // Large bg tile.
            bgTiles = new FlxTileblock(0, 0, FlxG.width + 48, FlxG.height / 2);
            bgTiles.loadTiles(FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["bgGraphic"]), 48, 64, 0);
            bgTiles.scrollFactor.X = 0.02f;
            bgTiles.scrollFactor.Y = 0.02f;
            bgTiles.boundingBoxOverride = false;
            add(bgTiles);


            // Generate the levels caves/tiles.

            FlxCaveGenerator cav = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["width"]), Convert.ToInt32(levelAttrs["height"]));
            cav.initWallRatio = (float)Convert.ToDouble(levelAttrs["startCaveGenerateBias"]);
            cav.numSmoothingIterations = 5;
            cav.genInitMatrix(Convert.ToInt32(levelAttrs["width"]), Convert.ToInt32(levelAttrs["height"]));

            int[,] matr = cav.generateCaveLevel(3, 0, 2, 0, 1, 0, 1, 0);

            string newMap = cav.convertMultiArrayToString(matr);

            mainTilemap = new FlxTilemap();
            mainTilemap.auto = FlxTilemap.AUTO;
            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["tiles"]), 16, 16);
            add(mainTilemap);

            // add the decorations tilemap.

            int[,] decr = cav.createDecorationsMap(matr);
            string newDec = cav.convertMultiArrayToString(decr);
            Texture2D DecorTex = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["decorationTiles"]);

            decorationsTilemap = new FlxTilemap();
            decorationsTilemap.auto = FlxTilemap.RANDOM;
            decorationsTilemap.randomLimit = (int)DecorTex.Width / 16;

            decorationsTilemap.loadMap(newDec, DecorTex, 16, 16);
            add(decorationsTilemap);




        }

        override public void update()
        {




            base.update();
        }


    }
}

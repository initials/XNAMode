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
        /// <summary>
        /// The leading of the camera
        /// </summary>
        private const float FOLLOW_LERP = 3.0f;

        /// <summary>
        /// How many bullets to create for each actor.
        /// </summary>
        private const int BULLETS_PER_ACTOR = 100;

        /// <summary>
        /// A holder for all of the level data to generate a level from.
        /// </summary>
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
        private FlxTilemap decorationsTilemap;

        /// <summary>
        /// An array of the decorations map.
        /// </summary>
        private int[,] decorationsArray;

        /// <summary>
        /// A cave generator object that creates the level
        /// </summary>
        private FlxCaveGenerator cave;

        // --- FlxGroups, for overlap collide.

        /// <summary>
        /// Fireballs for the warlock
        /// </summary>
        protected FlxGroup fireballs;

        /// <summary>
        /// Arrows for the Marksman
        /// </summary>
        protected FlxGroup arrows;

        /// <summary>
        /// Complete group of all the actors.
        /// </summary>
        private FlxGroup actors;

        /// <summary>
        /// Every single bullet in the scene.
        /// </summary>
        protected FlxGroup bullets;

        private Automaton automaton;
        private Corsair corsair;
        private Executor executor;
        private Gloom gloom;
        private Harvester harvester;

        /// <summary>
        /// A Marksman player actor.
        /// Can shoot arrows.
        /// </summary>
        private Marksman marksman;
        private Medusa medusa;
        private Mistress mistress;
        private Mummy mummy;
        private Nymph nymph;
        private Paladin paladin;
        private Seraphine seraphine;
        private Succubus succubus;
        private Tormentor tormentor;
        private Unicorn unicorn;
        private Warlock warlock;
        private Vampire vampire;
        private Zombie zombie;

        override public void create()
        {
            base.create();

            //important to reset the hud to get the text, gamepad buttons out.
            FlxG.resetHud();
            FlxG.showHud();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            // initialize a bunch of groups
            actors = new FlxGroup();
            fireballs = new FlxGroup();
            bullets = new FlxGroup();
            arrows = new FlxGroup();


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

            cave = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["width"]), Convert.ToInt32(levelAttrs["height"]));
            cave.initWallRatio = (float)Convert.ToDouble(levelAttrs["startCaveGenerateBias"]);
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(Convert.ToInt32(levelAttrs["width"]), Convert.ToInt32(levelAttrs["height"]));

            int[,] matr = cave.generateCaveLevel(3, 0, 2, 0, 1, 0, 1, 0);

            string newMap = cave.convertMultiArrayToString(matr);

            mainTilemap = new FlxTilemap();
            mainTilemap.auto = FlxTilemap.AUTO;
            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["tiles"]), 16, 16);
            add(mainTilemap);

            // add the decorations tilemap.

            decorationsArray = cave.createDecorationsMap(matr);
            string newDec = cave.convertMultiArrayToString(decorationsArray);
            Texture2D DecorTex = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["decorationTiles"]);

            decorationsTilemap = new FlxTilemap();
            decorationsTilemap.auto = FlxTilemap.RANDOM;
            decorationsTilemap.randomLimit = (int)DecorTex.Width / 16;

            decorationsTilemap.loadMap(newDec, DecorTex, 16, 16);
            add(decorationsTilemap);




            // build characters here


            // build atmospheric effects here




        }

        override public void update()
        {
            base.update();
        }



        public void buildActor(string ActorType, int NumberOfActors)
        {
            #region Marksman

            if (ActorType == "Marksman")
            {
                for (int i = 0; i < BULLETS_PER_ACTOR; i++)
                    arrows.add(new Arrow());
                bullets.add(arrows);

                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    marksman = new Marksman(p[1] * 16, p[0] * 16, arrows.members);
                    actors.add(marksman);
                }
            }
            #endregion

        }

    }
}

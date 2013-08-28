﻿using System;
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

        private FlxSprite bgSprite;

        /// <summary>
        /// Tells you the time of day (between 0.0f and 24.99f)
        /// </summary>
        private float timeOfDay = 0.0f;

        /// <summary>
        /// Helper to determine how fast time passes.
        /// </summary>
        private float timeScale = 10.10f;

        /// <summary>
        /// Helper to tint the game based on the time of day.
        /// </summary>
        private Texture2D paletteTexture;

        /// <summary>
        /// The main tile map. Collisions etc happen on this.
        /// </summary>
        private FlxTilemap mainTilemap;

        /// <summary>
        /// Decorations tile map
        /// </summary>
        private FlxTilemap decorationsTilemap;

        /// <summary>
        /// A tile map of all the ladders.
        /// </summary>
        private FlxTilemap ladderTilemap;

        /// <summary>
        /// An array of the decorations map.
        /// </summary>
        private int[,] decorationsArray;

        /// <summary>
        /// An array of the main level
        /// </summary>
        private int[,] mainTilemapArray;

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

        private FlxEmitter blood;

        private Artist artist;
        private Assassin assassin;
        private Automaton automaton;
        private Bat bat;
        private Blight blight;
        private Bloatedzombie bloatedzombie;
        private Bogbeast bogbeast;
        private Bombling bombling;
        private Centaur centaur;
        private Chicken chicken;
        private Chimaera chimaera;
        private Corsair corsair;
        private Cow cow;
        private Cyclops cyclops;
        private Deathclaw deathclaw;
        private Deer deer;
        private Devil devil;
        private Djinn djinn;
        private Drone drone;
        private Druid druid;
        private Dwarf dwarf;
        private Embersteed embersteed;
        private Executor executor;
        private Feline feline;
        private Floatingeye floatingeye;
        private Fungant fungant;
        private Gelatine gelatine;
        private Gloom gloom;
        private Glutton glutton;
        private Goblin goblin;
        private Golem golem;
        private Gorgon gorgon;
        private Gourmet gourmet;
        private Grimwarrior grimwarrior;
        private Grizzly grizzly;
        private Harvester harvester;
        private Horse horse;
        private Ifrit ifrit;
        private Imp imp;
        private Kerberos kerberos;
        private Lich lich;
        private Lion lion;
        /// <summary>
        /// A Marksman player actor.
        /// Can shoot arrows.
        /// </summary>
        private Marksman marksman;
        private Mechanic mechanic;
        private Mephisto mephisto;
        private Merchant merchant;
        private Mermaid mermaid;
        private Mimick mimick;
        private Mistress mistress;
        private Monk monk;
        private Mummy mummy;
        private Nightmare nightmare;
        private Nymph nymph;
        private Ogre ogre;
        private Paladin paladin;
        private Phantom phantom;
        private Priest priest;
        private Prism prism;
        private Rat rat;
        private Savage savage;
        private Seraphine seraphine;
        private Sheep sheep;
        private Skeleton skeleton;
        private Snake snake;
        private Soldier soldier;
        private Sphinx sphinx;
        private Spider spider;
        private Succubus succubus;
        private Tauro tauro;
        private Toad toad;
        private Tormentor tormentor;
        private Treant treant;
        private Troll troll;
        private Unicorn unicorn;
        private Vampire vampire;
        private Warlock warlock;
        private Willowisp willowisp;
        private Wizard wizard;
        private Wolf wolf;
        private Zinger zinger;
        private Zombie zombie;


        override public void create()
        {

            base.create();

            //important to reset the hud to get the text, gamepad buttons out.
            FlxG.resetHud();
            FlxG.showHud();

            //FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            // initialize a bunch of groups
            actors = new FlxGroup();
            fireballs = new FlxGroup();
            bullets = new FlxGroup();
            arrows = new FlxGroup();


            //First build a dictionary of levelAttrs
            //This will determine how the level is built.

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            string currentLevel = "l" + FlxG.level.ToString();

            XElement xelement = XElement.Load("levelSettings.xml");

            foreach (XElement xEle in xelement.Descendants("settings").Elements())
            {
                XElement firstSpecificChildElement = xEle.Element(currentLevel);

                if (firstSpecificChildElement != null )
                {
                    if (firstSpecificChildElement.Value.ToString()=="") 
                    {
                        levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());

                        //Console.WriteLine(" empty string " + xEle.Name.ToString() + "  " +  xEle.Attribute("default").Value.ToString());
                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");

                        if (playerControlled != null)
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }

                    }
                    else 
                    {
                        levelAttrs.Add(xEle.Name.ToString(), firstSpecificChildElement.Value.ToString());
                        
                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");

                        if (playerControlled != null )
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }
                    }
                }
                else
                {

                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());

                }
            }


            
            // Large bg tile.
            //bgTiles = new FlxTileblock(0, 0, FlxG.width + 48, FlxG.height / 2);
            //bgTiles.loadTiles(FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["bgGraphic"]), 48, 64, 0);
            //bgTiles.scrollFactor.X = 0.02f;
            //bgTiles.scrollFactor.Y = 0.02f;
            //bgTiles.boundingBoxOverride = false;
            //add(bgTiles);

            Texture2D bgGraphic = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["bgGraphic"]);


            bgSprite = new FlxSprite(0, 0, bgGraphic);

            bgSprite.loadGraphic(bgGraphic);
            //bgSprite.loadTiles(FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["bgGraphic"]), 48, 64, 0);
            bgSprite.scrollFactor.X = 0.4f;
            bgSprite.scrollFactor.Y = 0.4f;
            bgSprite.x = 0;
            bgSprite.y = 0;
            bgSprite.color = Color.DarkGray;

            bgSprite.boundingBoxOverride = false;
            add(bgSprite);

            // Generate some ladders

            FlxCaveGenerator ladderCave = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));
            int[,] ladderMatr = ladderCave.generateLadderLevel( (Convert.ToInt32(levelAttrs["ladders"])), 2, 10);
            string ladderMap = ladderCave.convertMultiArrayToString(ladderMatr);

            ladderTilemap = new FlxTilemap();
            //ladderTilemap.auto = FlxTilemap.AUTO;
            ladderTilemap.auto = FlxTilemap.RANDOM;
            ladderTilemap.randomLimit = 2;
            ladderTilemap.loadMap(ladderMap, FlxG.Content.Load<Texture2D>("initials/ladderTiles_16x16"), 16, 16);
            ladderTilemap.boundingBoxOverride = true;

            add(ladderTilemap);



            // Generate the levels caves/tiles.

            cave = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));
            cave.initWallRatio = (float)Convert.ToDouble(levelAttrs["startCaveGenerateBias"]);
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));

            //int[,] matr = cave.generateCaveLevel(3, 0, 2, 0, 1, 0, 1, 0);


            int[] solidColumnsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidColumnsBeforeSmooth"]);
            int[] solidRowsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidRowsBeforeSmooth"]);

            int[] emptyColumnsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyColumnsBeforeSmooth"]);
            int[] emptyRowsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyRowsBeforeSmooth"]);

            int[] solidColumnsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidColumnsAfterSmooth"]);
            int[] solidRowsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidRowsAfterSmooth"]);

            int[] emptyColumnsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyColumnsAfterSmooth"]);
            int[] emptyRowsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyRowsAfterSmooth"]);


            mainTilemapArray = cave.generateCaveLevel(solidRowsBeforeSmooth, solidColumnsBeforeSmooth, solidRowsAfterSmooth, solidColumnsAfterSmooth, emptyRowsBeforeSmooth, emptyColumnsBeforeSmooth, emptyRowsAfterSmooth, emptyColumnsAfterSmooth);


            string newMap = cave.convertMultiArrayToString(mainTilemapArray);

            mainTilemap = new FlxTilemap();
            mainTilemap.auto = FlxTilemap.AUTO;
            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["tiles"]), 16, 16);
            mainTilemap.boundingBoxOverride = true;

            add(mainTilemap);
            

            // add the decorations tilemap.

            decorationsArray = cave.createDecorationsMap(mainTilemapArray);
            string newDec = cave.convertMultiArrayToString(decorationsArray);
            Texture2D DecorTex = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["decorationTiles"]);

            decorationsTilemap = new FlxTilemap();
            decorationsTilemap.auto = FlxTilemap.RANDOM;
            decorationsTilemap.randomLimit = (int)DecorTex.Width / 16;
            decorationsTilemap.boundingBoxOverride = false;
            decorationsTilemap.loadMap(newDec, DecorTex, 16, 16);
            //add it after the actors.

            // build characters here

            /// Looks through the level dictionary and builds neccessary actors.
            foreach (KeyValuePair<string, string> pair in levelAttrs)
            {
                int noa = 0;
                if (pair.Value != null && pair.Value != "")
                {
                    int number;
                    bool result = Int32.TryParse(pair.Value.ToString(), out number);
                    if (result)
                    {
                        noa = number;
                    }
                    else
                    {
                        noa = 0;
                    }
                    if (pair.Value != "" && pair.Value != null && pair.Value != "0")
                    {
                        if (noa != 0)
                        {
                            buildActor(pair.Key, Convert.ToInt32(pair.Value));
                        }
                    }
                }
            }

            // build atmospheric effects here

            paletteTexture = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["timeOfDayPalette"]);



            //FlxG.followAdjust(0.5f, 0.0f);
            FlxG.followBounds(0, 0, Convert.ToInt32(levelAttrs["levelWidth"]) * 16, Convert.ToInt32(levelAttrs["levelHeight"]) * 16);

            add(actors);
            add(bullets);

            blood = new FlxEmitter();
            blood.x = 0;
            blood.y = 0;
            blood.width = 2;
            blood.height = 2;
            blood.delay = 0.8f;
            //blood.del
            blood.setXSpeed(-22, 22);
            blood.setYSpeed(-5, 5);
            blood.setRotation(0, 0);
            blood.gravity = 98;
            blood.createSprites(FlxG.Content.Load<Texture2D>("initials/blood"), 1500, true, 1.0f, 1.0f);
            

            add(blood);

            add(decorationsTilemap);

            //FlxG.autoHandlePause = true;

            FlxG.resetHud();
            FlxG.showHud();


        }

        override public void update()
        {
            if (FlxG.keys.justPressed(Keys.F9) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.level++;
                if (FlxG.level > 25) FlxG.level = 1;

                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");


                FlxG.transition.startFadeIn(0.2f);

                FlxG.state = new BasePlayState();

                return;
            }
            else if (FlxG.keys.justPressed(Keys.F7) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.level--;
                if (FlxG.level < 1) FlxG.level = 25;

                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                
                FlxG.transition.startFadeIn(0.2f);

                FlxG.state = new BasePlayState();

                return;
            }
            else if (FlxG.keys.justPressed(Keys.F8) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                FlxG.transition.startFadeIn(0.2f);
                FlxG.state = new BasePlayState();
                return;
            }



            FlxG.setHudText(1, FlxG.score.ToString());


            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            //FlxG.write("Paused? " + FlxG.pause);
            // pause

            //if (FlxG.keys.justPressed(Keys.P) || FlxG.gamepads.isNewButtonPress(Buttons.Start))
            //{
            //    FlxG.write("Paused");

            //    if (FlxG.pause == true) FlxG.pause = false;
            //    else if (FlxG.pause == false) FlxG.pause = true;
            //}

            //calculate time of day.
            timeOfDay += FlxG.elapsed * timeScale;
            if (timeOfDay > 24.99f) timeOfDay = 0.0f;
            //timeOfDay = timeOfDayTotal / timeScale;

            // color bg tiles
            //bgTiles.color = FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, 1);
            
            // color whole game.
            FlxG.color(FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, 1));

            //collides
            FlxU.collide(actors, mainTilemap);

            FlxU.overlap(actors, bullets, overlapped);
            
            FlxU.collide(mainTilemap, bullets);

            FlxU.collide(blood, mainTilemap);




            if (FlxG.mouse.pressedRightButton())
            {
                
                mainTilemap.setTile((int)FlxG.mouse.x / 16, (int)FlxG.mouse.y / 16, 0, true);
                
                decorationsTilemap.setTile((int)FlxG.mouse.x / 16, ((int)FlxG.mouse.y / 16) - 1, 0, true);
            }
            if (FlxG.mouse.pressedLeftButton())
            {
                mainTilemap.setTile((int)FlxG.mouse.x / 16, (int)FlxG.mouse.y / 16, 1, true);
            }





            base.update();



        }

        protected bool overlapWithLadder(object Sender, FlxSpriteCollisionEvent e)
        {
            //((Actor)(e.Object2)).canClimbLadder = true;
            return true;
        }

        //actors, bullets
        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {
            /*
            if ((e.Object1 is BotBullet) || (e.Object1 is Bullet))
                e.Object1.kill();
            e.Object2.hurt(1);
            return true;
             */

            // First reject Actors and their bullets.
            if ((e.Object1 is Warlock) && (e.Object2 is Fireball))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is Arrow))
            {

            }
            else if ((e.Object1 is Mistress) && (e.Object2 is WhipHitBox))
            {

            }
            // Now that it's a kill, spurt some blood and "hurt" both parties.
            else if (e.Object1.dead == false && e.Object2.dead == false)
            {
                e.Object1.acceleration.Y = 820;
                e.Object1.velocity.X = e.Object2.velocity.X ;
                e.Object1.velocity.Y = e.Object2.velocity.Y ;

                e.Object1.hurt(1);

                e.Object2.kill();

                blood.at(e.Object1);

                blood.start(true, 0, 10);
            }

            return true;

        }
        public void buildActor(string ActorType, int NumberOfActors)
        {
            #region Marksman
            if (ActorType == "marksman")
            {
                for (int i = 0; i < BULLETS_PER_ACTOR; i++)
                    arrows.add(new Arrow());
                bullets.add(arrows);

                for (int i = 0; i < NumberOfActors; i++)
                {
                    //FlxG.write("Marksman being made " + NumberOfActors);

                    int[] p = cave.findRandomSolid(decorationsArray);
                    marksman = new Marksman(p[1] * 16, p[0] * 16, arrows.members);
                    actors.add(marksman);
                }

                if (levelAttrs["playerControlled"] == "marksman")
                {
                    marksman.isPlayerControlled = true;
                    FlxG.follow(marksman, FOLLOW_LERP);
                }
            }
            #endregion

            #region Mistress
            if (ActorType == "mistress")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mistress = new Mistress(p[1] * 16, p[0] * 16);
                    actors.add(mistress);
                    bullets.add(mistress.whipHitBox);

                }

                if (levelAttrs["playerControlled"] == "mistress")
                {
                    mistress.isPlayerControlled = true;
                    FlxG.follow(mistress, FOLLOW_LERP);
                }
            }
            #endregion
            
            #region Warlock
            if (ActorType == "warlock")
            {
                int x = 0;
                for (x = 0; x < BULLETS_PER_ACTOR; x++)
                    fireballs.add(new Fireball());
                bullets.add(fireballs);
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    warlock = new Warlock(p[1] * 16, p[0] * 16, fireballs.members);
                    actors.add(warlock);
                }
                if (levelAttrs["playerControlled"] == "warlock")
                {
                    warlock.isPlayerControlled = true;
                    FlxG.follow(warlock, FOLLOW_LERP);
                }
            }
            #endregion

            #region Artist
            if (ActorType == "artist")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    artist = new Artist(p[1] * 16, p[0] * 16);
                    actors.add(artist);
                }
            }
            #endregion
            #region Assassin
            if (ActorType == "assassin")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    assassin = new Assassin(p[1] * 16, p[0] * 16);
                    actors.add(assassin);
                }
            }
            #endregion
            #region Automaton
            if (ActorType == "automaton")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    automaton = new Automaton(p[1] * 16, p[0] * 16);
                    actors.add(automaton);
                    automaton.velocity.X = 50;

                }
            }
            #endregion
            #region Bat
            if (ActorType == "bat")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(mainTilemapArray );
                    bat = new Bat(p[1] * 16, p[0] * 16 - 50);
                    actors.add(bat);
                }
            }
            #endregion
            #region Blight
            if (ActorType == "blight")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    blight = new Blight(p[1] * 16, p[0] * 16);
                    actors.add(blight);
                }
            }
            #endregion
            #region Bloatedzombie
            if (ActorType == "bloatedzombie")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    bloatedzombie = new Bloatedzombie(p[1] * 16, p[0] * 16);
                    actors.add(bloatedzombie);
                }
            }
            #endregion
            #region Bogbeast
            if (ActorType == "bogbeast")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    bogbeast = new Bogbeast(p[1] * 16, p[0] * 16);
                    actors.add(bogbeast);
                }
            }
            #endregion
            #region Bombling
            if (ActorType == "bombling")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    bombling = new Bombling(p[1] * 16, p[0] * 16);
                    actors.add(bombling);
                }
            }
            #endregion
            #region Centaur
            if (ActorType == "centaur")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    centaur = new Centaur(p[1] * 16, p[0] * 16);
                    actors.add(centaur);
                }
            }
            #endregion
            #region Chicken
            if (ActorType == "chicken")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    chicken = new Chicken(p[1] * 16, p[0] * 16);
                    actors.add(chicken);
                }
            }
            #endregion
            #region Chimaera
            if (ActorType == "chimaera")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    chimaera = new Chimaera(p[1] * 16, p[0] * 16);
                    actors.add(chimaera);
                }
            }
            #endregion
            #region Corsair
            if (ActorType == "corsair")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    corsair = new Corsair(p[1] * 16, p[0] * 16);
                    actors.add(corsair);
                }
            }
            #endregion
            #region Cow
            if (ActorType == "cow")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    cow = new Cow(p[1] * 16, p[0] * 16);
                    actors.add(cow);
                }
            }
            #endregion
            #region Cyclops
            if (ActorType == "cyclops")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    cyclops = new Cyclops(p[1] * 16, p[0] * 16);
                    actors.add(cyclops);
                }
            }
            #endregion
            #region Deathclaw
            if (ActorType == "deathclaw")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    deathclaw = new Deathclaw(p[1] * 16, p[0] * 16);
                    actors.add(deathclaw);
                }
            }
            #endregion
            #region Deer
            if (ActorType == "deer")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    deer = new Deer(p[1] * 16, p[0] * 16);
                    actors.add(deer);
                }
            }
            #endregion
            #region Devil
            if (ActorType == "devil")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    devil = new Devil(p[1] * 16, p[0] * 16);
                    actors.add(devil);
                }
            }
            #endregion
            #region Djinn
            if (ActorType == "djinn")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    djinn = new Djinn(p[1] * 16, p[0] * 16);
                    actors.add(djinn);
                }
            }
            #endregion
            #region Drone
            if (ActorType == "drone")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    drone = new Drone(p[1] * 16, p[0] * 16);
                    actors.add(drone);
                }
            }
            #endregion
            #region Druid
            if (ActorType == "druid")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    druid = new Druid(p[1] * 16, p[0] * 16);
                    actors.add(druid);
                }
            }
            #endregion
            #region Dwarf
            if (ActorType == "dwarf")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    dwarf = new Dwarf(p[1] * 16, p[0] * 16);
                    actors.add(dwarf);
                }
            }
            #endregion
            #region Embersteed
            if (ActorType == "embersteed")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    embersteed = new Embersteed(p[1] * 16, p[0] * 16);
                    actors.add(embersteed);
                }
            }
            #endregion
            #region Executor
            if (ActorType == "executor")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    executor = new Executor(p[1] * 16, p[0] * 16);
                    actors.add(executor);
                }
            }
            #endregion
            #region Feline
            if (ActorType == "feline")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    feline = new Feline(p[1] * 16, p[0] * 16);
                    actors.add(feline);
                }
            }
            #endregion
            #region Floatingeye
            if (ActorType == "floatingeye")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    floatingeye = new Floatingeye(p[1] * 16, p[0] * 16);
                    actors.add(floatingeye);
                }
            }
            #endregion
            #region Fungant
            if (ActorType == "fungant")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    fungant = new Fungant(p[1] * 16, p[0] * 16);
                    actors.add(fungant);
                }
            }
            #endregion
            #region Gelatine
            if (ActorType == "gelatine")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    gelatine = new Gelatine(p[1] * 16, p[0] * 16);
                    actors.add(gelatine);
                }
            }
            #endregion
            #region Gloom
            if (ActorType == "gloom")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    gloom = new Gloom(p[1] * 16, p[0] * 16);
                    actors.add(gloom);
                }
            }
            #endregion
            #region Glutton
            if (ActorType == "glutton")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    glutton = new Glutton(p[1] * 16, p[0] * 16);
                    actors.add(glutton);
                }
            }
            #endregion
            #region Goblin
            if (ActorType == "goblin")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    goblin = new Goblin(p[1] * 16, p[0] * 16);
                    actors.add(goblin);
                }
            }
            #endregion
            #region Golem
            if (ActorType == "golem")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    golem = new Golem(p[1] * 16, p[0] * 16);
                    actors.add(golem);
                }
            }
            #endregion
            #region Gorgon
            if (ActorType == "gorgon")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    gorgon = new Gorgon(p[1] * 16, p[0] * 16);
                    actors.add(gorgon);
                }
            }
            #endregion
            #region Gourmet
            if (ActorType == "gourmet")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    gourmet = new Gourmet(p[1] * 16, p[0] * 16);
                    actors.add(gourmet);
                }
            }
            #endregion
            #region Grimwarrior
            if (ActorType == "grimwarrior")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    grimwarrior = new Grimwarrior(p[1] * 16, p[0] * 16);
                    actors.add(grimwarrior);
                }
            }
            #endregion
            #region Grizzly
            if (ActorType == "grizzly")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    grizzly = new Grizzly(p[1] * 16, p[0] * 16);
                    actors.add(grizzly);
                }
            }
            #endregion
            #region Harvester
            if (ActorType == "harvester")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    harvester = new Harvester(p[1] * 16, p[0] * 16);
                    actors.add(harvester);
                }
            }
            #endregion
            #region Horse
            if (ActorType == "horse")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    horse = new Horse(p[1] * 16, p[0] * 16);
                    actors.add(horse);
                }
            }
            #endregion
            #region Ifrit
            if (ActorType == "ifrit")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    ifrit = new Ifrit(p[1] * 16, p[0] * 16);
                    actors.add(ifrit);
                }
            }
            #endregion
            #region Imp
            if (ActorType == "imp")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    imp = new Imp(p[1] * 16, p[0] * 16);
                    actors.add(imp);
                }
            }
            #endregion
            #region Kerberos
            if (ActorType == "kerberos")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    kerberos = new Kerberos(p[1] * 16, p[0] * 16);
                    actors.add(kerberos);
                }
            }
            #endregion
            #region Lich
            if (ActorType == "lich")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    lich = new Lich(p[1] * 16, p[0] * 16);
                    actors.add(lich);
                }
            }
            #endregion
            #region Lion
            if (ActorType == "lion")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    lion = new Lion(p[1] * 16, p[0] * 16);
                    actors.add(lion);
                }
            }
            #endregion

            // marksman was here.

            #region Mechanic
            if (ActorType == "mechanic")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mechanic = new Mechanic(p[1] * 16, p[0] * 16);
                    actors.add(mechanic);
                }
            }
            #endregion
            #region Mephisto
            if (ActorType == "mephisto")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mephisto = new Mephisto(p[1] * 16, p[0] * 16);
                    actors.add(mephisto);
                }
            }
            #endregion
            #region Merchant
            if (ActorType == "merchant")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    merchant = new Merchant(p[1] * 16, p[0] * 16);
                    actors.add(merchant);
                }
            }
            #endregion
            #region Mermaid
            if (ActorType == "mermaid")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mermaid = new Mermaid(p[1] * 16, p[0] * 16);
                    actors.add(mermaid);
                }
            }
            #endregion
            #region Mimick
            if (ActorType == "mimick")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mimick = new Mimick(p[1] * 16, p[0] * 16);
                    actors.add(mimick);
                }
            }
            #endregion
            #region Monk
            if (ActorType == "monk")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    monk = new Monk(p[1] * 16, p[0] * 16);
                    actors.add(monk);
                }
            }
            #endregion
            #region Mummy
            if (ActorType == "mummy")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    mummy = new Mummy(p[1] * 16, p[0] * 16);
                    actors.add(mummy);
                }
            }
            #endregion
            #region Nightmare
            if (ActorType == "nightmare")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    nightmare = new Nightmare(p[1] * 16, p[0] * 16);
                    actors.add(nightmare);
                }
            }
            #endregion
            #region Nymph
            if (ActorType == "nymph")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    nymph = new Nymph(p[1] * 16, p[0] * 16);
                    actors.add(nymph);
                }
            }
            #endregion
            #region Ogre
            if (ActorType == "ogre")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    ogre = new Ogre(p[1] * 16, p[0] * 16);
                    actors.add(ogre);
                }
            }
            #endregion
            #region Paladin
            if (ActorType == "paladin")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    paladin = new Paladin(p[1] * 16, p[0] * 16);
                    actors.add(paladin);
                }
            }
            #endregion
            #region Phantom
            if (ActorType == "phantom")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    phantom = new Phantom(p[1] * 16, p[0] * 16);
                    actors.add(phantom);
                }
            }
            #endregion
            #region Priest
            if (ActorType == "priest")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    priest = new Priest(p[1] * 16, p[0] * 16);
                    actors.add(priest);
                }
            }
            #endregion
            #region Prism
            if (ActorType == "prism")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    prism = new Prism(p[1] * 16, p[0] * 16);
                    actors.add(prism);
                }
            }
            #endregion
            #region Rat
            if (ActorType == "rat")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    rat = new Rat(p[1] * 16, p[0] * 16);
                    actors.add(rat);
                }
            }
            #endregion
            #region Savage
            if (ActorType == "savage")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    savage = new Savage(p[1] * 16, p[0] * 16);
                    actors.add(savage);
                }
            }
            #endregion
            #region Seraphine
            if (ActorType == "seraphine")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    seraphine = new Seraphine(p[1] * 16, p[0] * 16);
                    actors.add(seraphine);
                }
            }
            #endregion
            #region Sheep
            if (ActorType == "sheep")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    sheep = new Sheep(p[1] * 16, p[0] * 16);
                    actors.add(sheep);
                }
            }
            #endregion
            #region Skeleton
            if (ActorType == "skeleton")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    skeleton = new Skeleton(p[1] * 16, p[0] * 16);
                    actors.add(skeleton);
                }
            }
            #endregion
            #region Snake
            if (ActorType == "snake")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    snake = new Snake(p[1] * 16, p[0] * 16);
                    actors.add(snake);
                }
            }
            #endregion
            #region Soldier
            if (ActorType == "soldier")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    soldier = new Soldier(p[1] * 16, p[0] * 16);
                    actors.add(soldier);
                }
            }
            #endregion
            #region Sphinx
            if (ActorType == "sphinx")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    sphinx = new Sphinx(p[1] * 16, p[0] * 16);
                    actors.add(sphinx);
                }
            }
            #endregion
            #region Spider
            if (ActorType == "spider")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    spider = new Spider(p[1] * 16, p[0] * 16);
                    actors.add(spider);
                }
            }
            #endregion
            #region Succubus
            if (ActorType == "succubus")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    succubus = new Succubus(p[1] * 16, p[0] * 16);
                    actors.add(succubus);
                }
            }
            #endregion
            #region Tauro
            if (ActorType == "tauro")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    tauro = new Tauro(p[1] * 16, p[0] * 16);
                    actors.add(tauro);
                }
            }
            #endregion
            #region Toad
            if (ActorType == "toad")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    toad = new Toad(p[1] * 16, p[0] * 16);
                    actors.add(toad);
                }
            }
            #endregion
            #region Tormentor
            if (ActorType == "tormentor")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    tormentor = new Tormentor(p[1] * 16, p[0] * 16);
                    actors.add(tormentor);
                }
            }
            #endregion
            #region Treant
            if (ActorType == "treant")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    treant = new Treant(p[1] * 16, p[0] * 16);
                    actors.add(treant);
                }
            }
            #endregion
            #region Troll
            if (ActorType == "troll")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    troll = new Troll(p[1] * 16, p[0] * 16);
                    actors.add(troll);
                }
            }
            #endregion
            #region Unicorn
            if (ActorType == "unicorn")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    unicorn = new Unicorn(p[1] * 16, p[0] * 16);
                    actors.add(unicorn);
                }
            }
            #endregion
            #region Vampire
            if (ActorType == "vampire")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    vampire = new Vampire(p[1] * 16, p[0] * 16);
                    actors.add(vampire);
                }
            }
            #endregion

            

             

            #region Willowisp
            if (ActorType == "willowisp")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    willowisp = new Willowisp(p[1] * 16, p[0] * 16);
                    actors.add(willowisp);
                }
            }
            #endregion
            #region Wizard
            if (ActorType == "wizard")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    wizard = new Wizard(p[1] * 16, p[0] * 16);
                    actors.add(wizard);
                }
            }
            #endregion
            #region Wolf
            if (ActorType == "wolf")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    wolf = new Wolf(p[1] * 16, p[0] * 16);
                    actors.add(wolf);
                }
            }
            #endregion
            #region Zinger
            if (ActorType == "zinger")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    zinger = new Zinger(p[1] * 16, p[0] * 16);
                    actors.add(zinger);
                }
            }
            #endregion
            #region Zombie
            if (ActorType == "zombie")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    zombie = new Zombie(p[1] * 16, p[0] * 16);
                    actors.add(zombie);
                }
            }
            #endregion



        }

    }
}

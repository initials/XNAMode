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

        private FlxSprite bgSprite;

        /// <summary>
        /// Tells you the time of day (between 0.0f and 24.99f)
        /// </summary>
        private float timeOfDay = 0.0f;

        /// <summary>
        /// Helper to keep track of the time of day.
        /// </summary>
        private float timeOfDayTotal = 0.0f;

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

            FlxG.level = 1;

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
            string levelname = "level_" + FlxG.level.ToString();
            Console.WriteLine(levelname);

            XElement xelement = XElement.Load("levelDetails.xml");

            foreach (XElement xEle in xelement.Descendants(levelname).Elements())
            {
                if (xEle.Value.ToString() == "")
                {
                    //Console.WriteLine("xele == nothing " + xEle.Name.ToString() + " " + xEle.Attribute("default").Value.ToString());
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                }
                else
                {
                    //Console.WriteLine("xele has a value " + xEle.Name.ToString() + " " + xEle.Value.ToString());
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Value.ToString());
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
            bgSprite.scrollFactor.X = 0.02f;
            bgSprite.scrollFactor.Y = 0.02f;
            bgSprite.boundingBoxOverride = false;
            add(bgSprite);

            // Generate the levels caves/tiles.

            cave = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));
            cave.initWallRatio = (float)Convert.ToDouble(levelAttrs["startCaveGenerateBias"]);
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));

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

            foreach (KeyValuePair<string, string> pair in levelAttrs)
            {
                //Console.WriteLine("dict -----> {0}, {1}",
                //pair.Key,
                //pair.Value);
                
                int noa = 0;

                try { noa = Convert.ToInt32(pair.Value); }
                catch { noa = 0; }
                if (pair.Value != "" && pair.Value != null && pair.Value != "0")
                {
                    //Console.WriteLine("Ok we are building {0}, {1}, ", pair.Key, pair.Value);

                    if (noa != 0)
                    {
                        buildActor(pair.Key, Convert.ToInt32(pair.Value));
                    }
                }
            }


            //foreach (KeyValuePair<string, string> pair in levelAttrs)
            //{
            //    /// try-catch may be a dirty way of parsing out the characters.
            //    try
            //    {
            //        buildActor(pair.Key, Convert.ToInt32(pair.Value));
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Can't build" + pair.Key );
            //    }
                
            //}


            // build atmospheric effects here

            paletteTexture = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["timeOfDayPalette"]);

            marksman.isPlayerControlled = true;
            FlxG.follow(marksman, FOLLOW_LERP);
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

            //FlxG.autoHandlePause = true;

        }

        override public void update()
        {

            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            //Console.WriteLine("Paused? " + FlxG.pause);
            // pause

            //if (FlxG.keys.justPressed(Keys.P) || FlxG.gamepads.isNewButtonPress(Buttons.Start))
            //{
            //    Console.WriteLine("Paused");

            //    if (FlxG.pause == true) FlxG.pause = false;
            //    else if (FlxG.pause == false) FlxG.pause = true;
            //}

            //calculate time of day.
            timeOfDayTotal += FlxG.elapsed * timeScale;
            if (timeOfDayTotal > 24.99f) timeOfDayTotal = 0.0f;
            //timeOfDay = timeOfDayTotal / timeScale;

            // color bg tiles
            //bgTiles.color = FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, 1);
            
            // color whole game.
            FlxG.color(FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDayTotal, 1));


            //collides
            FlxU.collide(actors, mainTilemap);

            FlxU.overlap(actors, bullets, overlapped);
            FlxU.collide(mainTilemap, bullets);

            FlxU.collide(blood, mainTilemap);


            if (FlxG.keys.A && FlxG.debug)
            {
                FlxG.transition.startFadeIn(0.025f);

                FlxG.state = new BasePlayState();
            }

            base.update();
        }

        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {
            /*
            if ((e.Object1 is BotBullet) || (e.Object1 is Bullet))
                e.Object1.kill();
            e.Object2.hurt(1);
            return true;
             */

            if ((e.Object1 is Warlock) && (e.Object2 is Fireball))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is Arrow))
            {

            }

            else
            {
                e.Object1.velocity.X = e.Object2.velocity.X * 10;
                e.Object1.hurt(1);

                e.Object2.kill();

                blood.at(e.Object1);
                //blood.start(false, 0, 50);
                //blood.emitParticle();
                //blood.emitParticle();
                //blood.emitParticle();
                //blood.quantity = 20;

                blood.start(true, 0, 10);
                //blood.stop();

            }

            return true;

        }
        public void buildActor(string ActorType, int NumberOfActors)
        {
            #region Marksman
            if (ActorType == "marksman")
            {
                //Console.WriteLine("Marksman being made " + NumberOfActors);

                for (int i = 0; i < BULLETS_PER_ACTOR; i++)
                    arrows.add(new Arrow());
                bullets.add(arrows);

                for (int i = 0; i < NumberOfActors; i++)
                {
                    //Console.WriteLine("Marksman being made " + NumberOfActors);

                    int[] p = cave.findRandomSolid(decorationsArray);
                    marksman = new Marksman(p[1] * 16, p[0] * 16, arrows.members);
                    actors.add(marksman);
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
                    int[] p = cave.findRandomSolid(decorationsArray);
                    bat = new Bat(p[1] * 16, p[0] * 16 - 50);
                    actors.add(bat);
                }
            }
            #endregion
            #region Blight
            if (ActorType == "Blight")
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
            if (ActorType == "Bloatedzombie")
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
            if (ActorType == "Bogbeast")
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
            if (ActorType == "Bombling")
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
            if (ActorType == "Centaur")
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
            if (ActorType == "Chicken")
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
            if (ActorType == "Chimaera")
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
            if (ActorType == "Corsair")
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
            if (ActorType == "Cow")
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
            if (ActorType == "Cyclops")
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
            if (ActorType == "Deathclaw")
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
            if (ActorType == "Deer")
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
            if (ActorType == "Devil")
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
            if (ActorType == "Djinn")
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
            if (ActorType == "Drone")
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
            if (ActorType == "Druid")
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
            if (ActorType == "Dwarf")
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
            if (ActorType == "Embersteed")
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
            if (ActorType == "Executor")
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
            if (ActorType == "Feline")
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
            if (ActorType == "Floatingeye")
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
            if (ActorType == "Fungant")
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
            if (ActorType == "Gelatine")
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
            if (ActorType == "Gloom")
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
            if (ActorType == "Glutton")
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
            if (ActorType == "Goblin")
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
            if (ActorType == "Golem")
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
            if (ActorType == "Gorgon")
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
            if (ActorType == "Gourmet")
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
            if (ActorType == "Grimwarrior")
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
            if (ActorType == "Grizzly")
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
            if (ActorType == "Harvester")
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
            if (ActorType == "Horse")
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
            if (ActorType == "Ifrit")
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
            if (ActorType == "Imp")
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
            if (ActorType == "Kerberos")
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
            if (ActorType == "Lich")
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
            if (ActorType == "Lion")
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
            if (ActorType == "Mechanic")
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
            if (ActorType == "Mephisto")
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
            if (ActorType == "Merchant")
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
            if (ActorType == "Mermaid")
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
            if (ActorType == "Mimick")
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
            if (ActorType == "Monk")
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
            if (ActorType == "Mummy")
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
            if (ActorType == "Nightmare")
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
            if (ActorType == "Nymph")
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
            if (ActorType == "Ogre")
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
            if (ActorType == "Paladin")
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
            if (ActorType == "Phantom")
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
            if (ActorType == "Priest")
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
            if (ActorType == "Prism")
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
            if (ActorType == "Rat")
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
            if (ActorType == "Savage")
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
            if (ActorType == "Seraphine")
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
            if (ActorType == "Sheep")
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
            if (ActorType == "Skeleton")
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
            if (ActorType == "Snake")
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
            if (ActorType == "Soldier")
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
            if (ActorType == "Sphinx")
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
            if (ActorType == "Spider")
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
            if (ActorType == "Succubus")
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
            if (ActorType == "Tauro")
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
            if (ActorType == "Toad")
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
            if (ActorType == "Tormentor")
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
            if (ActorType == "Treant")
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
            if (ActorType == "Troll")
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
            if (ActorType == "Unicorn")
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
            if (ActorType == "Vampire")
            {
                for (int i = 0; i <= NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(decorationsArray);
                    vampire = new Vampire(p[1] * 16, p[0] * 16);
                    actors.add(vampire);
                }
            }
            #endregion

            
            #region Warlock
            if (ActorType == "Warlock")
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
            }
            #endregion
             

            #region Willowisp
            if (ActorType == "Willowisp")
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
            if (ActorType == "Wizard")
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
            if (ActorType == "Wolf")
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
            if (ActorType == "Zinger")
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
            if (ActorType == "Zombie")
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

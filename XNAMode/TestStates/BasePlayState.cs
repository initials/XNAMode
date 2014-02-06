using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    /// <summary>
    /// BasePlayState reads it's values from a LevelSettings XML file.
    /// 
    /// levelAttrs = FlxXMLReader.readCustomXMLLevelsAttrs("levelSettings.xml");
    /// 
    /// </summary>
    public class BasePlayState : FlxState
    {
        /// <summary>
        /// The Hud!
        /// Adjust your score here.
        /// </summary>
        private PlayHud localHud;

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
        /// Rear Decorations
        /// </summary>
        private FlxTilemap decorationsRearTilemap;

        private FlxGroup ladders;

        private FlxGroup allLevelTiles;

        private FlxTileblock ladder;

        /// <summary>
        /// An array of the positions that a character can spawn.
        /// </summary>
        private int[,] characterSpawnPositionsArray;

        private int[,] decorationsFGArray;

        private int[,] decorationsBGArray;

        private int[,] hangingArray;

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

        private FlxGroup playerControlledActors;

        //private FlxGroup enemyActors;

        /// <summary>
        /// Every single bullet in the scene.
        /// </summary>
        protected FlxGroup bullets;

        private FlxEmitter blood;

        #region Actors
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
        #endregion

        private ZingerNest ZingerNest;
        private FlxGroup zingers;
        private FlxGroup powerUps;
        private PowerUp powerUp;
        private Door door;
        
        public Arrow arrow;
        private BigExplosion bigEx;


        override public void create()
        {

            base.create();


            Console.WriteLine("Loading BasePlayState Level");

            //important to reset the hud to get the text, gamepad buttons out.
            FlxG.resetHud();
            FlxG.showHud();
            FlxG.showHudGraphic();

            //FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            // initialize a bunch of groups
            actors = new FlxGroup();
            fireballs = new FlxGroup();
            bullets = new FlxGroup();
            arrows = new FlxGroup();
            ladders = new FlxGroup();
            allLevelTiles = new FlxGroup();
            playerControlledActors = new FlxGroup();
            zingers = new FlxGroup();
            powerUps = new FlxGroup();

            bigEx = new BigExplosion(-1000, -1000);


            //First build a dictionary of levelAttrs
            //This will determine how the level is built.

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            levelAttrs = FlxXMLReader.readCustomXMLLevelsAttrs("levelSettings.xml");

            #region old level attrs load.

            //string currentLevel = "l" + FlxG.level.ToString();

            //XElement xelement = XElement.Load("levelSettings.xml");

            //foreach (XElement xEle in xelement.Descendants("settings").Elements())
            //{
            //    XElement firstSpecificChildElement = xEle.Element(currentLevel);
            //    if (firstSpecificChildElement != null )
            //    {
            //        if (firstSpecificChildElement.Value.ToString()=="") 
            //        {
            //            levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
            //            XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");
            //            if (playerControlled != null)
            //            {
            //                levelAttrs.Add("playerControlled", xEle.Name.ToString());
            //            }
            //        }
            //        else 
            //        {
            //            levelAttrs.Add(xEle.Name.ToString(), firstSpecificChildElement.Value.ToString());
            //            XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");
            //            if (playerControlled != null )
            //            {
            //                levelAttrs.Add("playerControlled", xEle.Name.ToString());
            //            }
            //        }
            //    }
            //    else
            //    {
            //        levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
            //    }
            //}

            #endregion


            FlxG.levelWidth = Convert.ToInt32(levelAttrs["levelWidth"]) * FourChambers_Globals.TILE_SIZE_X;
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["levelHeight"]) * FourChambers_Globals.TILE_SIZE_Y;

            Texture2D bgGraphic = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["bgGraphic"]);
            bgSprite = new FlxSprite(0, 0, bgGraphic);
            bgSprite.loadGraphic(bgGraphic);
            bgSprite.scrollFactor.X = 0.4f;
            bgSprite.scrollFactor.Y = 0.4f;
            bgSprite.x = 0;
            bgSprite.y = 0;
            bgSprite.color = Color.DarkGray;
            bgSprite.boundingBoxOverride = false;
            add(bgSprite);

            Console.WriteLine("Generate the levels caves/tiles.");

            // Generate the levels caves/tiles.

            cave = new FlxCaveGenerator(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));
            cave.initWallRatio = (float)Convert.ToDouble(levelAttrs["startCaveGenerateBias"]);
            cave.numSmoothingIterations = 5;
            cave.genInitMatrix(Convert.ToInt32(levelAttrs["levelWidth"]), Convert.ToInt32(levelAttrs["levelHeight"]));

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
            mainTilemap.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["tiles"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            mainTilemap.boundingBoxOverride = true;


            allLevelTiles.add(mainTilemap);

            


            // Generate some random ladders

            for (int i = 0; i < 3; i++)
            {
                int rx = (int)(FlxU.random() * (FlxG.levelWidth / FourChambers_Globals.TILE_SIZE_X));
                int ry = (int)(FlxU.random() * (FlxG.levelHeight / FourChambers_Globals.TILE_SIZE_Y));

                ladder = new FlxTileblock(rx * FourChambers_Globals.TILE_SIZE_X, ry * FourChambers_Globals.TILE_SIZE_Y, FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_X * 10);
                ladder.loadTiles(FlxG.Content.Load<Texture2D>("initials/ladderTiles_16x16"), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);
                ladders.add(ladder);
            }
            //generate some ladders in the empty columns

            if (emptyColumnsAfterSmooth != null)
            {
                foreach (var item in emptyColumnsAfterSmooth)
                {
                    ladder = new FlxTileblock(item * FourChambers_Globals.TILE_SIZE_X, 0, FourChambers_Globals.TILE_SIZE_X, FlxG.levelHeight);
                    ladder.loadTiles(FlxG.Content.Load<Texture2D>("initials/ladderTiles_16x16"), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);
                    ladders.add(ladder);
                }
            }
            add(ladders);
            add(allLevelTiles);

            for (int i = 0; i < 2; i++)
            {

                int rx = (int)(FlxU.random() * (FlxG.levelWidth / FourChambers_Globals.TILE_SIZE_X));
                int ry = (int)(FlxU.random() * (FlxG.levelHeight / FourChambers_Globals.TILE_SIZE_Y));

                for (int j = 0; j < 10; j++)
                {
                    FallAwayBridgeBlock f = new FallAwayBridgeBlock((rx * FourChambers_Globals.TILE_SIZE_X) + (j * FourChambers_Globals.TILE_SIZE_X), ry * FourChambers_Globals.TILE_SIZE_Y);
                    allLevelTiles.add(f);

                }
            }




            // add the decorations tilemap.

            characterSpawnPositionsArray = cave.createDecorationsMap(mainTilemapArray, 1.0f);

            hangingArray = cave.createHangingDecorationsMap(mainTilemapArray, 1.0f);

            decorationsFGArray = cave.createDecorationsMap(mainTilemapArray, 0.5f);

            string newDec = cave.convertMultiArrayToString(decorationsFGArray);

            Texture2D DecorTex = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["decorationTiles"]);

            decorationsTilemap = new FlxTilemap();
            decorationsTilemap.auto = FlxTilemap.RANDOM;
            decorationsTilemap.randomLimit = (int)DecorTex.Width / FourChambers_Globals.TILE_SIZE_X;
            decorationsTilemap.boundingBoxOverride = false;
            decorationsTilemap.loadMap(newDec, DecorTex, FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            //add it after the actors.

            decorationsBGArray = cave.createDecorationsMap(mainTilemapArray, 0.2f);

            string newDec2 = cave.convertMultiArrayToString(decorationsBGArray);

            Texture2D DecorTexRear = FlxG.Content.Load<Texture2D>("initials/decorationsBack_16x16");
            decorationsRearTilemap = new FlxTilemap();
            decorationsRearTilemap.auto = FlxTilemap.RANDOM;
            decorationsRearTilemap.randomLimit = (int)DecorTex.Width / FourChambers_Globals.TILE_SIZE_X;
            decorationsRearTilemap.boundingBoxOverride = false;
            decorationsRearTilemap.loadMap(newDec2, DecorTexRear, FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            add(decorationsRearTilemap);

            // build characters here
            
            buildActor("marksman", 1, true);

            // Looks through the level dictionary and builds neccessary actors.
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

            

            for (int i = 0; i < 12; i++)
            {
                int[] p = cave.findRandomSolid(hangingArray);
                ZingerNest = new ZingerNest(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_Y, zingers);
                actors.add(ZingerNest);

                zinger = new Zinger(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                zingers.add(zinger);
                actors.add(zinger);
                zinger.dead = true;
                zinger.visible = false;

                powerUp = new PowerUp(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                powerUp.dead = true;
                powerUp.visible = false;
                powerUps.add(powerUp);



            }
            int[] p1 = cave.findRandomSolid(characterSpawnPositionsArray);
            door = new Door(p1[1] * FourChambers_Globals.TILE_SIZE_X, p1[0] * FourChambers_Globals.TILE_SIZE_X);
            add(door);

            Console.WriteLine("Done generating levels");

            // build atmospheric effects here

            paletteTexture = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["timeOfDayPalette"]);



            //FlxG.followAdjust(0.5f, 0.0f);
            FlxG.followBounds(0, 0, Convert.ToInt32(levelAttrs["levelWidth"]) * FourChambers_Globals.TILE_SIZE_X, Convert.ToInt32(levelAttrs["levelHeight"]) * FourChambers_Globals.TILE_SIZE_Y);

            add(actors);
            add(bullets);
            add(powerUps);

            blood = new FlxEmitter();
            blood.x = 0;
            blood.y = 0;
            blood.width = 6;
            blood.height = 6;
            blood.delay = 0.8f;
            
            //blood.del
            blood.setXSpeed(-152, 152);
            blood.setYSpeed(-250, -50);
            blood.setRotation(0, 0);
            blood.gravity = FourChambers_Globals.GRAVITY;
            blood.createSprites(FlxG.Content.Load<Texture2D>("initials/blood"), 1500, true, 1.0f, 0.1f);
            add(blood);

            
            add(bigEx);

            add(decorationsTilemap);

            //FlxG.autoHandlePause = true;


            if (FlxG.joystickBeingUsed) FlxG.mouse.hide();
            else FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));

            localHud = new PlayHud();
            FlxG._game.hud.hudGroup = localHud;

            Console.WriteLine("Starting at: " + FlxG.level);


        }

        override public void update()
        {

            #region debugLevelSkip
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

            // Allow editing of terrain if SHIFT + Mouse is pressed.
            if (FlxG.mouse.pressedRightButton() && FlxG.keys.SHIFT)
            {
                //Console.WriteLine((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X + " " + (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y);

                mainTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y, 0, true);
                decorationsTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, ((int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y) - 1, 0, true);
            }
            if (FlxG.mouse.pressedLeftButton() && FlxG.keys.SHIFT)
            {
                mainTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y, 1, true);
            }

            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            #endregion

            localHud.score.text = FlxG.score.ToString();
            if( marksman != null) {
                localHud.setArrowsRemaining(marksman.arrowsRemaining);
            }
            //calculate time of day.
            timeOfDay += FlxG.elapsed * timeScale;
            if (timeOfDay > 24.99f) timeOfDay = 0.0f;
            //timeOfDay = timeOfDayTotal / timeScale;

            // color bg tiles
            //bgTiles.color = FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, 1);
            
            // color whole game.
            FlxG.color(FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, 1));

            //collides
            FlxU.collide(actors, allLevelTiles);

            FlxU.collide(powerUps, allLevelTiles);
            FlxU.overlap(playerControlledActors, door, goToNextLevel);

            FlxU.overlap(actors, bullets, overlapped);
            FlxU.overlap(actors, ladders, overlapWithLadder);

            
            
            FlxU.collide(mainTilemap, bullets);
            
            FlxU.collide(blood, mainTilemap);


            FlxU.overlap(actors, playerControlledActors, actorOverlap);

            FlxU.overlap(powerUps, playerControlledActors, getPowerUp);

            // removing tile from the explosion.
            //try
            //{
            //    int xtile = (int)((bigEx.x + 16) / FourChambers_Globals.TILE_SIZE_X);
            //    int ytile = (int)((bigEx.y + 16) / FourChambers_Globals.TILE_SIZE_Y);
            //    //Console.WriteLine(xtile + " " + ytile);

            //    mainTilemap.setTile(xtile, ytile , 0, true);
            //    mainTilemap.setTile(xtile-1, ytile, 0, true);
            //    mainTilemap.setTile(xtile+1, ytile, 0, true);

            //    //decorationsTilemap.setTile((int)bigEx.x - 16 / FourChambers_Globals.TILE_SIZE_X, ((int)bigEx.y - 16 / FourChambers_Globals.TILE_SIZE_Y) - 1, 0, true);
            //}
            //catch { }

            base.update();

            // exit.
            if (FlxG.keys.justPressed(Keys.Escape) )
            {
                int i = 0;
                int l = playerControlledActors.members.Count;
                while (i < l)
                {
                    (actors.members[i] as FlxSprite).dead = true;
                    i++;
                }

                Console.WriteLine("Just pressed Escape and killed all player characters.");
                

            }

            if (playerControlledActors.getFirstAlive() == null)
            {

                //FlxG.setHudText(1, "Press X to go to Menu \n Press Y to restart.");

                FlxG._game.hud.p1HudText.alignment = FlxJustification.Center;
                FlxG._game.hud.p1HudText.text = "Press X to go to Menu \n Press Y to restart.";
                FlxG.setHudTextScale(1, 2);
                FlxG.setHudTextPosition(1, 0, FlxG.height / 2);


                if (FlxG.gamepads.isButtonDown(Buttons.X) || FlxG.mouse.pressed() )
                {
                    FlxOnlineStatCounter.sendStats("fourchambers", "marksman", FlxG.score);
                    goToMenu();
                }

                if (FlxG.gamepads.isButtonDown(Buttons.Y) || FlxG.mouse.pressed())
                {
                    FlxOnlineStatCounter.sendStats("fourchambers", "marksman", FlxG.score);
                    restart();
                }


            }
        }

        private void restart()
        {
            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.state = new BasePlayState();
        }
        private void goToMenu()
        {
            FlxG.state = new GameSelectionMenuState();
        }

        protected bool goToNextLevel(object Sender, FlxSpriteCollisionEvent e)
        {
            FlxG.level++;
            if (FlxG.level > 25) FlxG.level = 1;

            FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

            FlxG.transition.startFadeIn(0.2f);

            FlxG.state = new BasePlayState();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected bool overlapWithLadder(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1 is Actor) 
            {
                ((Actor)(e.Object1)).ladderPosX = e.Object2.x;
                ((Actor)(e.Object1)).canClimbLadder = true;
            }
            return true;
        }



        protected bool getPowerUp(object Sender, FlxSpriteCollisionEvent e)
        {
            int x = ((PowerUp)e.Object1).typeOfPowerUp;
            if (x == 154 || x == 155 || x == 156 || x == 157)
            {
                if (marksman != null)
                    marksman.arrowsRemaining += 20;
            }
            else
            {
                FlxG.score += 1000;
            }

            e.Object1.kill();

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected bool actorOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1.dead == false && e.Object2.dead == false && e.Object1.flickering() == false && e.Object2.flickering() == false) 
            {
                e.Object2.hurt(1);
                e.Object1.hurt(1);
            }

            return true;
        }

        /// <summary>
        /// e1=actors,
        /// e2=bullets
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {

            // Console.WriteLine("Overlapped.");

            // First reject Actors and their bullets.
            if ((e.Object1 is Warlock) && (e.Object2 is WarlockFireBall))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is Arrow))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is MeleeHitBox)) { } 
            else if ((e.Object1 is Mistress) && (e.Object2 is MeleeHitBox))
            {

            }
            else if ((e.Object1 is ZingerNest) && (e.Object2 is Arrow))
            {
                bigEx.x = e.Object1.x;
                bigEx.y = e.Object1.y;
                bigEx.play("explode", true);


                blood.at(e.Object1);

                blood.start(true, 0, 10);
                FlxObject z = zingers.getFirstDead();
                if (z != null)
                {
                    z.dead = false;
                    z.exists = true;
                    z.x = e.Object1.x;
                    z.y = e.Object2.y;
                    z.flicker(0.001f);
                    z.velocity.X = 50;
                    z.velocity.Y = FlxU.random(-200, 200);
                    z.angle = 0;
                    z.visible = true;
                }
                // throw out a power up.
                FlxObject p = powerUps.getFirstDead();
                if (p != null)
                {
                    p.dead = false;
                    p.acceleration.Y = FourChambers_Globals.GRAVITY;
                    p.velocity.X = FlxU.random(-5, 5);
                    p.exists = true;
                    p.x = e.Object1.x + 8;
                    p.y = e.Object2.y - 8;
                    p.flicker(0.001f);
                    p.angle = 0;
                    p.visible = true;
                }

                e.Object2.x = -1000;
                e.Object2.y = -1000;
                e.Object2.kill();
                e.Object1.kill();

            }
            // Now that it's a kill, spurt some blood and "hurt" both parties.
            else if (e.Object1.dead == false && e.Object2.dead == false)
            {
                //pointBurst.alpha = 1;

                //e.Object1.acceleration.Y = 820;

                e.Object1.velocity.X = e.Object2.velocity.X;
                e.Object1.velocity.Y = e.Object2.velocity.Y;

                e.Object1.hurt(1);

                e.Object2.x = -1000;
                e.Object2.y = -1000;
                e.Object2.kill();

                blood.at(e.Object1);

                blood.start(true, 0, 10);
            }

            return true;

        }

        public void buildActor(string ActorType, int NumberOfActors)
        {
            buildActor(ActorType, NumberOfActors, false);
        }
        public void buildActor(string ActorType, int NumberOfActors, bool playerControlled = false)
        {
            #region Marksman
            if (ActorType == "marksman")
            {
                for (int i = 0; i < BULLETS_PER_ACTOR; i++)
                {
                    arrow = new Arrow(-1000, 1000, bigEx);
                    arrows.add(arrow);
                }
                bullets.add(arrows);

                for (int i = 0; i < NumberOfActors; i++)
                {
                    //FlxG.write("Marksman being made " + NumberOfActors);

                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    marksman = new Marksman(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X, arrows.members);
                    marksman.flicker(2);
                    actors.add(marksman);
                    bullets.add(marksman.meleeHitBox);
                    playerControlledActors.add(marksman);

                }

                if (playerControlled == true) //levelAttrs["playerControlled"] == "marksman" ||
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
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mistress = new Mistress(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mistress);
                    mistress.flicker(2);
                    playerControlledActors.add(mistress);
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
                    fireballs.add(new WarlockFireBall());
                bullets.add(fireballs);
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    warlock = new Warlock(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X, fireballs.members);
                    actors.add(warlock);
                    warlock.flicker(2);
                    playerControlledActors.add(warlock);
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
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    artist = new Artist(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(artist);
                }
            }
            #endregion
            #region Assassin
            if (ActorType == "assassin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    assassin = new Assassin(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(assassin);
                }
            }
            #endregion
            #region Automaton
            if (ActorType == "automaton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    automaton = new Automaton(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(automaton);
                    

                }
            }
            #endregion
            #region Bat
            if (ActorType == "bat")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomEmpty(mainTilemapArray );
                    bat = new Bat(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(bat);
                }
            }
            #endregion
            #region Blight
            if (ActorType == "blight")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    blight = new Blight(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(blight);
                }
            }
            #endregion
            #region Bloatedzombie
            if (ActorType == "bloatedzombie")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    bloatedzombie = new Bloatedzombie(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(bloatedzombie);
                }
            }
            #endregion
            #region Bogbeast
            if (ActorType == "bogbeast")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    bogbeast = new Bogbeast(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(bogbeast);
                }
            }
            #endregion
            #region Bombling
            if (ActorType == "bombling")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    bombling = new Bombling(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(bombling);
                }
            }
            #endregion
            #region Centaur
            if (ActorType == "centaur")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    centaur = new Centaur(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(centaur);
                }
            }
            #endregion
            #region Chicken
            if (ActorType == "chicken")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    chicken = new Chicken(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(chicken);
                }
            }
            #endregion
            #region Chimaera
            if (ActorType == "chimaera")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    chimaera = new Chimaera(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(chimaera);
                }
            }
            #endregion
            #region Corsair
            if (ActorType == "corsair")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    corsair = new Corsair(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(corsair);
                }
            }
            #endregion
            #region Cow
            if (ActorType == "cow")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    cow = new Cow(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(cow);
                }
            }
            #endregion
            #region Cyclops
            if (ActorType == "cyclops")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    cyclops = new Cyclops(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(cyclops);
                }
            }
            #endregion
            #region Deathclaw
            if (ActorType == "deathclaw")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    deathclaw = new Deathclaw(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(deathclaw);
                }
            }
            #endregion
            #region Deer
            if (ActorType == "deer")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    deer = new Deer(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(deer);
                }
            }
            #endregion
            #region Devil
            if (ActorType == "devil")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    devil = new Devil(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(devil);
                }
            }
            #endregion
            #region Djinn
            if (ActorType == "djinn")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    djinn = new Djinn(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(djinn);
                }
            }
            #endregion
            #region Drone
            if (ActorType == "drone")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomEmpty(mainTilemapArray);
                    drone = new Drone(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(drone);
                }
            }
            #endregion
            #region Druid
            if (ActorType == "druid")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    druid = new Druid(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(druid);
                }
            }
            #endregion
            #region Dwarf
            if (ActorType == "dwarf")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    dwarf = new Dwarf(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(dwarf);
                }
            }
            #endregion
            #region Embersteed
            if (ActorType == "embersteed")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    embersteed = new Embersteed(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(embersteed);
                }
            }
            #endregion
            #region Executor
            if (ActorType == "executor")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    executor = new Executor(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(executor);
                }
            }
            #endregion
            #region Feline
            if (ActorType == "feline")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    feline = new Feline(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(feline);
                }
            }
            #endregion
            #region Floatingeye
            if (ActorType == "floatingeye")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    floatingeye = new Floatingeye(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(floatingeye);
                }
            }
            #endregion
            #region Fungant
            if (ActorType == "fungant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    fungant = new Fungant(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(fungant);
                }
            }
            #endregion
            #region Gelatine
            if (ActorType == "gelatine")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    gelatine = new Gelatine(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(gelatine);
                }
            }
            #endregion
            #region Gloom
            if (ActorType == "gloom")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    gloom = new Gloom(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(gloom);
                }
            }
            #endregion
            #region Glutton
            if (ActorType == "glutton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    glutton = new Glutton(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(glutton);
                }
            }
            #endregion
            #region Goblin
            if (ActorType == "goblin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    goblin = new Goblin(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(goblin);
                }
            }
            #endregion
            #region Golem
            if (ActorType == "golem")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    golem = new Golem(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(golem);
                }
            }
            #endregion
            #region Gorgon
            if (ActorType == "gorgon")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    gorgon = new Gorgon(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(gorgon);
                }
            }
            #endregion
            #region Gourmet
            if (ActorType == "gourmet")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    gourmet = new Gourmet(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(gourmet);
                }
            }
            #endregion
            #region Grimwarrior
            if (ActorType == "grimwarrior")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    grimwarrior = new Grimwarrior(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(grimwarrior);
                }
            }
            #endregion
            #region Grizzly
            if (ActorType == "grizzly")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    grizzly = new Grizzly(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(grizzly);
                }
            }
            #endregion
            #region Harvester
            if (ActorType == "harvester")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    harvester = new Harvester(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(harvester);
                }
            }
            #endregion
            #region Horse
            if (ActorType == "horse")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    horse = new Horse(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(horse);
                }
            }
            #endregion
            #region Ifrit
            if (ActorType == "ifrit")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    ifrit = new Ifrit(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(ifrit);
                }
            }
            #endregion
            #region Imp
            if (ActorType == "imp")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    imp = new Imp(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(imp);
                }
            }
            #endregion
            #region Kerberos
            if (ActorType == "kerberos")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    kerberos = new Kerberos(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(kerberos);
                }
            }
            #endregion
            #region Lich
            if (ActorType == "lich")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    lich = new Lich(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(lich);
                }
            }
            #endregion
            #region Lion
            if (ActorType == "lion")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    lion = new Lion(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(lion);
                }
            }
            #endregion

            // marksman was here.

            #region Mechanic
            if (ActorType == "mechanic")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mechanic = new Mechanic(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mechanic);
                }
            }
            #endregion
            #region Mephisto
            if (ActorType == "mephisto")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mephisto = new Mephisto(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mephisto);
                }
            }
            #endregion
            #region Merchant
            if (ActorType == "merchant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    merchant = new Merchant(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(merchant);
                }
            }
            #endregion
            #region Mermaid
            if (ActorType == "mermaid")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mermaid = new Mermaid(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mermaid);
                }
            }
            #endregion
            #region Mimick
            if (ActorType == "mimick")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mimick = new Mimick(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mimick);
                }
            }
            #endregion
            #region Monk
            if (ActorType == "monk")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    monk = new Monk(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(monk);
                }
            }
            #endregion
            #region Mummy
            if (ActorType == "mummy")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    mummy = new Mummy(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(mummy);
                }
            }
            #endregion
            #region Nightmare
            if (ActorType == "nightmare")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    nightmare = new Nightmare(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(nightmare);
                }
            }
            #endregion
            #region Nymph
            if (ActorType == "nymph")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    nymph = new Nymph(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(nymph);
                }
            }
            #endregion
            #region Ogre
            if (ActorType == "ogre")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    ogre = new Ogre(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(ogre);
                }
            }
            #endregion
            #region Paladin
            if (ActorType == "paladin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    paladin = new Paladin(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(paladin);
                }
            }
            #endregion
            #region Phantom
            if (ActorType == "phantom")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    phantom = new Phantom(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(phantom);
                }
            }
            #endregion
            #region Priest
            if (ActorType == "priest")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    priest = new Priest(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(priest);
                }
            }
            #endregion
            #region Prism
            if (ActorType == "prism")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    prism = new Prism(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(prism);
                }
            }
            #endregion
            #region Rat
            if (ActorType == "rat")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    rat = new Rat(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(rat);
                }
            }
            #endregion
            #region Savage
            if (ActorType == "savage")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    savage = new Savage(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(savage);
                }
            }
            #endregion
            #region Seraphine
            if (ActorType == "seraphine")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    seraphine = new Seraphine(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(seraphine);
                }
            }
            #endregion
            #region Sheep
            if (ActorType == "sheep")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    sheep = new Sheep(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(sheep);
                }
            }
            #endregion
            #region Skeleton
            if (ActorType == "skeleton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    skeleton = new Skeleton(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(skeleton);
                }
            }
            #endregion
            #region Snake
            if (ActorType == "snake")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    snake = new Snake(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(snake);
                }
            }
            #endregion
            #region Soldier
            if (ActorType == "soldier")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    soldier = new Soldier(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(soldier);
                }
            }
            #endregion
            #region Sphinx
            if (ActorType == "sphinx")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    sphinx = new Sphinx(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(sphinx);
                }
            }
            #endregion
            #region Spider
            if (ActorType == "spider")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    spider = new Spider(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(spider);
                }
            }
            #endregion
            #region Succubus
            if (ActorType == "succubus")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    succubus = new Succubus(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(succubus);
                }
            }
            #endregion
            #region Tauro
            if (ActorType == "tauro")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    tauro = new Tauro(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(tauro);
                }
            }
            #endregion
            #region Toad
            if (ActorType == "toad")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    toad = new Toad(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(toad);
                }
            }
            #endregion
            #region Tormentor
            if (ActorType == "tormentor")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    tormentor = new Tormentor(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(tormentor);
                }
            }
            #endregion
            #region Treant
            if (ActorType == "treant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    treant = new Treant(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(treant);
                }
            }
            #endregion
            #region Troll
            if (ActorType == "troll")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    troll = new Troll(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(troll);
                }
            }
            #endregion
            #region Unicorn
            if (ActorType == "unicorn")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    unicorn = new Unicorn(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(unicorn);
                }
            }
            #endregion
            #region Vampire
            if (ActorType == "vampire")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    vampire = new Vampire(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(vampire);
                }
            }
            #endregion
            #region Willowisp
            if (ActorType == "willowisp")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    willowisp = new Willowisp(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(willowisp);
                }
            }
            #endregion
            #region Wizard
            if (ActorType == "wizard")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    wizard = new Wizard(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(wizard);
                }
            }
            #endregion
            #region Wolf
            if (ActorType == "wolf")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    wolf = new Wolf(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(wolf);
                }
            }
            #endregion
            #region Zinger
            if (ActorType == "zinger")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomEmpty(mainTilemapArray);
                    zinger = new Zinger(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    zingers.add(zinger);
                    actors.add(zinger);

                    if (FourChambers_Globals.PIRATE_COPY)
                    {
                        ZingerHoming z = new ZingerHoming(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X, marksman);
                        zingers.add(z);
                        actors.add(z);
                    }



                }
            }
            #endregion
            #region Zombie
            if (ActorType == "zombie")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    int[] p = cave.findRandomSolid(characterSpawnPositionsArray);
                    zombie = new Zombie(p[1] * FourChambers_Globals.TILE_SIZE_X, p[0] * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(zombie);
                }
            }
            #endregion

        }
    }
}

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
    /// <summary>
    /// BasePlayState reads it's values from a LevelSettings XML file.
    /// 
    /// levelAttrs = FlxXMLReader.readCustomXMLLevelsAttrs("levelSettings.xml");
    /// 
    /// </summary>
    public class BasePlayStateFromOel : FlxState
    {
        /// <summary>
        /// The Hud!
        /// Adjust your score here.
        /// </summary>
        private PlayHud localHud;

        /// <summary>
        /// The leading of the camera
        /// </summary>
        private const float FOLLOW_LERP = 7.0f;

        /// <summary>
        /// How many bullets to create for each actor.
        /// </summary>
        private const int BULLETS_PER_ACTOR = 100;

        /// <summary>
        /// A holder for all of the level data to generate a level from.
        /// </summary>
        Dictionary<string, string> levelAttrs;

        Dictionary<string, string> destructableAttrs;
        Dictionary<string, string> indestructableAttrs;

        List<Dictionary<string, string>> eventsAttrs;
        List<Dictionary<string, string>> actorsAttrs;

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
        private FlxTilemap destructableTilemap;
        private FlxTilemap indestructableTilemap;

        private FlxGroup ladders;

        private FlxGroup allLevelTiles;

        private FlxTileblock ladder;

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

        private FlxGroup eventSprites;

        //private FlxGroup enemyActors;

        /// <summary>
        /// Every single bullet in the scene.
        /// </summary>
        protected FlxGroup bullets;

        private FlxEmitter blood;

        private FlxEmitter tilesExplode;

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

        public Arrow arrow;
        private BigExplosion bigEx;

        private FlxSprite leftExitBlockerWall;
        private FlxSprite rightExitBlockerWall;

        public void test()
        {
            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            string levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level");

            Console.WriteLine("----------------------------------" + levelAttrs);

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }

            List<Dictionary<string, string>> levelNodes = FlxXMLReader.readNodesFromOelFile(levelFile, "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in levelNodes)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    Console.Write("Key = {0}, Value = {1}, ",
                        kvp.Key, kvp.Value);
                }
                Console.Write("\r\n");
            }

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/TilesLayer");

            Console.WriteLine("----------------------------------" + levelAttrs);

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
        }

        override public void create()
        {
            


            base.create();

            string levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";

            //this.test();

            Console.WriteLine("Loading BasePlayStateFromOel Level: " + levelFile);

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
            eventSprites = new FlxGroup();

            bigEx = new BigExplosion(-1000, -1000);


            //First build a dictionary of levelAttrs
            //This will determine how the level is built.

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level");

            //Console.WriteLine("----------------------------------" + levelAttrs);

            //foreach (KeyValuePair<string, string> kvp in levelAttrs)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}",
            //        kvp.Key, kvp.Value);
            //}


            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]) ;
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]) ;

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);


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

            destructableAttrs = new Dictionary<string, string>();
            destructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/DestructableTerrain");

            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;
            destructableTilemap.loadMap(destructableAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("initials/" + destructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            destructableTilemap.boundingBoxOverride = true;
            allLevelTiles.add(destructableTilemap);


            indestructableAttrs = new Dictionary<string, string>();
            indestructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/IndestructableTerrain");

            //leftExitBlockerWall = new FlxTileblock(0, FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FourChambers_Globals.TILE_SIZE_X * 2, FourChambers_Globals.TILE_SIZE_Y * 3);
            //leftExitBlockerWall.@fixed = false;
            //leftExitBlockerWall.loadTiles(FlxG.Content.Load<Texture2D>("initials/" + indestructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);
            //leftExitBlockerWall.velocity.X = -2000;

            leftExitBlockerWall = new FlxSprite(0, FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FlxG.Content.Load<Texture2D>("initials/exitBlocker"));
            leftExitBlockerWall.@fixed = true;
            allLevelTiles.add(leftExitBlockerWall);

            //rightExitBlockerWall = new FlxTileblock(FlxG.levelWidth - (FourChambers_Globals.TILE_SIZE_X * 2), FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FourChambers_Globals.TILE_SIZE_X * 2, FourChambers_Globals.TILE_SIZE_Y * 3);
            //rightExitBlockerWall.loadTiles(FlxG.Content.Load<Texture2D>("initials/" + indestructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);

            //rightExitBlockerWall.add(rightExitBlockerWall);

            rightExitBlockerWall = new FlxSprite(FlxG.levelWidth - (FourChambers_Globals.TILE_SIZE_X * 2), FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FlxG.Content.Load<Texture2D>("initials/exitBlocker"));
            rightExitBlockerWall.@fixed = true;
            allLevelTiles.add(rightExitBlockerWall);

            indestructableTilemap = new FlxTilemap();
            indestructableTilemap.auto = FlxTilemap.STRING;
            indestructableTilemap.loadMap(indestructableAttrs["IndestructableTerrain"], FlxG.Content.Load<Texture2D>("initials/" + indestructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            indestructableTilemap.boundingBoxOverride = true;
            allLevelTiles.add(indestructableTilemap);



            actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile(levelFile, "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                bool pc = false;
                int localWidth = 0;
                int localHeight = 0;

                if (nodes.ContainsKey("isPlayerControlled"))
                {
                    pc = Convert.ToBoolean(nodes["isPlayerControlled"]);
                }
                if (nodes.ContainsKey("width"))
                {
                    localWidth = Convert.ToInt32(nodes["width"]);
                }
                if (nodes.ContainsKey("height"))
                {
                    localHeight = Convert.ToInt32(nodes["height"]);
                }

                buildActor(nodes["Name"], 1, pc , Convert.ToInt32(nodes["x"]),Convert.ToInt32(nodes["y"]), localWidth, localHeight);

                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ",
                    //    kvp.Key, kvp.Value);


                }
                //Console.Write("\r\n");
            }

            eventsAttrs = new List<Dictionary<string, string>>();
            eventsAttrs = FlxXMLReader.readNodesFromOelFile(levelFile, "level/EventsLayer");

            foreach (Dictionary<string, string> nodes in eventsAttrs)
            {
                EventSprite s2 = new EventSprite(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), eventSpriteRun, Convert.ToInt32(nodes["repeat"]), nodes["event"] );
                s2.createGraphic(Convert.ToInt32(nodes["width"]), Convert.ToInt32(nodes["height"]), Color.Red);
                
                eventSprites.add(s2);

                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ",
                    //    kvp.Key, kvp.Value);


                }
                //Console.Write("\r\n");

                
            }

            add(eventSprites);






            Console.WriteLine("Done generating levels");

            // build atmospheric effects here

            paletteTexture = FlxG.Content.Load<Texture2D>("initials/" + levelAttrs["timeOfDayPalette"]);



            //FlxG.followAdjust(0.5f, 0.0f);
            FlxG.followBounds(0, 0, Convert.ToInt32(levelAttrs["width"]) , Convert.ToInt32(levelAttrs["height"]) );

            
            add(bullets);
            
            add(allLevelTiles);
            add(ladders);
            add(actors);
            add(powerUps);

            seraphine = new Seraphine(0, 0);
            seraphine.play("fly");
            add(seraphine);


            blood = new FlxEmitter();
            blood.x = 0;
            blood.y = 0;
            blood.width = 6;
            blood.height = 6;
            blood.delay = 0.8f;
            blood.setXSpeed(-152, 152);
            blood.setYSpeed(-250, -50);
            blood.setRotation(0, 0);
            blood.gravity = FourChambers_Globals.GRAVITY;
            blood.createSprites(FlxG.Content.Load<Texture2D>("initials/blood"), 1500, true, 1.0f, 0.1f);
            add(blood);


            tilesExplode = new FlxEmitter();
            tilesExplode.x = 0;
            tilesExplode.y = 0;
            tilesExplode.width = 16;
            tilesExplode.height = 16;
            tilesExplode.delay = 0.8f;
            tilesExplode.setXSpeed(-50, 50);
            tilesExplode.setYSpeed(-150, -50);
            tilesExplode.setRotation(0, 0);
            tilesExplode.gravity = FourChambers_Globals.GRAVITY;
            tilesExplode.createSprites(FlxG.Content.Load<Texture2D>("initials/" + destructableAttrs["tileset"]), 100, true, 1.0f, 0.1f);
            add(tilesExplode);
            tilesExplode.setScale(0.5f);



            add(bigEx);

            //if (FlxG.joystickBeingUsed) FlxG.mouse.hide();
            //else FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
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

                FlxG.state = new BasePlayStateFromOel();

                return;
            }
            else if (FlxG.keys.justPressed(Keys.F7) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.level--;
                if (FlxG.level < 1) FlxG.level = 25;

                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

                FlxG.transition.startFadeIn(0.2f);

                FlxG.state = new BasePlayStateFromOel();

                return;
            }
            else if (FlxG.keys.justPressed(Keys.F8) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                FlxG.transition.startFadeIn(0.2f);
                FlxG.state = new BasePlayStateFromOel();
                return;
            }

            // Allow editing of terrain if SHIFT + Mouse is pressed.
            if (FlxG.mouse.pressedRightButton() && FlxG.keys.SHIFT)
            {
                //Console.WriteLine((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X + " " + (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y);

                destructableTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y, 0, true);
                //decorationsTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, ((int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y) - 1, 0, true);
            }
            if (FlxG.mouse.pressedLeftButton() && FlxG.keys.SHIFT)
            {
                destructableTilemap.setTile((int)FlxG.mouse.x / FourChambers_Globals.TILE_SIZE_X, (int)FlxG.mouse.y / FourChambers_Globals.TILE_SIZE_Y, 1, true);
            }

            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            #endregion

            localHud.score.text = FlxG.score.ToString();
            if (marksman != null)
            {
                localHud.setArrowsRemaining(marksman.arrowsRemaining);

                localHud.nestsRemaining.text = actors.countLivingOfType("XNAMode.ZingerNest").ToString();
            }
            if (actors.countLivingOfType("XNAMode.ZingerNest") <= 0)
            {

                if (leftExitBlockerWall.velocity.Y == 0)
                {
                    FlxG.quake.start(0.005f, 0.5f);
                }

                leftExitBlockerWall.velocity.Y = -50;
                rightExitBlockerWall.velocity.Y = -50;

            }

            if (FlxG.gamepads.isButtonDown(Buttons.Y))
            {
                seraphine.velocity.Y = 0;
                seraphine.x = marksman.x - marksman.width / 2;
                seraphine.y = marksman.y - marksman.height;

            }
            else
            {
                seraphine.velocity.Y = -50;

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
            
            FlxU.overlap(playerControlledActors, eventSprites, eventCallback);
            FlxU.overlap(actors, bullets, overlapped);
            FlxU.overlap(actors, ladders, overlapWithLadder);

            FlxU.overlap(marksman.meleeHitBox, destructableTilemap, destroyTileAtMelee);


            FlxU.collide(destructableTilemap, bullets);

            FlxU.collide(blood, destructableTilemap);


            FlxU.overlap(actors, playerControlledActors, actorOverlap);

            FlxU.overlap(powerUps, playerControlledActors, getPowerUp);

            base.update();

            // exit.
            if (FlxG.keys.justPressed(Keys.Escape))
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

            int i2 = 0;
            int l2 = playerControlledActors.members.Count;
            while (i2 < l2)
            {

                //Console.WriteLine((playerControlledActors.members[i2] as FlxSprite).x + "    " + FlxG.levelWidth + "  " + playerControlledActors.members[i2]);

                if ((playerControlledActors.members[i2] as FlxSprite).x < 0)
                {
                    goToLevel(--FlxG.level);
                }
                if ((playerControlledActors.members[i2] as FlxSprite).x > FlxG.levelWidth)
                {
                    goToLevel(++FlxG.level);
                }

                i2++;
            }

            if (playerControlledActors.getFirstAlive() == null)
            {

                //FlxG.setHudText(1, "Press X to go to Menu \n Press Y to restart.");

                FlxG._game.hud.p1HudText.alignment = FlxJustification.Center;
                FlxG._game.hud.p1HudText.text = "Press X to go to Menu \n Press Y to restart.";
                FlxG.setHudTextScale(1, 2);
                FlxG.setHudTextPosition(1, 0, FlxG.height / 2);


                if (FlxG.gamepads.isButtonDown(Buttons.X) || FlxG.mouse.pressed())
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
            FlxG.state = new BasePlayStateFromOel();
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


        protected bool goToLevel(int Level)
        {
            FlxG.level = Level;

            if (FlxG.level > 11) FlxG.level = 1;
            else if (FlxG.level < 1) FlxG.level = 10;
            FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

            FlxG.transition.startFadeIn(0.2f);

            FlxG.state = new BasePlayStateFromOel();

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


        protected bool destroyTileAtMelee(object Sender, FlxSpriteCollisionEvent e)
        {
            if (destructableTilemap.getTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) != 0)
            {
                destructableTilemap.setTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y, 0, true);

                tilesExplode.x = (int)marksman.meleeHitBox.x;
                tilesExplode.y = (int)marksman.meleeHitBox.y;
                tilesExplode.start(true, 0, 4);
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
            if ((e.Object1 is Warlock) && (e.Object2 is Fireball))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is Arrow))
            {

            }
            else if ((e.Object1 is Marksman) && (e.Object2 is MeleeHitBox)) { }
            else if ((e.Object1 is Mistress) && (e.Object2 is MeleeHitBox))
            {

            }
            else if (e.Object1 is ZingerNest)
            {
                bigEx.x = e.Object1.x;
                bigEx.y = e.Object1.y;
                bigEx.play("explode", true);


                blood.at(e.Object1);

                blood.start(true, 0, 10);
                FlxObject z = zingers.getFirstDead();
                if (z != null)
                {
                    localHud.nestsRemaining.scale = 4;

                    z.dead = false;
                    z.exists = true;
                    z.x = e.Object1.x;
                    z.y = e.Object2.y;
                    z.flicker(0.001f);
                    z.velocity.X = 50;
                    z.velocity.Y = FlxU.random(-20, 20);
                    z.angle = 0;
                    z.visible = true;
                }
                // throw out a power up.
                FlxObject p = powerUps.getFirstDead();
                if (p != null)
                {
                    p.dead = false;
                    p.acceleration.Y = FourChambers_Globals.GRAVITY;
                    p.velocity.X = 0;
                    p.exists = true;
                    p.x = e.Object1.x;
                    p.y = e.Object1.y;
                    p.flicker(0.001f);
                    p.angle = 0;
                    p.visible = true;
                }
                else
                {
                    
                }
                
                //localHud.score.scale = 4;

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

        protected bool eventCallback(object Sender, FlxSpriteCollisionEvent e)
        {

            ((EventSprite)e.Object2).runCallback();
            ((EventSprite)e.Object2).hurt(1);


            return true;

        }


        public void eventSpriteRun(string command)
        {
            //Console.WriteLine("command is: " + command);

            if (command == "quake")
            {
                FlxG.quake.start(0.01f, 1.0f);
                
            }


        }

        public void buildActor(string ActorType, int NumberOfActors)
        {
            buildActor(ActorType, NumberOfActors, false, 0, 0,0,0);
        }
        public void buildActor(string ActorType, int NumberOfActors, bool playerControlled = false, int x=0, int y=0, int width=0, int height=0)
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
                    marksman = new Marksman(x, y, arrows.members);
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
                    
                    mistress = new Mistress(x, y );
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
                int j = 0;
                for (j = 0; j < BULLETS_PER_ACTOR;j++)
                    fireballs.add(new Fireball());
                bullets.add(fireballs);
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    warlock = new Warlock(x, y , fireballs.members);
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
                    
                    artist = new Artist(x, y);
                    actors.add(artist);
                }
            }
            #endregion
            #region Assassin
            if (ActorType == "assassin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    assassin = new Assassin(x, y);
                    actors.add(assassin);
                }
            }
            #endregion
            #region Automaton
            if (ActorType == "automaton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    automaton = new Automaton(x, y);
                    actors.add(automaton);


                }
            }
            #endregion
            #region Bat
            if (ActorType == "bat")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    bat = new Bat(x, y);
                    actors.add(bat);
                }
            }
            #endregion
            #region Blight
            if (ActorType == "blight")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    blight = new Blight(x, y);
                    actors.add(blight);
                }
            }
            #endregion
            #region Bloatedzombie
            if (ActorType == "bloatedzombie")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    bloatedzombie = new Bloatedzombie(x, y);
                    actors.add(bloatedzombie);
                }
            }
            #endregion
            #region Bogbeast
            if (ActorType == "bogbeast")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    bogbeast = new Bogbeast(x, y);
                    actors.add(bogbeast);
                }
            }
            #endregion
            #region Bombling
            if (ActorType == "bombling")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    bombling = new Bombling(x, y);
                    actors.add(bombling);
                }
            }
            #endregion
            #region Centaur
            if (ActorType == "centaur")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    centaur = new Centaur(x, y);
                    actors.add(centaur);
                }
            }
            #endregion
            #region Chicken
            if (ActorType == "chicken")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    chicken = new Chicken(x, y);
                    actors.add(chicken);
                }
            }
            #endregion
            #region Chimaera
            if (ActorType == "chimaera")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    chimaera = new Chimaera(x, y);
                    actors.add(chimaera);
                }
            }
            #endregion
            #region Corsair
            if (ActorType == "corsair")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    corsair = new Corsair(x, y);
                    actors.add(corsair);
                }
            }
            #endregion
            #region Cow
            if (ActorType == "cow")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    cow = new Cow(x, y);
                    actors.add(cow);
                }
            }
            #endregion
            #region Cyclops
            if (ActorType == "cyclops")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    cyclops = new Cyclops(x, y);
                    actors.add(cyclops);
                }
            }
            #endregion
            #region Deathclaw
            if (ActorType == "deathclaw")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    deathclaw = new Deathclaw(x, y);
                    actors.add(deathclaw);
                }
            }
            #endregion
            #region Deer
            if (ActorType == "deer")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    deer = new Deer(x, y);
                    actors.add(deer);
                }
            }
            #endregion
            #region Devil
            if (ActorType == "devil")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    devil = new Devil(x, y);
                    actors.add(devil);
                }
            }
            #endregion
            #region Djinn
            if (ActorType == "djinn")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    djinn = new Djinn(x, y);
                    actors.add(djinn);
                }
            }
            #endregion
            #region Drone
            if (ActorType == "drone")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    drone = new Drone(x, y);
                    actors.add(drone);
                }
            }
            #endregion
            #region Druid
            if (ActorType == "druid")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    druid = new Druid(x, y);
                    actors.add(druid);
                }
            }
            #endregion
            #region Dwarf
            if (ActorType == "dwarf")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    dwarf = new Dwarf(x, y);
                    actors.add(dwarf);
                }
            }
            #endregion
            #region Embersteed
            if (ActorType == "embersteed")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    embersteed = new Embersteed(x, y);
                    actors.add(embersteed);
                }
            }
            #endregion
            #region Executor
            if (ActorType == "executor")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    executor = new Executor(x, y);
                    actors.add(executor);
                }
            }
            #endregion
            #region Feline
            if (ActorType == "feline")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    feline = new Feline(x, y);
                    actors.add(feline);
                }
            }
            #endregion
            #region Floatingeye
            if (ActorType == "floatingeye")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    floatingeye = new Floatingeye(x, y);
                    actors.add(floatingeye);
                }
            }
            #endregion
            #region Fungant
            if (ActorType == "fungant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    fungant = new Fungant(x, y);
                    actors.add(fungant);
                }
            }
            #endregion
            #region Gelatine
            if (ActorType == "gelatine")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    gelatine = new Gelatine(x, y);
                    actors.add(gelatine);
                }
            }
            #endregion
            #region Gloom
            if (ActorType == "gloom")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    gloom = new Gloom(x, y);
                    actors.add(gloom);
                }
            }
            #endregion
            #region Glutton
            if (ActorType == "glutton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    glutton = new Glutton(x, y);
                    actors.add(glutton);
                }
            }
            #endregion
            #region Goblin
            if (ActorType == "goblin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    goblin = new Goblin(x, y);
                    actors.add(goblin);
                }
            }
            #endregion
            #region Golem
            if (ActorType == "golem")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    golem = new Golem(x, y);
                    actors.add(golem);
                }
            }
            #endregion
            #region Gorgon
            if (ActorType == "gorgon")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    gorgon = new Gorgon(x, y);
                    actors.add(gorgon);
                }
            }
            #endregion
            #region Gourmet
            if (ActorType == "gourmet")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    gourmet = new Gourmet(x, y);
                    actors.add(gourmet);
                }
            }
            #endregion
            #region Grimwarrior
            if (ActorType == "grimwarrior")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    grimwarrior = new Grimwarrior(x, y);
                    actors.add(grimwarrior);
                }
            }
            #endregion
            #region Grizzly
            if (ActorType == "grizzly")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    grizzly = new Grizzly(x, y);
                    actors.add(grizzly);
                }
            }
            #endregion
            #region Harvester
            if (ActorType == "harvester")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    harvester = new Harvester(x, y);
                    actors.add(harvester);
                }
            }
            #endregion
            #region Horse
            if (ActorType == "horse")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    horse = new Horse(x, y);
                    actors.add(horse);
                }
            }
            #endregion
            #region Ifrit
            if (ActorType == "ifrit")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    ifrit = new Ifrit(x, y);
                    actors.add(ifrit);
                }
            }
            #endregion
            #region Imp
            if (ActorType == "imp")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    imp = new Imp(x, y);
                    actors.add(imp);
                }
            }
            #endregion
            #region Kerberos
            if (ActorType == "kerberos")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    kerberos = new Kerberos(x, y);
                    actors.add(kerberos);
                }
            }
            #endregion
            #region Lich
            if (ActorType == "lich")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    lich = new Lich(x, y);
                    actors.add(lich);
                }
            }
            #endregion
            #region Lion
            if (ActorType == "lion")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    lion = new Lion(x, y);
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
                    
                    mechanic = new Mechanic(x, y);
                    actors.add(mechanic);
                }
            }
            #endregion
            #region Mephisto
            if (ActorType == "mephisto")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    mephisto = new Mephisto(x, y);
                    actors.add(mephisto);
                }
            }
            #endregion
            #region Merchant
            if (ActorType == "merchant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    merchant = new Merchant(x, y);
                    actors.add(merchant);
                }
            }
            #endregion
            #region Mermaid
            if (ActorType == "mermaid")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    mermaid = new Mermaid(x, y);
                    actors.add(mermaid);
                }
            }
            #endregion
            #region Mimick
            if (ActorType == "mimick")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    mimick = new Mimick(x, y);
                    actors.add(mimick);
                }
            }
            #endregion
            #region Monk
            if (ActorType == "monk")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    monk = new Monk(x, y);
                    actors.add(monk);
                }
            }
            #endregion
            #region Mummy
            if (ActorType == "mummy")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    mummy = new Mummy(x, y);
                    actors.add(mummy);
                }
            }
            #endregion
            #region Nightmare
            if (ActorType == "nightmare")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    nightmare = new Nightmare(x, y);
                    actors.add(nightmare);
                }
            }
            #endregion
            #region Nymph
            if (ActorType == "nymph")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    nymph = new Nymph(x, y);
                    actors.add(nymph);
                }
            }
            #endregion
            #region Ogre
            if (ActorType == "ogre")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    ogre = new Ogre(x, y);
                    actors.add(ogre);
                }
            }
            #endregion
            #region Paladin
            if (ActorType == "paladin")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    paladin = new Paladin(x, y);
                    actors.add(paladin);
                }
            }
            #endregion
            #region Phantom
            if (ActorType == "phantom")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    phantom = new Phantom(x, y);
                    actors.add(phantom);
                }
            }
            #endregion
            #region Priest
            if (ActorType == "priest")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    priest = new Priest(x, y);
                    actors.add(priest);
                }
            }
            #endregion
            #region Prism
            if (ActorType == "prism")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    prism = new Prism(x, y);
                    actors.add(prism);
                }
            }
            #endregion
            #region Rat
            if (ActorType == "rat")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    rat = new Rat(x, y);
                    actors.add(rat);
                }
            }
            #endregion
            #region Savage
            if (ActorType == "savage")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    savage = new Savage(x, y);
                    actors.add(savage);
                }
            }
            #endregion
            #region Seraphine
            if (ActorType == "seraphine")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    seraphine = new Seraphine(x, y);
                    actors.add(seraphine);
                }
            }
            #endregion
            #region Sheep
            if (ActorType == "sheep")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    sheep = new Sheep(x, y);
                    actors.add(sheep);
                }
            }
            #endregion
            #region Skeleton
            if (ActorType == "skeleton")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    skeleton = new Skeleton(x, y);
                    actors.add(skeleton);
                }
            }
            #endregion
            #region Snake
            if (ActorType == "snake")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    snake = new Snake(x, y);
                    actors.add(snake);
                }
            }
            #endregion
            #region Soldier
            if (ActorType == "soldier")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    soldier = new Soldier(x, y);
                    actors.add(soldier);
                }
            }
            #endregion
            #region Sphinx
            if (ActorType == "sphinx")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    sphinx = new Sphinx(x, y);
                    actors.add(sphinx);
                }
            }
            #endregion
            #region Spider
            if (ActorType == "spider")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    spider = new Spider(x, y);
                    actors.add(spider);
                }
            }
            #endregion
            #region Succubus
            if (ActorType == "succubus")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    succubus = new Succubus(x, y);
                    actors.add(succubus);
                }
            }
            #endregion
            #region Tauro
            if (ActorType == "tauro")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    tauro = new Tauro(x, y);
                    actors.add(tauro);
                }
            }
            #endregion
            #region Toad
            if (ActorType == "toad")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    toad = new Toad(x, y);
                    actors.add(toad);
                }
            }
            #endregion
            #region Tormentor
            if (ActorType == "tormentor")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    tormentor = new Tormentor(x, y);
                    actors.add(tormentor);
                }
            }
            #endregion
            #region Treant
            if (ActorType == "treant")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    treant = new Treant(x, y);
                    actors.add(treant);
                }
            }
            #endregion
            #region Troll
            if (ActorType == "troll")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    troll = new Troll(x, y);
                    actors.add(troll);
                }
            }
            #endregion
            #region Unicorn
            if (ActorType == "unicorn")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    unicorn = new Unicorn(x, y);
                    actors.add(unicorn);
                }
            }
            #endregion
            #region Vampire
            if (ActorType == "vampire")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    vampire = new Vampire(x, y);
                    actors.add(vampire);
                }
            }
            #endregion
            #region Willowisp
            if (ActorType == "willowisp")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    willowisp = new Willowisp(x, y);
                    actors.add(willowisp);
                }
            }
            #endregion
            #region Wizard
            if (ActorType == "wizard")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    wizard = new Wizard(x, y);
                    actors.add(wizard);
                }
            }
            #endregion
            #region Wolf
            if (ActorType == "wolf")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    wolf = new Wolf(x, y);
                    actors.add(wolf);
                }
            }
            #endregion
            #region Zinger
            if (ActorType == "zinger")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    
                    zinger = new Zinger(x, y);
                    zingers.add(zinger);
                    actors.add(zinger);

                    if (FourChambers_Globals.PIRATE_COPY)
                    {
                        ZingerHoming z = new ZingerHoming(x, y, marksman);
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
                    
                    zombie = new Zombie(x * FourChambers_Globals.TILE_SIZE_X, y * FourChambers_Globals.TILE_SIZE_X);
                    actors.add(zombie);
                }
            }
            #endregion
            #region ZingerNest
            if (ActorType == "zingernest")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    ZingerNest = new ZingerNest(x,y, zingers);
                    actors.add(ZingerNest);

                    zinger = new Zinger(x,y);
                    zingers.add(zinger);
                    actors.add(zinger);
                    zinger.dead = true;
                    zinger.visible = false;

                    powerUp = new PowerUp(x,y);
                    powerUp.dead = true;
                    powerUp.visible = false;
                    powerUps.add(powerUp);
                }
            }
            #endregion



            #region ladder
            if (ActorType == "ladder")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    ladder = new FlxTileblock(x,y,16, height );
                    ladder.loadTiles(FlxG.Content.Load<Texture2D>("initials/ladderTiles_16x16"), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);
                    ladders.add(ladder);
                }
            }
            
            #endregion

            #region fallAwayBridge
            if (ActorType == "fallAwayBridge")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    FallAwayBridgeBlock f;
                    if (width == 16) {
                        f = new FallAwayBridgeBlock(x,y);
                        allLevelTiles.add(f);
                    }
                    else
                    {
                        for (int e = 0; e < width / FourChambers_Globals.TILE_SIZE_X; e++)
                        {
                            f = new FallAwayBridgeBlock(x + e * FourChambers_Globals.TILE_SIZE_X, y + FourChambers_Globals.TILE_SIZE_Y);
                            allLevelTiles.add(f);
                        }
                    }

                    
                }
            }

            #endregion

        }
    }
}

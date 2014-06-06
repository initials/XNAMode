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

        private FireBall fireBall;
        private static FlxGroup fireBalls;

        // --- FlxGroups, for overlap collide.

        /// <summary>
        /// Fireballs for the warlock
        /// </summary>
        protected FlxGroup warlockFireBalls;

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

        private FlxEmitter specialFX;

        private LevelBeginText comboInfo;


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
        public Executor executor;
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
        public Marksman marksman;
        private Mechanic mechanic;
        private Mephisto mephisto;
        private Merchant merchant;
        private Mermaid mermaid;
        private Mimick mimick;
        public Mistress mistress;
        private Monk monk;
        private Mummy mummy;
        private Nightmare nightmare;
        private Nymph nymph;
        private Ogre ogre;
        public Paladin paladin;
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
        public Unicorn unicorn;
        public Vampire vampire;
        public Warlock warlock;
        private Willowisp willowisp;
        private Wizard wizard;
        private Wolf wolf;
        private Zinger zinger;
        private Zombie zombie;
        #endregion

        private MovingPlatform movingPlatform;
        private FireThrower fireThrower;
        private ZingerNest zingerNest;
        private FlxGroup zingers;
        private FlxGroup powerUps;
        private PowerUp powerUp;

        public Arrow arrow;
        private BigExplosion bigEx;

        private FlxSprite leftExitBlockerWall;
        private FlxSprite rightExitBlockerWall;
        private Door door;
        private FlxGroup doors;
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

            FlxG.colorFlickeringEnabled = true;
            FlxG.autoHandlePause = true;

            bgColor = Color.Black;

            // used for tutorial prompts.
            FlxG._game.hud.p1HudText.alignment = FlxJustification.Center;
            FlxG._game.hud.xboxButton.visible = false;
            FlxG._game.hud.xboxDirection.visible = false;

            // Account for minus levels (tutorials etc)
            // -1 = Tutorial
            string levelFile;
            if (FlxG.level >= 1)
            {
                levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";
            }
            else if (FlxG.level == -1)
            {
                levelFile = "ogmoLevels/levelTutorial.oel";
            }
            else
            {
                Console.WriteLine("Unknown level, loading level : " + FlxG.level.ToString());

                levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";
            }

            Console.WriteLine("Loading BasePlayStateFromOel Level: " + levelFile);

            //important to reset the hud to get the text, gamepad buttons out.
            FlxG.resetHud();
            FlxG.showHud();

            //FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            // initialize a bunch of groups
            actors = new FlxGroup();
            warlockFireBalls = new FlxGroup();
            bullets = new FlxGroup();
            arrows = new FlxGroup();
            ladders = new FlxGroup();
            allLevelTiles = new FlxGroup();
            playerControlledActors = new FlxGroup();
            zingers = new FlxGroup();
            powerUps = new FlxGroup();
            eventSprites = new FlxGroup();
            fireBalls = new FlxGroup();
            doors = new FlxGroup();


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


            if (FourChambers_Globals.gif== false)
            {
                Texture2D bgGraphic = FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["bgGraphic"]);
                bgSprite = new FlxSprite(0, 0, bgGraphic);
                bgSprite.loadGraphic(bgGraphic);
                bgSprite.scrollFactor.X = 0.4f;
                bgSprite.scrollFactor.Y = 0.4f;
                bgSprite.x = 0;
                bgSprite.y = 0;
                bgSprite.color = Color.DarkGray;
                bgSprite.boundingBoxOverride = false;
                add(bgSprite);
            }

            Console.WriteLine("Generate the levels caves/tiles.");

            destructableAttrs = new Dictionary<string, string>();
            destructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/DestructableTerrain");

            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;
            destructableTilemap.loadMap(destructableAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            destructableTilemap.boundingBoxOverride = true;
            allLevelTiles.add(destructableTilemap);
            destructableTilemap.collideMin = 0;
            destructableTilemap.collideMax = 21;

            indestructableAttrs = new Dictionary<string, string>();
            indestructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/IndestructableTerrain");

            //leftExitBlockerWall = new FlxSprite(0, FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FlxG.Content.Load<Texture2D>("fourchambers/exitBlocker"));
            //leftExitBlockerWall.@fixed = true;
            //allLevelTiles.add(leftExitBlockerWall);

            //rightExitBlockerWall = new FlxSprite(FlxG.levelWidth - (FourChambers_Globals.TILE_SIZE_X * 2), FlxG.levelHeight - (FourChambers_Globals.TILE_SIZE_X * 6), FlxG.Content.Load<Texture2D>("fourchambers/exitBlocker"));
            //rightExitBlockerWall.@fixed = true;
            //allLevelTiles.add(rightExitBlockerWall);

            indestructableTilemap = new FlxTilemap();
            indestructableTilemap.auto = FlxTilemap.STRING;
            indestructableTilemap.loadMap(indestructableAttrs["IndestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + indestructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            indestructableTilemap.boundingBoxOverride = true;

            allLevelTiles.add(indestructableTilemap);

            actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile(levelFile, "level/ActorsLayer");

            string addedMap = destructableAttrs["DestructableTerrain"];

            for (int i = 0; i < 20; i++)
            {
                fireBall = new FireBall(-1000, -1000);
                fireBalls.add(fireBall);

                //bullets.add(fireBall);
            }

            //bullets.add(fireBalls);

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                bool pc = false;
                int localWidth = 0;
                int localHeight = 0;
                string PX = "";
                string PY = "";
                uint PT = 0;
                int PS = 0;
                float PC = 0.0f;

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

                if (nodes.ContainsKey("pathNodesX")) PX = nodes["pathNodesX"];
                if (nodes.ContainsKey("pathNodesY")) PY = nodes["pathNodesY"];

                if (nodes.ContainsKey("pathType")) PT = FlxPath.convertStringValueForPathType(nodes["pathType"]);
                if (nodes.ContainsKey("pathSpeed")) PS = Convert.ToInt32(nodes["pathSpeed"]);
                if (nodes.ContainsKey("pathCornering")) PC = (float)(Convert.ToInt32(nodes["pathCornering"]));

                // these are for fireballs only!
                if (nodes.ContainsKey("angleCounter")) localWidth = Convert.ToInt32(nodes["angleCounter"]);
                if (nodes.ContainsKey("shootEvery")) PC = float.Parse(nodes["shootEvery"]);
                if (nodes.ContainsKey("levelToGoTo")) PC = float.Parse(nodes["levelToGoTo"]);
                // -----------------------------

                buildActor(nodes["Name"], 1, pc , Convert.ToInt32(nodes["x"]),Convert.ToInt32(nodes["y"]), localWidth, localHeight, PX,PY,PT,PS, PC);

                if (nodes["Name"] == "_event")
                {
                    buildEvent(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), Convert.ToInt32(nodes["width"]), Convert.ToInt32(nodes["height"]), Convert.ToInt32(nodes["repeat"]), nodes["event"]);

                }
                if (nodes["Name"] == "_procedurallyGeneratedArea")
                {
                    //Console.WriteLine(" PROCE GENE {0}, {1}", Convert.ToInt32(nodes["width"]) ,Convert.ToInt32(nodes["height"]));

                    FlxCaveGeneratorExt caveExt = new FlxCaveGeneratorExt(Convert.ToInt32(nodes["height"]) / FourChambers_Globals.TILE_SIZE_X, Convert.ToInt32(nodes["width"]) / FourChambers_Globals.TILE_SIZE_X);
                    caveExt.numSmoothingIterations = 5;
                    caveExt.initWallRatio = 0.55f;
                    string[,] tiles = caveExt.generateCaveLevel();
                    //caveExt.printCave(tiles);
                    string newMap = caveExt.convertMultiArrayStringToString(tiles);

                    addedMap = caveExt.addStrings(destructableAttrs["DestructableTerrain"], newMap, Convert.ToInt32(nodes["x"]) / FourChambers_Globals.TILE_SIZE_X, Convert.ToInt32(nodes["y"]) / FourChambers_Globals.TILE_SIZE_X, Convert.ToInt32(nodes["width"]) / FourChambers_Globals.TILE_SIZE_X, Convert.ToInt32(nodes["height"]) / FourChambers_Globals.TILE_SIZE_X );
                }

                //foreach (KeyValuePair<string, string> kvp in nodes)
                //{
                //    //Console.Write("Key = {0}, Value = {1}, ",
                //    //    kvp.Key, kvp.Value);
                //}
                //Console.Write("\r\n");
            }

            //reload the map
            destructableTilemap.loadMap(addedMap, FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            

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

            //paletteTexture = FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["timeOfDayPalette"]);
            paletteTexture = FlxG.Content.Load<Texture2D>("fourchambers/skyPalettes");

            //FlxG.followAdjust(0.5f, 0.0f);
            FlxG.followBounds(0, 0, Convert.ToInt32(levelAttrs["width"]) , Convert.ToInt32(levelAttrs["height"]) );

            
            add(bullets);
            add(allLevelTiles);
            add(ladders);
            add(doors);
            add(actors);
            add(powerUps);
            

            if (seraphine == null)
            {
                FlxG.write("Seraphine is null, making one");

                seraphine = new Seraphine(-100, -100);
                seraphine.play("fly");
                add(seraphine);
            }

            //buildActor("imp", 1);
            //buildActor("harvester", 1,false,-100,100,0,0,null,null,0,0,0);
            
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
            blood.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/blood"), 1500, true, 1.0f, 0.1f);
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
            tilesExplode.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), 100, true, 1.0f, 0.1f);
            add(tilesExplode);
            tilesExplode.setScale(0.5f);


            specialFX = new FlxEmitter();
            specialFX.x = 0;
            specialFX.y = 0;
            specialFX.width = 4;
            specialFX.height = 4;
            specialFX.delay = 0.8f;
            specialFX.setXSpeed(-50, 50);
            specialFX.setYSpeed(-850, 0);
            specialFX.setRotation(0, 360);
            specialFX.gravity = FourChambers_Globals.GRAVITY ;
            specialFX.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/sparkles_small"), 20, true, 1.0f, 0.1f);
            add(specialFX);

            
            add(bigEx);
            add(fireBalls);

            LevelBeginText t = new LevelBeginText(0, 50, FlxG.width);
            t.text = levelAttrs["levelName"];

            add(t);

            //if (FlxG.joystickBeingUsed) FlxG.mouse.hide();
            //else FlxG.mouse.show(FlxG.Content.Load<Texture2D>("fourchambers/crosshair"));

            if (marksman.hasRangeWeapon) FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
            else FlxG.mouse.hide();

            localHud = new PlayHud();
            FlxG._game.hud.hudGroup = localHud;

            Console.WriteLine("Level is: " + FlxG.level);

            FlxG.playMp3("music/" + levelAttrs["music"], 1.0f);

            // Exit the game and open up a webpage for buying the game, if it's a pirate copy.
            if (FourChambers_Globals.PIRATE_COPY && FlxG.level >= 4)
            {
                FlxU.openURL("http://initialsgames.com/fourchambers/purchasecopy.php");
                
                FlxG.Game.Exit();
            }

            comboInfo = new LevelBeginText(-1100, -1100, 200);
            comboInfo.setFormat(null, 1, Color.White, FlxJustification.Left, Color.Black);
            comboInfo.scrollFactor.X = 1;
            comboInfo.scrollFactor.Y = 1;
            comboInfo.text = "Combo Info!";
            add(comboInfo);

            comboInfo.limit = 0.74f;
            comboInfo.style = "up";

            // place marksman at the correct door

            foreach (Door d in doors.members)
            {
                if (d.levelToGoTo == FourChambers_Globals.previousLevel)
                {
                    marksman.x = d.x+door.width + 5;
                    marksman.y = d.y;
                }
            }

            //plots down some clusters of fireflies.
            for (int i = 0; i < 10; i++)
            {
                int xp = (int)FlxU.random(0, FlxG.levelWidth);
                int yp = (int)FlxU.random(0, FlxG.levelHeight);

                for (int j = 0; j < 10; j++)
                {
                    Firefly f = new Firefly(xp + (int)FlxU.random(-30, 30), yp - (int)FlxU.random(-30, 30));
                    add(f);

                    //Console.WriteLine("Color: " + FlxColor.ToColor(levelAttrs["fireflyColor"]));


                    f.color = FlxColor.ToColor(levelAttrs["fireflyColor"]);
                }
            }
        }

        override public void update()
        {
            runCheat();

            #region debugLevelSkip
            if (FlxG.keys.justPressed(Keys.F10) && FlxG.debug && timeOfDay > 2.0f)
            {
                foreach (var item in actors.members)
                {
                    if (item is ZingerNest) item.dead = true;
                }
            }
            if (FlxG.keys.justPressed(Keys.F9) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.level++;
                //if (FlxG.level > 25) FlxG.level = 1;

                FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

                FlxG.transition.startFadeIn(0.2f);

                FlxG.state = new BasePlayStateFromOel();

                return;
            }
            else if (FlxG.keys.justPressed(Keys.F7) && FlxG.debug && timeOfDay > 2.0f)
            {
                FlxG.level--;
                //if (FlxG.level < 1) FlxG.level = 25;

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

            localHud.combo.text = FourChambers_Globals.arrowCombo.ToString() + "x Combo";
            if (FourChambers_Globals.arrowCombo>5) localHud.combo.text += "!";

            localHud.score.text = "Score: " + FlxG.score.ToString();
            localHud.healthText.text = playerControlledActors.members[0].health.ToString();


            if (marksman != null)
            {
                localHud.setArrowsRemaining(marksman.arrowsRemaining);

				//localHud.nestsRemaining.text = "Unicorns Left: " + actors.countLivingOfType("FourChambers.Unicorn").ToString();
            }
            //if (actors.countLivingOfType("FourChambers.Unicorn") <= 0)
            //{

            //    if (leftExitBlockerWall.velocity.Y == 0)
            //    {
            //        FlxG.quake.start(0.025f, 1.5f);
            //    }

            //    leftExitBlockerWall.velocity.Y = -50;
            //    rightExitBlockerWall.velocity.Y = -50;

            //}

            if ((FlxG.gamepads.isButtonDown(Buttons.Y) || FlxG.keys.W) && FourChambers_Globals.seraphineHasBeenKilled == false)
            {
                //Console.WriteLine("SEREAPHINE");

				#if !__ANDROID__
				FlxG.bloom.Visible = true;
				#endif
                seraphine.velocity.Y = 0;
                seraphine.x = marksman.x - marksman.width / 2;
                seraphine.y = marksman.y - marksman.height;
                seraphine.facing = marksman.facing;

            }
            else if ((FlxG.gamepads.isButtonDown(Buttons.Y) || FlxG.keys.W) && FourChambers_Globals.seraphineHasBeenKilled == true)
            {
                //imp.x = marksman.x - 30;
                //imp.y = marksman.y - marksman.height;
                //imp.facing = marksman.facing;
            }
            else if (seraphine.dead == true)
            {
                seraphine.acceleration.Y = FourChambers_Globals.GRAVITY;
            }
            else if (marksman.canFly)
            {
                FlxG.bloom.Visible = false;
                seraphine.velocity.Y = -50;
            }
            else
            {
                FlxG.bloom.Visible = false;
                
            }


            timeOfDay += FlxG.elapsed * timeScale;
            if (timeOfDay > 239.99f) timeOfDay = 0.0f;

            //calculate time of day.
            if (FourChambers_Globals.gif == false && FlxG.level >= 1 && FlxG.level <= 100)
            {
                // color whole game.
                FlxG.color(FlxU.getColorFromBitmapAtPoint(paletteTexture, (int)timeOfDay, ((FlxG.level-1) * 10) + 5 ));
            }
            
            // bring time back to regular.
            if (FlxG.timeScale != 1.0f) FlxG.timeScale += 0.05f;
            if (FlxG.timeScale > 1.0f) FlxG.timeScale = 1.0f;

            bool ev = FlxU.overlap(playerControlledActors, eventSprites, eventCallback);
            if (!ev)
            {
                FlxG.setHudText(1, "");
            }

            //collides
            FlxU.collide(actors, allLevelTiles);
            FlxU.collide(powerUps, allLevelTiles);
            FlxU.overlap(actors, bullets, overlapped);
            FlxU.overlap(actors, fireBalls, overlappFireball);
            FlxU.overlap(seraphine, bullets, overlapped);

            FlxU.overlap(actors, doors, openDoor);

            FlxU.overlap(actors, ladders, overlapWithLadder);

            if (FourChambers_Globals.canDestroyTerrain)
            {
                FlxU.overlap(marksman.meleeHitBox, destructableTilemap, destroyTileAtMelee);
            }

            FlxU.overlap(seraphine, playerControlledActors, canNowFly);
            FlxU.collide(seraphine, allLevelTiles);
            // Maybe use the return value of this to reset the combo counter.
            FlxU.collide(allLevelTiles, bullets);
            FlxU.collide(blood, allLevelTiles);
            FlxU.overlap(actors, playerControlledActors, actorOverlap);
            FlxU.overlap(powerUps, playerControlledActors, getPowerUp);
            if (seraphine.dead) FlxU.collide(seraphine, allLevelTiles);
            FlxU.collide(fireBalls, allLevelTiles);

            base.update();

            // exit.
            if (FlxG.keys.justPressed(Keys.Escape))
            {
                int i = 0;
                int l = playerControlledActors.members.Count;
                while (i < l)
                {
                    (playerControlledActors.members[i] as FlxSprite).dead = true;
                    i++;
                }
                Console.WriteLine("Just pressed Escape and killed all player characters.");
            }

            int i2 = 0;
            int l2 = playerControlledActors.members.Count;
            while (i2 < l2)
            {
                //Console.WriteLine((playerControlledActors.members[i2] as FlxSprite).x + "    " + FlxG.levelWidth + "  " + playerControlledActors.members[i2]);
                if ((playerControlledActors.members[i2] as FlxSprite).x < 9)
                {
                    //Console.WriteLine("ArrowsFired : {0} Arrows Hit : {1}", FourChambers_Globals.arrowsFired, FourChambers_Globals.arrowsHitTarget);
                    //int newLevel = (int)FlxU.random(0, FourChambers_Globals.availableLevels.Count);
                    //FlxG.level = FourChambers_Globals.availableLevels[newLevel];
                    //FourChambers_Globals.availableLevels.RemoveAt(newLevel);
                    //goToLevel(++FlxG.level);
                    (playerControlledActors.members[i2] as FlxSprite).x = 10;
                }
                if ((playerControlledActors.members[i2] as FlxSprite).x > FlxG.levelWidth - 10)
                {
                    //goToLevel(++FlxG.level);
                    (playerControlledActors.members[i2] as FlxSprite).x = FlxG.levelWidth - 11;
                }

                i2++;
            }

            if (playerControlledActors.getFirstAlive() == null)
            {
                FourChambers_Globals.hasMeleeWeapon = false;
                FourChambers_Globals.hasRangeWeapon = false;
                //FlxG.setHudText(1, "Press X to go to Menu \n Press Y to restart.");
                FlxG._game.hud.p1HudText.alignment = FlxJustification.Center;

                if (marksman.hasUsedJoystickToAim)
                {
                    FlxG._game.hud.p1HudText.text = "Press B to go to Menu \n Press X to restart.";
                }
                else if (!marksman.hasUsedJoystickToAim)
                {
                    FlxG._game.hud.p1HudText.text = "Left Click to go to Menu \n Right Click to restart.";
                }
                FlxG.setHudTextScale(1, 2);
                FlxG.setHudTextPosition(1, 0, FlxG.height / 2);
                FourChambers_Globals.seraphineHasBeenKilled = false;

                if (FlxG.gamepads.isButtonDown(Buttons.B) || FlxG.mouse.pressedLeftButton())
                {
                    FlxOnlineStatCounter.sendStats("fourchambers", "marksman", FlxG.score);
                    goToMenu();
                }

                if (FlxG.gamepads.isButtonDown(Buttons.X) || FlxG.mouse.pressedRightButton())
                {
                    FlxOnlineStatCounter.sendStats("fourchambers", "marksman", FlxG.score);
                    restart();
                }
            }
        }

        /// <summary>
        /// Resets all values and restarts the playstate.
        /// </summary>
        private void restart()
        {
            FourChambers_Globals.seraphineHasBeenKilled = false;
            FourChambers_Globals.startGame();
            FlxG.state = new BasePlayStateFromOel();
        }

        /// <summary>
        /// Goes to menu.
        /// </summary>
        private void goToMenu()
        {
            FlxG.state = new GameSelectionMenuState();
        }

        protected bool goToLevel(int Level)
        {
            FourChambers_Globals.previousLevel = FlxG.level;

            FlxG.level = Level;

            FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

            FlxG.transition.startFadeIn(0.2f);

            FlxG.state = new BasePlayStateFromOel();

            return true;
        }


        protected bool openDoor(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1 is Marksman && (FlxControl.UPJUSTPRESSED ))
            {
                goToLevel(((Door)(e.Object2)).levelToGoTo);

            }
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
            if (e.Object1 is BaseActor)
            {
                if (!((BaseActor)(e.Object1)).flying)
                {
                    ((BaseActor)(e.Object1)).ladderPosX = e.Object2.x;
                    ((BaseActor)(e.Object1)).canClimbLadder = true;
                }
            }
            return true;
        }


        protected bool destroyTileAtMelee(object Sender, FlxSpriteCollisionEvent e)
        {
            if (destructableTilemap.getTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) > destructableTilemap.collideMin &&
                destructableTilemap.getTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) <= destructableTilemap.collideMax)
            {
                if (destructableTilemap.getTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) - 1) > destructableTilemap.collideMax)
                {
                    destructableTilemap.setTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) - 1, 0, true);
                }
                if (destructableTilemap.getTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) + 1) > destructableTilemap.collideMax)
                {
                    destructableTilemap.setTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y) + 1, 0, true);
                }
                if (destructableTilemap.getTile((int)(marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X)-1, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y)) > destructableTilemap.collideMax)
                {
                    destructableTilemap.setTile((int)(marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X)-1, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y), 0, true);
                }
                if (destructableTilemap.getTile((int)(marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X) + 1, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y)) > destructableTilemap.collideMax)
                {
                    destructableTilemap.setTile((int)(marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X) + 1, (int)(marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y), 0, true);
                }

                destructableTilemap.setTile((int)marksman.meleeHitBox.x / FourChambers_Globals.TILE_SIZE_X, (int)marksman.meleeHitBox.y / FourChambers_Globals.TILE_SIZE_Y, 0, true);

                tilesExplode.x = (int)marksman.meleeHitBox.x;
                tilesExplode.y = (int)marksman.meleeHitBox.y;
                tilesExplode.start(true, 0, 4);
            }
            return true;
        }

        protected bool getPowerUp(object Sender, FlxSpriteCollisionEvent e)
        {
            specialFX.at(e.Object1);
            specialFX.start(true, 0, 30);

            int x = ((PowerUp)e.Object1).typeOfPowerUp;
            if (x == 154 || x == 155 || x == 156 || x == 157)
            {
                if (marksman != null)
                    marksman.arrowsRemaining += 20;
            }
            else if (x == 190)
            {
                marksman.hasRangeWeapon = true;

                FourChambers_Globals.hasRangeWeapon = true;

                FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));

            }
            else if (x == 208)
            {
                marksman.hasMeleeWeapon = true;

                FourChambers_Globals.hasMeleeWeapon = true;

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
            // Only do this for object2, which is the player controlled actor.
            if (e.Object1.dead == false && e.Object2.dead == false && e.Object1.flickering() == false && e.Object2.flickering() == false)
            {
                
                if (! ((FlxSprite)(e.Object2)).colorFlickering())
                {
                    blood.at(e.Object2);

                    blood.start(true, 0, 10);

                    e.Object2.hurt(1);
                }
                //e.Object1.hurt(1);
            }

            if (e.Object1.dead == true && e.Object2.dead == false && 
                (((FlxG.keys.S&&FlxG.mouse.justPressedRightButton()) 
                || (FlxG.gamepads.isNewButtonPress(Buttons.X) && (FlxG.gamepads.isButtonDown(Buttons.DPadDown) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown))) )   )
                
                )
            {
                blood.at(e.Object1);

                blood.start(true, 0, 50);
            }

            return true;
        }

        protected bool canNowFly(object Sender, FlxSpriteCollisionEvent e)
        {
            FlxG.write("Player can now fly");

            if (marksman.canFly == false)
            {
                specialFX.at(e.Object1);
                specialFX.start(true, 0, 30);
            }

            FourChambers_Globals.seraphineHasBeenKilled = false;

            marksman.canFly = true;
            return true;
        }
        protected bool overlappFireball(object Sender, FlxSpriteCollisionEvent e)
        {
            blood.at(e.Object1);

            blood.start(true, 0, 10);

            e.Object1.hurt(2);
            e.Object2.hurt(2);
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
            // First reject Actors and their bullets.
            if ((e.Object1 is Warlock) && (e.Object2 is WarlockFireBall)) { }
            else if ((e.Object1 is Marksman) && (e.Object2 is Arrow)) { }
            else if ((e.Object1 is Marksman) && (e.Object2 is MeleeHitBox)) { }
            else if ((e.Object1 is Mistress) && (e.Object2 is MeleeHitBox)) 
            {
                if (((MeleeHitBox)(e.Object2)).belongsTo == "mistress")
                {

                }
                else
                {
                    e.Object1.hurt(1);

                    if (!e.Object1.dead && !((FlxSprite)(e.Object2)).colorFlickering())
                    {
                        blood.at(e.Object1);
                        blood.start(true, 0, 10);
                    }
                }

            }
            else if ((e.Object1 is Mistress) && (e.Object2 is Arrow) && (e.Object1 as Mistress).hurtTimer < 1.0f) {  }
            //Then collide custom objects.
            else if (e.Object1 is ZingerNest)
            {
                if (e.Object2 is Arrow)
                {
                    FourChambers_Globals.arrowsHitTarget++;
                    FourChambers_Globals.arrowCombo++;
                }
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
                    z.acceleration.Y = 50;
                    ((FlxSprite)(z)).play("fly");

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

                e.Object2.x = -1000;
                e.Object2.y = -1000;
                e.Object2.kill();
                e.Object1.kill();

            }
            else if ((e.Object1 is Seraphine) && (e.Object2 is Arrow))
            {
                FourChambers_Globals.seraphineHasBeenKilled = true;

                if (FourChambers_Globals.gif == false)
                {
                    foreach (var p in powerUps.members)
                    {
                        p.dead = false;
                        p.acceleration.Y = FourChambers_Globals.GRAVITY;
                        p.velocity.X = FlxU.random(-220, 220);
                        p.exists = true;
                        p.x = e.Object1.x;
                        p.y = e.Object1.y;
                        p.flicker(0.001f);
                        p.angle = 0;
                        p.visible = true;
                    }
                }

                if (e.Object2 is Arrow)
                {
                    FourChambers_Globals.arrowsHitTarget++;
                    FourChambers_Globals.arrowCombo++;
                }
                e.Object1.velocity.X = e.Object2.velocity.X;
                e.Object1.velocity.Y = e.Object2.velocity.Y;

                e.Object1.hurt(1);

                e.Object2.x = -1000;
                e.Object2.y = -1000;
                e.Object2.kill();

                blood.at(e.Object1);

                blood.start(true, 0, 50);
            }
            // Now that it's a kill, spurt some blood and "hurt" both parties.
            else if (e.Object1.dead == false && e.Object2.dead == false && !((FlxSprite)(e.Object1)).colorFlickering())
            {
                if (e.Object2 is Arrow)
                {
                    FourChambers_Globals.arrowsHitTarget++;
                    FourChambers_Globals.arrowCombo++;

                    //comboInfo.x = e.Object1.x + 20;
                    //comboInfo.y = e.Object1.y;

                    BaseActor zx = ((BaseActor)(e.Object1));
                    //comboInfo.text = zx.actorName + " " + FourChambers_Globals.arrowCombo + "x" + zx.score.ToString() + "=" + (FourChambers_Globals.arrowCombo * zx.score).ToString();
                    //comboInfo.text = (FourChambers_Globals.arrowCombo * zx.score).ToString();
                    //comboInfo.counter = 0;


                    Vector2 p = e.Object1.getScreenXY();


                    localHud.comboOnScreen.x = p.X * FlxG.zoom;
                    localHud.comboOnScreen.y = (p.Y - 20) * FlxG.zoom;

                    localHud.comboOnScreen.counter = 0;

                    localHud.comboOnScreen.text = zx.actorName + "\n" + FourChambers_Globals.arrowCombo + " x " + zx.score.ToString() ;
                    localHud.comboOnScreen.flyAwayText = zx.actorName+"\n" + (FourChambers_Globals.arrowCombo * zx.score).ToString();

                    //Console.WriteLine(localHud.comboOnScreen.x + " " + localHud.comboOnScreen.y + " " + zx.x + " " + zx.y + " " + FlxG.mouse.x + " " + FlxG.scroll.X);

                }

                

                e.Object2.x = -1000;
                e.Object2.y = -1000;
                e.Object2.kill();


                // -- 
                if (!e.Object1.dead && !((FlxSprite)(e.Object1)).colorFlickering())
                {
                    e.Object1.hurt(e.Object2.damage);
                    blood.at(e.Object1);
                    blood.start(true, 0, 10);
                }

                if (!e.Object1.dead) localHud.comboOnScreen.x = -1000;

            }

            return true;

        }

        protected bool eventCallback(object Sender, FlxSpriteCollisionEvent e)
        {

            ((EventSprite)e.Object2).runCallback();
            if (((EventSprite)e.Object2).repeats >=0)
                ((EventSprite)e.Object2).hurt(((EventSprite)e.Object2).repeats);

            return true;
        }

        
        public void eventSpriteRun(string command)
        {
            #region commands

            if (command.StartsWith("quake"))
            {
                FlxG.quake.start(0.01f, 1.0f);
            }
            else if (command.StartsWith("artist"))
            {
                artist.startPlayingBack();
            }
            else if (command.StartsWith("assassin"))
            {
                assassin.startPlayingBack();
            }
            else if (command.StartsWith("automaton"))
            {
                automaton.startPlayingBack();
            }
            else if (command.StartsWith("blight"))
            {
                blight.startPlayingBack();
            }
            else if (command.StartsWith("bloatedzombie"))
            {
                bloatedzombie.startPlayingBack();
            }
            else if (command.StartsWith("bogbeast"))
            {
                bogbeast.startPlayingBack();
            }
            else if (command.StartsWith("bombling"))
            {
                bombling.startPlayingBack();
            }
            else if (command.StartsWith("centaur"))
            {
                centaur.startPlayingBack();
            }
            else if (command.StartsWith("chicken"))
            {
                chicken.startPlayingBack();
            }
            else if (command.StartsWith("chimaera"))
            {
                chimaera.startPlayingBack();
            }
            else if (command.StartsWith("corsair"))
            {
                corsair.startPlayingBack();
            }
            else if (command.StartsWith("cow"))
            {
                cow.startPlayingBack();
            }
            else if (command.StartsWith("cyclops"))
            {
                cyclops.startPlayingBack();
            }
            else if (command.StartsWith("deathclaw"))
            {
                deathclaw.startPlayingBack();
            }
            else if (command.StartsWith("deer"))
            {
                deer.startPlayingBack();
            }
            else if (command.StartsWith("devil"))
            {
                devil.startPlayingBack();
            }
            else if (command.StartsWith("djinn"))
            {
                djinn.startPlayingBack();
            }
            else if (command.StartsWith("druid"))
            {
                druid.startPlayingBack();
            }
            else if (command.StartsWith("dwarf"))
            {
                dwarf.startPlayingBack();
            }
            else if (command.StartsWith("embersteed"))
            {
                embersteed.startPlayingBack();
            }
            else if (command.StartsWith("executor"))
            {
                executor.startPlayingBack();
            }
            else if (command.StartsWith("feline"))
            {
                feline.startPlayingBack();
            }
            else if (command.StartsWith("floatingeye"))
            {
                floatingeye.startPlayingBack();
            }
            else if (command.StartsWith("fungant"))
            {
                fungant.startPlayingBack();
            }
            else if (command.StartsWith("gelatine"))
            {
                gelatine.startPlayingBack();
            }
            else if (command.StartsWith("gloom"))
            {
                gloom.startPlayingBack();
            }
            else if (command.StartsWith("glutton"))
            {
                glutton.startPlayingBack();
            }
            else if (command.StartsWith("goblin"))
            {
                goblin.startPlayingBack();
            }
            else if (command.StartsWith("golem"))
            {
                golem.startPlayingBack();
            }
            else if (command.StartsWith("gorgon"))
            {
                gorgon.startPlayingBack();
            }
            else if (command.StartsWith("gourmet"))
            {
                gourmet.startPlayingBack();
            }
            else if (command.StartsWith("grimwarrior"))
            {
                grimwarrior.startPlayingBack();
            }
            else if (command.StartsWith("grizzly"))
            {
                grizzly.startPlayingBack();
            }
            else if (command.StartsWith("harvester"))
            {
                harvester.startPlayingBack();
            }
            else if (command.StartsWith("horse"))
            {
                horse.startPlayingBack();
            }
            else if (command.StartsWith("ifrit"))
            {
                ifrit.startPlayingBack();
            }
            else if (command.StartsWith("imp"))
            {
                imp.startPlayingBack();
            }
            else if (command.StartsWith("kerberos"))
            {
                kerberos.startPlayingBack();
            }
            else if (command.StartsWith("lich"))
            {
                lich.startPlayingBack();
            }
            else if (command.StartsWith("lion"))
            {
                lion.startPlayingBack();
            }
            else if (command.StartsWith("mechanic"))
            {
                mechanic.startPlayingBack();
            }
            else if (command.StartsWith("mephisto"))
            {
                mephisto.startPlayingBack();
            }
            else if (command.StartsWith("merchant"))
            {
                merchant.startPlayingBack();
            }
            else if (command.StartsWith("mermaid"))
            {
                mermaid.startPlayingBack();
            }
            else if (command.StartsWith("mimick"))
            {
                mimick.startPlayingBack();
            }
            else if (command.StartsWith("mistress"))
            {
                string[] split = command.Split('_');
                if (split.Length == 1) mistress.startPlayingBack();
                else mistress.startPlayingBack("FourChambers/ActorRecording/" + command + ".txt");
            }
            else if (command.StartsWith("monk"))
            {
                monk.startPlayingBack();
            }
            else if (command.StartsWith("mummy"))
            {
                mummy.startPlayingBack();
            }
            else if (command.StartsWith("nightmare"))
            {
                nightmare.startPlayingBack();
            }
            else if (command.StartsWith("nymph"))
            {
                nymph.startPlayingBack();
            }
            else if (command.StartsWith("ogre"))
            {
                ogre.startPlayingBack();
            }
            else if (command.StartsWith("paladin"))
            {
                string[] split = command.Split('_');
                if (split.Length == 1) paladin.startPlayingBack();
                else paladin.startPlayingBack(split[1]);
            }
            else if (command.StartsWith("phantom"))
            {
                phantom.startPlayingBack();
            }
            else if (command.StartsWith("priest"))
            {
                priest.startPlayingBack();
            }
            else if (command.StartsWith("prism"))
            {
                prism.startPlayingBack();
            }
            else if (command.StartsWith("rat"))
            {
                rat.startPlayingBack();
            }
            else if (command.StartsWith("savage"))
            {
                savage.startPlayingBack();
            }
            else if (command.StartsWith("sheep"))
            {
                sheep.startPlayingBack();
            }
            else if (command.StartsWith("skeleton"))
            {
                skeleton.startPlayingBack();
            }
            else if (command.StartsWith("snake"))
            {
                snake.startPlayingBack();
            }
            else if (command.StartsWith("soldier"))
            {
                soldier.startPlayingBack();
            }
            else if (command.StartsWith("sphinx"))
            {
                sphinx.startPlayingBack();
            }
            else if (command.StartsWith("spider"))
            {
                spider.startPlayingBack();
            }
            else if (command.StartsWith("succubus"))
            {
                succubus.startPlayingBack();
            }
            else if (command.StartsWith("tauro"))
            {
                tauro.startPlayingBack();
            }
            else if (command.StartsWith("toad"))
            {
                toad.startPlayingBack();
            }
            else if (command.StartsWith("tormentor"))
            {
                tormentor.startPlayingBack();
            }
            else if (command.StartsWith("treant"))
            {
                treant.startPlayingBack();
            }
            else if (command.StartsWith("troll"))
            {
                troll.startPlayingBack();
            }
            else if (command.StartsWith("unicorn"))
            {
                string[] split = command.Split('_');
                if (split.Length == 1) unicorn.startPlayingBack();
                else unicorn.startPlayingBack(split[1]);
            }
            else if (command.StartsWith("vampire"))
            {
                vampire.startPlayingBack();
            }
            //else if (command.StartsWith("warlock"))
            //{
            //    warlock.startPlayingBack();
            //}
            else if (command.StartsWith("willowisp"))
            {
                willowisp.startPlayingBack();
            }
            else if (command.StartsWith("wizard"))
            {
                wizard.startPlayingBack();
            }
            else if (command.StartsWith("wolf"))
            {
                wolf.startPlayingBack();
            }
            else if (command.StartsWith("zombie"))
            {
                zombie.startPlayingBack();
            }

            else
            {
                FlxG.setHudText(1, command);
                FlxG.setHudTextScale(1, 2);
                FlxG.setHudTextPosition(1, FlxG._game.hud.p1OriginalPosition.X, 20);
            }

            #endregion
        }

        /// <summary>
        /// Runs cheat from the Global cheatstring.
        /// </summary>
        public void runCheat()
        {
            if (FlxGlobal.cheatString != null)
            {

                if (FlxGlobal.cheatString.StartsWith("killzingers"))
                {
                    foreach (var item in actors.members)
                    {
                        if (item is ZingerNest) item.dead = true;
                    }
                }
                else if (FlxGlobal.cheatString.StartsWith("killemall"))
                {
                    foreach (var item in actors.members)
                    {
                        if (!(item is Marksman)) item.dead = true;
                    }
                }
                else if (FlxGlobal.cheatString.StartsWith("build"))
                {

                    string actor = FlxGlobal.cheatString.Substring(5);

                    buildActor(actor, 1, true, 100, 40, 0, 0, null, null, 0, 0, 0);

                    
                }
                else if (FlxGlobal.cheatString.StartsWith("controlmistress"))
                {
                    mistress.isPlayerControlled = true;
                    marksman.isPlayerControlled = false;
                    FlxG.follow(mistress,1.0f);
                }
                else if (FlxGlobal.cheatString.StartsWith("door"))
                {
                    marksman.x = doors.members[Convert.ToInt32(FlxGlobal.cheatString.Substring(4))].x;
                    marksman.y = doors.members[Convert.ToInt32(FlxGlobal.cheatString.Substring(4))].y;
                }
                else if (FlxGlobal.cheatString.StartsWith("next")) marksman.x = FlxG.levelWidth + 3;

            }
            //FlxGlobal.cheatString = "";

        }

        public void buildEvent(int x=0, int y=0, int width=0, int height=0, int repeat=-1, string eventOrQuote="")
        {
            if (eventOrQuote == "sword")
            {
                powerUp = new PowerUp(x, y);
                //powerUp.typeOfPowerUp = 201;
                powerUps.add(powerUp);
                powerUp.TypeOfPowerUp(208);
            }
            else if (eventOrQuote == "bow")
            {
                powerUp = new PowerUp(x, y);
                //powerUp.typeOfPowerUp = 177;
                powerUps.add(powerUp);
                powerUp.TypeOfPowerUp(190);
            }
            else
            {
                EventSprite s2 = new EventSprite(x, y, eventSpriteRun, repeat, eventOrQuote);
                s2.createGraphic(width, height, Color.Red);
                eventSprites.add(s2);
            }

        }

        public void buildActor(string ActorType, int NumberOfActors)
        {
            buildActor(ActorType, NumberOfActors, false, 0, 0,0,0, "", "", 0, 40,3.0f);
        }
        public void buildActor(string ActorType, 
            int NumberOfActors, 
            bool playerControlled = false, 
            int x=0, 
            int y=0, 
            int width=0, 
            int height=0, 
            string PathNodesX="", 
            string PathNodesY="",
            uint PathType=0,
            int PathSpeed=40, 
            float PathCornering=3.0f)
        {
            if (ActorType == "door")
            {
                door = new Door(x - 8, y - 8);
                door.levelToGoTo = (int)PathCornering;
                doors.add(door);
            }
            //Console.WriteLine("Building actor " + ActorType + " " + NumberOfActors);
            if (ActorType == "fireThrower")
            {
                fireThrower = new FireThrower(x-8, y-8, fireBalls.members);
                fireThrower.shootEvery = PathCornering;
                fireThrower.angleCount = width;
                add(fireThrower);
            }
            #region movingPlatform
            if (ActorType == "movingPlatform")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    movingPlatform = new MovingPlatform(x, y);
                    movingPlatform.createGraphic(width, height, Color.Red);
                    movingPlatform.solid = true;
                    movingPlatform.@fixed = true;

                    allLevelTiles.add(movingPlatform);
                    //Console.WriteLine("Building a movingPlatform {0} {1} {2} {3}", x, y, PathNodesX, PathNodesY);

                    if (PathNodesX != "" && PathNodesY != "")
                    {
                        //Console.WriteLine("Building a path {0} {1} {2}", PathNodesX, PathNodesY, PathCornering);

                        FlxPath xpath = new FlxPath(null);
                        xpath.add(x, y);
                        xpath.addPointsUsingStrings(PathNodesX, PathNodesY);
                        movingPlatform.followPath(xpath, PathSpeed, PathType, false);
                        movingPlatform.pathCornering = PathCornering;


                    }

                }
            }
            #endregion            
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
                    //mistress.flicker(2);
                    //playerControlledActors.add(mistress);
                    bullets.add(mistress.whipHitBox);

                }

                //if (levelAttrs["playerControlled"] == "mistress")
                //{
                //    mistress.isPlayerControlled = true;
                //    FlxG.follow(mistress, FOLLOW_LERP);
                //}
            }
            #endregion
            #region Warlock
            if (ActorType == "warlock")
            {
                int j = 0;
                for (j = 0; j < BULLETS_PER_ACTOR;j++)
                    warlockFireBalls.add(new WarlockFireBall());
                bullets.add(warlockFireBalls);
                for (int i = 0; i < NumberOfActors; i++)
                {

                    warlock = new Warlock(x, y, warlockFireBalls.members);
                    actors.add(warlock);
                    warlock.flicker(2);
                    //playerControlledActors.add(warlock);
                }
                //if (levelAttrs["playerControlled"] == "warlock")
                //{
                //    warlock.isPlayerControlled = true;
                //    FlxG.follow(warlock, FOLLOW_LERP);
                //}
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
                    Console.WriteLine("Building a bat {0} {1} {2} {3}", x, y, PathNodesX, PathNodesY);

                    if (PathNodesX != "" && PathNodesY != "")
                    {
                        Console.WriteLine("Building a path {0} {1} {2}", PathNodesX, PathNodesY , PathCornering);

                        FlxPath xpath = new FlxPath(null);
                        xpath.add(x,y);
                        xpath.addPointsUsingStrings(PathNodesX,PathNodesY);
                        bat.followPath(xpath, PathSpeed, PathType, false);
                        bat.pathCornering = PathCornering;


                    }

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
                    
                    chicken = new Chicken(x, y-16);
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
                    FlxG.write("Making a seraphine" + x + " " + y);

                    seraphine = new Seraphine(x, y);
                    add(seraphine);
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
                    
                    unicorn = new Unicorn(x, y-4);
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

                    if (PathNodesX != "" && PathNodesY != "")
                    {
                        FlxPath xpath = new FlxPath(null);
                        xpath.add(x, y);
                        xpath.addPointsUsingStrings(PathNodesX, PathNodesY);
                        zinger.followPath(xpath, PathSpeed, PathType, false);
                    }


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
                    
                    zombie = new Zombie(x,y);
                    actors.add(zombie);
                }
            }
            #endregion
            #region ZingerNest
            if (ActorType == "zingernest")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    zingerNest = new ZingerNest(x,y, zingers);
                    actors.add(zingerNest);

                    powerUp = new PowerUp(x,y);
                    powerUp.dead = true;
                    powerUp.visible = false;
                    powerUps.add(powerUp);

                    if (FourChambers_Globals.PIRATE_COPY == true)
                    {
                        ZingerHoming z = new ZingerHoming(x, y, marksman);
                        zingers.add(z);
                        actors.add(z);
                        z.dead = true;
                        z.visible = false;
                    }
                    else
                    {
                        zinger = new Zinger(x, y);
                        zingers.add(zinger);
                        actors.add(zinger);
                        zinger.dead = true;
                        zinger.visible = false;
                    }


                }
            }
            #endregion
            #region ladder
            if (ActorType == "ladder")
            {
                for (int i = 0; i < NumberOfActors; i++)
                {
                    ladder = new FlxTileblock(x,y,16, height );
                    ladder.auto = FlxTileblock.RANDOM;
                    ladder.loadTiles(FlxG.Content.Load<Texture2D>("fourchambers/ladderTiles_16x16"), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);
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

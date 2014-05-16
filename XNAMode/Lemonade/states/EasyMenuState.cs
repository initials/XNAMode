using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;
using XNATweener;

namespace Lemonade
{
    /// <summary>
    /// Cheat codes: likeyouroldstuff - brings up Warehouse, Factory & Management.
    /// </summary>
    public class EasyMenuState : FlxState
    {
        FlxEmitter bubbleParticle;
        FlxText location;
        FlxText levelText;
        FlxText multiplayerText;

        FlxTilemap ny;
        FlxTilemap ny2;

        FlxTilemap military;
        FlxTilemap military2;
        FlxTilemap military3;

        FlxTilemap sydney;
        FlxTilemap sydney2;
        FlxTilemap sydney3;

        FlxTilemap management;

        FlxTilemap factory;

        FlxTilemap warehouse;

        FlxSprite badge1;
        FlxSprite badge2;
        FlxSprite badge3;
        FlxSprite badge4;

        Tweener tweenBounce;

        Color selected = new Color(237, 0, 142); //rgb(237, 0, 142)
        Color notSelected = new Color(1, 1, 1);

        int currentLocation;
        List<string> possibleLocations = new List<string>();
        List<string> totalLocations = new List<string>();

        int currentLevel = FlxG.level;

        int currentSelected;
        Color notDone;
        Color done;
        bool locked;
        string lockedPhrase;

        override public void create()
        {
			FlxG.maxElapsed = 0.666f;

            locked = false;
            lockedPhrase = "";
			currentLevel = FlxG.level;

            base.create();

            currentLocation = Lemonade_Globals.LAST_LOCATION ;
            currentSelected = Lemonade_Globals.LAST_SELECTED_ON_MENU;
            
            possibleLocations.Add("military");
            possibleLocations.Add("sydney");
            possibleLocations.Add("newyork");
            //possibleLocations.Add("warehouse");
            //possibleLocations.Add("factory");
            //possibleLocations.Add("management");


            totalLocations.Add("military");
            totalLocations.Add("sydney");
            totalLocations.Add("newyork");
            totalLocations.Add("warehouse");
            totalLocations.Add("factory");
            totalLocations.Add("management");

            // play some music
            FlxG.playMp3("Lemonade/music/music_menu_1", 0.75f);

            // load some tile maps

            //List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/newyork/bgnewyork.tmx", "map", "bg", FlxXMLReader.TILES);
            //ny = new FlxTilemap();
            //ny.auto = FlxTilemap.STRING;
            //ny.indexOffset = -1;
            //ny.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            //ny.boundingBoxOverride = true;
            //ny.setScrollFactors(0, 0);
            //ny.visible = false;
            //add(ny);

            add(ny = createMap("Lemonade/levels/slf2/newyork/bgnewyork.tmx", "bg", "newyork"));
            add(ny2 = createMap("Lemonade/levels/slf2/newyork/bgnewyork.tmx", "bg2", "newyork"));

            add(sydney = createMap("Lemonade/levels/slf2/sydney/bgsydney.tmx", "bg", "sydney"));
            add(sydney2 = createMap("Lemonade/levels/slf2/sydney/bgsydney.tmx", "bg2", "sydney"));
            add(sydney3 = createMap("Lemonade/levels/slf2/sydney/bgsydney.tmx", "bg3", "sydney"));

            add(military = createMap("Lemonade/levels/slf2/military/bgmilitary.tmx", "bg", "military"));
            add(military2 = createMap("Lemonade/levels/slf2/military/bgmilitary.tmx", "bg2", "military"));
            add(military3 = createMap("Lemonade/levels/slf2/military/bgmilitary.tmx", "bg3", "military"));

            //List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/military/bgmilitary.tmx", "map", "bg", FlxXMLReader.TILES);
            //miltary = new FlxTilemap();
            //miltary.auto = FlxTilemap.STRING;
            //miltary.indexOffset = -1;
            //miltary.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_military"), 20, 20);
            //miltary.boundingBoxOverride = true;
            //miltary.setScrollFactors(0, 0);
            //miltary.visible = false;
            //add(miltary);

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/warehouse/bgwarehouse.tmx", "map", "bg", FlxXMLReader.TILES);
            warehouse = new FlxTilemap();
            warehouse.auto = FlxTilemap.STRING;
            warehouse.indexOffset = -1;
            warehouse.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_warehouse"), 20, 20);
            warehouse.boundingBoxOverride = true;
            warehouse.setScrollFactors(0, 0);
            warehouse.visible = false;
            add(warehouse);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/management/bgmanagement.tmx", "map", "bg", FlxXMLReader.TILES);
            management = new FlxTilemap();
            management.auto = FlxTilemap.STRING;
            management.indexOffset = -1;
            management.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_management"), 20, 20);
            management.boundingBoxOverride = true;
            management.setScrollFactors(0, 0);
            management.visible = false;
            add(management);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/factory/bgfactory.tmx", "map", "bg", FlxXMLReader.TILES);
            factory = new FlxTilemap();
            factory.auto = FlxTilemap.STRING;
            factory.indexOffset = -1;
            factory.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_factory"), 20, 20);
            factory.boundingBoxOverride = true;
            factory.setScrollFactors(0, 0);
            factory.visible = false;
            add(factory);


			//FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            int pwidth = 260;
            bubbleParticle = new FlxEmitter();
            bubbleParticle.delay = 3;
            bubbleParticle.setXSpeed(-150, 150);
            bubbleParticle.setYSpeed(-40, 100);
            bubbleParticle.setRotation(-720, 720);
            bubbleParticle.gravity = Lemonade_Globals.GRAVITY * -0.25f;
            bubbleParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/bubble"), 1500, true, 1, 1);
            bubbleParticle.x = FlxG.width / 2 - pwidth/2;
            bubbleParticle.y = 50;
            bubbleParticle.setSize(pwidth, 20);
            bubbleParticle.start(false, 0.0101f, 1500);
            add(bubbleParticle);



            location = new FlxText(0, 50, FlxG.width);
            location.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Center, Color.Black);
            location.text = "< - LOCATION - >";
            location.color = selected;
            add(location);

            

            levelText = new FlxText(0, 150, FlxG.width);
            levelText.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Center, Color.Black);
            levelText.text = "< -LEVEL #- >";
            location.color = notSelected;
            add(levelText);

            multiplayerText = new FlxText(0, 250, FlxG.width);
            multiplayerText.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Center, Color.Black);
            multiplayerText.text = "-multiplayer-";
            multiplayerText.color = notSelected;
            add(multiplayerText);
            multiplayerText.visible = false;


            int YPOS = 125;

            notDone = new Color(0.1f, 0.1f, 0.1f);
            done = Color.White;
            badge1 = new FlxSprite((FlxG.width / 2) - 150, YPOS);
            badge1.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge1.frame = 4;
            badge1.color = notDone;
            add(badge1);

            badge2 = new FlxSprite((FlxG.width / 2) - 50, YPOS);
            badge2.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge2.frame = 5;
            badge2.color = notDone;
            add(badge2);

            badge3 = new FlxSprite((FlxG.width / 2) + 50, YPOS);
            badge3.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge3.frame = 3;
            badge3.color = notDone;
            add(badge3);

            badge4 = new FlxSprite((FlxG.width / 2) + 150, YPOS);
            badge4.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge4.frame = 2;
            badge4.color = notDone;
            add(badge4);

            tweenBounce = new Tweener(2.0f, 3.0f, TimeSpan.FromSeconds(0.85f), Elastic.EaseOut);
            tweenBounce.PingPong = true;


            try
            {
				FlxG.username = FlxU.loadFromDevice("nameinfo.txt", true);
                Console.WriteLine("Your username is: {0}", FlxG.username);
 
            }
            catch
            {
                Console.WriteLine("Cannot load name from file");
            }

            if (FlxG.username != "")
            {
                //_nameEntry.text = FlxG.username;

                FlxG.setHudText(3, "Username: " + FlxG.username);
                FlxG.setHudTextPosition(3, 50, FlxG.height - 30);
                FlxG.setHudTextScale(3, 2);
            }


            //FlxCamera cam1 = new FlxCamera(0, 0, FlxG.width/2, FlxG.height, 1);
            //cam1.color = Color.Blue;
            //FlxG.cameras.Add(cam1);

            //FlxCamera cam2 = new FlxCamera(FlxG.width , 0, FlxG.width / 2, FlxG.height, 1);
            //cam2.color = Color.GreenYellow;
            //FlxG.cameras.Add(cam2);

            string prog = "";
            try
            {
				prog = FlxU.loadFromDevice("gameProgress.slf", true);

                Lemonade_Globals.gameProgress = new Dictionary<string, GameProgress>();

                string[] lev = prog.Split('\n');
                foreach (var item in lev)
                {
                    string[] leve = item.Split(',');

                    //

                    if (leve.Length > 1)
                    {
                        Lemonade_Globals.gameProgress.Add(leve[0],
                            new GameProgress(bool.Parse(leve[1]),
                            bool.Parse(leve[2]),
                            bool.Parse(leve[3]),
                            bool.Parse(leve[4]), 
                            bool.Parse(leve[5])));
                    }
                }

            }
            catch
            {
                // ------------ Save new;

                Console.WriteLine("Cannot load game progress");
                string newProgressString = "";

                foreach (var item in totalLocations)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        // army, chef, insp, worker, level complete.
                        newProgressString += item + "_" + i.ToString() + ",false,false,false,false,false\n";
                    }
                }

                FlxU.saveToDevice(newProgressString, "gameProgress.slf");

                // ---------------- End Save

                // Read new/
				prog = FlxU.loadFromDevice("gameProgress.slf", true);

                Lemonade_Globals.gameProgress = new Dictionary<string, GameProgress>();

                string[] lev = prog.Split('\n');
                foreach (var item in lev)
                {
                    string[] leve = item.Split(',');

                    //

                    if (leve.Length > 1)
                    {
                        Lemonade_Globals.gameProgress.Add(leve[0],
                            new GameProgress(bool.Parse(leve[1]),
                            bool.Parse(leve[2]),
                            bool.Parse(leve[3]),
                            bool.Parse(leve[4]),
                            bool.Parse(leve[5])));
                    }
                }

                // end read.
            }

            //if (Lemonade_Globals.gameProgress["newyork_12"].LevelComplete==true)
            //{
            //    possibleLocations.Add("warehouse");
            //}
            //if (Lemonade_Globals.gameProgress["sydney_12"].LevelComplete == true)
            //{
            //    possibleLocations.Add("factory");
            //}
            //if (Lemonade_Globals.gameProgress["military_12"].LevelComplete == true)
            //{
            //    possibleLocations.Add("management");
            //}

        }

        public FlxTilemap createMap(string MapFile, string Layer, string Tiles)
        {
            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile(MapFile, "map", Layer, FlxXMLReader.TILES);
            FlxTilemap ny = new FlxTilemap();
            ny.auto = FlxTilemap.STRING;
            ny.indexOffset = -1;
            ny.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_"+ Tiles), 20, 20);
            ny.boundingBoxOverride = true;
            ny.setScrollFactors(0, 0);
            ny.visible = false;
            return ny;
        }

//        public string LoadFromDevice()
//        {
//            string value1 = File.ReadAllText("nameinfo.txt");
//            return value1.Substring(0, value1.Length - 1);
//        }

        override public void update()
        {
            if (FlxG.debug && FlxGlobal.cheatString == "ol")
            {
                if (!possibleLocations.Contains("warehouse"))
                {
                    possibleLocations.Add("warehouse");
                    possibleLocations.Add("factory");
                    possibleLocations.Add("management");
                }
            }

            //FlxG.cameras[0].angle += 5;

			//currentLevel = 1;

            tweenBounce.Update(FlxG.elapsedAsGameTime);

//            foreach (var item in Lemonade_Globals.gameProgress)
//            {
//                Console.WriteLine("K.{0} V.{1} {2} {3} {4}", item.Key, item.Value.KilledArmy, item.Value.KilledChef, item.Value.KilledInspector, item.Value.KilledWorker);
//            }

            int incompleteScale = 2;

            if (Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + currentLevel.ToString()].KilledArmy == false)
            {
                badge1.scale = incompleteScale;
                badge1.color = notDone;
            }
            else
            {
                badge1.scale = tweenBounce.Position;
                badge1.color = done;
            }

            if (Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + currentLevel.ToString()].KilledChef == false)
            {
                badge2.scale = incompleteScale;
                badge2.color = notDone;
            }
            else
            {
                badge2.scale = tweenBounce.Position;
                badge2.color = done;
            }

            if (Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + currentLevel.ToString()].KilledInspector == false)
            {
                badge3.scale = incompleteScale;
                badge3.color = notDone;
            }
            else
            {
                badge3.scale = tweenBounce.Position;
                badge3.color = done;
            }

            if (Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + currentLevel.ToString()].KilledWorker == false)
            {
                badge4.scale = incompleteScale;
                badge4.color = notDone;
            }
            else
            {
                badge4.scale = tweenBounce.Position;
                badge4.color = done;
            }

            if (currentLevel != 1)
            {
                if (Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + (currentLevel - 1).ToString()].LevelComplete == false)
                {
                    locked = true;
                    lockedPhrase = "is Locked";
                }
                else
                {
                    locked = false;
                }
            }
            else
            {
                locked = false;
            }
            if (currentLevel > 2 && Lemonade_Globals.PAID_VERSION == Lemonade_Globals.DEMO_MODE)
            {
                locked = true;
                lockedPhrase = "is Locked. Buy the game to play it";
            }

            if (FlxControl.UPJUSTPRESSED) 
            { 
                currentSelected--; 
                bubbleParticle.start(false, 0.0101f, 1500);
                FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);

            }
            if (FlxControl.DOWNJUSTPRESSED) 
            { 
                currentSelected++; 
                bubbleParticle.start(false, 0.0101f, 1500);
                FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);

            }

            if (currentSelected <= -1) currentSelected = 1;
            if (currentSelected >= 2) currentSelected = 0;

            if (currentSelected == 0)
            {
                location.color = selected;
                levelText.color = notSelected;
                multiplayerText.color = notSelected;

                bubbleParticle.y = location.y;

				if (FlxControl.RIGHTJUSTPRESSED || FlxG.keys.justPressed(Keys.Right))
                {
                    currentLocation++; tweenBounce.Reset(); FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);
                }
				if (FlxControl.LEFTJUSTPRESSED || FlxG.keys.justPressed(Keys.Left))
                {
                    currentLocation--; tweenBounce.Reset(); FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);
                }
                if (currentLocation <= -1) currentLocation = possibleLocations.Count - 1;
                if (currentLocation >= possibleLocations.Count) currentLocation = 0;
            }
            if (currentSelected == 1)
            {
                levelText.color = selected;
                location.color = notSelected;
                multiplayerText.color = notSelected;

                bubbleParticle.y = levelText.y;

				if (FlxControl.RIGHTJUSTPRESSED|| FlxG.keys.justPressed(Keys.Right))
                {
                    currentLevel++; FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);
                }
				if (FlxControl.LEFTJUSTPRESSED|| FlxG.keys.justPressed(Keys.Left))
                {
                    currentLevel--; FlxG.play("Lemonade/sfx/Blip_Select", 0.7f, false);
                }
                if (currentLevel <= 0) currentLevel = 12;
                else if (currentLevel >= 13) currentLevel = 1;
            }
            if (currentSelected == 2)
            {
                levelText.color = notSelected;
                location.color = notSelected;
                multiplayerText.color = selected;

                bubbleParticle.y = multiplayerText.y;
            }



            Lemonade_Globals.location = possibleLocations[currentLocation];

            if (locked == false)
                levelText.text = "<- Level " + currentLevel.ToString() + " ->";
            else
            {
                levelText.text = "<- Level " + currentLevel.ToString() + " " + lockedPhrase +" ->";
            }

            if (Lemonade_Globals.location == "newyork")
            {
                location.text = "<- New York City ->";
                setAllTilemapsToOff();
                ny.visible = true;
                ny2.visible = true;
            }
            if (Lemonade_Globals.location == "sydney")
            {
                location.text = "<- Sydney, Australia ->";
                setAllTilemapsToOff();
                sydney.visible = true;
                sydney2.visible = true;
                sydney3.visible = true;


            }
            if (Lemonade_Globals.location == "military")
            {
                location.text = "<- Military Complex ->";
                setAllTilemapsToOff();
                military.visible = true;
                military2.visible = true;
                military3.visible = true;
            }
            if (Lemonade_Globals.location == "warehouse")
            {
                location.text = "<- Warehouse ->";
                setAllTilemapsToOff();
                warehouse.visible = true;
            }
            if (Lemonade_Globals.location == "factory")
            {
                location.text = "<- Factory ->";
                setAllTilemapsToOff();
                factory.visible = true;
            }
            if (Lemonade_Globals.location == "management")
            {
                location.text = "<- Management ->";
                setAllTilemapsToOff();
                management.visible = true;
            }

            if (FlxControl.ACTIONJUSTPRESSED && !FlxG._game._console.visible)
            {
                if (locked == false)
                {
                    FlxG.play("Lemonade/sfx/ping", 0.8f, false);

                    Lemonade_Globals.LAST_SELECTED_ON_MENU = currentSelected;
                    Lemonade_Globals.LAST_LOCATION = currentLocation;
                    FlxG.level = currentLevel;
                    Lemonade_Globals.restartMusic = true;
                    FlxG.state = new PlayState();
                }
                else
                {
                    FlxG.play("Lemonade/sfx/deathSFX", 0.7f, false);

                    FlxG.quake.start(0.005f, 0.5f);
                }

            }

            base.update();

            if (FlxG.username == "" || FlxG.username == null)
            {
				#if !__ANDROID__
                FlxG.state = new DataEntryState();
				#endif
            }
        }

        public void setAllTilemapsToOff()
        {
            ny.visible = false;
            ny2.visible = false;

            military.visible = false;
            military2.visible = false;
            military3.visible = false;

            sydney.visible = false;
            sydney2.visible = false;
            sydney3.visible = false;

            warehouse.visible = false;
            factory.visible = false;
            management.visible = false;
        }

        public void startMultiplayerGame()
        {
            FlxG.state = new MultiplayerMenuState();
            return;
        }



    }
}

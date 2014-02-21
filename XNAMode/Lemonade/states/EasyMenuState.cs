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
    public class EasyMenuState : FlxState
    {
        FlxEmitter bubbleParticle;
        FlxText location;
        FlxText levelText;
        FlxText multiplayerText;

        FlxTilemap ny;
        FlxTilemap miltary;
        FlxTilemap sydney;
        FlxTilemap management;
        FlxTilemap factory;
        FlxTilemap warehouse;

        FlxSprite badge1;
        FlxSprite badge2;
        FlxSprite badge3;
        FlxSprite badge4;

        Tweener tweenBounce;
        
        Color selected = new Color(1.0f, 0.1f, 0.1f);
        Color notSelected = new Color(1, 1, 1);

        int currentLocation;
        List<string> possibleLocations = new List<string>();

        int currentLevel = 1;

        int currentSelected;


        override public void create()
        {

            base.create();

            // play some music

            currentLocation = 0;
            currentSelected = 0;
            
            possibleLocations.Add("military");
            possibleLocations.Add("sydney");
            possibleLocations.Add("newyork");
            possibleLocations.Add("warehouse");
            possibleLocations.Add("factory");
            possibleLocations.Add("management");

            FlxG.playMp3("Lemonade/music/music_menu_1", 0.75f);

            // load some tile maps

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/newyork/bgnewyork.tmx", "map", "bg", FlxXMLReader.TILES);
            ny = new FlxTilemap();
            ny.auto = FlxTilemap.STRING;
            ny.indexOffset = -1;
            ny.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            ny.boundingBoxOverride = true;
            ny.setScrollFactors(0, 0);
            ny.visible = false;
            add(ny);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/sydney/bgsydney.tmx", "map", "bg", FlxXMLReader.TILES);
            sydney = new FlxTilemap();
            sydney.auto = FlxTilemap.STRING;
            sydney.indexOffset = -1;
            sydney.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_sydney"), 20, 20);
            sydney.boundingBoxOverride = true;
            sydney.setScrollFactors(0, 0);
            sydney.visible = false;
            add(sydney);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/military/bgmilitary.tmx", "map", "bg", FlxXMLReader.TILES);
            miltary = new FlxTilemap();
            miltary.auto = FlxTilemap.STRING;
            miltary.indexOffset = -1;
            miltary.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_military"), 20, 20);
            miltary.boundingBoxOverride = true;
            miltary.setScrollFactors(0, 0);
            miltary.visible = false;
            add(miltary);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/newyork/bgnewyork.tmx", "map", "bg", FlxXMLReader.TILES);
            warehouse = new FlxTilemap();
            warehouse.auto = FlxTilemap.STRING;
            warehouse.indexOffset = -1;
            warehouse.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            warehouse.boundingBoxOverride = true;
            warehouse.setScrollFactors(0, 0);
            warehouse.visible = false;
            add(warehouse);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/sydney/bgsydney.tmx", "map", "bg", FlxXMLReader.TILES);
            management = new FlxTilemap();
            management.auto = FlxTilemap.STRING;
            management.indexOffset = -1;
            management.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_sydney"), 20, 20);
            management.boundingBoxOverride = true;
            management.setScrollFactors(0, 0);
            management.visible = false;
            add(management);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/military/bgmilitary.tmx", "map", "bg", FlxXMLReader.TILES);
            factory = new FlxTilemap();
            factory.auto = FlxTilemap.STRING;
            factory.indexOffset = -1;
            factory.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_military"), 20, 20);
            factory.boundingBoxOverride = true;
            factory.setScrollFactors(0, 0);
            factory.visible = false;
            add(factory);


            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            
            bubbleParticle = new FlxEmitter();
            bubbleParticle.delay = 3;
            bubbleParticle.setXSpeed(-150, 150);
            bubbleParticle.setYSpeed(-40, 100);
            bubbleParticle.setRotation(-720, 720);
            bubbleParticle.gravity = Lemonade_Globals.GRAVITY * -0.25f;
            bubbleParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/bubble"), 1500, true, 1, 1);
            bubbleParticle.x = FlxG.width / 2 - 80;
            bubbleParticle.y = 50;
            bubbleParticle.setSize(160, 20);
            bubbleParticle.start(false, 0.055f, 1500);
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



            Color notDone = new Color(0.1f, 0.1f, 0.1f);
            badge1 = new FlxSprite((FlxG.width / 2) - 150, 330);
            badge1.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge1.frame = 2;
            badge1.color = notDone;
            add(badge1);

            badge2 = new FlxSprite((FlxG.width / 2) - 50, 330);
            badge2.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge2.frame = 3;
            badge2.color = notDone;
            add(badge2);

            badge3 = new FlxSprite((FlxG.width / 2) + 50, 330);
            badge3.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge3.frame = 4;
            badge3.color = notDone;
            add(badge3);

            badge4 = new FlxSprite((FlxG.width / 2) + 150, 330);
            badge4.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge4.frame = 5;
            badge4.color = notDone;
            add(badge4);

            tweenBounce = new Tweener(5.0f, 8.0f, TimeSpan.FromSeconds(1.12f), Elastic.EaseOut);
            tweenBounce.PingPong = true;


            try
            {
                FlxG.username = LoadFromDevice();
            }
            catch
            {
                Console.WriteLine("Cannot load name from file");
            }

            if (FlxG.username != "")
            {
                //_nameEntry.text = FlxG.username;
                FlxG.setHudText(3, "Username:\n" + FlxG.username);
                FlxG.setHudTextPosition(3, 50, FlxG.height - 30);
                FlxG.setHudTextScale(3, 2);
            }

        }

        public string LoadFromDevice()
        {
            string value1 = File.ReadAllText("nameinfo.txt");
            return value1.Substring(0, value1.Length - 1);
        }

        override public void update()
        {
            tweenBounce.Update(FlxG.elapsedAsGameTime);

            badge1.scale = tweenBounce.Position;
            badge2.scale = tweenBounce.Position;
            badge3.scale = tweenBounce.Position;
            badge4.scale = tweenBounce.Position;

            if (FlxControl.UPJUSTPRESSED) { currentSelected--; bubbleParticle.start(false, 0.0101f, 1500); }
            if (FlxControl.DOWNJUSTPRESSED) { currentSelected++; bubbleParticle.start(false, 0.0101f, 1500); }
            if (currentSelected <= -1) currentSelected = 3;
            if (currentSelected >= 3) currentSelected = 0;



            if (currentSelected == 0)
            {
                location.color = selected;
                levelText.color = notSelected;
                multiplayerText.color = notSelected;

                bubbleParticle.y = location.y;

                if (FlxControl.RIGHTJUSTPRESSED) { currentLocation++; }
                if (FlxControl.LEFTJUSTPRESSED) { currentLocation--; }
                if (currentLocation <= -1) currentLocation = possibleLocations.Count - 1;
                if (currentLocation >= possibleLocations.Count) currentLocation = 0;
            }
            if (currentSelected == 1)
            {
                levelText.color = selected;
                location.color = notSelected;
                multiplayerText.color = notSelected;

                bubbleParticle.y = levelText.y;

                if (FlxControl.RIGHTJUSTPRESSED) { currentLevel++; }
                if (FlxControl.LEFTJUSTPRESSED) { currentLevel--; }
                if (currentLevel <= 0) currentLevel = 13;
                if (currentLevel >= 13) currentLevel = 1;
            }
            if (currentSelected == 2)
            {
                levelText.color = notSelected;
                location.color = notSelected;
                multiplayerText.color = selected;

                bubbleParticle.y = multiplayerText.y;
            }

            

            Lemonade_Globals.location = possibleLocations[currentLocation];
            levelText.text = "<- Level " + currentLevel.ToString() + " ->";

            if (Lemonade_Globals.location == "newyork")
            {
                location.text = "<- New York City ->";
                setAllTilemapsToOff();
                ny.visible = true;
            }
            if (Lemonade_Globals.location == "sydney")
            {
                location.text = "<- Sydney, Australia ->";
                setAllTilemapsToOff();
                sydney.visible = true;
            }
            if (Lemonade_Globals.location == "military")
            {
                location.text = "<- Military Complex ->";
                setAllTilemapsToOff();
                miltary.visible = true;
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

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                FlxG.level = currentLevel;
                FlxG.state = new PlayState();


            }

            base.update();

            if (FlxG.username == "" || FlxG.username == null)
            {
                FlxG.state = new DataEntryState();
            }
        }

        public void setAllTilemapsToOff()
        {
            ny.visible = false;
            miltary.visible = false;
            sydney.visible = false;

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

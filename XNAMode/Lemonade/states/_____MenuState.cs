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
    public class MenuState : FlxMenuState
    {

        FlxText location;

        FlxTilemap ny;
        FlxTilemap miltary;
        FlxTilemap sydney;

        FlxSprite badge1;
        FlxSprite badge2;
        FlxSprite badge3;
        FlxSprite badge4;

        Tweener tweenBounce;

        override public void create()
        {

            base.create();

            // play some music

            

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

			//FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            FlxButton b = new FlxButton((FlxG.width / 2) - 100, 150, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "Sydney"), new FlxText(-20, 10, 100, "Sydney!"));
            buttons.add(b);

            b = new FlxButton(FlxG.width / 2, 150, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "New York"), new FlxText(-20, 10, 100, "New York!"));
            buttons.add(b);

            b = new FlxButton((FlxG.width / 2) + 100, 150, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "Military"), new FlxText(-20, 10, 100, "Military!"));
            buttons.add(b);


            for (int i = 1; i < 13; i++)
            {
                FlxButton a = new FlxButton(175+(i*45), 225, startGame);
                a.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
                a.loadText(new FlxText(-40, 10, 100, i.ToString()), new FlxText(-40, 10, 100, i.ToString()+"!"));
                
                buttons.add(a);

            }

            b = new FlxButton((FlxG.width / 2) - 100, 275, startMultiplayerGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "Multiplayer"), new FlxText(-20, 10, 100, "Multiplayer!"));
            buttons.add(b);


            location = new FlxText(0, 50, FlxG.width);
            location.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Center, Color.Black);
            location.text = "";
            add(location);


            addButtons();

            FlxText badges = new FlxText(0, 330, 0, "Badges");
            badges.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Left, Color.Black);
            add(badges);

            Color notDone = new Color(0.1f, 0.1f, 0.1f);
            

            badge1 = new FlxSprite((FlxG.width/2) - 150, 330);
            badge1.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge1.frame = 2;
            badge1.color = notDone;
            add(badge1);

            badge2 = new FlxSprite((FlxG.width/2) - 50, 330);
            badge2.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge2.frame = 3;
            badge2.color = notDone;
            add(badge2);

            badge3 = new FlxSprite((FlxG.width/2) + 50, 330);
            badge3.loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/offscreenIcons"), true, false, 12, 12);
            badge3.frame = 4;
            badge3.color = notDone;
            add(badge3);

            badge4 = new FlxSprite((FlxG.width/2) + 150, 330);
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
                FlxG.setHudText(3, "Username:\n"+FlxG.username);
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

            if (FlxG.keys.justPressed(Keys.Enter))
            {
                startGame();
            }

            if (Lemonade_Globals.location == "newyork")
            {
                location.text = "New York City";
                ny.visible = true;
                miltary.visible = false;
                sydney.visible = false;
            }
            if (Lemonade_Globals.location == "sydney")
            {
                location.text = "Sydney, Australia";
                ny.visible = false;
                miltary.visible = false;
                sydney.visible = true;
            }
            if (Lemonade_Globals.location == "military")
            {
                location.text = "Military Complex";
                ny.visible = false;
                miltary.visible = true;
                sydney.visible = false;
            }

            base.update();

            if (FlxG.username == "" || FlxG.username==null)
            {
                FlxG.state = new DataEntryState();
            }
        }

        public void startMultiplayerGame()
        {
            FlxG.state = new MultiplayerMenuState();
            return;
        }

        public void startGame()
        {
            int sel = getCurrentSelected()[0];

            if (getCurrentSelected()[0] == 0)
            {
                Lemonade_Globals.location = "sydney";
            }
            else if (getCurrentSelected()[0] == 1)
            {
                Lemonade_Globals.location = "newyork";
            }
            else if (getCurrentSelected()[0] == 2)
            {
                Lemonade_Globals.location = "military";
            }
            else
            {
                FlxG.level = sel - 2;

                FlxG.state = new PlayState();
                return;
            }


        }

    }
}

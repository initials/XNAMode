using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Lemonade
{
    public class MenuState : FlxMenuState
    {

        FlxText location;

        FlxTilemap ny;
        FlxTilemap miltary;
        FlxTilemap sydney;

        override public void create()
        {

            base.create();

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/bgnewyork.tmx", "map", "bg", FlxXMLReader.TILES);
            ny = new FlxTilemap();
            ny.auto = FlxTilemap.STRING;
            ny.indexOffset = -1;
            ny.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            ny.boundingBoxOverride = true;
            ny.setScrollFactors(0, 0);
            ny.visible = false;
            add(ny);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/bgsydney.tmx", "map", "bg", FlxXMLReader.TILES);
            sydney = new FlxTilemap();
            sydney.auto = FlxTilemap.STRING;
            sydney.indexOffset = -1;
            sydney.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_sydney"), 20, 20);
            sydney.boundingBoxOverride = true;
            sydney.setScrollFactors(0, 0);
            sydney.visible = false;
            add(sydney);

            bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/bgmilitary.tmx", "map", "bg", FlxXMLReader.TILES);
            miltary = new FlxTilemap();
            miltary.auto = FlxTilemap.STRING;
            miltary.indexOffset = -1;
            miltary.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_military"), 20, 20);
            miltary.boundingBoxOverride = true;
            miltary.setScrollFactors(0, 0);
            miltary.visible = false;
            add(miltary);

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            FlxButton b = new FlxButton(100, 50, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "Sydney"), new FlxText(-20, 10, 100, "Sydney!"));
            buttons.add(b);

            b = new FlxButton(200, 50, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "New York"), new FlxText(-20, 10, 100, "New York!"));
            buttons.add(b);

            b = new FlxButton(300, 50, startGame);
            b.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
            b.loadText(new FlxText(-20, 10, 100, "Military"), new FlxText(-20, 10, 100, "Military!"));
            buttons.add(b);


            for (int i = 1; i < 13; i++)
            {
                FlxButton a = new FlxButton(100+(i*45), 100, startGame);
                a.loadGraphic(new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/button_ny")), new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/buttonPressed_ny")));
                a.loadText(new FlxText(-20, 10, 100, i.ToString()), new FlxText(-20, 10, 100, i.ToString()+"!"));
                buttons.add(a);
            }


            location = new FlxText(0, 50, FlxG.width);
            location.setFormat(FlxG.Content.Load<SpriteFont>("Lemonade/SMALL_PIXEL"), 3, Color.White, FlxJustification.Center, Color.Black);
            location.text = "";
            add(location);


            addButtons();

        }

        override public void update()
        {

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

                FlxG.state = new LemonadeTestState();
                return;
            }


        }

    }
}

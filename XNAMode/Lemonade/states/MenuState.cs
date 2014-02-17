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

        override public void create()
        {

            base.create();

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




            addButtons();

        }

        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.Enter))
            {
                startGame();
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

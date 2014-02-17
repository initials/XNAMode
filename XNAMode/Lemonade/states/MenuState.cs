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




            base.update();
        }

        public void startGame()
        {

        }

    }
}

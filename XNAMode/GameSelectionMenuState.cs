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
    public class GameSelectionMenuState : FlxState
    {

        FlxText _menuItems;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            _menuItems = new FlxText(10, 30, FlxG.width);

            _menuItems.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _menuItems.text = "1. Mode\n2. Hawksnest";

            add(_menuItems);




        }

        override public void update()
        {

            if (FlxG.keys.ONE)
            {
                FlxG.state = new MenuState();
            }
            if (FlxG.keys.TWO)
            {
                FlxG.state = new CaveState();

            }


            base.update();
        }


    }
}

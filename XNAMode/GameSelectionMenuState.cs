using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;
using Microsoft.Xna.Framework.GamerServices;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class GameSelectionMenuState : FlxState
    {

        FlxText _menuItems;

        FlxText _nameEntry;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            _menuItems = new FlxText(10, 30, FlxG.width);

            _menuItems.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _menuItems.text = "1. Mode\n2. Hawksnest\n\nEnter name, use @ symbol to specify Twitter handle.";

            add(_menuItems);




            _nameEntry = new FlxText(10, 200, FlxG.width);

            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _nameEntry.text = "";

            add(_nameEntry);


        }

        override public void update()
        {

            if (FlxG.keys.F1)
            {
                FlxG.state = new MenuState();

            }
            if (FlxG.keys.F2)
            {
                FlxG.state = new CaveState();

            }

            _nameEntry.text = FlxG.keys.trackingString;


            base.update();
        }


    }
}

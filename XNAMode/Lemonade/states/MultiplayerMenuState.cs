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
    public class MultiplayerMenuState : FlxMenuState
    {

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));


        }

        override public void update()
        {


            Lemonade_Globals.location = "";

            base.update();

            if (FlxG.keys.ESCAPE || FlxG.gamepads.isButtonDown(Buttons.Back))
            {
                FlxG.state = new MenuState();
            }

        }


    }
}

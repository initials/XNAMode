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
    public class EmptyIntroTestState : FlxState
    {

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));


            Color xc = FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/envir_dusk"), 5, 5);

            Console.WriteLine(xc);

        }

        override public void update()
        {




            base.update();
        }


    }
}

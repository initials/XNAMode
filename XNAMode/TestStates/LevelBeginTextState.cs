using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class LevelBeginTextState : FlxState
    {
        //float counter;
        FlxText t;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            t = new LevelBeginText(0, (FlxG.height / 2) - 40, FlxG.width);
            t.text = "START OF LEVELS";
            add(t);



        }

        override public void update()
        {
            



            base.update();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class LevelBeginText : FlxText
    {
        float counter;
        public LevelBeginText(int xPos, int yPos, int Width)
            : base(xPos, yPos, Width)
        {
            counter = 0;




        }

        override public void update()
        {
            if (counter < 5.0f)
                counter += FlxG.elapsed;

            if (counter > 2.5f) x += 5;

            base.update();

        }


    }
}

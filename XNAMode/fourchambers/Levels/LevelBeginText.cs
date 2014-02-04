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
        public float counter;
        public string style = "right";
        public float limit = 2.5f;
        public string flyAwayText = "";
        public int flyAwayScore = 0;

        public LevelBeginText(int xPos, int yPos, int Width)
            : base(xPos, yPos, Width)
        {
            counter = 0;




        }

        override public void update()
        {
            if (counter < 5.0f)
                counter += FlxG.elapsed;


            if (counter > limit)
            {
                if (style=="right")
                    x += 5;
                if (style == "up")
                {
                    text = flyAwayText;
                    y -= 3;
                }
            }
            base.update();

        }


    }
}

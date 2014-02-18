using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Lemonade
{
    class Ramp : FlxSprite
    {

        public Ramp(int xPos, int yPos)
            : base(xPos, yPos)
        {
            createGraphic(20, 20, Color.DarkRed);

            @fixed = true;
            solid = true;
        }

        override public void update()
        {


            base.update();

        }


    }
}

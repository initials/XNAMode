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
    class Ramp : FlxRamp
    {

        public Ramp(int xPos, int yPos, int Direction)
            : base(xPos, yPos, Direction)
        {
            //createGraphic(20, 20, Color.DarkRed);
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), true, false, 20, 20);

            if (direction == FlxRamp.LOW_SIDE_LEFT) frame = 340;
            else if (direction == FlxRamp.LOW_SIDE_RIGHT) frame = 341;


        }

        override public void update()
        {


            base.update();

        }


    }
}

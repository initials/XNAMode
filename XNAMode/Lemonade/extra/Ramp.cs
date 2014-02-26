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
    /// <summary>
    /// RAMP is not yet implemented.
    /// </summary>
    class Ramp : FlxRamp
    {

        public Ramp(int xPos, int yPos, int Direction)
            : base(xPos, yPos, Direction)
        {
            //createGraphic(20, 20, Color.DarkRed);
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), true, false, 20, 20);

            width = 30;
            height = 30;

            //x -= 5;
            

            if (direction == FlxRamp.LOW_SIDE_LEFT)
            {
                frame = 340;
                y -= 2;
                setOffset(0, -2);
                

            }
            else if (direction == FlxRamp.LOW_SIDE_RIGHT)
            {
                frame = 341;
                y -= 2;
                x -= 10;
                setOffset(-10, -2);
            }

        }

        override public void update()
        {
            base.update();
        }
    }
}

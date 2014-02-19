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
    class MovingPlatform : FlxMovingPlatform
    {

        public MovingPlatform(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), true, false, 80, 20);
            
            frame = 14;

            

        }

        override public void update()
        {


            base.update();

        }


    }
}

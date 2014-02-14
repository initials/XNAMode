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
    class Actor : FlxPlatformActor
    {

        public Actor(int xPos, int yPos)
            : base(xPos, yPos)
        {

            acceleration.Y = FourChambers_Globals.GRAVITY;


            play("idle");

        }

        override public void update()
        {
            base.update();
        }

        virtual public void overlapped(FlxObject obj)
        {

        }
    }
}

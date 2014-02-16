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
        public float trampolineTimer;
        private const float trampolineMaxLimit = 0.464f;

        public Actor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            trampolineTimer = float.MaxValue;

            play("idle");

        }

        override public void update()
        {
            trampolineTimer += FlxG.elapsed;

            if (trampolineTimer < trampolineMaxLimit)
            {
                acceleration.Y = Lemonade_Globals.GRAVITY *-1;
            }
            else
            {
                acceleration.Y = Lemonade_Globals.GRAVITY;
            }

            base.update();
        }

        virtual public void overlapped(FlxObject obj)
        {

        }
    }
}

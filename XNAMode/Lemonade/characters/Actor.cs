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
        public float trampolineTimer = 200000;
        protected const float trampolineMaxLimit = 0.164f;
        public float dashTimer = 200000;
        protected const float dashMaxLimit = 0.075f;


        public Actor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            trampolineTimer = float.MaxValue;

            play("idle");

        }

        override public void update()
        {
            trampolineTimer += FlxG.elapsed;
            dashTimer += FlxG.elapsed;

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

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            base.hitBottom(Contact, Velocity);
        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);
            if (obj.GetType().ToString() == "Lemonade.Trampoline")
            {
                velocity.Y = -1000;
                trampolineTimer = 0.0f;
            }
            else if (obj.GetType().ToString() == "Lemonade.Ramp")
            {
                float delta = x % 20;

                //FlxU.solveXCollision(obj, null);

            }
        }

        public override void kill()
        {
            control = Controls.none;
            dead = true;
            //base.kill();
        }


    }
}

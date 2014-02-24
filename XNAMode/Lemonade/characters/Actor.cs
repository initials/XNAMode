﻿using System;
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
        public bool piggyBacking;
        public float trampolineTimer = 200000;
        protected const float trampolineMaxLimit = 0.164f;
        public float dashTimer = 200000;
        protected const float dashMaxLimit = 0.075f;


        public Actor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            trampolineTimer = float.MaxValue;

            play("idle");

            piggyBacking = false;

        }

        override public void update()
        {

            if (piggyBacking == true)
            {
                animationPrefix = "piggyback_";
            }
            else
            {
                animationPrefix = "";
            }

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

            if (x < 0) x = FlxG.levelWidth;
            if (x > FlxG.levelWidth) x = 10;
            //if (y < 0) y = FlxG.levelHeight;
            if (y > FlxG.levelHeight) y = 0;



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

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
    class FlyingEnemy : BaseActor
    {

        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        //public int score;

        protected float chanceOfWingFlap = 0.023f;

        protected float speedOfWingFlapVelocity = -40;

        
        public FlyingEnemy(int xPos, int yPos)
            : base(xPos, yPos)
        {
            originalPosition.X = xPos;
            originalPosition.Y = yPos;

            int runSpeed = 30;
            acceleration.Y = 50;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;
            velocity.X = 100;

        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {

            if (path == null)
            {
                if (dead)
                {
                    timeDead += FlxG.elapsed;
                    acceleration.Y = FourChambers_Globals.GRAVITY;
                }
                else
                {
                    timeDead = 0;
                    acceleration.Y = 50;
                }
                //if (timeDead > 2)
                //{
                //    flicker(1.0f);
                //}
                //if (timeDead > 3)
                //{
                //    reset(originalPosition.X, originalPosition.Y);
                //    dead = false;
                //    angle = 0;
                //    flicker(-0.001f);
                //    angularVelocity = 0;
                //    angularDrag = 700;
                //    drag.X = 0;
                //    timeDead = 0;
                //    play("fly");
                //    velocity.X = 100;
                //}

                if (dead == false)
                {
                    if (FlxU.random() < chanceOfWingFlap)
                    {
                        velocity.Y = speedOfWingFlapVelocity;
                    }
                }
            }

            base.update();

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;
            }

            if (x > FlxG.levelWidth+20) x = 1;
            if (x < -20) x = FlxG.levelWidth - 1;

        }
        public override void kill()
        {
            play("death");
            dead = true;
            angularVelocity = 500;
            angularDrag = 700;
            drag.X = 1000;
            acceleration.Y = FourChambers_Globals.GRAVITY;

            FlxG.score += score * FourChambers_Globals.arrowCombo;

            //base.kill();
        }

    }
}

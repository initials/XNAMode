using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class Bat : FlxSprite
    {

        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        public int score;


        public Bat(int xPos, int yPos)
            : base(xPos, yPos)
        {
            score = 100;

            //actorName = "Bat";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/batParticles_12x12"), true, false, 12, 12);

            addAnimation("fly", new int[] { 0, 1, 2 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 2, 4 }, 18);
            addAnimation("death", new int[] { 1 }, 18);

            //bounding box tweaks
            width = 10;
            height = 9;
            offset.X = 1;
            offset.Y = 3;

            //basic player physics
            int runSpeed = 30;
            //drag.X = runSpeed * 4;
            acceleration.Y = 50;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            //jumpPower = -140;

            velocity.X = 100;

            play("fly");

            health = 0;


        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {
            if (dead) timeDead += FlxG.elapsed;
            else timeDead = 0;
            if (timeDead > 2)
            {
                flicker(1.0f);
            }
            if (timeDead > 3)
            {
                reset(originalPosition.X, originalPosition.Y);
            }

            if (FlxU.random() < 0.023 && dead==false) velocity.Y = -40;
            base.update();

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;
            }

            if (x > 50*16) x = 0;
            if (x < 0) x = 50 * 16;
        }
        public override void kill()
        {
            play("death");
            //velocity.X = 0;
            //velocity.Y = 0;
            dead = true;
            angularVelocity = 500;
            angularDrag = 700;
            drag.X = 1000;


            FlxG.score += score;

            //base.kill();
        }

    }
}

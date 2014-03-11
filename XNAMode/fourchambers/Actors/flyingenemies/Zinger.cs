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
    class Zinger : BaseActor
    {
        protected float chanceOfWingFlap = 0.023f;

        protected float speedOfWingFlapVelocity = -40;

        public Zinger(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Zinger";
            health = 1;
            score = 100;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Zinger_ss_12x14"), true, false, 12, 14);

            addAnimation("fly", new int[] { 0, 1 }, 30);
            addAnimation("death", new int[] { 2,3,4 }, 8, false);
            play("fly");

            //bounding box tweaks
            width = 10;
            height = 10;
            offset.X = 1;
            offset.Y = 4;

            chanceOfWingFlap += FlxU.random(0.005, 0.009);
            speedOfWingFlapVelocity += FlxU.random(0,3);

            originalPosition.X = xPos;
            originalPosition.Y = yPos;

            int runSpeed = 30;
            acceleration.Y = 50;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;
            velocity.X = 100;

        }

        override public void update()
        {

            if (dead == false)
            {
                if (FlxU.random() < chanceOfWingFlap)
                {
                    velocity.Y = speedOfWingFlapVelocity;
                }
            }
            //else
            //{
            //    acceleration.Y = FourChambers_Globals.GRAVITY;
            //    velocity.X = 0;

            //}

            base.update();

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;
            }

            if (x > FlxG.levelWidth) x = 1;
            if (x < 0) x = FlxG.levelWidth - 1;

        }
        public override void kill()
        {
            FlxG.score += score * FourChambers_Globals.arrowCombo;

            play("death");
            dead = true;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            velocity.X = 0;

            //base.kill();
        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
    }
}

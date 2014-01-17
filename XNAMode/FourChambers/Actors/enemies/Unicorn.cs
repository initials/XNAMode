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
    class Unicorn : EnemyActor
    {

        public Unicorn(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Fabulous Diamond Joe";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/unicorn_ss_20x25"), true, false, 20, 25);

            addAnimation("walk", new int[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 10);
            addAnimation("run", new int[] {2, 3, 4, 5, 6, 7,8,9 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0 }, 12);
            addAnimation("death", new int[] { 10, 11, 12, 13, 14, 14, 14, 14, 14, 14, 13, 14, 11, 12, 13, 14, 14, 14, 14, 14, 14, 13, 14, 15, 15, 14, 14, 15, 15, 14, 14, 15, 15, 14, 14, 15, 14, 15, 14, 15, 14, 15, 14, 15, 16 }, 12, false);
            addAnimation("hurt", new int[] { 1,1,1,1 }, 22);
            addAnimationCallback(finishedHurt);

            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 5;
            offset.Y = 5;

            //basic player physics
            int runSpeed = 120;
            drag.X = 0;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;
            velocity.X = 32;
            health = 5;
            
        }

        public void finishedHurt(string Name, uint Frame, int FrameIndex) 
        {
            //Console.WriteLine("Callback {0} {1} {2}", Name, Frame, FrameIndex);
            

            if (Name == "hurt" && Frame == 3)
            {
                hurtTimer += 55.0f;
                //Console.WriteLine("Callback {0} {1} {2}", Name, Frame, FrameIndex);
                color = Color.White;

            }
        }

        override public void update()
        {
            base.update();
        }

        public override void hurt(float Damage)
        {
            play("hurt");

            hurtTimer = 0;

            if (facing == Flx2DFacing.Right)
                velocity.X = 3300;
            else if (facing == Flx2DFacing.Left)
                velocity.X = -3300;
            else velocity.X = -3300;

            color = Color.PaleVioletRed;

            base.hurt(Damage);
        }
    }
}

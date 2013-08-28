using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using org.flixel;

namespace XNAMode
{
    /// <summary>
    /// 
    /// </summary>
    public class Arrow : FlxSprite
    {
        private Texture2D ImgBullet;

        private bool hasTouched;

        protected const string SndShoot = "sfx/arrowShoot";

        public Arrow()
        {
            ImgBullet = FlxG.Content.Load<Texture2D>("initials/arrow_8x1");

            loadGraphic(ImgBullet, false, false, 8,1);

            hasTouched = false;


            width = 8;
            height = 1;
            offset.X = 0;
            offset.Y = 0;
            exists = false;

            addAnimation("explode", new int[] { 0, 1 }, 10, false);
            addAnimation("normal", new int[] { 0 }, 0, false);

            play("normal");

            drag.X = 200;
            acceleration.Y = 820;
            maxVelocity.X = 1000;
            maxVelocity.Y = 1000;

        }

        override public void update()
        {
            if (hasTouched == false)
            {
                if (velocity.X > 0)
                {
                    double rot = Math.Atan2((float)velocity.Y, (float)velocity.X);
                    double degrees = rot * 180 / Math.PI;

                    angle = (float)degrees;
                }
                // reversing not working.
                else
                {
                    double rot = Math.Atan2((float)velocity.Y, (float)velocity.X);
                    double degrees = rot * 180 / Math.PI;

                    angle = (float)degrees;
                }
            }

            if (dead && finished) exists = false;
            else base.update();
        }

        override public void hitSide(FlxObject Contact, float Velocity) 
        { 
            hasTouched= true;
            kill(); 
        }
        override public void hitBottom(FlxObject Contact, float Velocity) 
        {
            hasTouched = true;
            dead = true;
            solid = false;
            kill(); 
        }
        override public void hitTop(FlxObject Contact, float Velocity) 
        {
            hasTouched = true;
            kill(); 
        }
        override public void kill()
        {
            visible = false;

            if (dead) return;
            velocity.X = 0;
            velocity.Y = 0;
            //if (onScreen()) FlxG.play(SndHit);
            
            play("explode");

            
        }

        public void shoot(int X, int Y, int VelocityX, int VelocityY)
        {
            visible = true;
            FlxG.play(SndShoot, 0.05f, false);

            play("normal");
            //FlxG.play(SndShoot);
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;
            hasTouched = false;
        }

    }
}

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

        public Arrow()
        {
            ImgBullet = FlxG.Content.Load<Texture2D>("initials/arrow_8x1");

            loadGraphic(ImgBullet, false, false, 8,1);

            width = 8;
            height = 1;
            offset.X = 0;
            offset.Y = 0;
            exists = false;

            addAnimation("explode", new int[] { 0, 1, 2 }, 12, false);
            addAnimation("normal", new int[] { 0 }, 0, false);

            play("normal");

            drag.X = 30;
            acceleration.Y = 820;
            maxVelocity.X = 1000;
            maxVelocity.Y = 1000;

        }

        override public void update()
        {
            if (dead && finished) exists = false;
            else base.update();
        }

        override public void hitSide(FlxObject Contact, float Velocity) { kill(); }
        override public void hitBottom(FlxObject Contact, float Velocity) { kill(); }
        override public void hitTop(FlxObject Contact, float Velocity) { kill(); }
        override public void kill()
        {
            if (dead) return;
            velocity.X = 0;
            velocity.Y = 0;
            //if (onScreen()) FlxG.play(SndHit);
            dead = true;
            solid = false;
            play("explode");
        }

        public void shoot(int X, int Y, int VelocityX, int VelocityY)
        {
            play("normal");
            //FlxG.play(SndShoot);
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;

        }

    }
}

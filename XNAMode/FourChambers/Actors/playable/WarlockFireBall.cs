using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using org.flixel;

namespace FourChambers
{
    /// <summary>
    /// 
    /// </summary>
    public class WarlockFireBall : FlxSprite
    {
        private Texture2D ImgBullet;

        public WarlockFireBall()
        {
            ImgBullet = FlxG.Content.Load<Texture2D>("fourchambers/warlock_fireball");

            loadGraphic(ImgBullet, false,false,6,9);
            width = 6;
            height = 9;
            offset.X = 0;
            offset.Y = 0;
            exists = false;

            addAnimation("explode", new int[] { 0,1,2 }, 12, false);
            addAnimation("normal", new int[] { 0 }, 0, false);

            play("normal");
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
            //FlxG.play(SndShoot);
            play("normal");
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;

        }

    }
}

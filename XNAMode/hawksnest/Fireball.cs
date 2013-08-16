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
    public class Fireball : FlxSprite
    {
        private Texture2D ImgBullet;

        public Fireball()
        {
            ImgBullet = FlxG.Content.Load<Texture2D>("initials/warlock_fireball");

            loadGraphic(ImgBullet, false,false,6,9);
            width = 6;
            height = 9;
            offset.X = 0;
            offset.Y = 0;
            exists = false;

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
            //play("poof");
        }

        public void shoot(int X, int Y, int VelocityX, int VelocityY)
        {
            //FlxG.play(SndShoot);
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;

        }

    }
}

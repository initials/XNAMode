using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using org.flixel;

namespace Revvolvver
{
    /// <summary>
    /// The bullet for the Player.
    /// </summary>
    public class BulletMulti : FlxSprite
    {

        private Texture2D ImgBullet;

        public bool exploding = false;
        public int tileOffsetX = 0;
        public int tileOffsetY = 0;

        public string firedFromPlayer;
        public int bulletNumber;



		public BulletMulti()
		{
            ImgBullet= FlxG.Content.Load<Texture2D>("Revvolvver/bullet");

			loadGraphic(ImgBullet,true);
			width = 6;
			height = 6;
			offset.X = 1;
			offset.Y = 1;
			exists = false;
			
			addAnimation("up", new int[] {0});
			addAnimation("down", new int[] {1});
			addAnimation("left",new int[] {2});
			addAnimation("right",new int[] {3});
			addAnimation("explode", new int[] {4, 5, 6, 7}, 50, false);
		}
		
		override public void update()
		{
            if (exploding && _caf == 7)
            {
                x = -1000;
                y = -1000;
                exploding = false;
            }
			if(dead && finished) exists = false;
			else base.update();

            if (_curAnim.name == "explode") exploding = true;
            else exploding = false;
		}
		
		//override public void hitSide(FlxObject Contact, float Velocity) { kill(); }


        override public void hitRight(FlxObject Contact, float Velocity)
        {
            x = ((int)(x / 21)) * 21;
            y = ((int)(y / 21)) * 21;

            tileOffsetX = 35;
            tileOffsetY = 0;

            //Console.WriteLine("hitRight");
            kill();
            //base.hitRight(Contact, Velocity);
            //x += 4;
            
        }

        public override void hitLeft(FlxObject Contact, float Velocity)
        {
            tileOffsetX = -11;
            tileOffsetY = 0;

            x = ((int)(x / 21)) * 21;
            y = ((int)(y / 21)) * 21;

            //Console.WriteLine("hitLeft");
            kill();
            //base.hitLeft(Contact, Velocity);
            //x -= 4;
            

        }

		override public void hitBottom(FlxObject Contact, float Velocity) 
        {
            tileOffsetX = 0;
            tileOffsetY = 18;

            //x = ((int)(x / 8)) * 8;
            //y = ((int)(y / 8)) * 8;



            //Console.WriteLine("hitBottom {0}", x % 8);
            kill();
            
        }
		override public void hitTop(FlxObject Contact, float Velocity) 
        {

            //x = ((int)(x / 8)) * 8;
            //y = ((int)(y / 8)) * 8;

            tileOffsetX = 0;
            tileOffsetY = -18;

            //Console.WriteLine("hitTop {0}", x % 8);
            kill(); 
        }

		override public void kill()
		{
			if(dead) return;
			velocity.X = 0;
			velocity.Y = 0;
			//if(onScreen()) FlxG.play(SndHit);
			dead = true;
			solid = false;
			play("explode");
		}
		
		public void shoot(int X, int Y, int VelocityX, int VelocityY, Color Colorx)
		{
            color = Colorx;

			//FlxG.play(SndShoot);
			base.reset(X,Y);
			solid = true;
			velocity.X = VelocityX;
			velocity.Y = VelocityY;
			if(velocity.Y < 0)
				play("up");
			else if(velocity.Y > 0)
				play("down");
			else if(velocity.X < 0)
				play("left");
			else if(velocity.X > 0)
				play("right");
		}

    }
}

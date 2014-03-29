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
    public class Arrow : FlxSprite
    {
        private Texture2D ImgBullet;

        private bool hasTouched;

        protected const string SndShoot = "fourchambers/sfx/arrowShoot1";
        protected const string SndHit = "fourchambers/sfx/arrowHit1";
        public FlxSprite _ex;

        public bool explodesOnImpact = false;

        protected FlxEmitter _fire;

        public Arrow(int xPos, int yPos, FlxSprite exp)
            : base(xPos, yPos)
        {


            _ex = exp;

            ImgBullet = FlxG.Content.Load<Texture2D>("fourchambers/arrow_8x1");

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

            
            _fire = new FlxEmitter();
            _fire.setSize(1, 1);
            _fire.setRotation();
            _fire.setXSpeed(-15, 15);
            _fire.setYSpeed(-15, 15);
            _fire.gravity = 0;
            _fire.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/arrowSparkles"), 25, true);

            damage = 5;


        }

        override public void render(SpriteBatch spriteBatch)
        {
            _fire.render(spriteBatch);
            base.render(spriteBatch);
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
            else
            {
                _fire.at(this);
                _fire.update();
                base.update();
            }
        }

        override public void hitSide(FlxObject Contact, float Velocity) 
        {
            _fire.setXSpeed(50, 100);
            _fire.setYSpeed(50, 100);
            _fire.start(true, 0.01f, 0);
            if (explodesOnImpact)
            {
                _ex.x = x - _ex.width / 2;
                _ex.y = y - _ex.height / 2;
                _ex.play("explode", true);
            }
            _fire.stop();
            FourChambers_Globals.arrowCombo = 0;
            hasTouched= true;
            kill(); 
        }

        public override void hitLeft(FlxObject Contact, float Velocity)
        {
            _fire.setXSpeed(50, 100);
            _fire.setYSpeed(50, 100);
            _fire.start(true, 0.01f, 0);

            FourChambers_Globals.arrowCombo = 0;
            base.hitLeft(Contact, Velocity);
        }

        public override void hitRight(FlxObject Contact, float Velocity)
        {
            _fire.setXSpeed(50, 100);
            _fire.setYSpeed(50, 100);
            _fire.start(true, 0.01f, 0);

            FourChambers_Globals.arrowCombo = 0;
            base.hitRight(Contact, Velocity);
        }

        override public void hitBottom(FlxObject Contact, float Velocity) 
        {
            _fire.setXSpeed(50, 100);
            _fire.setYSpeed(50, 100);
            _fire.start(true, 0.01f, 0);

            if (explodesOnImpact)
            {
                _ex.x = x - _ex.width / 2;
                _ex.y = y - _ex.height / 2;
                _ex.play("explode", true);
            }
            _fire.stop();
            hasTouched = true;
            dead = true;
            solid = false;
            FourChambers_Globals.arrowCombo = 0;
            kill(); 
        }
        override public void hitTop(FlxObject Contact, float Velocity) 
        {
            _fire.setXSpeed(50, 100);
            _fire.setYSpeed(50, 100);
            _fire.start(true, 0.01f, 0);

            if (explodesOnImpact)
            {
                _ex.x = x - _ex.width / 2;
                _ex.y = y - _ex.height / 2;
                _ex.play("explode", true);
            }
            _fire.stop();
            hasTouched = true;
            FourChambers_Globals.arrowCombo = 0;
            kill(); 
        }
        override public void kill()
        {
            FlxG.play(SndHit, 0.5f, false);
            _fire.stop();




            visible = false;

            if (dead) return;
            velocity.X = 0;
            velocity.Y = 0;
            //if (onScreen()) FlxG.play(SndHit);
            
            play("explode");
            dead = true;





            //base.kill();
            
        }

        public void shoot(int X, int Y, int VelocityX, int VelocityY)
        {
            _fire.start(false, 0.01f, 0);
            
            // Global counter for arrows fired.
            FourChambers_Globals.arrowsFired++;

            visible = true;
            FlxG.play(SndShoot, 1.0f, false);

            play("normal");
            
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;
            hasTouched = false;
            dead = false;

        }
    }
}

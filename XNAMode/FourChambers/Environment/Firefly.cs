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
    class Firefly : FlxSprite
    {

        private float targetTimer = 10.0f;

        public Firefly(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/Firefly"), true, false, 5, 5);

            //addAnimation("animation", new int[] { 72, 73, 74, 75, 76, 77 }, 12, true);

            //play("animation");

            maxVelocity.X = 20;
            maxVelocity.Y = 20;
            maxAngular = 10;
            angularDrag = 50;
            maxThrust = 20;
            drag.X = 60;
            drag.Y = 60;
            angularAcceleration = FlxU.random(-230, 230);
            angle = FlxU.random(-360, 360);

        }



        override public void update()
        {
            targetTimer += FlxG.elapsed;

            if (targetTimer > 2.0f)
            {
                alpha += FlxU.random(-0.05f, 0.05f);

                thrust += FlxU.random(-60, 60);

                if (angle < 180)
                    angle += FlxU.random(0, 150);
                else
                    angle -= FlxU.random(-150, 0);

                if (angularAcceleration < 5)
                    angularAcceleration = FlxU.random(-130, 130);

            }

            if (x < 0)
            {
                x = FlxU.random(0, FlxG.levelWidth);
                alpha = 0.0f;
            }
            if (y < 0)
            {
                y = FlxU.random(0, FlxG.levelHeight);
                alpha = 0.0f;
            }
            if (x > FlxG.levelWidth)
            {
                x = FlxU.random(0, FlxG.levelWidth);
                alpha = 0.0f;
            }
            if (y > FlxG.levelHeight)
            {
                y = FlxU.random(0, FlxG.levelHeight);
                alpha = 0.0f;
            }


            //if (FlxG.mouse.pressed())
            //{
            //    targetTimer = 0.0f;

            //    float ang = FlxU.getAngle(new Vector2(FlxG.mouse.screenX, FlxG.mouse.screenY), new Vector2(x, y));
                
            //    angle = ang + 90;
            //    thrust = 50;
            //    angularVelocity = 20;



            //}


            //if (FlxG.keys.A)
            //{
            //    thrust = -130;
            //    angularDrag = 440;
            //    angle = 90;
            //}
            //if (FlxG.keys.S)
            //{
            //    thrust = 130;
            //    angularDrag = 440;
            //    angle = 180;
            //}
            //if (FlxG.keys.D)
            //{
            //    thrust = -130;
            //    angularDrag = 440;
            //    angle = 270;
            //}
            //if (FlxG.keys.W)
            //{
            //    thrust = 130;
            //    angularDrag = 440;
            //    angle = 0;
            //}


            //Console.WriteLine(color);



            base.update();

        }


    }
}

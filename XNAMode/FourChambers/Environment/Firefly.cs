﻿using System;
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

        public Firefly(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/Firefly"), true, false, 5, 5);

            //addAnimation("animation", new int[] { 72, 73, 74, 75, 76, 77 }, 12, true);

            //play("animation");

            maxVelocity.X = 20;
            maxVelocity.Y = 20;
            maxAngular = 30;
            angularDrag = 50;
            maxThrust = 60;
            drag.X = 60;
            drag.Y = 60;
            angularAcceleration = FlxU.random(-230, 230);
            angle = FlxU.random(-360, 360);

            color = Color.OrangeRed;

        }

        override public void update()
        {

            alpha += FlxU.random(-0.05f, 0.05f);

            thrust += FlxU.random(-60, 60);

            if (angle < 180)
                angle += FlxU.random(0, 150);
            else
                angle -= FlxU.random(-150, 0);           
            
            if (angularAcceleration<5)
                angularAcceleration = FlxU.random(-130, 130);

            //if (x < 10 )
            //{
            //    angle = 180;
            //    angularAcceleration = 3300;
            //}
            //if (y < 10 )
            //{
            //    angle = 270;
            //    angularAcceleration = 3300;
            //}


            //if (FlxG.mouse.pressed())
            //{
            //    x = FlxG.mouse.x;
            //    y = FlxG.mouse.y;
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





            base.update();

        }


    }
}

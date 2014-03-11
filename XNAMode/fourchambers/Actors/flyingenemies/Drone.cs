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
    class Drone : FlyingEnemy
    {
        public Drone(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Drone";
            score = 25;



            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Drone_ss_9x13"), true, false, 9, 13);

            addAnimation("fly", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1 }, 30);
            addAnimation("dead", new int[] { 0 }, 30);
            addAnimation("start", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1 }, 60);
            play("fly");

            //bounding box tweaks
            //width = 6;
            //height = 11;
            //offset.X = 1;
            //offset.Y = 1;

            chanceOfWingFlap = 0.0f;

            speedOfWingFlapVelocity = FlxU.random(-30.0f, -20.0f);

            
        }

        override public void update()
        {
            if (timeDead > 3.0f)
            {
                //reset(originalPosition.X, originalPosition.Y);
                dead = false;
                angle = 0;
                flicker(-0.001f);
                angularVelocity = 0;
                angularDrag = 700;
                drag.X = 0;
                timeDead = 0;
                play("fly");
                velocity.X = 100;
                velocity.Y = -50;
                
            }
            else if (timeDead > 2.0f)
            {
                play("start");
            }
            else if (dead)
            {
                play("dead");
            }
            else 
            {
                play("fly");
            }

            if (dead == false)
            {
                if (frame == 8)
                {
                    velocity.Y = speedOfWingFlapVelocity;
                    speedOfWingFlapVelocity = FlxU.random(-30.0f, -10.0f);
                }
            }


            base.update();
        }
    }
}

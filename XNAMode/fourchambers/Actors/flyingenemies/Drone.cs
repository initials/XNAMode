using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class Drone : FlyingEnemy
    {
        public Drone(int xPos, int yPos)
            : base(xPos, yPos)
        {

            //actorName = "Drone";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/drone_ss_9x13"), true, false, 9, 13);

            addAnimation("fly", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1 }, 30);

            play("fly");

            //bounding box tweaks
            width = 6;
            height = 11;
            offset.X = 1;
            offset.Y = 1;

            chanceOfWingFlap = 0.0f;

            speedOfWingFlapVelocity = FlxU.random(-30.0f, -20.0f);


        }

        override public void update()
        {

            if (dead == false)
            {
                if (frame == 8)
                {
                    //Console.WriteLine(_curAnim.name.ToString());

                    velocity.Y = speedOfWingFlapVelocity;
                    speedOfWingFlapVelocity = FlxU.random(-30.0f, -10.0f);
                }
            }

            base.update();
        }
    }
}

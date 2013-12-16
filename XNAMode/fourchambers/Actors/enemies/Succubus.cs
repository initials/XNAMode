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
    class Succubus : EnemyActor
    {

        public Succubus(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Josie";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/succubus_ss_35x30"), true, false, 35, 20);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //bounding box tweaks
            width = 9;
            height = 20;
            offset.X = 8;
            offset.Y = 0;

            //basic player physics
            int runSpeed = 120;
           // drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(30, 50);

            score = 250;

        }

        override public void update()
        {



            base.update();

        }


    }
}

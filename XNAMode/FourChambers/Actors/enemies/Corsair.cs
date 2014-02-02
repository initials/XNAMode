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
    class Corsair : EnemyActor
    {

        public Corsair(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Sierra";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/corsair_18x21"), true, false, 18, 21);

            addAnimation("run", new int[] { 1, 2, 3, 4, 5}, 12);
            addAnimation("walk", new int[] { 1, 2, 3, 4, 5 }, 8);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("jump", new int[] { 1, 1, 1, 2, 3 }, 6, false);
            addAnimation("attack", new int[] { 0,1,2}, 12);
            addAnimation("death", new int[] { 6 }, 12, false);


            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 4;
            offset.Y = 1;

            //basic player physics
            int runSpeed = 120;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(-50, 50);

            score = 250;

        }

        override public void update()
        {



            base.update();

        }


    }
}

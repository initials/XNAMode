﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class Mummy : Actor
    {

        public Mummy(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Elizabeth";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/mummy_ss_13x19"), true, false, 13, 19);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2}, 12);

            //bounding box tweaks
            width = 9;
            height = 15;
            offset.X = 2;
            offset.Y = 4;

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;





        }

        override public void update()
        {



            base.update();

        }


    }
}
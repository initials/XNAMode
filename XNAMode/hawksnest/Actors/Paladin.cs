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
    class Paladin : Actor
    {

        public Paladin(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/paladin_ss_16x26"), true, false, 16, 26);

            addAnimation("run", new int[] {0, 1, 2, 3, 4, 5, 6, 7 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 3;
            offset.Y = 6;

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

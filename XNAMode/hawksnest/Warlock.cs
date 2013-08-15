using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace XNAMode
{
    class Warlock : Actor
    {
        private Texture2D ImgWarlock;


        public Warlock(int xPos, int yPos)
            : base(xPos, yPos)
        {

            isPlayerControlled = false;

            ImgWarlock = FlxG.Content.Load<Texture2D>("initials/warlock_ss_22x29");

            loadGraphic(ImgWarlock, true, false, 22, 29);

            //bounding box tweaks
            width = 14;
            height = 20;
            offset.X = 4;
            offset.Y = 9;

            //basic player physics
            int runSpeed = 80;
            drag.X = runSpeed * 8;
            acceleration.Y = 200;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 205;


            //animations
            addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attack", new int[] { 11, 12 }, 12);



        }

        override public void update()
        {


            base.update();

        }


    }
}

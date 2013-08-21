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
    class Mistress : Actor
    {

        public Mistress(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Linda Lee";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/mistress_ss_35x30"), true, false, 35, 22);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 12);

            //bounding box tweaks
            width = 7;
            height = 18;
            offset.X = 9;
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

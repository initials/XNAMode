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
    class Horse : EnemyActor
    {
        public Horse(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Fast Eddie";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/horse_ss_20x25"), true, false, 20, 25);

            addAnimation("run", new int[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0 }, 12);

            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 5;
            offset.Y = 5;

            //basic player physics
            int runSpeed = 120;
            //drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(30, 80);

            score = 225;

        }

        override public void update()
        {
            base.update();
        }
    }
}

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
    class Executor : EnemyActor
    {

        public Executor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Executor";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/executor_ss_21x21"), true, false, 21, 21);

            addAnimation("run", new int[] { 1, 2, 3, 4, 5,6,7,8 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //bounding box tweaks
            width = 7;
            height = 20;
            offset.X = 7;
            offset.Y = 1;

            //basic player physics
            int runSpeed = 60;
            //drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(12, 22);

            score = 150;

        }

        override public void update()
        {



            base.update();

        }


    }
}

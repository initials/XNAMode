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
    class Harvester : EnemyActor
    {

        public Harvester(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Death";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/harvester_ss_14x27"), true, false, 14, 27);

            addAnimation("run", new int[] { 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0,1,2 }, 18);

            //bounding box tweaks
            width = 8;
            height = 20;
            offset.X = 3;
            offset.Y = 7;

            //basic player physics
            int runSpeed = 120;
            //drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(11, 22);

            score = 50000;

        }

        override public void update()
        {



            base.update();

        }


    }
}

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
    class Gloom : EnemyActor
    {

        public Gloom(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Gloom";
            
            loadGraphic(FlxG.Content.Load<Texture2D>("initials/gloom_spritesheet"), true, false, 13, 26);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5,6,7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //bounding box tweaks
            width = 7;
            height = 26;
            offset.X = 2;
            offset.Y = 0;

            //basic player physics
            int runSpeed = 120;
            //drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            velocity.X = FlxU.random(30, 50);

        }

        override public void update()
        {





            base.update();

        }


    }
}

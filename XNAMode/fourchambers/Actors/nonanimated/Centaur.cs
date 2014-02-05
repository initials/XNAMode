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
    class Centaur : EnemyActor
    {
        public Centaur(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Centaur";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/allActors"), true, false, 26, 26);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { FR_centaur }, 0);
            //addAnimation("attack", new int[] { 2, 4 }, 18);

            //bounding box tweaks
            width = 20;
            height = 20;
            offset.X = 3;
            offset.Y = 6;

            //basic player physics
            int runSpeed = 30;
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

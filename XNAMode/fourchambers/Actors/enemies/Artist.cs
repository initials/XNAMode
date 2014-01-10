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
    class Artist : EnemyActor
    {
        public Artist(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Scott The Rocks";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/artist_ss_20x20"), true, false, 20, 20);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3, 4 }, 12);
            addAnimation("death", new int[] { 6 }, 0);
            //addAnimation("attack", new int[] { 2, 4 }, 18);

            //bounding box tweaks
            width = 16;
            height = 16;
            offset.X = 2;
            offset.Y = 4;

            //basic player physics
            int runSpeed = 30;
            drag.X = runSpeed * 4;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

        }

        override public void update()
        {
            base.update();
        }
    }
}

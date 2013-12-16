using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace FourChambers
{
    class Vampire : EnemyActor
    {
        private Texture2D ImgVampire;

        public Vampire(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Count Esperanza";

            ImgVampire = FlxG.Content.Load<Texture2D>("initials/vampire_ss_14x19");

            loadGraphic(ImgVampire, true, false, 14, 19);

            //bounding box tweaks
            width = 6;
            height = 18;
            offset.X = 4;
            offset.Y = 1;

            //basic player physics
            //int runSpeed = 35;
            //drag.X = runSpeed * 4;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            //maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            //animations

            int frameRate = (int)FlxU.random(10, 14);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, frameRate);
            addAnimation("idle", new int[] { 0 }, frameRate);
            addAnimation("attack", new int[] { 0, 1, 2 }, frameRate);

            velocity.X = FlxU.random(30, 50) ;

            score = 10000;



        }

        override public void update()
        {



            base.update();

        }


    }
}

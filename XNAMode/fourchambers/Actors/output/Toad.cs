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
    class Toad : Actor
    {
        public Toad(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "The TemplateActor";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/toad_11x24"), true, false, 11, 24);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 2, 4 }, 18);

            //bounding box tweaks
            width = 7;
            height = 20;
            offset.X = 2;
            offset.Y = 4;

            //basic player physics
            int runSpeed = 30;
            drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            jumpPower = -140;
        }

        override public void update()
        {
            base.update();
        }
    }
}

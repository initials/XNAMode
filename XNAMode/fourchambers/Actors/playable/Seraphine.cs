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
    class Seraphine : FlxSprite
    {

        public Seraphine(int xPos, int yPos)
            : base(xPos, yPos)
        {
            //actorName = "Jennifer Twist";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/seraphine_ss_24x30"), true, false, 24, 30);

            addAnimation("fly", new int[] {0,1,2,3,4,5,6,7,8,9 }, 18);
            addAnimation("jump", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = 600;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            width = 10;
            height = 20;
            offset.X = 7;
            offset.Y = 10;



        }

        override public void update()
        {
            //if (isPlayerControlled)
            //{
            //    PlayerIndex pi;
            //    if ((FlxG.keys.justPressed(Keys.X) || FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi)))
            //    {
            //        velocity.Y = -155;
            //    }
            //}

            base.update();
        }
    }
}

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
    class Bat : FlyingEnemy
    {

        public Bat(int xPos, int yPos)
            : base(xPos, yPos)
        {
            score = 100;
            health = 1;
            actorName = "Bat";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Bat_12x12"), true, false, 12, 12);

            addAnimation("fly", new int[] { 0, 1, 2 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);

            addAnimation("attack", new int[] { 2, 4 }, 18);
            addAnimation("death", new int[] { 1 }, 18);

            //bounding box tweaks
            width = 10;
            height = 9;
            offset.X = 1;
            offset.Y = 3;

            //basic player physics
            int runSpeed = 30;
            //drag.X = runSpeed * 4;
            acceleration.Y = 50;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            //jumpPower = -140;

            velocity.X = 100;

            play("fly");



        }
       
        override public void update()
        {

            base.update();

        }


    }
}

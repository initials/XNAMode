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
    class ZingerHoming : FlxSprite
    {
        protected float chanceOfWingFlap = 0.023f;

        protected float speedOfWingFlapVelocity = -40;

        /// <summary>
        /// The score to recieve when killing this actor
        /// </summary>
        public int score = 50;

        FlxSprite _player;


        public ZingerHoming(int xPos, int yPos, FlxSprite player)
            : base(xPos, yPos)
        {

            //actorName = "Zinger";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/zinger_ss_12x14"), true, false, 12, 14);

            addAnimation("fly", new int[] { 0, 1 }, 30);
            play("fly");

            //bounding box tweaks
            width = 10;
            height = 10;
            offset.X = 1;
            offset.Y = 4;

            chanceOfWingFlap += FlxU.random(0.005, 0.009);
            speedOfWingFlapVelocity += FlxU.random(0, 3);

            originalPosition.X = xPos;
            originalPosition.Y = yPos;

            int runSpeed = 30;
            acceleration.Y = 50;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;
            velocity.X = 100;

            _player = player;
            
            
            color = Color.Red;
            maxAngular = 120;
            angularDrag = 400;
            maxThrust = 200;
            drag.X = 80;
            drag.Y = 80;

        }

        override public void update()
        {

            float rightX1 = _player.x;
            float rightY1 = _player.y;

            float xDiff = x - rightX1;
            float yDiff = y - rightY1;

            double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

            double radians = Math.PI / 180 * degrees;

            double velocity_x = Math.Cos((float)radians);
            double velocity_y = Math.Sin((float)radians);


            velocity.X = (float)velocity_x * -400;
            velocity.Y = (float)velocity_y * -400;

            base.update();



        }
        public override void kill()
        {
            FlxG.score += score;

            play("death");
            dead = true;
            base.kill();
        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
    }
}

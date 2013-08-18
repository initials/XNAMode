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
    class Actor : FlxSprite
    {
        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;

        /// <summary>
        /// How high the character will jump.
        /// </summary>
        public float jumpPower;

        /// <summary>
        /// How many frames have passed since the character left the ground.
        /// </summary>
        public float framesSinceLeftGround;


        public List<FlxObject> _bullets;
        public int _curBullet;



        public Actor(int xPos, int yPos)
            : base(xPos,yPos)
		{

            isPlayerControlled = false;


            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            jumpPower = -305;

        }

        override public void update()
        {

            PlayerIndex pi;

            // Calculate how many frames since the player left the ground

            if (velocity.Y == 0) framesSinceLeftGround = 0;

            else
            {
                framesSinceLeftGround++;


            }

            //MOVEMENT

            if (isPlayerControlled)
            {
                acceleration.X = 0;
                if (FlxG.keys.LEFT || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Left;
                    acceleration.X -= drag.X;
                }
                else if (FlxG.keys.RIGHT || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Right;
                    acceleration.X += drag.X;
                }

                // && velocity.Y == 0
                if ((FlxG.keys.justPressed(Keys.X) || FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi)) && framesSinceLeftGround < 10)
                {

                    velocity.Y = jumpPower;

                }
                if ((FlxG.keys.justPressed(Keys.C) || FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi)))
                {
                    FlxG.log("Attacking");

                    play("attack");

                }

    //            if (!flickering() && 
    //    (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickDown, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickUp, FlxG.controllingPlayer, out pi)))
    //            {

    //                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

    //                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

    //                if (rightY < -0.75)
    //                {
    //                    velocity.Y -= 36;
    //                }

    //                float rotation = (float)Math.Atan2(rightX, rightY);
    //                rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);



    //            }
            }


            //ANIMATION
            if (velocity.Y != 0)
            {
                play("jump");
            }
            else if (velocity.X == 0)
            {
                play("idle");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                play("run");
            }

            base.update();

        }


    }
}

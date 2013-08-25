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
        public const float DEADZONE = 0.5f;
        
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;


        public List<FlxObject> _bullets;
        public int _curBullet;

        public bool attacking = false;



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

        public void stopAttacking(string Name, uint Frame, int FrameIndex)
        {
            //attacking = false;
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
                    attacking = false;
                    facing = Flx2DFacing.Left;
                    acceleration.X -= drag.X;
                }
                else if (FlxG.keys.RIGHT || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    attacking = false;
                    facing = Flx2DFacing.Right;
                    acceleration.X += drag.X;
                }

                // && velocity.Y == 0
                if ((FlxG.keys.justPressed(Keys.X) || FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi)) && framesSinceLeftGround < 10)
                {
                    attacking = false;
                    velocity.Y = jumpPower;

                }
                if ((FlxG.keys.justPressed(Keys.C) || FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, FlxG.controllingPlayer, out pi)))
                {
                    attacking = true;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < DEADZONE * -1.0f ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < DEADZONE * -1.0f)
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }

                if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Left;
                }
                if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Right;
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
            if (dead)
            {
                play("death");
            }
            else if (attacking)
            {
                play("attack");
            }
            else if (velocity.Y != 0)
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
            else
            {
                play("idle");
            }

            base.update();

        }

        public override void kill()
        {
            base.kill();
            
            //FlxG.bloom.Visible = true;

        }


    }

}

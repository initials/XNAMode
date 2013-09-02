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
    class Marksman : Actor
    {
        public static int _curArrow;

        public Marksman(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Marqu";

            _bullets = Bullets;

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/marksman_ss_31x24"), true, false, 31, 24);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 26 ,true);
            addAnimation("jump", new int[] { 3, 4, 5, 6, 7, 8, 9 }, 3, true);

            //addAnimationCallback(stopAttacking);

            //bounding box tweaks
            width = 5;
            height = 16;
            offset.X = 13;
            offset.Y = 8;

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = Actor.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

        }

        override public void update()
        {

            //PlayerIndex pi;

            //SHOOTING
            //if (!flickering() && (FlxG.keys.justPressed(Keys.C) ||
            //        FlxG.gamepads.isNewButtonPress(Buttons.RightTrigger, FlxG.controllingPlayer, out pi)) ||
            //        FlxG.gamepads.isButtonDown(Buttons.LeftTrigger, FlxG.controllingPlayer, out pi))
            //{
            //    //attacking = true;

            

            if ((_curFrame == 8 || _curFrame==9 || _curFrame==10) && attackingJoystick )
            {
                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                
                // No Right Stick so do a generic shoot.
                if (rightX ==0 && rightY ==0 )
                {
                    if (facing == Flx2DFacing.Right)
                        ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), 600, -100);
                    else
                        ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), -600, -100);
                }
                // use the right stick to fire a weapon
                else
                {
                    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(rightX * 600), (int)(rightY *= -600));
                }
                if (rightX < 0)
                {
                    ((Arrow)(_bullets[_curArrow])).facing = Flx2DFacing.Left;
                }
                else
                {
                    ((Arrow)(_bullets[_curArrow])).facing = Flx2DFacing.Right;
                }

                if (++_curArrow >= _bullets.Count)
                    _curArrow = 0;
                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }

            // use the mouse position to fire a bullet.
            if ((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && (attackingMouse))
            {
                float rightX1 = FlxG.mouse.x;
                float rightY1 = FlxG.mouse.y;

                float xDiff = x - rightX1;
                float yDiff = y - rightY1;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                double velocity_x = Math.Cos((float)radians);
                double velocity_y = Math.Sin((float)radians);

                //angle = (float)degrees;
                Console.WriteLine(" x {0} y {1} x {2} y {3} vx {4} vy {5} rot {6} degrees {7}", x, y, rightX1, rightY1, velocity_x, velocity_y, radians, degrees);

            
                ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(velocity_x *= -600 ), (int)(velocity_y *= -600 ));

                if (++_curArrow >= _bullets.Count)
                    _curArrow = 0;
                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }


            

            base.update();

        }
    }
}

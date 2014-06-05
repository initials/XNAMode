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
    public class Marksman : Actor
    {
        public static int _curArrow;

        /// <summary>
        /// arrows left
        /// </summary>
        public int arrowsRemaining = 0;

        public MeleeHitBox meleeHitBox;

        public bool hasUsedJoystickToAim = false;

        private double _degrees;

        private Vector2 lastJoystickDirection;

        

        public Marksman(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Marqu";

            _bullets = Bullets;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Marksman_ss_31x24"), true, false, 31, 24);

            addAnimation("run", new int[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 }, 12);
            addAnimation("idle", new int[] { 30 }, 12);
            addAnimation("idleMelee", new int[] { 28 }, 12);
            addAnimation("attack", new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 60 ,true);
            addAnimation("attackMelee", new int[] { 0, 24, 24, 25, 26, 27, 27, 26, 26, 26, 26, 26, 26 }, 60, true);
            
            addAnimation("jump", new int[] { 33, 34, 35, 36, 37, 38, 39 }, 3, true);
            addAnimation("jumpRange", new int[] { 3, 4, 5, 6, 7, 8, 9 }, 3, true);
            addAnimation("climb", new int[] { 20, 21 }, 6, true);
            addAnimation("climbidle", new int[] { 20 }, 0, true);
            addAnimation("death", new int[] { 22,23 }, 4, false);
            addAnimation("hurt", new int[] { 22, 23 }, 4, false);

            addAnimation("runRange", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idleRange", new int[] { 0 }, 12);
            //addAnimationCallback(stopAttacking);

            //bounding box tweaks
            width = 5;
            height = 16;
            offset.X = 13;
            offset.Y = 8;

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            arrowsRemaining = 20;

            meleeHitBox = new MeleeHitBox(xPos, yPos);
            meleeHitBox.width = 5;
            meleeHitBox.height = 5;

            lastJoystickDirection = new Vector2(0, 0);

            timeDownAfterHurt = 2.0f;


            hasMeleeWeapon = FourChambers_Globals.hasMeleeWeapon;
            hasRangeWeapon = FourChambers_Globals.hasRangeWeapon;


        }

        public void adjustMeleeBox()
        {
            if (attackingMelee)
            {
                meleeHitBox.width = 5;
                meleeHitBox.height = 5;
                // position the hit box of the whip.

                if (facing == Flx2DFacing.Right)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x + 14;
                            meleeHitBox.y = y;
                            break;
                        case 5:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x + 16;
                            meleeHitBox.y = y + 2;
                            break;
                        case 6:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 7;
                            meleeHitBox.height = 7;
                            meleeHitBox.x = x + 28;
                            meleeHitBox.y = y + 3;
                            break;
                        case 7:
                            attackingMelee = false;
                            break;
                        default:
                            meleeHitBox.dead = true;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 10;
                            meleeHitBox.x = x;
                            meleeHitBox.y = y;
                            break;
                    }
                }
                if (facing == Flx2DFacing.Left)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x - 10;
                            meleeHitBox.y = y;
                            break;
                        case 5:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x - 12;
                            meleeHitBox.y = y + 2;
                            break;
                        case 6:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 7;
                            meleeHitBox.height = 7;
                            meleeHitBox.x = x - 24;
                            meleeHitBox.y = y + 3;
                            break;
                        case 7:
                            attackingMelee = false;
                            break;

                        default:
                            meleeHitBox.dead = true;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 10;
                            meleeHitBox.x = x;
                            meleeHitBox.y = y;
                            break;
                    }
                }
            }
            else
            {
                meleeHitBox.dead = true;
                meleeHitBox.x = x;
                meleeHitBox.y = y;
                meleeHitBox.width = 0;
                meleeHitBox.height = 0;
            }
        }

        override public void update()
        {

            adjustMeleeBox();

            float rightX11 = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
            float rightY11 = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

			#if __ANDROID__

			//rightY11 *= -1;

			#endif

            if (rightX11 != 0 || rightY11 != 0 || hasUsedJoystickToAim)
            {
                hasUsedJoystickToAim = true;

                float xDiff = 0 - rightX11;
                float yDiff = 0 - rightY11;

                if (rightX11 == 0 && rightY11 == 0)
                {

                }
                else
                {
                    _degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                }

                double radians = Math.PI / 180 * _degrees;

                Vector2 rotpoint = FlxU.rotatePoint(x - 50, y, x, y, (float)_degrees * -1);
                FlxG.mouse.cursor.x = rotpoint.X;
                FlxG.mouse.cursor.y = rotpoint.Y;

            }

            if (FlxG.mouse.pressed())
            {
                hasUsedJoystickToAim = false;
            }

            if (hasRangeWeapon && ((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && attackingJoystick) || (FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder) && velocity.X != 0) )
            {

                Console.WriteLine("Shooting Arrow " + FlxG.elapsedTotal + " This is the frame of the Marksman animation" + _curFrame);


                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

				#if __ANDROID__

				//rightY *= -1;

				#endif
                
                // No Right Stick so do a generic shoot...
                if (arrowsRemaining >= 1)
                {
                    for (int i = 0; i < FourChambers_Globals.arrowsToFire; i++)
                    {
                        if (rightX == 0 && rightY == 0)
                        {
                            //if (facing == Flx2DFacing.Right)
                            //    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), 600, -100 + (i * 40));
                            //else
                            //    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), -600, -100 + (i * 40));

                            //Console.WriteLine(12 * (int)(x - FlxG.mouse.cursor.x) * -1);
                            
                            int yVel = (int)(12 * (int)(y - FlxG.mouse.cursor.y) * -1);
                            int yVelAdjusted = yVel - (i * 40);
                            ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), 12 * (int)(x - FlxG.mouse.cursor.x) * -1, yVelAdjusted);
                            
                        }
                        // use the right stick to fire a weapon
                        else
                        {
                            int yVel = (int)(rightY * -600);
                            int yVelAdjusted = yVel - (i * 40);

                            ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(rightX * 600), yVelAdjusted);
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
                    }
                    arrowsRemaining--;
                }


                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }

            // use the mouse position to fire a bullet.
            if ((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && (attackingMouse) && hasRangeWeapon)
            {
                Console.WriteLine("Shooting Arrow " + FlxG.elapsedTotal + " This is the frame of the Marksman animation" + _curFrame);

                float rightX1 = FlxG.mouse.x;
                float rightY1 = FlxG.mouse.y;

                float xDiff = x - rightX1;
                float yDiff = y - rightY1;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                double velocity_x = Math.Cos((float)radians);
                double velocity_y = Math.Sin((float)radians);

                if (arrowsRemaining >= 1)
                {
                    for (int i = 0; i < FourChambers_Globals.arrowsToFire; i++)
                    {
                        int yVel = (int)(velocity_y * -600);
                        int yVelAdjusted = yVel - (i * 40);

                        ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(velocity_x *= -600), yVelAdjusted);
                        arrowsRemaining--;
                    }
                }

                if (rightX1 - x < 0)
                {
                    facing = Flx2DFacing.Left;

                    //Console.WriteLine("Left");
                }
                else
                {
                    facing = Flx2DFacing.Right;

                    //Console.WriteLine("Right");
                }

                if (++_curArrow >= _bullets.Count)
                    _curArrow = 0;
                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }


            if (FourChambers_Globals.seraphineHasBeenKilled == false)
            {
                if (FlxG.gamepads.isButtonDown(Buttons.Y) || FlxG.keys.W)
                {
                    velocity.Y = -100;
                    //FlxG.bloom.Visible = true;
                    flying = true;

                }
                else
                {
                    //velocity.Y = FourChambers_Globals.GRAVITY;
                    //FlxG.bloom.Visible = false;
                    flying = false;
                }
            }

            if (FlxGlobal.cheatString == "weapons")
            {
                hasMeleeWeapon = true;
                hasRangeWeapon = true;

                FourChambers_Globals.hasMeleeWeapon = true;
                FourChambers_Globals.hasRangeWeapon = true;
            }

            base.update();

        }

        public override void render(SpriteBatch spriteBatch)
        {
            
            base.render(spriteBatch);
            meleeHitBox.render(spriteBatch);
        }
    }
}

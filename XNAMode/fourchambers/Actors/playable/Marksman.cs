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

        /// <summary>
        /// arrows left
        /// </summary>
        public int arrowsRemaining = 0;

        public FlxSprite meleeHitBox;
        

        public Marksman(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Marqu";

            _bullets = Bullets;

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/marksman_ss_31x24"), true, false, 31, 24);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("idleMelee", new int[] { 28 }, 12);
            addAnimation("attack", new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 60 ,true);
            addAnimation("attackMelee", new int[] { 0, 24, 24, 25, 26, 27, 27, 26, 26, 26, 26, 26, 26 }, 60, true);
            
            addAnimation("jump", new int[] { 3, 4, 5, 6, 7, 8, 9 }, 3, true);
            addAnimation("climb", new int[] { 20, 21 }, 6, true);
            addAnimation("climbidle", new int[] { 20 }, 0, true);
            addAnimation("death", new int[] { 22,23 }, 15, true);
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
                            meleeHitBox.x = x + 18;
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
                            meleeHitBox.x = x - 14;
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
            if (rightX11 != 0 || rightY11 != 0)
            {
                
                float xDiff = 0 - rightX11;
                float yDiff = 0 - rightY11;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                Vector2 rotpoint = FlxU.rotatePoint(x-50, y, x, y, (float)degrees*-1);
                FlxG.mouse.cursor.x = rotpoint.X;
                FlxG.mouse.cursor.y = rotpoint.Y;

            }


            if (((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && attackingJoystick) || (FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder) && velocity.X != 0) )
            {

                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                
                // No Right Stick so do a generic shoot.
                if (arrowsRemaining >= 1)
                {
                    for (int i = 0; i < FourChambers_Globals.arrowsToFire; i++)
                    {
                        if (rightX == 0 && rightY == 0)
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
                        
                    }
                    arrowsRemaining--;
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

                if (arrowsRemaining >= 1)
                {
                    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(velocity_x *= -600), (int)(velocity_y *= -600));
                    arrowsRemaining--;
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
                if (FlxG.gamepads.isButtonDown(Buttons.Y))
                {
                    velocity.Y = -100;
                    //FlxG.bloom.Visible = true;

                }
                else
                {
                    //velocity.Y = FourChambers_Globals.GRAVITY;
                    //FlxG.bloom.Visible = false;

                }
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

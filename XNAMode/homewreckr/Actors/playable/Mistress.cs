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
    class Mistress : Actor
    {
        public FlxSprite whipHitBox;

        public Mistress(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Linda Lee";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/mistress_ss_35x22"), true, false, 35, 22);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 30);

            //bounding box tweaks
            width = 9;
            height = 16;
            offset.X = 13;
            offset.Y = 6;

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            whipHitBox = new WhipHitBox(xPos, yPos);
            whipHitBox.width = 5;
            whipHitBox.height = 5;

            


        }

        override public void update()
        {
            if (attackingJoystick || attackingMouse)
            {
                whipHitBox.width = 5;
                whipHitBox.height = 5;
                // position the hit box of the whip.

                if (facing == Flx2DFacing.Right)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            whipHitBox.dead = false;
                            whipHitBox.x = x + 14;
                            whipHitBox.y = y ;
                            break;
                        case 5:
                            whipHitBox.dead = false;
                            whipHitBox.x = x + 16;
                            whipHitBox.y = y + 2;
                            break;
                        case 6:
                            whipHitBox.dead = false;
                            whipHitBox.width = 7;
                            whipHitBox.height = 7;
                            whipHitBox.x = x + 18;
                            whipHitBox.y = y + 3;
                            break;
                        default:
                            whipHitBox.dead = true;
                            whipHitBox.width = 10;
                            whipHitBox.height = 10;
                            whipHitBox.x = x;
                            whipHitBox.y = y;
                            break;
                    }
                }
                if (facing == Flx2DFacing.Left)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            whipHitBox.dead = false;
                            whipHitBox.x = x - 10;
                            whipHitBox.y = y ;
                            break;
                        case 5:
                            whipHitBox.dead = false;
                            whipHitBox.x = x - 12;
                            whipHitBox.y = y + 2;
                            break;
                        case 6:
                            whipHitBox.dead = false;
                            whipHitBox.width = 7;
                            whipHitBox.height = 7;
                            whipHitBox.x = x - 14;
                            whipHitBox.y = y + 3;
                            break;
                        default:
                            whipHitBox.dead = true;
                            whipHitBox.width = 10;
                            whipHitBox.height = 10;
                            whipHitBox.x = x;
                            whipHitBox.y = y;
                            break;
                    }
                }
            }
            else
            {
                whipHitBox.dead = true;
                whipHitBox.x = x;
                whipHitBox.y = y;
                whipHitBox.width = 0;
                whipHitBox.height = 0;
            }

            //Console.WriteLine("Can climb? " + canClimbLadder);


            base.update();

            //Console.WriteLine("Can climb? " + canClimbLadder);
            //canClimbLadder = false;

        }


    }
}

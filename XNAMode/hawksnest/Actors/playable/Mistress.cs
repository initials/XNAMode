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

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/mistress_ss_35x30"), true, false, 35, 22);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 18);

            //bounding box tweaks
            width = 7;
            height = 18;
            offset.X = 9;
            offset.Y = 4;

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
            if (attacking)
            {
                whipHitBox.width = 5;
                whipHitBox.height = 5;
                // position the hit box of the whip.
                switch (_curFrame)
                {
                    case 0:
                        whipHitBox.x = x + 10;
                        whipHitBox.y = y;
                        break;
                    case 1:
                        whipHitBox.x = x + 8;
                        whipHitBox.y = y + 2;
                        break;
                    case 2:
                        whipHitBox.x = x + 6;
                        whipHitBox.y = y + 4;
                        break;
                    case 3:
                        whipHitBox.x = x + 4;
                        whipHitBox.y = y+6;
                        break;
                    default:
                        whipHitBox.x = x + 5;
                        whipHitBox.y = y + 5;
                        break;
                }
            }
            else
            {
                whipHitBox.x = x;
                whipHitBox.y = y;
                whipHitBox.width = 0;
                whipHitBox.height = 0;
            }

            base.update();

        }


    }
}

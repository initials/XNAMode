using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace FourChambers
{
    public class Warlock : Actor
    {
        private Texture2D ImgWarlock;

        public Warlock(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Terry";

            _bullets = Bullets;

            isPlayerControlled = false;

            ImgWarlock = FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Warlock_ss_29x29");

            loadGraphic(ImgWarlock, true, false, 29, 29);

            //bounding box tweaks
            width = 7;
            height = 16;
            offset.X = 11;
            offset.Y = 13;


            //animations
            addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attack", new int[] { 11,12,13,14,15,16 }, 14);


        }

        override public void update()
        {

            //PlayerIndex pi;
            
            //SHOOTING
            if ((frame == 16 ) && attackingJoystick)
            {
                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (rightX == 0 && rightY == 0)
                {
                    if (facing == Flx2DFacing.Right)
                        ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), 600, -100);
                    else
                        ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), -600, -100);
                }
                else
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), (int)(rightX * 600), (int)(rightY *= -600));
                }
                if (rightX < 0)
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).facing = Flx2DFacing.Left;
                }
                else
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).facing = Flx2DFacing.Right;
                }

                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;

                attackingJoystick = false;
                attackingMouse = false;
                frame = 0;

            }


            base.update();

        }


    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace XNAMode
{
    class Warlock : Actor
    {
        private Texture2D ImgWarlock;

        public Warlock(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Terry";

            _bullets = Bullets;

            isPlayerControlled = false;

            ImgWarlock = FlxG.Content.Load<Texture2D>("initials/warlock_ss_29x29");

            loadGraphic(ImgWarlock, true, false, 29, 29);

            //bounding box tweaks
            width = 7;
            height = 16;
            offset.X = 11;
            offset.Y = 13;


            //animations
            addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attack", new int[] { 11, 11,11,11,11,11,12 }, 30);



        }

        override public void update()
        {

            //PlayerIndex pi;
            
            //SHOOTING
            if ((_curFrame == 6 ) && attackingJoystick)
            {
                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (rightX == 0 && rightY == 0)
                {
                    if (facing == Flx2DFacing.Right)
                        ((Fireball)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 2)), 600, -100);
                    else
                        ((Fireball)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 2)), -600, -100);
                }
                else
                {
                    ((Fireball)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 2)), (int)(rightX * 600), (int)(rightY *= -600));
                }
                if (rightX < 0)
                {
                    ((Fireball)(_bullets[_curBullet])).facing = Flx2DFacing.Left;
                }
                else
                {
                    ((Fireball)(_bullets[_curBullet])).facing = Flx2DFacing.Right;
                }

                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;

                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }


            base.update();

        }


    }
}

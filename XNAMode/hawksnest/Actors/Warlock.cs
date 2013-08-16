using System;
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

            _bullets = Bullets;

            isPlayerControlled = false;

            ImgWarlock = FlxG.Content.Load<Texture2D>("initials/warlock_ss_22x29");

            loadGraphic(ImgWarlock, true, false, 22, 29);

            //bounding box tweaks
            width = 14;
            height = 20;
            offset.X = 4;
            offset.Y = 9;


            //animations
            addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attack", new int[] { 11, 12 }, 12);



        }

        override public void update()
        {

            PlayerIndex pi;
            //SHOOTING
            if (!flickering() && (FlxG.keys.justPressed(Keys.C) ||
                    FlxG.gamepads.isNewButtonPress(Buttons.RightTrigger, FlxG.controllingPlayer, out pi)) || 
                    FlxG.gamepads.isButtonDown(Buttons.LeftTrigger, FlxG.controllingPlayer, out pi))
            {
                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                //Console.WriteLine(rightX + " " + rightY);



                ((Fireball)(_bullets[_curBullet])).shoot((int)x, (int)y, (int)(rightX * 300), (int)(rightY*=-300));
                //((Fireball)(_bullets[_curBullet])).angularVelocity = 10;
                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;
            }


            base.update();

        }


    }
}

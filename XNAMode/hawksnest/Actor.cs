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



        public Actor(int xPos, int yPos)
            : base(xPos,yPos)
		{
            //basic player physics
            int runSpeed = 80;
            drag.X = runSpeed * 8;
            acceleration.Y = 420;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 205;




        }

        override public void update()
        {

            PlayerIndex pi;

            //MOVEMENT
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
            if ((FlxG.keys.justPressed(Keys.X) || FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi))
                && velocity.Y == 0)
            {
                velocity.Y = -205;

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
            else
            {
                play("run");
            }

            base.update();

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace org.flixel
{
    class FlxPlatformActor : FlxSprite
    {

        /// <summary>
        /// A set of Actions to determine what animation to play.
        /// </summary>
        public enum Actions
        {
            idle = 1,
            walk = 2,
            run = 3,
            hurt = 4,
            death = 5,
            talk = 6,
        }

        /// <summary>
        /// The current action.
        /// </summary>
        public Actions action = Actions.idle;


        /// <summary>
        /// <para>What is currently controlling the Actor</para>
        /// <para>None = No ability to control.</para>
        /// <para>Player = controlling with the keyboard or mouse or gamepad</para>
        /// <para>File = playback from a file.</para>
        /// </summary>
        public enum Controls
        {
            none = 0,
            player = 1,
            file = 2,
        }

        /// <summary>
        /// The current controlling action.
        /// </summary>
        public Controls control = Controls.none;

        public string controlFile = "";

        /// <summary>
        /// Player index controls which controller is controlling this. 1,2,3 or 4.
        /// </summary>
        private PlayerIndex playerIndex;

        private int playerIndexAsInt = 0;

        //Player Physics

        public int runSpeed;

        public PlayerIndex ControllingPlayer
        {
               get 
               { 
                  return playerIndex; 
               }
               set 
               {
                   playerIndex = value;

                   if (value == PlayerIndex.One) playerIndexAsInt = 1;
                   else if (value == PlayerIndex.Two) playerIndexAsInt = 2;
                   else if (value == PlayerIndex.Three) playerIndexAsInt = 3;
                   else if (value == PlayerIndex.Four) playerIndexAsInt = 4;

               }
        }

        

        public FlxPlatformActor(int xPos, int yPos)
            : base(xPos, yPos)
        {


        }

        override public void update()
        {
            PlayerIndex pi;

            if (control == Controls.player)
            {
                if (FlxG.keys.A) leftPressed();
                if (FlxG.gamepads.isButtonDown(Buttons.DPadLeft, ControllingPlayer, out pi)) leftPressed();
                if (FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, ControllingPlayer, out pi)) leftPressed();

                if (FlxG.keys.D) rightPressed();
                if (FlxG.gamepads.isButtonDown(Buttons.DPadRight, ControllingPlayer, out pi)) rightPressed();
                if (FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, ControllingPlayer, out pi)) rightPressed();


            }
            else if (control == Controls.file)
            {

            }


            updateAnims();

            base.update();
        }


        private void leftPressed()
        {
            acceleration.X -= runSpeed;
        }
        private void rightPressed()
        {
            acceleration.X += runSpeed;
        }

        /// <summary>
        /// Updates to the correct animation
        /// </summary>
        private void updateAnims()
        {
            if (dead)
            {
                play("death");
            }
            else if (velocity.Y != 0)
            {
                play("jump");
            }
            else if (velocity.X == 0)
            {
                play("idle");
            }
            else if (velocity.X > 1)
            {
                facing = Flx2DFacing.Right;
                play("run");
            }
            else if (velocity.X < 1)
            {
                facing = Flx2DFacing.Left;
                play("run");
            }


        }
    }
}

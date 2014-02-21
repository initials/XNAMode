using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace org.flixel
{
    /// <summary>
    /// Helper class to check for an input across keyboard and gamepad.
    /// </summary>
    public class FlxControl
    {
        public bool LEFT
        {
            get { return FlxG.keys.A || FlxG.keys.LEFT || FlxG.gamepads.isButtonDown(Buttons.DPadLeft) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft); }
        }

        public bool LEFTJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.A) || FlxG.keys.justPressed(Keys.Left) || FlxG.gamepads.isNewButtonPress(Buttons.DPadLeft) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickLeft); }
        }

        public bool RIGHT
        {
            get { return FlxG.keys.D || FlxG.keys.RIGHT || FlxG.gamepads.isButtonDown(Buttons.DPadRight) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight); }
        }

        public bool RIGHTJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.D) || FlxG.keys.justPressed(Keys.Right) || FlxG.gamepads.isNewButtonPress(Buttons.DPadRight) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickRight); }
        }

        public bool UP
        {
            get { return FlxG.keys.W || FlxG.keys.UP || FlxG.gamepads.isButtonDown(Buttons.DPadUp) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp); }
        }

        public bool UPJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.W) || FlxG.keys.justPressed(Keys.Up) || FlxG.gamepads.isNewButtonPress(Buttons.DPadUp) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickUp); }
        }

        public bool DOWN
        {
            get { return FlxG.keys.S || FlxG.keys.DOWN || FlxG.gamepads.isButtonDown(Buttons.DPadDown) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown); }
        }

        public bool DOWNJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.S) || FlxG.keys.justPressed(Keys.Down) || FlxG.gamepads.isNewButtonPress(Buttons.DPadDown) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickDown); }
        }

        public bool ACTION
        {
            get { return FlxG.keys.N || 
                FlxG.keys.X || 
                FlxG.keys.ENTER || 
                FlxG.keys.SPACE || 
                FlxG.gamepads.isButtonDown(Buttons.A) || 
                FlxG.gamepads.isButtonDown(Buttons.Start); }
        }

        public bool ACTIONJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.N) ||
                FlxG.keys.justPressed(Keys.X) || 
                FlxG.keys.justPressed(Keys.Enter) ||
                FlxG.keys.justPressed(Keys.Space) || 
                FlxG.gamepads.isNewButtonPress(Buttons.A) || 
                FlxG.gamepads.isNewButtonPress(Buttons.Start); }
        }

        public bool CANCEL
        {
            get { return FlxG.keys.ESCAPE || 
                FlxG.gamepads.isButtonDown(Buttons.Back) || 
                FlxG.gamepads.isButtonDown(Buttons.B); }
        }

        public bool CANCELJUSTPRESSED
        {
            get { return FlxG.keys.justPressed(Keys.Escape) || 
                FlxG.gamepads.isNewButtonPress(Buttons.Back) || 
                FlxG.gamepads.isNewButtonPress(Buttons.B); }
        }


    }
}

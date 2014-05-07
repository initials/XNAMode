using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.Diagnostics;

using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    /// <summary>
    /// 
    /// </summary>
    public class DataEntryState : FlxState
    {
        FlxText _nameEntry;
        FlxButton play;

        //PlayHud localHud;

        override public void create()
        {
            FlxG.backColor = Color.Black;


            /// why was there two?
            //base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
            
            //FlxG.backColor = new Color(0xc2, 0x88, 0x83);
            //two base creates?
            base.create();

            FlxG.resetHud();
            FlxG.showHud();

            _nameEntry = new FlxText(30, 60, FlxG.width);
            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            _nameEntry.text = "";
            add(_nameEntry);

            try
            {
                _nameEntry.text = LoadFromDevice();
            }
            catch
            {
                Console.WriteLine("Cannot load name from file");
            }

            FlxG.showHud();
            FlxG.setHudText(1, "Enter name, use @ symbol to specify Twitter handle.\nPress enter when complete.");
            FlxG.setHudTextScale(1, 2);
            FlxG.setHudTextScale(3, 2);
            FlxG.setHudTextPosition(1, 30, 40);
            FlxG.setHudTextPosition(3, 10, 20);

            play = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 30, advanceToNextState , FlxButton.ControlPadA);
            //play = new FlxButton(0, FlxG.height - 30, advanceToNextState, FlxButton.ControlPadA);

            play.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            
            play.loadText(new FlxText(2, 2, 100, "Enter"), new FlxText(2, 2, 100, "ENTER"));
            add(play);

            FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 27);

            FlxG.flash.start(Color.Black, 1.5f);

        }

        public void SaveToDevice()
        {
            // Compose a string that consists of three lines.
            string lines = _nameEntry.text;

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("nameinfo.txt");
            file.WriteLine(lines);

            file.Close();
        }

        public string LoadFromDevice()
        {
            string value1 = File.ReadAllText("nameinfo.txt");

            //Console.WriteLine("--- Contents of file.txt: ---");
            //Console.WriteLine(value1);

            return value1.Substring(0, value1.Length - 1) ;


        }

        override public void update()
        {
            PlayerIndex pi;
            if (FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi))
            {
                FlxG.joystickBeingUsed = true;
            }

            keyboardEntry();

            base.update();
        }

        public void advanceToNextState2()
        {
            Console.WriteLine("Advance to next state");
        }

        public void advanceToNextState()
        {
            Console.WriteLine("Advance to next state");

            if (_nameEntry.text == "")
            {
                FlxG.setHudText(2, "Name cannot be blank");

                return;
            }

            FlxG.username = _nameEntry.text;

            SaveToDevice();
            
            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            FlxG.state = new GameSelectionMenuState();
            return;
        }

        public void keyboardEntry()
        {
            PlayerIndex pi;
            bool shift = FlxG.keys.SHIFT;

            if (FlxG.keys.isNewKeyPress(Keys.A, null, out pi)) _nameEntry.text += shift ? "A" : "a";
            if (FlxG.keys.isNewKeyPress(Keys.B, null, out pi)) _nameEntry.text += shift ? "B" : "b";
            if (FlxG.keys.isNewKeyPress(Keys.C, null, out pi)) _nameEntry.text += shift ? "C" : "c";
            if (FlxG.keys.isNewKeyPress(Keys.D, null, out pi)) _nameEntry.text += shift ? "D" : "d";
            if (FlxG.keys.isNewKeyPress(Keys.E, null, out pi)) _nameEntry.text += shift ? "E" : "e";
            if (FlxG.keys.isNewKeyPress(Keys.F, null, out pi)) _nameEntry.text += shift ? "F" : "f";
            if (FlxG.keys.isNewKeyPress(Keys.G, null, out pi)) _nameEntry.text += shift ? "G" : "g";
            if (FlxG.keys.isNewKeyPress(Keys.H, null, out pi)) _nameEntry.text += shift ? "H" : "h";
            if (FlxG.keys.isNewKeyPress(Keys.I, null, out pi)) _nameEntry.text += shift ? "I" : "i";
            if (FlxG.keys.isNewKeyPress(Keys.J, null, out pi)) _nameEntry.text += shift ? "J" : "j";
            if (FlxG.keys.isNewKeyPress(Keys.K, null, out pi)) _nameEntry.text += shift ? "K" : "k";
            if (FlxG.keys.isNewKeyPress(Keys.L, null, out pi)) _nameEntry.text += shift ? "L" : "l";
            if (FlxG.keys.isNewKeyPress(Keys.M, null, out pi)) _nameEntry.text += shift ? "M" : "m";
            if (FlxG.keys.isNewKeyPress(Keys.N, null, out pi)) _nameEntry.text += shift ? "N" : "n";
            if (FlxG.keys.isNewKeyPress(Keys.O, null, out pi)) _nameEntry.text += shift ? "O" : "o";
            if (FlxG.keys.isNewKeyPress(Keys.P, null, out pi)) _nameEntry.text += shift ? "P" : "p";
            if (FlxG.keys.isNewKeyPress(Keys.Q, null, out pi)) _nameEntry.text += shift ? "Q" : "q";
            if (FlxG.keys.isNewKeyPress(Keys.R, null, out pi)) _nameEntry.text += shift ? "R" : "r";
            if (FlxG.keys.isNewKeyPress(Keys.S, null, out pi)) _nameEntry.text += shift ? "S" : "s";
            if (FlxG.keys.isNewKeyPress(Keys.T, null, out pi)) _nameEntry.text += shift ? "T" : "t";
            if (FlxG.keys.isNewKeyPress(Keys.U, null, out pi)) _nameEntry.text += shift ? "U" : "u";
            if (FlxG.keys.isNewKeyPress(Keys.V, null, out pi)) _nameEntry.text += shift ? "V" : "v";
            if (FlxG.keys.isNewKeyPress(Keys.W, null, out pi)) _nameEntry.text += shift ? "W" : "w";
            if (FlxG.keys.isNewKeyPress(Keys.X, null, out pi)) _nameEntry.text += shift ? "X" : "x";
            if (FlxG.keys.isNewKeyPress(Keys.Y, null, out pi)) _nameEntry.text += shift ? "Y" : "y";
            if (FlxG.keys.isNewKeyPress(Keys.Z, null, out pi)) _nameEntry.text += shift ? "Z" : "z";

            if (FlxG.keys.isNewKeyPress(Keys.D1, null, out pi)) _nameEntry.text += shift ? "!" : "1";
            if (FlxG.keys.isNewKeyPress(Keys.D2, null, out pi)) _nameEntry.text += shift ? "@" : "2";
            if (FlxG.keys.isNewKeyPress(Keys.D3, null, out pi)) _nameEntry.text += shift ? "#" : "3";
            if (FlxG.keys.isNewKeyPress(Keys.D4, null, out pi)) _nameEntry.text += shift ? "$" : "4";
            if (FlxG.keys.isNewKeyPress(Keys.D5, null, out pi)) _nameEntry.text += shift ? "%" : "5";
            if (FlxG.keys.isNewKeyPress(Keys.D6, null, out pi)) _nameEntry.text += shift ? "^" : "6";
            if (FlxG.keys.isNewKeyPress(Keys.D7, null, out pi)) _nameEntry.text += shift ? "&" : "7";
            if (FlxG.keys.isNewKeyPress(Keys.D8, null, out pi)) _nameEntry.text += shift ? "*" : "8";
            if (FlxG.keys.isNewKeyPress(Keys.D9, null, out pi)) _nameEntry.text += shift ? "(" : "9";
            if (FlxG.keys.isNewKeyPress(Keys.D0, null, out pi)) _nameEntry.text += shift ? ")" : "0";
            if (FlxG.keys.isNewKeyPress(Keys.OemMinus, null, out pi)) _nameEntry.text += shift ? "_" : "-";

            if (FlxG.keys.isNewKeyPress(Keys.Back, null, out pi))
            {

                if (_nameEntry.text.Length <= 0)
                {
                    return;
                }

                string backspaced = _nameEntry.text;

                _nameEntry.text = backspaced.Remove(backspaced.Length - 1);
            }

            if (FlxG.keys.isNewKeyPress(Keys.Enter, null, out pi))
            {
                advanceToNextState();
            }

        }
    }
}

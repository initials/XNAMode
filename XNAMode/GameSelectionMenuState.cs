﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;
using Microsoft.Xna.Framework.GamerServices;

using System.Linq;
using System.Xml.Linq;

using XNATweener;

namespace XNAMode
{
    public class GameSelectionMenuState : FlxState
    {

        FlxText _menuItems;

        FlxText _nameEntry;

        FlxSave save;
        bool hasCheckedSave;

        //FlxTransition transition;

        FlxButton play;

        FlxSprite bgSprite;

        private Tweener tween;



        override public void create()
        {

            FlxG.backColor = new Color(0xc2, 0x88, 0x83);

            base.create();

            FlxG.resetHud();
            FlxG.showHud();

            //FlxTileblock bg = new FlxTileblock(0, FlxG.height - 256, 256 * 3, 256);
            //bg.loadTiles(FlxG.Content.Load<Texture2D>("initials/Ambience"), 256, 256, 0);
            //add(bg);


            tween = new Tweener(0, -240 , TimeSpan.FromSeconds(3.9f), Quadratic.EaseOut);

            // -350, -310
            bgSprite = new FlxSprite(-350, 0, FlxG.Content.Load<Texture2D>("initials/Fear"));
            add(bgSprite);


            hasCheckedSave = false;

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));
            
            _menuItems = new FlxText(10, 10, FlxG.width);
            _menuItems.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            _menuItems.text = "Homewreckr\n\nEnter name, use @ symbol to specify Twitter handle.\nPress enter when complete.";
            add(_menuItems);


            save = new FlxSave();

            _nameEntry = new FlxText(10, 100, FlxG.width);
            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            _nameEntry.text = "";
            add(_nameEntry);

            play = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 30, playGame, FlxButton.ControlPadA);
            play.loadGraphic((new FlxSprite()).createGraphic(100, 20, new Color(0xe4, 0xb4, 0x8a)), (new FlxSprite()).createGraphic(102, 22, new Color(0xdd, 0xa1, 0x6d)));
            play.loadText(new FlxText(2, 2, 100, "Play Game"), new FlxText(2, 2, 100, "WRECK HOMEZ"));
            add(play);

            FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54 , FlxG.height - 27);

            FlxG.flash.start(Color.Black, 1.5f);
        }

        override public void update()
        {
            tween.Update(FlxG.elapsedAsGameTime);
            bgSprite.y = tween.Position;



            if (FlxG.keys.F1)
            {
                FlxG.state = new MenuState();

            }
            if (FlxG.keys.F2)
            {
                FlxG.state = new CaveState();

            }

            if (FlxG.keys.F3)
            {
                FlxG.state = new CutsceneState();

            }

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


            if (!hasCheckedSave)
            {

                if (save.waitingOnDeviceSelector)
                {
                    return;
                }
                else if (save.canSave)
                {
                    hasCheckedSave = true;
                    if (save.bind("Mode"))
                    {
                        if (save.data["player_name"] == null)
                            _nameEntry.text = "";
                        else
                            _nameEntry.text = save.data["player_name"];
                        FlxG.log("Player name is: " + save.data["player_name"]);
                        FlxG.username = save.data["player_name"];
                        save.forceSave(0);
                    }
                }
            }
            if (FlxG.keys.isNewKeyPress(Keys.Enter, null, out pi))
            {
                playGame();
            }

            if (FlxG.transition.complete)
            {
                FlxG.state = new BasePlayState();
            }


            base.update();

            if (FlxG.keys.justPressed(Keys.F3))
            {
                FlxG.bloom.Visible = !FlxG.bloom.Visible;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        public void playGame()
        {
            FlxG.level = 1;
            FlxG.hideHud();


            if (_nameEntry.text == "")
            {
                _menuItems.text = "Name cannot be blank";

                return;
            }
            if (save.waitingOnDeviceSelector)
            {
                return;
            }
            else if (save.canSave)
            {
                if (save.bind("Mode"))
                {
                    save.data["player_name"] = _nameEntry.text;
                    FlxG.log("Player name is: " + save.data["player_name"]);
                    FlxG.username = save.data["player_name"];

                    save.forceSave(0);
                }
            }

            FlxG.transition.startFadeOut(0.1f,0,120);
        }

    }
        
}

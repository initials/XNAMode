using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using XNATweener;
using System.IO;

namespace FourChambers
{
    public class GameSelectionMenuState : FlxState
    {

        private FlxText _menuItems;

        private FlxButton play;
        private FlxButton playProcedural;
        private FlxButton editName;

        private FlxSprite bgSprite;

        private Tweener tween;

        override public void create()
        {

            FlxG.playMusic("music/" + FourChambers_Globals.MUSIC_MENU, 1.0f);

            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1]; //6 is the super bright.

            FlxG.backColor = new Color(0xc2, 0x88, 0x83);

            base.create();

            FlxG._game.hud.hudGroup = new FlxGroup();

            FlxG.resetHud();
            FlxG.showHud();

            tween = new Tweener(0, -240 , TimeSpan.FromSeconds(3.9f), Quadratic.EaseOut);

            // -350, -310
            bgSprite = new FlxSprite(-350, 0, FlxG.Content.Load<Texture2D>("fourchambers/Fear"));
            add(bgSprite);

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
            FlxG.mouse.cursor.offset.X = 5;
            FlxG.mouse.cursor.offset.Y = 5;


            
            _menuItems = new FlxText(0, 10, FlxG.width);
            _menuItems.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, Color.White, FlxJustification.Center, Color.White);
            //_menuItems.text = "Four Chambers\n\nEnter name, use @ symbol to specify Twitter handle.\nPress enter when complete.";
            _menuItems.text = "The Four\nChambers Of The\nHuman Heart";
            _menuItems.shadow = Color.Black;
            add(_menuItems);

            //_nameEntry = new FlxText(10, FlxG.height, FlxG.width);
            //_nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            //_nameEntry.text = "Username";
            //add(_nameEntry);

            play = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 80, playGame, FlxButton.ControlPadA);
            play.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false,false,100,20));
            play.loadText(new FlxText(2, 2, 100, "Play Game"), new FlxText(2, 2, 100, "Play Game!"));
            add(play);
            play.on = true;
            play.debugName = "playGame";

            playProcedural = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 55, playGameTutorial, FlxButton.ControlPadA);
            playProcedural.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            playProcedural.loadText(new FlxText(2, 2, 100, "Tutorial"), new FlxText(2, 2, 100, "Tutorial"));
            add(playProcedural);
            playProcedural.debugName = "tutorial";

            editName = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 30, goToDataEntryState, FlxButton.ControlPadA);
            editName.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            editName.loadText(new FlxText(2, 2, 100, "Edit Name"), new FlxText(2, 2, 100, "Edit Name"));
            add(editName);
            editName.debugName = "editName";



            FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54 , FlxG.height - 80);

            FlxG.flash.start(Color.Black, 1.5f);

            FlxG.color(Color.White);

            try
            {
                FlxG.username = LoadFromDevice();
            }
            catch
            {
                Console.WriteLine("Cannot load name from file");
            }

            if (FlxG.username != "")
            {
                //_nameEntry.text = FlxG.username;
                FlxG.setHudText(3, "Username:\n"+FlxG.username);
                FlxG.setHudTextPosition(3, 50, FlxG.height - 30);
                FlxG.setHudTextScale(3, 2);



            }

        }

        public string LoadFromDevice()
        {
            string value1 = File.ReadAllText("nameinfo.txt");
            return value1.Substring(0, value1.Length - 1);
        }

        public void setAllButtonsToOff()
        {
            play.on = false;
            playProcedural.on = false;
            editName.on = false;
        }

        override public void update()
        {


            if (FlxG.gamepads.isNewButtonPress(Buttons.DPadUp) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickUp ))
            {

                if (play.on)
                {
                    setAllButtonsToOff();
                    editName.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 27);
                }
                else if (playProcedural.on)
                {
                    setAllButtonsToOff();
                    play.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 80);
                }
                else if (editName.on)
                {
                    setAllButtonsToOff();
                    playProcedural.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 53);
                }
            }
            if (FlxG.gamepads.isNewButtonPress(Buttons.DPadDown) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickDown))
            {

                if (play.on)
                {
                    setAllButtonsToOff();
                    playProcedural.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 53);
                }
                else if (playProcedural.on)
                {
                    setAllButtonsToOff();
                    editName.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 27);
                }
                else if (editName.on)
                {
                    setAllButtonsToOff();
                    play.on = true;
                    FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54, FlxG.height - 80);
                }
            }
            if (FlxG.gamepads.isNewButtonRelease(Buttons.A) && play._counter > 0.5f)
            {

                if (play.on)
                {
                    playGame();
                }
                else if (playProcedural.on)
                {
                    playGameTutorial();
                }
                else if (editName.on)
                {
                    goToDataEntryState();
                }
            }


            PlayerIndex pi;
            if (FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi))
            {
                FlxG.joystickBeingUsed = true;
            }


            tween.Update(FlxG.elapsedAsGameTime);
            bgSprite.y = tween.Position;



            if (FlxG.keys.F1)
            {
                FlxG.state = new XNAMode.MenuState();

            }
            if (FlxG.keys.F2)
            {
                FlxG.state = new CaveState();

            }

            if (FlxG.keys.F3)
            {
                FlxG.state = new CutsceneState();

            }

            if (FlxG.keys.F4)
            {
                FlxG.state = new EmptyIntroTestState();

            }

            base.update();

            if (FlxG.keys.justPressed(Keys.F9))
            {
                FlxG.bloom.Visible = !FlxG.bloom.Visible;
            }

            if (FlxG.username == "" || FlxG.username==null)
            {
                FlxG.state = new DataEntryState();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void playGame()
        {
            Console.WriteLine("Play Game");

            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            play = null;

            if (FlxG.debug == false)
            {
                FourChambers_Globals.startGame();
            }

            FlxG.state = new BasePlayStateFromOel();
            return;

        }
        //playGameTutorial
        public void playGameTutorial()
        {
            Console.WriteLine("Play Tutorial");

            FlxG.level = -1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            FlxG.state = new BasePlayStateFromOelTutorial();

        }

        public void playGameProcedural()
        {
            Console.WriteLine("Play Game Proc");

            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            FlxG.state = new FourChambers.BasePlayState();

        }
        public void goToDataEntryState()
        {
            FlxG.transition.resetAndStop();
            FlxG.state = new FourChambers.DataEntryState();
        }
    }
        
}

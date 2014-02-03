using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

namespace org.flixel
{
    /// <summary>
    /// Recording style.
    /// </summary>
    public enum Recording
    {
        None = 0,
        RecordingController = 1,
        RecordingPosition = 2,
        Playback = 3,
        PlaybackReverse = 4
    }

    public enum ButtonMap
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        A = 4,
        B = 5,
        X = 6,
        Y = 7,
        LeftShoulder = 8,
        LeftTrigger = 9,
        RightShoulder = 10,
        RightTrigger = 11
    }

    /// <summary>
    /// The main "game object" class, handles basic physics and animation.
    /// </summary>
    public class FlxRecord : FlxGroup
    {

        private List<bool[]> _history = new List<bool[]>();

        private Recording _rec = Recording.None;

        public PlayerIndex? controller;


        private FlxButton openBtn;
        private FlxButton pauseBtn;
        private FlxButton playBtn;
        private FlxButton recordBtn;
        private FlxButton restartBtn;
        private FlxButton stepBtn;
        private FlxButton stopBtn;

        private FlxText infoText;

        public FlxRecord()
        {
            openBtn = new FlxButton(110, 110, openRecording);
            openBtn.scrollFactor.X = 0;
            openBtn.scrollFactor.Y = 0;
            openBtn.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("flixel/vcr/open"), false, false, 11, 11), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("flixel/vcr/open"), false, false, 11, 11));

            pauseBtn = new FlxButton(110, 130, pause);
            pauseBtn.scrollFactor.X = 1;
            pauseBtn.scrollFactor.Y = 1;
            pauseBtn.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("flixel/vcr/pause"), false, false, 11, 11), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("flixel/vcr/pause"), false, false, 11, 11));
            pauseBtn.x = 100;


        }

        public override void render(SpriteBatch spriteBatch)
        {
            openBtn.render(spriteBatch);
            pauseBtn.render(spriteBatch);
            base.render(spriteBatch);
        }
        
        public override void update()
        {
            PlayerIndex pi;

            if (_rec == Recording.RecordingController)
            {

                _history.Add(new bool[] { 
                    (FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.DPadUp, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp)), //0
                    (FlxG.keys.D || FlxG.gamepads.isButtonDown(Buttons.DPadRight, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight)), //1
                    (FlxG.keys.S || FlxG.gamepads.isButtonDown(Buttons.DPadDown, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown)), //2
                    (FlxG.keys.A || FlxG.gamepads.isButtonDown(Buttons.DPadLeft, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft)), //3
                    (FlxG.keys.M || FlxG.gamepads.isButtonDown(Buttons.A, controller, out pi)), //4
                    (FlxG.keys.N || FlxG.gamepads.isButtonDown(Buttons.B, controller, out pi)), //5
                    (FlxG.keys.J || FlxG.gamepads.isNewButtonPress(Buttons.X, controller, out pi)), //6
                    (FlxG.keys.K || FlxG.gamepads.isButtonDown(Buttons.Y, controller, out pi)), //7
                    (FlxG.keys.H || FlxG.gamepads.isButtonDown(Buttons.LeftShoulder, controller, out pi)), //8
                    (FlxG.keys.U || FlxG.gamepads.isButtonDown(Buttons.LeftTrigger, controller, out pi)), //9
                    (FlxG.keys.L || FlxG.gamepads.isButtonDown(Buttons.RightShoulder, controller, out pi)), //10
                    (FlxG.keys.I || FlxG.gamepads.isButtonDown(Buttons.RightTrigger, controller, out pi)) //11
                });


            }

        }
        public void pause()
        {

        }

        public void openRecording()
        {

        }

        public void saveRecording()
        {
            string _historyString = "";
            foreach (var item in _history)
            {
                _historyString += item[0].ToString() + "," + item[1].ToString() + "," + item[2].ToString() + "," + item[3].ToString() + "," +
                    item[4].ToString() + "," + item[5].ToString() + "," + item[6].ToString() + "," + item[7].ToString() + "," +
                    item[8].ToString() + "," + item[9].ToString() + "," + item[10].ToString() + "," + item[11].ToString() + "\n";
            }

            FlxU.saveToDevice(_historyString, ("Revvolvver/Level" + FlxG.level.ToString() + "_" + controller.ToString() + "PlayerData.txt"));


        }
    }
}

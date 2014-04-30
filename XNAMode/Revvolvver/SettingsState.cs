using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if !WINDOWS_PHONE
using Microsoft.Xna.Framework.Storage;
#endif
using org.flixel;

namespace Revvolvver
{
    /// <summary>
    /// The main menu for the game Mode.
    /// </summary>
    public class SettingsState : FlxState
    {
        //private static bool _alreadySaved = false;

        private Texture2D ImgGibs;
        private Texture2D ImgCursor;

        private FlxEmitter _gibs;
        //private bool _ok2;
        private FlxGroup textGrp;

        //private int frameCount;
        //private int clickCount;

        private const string SndClick = "Revvolvver/sfx/gunclick";
        private const string SndGun1 = "Revvolvver/sfx/gunshot1";
        private const string SndGun2 = "Revvolvver/sfx/gunshot2";
        private const string SndChord1 = "Revvolvver/sfx/chord_01";
        private const string SndChord2 = "Revvolvver/sfx/chord_02";
        private const string SndChord3 = "Revvolvver/sfx/chord_03";
        private const string SndChord4 = "Revvolvver/sfx/chord_04";
        private const string SndChord5 = "Revvolvver/sfx/chord_05";
        private const string SndChord6 = "Revvolvver/sfx/chord_06";
        private const string SndChord7 = "Revvolvver/sfx/chord_07";

        //private string[] Snds;

        private const string Music = "Revvolvver/sfx/asong";

        //private FlxSound SndFlxClick;

        private FlxSprite flower;

        private FlxText playersText;
        //private FlxText playersTextx;

		private int currentTextSelected = 10;

        public float timer;

        override public void create()
        {

            FlxG.bloom.Visible = false;

            FlxG.backColor = new Color(0xaf, 0xe0, 0xe4);

            base.create();

            flower = new FlxSprite(FlxG.width / 2 - 290 / 2, FlxG.height / 2 - 290 / 2 + 60);
            flower.loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/flower"), false, false, 290, 290);
            add(flower);
            flower.visible = false;
            flower.alpha = 0.1f;

            flower.angularDrag = 1500;

			string level = "Revvolvver/titlescreenOUYA.oel";

			#if __ANDROID__

			level = "Revvolvver/titlescreenOUYA.oel";

			#endif

            Dictionary<string, string> attrs = new Dictionary<string, string>();
			attrs = FlxXMLReader.readAttributesFromOelFile(level, "level/NonDestructable");
            FlxTilemap _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 21, 21);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            add(_tileMap);

            FlxG.hideHud();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgCursor = FlxG.Content.Load<Texture2D>("initials/crosshair");

            _gibs = new FlxEmitter(0, FlxG.height - 30);
            _gibs.setSize(FlxG.width, 30);
            _gibs.setYSpeed(-1400, -1020);
            _gibs.setRotation(-720, 720);
            _gibs.gravity = 50;
            _gibs.createSprites(ImgGibs, 1000);
            add(_gibs);


			textGrp = new FlxGroup();
			int i = 0;
			for ( i=0; i < Revvolvver_Globals.GameSettings.Length; i++)
			{
				//string value = Revvolvver_Globals.GameSettings[i].Name + ": " + Revvolvver_Globals.GameSettings[i].GameValue;




				playersText = new FlxText(280, 140 + (i * 20), FlxG.width, findMenuString(i));
				playersText.alignment = FlxJustification.Left;
				playersText.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xff, 0x6e, 0x55), FlxJustification.Left, new Color(0xff, 0x6e, 0x55));
				playersText.shadow = new Color(0xff, 0x6e, 0x55);
				playersText.scale = 1; // size = 32
				playersText.color = new Color(0xff, 0x6e, 0x55);
				playersText.antialiasing = false;
				textGrp.add(playersText);
				if (i == 0) playersText.color = Color.Red;

			}

			add(textGrp);

            timer = 0.0f;


            if (Revvolvver_Globals.GAMES_PLAYS_ITSELF)
            {

                for (int i1 = 0; i1 < textGrp.members.Count; i++)
                {
                    Revvolvver_Globals.GameSettings[i1].GameValue = (int)FlxU.random(Revvolvver_Globals.GameSettings[i1].MinAmount, Revvolvver_Globals.GameSettings[i1].MaxAmount);
                    string value = findMenuString(i1);
                    ((FlxText)(textGrp.members[i1])).text = value;
                }

                FlxG.play(SndGun2, 0.35f);
                FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onFade, false);
                return;

            }

        }

        public string findMenuString(int i)
        {
            string value = "";

			if (Revvolvver_Globals.GameSettings[i].Name == "Play Now" || Revvolvver_Globals.GameSettings[i].Name == "Randomonium" || Revvolvver_Globals.GameSettings[i].Name == "< Presets >")
                value = Revvolvver_Globals.GameSettings[i].Name;
            else
                value = Revvolvver_Globals.GameSettings[i].Name + ": " + Revvolvver_Globals.GameSettings[i].GameValue.ToString();

            return value;
        }

        override public void update()
        {
            

            base.update();

            if (FlxG.keys.justPressed(Keys.Up) || FlxG.gamepads.isNewButtonPress(Buttons.DPadUp) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickUp))
            {
                currentTextSelected--;
                if (currentTextSelected < 0) currentTextSelected = textGrp.members.Count - 1;
            }
            if (FlxG.keys.justPressed(Keys.Down) || FlxG.gamepads.isNewButtonPress(Buttons.DPadDown) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickDown))
            {
                currentTextSelected++;
                if (currentTextSelected > textGrp.members.Count - 1) currentTextSelected = 0;
            }

            if (FlxG.keys.justPressed(Keys.Right) || FlxG.gamepads.isNewButtonPress(Buttons.DPadRight) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickRight) || FlxG.gamepads.isButtonDown(Buttons.RightTrigger))
            {
                Revvolvver_Globals.GameSettings[currentTextSelected].GameValue += Revvolvver_Globals.GameSettings[currentTextSelected].Increment;

                if (Revvolvver_Globals.GameSettings[currentTextSelected].GameValue > Revvolvver_Globals.GameSettings[currentTextSelected].MaxAmount)
                {
                    Revvolvver_Globals.GameSettings[currentTextSelected].GameValue = Revvolvver_Globals.GameSettings[currentTextSelected].MaxAmount;
                }
                string value = findMenuString(currentTextSelected);

                ((FlxText)(textGrp.members[currentTextSelected])).text = value;

                
            }
            else if (FlxG.keys.justPressed(Keys.Left) || FlxG.gamepads.isNewButtonPress(Buttons.DPadLeft) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickLeft) || FlxG.gamepads.isButtonDown(Buttons.LeftTrigger) )
            {
                Revvolvver_Globals.GameSettings[currentTextSelected].GameValue -= Revvolvver_Globals.GameSettings[currentTextSelected].Increment;

                if (Revvolvver_Globals.GameSettings[currentTextSelected].GameValue < Revvolvver_Globals.GameSettings[currentTextSelected].MinAmount)
                {
                    Revvolvver_Globals.GameSettings[currentTextSelected].GameValue = Revvolvver_Globals.GameSettings[currentTextSelected].MinAmount;
                }

                string value = findMenuString(currentTextSelected);
                ((FlxText)(textGrp.members[currentTextSelected])).text = value;

                
            }
            else
            {
                timer = 0.0f;
            }

            timer += FlxG.elapsed;

            for (int i = 0; i < textGrp.members.Count; i++)
            {
                ((FlxText)(textGrp.members[i])).color = Color.White; ;
            }

            ((FlxText)(textGrp.members[currentTextSelected])).color = Color.Red;

			if (FlxG.keys.justPressed(Keys.Enter) || FlxG.gamepads.isNewButtonPress(Buttons.Start) || FlxG.gamepads.isNewButtonPress(Buttons.A) || FlxG.gamepads.isButtonDown(Buttons.A))
            {

                if (Revvolvver_Globals.GameSettings[currentTextSelected].Name == "Play Now")
                {
					//Revvolvver_Globals.GameSettings [currentTextSelected].Name = "Play Now!!!";

					FlxG.play(SndGun2, 0.35f);
                    FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onFade, false);
                    return;
                }
                else if (Revvolvver_Globals.GameSettings[currentTextSelected].Name == "Randomonium")
                {
                    for (int i = 0; i < textGrp.members.Count; i++)
                    {
                        Revvolvver_Globals.GameSettings[i].GameValue = (int)FlxU.random(Revvolvver_Globals.GameSettings[i].MinAmount, Revvolvver_Globals.GameSettings[i].MaxAmount);
                        string value = findMenuString(i);
                        ((FlxText)(textGrp.members[i])).text = value;
                    }
                }
                else
                {
                    Revvolvver_Globals.GameSettings[currentTextSelected].GameValue = Revvolvver_Globals.GameSettings[currentTextSelected].DefaultAmount;
                    string value = findMenuString(currentTextSelected);
                    ((FlxText)(textGrp.members[currentTextSelected])).text = value;

                }


            }


			if (FlxG.gamepads.isNewButtonPress (Buttons.Y)) {

				for (int i = 0; i < textGrp.members.Count; i++) {
					Revvolvver_Globals.GameSettings[i].GameValue = Revvolvver_Globals.GameSettings[i].DefaultAmount;
					string value = findMenuString(i);
					((FlxText)(textGrp.members[i])).text = value;
				}
					


			}

			if (FlxG.keys.justPressed(Keys.Escape) || FlxG.gamepads.isNewButtonPress(Buttons.RightStick) )
            {
                FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onExitFade, false);
                return;
            }


        }

        private void onExitFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new MenuState();

        }

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new PlayStateMulti();
            
        }

    }
}

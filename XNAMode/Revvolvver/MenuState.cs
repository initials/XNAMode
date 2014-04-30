using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#if !WINDOWS_PHONE
using Microsoft.Xna.Framework.Storage;
#endif
using org.flixel;
using Microsoft.Xna.Framework.Media;

namespace Revvolvver
{
    /// <summary>
    /// The main menu for the game Mode.
    /// </summary>
    public class MenuState : FlxState
    {
        //private static bool _alreadySaved = false;

        private Texture2D ImgGibs;
        private Texture2D ImgCursor;

        private FlxEmitter _gibs;
        //private bool _ok2 = false;
        private FlxGroup letters;

        private int frameCount;
        private int clickCount;

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

        private string[] Snds;

        private const string Music = "Revvolvver/sfx/asong";

        //private FlxSound SndFlxClick;

        private FlxSprite flower;

        private FlxText playersText;
        private FlxText playersTextx;

        private bool _fading;

        //private FlxText credits;

        //#if !WINDOWS_PHONE
        //        FlxSave save;
        //        private bool hasCheckedSaveFile = false;
        //#endif

        override public void create()
        {
            _fading = false;
            FlxG.bloom.Visible = false;

            Snds = new string[8] { SndClick, SndChord1, SndChord2, SndChord3, SndChord4, SndChord5, SndChord6, SndChord7};
            
			//FlxG.playMusic(Music, 1.0f);

			

			//if (FlxG.music.playing)
			//    FlxG.music.stop();

            //afe0e4
            //FlxG.backColor = new Color(0xdb, 0xd8, 0xac);
            FlxG.backColor = new Color(0xaf, 0xe0, 0xe4);


            base.create();

			FlxG.autoHandlePause = false;

            flower = new FlxSprite(FlxG.width / 2 - 290 / 2, FlxG.height / 2 - 290 / 2 + 60);
            flower.loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/flower"), false, false, 290, 290);
            add(flower);
            flower.visible = false;
            flower.alpha = 0.1f;

            flower.angularDrag = 1500;


            Dictionary<string, string> attrs = new Dictionary<string, string>();
			string level = "Revvolvver/titlescreen.oel";

			#if __ANDROID__

            FlxG.playAndroidMusic("music/Intro", 0.5f);

			level = "Revvolvver/titlescreenOUYA.oel";

#endif

            level = "Revvolvver/titlescreenOUYA.oel";

			attrs = FlxXMLReader.readAttributesFromOelFile(level, "level/NonDestructable");
            FlxTilemap _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 21, 21);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            add(_tileMap);

            Dictionary<string, string> attrs2 = new Dictionary<string, string>();
			attrs2 = FlxXMLReader.readAttributesFromOelFile(level, "level/Destructable");
            FlxTilemap _tileMap2 = new FlxTilemap();
            _tileMap2.auto = FlxTilemap.STRING;
            _tileMap2.loadMap(attrs2["Destructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 21, 21);
            _tileMap2.collideMin = 1;
            _tileMap2.collideMax = 21;
            add(_tileMap2);


            FlxG.hideHud();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgCursor = FlxG.Content.Load<Texture2D>("initials/crosshair");

            _gibs = new FlxEmitter(0, FlxG.height  - 30);
            _gibs.setSize(FlxG.width, 30);
            _gibs.setYSpeed(-1400, -1020);
            _gibs.setRotation(-720, 720);
            _gibs.gravity = 50;
            _gibs.createSprites(ImgGibs, 1000);
            add(_gibs);

            letters = new FlxGroup();

			string title = "REVVOLVVER";
			int count = 0;
			foreach (char c in title)
			{
				createLetter(c, (FlxG.width/2 - 250) + count * 50, 80);

				count++;
			}

			add(letters);

			frameCount = 1;

			//SndFlxClick = new FlxSound();


			playersText = new FlxText(0, FlxG.height/2, FlxG.width, "");
			playersText.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xff, 0x6e, 0x55), FlxJustification.Center, new Color(0xff, 0x6e, 0x55));
			playersText.shadow = new Color(0xff, 0x6e, 0x55);
			playersText.scale = 1; // size = 32
			playersText.color = new Color(0xff, 0x6e, 0x55);
			playersText.antialiasing = false;
			add(playersText);

			if (Revvolvver_Globals.PLAYERS==1) playersText.text = "<- Players: 1 ->";
			else if (Revvolvver_Globals.PLAYERS == 2) playersText.text = "<- Players: 2 ->";
			else if (Revvolvver_Globals.PLAYERS == 3) playersText.text = "<- Players: 3 ->";
			else if (Revvolvver_Globals.PLAYERS == 4) playersText.text = "<- Players: 4 ->";

            string instruct = "Press Start or Enter to begin the shootout";

			#if __ANDROID__
			instruct = "Press O to begin the shootout";
			#endif

			//instruct = "Press O to begin the shootout";

			playersTextx = new FlxText(0, FlxG.height / 2 + 100, FlxG.width, instruct);
			playersTextx.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xff, 0x6e, 0x55), FlxJustification.Center, new Color(0xff, 0x6e, 0x55));
			playersTextx.shadow = new Color(0xff, 0x6e, 0x55);
			playersTextx.scale = 1; // size = 32
			playersTextx.color = new Color(0xff, 0x6e, 0x55);
			playersTextx.antialiasing = false;
			add(playersTextx);

			playersText.visible = false;
			playersTextx.visible = false;

            //credits = new FlxText(FlxG.width, FlxG.height/2 + 140, FlxG.width, "Revvolvver is a game by Initials, Art by Cellusious, Additional Game Design by Ees, Engine X-Flixel");
            //credits.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0x80, 0x6e, 0x55), FlxJustification.Left, new Color(0x80, 0x6e, 0x55));
            //credits.shadow = new Color(0x80, 0x6e, 0x55);
            //credits.scale = 1; // size = 32
            //credits.color = new Color(0x80, 0x6e, 0x55);
            //credits.antialiasing = false;
            
            //add(credits);

            if (Revvolvver_Globals.GAMES_PLAYS_ITSELF)
            {
                _fading = true;
                FlxG.play(SndGun2, 0.35f);
                // _gibs.start(true, 5);
                //FlxG.state = new PlayStateMulti();        
                FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onFade, false);
            }



        }

        public void createLetter(char Letter, int x, int y)
        {
            FlxText _t1 = new FlxText(x,y,20, Letter.ToString() );
            _t1.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xd1, 0x6e, 0x55), FlxJustification.Left, new Color(0xd1, 0x6e, 0x55));
            _t1.shadow = new Color(0xd1, 0x6e, 0x55);
            _t1.scale = 4; // size = 32
            _t1.color = Color.White;
            _t1.antialiasing = false;
            _t1.angle = -5;

            letters.add(_t1);
        }

        override public void update()
        {
            //Console.WriteLine("FlxG.debug is: " + FlxG.debug);

            PlayerIndex pi;
            
            frameCount++;

            if (frameCount % 5 == 0 && clickCount < 10)
            {
                letters.members[clickCount].angle = 0;

                // play a sound;
                int snd= (int)FlxU.random(1,8);
				//FlxG.play(Snds[snd], 0.95f);
                clickCount++;
            }
            if (frameCount % 5 == 11 && clickCount < 10)
            {
                //GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }

            if (frameCount > 70)
            {
                //GamePad.SetVibration(PlayerIndex.One, 0, 0);

                if (frameCount == 71)
                {
                    playersText.visible = true;
                    playersTextx.visible = true;

                    FlxG.playMusic(Music, 1.0f);
                }
                
                if (FlxU.random() < 0.015f)
                {
                    flower.visible = true;
                    flower.angularVelocity = 500;

                    int r = (int)FlxU.random(0, 10);
                    letters.members[r].angle= FlxU.random(-45,45);
                    //letters.members[r].angularDrag = 100;
                    FlxG.play(SndGun2, 0.025f);
                }
            }


            if (clickCount > 160)
            {
                if (((FlxG.keys.isKeyDown(Keys.X, FlxG.controllingPlayer, out pi) && FlxG.keys.isKeyDown(Keys.C, FlxG.controllingPlayer, out pi))
                    || (FlxG.gamepads.isNewButtonPress(Buttons.Start, FlxG.controllingPlayer, out pi))))
                {
                    //_ok2 = true;
                    //FlxG.play(SndHit2);

                    //d0f4f7
                    FlxG.flash.start(new Color(0xd0, 0xf4, 0xf7), 0.5f, null, false);
                    FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onFade, false);

                    //FlxG.flash.start(new Color(0xd1, 0x6e, 0x55), 0.5f, null, false);
                    //FlxG.fade.start(new Color(0xd1, 0x6e, 0x55), 1f, onFade, false);
                }

            }


            if (FlxG.gamepads.isNewButtonPress(Buttons.Back, FlxG.controllingPlayer, out pi))
            {
                FlxG.Game.Exit();
            }

            base.update();

            // exit.
            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.Game.Exit();
                return;
            }

            if (Revvolvver_Globals.PLAYERS == 0) playersText.text = "<- Players: 0 ->";
            if (Revvolvver_Globals.PLAYERS == 1) playersText.text = "<- Players: 1 ->";
            if (Revvolvver_Globals.PLAYERS == 2) playersText.text = "<- Players: 2 ->";
            if (Revvolvver_Globals.PLAYERS == 3) playersText.text = "<- Players: 3 ->";
            if (Revvolvver_Globals.PLAYERS == 4) playersText.text = "<- Players: 4 ->";


            if (clickCount > 2 && !_fading)
            {

                if (FlxG.keys.justPressed(Keys.Left) || FlxG.gamepads.isNewButtonPress(Buttons.DPadLeft) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickLeft))
                {
                    Revvolvver_Globals.PLAYERS--;

                    if (Revvolvver_Globals.PLAYERS == 0) Revvolvver_Globals.PLAYERS = 4;

                }
                if (FlxG.keys.justPressed(Keys.Right) || FlxG.gamepads.isNewButtonPress(Buttons.DPadRight) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickRight))
                {
                    Revvolvver_Globals.PLAYERS++;

                    if (Revvolvver_Globals.PLAYERS >=5) Revvolvver_Globals.PLAYERS = 1;
                }
                if (FlxG.keys.justPressed(Keys.F8))
                {
                    Revvolvver_Globals.PLAYERS=0;

                }
                //if (FlxG.keys.justPressed(Keys.D1) || FlxG.keys.justPressed(Keys.F1) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.One, out pi))
                //{
                //    // VOICE OVER: "One Player";
                //    playersText.text = "<- Players: 1 ->";
                //    FlxG.play(SndGun1, 0.25f);
                //    //_gibs.start(true, 5);
                //    Revvolvver_Globals.PLAYERS = 1;
                //    //FlxG.state = new PlayStateMulti();
                //    //FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                //    return;
                //}
                //if (FlxG.keys.justPressed(Keys.D2) || FlxG.keys.justPressed(Keys.F2) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Two, out pi))
                //{
                //    // VOICE OVER: "Two Players";

                //    FlxG.play(SndGun1, 0.25f);
                //    //_gibs.start(true, 5);
                //    Revvolvver_Globals.PLAYERS = 2;
                //    playersText.text = "<- Players: 2 ->";
                //    //FlxG.state = new PlayStateMulti();
                //    //FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                //    return;
                //}
                //if (FlxG.keys.justPressed(Keys.D3) || FlxG.keys.justPressed(Keys.F3) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Three, out pi))
                //{
                //    // VOICE OVER: "Three Players";
                //    playersText.text = "<- Players: 3 ->";
                //    FlxG.play(SndGun1, 0.25f);
                //    //_gibs.start(true, 5);
                //    Revvolvver_Globals.PLAYERS = 3;
                //    //FlxG.state = new PlayStateMulti();
                //    //FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                //    return;
                //}
                //if (FlxG.keys.justPressed(Keys.D4) || FlxG.keys.justPressed(Keys.F4) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Four, out pi))
                //{
                //    // VOICE OVER: "Maximum Four Players";

                //    FlxG.play(SndGun1, 0.25f);
                //    //_gibs.start(true, 5);
                //    Revvolvver_Globals.PLAYERS = 4;
                //    playersText.text = "<- Players: 4 ->";
                //    //FlxG.state = new PlayStateMulti();
                //    //FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                //    return;
                //}



				if (FlxG.keys.justPressed(Keys.Enter) || FlxG.gamepads.isNewButtonPress(Buttons.Start) || FlxG.gamepads.isNewButtonPress(Buttons.A))
                {
                    _fading = true;
                    FlxG.play(SndGun2, 0.35f);
                   // _gibs.start(true, 5);
                    //FlxG.state = new PlayStateMulti();        
                    FlxG.fade.start(new Color(0xd0, 0xf4, 0xf7), 1f, onFade, false);
                    
                    return;
                }
                if (FlxG.keys.justPressed(Keys.Q) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
                {
                    FlxG.state = new SettingsState();
                    return;
                }

            }

//			if ( FlxG.gamepads.isButtonDown(Buttons.LeftStick) || FlxG.gamepads.isButtonDown(Buttons.Start) || FlxG.gamepads.isButtonDown(Buttons.BigButton) || FlxG.gamepads.isButtonDown(Buttons.Y)   )
//			{
//				Console.WriteLine("OK!");
//			}

//			if (FlxG.gamepads.isButtonDown (Buttons.LeftTrigger)) {
//
//				// works!
//				//Song x = FlxG.Content.Load<Song>("music/Intro");
//				//MediaPlayer.Play (x);
//
//
//				Console.WriteLine ("Is music playing?" + FlxG.music.playing);
//
//				FlxG.playAndroidMusic ("music/Intro", 0.5f);
//
//
//
//			}
        }

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            //FlxG.level = (int)FlxU.random(1,9.9);
            FlxG.level = 1;

            FlxG.state = new SettingsState();
            //FlxG.state = new PlayStateTiles();
        }

    }
}

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
    public class MenuState : FlxState
    {
        //private static bool _alreadySaved = false;

        private Texture2D ImgGibs;
        private Texture2D ImgCursor;

        private FlxEmitter _gibs;
        private bool _ok2;
        private FlxGroup letters;

        private int frameCount;
        private int clickCount;

        private const string SndClick = "Revvolvver/sfx/gunclick";
        private const string SndGun1 = "Revvolvver/sfx/gunshot1";
        private const string SndGun2 = "Revvolvver/sfx/gunshot2";

        private FlxSound SndFlxClick;

        private FlxSprite flower;

        //#if !WINDOWS_PHONE
        //        FlxSave save;
        //        private bool hasCheckedSaveFile = false;
        //#endif

        override public void create()
        {

            FlxG.backColor = new Color(0xdb, 0xd8, 0xac);

            base.create();

            flower = new FlxSprite(FlxG.width / 2 - 290 / 2, FlxG.height / 2 - 290 / 2 + 60);
            flower.loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/flower"), false, false, 290, 290);
            add(flower);
            flower.visible = false;

            flower.angularDrag = 1500;


            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Revvolvver/titlescreen.oel", "level/NonDestructable");
            FlxTilemap _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 8, 8);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            add(_tileMap);




            FlxG.hideHud();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgCursor = FlxG.Content.Load<Texture2D>("initials/crosshair");

            _gibs = new FlxEmitter(0, FlxG.height  - 30);
            _gibs.setSize(FlxG.width, 30);
            _gibs.setYSpeed(-400, -20);
            _gibs.setRotation(-720, 720);
            _gibs.gravity = 50;
            _gibs.createSprites(ImgGibs, 1000);
            add(_gibs);

            letters = new FlxGroup();

            string title = "REVVOLVVER";
            int count = 0;
            foreach (char c in title)
            {
                createLetter(c, 70 + count * 50, 60);

                count++;
            }

            add(letters);

            frameCount = 1;

            //SndFlxClick = new FlxSound();
            

        }

        public void createLetter(char Letter, int x, int y)
        {
            FlxText _t1 = new FlxText(x,y,20, Letter.ToString() );
            _t1.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, new Color(0xd1, 0x6e, 0x55), FlxJustification.Left, new Color(0xd1, 0x6e, 0x55));
            _t1.shadow = new Color(0xd1, 0x6e, 0x55);
            _t1.scale = 3; // size = 32
            _t1.color = new Color(0xd1, 0x6e, 0x55);
            _t1.antialiasing = false;
            _t1.angle = -5;

            letters.add(_t1);
        }

        override public void update()
        {
            PlayerIndex pi;

            frameCount++;

            if (frameCount % 15 == 0 && clickCount < 10)
            {
                letters.members[clickCount].angle = 0;
                // play a sound;

                FlxG.play(SndClick, 0.5f);

                //FlxG.gamepads.vibrate(clickCount, 1.0f, 0.5f, 0.5f);


                //GamePad.SetVibration(PlayerIndex.One, (float)(clickCount)/10, 0);

                //GamePad.SetVibration(PlayerIndex.One, 0, (float)(clickCount) / 10);

                //SndFlxClick.proximity(clickCount * 10, 20, null, 30.0f, false);

                //SndFlxClick.play();


                clickCount++;
            }
            if (frameCount % 15 == 11 && clickCount < 10)
            {
                //GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }

            if (frameCount > 160)
            {
                //GamePad.SetVibration(PlayerIndex.One, 0, 0);

                if (FlxU.random() < 0.015f)
                {
                    flower.visible = true;
                    flower.angularVelocity = 500;

                    int r = (int)FlxU.random(0, 10);
                    letters.members[r].angle= FlxU.random(-45,45);
                    //letters.members[r].angularDrag = 100;
                    FlxG.play(SndGun2, 0.25f);
                }
            }


            if (clickCount > 160)
            {
                if (((FlxG.keys.isKeyDown(Keys.X, FlxG.controllingPlayer, out pi) && FlxG.keys.isKeyDown(Keys.C, FlxG.controllingPlayer, out pi))
                    || (FlxG.gamepads.isNewButtonPress(Buttons.Start, FlxG.controllingPlayer, out pi))))
                {
                    _ok2 = true;
                    //FlxG.play(SndHit2);
                    FlxG.flash.start(new Color(0xd8, 0xeb, 0xa2), 0.5f, null, false);
                    FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
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
                FlxG.state = new FourChambers.GameSelectionMenuState();
                return;
            }


            if (clickCount > 9)
            {
                if (FlxG.keys.justPressed(Keys.D1) || FlxG.keys.justPressed(Keys.F1) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.One, out pi))
                {
                    // VOICE OVER: "One Player";

                    FlxG.play(SndGun1);
                    _gibs.start(true, 5);
                    Revvolvver_Globals.PLAYERS = 1;
                    //FlxG.state = new PlayStateMulti();
                    FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                    return;
                }
                if (FlxG.keys.justPressed(Keys.D2) || FlxG.keys.justPressed(Keys.F2) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Two, out pi))
                {
                    // VOICE OVER: "Two Players";

                    FlxG.play(SndGun1);
                    _gibs.start(true, 5);
                    Revvolvver_Globals.PLAYERS = 2;
                    //FlxG.state = new PlayStateMulti();
                    FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                    return;
                }
                if (FlxG.keys.justPressed(Keys.D3) || FlxG.keys.justPressed(Keys.F3) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Three, out pi))
                {
                    // VOICE OVER: "Three Players";

                    FlxG.play(SndGun1);
                    _gibs.start(true, 5);
                    Revvolvver_Globals.PLAYERS = 3;
                    //FlxG.state = new PlayStateMulti();
                    FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                    return;
                }
                if (FlxG.keys.justPressed(Keys.D4) || FlxG.keys.justPressed(Keys.F4) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Four, out pi))
                {
                    // VOICE OVER: "Maximum Four Players";

                    FlxG.play(SndGun1);
                    _gibs.start(true, 5);
                    Revvolvver_Globals.PLAYERS = 4;
                    //FlxG.state = new PlayStateMulti();
                    FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                    return;
                }
            }


        }

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new PlayStateMulti();
            //FlxG.state = new PlayStateTiles();
        }

    }
}

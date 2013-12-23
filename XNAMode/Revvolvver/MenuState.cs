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


        //#if !WINDOWS_PHONE
        //        FlxSave save;
        //        private bool hasCheckedSaveFile = false;
        //#endif

        override public void create()
        {

            FlxG.backColor = new Color(0xdb, 0xd8, 0xac);

            base.create();

            FlxG.hideHud();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgCursor = FlxG.Content.Load<Texture2D>("initials/crosshair");

            _gibs = new FlxEmitter(FlxG.width / 2 - 50, FlxG.height  - 10);
            _gibs.setSize(200, 30);
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

        }

        public void createLetter(char Letter, int x, int y)
        {
            FlxText _t1 = new FlxText(x,y,20, Letter.ToString() );
            _t1.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, Color.Black, FlxJustification.Left, Color.Black);

            _t1.scale = 3; // size = 32
            _t1.color = new Color(0xd1, 0x6e, 0x55);
            _t1.antialiasing = true;
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

                clickCount++;
            }

            //X + C were pressed, fade out and change to play state
            if (((FlxG.keys.isKeyDown(Keys.X, FlxG.controllingPlayer, out pi) && FlxG.keys.isKeyDown(Keys.C, FlxG.controllingPlayer, out pi))
                || (FlxG.gamepads.isNewButtonPress(Buttons.Start, FlxG.controllingPlayer, out pi))))
            {
                _ok2 = true;
                //FlxG.play(SndHit2);
                FlxG.flash.start(new Color(0xd8, 0xeb, 0xa2), 0.5f, null, false);
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
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

            if (FlxG.keys.justPressed(Keys.D2) || FlxG.keys.justPressed(Keys.F2) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Two, out pi))
            {
                _gibs.start(true, 5);
                Revvolvver_Globals.PLAYERS = 2;
                //FlxG.state = new PlayStateMulti();
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                return;
            }
            if (FlxG.keys.justPressed(Keys.D3) || FlxG.keys.justPressed(Keys.F3) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Three, out pi))
            {
                _gibs.start(true, 5);
                Revvolvver_Globals.PLAYERS = 3;
                //FlxG.state = new PlayStateMulti();
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                return;
            }
            if (FlxG.keys.justPressed(Keys.D4) || FlxG.keys.justPressed(Keys.F4) || FlxG.gamepads.isNewButtonPress(Buttons.A, PlayerIndex.Four, out pi))
            {
                _gibs.start(true, 5);
                Revvolvver_Globals.PLAYERS = 4;
                //FlxG.state = new PlayStateMulti();
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
                return;
            }


        }

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new PlayStateMulti();
            //FlxG.state = new PlayStateTiles();
        }

    }
}

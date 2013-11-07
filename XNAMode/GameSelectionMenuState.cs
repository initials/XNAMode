using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using XNATweener;

namespace XNAMode
{
    public class GameSelectionMenuState : FlxState
    {

        FlxText _menuItems;

        FlxText _nameEntry;

        FlxButton play;

        FlxSprite bgSprite;

        private Tweener tween;

        override public void create()
        {
            

            FlxG.backColor = new Color(0xc2, 0x88, 0x83);

            base.create();

            FlxG.resetHud();
            FlxG.showHud();

            tween = new Tweener(0, -240 , TimeSpan.FromSeconds(3.9f), Quadratic.EaseOut);

            // -350, -310
            bgSprite = new FlxSprite(-350, 0, FlxG.Content.Load<Texture2D>("initials/Fear"));
            add(bgSprite);

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));
            
            _menuItems = new FlxText(0, 30, FlxG.width);
            _menuItems.setFormat(null, 2, Color.White, FlxJustification.Center, Color.White);
            //_menuItems.text = "Four Chambers\n\nEnter name, use @ symbol to specify Twitter handle.\nPress enter when complete.";
            _menuItems.text = "The Four Chambers\nOf The Human Heart";
            _menuItems.shadow = Color.Black;
            add(_menuItems);

            _nameEntry = new FlxText(10, 100, FlxG.width);
            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            _nameEntry.text = "";
            add(_nameEntry);

            play = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 30, playGame, FlxButton.ControlPadA);
            play.loadGraphic((new FlxSprite()).createGraphic(100, 20, new Color(0xe4, 0xb4, 0x8a)), (new FlxSprite()).createGraphic(102, 22, new Color(0xdd, 0xa1, 0x6d)));
            play.loadText(new FlxText(2, 2, 100, "Play Game"), new FlxText(2, 2, 100, "Play Game!"));
            add(play);

            FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width / 2 + 54 , FlxG.height - 27);

            FlxG.flash.start(Color.Black, 1.5f);

            FlxG.color(Color.White);

        }

        override public void update()
        {

            PlayerIndex pi;
            if (FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi))
            {
                FlxG.joystickBeingUsed = true;
            }


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

            if (FlxG.keys.F4)
            {
                FlxG.state = new EmptyIntroTestState();

            }

            base.update();

            if (FlxG.keys.justPressed(Keys.F9))
            {
                FlxG.bloom.Visible = !FlxG.bloom.Visible;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        public void playGame()
        {

            Console.WriteLine("Just pressed Enter");

            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            FlxG.state = new BasePlayStateFromOel();

        }

    }
        
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XNATweener;

namespace org.flixel
{
    /// <summary>
    /// @benbaird X-flixel only. Moves all of the flixel logo screen stuff to a FlxState.
    /// 
    /// @initials_games customised for initials releases.
    /// </summary>
    public class FlxSplash : FlxState
    {
        //logo stuff
        private List<FlxLogoPixel> _f;
        private static Color _fc = Color.Yellow;
        private float _logoTimer = 0;
        private Texture2D _poweredBy;
        private SoundEffect _fSound;
        private static FlxState _nextScreen;

        private Texture2D _initialsLogo;
        private const string SndTag = "initials/FourChambers_IntroTag_03";

        private FlxSprite _logo;

        private Tweener _logoTweener;


        public FlxSplash()
            : base()
        {
        }

        public override void create()
        {
            base.create();
            _f = null;
            _poweredBy = FlxG.Content.Load<Texture2D>("flixel/poweredby");
            _fSound = FlxG.Content.Load<SoundEffect>("flixel/flixel");

            _initialsLogo = FlxG.Content.Load<Texture2D>("initials/initialsLogo");

            _logo = new FlxSprite();
            _logo.loadGraphic(_initialsLogo, false, false, 216,24);
            _logo.x = FlxG.width / 2 - 216 / 2;
            _logo.y = FlxG.height / 2 - 24;
            add(_logo);

            _logoTweener = new Tweener(-100, FlxG.height / 2 - 24, TimeSpan.FromSeconds(0.9f), Bounce.EaseOut);
            //_logoTweener.PositionChanged += delegate (float newRotation) { _logo.y = newRotation } 
            

            FlxG.play(SndTag,1.0f);

            FlxG.transition.startFadeIn(0.1f);
        }

        public static void setSplashInfo(Color flixelColor, FlxState nextScreen)
        {
            _fc = flixelColor;
            _nextScreen = nextScreen;
        }

        public override void update()
        {
            _logoTweener.Update(FlxG.elapsedAsGameTime);
            _logo.y = _logoTweener.Position;

            if (_f == null && _logoTimer > 2.5f)
            {

                //_logo.visible = false;

                _logoTweener.Reverse();

                //FlxG.flash.start(FlxG.backColor, 1.0f, null, false);

                _f = new List<FlxLogoPixel>();
                int scale = 10;
                float pwrscale;

                int pixelsize = (FlxG.height / scale);
                int top = (FlxG.height / 2) - (pixelsize * 2);
                int left = (FlxG.width / 2) - pixelsize;

                pwrscale = ((float)pixelsize / 24f);

                //Add logo pixels
                add(new FlxLogoPixel(left + pixelsize, top, pixelsize, 0, _fc));
                add(new FlxLogoPixel(left, top + pixelsize, pixelsize, 1, _fc));
                add(new FlxLogoPixel(left, top + (pixelsize * 2), pixelsize, 2, _fc));
                add(new FlxLogoPixel(left + pixelsize, top + (pixelsize * 2), pixelsize, 3, _fc));
                add(new FlxLogoPixel(left, top + (pixelsize * 3), pixelsize, 4, _fc));

                FlxSprite pwr = new FlxSprite((FlxG.width - (int)((float)_poweredBy.Width * pwrscale)) / 2, top + (pixelsize * 4) + 16, _poweredBy);
                pwr.loadGraphic(_poweredBy, false, false, 64);

                //pwr.color = _fc;
                //pwr.scale = pwrscale;
                add(pwr);

                _fSound.Play(FlxG.volume, 0f, 0f);
            }

            _logoTimer += FlxG.elapsed;
            
            base.update();

            if (_logoTimer > 5.5f || FlxG.keys.SPACE || FlxG.keys.ENTER || FlxG.gamepads.isButtonDown(Buttons.A))
            {
                FlxG.destroySounds(true);

                FlxG.state = _nextScreen;
            }
            if (FlxG.keys.F1)
            {
                FlxG.destroySounds(true);
				FlxG.state = new XNAMode.DebugMenuState();
            }
        }
    }
}

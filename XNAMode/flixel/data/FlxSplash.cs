using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace org.flixel
{
    //@benbaird X-flixel only. Moves all of the flixel logo screen stuff to a FlxState.
    public class FlxSplash : FlxState
    {
        //logo stuff
        private List<FlxLogoPixel> _f;
        private static Color _fc = Color.Gray;
        private float _logoTimer = 0;
        private Texture2D _poweredBy;
        private SoundEffect _fSound;
        private static FlxState _nextScreen;

        private Texture2D _initialsLogo;
        private SoundEffect _tagtoneSound;
        private const string SndTag = "initials/initials_empire_tagtone3";

        private FlxSprite _logo;


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
            //_tagtoneSound = FlxG.Content.Load<SoundEffect>("initials/initials_empire_tagtone3");
            

            


            _logo = new FlxSprite();
            _logo.loadGraphic(_initialsLogo, false, false, 216,24);
            _logo.x = FlxG.width / 2 - 216 / 2;
            _logo.y = FlxG.height / 2 - 24;

            add(_logo);

            //_tagtoneSound.Play(FlxG.volume, 0.0f, 0.0f);

            FlxG.play(SndTag,1.0f);


        }

        public static void setSplashInfo(Color flixelColor, FlxState nextScreen)
        {
            _fc = flixelColor;
            _nextScreen = nextScreen;
        }

        public override void update()
        {



            if (_f == null && _logoTimer > 5.5f)
            {

                _logo.visible = false;

                FlxG.flash.start(FlxG.backColor, 1f, null, false);

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
                pwr.loadGraphic(_poweredBy, false, false, (int)((float)_poweredBy.Width * pwrscale), (int)((float)_poweredBy.Height * pwrscale));

                pwr.color = _fc;
                pwr.scale = pwrscale;
                add(pwr);

                _fSound.Play(FlxG.volume, 0f, 0f);
            }

            _logoTimer += FlxG.elapsed;

            base.update();

            if (_logoTimer > 8.5f || FlxG.keys.SPACE)
            {
                FlxG.destroySounds(true);

                FlxG.state = _nextScreen;
            }
        }
    }
}

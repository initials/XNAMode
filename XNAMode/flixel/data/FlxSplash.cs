using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

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
        private FlxSprite pwr;

        private Texture2D Bubbles;
        private FlxEmitter _bubbles;

        public FlxSplash()
            : base()
        {
        }

        public override void create()
        {
            base.create();


            Bubbles = FlxG.Content.Load<Texture2D>("Mode/bubble");
            _bubbles = new FlxEmitter();
            _bubbles.x = 125;
            _bubbles.y = 125;
            _bubbles.width = 24;
            _bubbles.height = 24;
            _bubbles.delay = 3.0f;
            _bubbles.setXSpeed(-200, 200);
            _bubbles.setYSpeed(-200, 200);
            _bubbles.createSprites(Bubbles, 100, true, 1.0f, 1.0f);
            _bubbles.start(true, 3.0f, 100);

            add(_bubbles);


            _f = null;
            _poweredBy = FlxG.Content.Load<Texture2D>("flixel/initialsLogo");
            _fSound = FlxG.Content.Load<SoundEffect>("flixel/flixel");

            FlxG.flash.start(FlxG.backColor, 1f, null, false);
        }

        public static void setSplashInfo(Color flixelColor, FlxState nextScreen)
        {
            _fc = flixelColor;
            _nextScreen = nextScreen;
        }

        public override void update()
        {
            if (_f == null)
            {
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

                //pwr = new FlxSprite((FlxG.width - (int)((float)_poweredBy.Width * pwrscale)) / 2, top + (pixelsize * 4) + 16, _poweredBy);
                //pwr.loadGraphic(_poweredBy, false, false, (int)((float)_poweredBy.Width * pwrscale), (int)((float)_poweredBy.Height * pwrscale));

                //pwr.color = _fc;
                //pwr.scale = pwrscale;
                //pwr.angularAcceleration = 1;
                //add(pwr);

                pwr = new FlxSprite(20, 20, _poweredBy);
                pwr.loadGraphic(_poweredBy, false, false, (int)(float)_poweredBy.Width, (int)(float)_poweredBy.Height);
                pwr.color = _fc;
                add(pwr);





                _fSound.Play(FlxG.volume, 0f, 0f);
            }

            _logoTimer += FlxG.elapsed;

            base.update();

            pwr.scale += 0.02f;
            


            if (_logoTimer > 2.5f)
            {
                FlxG.state = _nextScreen;
            }
        }
    }
}

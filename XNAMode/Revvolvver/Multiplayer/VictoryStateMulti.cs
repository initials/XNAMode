﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

namespace Revvolvver
{
    public class VictoryStateMulti : FlxState
    {
        private Texture2D ImgGibs;
        private const string SndMenu = "Revvolvver/menu_hit_2";

        private float _timer;
        private bool _fading;

        override public void create()
        {
            base.create();

            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");

            _timer = 0;
            _fading = false;
            FlxG.flash.start(new Color(0xd1, 0x6e, 0x55));
            //Gibs emitted upon death
            FlxEmitter gibs = new FlxEmitter(0, 0);
            gibs.setSize(FlxG.width, FlxG.height);
            gibs.setXSpeed(-100, 100);
            gibs.setYSpeed(-100, 100);
            gibs.setRotation(-360, 360);
            gibs.gravity = 80;
            gibs.createSprites(ImgGibs, 800);
            add(gibs);
            gibs.start(false, 0.005f);

            if (FlxG.scores[0]>10)
                add((new FlxText(0, FlxG.height / 3, FlxG.width, "PLAYER 1 WINS\nSCORE: " + FlxG.scores[0])).setFormat(null, 3, new Color(0xd1, 0x6e, 0x55), FlxJustification.Center, Color.Black));
            if (FlxG.scores[1] > 10)
                add((new FlxText(0, FlxG.height / 3, FlxG.width, "PLAYER 2 WINS\nSCORE: " + FlxG.scores[1])).setFormat(null, 3, new Color(0xd1, 0x6e, 0x55), FlxJustification.Center, Color.Black));
            if (FlxG.scores[2] > 10)
                add((new FlxText(0, FlxG.height / 3, FlxG.width, "PLAYER 3 WINS\nSCORE: " + FlxG.scores[2])).setFormat(null, 3, new Color(0xd1, 0x6e, 0x55), FlxJustification.Center, Color.Black));
            if (FlxG.scores[3] > 10)
                add((new FlxText(0, FlxG.height / 3, FlxG.width, "PLAYER 4 WINS\nSCORE: " + FlxG.scores[3])).setFormat(null, 3, new Color(0xd1, 0x6e, 0x55), FlxJustification.Center, Color.Black));
               
        
        }

        override public void update()
        {
            base.update();
            if (!_fading)
            {
                _timer += FlxG.elapsed;
                if ((_timer > 0.35) && ((_timer > 10) || FlxG.keys.justPressed(Keys.X) || FlxG.keys.justPressed(Keys.C) || FlxG.gamepads.isNewButtonPress(Buttons.Start) || FlxG.gamepads.isNewButtonPress(Buttons.A)))
                {
                    _fading = true;
                    FlxG.play(SndMenu);
                    FlxG.fade.start(new Color(0xd1, 0x6e, 0x55), 2, onPlay, false);
                }
            }
        }

        private void onPlay(object Sender, FlxEffectCompletedEvent e)
        {
            FlxG.state = new PlayStateMulti();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

namespace Revvolvver
{
    /// <summary>
    /// The main play state.
    /// </summary>
    public class PlayStateMulti : FlxState
    {
        protected Texture2D ImgTech;
        protected Texture2D ImgDirtTop;
        protected Texture2D ImgDirt;
        protected Texture2D ImgNotch;
        private Texture2D ImgGibs;
        private Texture2D ImgSpawnerGibs;

        //major game objects
        protected FlxGroup _blocks;
        protected FlxGroup _decorations;
        protected FlxGroup _bullets;
        protected FlxTilemap _tileMap;
        protected FlxTilemap _caveMap;
        Dictionary<string, string> attrs;

        protected FlxGroup _players;
        protected PlayerMulti _player1;
        protected PlayerMulti _player2;
        protected PlayerMulti _player3;
        protected PlayerMulti _player4;


        protected FlxGroup _bots;
        protected FlxGroup _spawners;
        protected FlxGroup _botBullets;
        protected FlxEmitter _littleGibs;
        protected FlxEmitter _bigGibs;

        protected FlxEmitter _clouds;

        //meta groups, to help speed up collisions
        protected FlxGroup _objects;
        protected FlxGroup _enemies;
        protected FlxGroup _cloudsGrp;

        //HUD
        protected FlxText _score;
        protected FlxText _score2;
        protected float _scoreTimer;
        protected float _jamTimer;
        protected FlxSprite _jamBar;
        protected FlxText _jamText;
        protected List<FlxSprite> _notches = new List<FlxSprite>();

        //just to prevent weirdness during level transition
        protected bool _fading;

        //used to safely reload the playstate after dying
        public bool reload;

        public FlxGroup hudElements;

        private  Color p1Color = Color.PaleVioletRed;
        private  Color p2Color = Color.MediumSpringGreen;
        private  Color p3Color = Color.MediumSlateBlue;
        private  Color p4Color = Color.Cyan;

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

        private float bloomTimer = 0.0f;

        private float regenTimer = 0.0f;


        override public void create()
        {
            base.create();
            
            _caveMap = new FlxTilemap();

            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[6];

            FlxSprite bg = new FlxSprite(0, 0);
            bg.loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/bg"));
            add(bg);

            ImgTech = FlxG.Content.Load<Texture2D>("Revvolvver/tech_tiles");
            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/gibs");
            ImgSpawnerGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgNotch = FlxG.Content.Load<Texture2D>("Revvolvver/notch");

            _littleGibs = new FlxEmitter();
            _littleGibs.delay = 3;
            _littleGibs.setXSpeed(-150, 150);
            _littleGibs.setYSpeed(-200, 0);
            _littleGibs.setRotation(-720, -720);
            _littleGibs.createSprites(ImgGibs, 100, true, 0.5f, 0.65f);
            _bigGibs = new FlxEmitter();
            _bigGibs.setXSpeed(-200, 200);
            _bigGibs.setYSpeed(-300, 0);
            _bigGibs.setRotation(-720, -720);
            _bigGibs.createSprites(ImgSpawnerGibs, 50, true, 0.5f, 0.35f);





            FlxG.mouse.hide();
            reload = false;


            _cloudsGrp = new FlxGroup();

            for (int vv = 0; vv < 10; vv++)
            {
                Cloud c = new Cloud((int)FlxU.random(-50, 400), (int)FlxU.random(0, 100));
                c.velocity.X = FlxU.random(50, 150);
                _cloudsGrp.add(c);
            }

            add(_cloudsGrp);


            //level generation needs to know about the spawners (and thusly the bots, players, etc)
            _blocks = new FlxGroup();
            _decorations = new FlxGroup();
            _bullets = new FlxGroup();
            _players = new FlxGroup();

            List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("Revvolvver/level" + FlxG.level.ToString() + ".oel", "level/Items");

            _player1 = new PlayerMulti(Convert.ToInt32(actorsAttrs[0]["x"]), Convert.ToInt32(actorsAttrs[0]["y"]), _bullets.members, _littleGibs);
            _player1.controller = PlayerIndex.One;
            _player1.controllerAsInt = 1;
            _player1.color = p1Color;

            FlxG._game.hud.p1HudText.scale = 3;
            FlxG._game.hud.p1HudText.x += 40;
            FlxG._game.hud.p1HudText.color = p1Color;


            _player2 = new PlayerMulti(Convert.ToInt32(actorsAttrs[1]["x"]), Convert.ToInt32(actorsAttrs[1]["y"]), _bullets.members, _littleGibs);
            _player2.controller = PlayerIndex.Two;
            _player2.controllerAsInt = 2;
            _player2.color = p2Color;

            FlxG._game.hud.p2HudText.scale = 3;
            FlxG._game.hud.p2HudText.x -= 40;
            FlxG._game.hud.p2HudText.color = p2Color;

            _player3 = new PlayerMulti(Convert.ToInt32(actorsAttrs[2]["x"]), Convert.ToInt32(actorsAttrs[2]["y"]), _bullets.members, _littleGibs);
            _player3.controller = PlayerIndex.Three;
            _player3.controllerAsInt = 3;
            _player3.color = p3Color;

            FlxG._game.hud.p3HudText.scale = 3;
            FlxG._game.hud.p3HudText.x += 40 ;
            FlxG._game.hud.p3HudText.y -= 20;
            FlxG._game.hud.p3HudText.color = p3Color;

            
            _player4 = new PlayerMulti(Convert.ToInt32(actorsAttrs[3]["x"]), Convert.ToInt32(actorsAttrs[3]["y"]), _bullets.members, _littleGibs);
            _player4.controller = PlayerIndex.Four;
            _player4.controllerAsInt = 4;
            _player4.color = p4Color;

            FlxG._game.hud.p4HudText.scale = 3;
            FlxG._game.hud.p4HudText.x -= 40;
            FlxG._game.hud.p4HudText.y -= 20;
            FlxG._game.hud.p4HudText.color = p4Color;

            if (Revvolvver_Globals.PLAYERS == 1)
            {
                _player2.startPlayingBack();
                _player3.startPlayingBack();
                _player4.startPlayingBack();
            }

            if (Revvolvver_Globals.PLAYERS == 2)
            {
                _player3.startPlayingBack();
                _player4.startPlayingBack();
            }

            if (Revvolvver_Globals.PLAYERS == 3)
            {
                _player4.startPlayingBack();
            }

            if (Revvolvver_Globals.PLAYERS == 4)
            {

            }


            

            _bots = new FlxGroup();
            _botBullets = new FlxGroup();
            _spawners = new FlxGroup();
            hudElements = new FlxGroup();

            attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Revvolvver/level" + FlxG.level.ToString() + ".oel", "level/NonDestructable");
            _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]),16, 16);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            _blocks.add(_tileMap);

            attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Revvolvver/level" + FlxG.level.ToString() + ".oel", "level/Destructable");
            _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["Destructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]),16, 16);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            _blocks.add(_tileMap);
            //_tileMap.color = new Color(FlxU.random(0, 1), FlxU.random(0, 1), FlxU.random(0, 1));


            regen();

            //FlxCaveGeneratorExt caveExt = new FlxCaveGeneratorExt(40, 30);
            //caveExt.numSmoothingIterations = 5;
            //caveExt.initWallRatio = 0.485f;
            //FlxTilemap _tileMap2 = new FlxTilemap();
            //_tileMap2.auto = FlxTilemap.AUTO;
            //string[,] tiles = caveExt.generateCaveLevel(null,new int[] {21},null,null,null,new int[] {0,1,2,37,38},null,null);              
            //string newMap = caveExt.convertMultiArrayStringToString(tiles);
            //_tileMap.loadMap(newMap, FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 16, 16);
            //_blocks.add(_tileMap2);

            add(_littleGibs);
            add(_bigGibs);

            
            add(_blocks);
            add(_decorations);
            add(_bots);

            int i;
            for (i = 0; i < 80; i++)
                _bullets.add(new BulletMulti());

            //add player and set up scrolling camera
            _players.add(_player1);
            _players.add(_player2);
            _players.add(_player3);
            _players.add(_player4);
            add(_players);

            FlxSprite movingPlatform = new FlxSprite(320, 0, FlxG.Content.Load<Texture2D>("Revvolvver/movingPlatform"));
            _blocks.add(movingPlatform);
            FlxPath batpath = new FlxPath(null);
            batpath.addPointsUsingStrings("320,320,320,320,", "112,284,284,112,");
            movingPlatform.followPath(batpath, 80, FlxObject.PATH_YOYO, false);
            movingPlatform.pathCornering = 4.0f;
            movingPlatform.solid = true;
            movingPlatform.@fixed = true;
            //movingPlatform.startFollowingPath();




            //FlxG.follow(_player1, 2.5f);
            //FlxG.followAdjust(0.5f, 0.0f);
            //FlxG.followBounds(0, 0, FlxG.width, FlxG.height);


            //add gibs + bullets to scene here, so they're drawn on top of pretty much everything
            add(_botBullets);
            add(_bullets);

            //finally we are going to sort things into a couple of helper groups.
            //we don't add these to the state, we just use them for collisions later!
            _enemies = new FlxGroup();
            _enemies.add(_botBullets);
            _enemies.add(_spawners);
            _enemies.add(_bots);
            _objects = new FlxGroup();
            //_objects.add(_botBullets);
            _objects.add(_bullets);
            _objects.add(_bots);
            _objects.add(_player1);
            _objects.add(_player2);
            _objects.add(_player3);
            _objects.add(_player4);
            _objects.add(_littleGibs);
            _objects.add(_bigGibs);

            FlxG.playMusic("Revvolvver/sfx/fullHeavyMetalJacket");
            FlxG.flash.start(Color.Black, 0.5f, null, false);
            _fading = false;

            FlxG.scores.Clear();
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);

            FlxG.showHud();
            FlxG.setHudGamepadButton(0, -200, -200);

            FlxG.setHudText(1, FlxG.scores[0].ToString());

            FlxG._game.hud.hudGroup.members.Clear();

            Color xc = Color.Wheat;

            for (i = 0; i < 24; i++)
            {
                int xp = 0;
                
                //TITLE SAFE
                //int yp = FlxG.height * 2 - 120;

                int yp = FlxG.height * 2 - 40;
                int xo = 180;
                

                if (i < 6)
                {
                    xp = xo + i * 32;
                    xc = p1Color;

                    if (i == 0)
                    {
                        FlxG._game.hud.p1HudText.x = xp;
                        FlxG._game.hud.p1HudText.y = yp - 40;
                    }
                }
                else if (i < 12)
                {
                    //xp = (int)((FlxG.width*1.6f) + (i-6) * 32);
                    xp = (xo+50) + i * 32;
                    xc = p2Color;
                    if (i == 6)
                    {
                        FlxG._game.hud.p2HudText.x = xp;
                        FlxG._game.hud.p2HudText.y = yp - 40;
                        FlxG._game.hud.p2HudText.alignment = FlxJustification.Left;
                    }
                }
                else if (i < 18)
                {
                    xp = (xo + 100) + i * 32;
                    
                    xc = p3Color;
                    if (i == 12)
                    {
                        FlxG._game.hud.p3HudText.x = xp;
                        FlxG._game.hud.p3HudText.y = yp - 40;
                    }

                }
                else if (i < 24)
                {
                    xp = (xo + 150) + i * 32;
                    
                    xc = p4Color;
                    if (i == 18)
                    {
                        FlxG._game.hud.p4HudText.x = xp;
                        FlxG._game.hud.p4HudText.y = yp - 40;
                        FlxG._game.hud.p4HudText.alignment = FlxJustification.Left;
                    }
                }


                FlxSprite bulletHUD = new FlxSprite(xp,yp);
                bulletHUD.loadGraphic(ImgNotch, true);
                bulletHUD.scrollFactor.X = bulletHUD.scrollFactor.Y = 0;
                bulletHUD.scale = 3;
                bulletHUD.color = xc;
                bulletHUD.addAnimation("ready", new int[] { 0 });
                bulletHUD.addAnimation("missed", new int[] { 1 });
                bulletHUD.addAnimation("hit", new int[] { 2 });
                bulletHUD.moves = false;
                bulletHUD.solid = false;
                bulletHUD.debugName = "ready";
                bulletHUD.play("on");
                FlxG._game.hud.hudGroup.add(bulletHUD);

            }

            //FlxG._game.hud.hudGroup.add(hudElements);

            FlxG.setHudText(1, "P1: 0" );
            FlxG.setHudText(2, "P2: 0" );
            FlxG.setHudText(3, "P3: 0" );
            FlxG.setHudText(4, "P4: 0" );


        }


        public void regen()
        {
            FlxCaveGeneratorExt caveExt = new FlxCaveGeneratorExt(40, 30);
            caveExt.numSmoothingIterations = 5;
            caveExt.initWallRatio = 0.505f;
            
            _caveMap.auto = FlxTilemap.AUTO;
            string[,] tiles = caveExt.generateCaveLevel(null, new int[] { 21 }, null, null, new int[] { 15, 25 }, new int[] { 20}, new int[] { 15, 25 }, new int[] { 0, 1, 2, 18,19,20,21, 37, 38 });
            string newMap = caveExt.convertMultiArrayStringToString(tiles);
            _caveMap.loadMap(newMap, FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 16, 16);
            _blocks.add(_caveMap);


            _caveMap.setTile((int)(_player1.x) / 16, (int)(_player1.y) / 16, 0, true);
            _caveMap.setTile((int)(_player2.x) / 16, (int)(_player2.y) / 16, 0, true);
            _caveMap.setTile((int)(_player3.x) / 16, (int)(_player3.y) / 16, 0, true);
            _caveMap.setTile((int)(_player4.x) / 16, (int)(_player4.y) / 16, 0, true);

            _caveMap.setTile((int)(_player1.x + 16) / 16, (int)(_player1.y) / 16, 0, true);
            _caveMap.setTile((int)(_player2.x + 16) / 16, (int)(_player2.y) / 16, 0, true);
            _caveMap.setTile((int)(_player3.x + 16) / 16, (int)(_player3.y) / 16, 0, true);
            _caveMap.setTile((int)(_player4.x + 16) / 16, (int)(_player4.y) / 16, 0, true);

            _caveMap.setTile((int)(_player1.x - 16) / 16, (int)(_player1.y) / 16, 0, true);
            _caveMap.setTile((int)(_player2.x - 16) / 16, (int)(_player2.y) / 16, 0, true);
            _caveMap.setTile((int)(_player3.x - 16) / 16, (int)(_player3.y) / 16, 0, true);
            _caveMap.setTile((int)(_player4.x - 16) / 16, (int)(_player4.y) / 16, 0, true);

            _caveMap.setTile((int)(_player1.x) / 16, (int)(_player1.y - 16) / 16, 0, true);
            _caveMap.setTile((int)(_player2.x) / 16, (int)(_player2.y - 16) / 16, 0, true);
            _caveMap.setTile((int)(_player3.x) / 16, (int)(_player3.y - 16) / 16, 0, true);
            _caveMap.setTile((int)(_player4.x) / 16, (int)(_player4.y - 16) / 16, 0, true);



        }

        override public void update()
        {
            bloomTimer += FlxG.elapsed;
            if (bloomTimer > 0.5f)
            {
                FlxG.bloom.Visible = false;
            }

            regenTimer += FlxG.elapsed;

            if (regenTimer > 5.0f)
            {
                _caveMap.rainbow = true;
            }

            if (regenTimer > 6.2f)
            {
                _caveMap.rainbow = false;
                regen();
                regenTimer = 0;
                _caveMap.color = Color.White;
            }

            FlxG.setHudText(1, "P1: " + FlxG.scores[0].ToString());
            FlxG.setHudText(2, "P2: " + FlxG.scores[1].ToString());
            FlxG.setHudText(3, "P3: " + FlxG.scores[2].ToString());
            FlxG.setHudText(4, "P4: " + FlxG.scores[3].ToString());

            if (FourChambers_Globals.cheatString == "winegar")
            {
                FourChambers_Globals.cheatString = " ";

                FlxG.scores[0] = Revvolvver_Globals.WINNING_SCORE - 1;

            }

            //PlayerIndex pi;

            //int os = FlxG.score;

            //FlxU.overlap(_bullets, _tileMap, destroyTileAt);


            base.update();

            //collisions with environment

            

            FlxU.collide(_blocks, _objects);
            //FlxU.overlap(_enemies, _players, overlapped);
            //FlxU.overlap(_bullets, _enemies, overlapped);
            FlxU.overlap(_bullets, _players, hitPlayer);
            FlxU.overlap(_bullets, _bullets, hitBullet);


            // THIS IS WHERE IT USED TO DESTROY TILES
            
            foreach (BulletMulti item in _bullets.members)
            {
                if (item.exploding == true)
                {
                    if (_caveMap.getTile((int)(item.x + item.tileOffsetX) / 16, (int)(item.y + item.tileOffsetY) / 16) != 0)
                    {
                        _caveMap.setTile((int)(item.x + item.tileOffsetX) / 16, (int)(item.y + item.tileOffsetY) / 16, 0, true);
                        _caveMap.setTile((int)(item.x + 9) / 16, (int)(item.y + item.tileOffsetY) / 16, 0, true);
                    }

                }
            }

            //// Rays aren't working.

            //Vector2 result = new Vector2();
            //if (_caveMap.ray((int)_player3.x, (int)_player3.y, (int)_player1.x, (int)_player1.y, result, 1) == false)
            //{
            //    Console.WriteLine("Shoot");

            //    if (_player3.timeSinceLastShot > 0.25f)
            //        _player3.shoot = true;


            //}


            //Toggle the bounding box visibility
            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.N) && FlxG.debug)
                regen();

            if (FlxG.gamepads.isNewButtonPress(Buttons.Back) || FlxG.keys.ESCAPE )
            {
                _fading = true;
                //FlxG.play(SndHit2);
                FlxG.flash.start(Color.White, 0.5f, null, false);
                FlxG.fade.start(Color.White, 1f, onFade, false);
            }

            if (FlxG.scores[0] == Revvolvver_Globals.WINNING_SCORE-1) 
            {
                if (_tileMap.color != p1Color) FlxG.play(SndChord7);
                _tileMap.color = p1Color;
                
            }
            if (FlxG.scores[1] == Revvolvver_Globals.WINNING_SCORE - 1)
            {
                if (_tileMap.color != p2Color) FlxG.play(SndChord6);
                _tileMap.color = p2Color;

            }
            if (FlxG.scores[2] == Revvolvver_Globals.WINNING_SCORE - 1)
            {
                if (_tileMap.color != p3Color) FlxG.play(SndChord5);
                _tileMap.color = p3Color;

            }
            if (FlxG.scores[3] == Revvolvver_Globals.WINNING_SCORE - 1)
            {
                if (_tileMap.color != p4Color) FlxG.play(SndChord4);
                _tileMap.color = p4Color;

            }
            
            if ((FlxG.scores[0] >= Revvolvver_Globals.WINNING_SCORE) || (FlxG.scores[1] >= Revvolvver_Globals.WINNING_SCORE) || (FlxG.scores[2] >= Revvolvver_Globals.WINNING_SCORE) || (FlxG.scores[3] >= Revvolvver_Globals.WINNING_SCORE))
            {
                FlxG.fade.start(Color.White, 3, onVictory, false);
            }

            adjustHUD();
            checkForPerfectRound();
        }

        private void checkForPerfectRound()
        {
            if ((FlxG._game.hud.hudGroup.members[0] as FlxSprite).debugName == "hit" && 
                (FlxG._game.hud.hudGroup.members[1] as FlxSprite).debugName == "hit" && 
                (FlxG._game.hud.hudGroup.members[2] as FlxSprite).debugName == "hit" && 
                (FlxG._game.hud.hudGroup.members[3] as FlxSprite).debugName == "hit" && 
                (FlxG._game.hud.hudGroup.members[4] as FlxSprite).debugName == "hit" && 
                (FlxG._game.hud.hudGroup.members[5] as FlxSprite).debugName == "hit" )
            {
                FlxG.scores[0] += 25;
            }

            if ((FlxG._game.hud.hudGroup.members[6] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[7] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[8] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[9] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[10] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[11] as FlxSprite).debugName == "hit")
            {
                FlxG.scores[1] += 25;
            }
            if ((FlxG._game.hud.hudGroup.members[12] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[13] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[14] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[15] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[16] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[17] as FlxSprite).debugName == "hit")
            {
                FlxG.scores[2] += 25;
            }

            if ((FlxG._game.hud.hudGroup.members[18] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[19] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[20] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[21] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[22] as FlxSprite).debugName == "hit" &&
                (FlxG._game.hud.hudGroup.members[23] as FlxSprite).debugName == "hit")
            {
                FlxG.scores[3] += 25;
            }
        }

        private void adjustHUD()
        {
            int offset = 0;
            foreach (PlayerMulti item in _players.members)
            {
                if (item.bulletsLeft == 6)
                {

                    //Console.WriteLine("p" + item.getScreenXY().ToString());

                    for (int i = offset; i < offset+6; i++)
                    {
                        //Console.WriteLine(i);
                        (FlxG._game.hud.hudGroup.members[i] as FlxSprite).play("ready");
                        (FlxG._game.hud.hudGroup.members[i] as FlxSprite).debugName = "ready";
                    }   
                }
                offset += 6;
            }
        }

        protected bool hitBullet(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object1.kill();
            e.Object2.kill();

            return true;
        }

        protected bool destroyTileAt(object Sender, FlxSpriteCollisionEvent e)
        {

            if (_caveMap.getTile((int)e.Object2.x / 8, (int)e.Object2.y / 8) != 0)
            {
                _caveMap.setTile((int)e.Object2.x / 8, (int)e.Object2.y / 8, 0, true);
            }

            //if (e.Object1 is BulletMulti)
            //    e.Object1.kill();
            //if (e.Object2 is BulletMulti)
            //    e.Object2.kill();

            //Console.WriteLine("DESTORY TILE AT " + e.Object1.GetType() + " " + e.Object2.GetType());
             

            //e.Object2.kill();

            return true;
        }


        protected bool hitPlayer(object Sender, FlxSpriteCollisionEvent e)
        {



            if (((FlxSprite)(e.Object1)).color != ((FlxSprite)(e.Object2)).color && !((PlayerMulti)(e.Object2)).dead)
            {
                /*
                if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.One && FlxG.scores[0]>0) FlxG.scores[0]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Two && FlxG.scores[1] > 0) FlxG.scores[1]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Three && FlxG.scores[2] > 0) FlxG.scores[2]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Four && FlxG.scores[3] > 0) FlxG.scores[3]--;
                */

                FlxG.gamepads.vibrate(((PlayerMulti)(e.Object2)).controllerAsInt , 1.0f, 0.6f, 0.6f);


                if (((BulletMulti)(e.Object1)).color == p1Color) FlxG.scores[0]++;
                else if (((BulletMulti)(e.Object1)).color == p2Color) FlxG.scores[1]++;
                else if (((BulletMulti)(e.Object1)).color == p3Color) FlxG.scores[2]++;
                else if (((BulletMulti)(e.Object1)).color == p4Color) FlxG.scores[3]++;


                string bulletData = ((BulletMulti)(e.Object1)).firedFromPlayer;
                int bulletInt = ((BulletMulti)(e.Object1)).bulletNumber;

                int notchToRender = 6 - bulletInt;

                

                if (bulletData == "Two")
                {
                    notchToRender += 6;
                }
                if (bulletData == "Three")
                {
                    notchToRender += 12;
                }
                if (bulletData == "Four")
                {
                    notchToRender += 18;
                }

                (FlxG._game.hud.hudGroup.members[notchToRender] as FlxSprite).play("hit");
                (FlxG._game.hud.hudGroup.members[notchToRender] as FlxSprite).debugName = "hit";

                if  (e.Object1 is BulletMulti)
                {
                    e.Object1.kill();
                }

                ((PlayerMulti)(e.Object2)).frameCount = 0;

                //((PlayerMulti)(e.Object2)).x = ((PlayerMulti)(e.Object2)).originalPosition.X;
                //((PlayerMulti)(e.Object2)).y = ((PlayerMulti)(e.Object2)).originalPosition.Y;

                //Console.WriteLine(e.Object2.GetType());

                ((PlayerMulti)(e.Object2)).angularVelocity = 1000;
                ((PlayerMulti)(e.Object2)).angularDrag = 450;
                ((PlayerMulti)(e.Object2)).dead = true;
                ((PlayerMulti)(e.Object2)).velocity.Y = -250;
                FlxG.quake.start(0.005f, 0.5f);

                FlxG.bloom.Visible = false;

                bloomTimer = 0.0f;

                //e.Object2.x = 10;
                //e.Object2.y = 10;

            }
            return true;
        }

        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {
            if  (e.Object1 is BulletMulti)
                e.Object1.kill();
            e.Object2.hurt(1);




            return true;
        }

        protected void onVictory(object Sender, FlxEffectCompletedEvent e)
        {
            //FlxG.music.stop();
            FlxG.state = new VictoryStateMulti();
        }



        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.bloom.Visible = true;
            //FlxG.music.stop();
            FlxG.state = new MenuState();
        }

    }
}

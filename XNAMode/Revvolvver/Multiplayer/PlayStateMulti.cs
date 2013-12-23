using System;
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

        //meta groups, to help speed up collisions
        protected FlxGroup _objects;
        protected FlxGroup _enemies;

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

        private  Color p1Color = Color.Red;
        private  Color p2Color = Color.Green;
        private  Color p3Color = Color.Blue;
        private  Color p4Color = Color.Yellow;


        override public void create()
        {
            base.create();

            ImgTech = FlxG.Content.Load<Texture2D>("Revvolvver/tech_tiles");
            ImgGibs = FlxG.Content.Load<Texture2D>("Revvolvver/gibs");
            ImgSpawnerGibs = FlxG.Content.Load<Texture2D>("Revvolvver/spawner_gibs");
            ImgNotch = FlxG.Content.Load<Texture2D>("Revvolvver/notch");

            FlxG.mouse.hide();
            reload = false;

            //get the gibs set up and out of the way
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

            //level generation needs to know about the spawners (and thusly the bots, players, etc)
            _blocks = new FlxGroup();
            _decorations = new FlxGroup();
            _bullets = new FlxGroup();
            _players = new FlxGroup();

            List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("Revvolvver/level2.oel", "level/Items");

            if (Revvolvver_Globals.PLAYERS >= 2)
            {
                _player1 = new PlayerMulti(Convert.ToInt32(actorsAttrs[0]["x"]), Convert.ToInt32(actorsAttrs[0]["y"]), _bullets.members, _littleGibs);
                _player1.controller = PlayerIndex.One;
                _player1.color = p1Color;

                FlxG._game.hud.p1HudText.scale = 3;
                FlxG._game.hud.p1HudText.x += 40;
                FlxG._game.hud.p1HudText.color = p1Color;


                _player2 = new PlayerMulti(Convert.ToInt32(actorsAttrs[1]["x"]), Convert.ToInt32(actorsAttrs[1]["y"]), _bullets.members, _littleGibs);
                _player2.controller = PlayerIndex.Two;
                _player2.color = p2Color;

                FlxG._game.hud.p2HudText.scale = 3;
                FlxG._game.hud.p2HudText.x -= 40;
                FlxG._game.hud.p2HudText.color = p2Color;

            }

            if (Revvolvver_Globals.PLAYERS >= 3)
            {
                _player3 = new PlayerMulti(Convert.ToInt32(actorsAttrs[2]["x"]), Convert.ToInt32(actorsAttrs[2]["y"]), _bullets.members, _littleGibs);
                _player3.controller = PlayerIndex.Three;
                _player3.color = p3Color;

                FlxG._game.hud.p3HudText.scale = 3;
                FlxG._game.hud.p3HudText.x += 40 ;
                FlxG._game.hud.p3HudText.y -= 20;
                FlxG._game.hud.p3HudText.color = p3Color;

            }
            if (Revvolvver_Globals.PLAYERS >= 4)
            {
                _player4 = new PlayerMulti(Convert.ToInt32(actorsAttrs[3]["x"]), Convert.ToInt32(actorsAttrs[3]["y"]), _bullets.members, _littleGibs);
                _player4.controller = PlayerIndex.Four;
                _player4.color = p4Color;

                FlxG._game.hud.p4HudText.scale = 3;
                FlxG._game.hud.p4HudText.x -= 40;
                FlxG._game.hud.p4HudText.y -= 20;
                FlxG._game.hud.p4HudText.color = p4Color;

            }

            _bots = new FlxGroup();
            _botBullets = new FlxGroup();
            _spawners = new FlxGroup();
            hudElements = new FlxGroup();

            attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Revvolvver/level2.oel", "level/NonDestructable");
            _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Revvolvver/" + attrs["tileset"]), 8, 8);
            _tileMap.collideMin = 1;
            _tileMap.collideMax = 21;
            _blocks.add(_tileMap);



            //Add bots and spawners after we add blocks to the state,
            // so that they're drawn on top of the level, and so that
            // the bots are drawn on top of both the blocks + the spawners.
            add(_spawners);
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

            //FlxG.playMusic(SndMode);
            FlxG.flash.start(new Color(0x13, 0x1c, 0x1b), 0.5f, null, false);
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
                int yp = 40;
                int xo = 40;
                

                if (i < 6)
                {
                    xp = xo + i * 32;
                    xc = p1Color;
                }
                else if (i < 12)
                {
                    xp = (int)((FlxG.width*1.6f) + (i-6) * 32);
                    xc = p2Color;
                }
                else if (i < 18)
                {
                    xp = xo + (i - 12) * 32;
                    yp = FlxG.height*2 - 60;
                    xc = p3Color;
                }
                else if (i < 24)
                {
                    xp = (int)((FlxG.width * 1.6f) + (i-18) * 32);
                    yp = FlxG.height * 2 - 60;
                    xc = p4Color;
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
                bulletHUD.play("on");
                FlxG._game.hud.hudGroup.add(bulletHUD);

            }

            //FlxG._game.hud.hudGroup.add(hudElements);






        }




        override public void update()
        {

            FlxG.setHudText(1, "Player 1: " + FlxG.scores[0].ToString());
            FlxG.setHudText(2, "Player 2: " + FlxG.scores[1].ToString());
            FlxG.setHudText(3, "Player 3: " + FlxG.scores[2].ToString());
            FlxG.setHudText(4, "Player 4: " + FlxG.scores[3].ToString());


            PlayerIndex pi;

            int os = FlxG.score;

            //FlxU.overlap(_bullets, _tileMap, destroyTileAt);


            base.update();

            //collisions with environment

            

            FlxU.collide(_blocks, _objects);
            //FlxU.overlap(_enemies, _players, overlapped);
            //FlxU.overlap(_bullets, _enemies, overlapped);
            FlxU.overlap(_bullets, _players, hitPlayer);
            FlxU.overlap(_bullets, _bullets, hitBullet);

            foreach (BulletMulti item in _bullets.members)
            {
                if (item.exploding == true)
                {
                    if (_tileMap.getTile((int)(item.x + item.tileOffsetX) / 8, (int)(item.y + item.tileOffsetY) / 8) != 0)
                    {
                        _tileMap.setTile((int)(item.x + item.tileOffsetX) / 8, (int)(item.y + item.tileOffsetY) / 8, 0, true);
                        _tileMap.setTile((int)(item.x + 9) / 8, (int)(item.y + item.tileOffsetY) / 8, 0, true);
                    }



                }
            }



            //Toggle the bounding box visibility
            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B))
                FlxG.showBounds = !FlxG.showBounds;

            if (FlxG.gamepads.isNewButtonPress(Buttons.Back) || FlxG.keys.ESCAPE )
            {
                _fading = true;
                //FlxG.play(SndHit2);
                FlxG.flash.start(new Color(0xd8, 0xeb, 0xa2), 0.5f, null, false);
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
            }

            int hi = 20;
            if ((FlxG.scores[0] > hi) || (FlxG.scores[1] > hi) || (FlxG.scores[2] > hi) || (FlxG.scores[3] > hi))
            {
                FlxG.fade.start(new Color(0xd8, 0xeb, 0xa2), 3, onVictory, false);
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

            if (_tileMap.getTile((int)e.Object2.x / 8, (int)e.Object2.y / 8) != 0)
            {
                _tileMap.setTile((int)e.Object2.x / 8, (int)e.Object2.y / 8, 0, true);
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

                if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.One && FlxG.scores[0]>0) FlxG.scores[0]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Two && FlxG.scores[1] > 0) FlxG.scores[1]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Three && FlxG.scores[2] > 0) FlxG.scores[2]--;
                else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Four && FlxG.scores[3] > 0) FlxG.scores[3]--;

                if (((BulletMulti)(e.Object1)).color == Color.White) FlxG.scores[0]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Red) FlxG.scores[1]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Teal) FlxG.scores[2]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Yellow) FlxG.scores[3]++;


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
            FlxG.music.stop();
            FlxG.state = new VictoryStateMulti();
        }



        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            //FlxG.music.stop();
            FlxG.state = new MenuState();
        }

    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

namespace XNAMode
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
        protected const string SndHit2 = "Mode/menu_hit_2";
        protected const string SndMode = "Mode/mode";
        protected const string SndCount = "Mode/countdown";
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
		
		override public void create()
		{
            base.create();

            ImgTech=FlxG.Content.Load<Texture2D>("Mode/tech_tiles");
            ImgDirtTop=FlxG.Content.Load<Texture2D>("Mode/dirt_top");
            ImgDirt=FlxG.Content.Load<Texture2D>("Mode/dirt");
            ImgNotch=FlxG.Content.Load<Texture2D>("Mode/notch");
            ImgGibs=FlxG.Content.Load<Texture2D>("Mode/gibs");
            ImgSpawnerGibs = FlxG.Content.Load<Texture2D>("Mode/spawner_gibs");

			FlxG.mouse.hide();
			reload = false;
			
			//get the gibs set up and out of the way
			_littleGibs = new FlxEmitter();
			_littleGibs.delay = 3;
			_littleGibs.setXSpeed(-150,150);
			_littleGibs.setYSpeed(-200,0);
			_littleGibs.setRotation(-720,-720);
			_littleGibs.createSprites(ImgGibs,100,true,0.5f,0.65f);
			_bigGibs = new FlxEmitter();
			_bigGibs.setXSpeed(-200,200);
			_bigGibs.setYSpeed(-300,0);
			_bigGibs.setRotation(-720,-720);
			_bigGibs.createSprites(ImgSpawnerGibs,50,true,0.5f,0.35f);
			
			//level generation needs to know about the spawners (and thusly the bots, players, etc)
			_blocks = new FlxGroup();
			_decorations = new FlxGroup();
			_bullets = new FlxGroup();
            _players = new FlxGroup();

            List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("Mode/level1.oel", "level/Items");

            if (Mode_Globals.PLAYERS >= 2)
            {
                _player1 = new PlayerMulti(Convert.ToInt32(actorsAttrs[0]["x"]), Convert.ToInt32(actorsAttrs[0]["y"]), _bullets.members, _littleGibs);
                _player1.controller = PlayerIndex.One;
                _player1.color = Color.White;

                FlxG._game.hud.p1HudText.scale = 3;
                FlxG._game.hud.p1HudText.color = Color.LightGreen;


                _player2 = new PlayerMulti(Convert.ToInt32(actorsAttrs[1]["x"]), Convert.ToInt32(actorsAttrs[1]["y"]), _bullets.members, _littleGibs);
                _player2.controller = PlayerIndex.Two;
                _player2.color = Color.Red;

                FlxG._game.hud.p2HudText.scale = 3;
                FlxG._game.hud.p2HudText.color = Color.Red;

            }

            if (Mode_Globals.PLAYERS >= 3)
            {
                _player3 = new PlayerMulti(Convert.ToInt32(actorsAttrs[2]["x"]), Convert.ToInt32(actorsAttrs[2]["y"]), _bullets.members, _littleGibs);
                _player3.controller = PlayerIndex.Three;
                _player3.color = Color.Teal;

                FlxG._game.hud.p3HudText.scale = 3;
                FlxG._game.hud.p3HudText.y -= 20;
                FlxG._game.hud.p3HudText.color = Color.Teal;

            }
            if (Mode_Globals.PLAYERS >= 4)
            {
                _player4 = new PlayerMulti(Convert.ToInt32(actorsAttrs[3]["x"]), Convert.ToInt32(actorsAttrs[3]["y"]), _bullets.members, _littleGibs);
                _player4.controller = PlayerIndex.Four;
                _player4.color = Color.Yellow;

                FlxG._game.hud.p4HudText.scale = 3;
                FlxG._game.hud.p4HudText.y -= 20;
                FlxG._game.hud.p4HudText.color = Color.Yellow;

            }

			_bots = new FlxGroup();
			_botBullets = new FlxGroup();
			_spawners = new FlxGroup();
			
            attrs = new Dictionary<string, string>();
            attrs = FlxXMLReader.readAttributesFromOelFile("Mode/level1.oel", "level/NonDestructable");
            _tileMap = new FlxTilemap();
            _tileMap.auto = FlxTilemap.STRING;
            _tileMap.loadMap(attrs["NonDestructable"], FlxG.Content.Load<Texture2D>("Mode/" + attrs["tileset"]), 8, 8);
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
			for(i = 0; i < 80; i++)
				_bullets.add(new BulletMulti());

			//add player and set up scrolling camera
			_players.add(_player1);
            _players.add(_player2);
            _players.add(_player3);
            _players.add(_player4);
            add(_players);

			FlxG.follow(_player1,2.5f);
			FlxG.followAdjust(0.5f,0.0f);
			FlxG.followBounds(0,0,FlxG.width,FlxG.height);

			
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
			_objects.add(_botBullets);
			_objects.add(_bullets);
			_objects.add(_bots);
			_objects.add(_player1);
            _objects.add(_player2);
            _objects.add(_player3);
            _objects.add(_player4);
            _objects.add(_littleGibs);
			_objects.add(_bigGibs);
			
			FlxG.playMusic(SndMode);
			FlxG.flash.start(new Color(0x13, 0x1c, 0x1b), 0.5f, null, false);
			_fading = false;

            FlxG.scores.Clear();
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);
            FlxG.scores.Add(0);

            FlxG.showHud();
            FlxG.setHudGamepadButton(0, -200, -200);

            FlxG.setHudText(1, FlxG.scores[0].ToString() );




		}

		override public void update()
		{

            FlxG.setHudText(1, FlxG.scores[0].ToString());
            FlxG.setHudText(2, FlxG.scores[1].ToString());
            FlxG.setHudText(3, FlxG.scores[2].ToString());
            FlxG.setHudText(4, FlxG.scores[3].ToString());


            PlayerIndex pi;

			int os = FlxG.score;
			
			base.update();
			
			//collisions with environment
			FlxU.collide(_blocks,_objects);
			FlxU.overlap(_enemies,_players,overlapped);
			FlxU.overlap(_bullets,_enemies,overlapped);

            FlxU.overlap(_bullets, _players, hitPlayer);
            FlxU.overlap(_bullets, _bullets, hitBullet);
			
			//Jammed message
			if(FlxG.keys.justPressed(Keys.C) && _player1.flickering())
			{
				_jamTimer = 1;
				_jamBar.visible = true;
				_jamText.visible = true;
			}
			if(_jamTimer > 0)
			{
				if(!_player1.flickering()) _jamTimer = 0;
				_jamTimer -= FlxG.elapsed;
				if(_jamTimer < 0)
				{
					_jamBar.visible = false;
					_jamText.visible = false;
				}
			}

			if(!_fading)
			{
				//Score + countdown stuffs
				if(os != FlxG.score) _scoreTimer = 2;
				_scoreTimer -= FlxG.elapsed;
				if(_scoreTimer < 0)
				{
					if(FlxG.score > 0) 
					{
						FlxG.play(SndCount);
						if(FlxG.score > 100) FlxG.score -= 100;
						else { FlxG.score = 0; _player1.kill(); }
						_scoreTimer = 1;
						if(FlxG.score < 600)
							FlxG.play(SndCount);
						if(FlxG.score < 500)
							FlxG.play(SndCount);
						if(FlxG.score < 400)
							FlxG.play(SndCount);
						if(FlxG.score < 300)
							FlxG.play(SndCount);
						if(FlxG.score < 200)
							FlxG.play(SndCount);
					}
				}
			}
			
			//actually update score text if it changed
			if(os != FlxG.score)
			{
				if(_player1.dead) FlxG.score = 0;
				_score.text = FlxG.score.ToString();
			}
			
			if(reload)
				FlxG.state = new PlayState();
			
			//Toggle the bounding box visibility
			if(FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B))
				FlxG.showBounds = !FlxG.showBounds;

            if (FlxG.gamepads.isNewButtonPress(Buttons.Back, FlxG.controllingPlayer, out pi))
            {
                _fading = true;
                FlxG.play(SndHit2);
                FlxG.flash.start(new Color(0xd8, 0xeb, 0xa2), 0.5f, null, false);
                FlxG.fade.start(new Color(0x13, 0x1c, 0x1b), 1f, onFade, false);
            }

            int hi = 10;
            if ( (FlxG.scores[0] > hi) || (FlxG.scores[1] > hi) || (FlxG.scores[2] > hi) || (FlxG.scores[3] > hi))
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


        protected bool hitPlayer(object Sender, FlxSpriteCollisionEvent e)
        {
            if (((FlxSprite)(e.Object1)).color != ((FlxSprite)(e.Object2)).color && !((PlayerMulti)(e.Object2)).dead)
            {

                //if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.One) FlxG.scores[0]--;
                //else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Two) FlxG.scores[1]--;
                //else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Three) FlxG.scores[2]--;
                //else if (((PlayerMulti)(e.Object2)).controller == PlayerIndex.Four) FlxG.scores[3]--;

                if (((BulletMulti)(e.Object1)).color == Color.White) FlxG.scores[0]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Red) FlxG.scores[1]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Teal) FlxG.scores[2]++;
                else if (((BulletMulti)(e.Object1)).color == Color.Yellow) FlxG.scores[3]++;

                if ((e.Object1 is BotBullet) || (e.Object1 is BulletMulti))
                {
                    e.Object1.kill();
                }

                ((PlayerMulti)(e.Object2)).frameCount = 0;

                //((PlayerMulti)(e.Object2)).x = ((PlayerMulti)(e.Object2)).originalPosition.X;
                //((PlayerMulti)(e.Object2)).y = ((PlayerMulti)(e.Object2)).originalPosition.Y;

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
			if((e.Object1 is BotBullet) || (e.Object1 is BulletMulti))
				e.Object1.kill();
			e.Object2.hurt(1);




            return true;
		}
		
		protected void onVictory(object Sender, FlxEffectCompletedEvent e)
		{
			FlxG.music.stop();
			FlxG.state = new VictoryStateMulti();
		}
		
		/// <summary>
        /// Just plops down a spawner and some blocks - haphazard and crappy atm but functional!
		/// </summary>
		/// <param name="RX">The room to build in X</param>
        /// <param name="RY">The room to build in Y</param>
        protected void buildRoom(int RX, int RY)
        {
            buildRoom(RX, RY, false);
        }

        /// <summary>
        /// Just plops down a spawner and some blocks - haphazard and crappy atm but functional!
        /// </summary>
        /// <param name="RX"></param>
        /// <param name="RY"></param>
        /// <param name="Spawners">Whether or not to include a Spawner.</param>
		protected void buildRoom(int RX, int RY, bool Spawners)
		{
			//first place the spawn point (if necessary)
			int rw = 20;
			int sx = 0;
			int sy = 0;
			if(Spawners)
			{
				sx = 2+(int)(FlxU.random()*(rw-7));
				sy = 2+(int)(FlxU.random()*(rw-7));
			}
			
			//then place a bunch of blocks
			int numBlocks = 3+(int)(FlxU.random()*4);
			if(!Spawners) numBlocks++;
			int maxW = 10;
			int minW = 2;
			int maxH = 6;
			int minH = 1;
			int bx;
			int by;
			int bw;
			int bh;
			bool check;
			for(int i = 0; i < numBlocks; i++)
			{
				check = false;
				do
				{
					//keep generating different specs if they overlap the spawner
					bw = minW + (int)(FlxU.random()*(maxW-minW));
                    bh = minH + (int)(FlxU.random() * (maxH - minH));
                    bx = -1 + (int)(FlxU.random() * (rw + 1 - bw));
                    by = -1 + (int)(FlxU.random() * (rw + 1 - bh));
					if(Spawners)
						check = ((sx>bx+bw) || (sx+3<bx) || (sy>by+bh) || (sy+3<by));
					else
						check = true;
				} while(!check);
				
				FlxTileblock b;
				
				b = new FlxTileblock(RX+bx*8,RY+by*8,bw*8,bh*8);
				b.loadTiles(ImgTech);
				_blocks.add(b);
				
				//If the block has room, add some non-colliding "dirt" graphics for variety
				if((bw >= 4) && (bh >= 5))
				{
					b = new FlxTileblock(RX+bx*8+8,RY+by*8,bw*8-16,8);
					b.loadTiles(ImgDirtTop);
					_decorations.add(b);
					
					b = new FlxTileblock(RX+bx*8+8,RY+by*8+8,bw*8-16,bh*8-24);
					b.loadTiles(ImgDirt);
					_decorations.add(b);
				}
			}
			
			//Finally actually add the spawner
			//if(Spawners)
				//_spawners.add(new Spawner(RX+sx*8,RY+sy*8,_bigGibs,_bots,_botBullets.members,_littleGibs,_player1));
		}

        private void onFade(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.music.stop();
            FlxG.state = new MenuState();
        }

    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    /// <summary>
    /// 
    /// </summary>
    public class XMLPlayState : FlxState
    {
        protected Texture2D ImgDirt;

        private Texture2D Bubbles;
        private Texture2D DecorTex;
        private FlxEmitter _bubbles;
        private FlxTileblock rotatore;
        private FlxTilemap tiles;
        private FlxTilemap decorations;

        private Player _player;

        override public void create()
        {
            base.create();

            //FlxG.backColor = Color.SlateGray;

            ImgDirt = FlxG.Content.Load<Texture2D>("Mode/dirt");


            Bubbles = FlxG.Content.Load<Texture2D>("Mode/bubble");
            _bubbles = new FlxEmitter();
            _bubbles.x = 0;
            _bubbles.y = FlxG.height - 20;
            _bubbles.width = FlxG.width;
            _bubbles.height = 20;
            _bubbles.delay = 3.0f;
            _bubbles.setXSpeed(-200, 200);
            _bubbles.setYSpeed(-2, 2);
            _bubbles.setRotation(0, 0);
            _bubbles.gravity = -98;
            _bubbles.createSprites(Bubbles, 1500, true, 1.0f, 1.0f);
            _bubbles.start(false, 0.1f, 100);

            //add(_bubbles);



            // Good example of reading a level

            /*
            XElement xelement = XElement.Load("level1.oel");

            FlxG.write("List of all rects");
            foreach (XElement xEle in xelement.Descendants("rect"))
            {
                FlxG.write("Rect: " + (string)xEle.Attribute("x") + " " + (string)xEle.Attribute("y") + " " + (string)xEle.Attribute("w") + " " + (string)xEle.Attribute("h"));
                int x = (int)xEle.Attribute("x");
                int y = (int)xEle.Attribute("y");
                int w = (int)xEle.Attribute("w");
                int h = (int)xEle.Attribute("h");


                FlxTileblock b = new FlxTileblock(x, y, w, h);

                b.loadTiles(ImgDirt);

                //add(b);




            }
            */


            rotatore = new FlxTileblock(30,30,120,50);

            rotatore.loadTiles(ImgDirt, 16,16,0);

            ////add(rotatore);





            //_bigGibs = new FlxEmitter();
            //_bigGibs.setXSpeed(-200, 200);
            //_bigGibs.setYSpeed(-300, 0);
            //_bigGibs.setRotation(-720, -720);

            //_bigGibs.createSprites(ImgSpawnerGibs, 50, true, 0.5f, 0.35f);


            FlxText t = new FlxText(10, 50, 200);
            t.text = "Initials Video Games";
            t.scale = 3.0f;
            //add(t);





            FlxCaveGenerator cav = new FlxCaveGenerator(40,22);
            cav.initWallRatio = 0.48f;
            cav.numSmoothingIterations = 5;

            cav.genInitMatrix(40, 22);
            
            // works!
            //int[,] matr = cav.generateCaveLevel();


            int[,] matr = cav.generateCaveLevel(2,2,2,0,1,1,1,0);


            string newMap = cav.convertMultiArrayToString(matr);

            //FlxG.write(newMap);



            tiles = new FlxTilemap();

            tiles.auto = FlxTilemap.AUTO;
            
            //tiles.loadMap("1,0,0,0,0,0,0,1,1,0\n1,1,0,0,0,0,0,1,1,1\n0,1,0,0,0,0,0,1,1,0\n0,0,0,1,0,0,0,1,1,1\n0,0,0,1,0,0,0,1,1,0\n0,0,0,0,0,0,0,1,1,1\n", FlxTilemap.ImgAuto);

            tiles.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"));

            add(tiles);



            int[,] decr = cav.createDecorationsMap(matr, 0.8f);
            string newDec = cav.convertMultiArrayToString(decr);

            DecorTex = FlxG.Content.Load<Texture2D>("initials/decorations_16x16");


            decorations = new FlxTilemap();
            decorations.auto = FlxTilemap.RANDOM;
            decorations.randomLimit = (int)DecorTex.Width / 16;

            decorations.loadMap(newDec, DecorTex ,16,16);
            add(decorations);




            _player = new Player(55, 1, null, null);
            //add(_player);
            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            


            
        }

        override public void update()
        {


            Vector2 np = FlxU.rotatePoint(rotatore.x, rotatore.y, 80, 80, 0.1f);
            rotatore.x = np.X;
            rotatore.y = np.Y;

            FlxU.collide(_player, tiles);

            //FlxG.write(FlxG.gamepads.isNewThumbstickDown(FlxG.controllingPlayer));

            float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

            float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

            float rotation = (float)Math.Atan2(rightX, rightY);
            rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);

            //FlxG.write(rotation);




            /*
            if (!flickering() && (_bulletInterval > 8) &&
                    (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickDown, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickUp, FlxG.controllingPlayer, out pi)))
            {

                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (rightY < -0.75)
                {
                    velocity.Y -= 36;
                }

                float rotation = (float)Math.Atan2(rightX, rightY);
                rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);


                _bulletInterval = 0;
                rightX *= 100;
                rightY *= -100;



                int bXVel = 0;
                int bYVel = 0;
                int bX = (int)x;
                int bY = (int)y;

                bY -= (int)_bullets[_curBullet].height - 4;
                bYVel = (int)rightY;
                bX -= (int)_bullets[_curBullet].width - 4;
                bXVel = (int)rightX;



                ((Bullet)(_bullets[_curBullet])).shoot(bX, bY, bXVel, bYVel);
                ((Bullet)(_bullets[_curBullet])).angle = rotation;

                //System.Diagnostics.Trace.WriteLine(rightX + " .. ry " + rightY + " " + rotation + " " + MathHelper.ToDegrees(rotation));

                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;
            }
             * 
             */

            if (FlxG.keys.A)
            {
                FlxG.state = new XMLPlayState();
            }

            /*
             * 			highlightBox.x = Math.floor(FlxG.mouse.x / TILE_WIDTH) * TILE_WIDTH;
			highlightBox.y = Math.floor(FlxG.mouse.y / TILE_HEIGHT) * TILE_HEIGHT;

			if (FlxG.mouse.pressed())
			{
				// FlxTilemaps can be manually edited at runtime as well.
				// Setting a tile to 0 removes it, and setting it to anything else will place a tile.
				// If auto map is on, the map will automatically update all surrounding tiles.
				collisionMap.setTile(FlxG.mouse.x / TILE_WIDTH, FlxG.mouse.y / TILE_HEIGHT, FlxG.keys.SHIFT?0:1);
			}
             * 
             */ 
            if (FlxG.mouse.pressed())
            {

                tiles.setTile((int)FlxG.mouse.x / 16, (int)FlxG.mouse.y / 16, 0, true);
                decorations.setTile((int)FlxG.mouse.x / 16, ((int)FlxG.mouse.y / 16) - 1, 0, true);
                
            }

            if (FlxG.keys.justPressed(Keys.K))
            {
                FlxOnlineStatCounter.sendStats("hawksnest", "marksman", 1);

                FlxG.write(FlxOnlineStatCounter.lastRecievedStat);
            }
            if (FlxG.keys.justPressed(Keys.L))
            {
                FlxOnlineStatCounter.getStatsForLevel("hawksnest", "marksman", 1);

                FlxG.write(FlxOnlineStatCounter.lastRecievedStat);

            }
            if (FlxG.keys.justPressed(Keys.J))
            {
                FlxOnlineStatCounter.getAllStats("hawksnest");

            }           


            base.update();
        }


    }
}

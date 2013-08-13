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
    public class XMLPlayState : FlxState
    {
        protected Texture2D ImgDirt;

        private Texture2D Bubbles;
        private FlxEmitter _bubbles;
        private FlxTileblock rotatore;
        private FlxTilemap tiles;

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

            XElement xelement = XElement.Load("level1.oel");

            Console.WriteLine("List of all rects");
            foreach (XElement xEle in xelement.Descendants("rect"))
            {
                Console.WriteLine("Rect: " + (string)xEle.Attribute("x") + " " + (string)xEle.Attribute("y") + " " + (string)xEle.Attribute("w") + " " + (string)xEle.Attribute("h"));
                int x = (int)xEle.Attribute("x");
                int y = (int)xEle.Attribute("y");
                int w = (int)xEle.Attribute("w");
                int h = (int)xEle.Attribute("h");


                FlxTileblock b = new FlxTileblock(x, y, w, h);

                b.loadTiles(ImgDirt);

                //add(b);




            }


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





            FlxCaveGenerator cav = new FlxCaveGenerator(70,70);
            cav.genInitMatrix(70, 70);
            int[,] matr = cav.generateCaveLevel();

            string newMap = cav.convertMultiArrayToString(matr);

            //Console.WriteLine(newMap);



            tiles = new FlxTilemap();

            tiles.auto = FlxTilemap.AUTO;
            
            //tiles.loadMap("1,0,0,0,0,0,0,1,1,0\n1,1,0,0,0,0,0,1,1,1\n0,1,0,0,0,0,0,1,1,0\n0,0,0,1,0,0,0,1,1,1\n0,0,0,1,0,0,0,1,1,0\n0,0,0,0,0,0,0,1,1,1\n", FlxTilemap.ImgAuto);

            tiles.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"));

            add(tiles);

            _player = new Player(55, 1, null, null);
            //add(_player);


            
        }

        override public void update()
        {


            Vector2 np = FlxU.rotatePoint(rotatore.x, rotatore.y, 80, 80, 0.1f);
            rotatore.x = np.X;
            rotatore.y = np.Y;

            FlxU.collide(_player, tiles);

            PlayerIndex pi;
            //Console.WriteLine(FlxG.gamepads.isNewThumbstickDown(FlxG.controllingPlayer));

            float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

            float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

            float rotation = (float)Math.Atan2(rightX, rightY);
            rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);

            //Console.WriteLine(rotation);




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


            base.update();
        }


    }
}

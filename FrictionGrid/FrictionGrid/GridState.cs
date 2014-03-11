using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FrictionGrid
{
    public class GridState : FlxState
    {

        private FlxSprite grid;
        private FlxGroup grids;
        private FlxSprite player;
        private FlxGroup bullets;
        private FlxGroup tiles;
        private FlxGroup enemies;

        private float wideMult;
        private float speedMult;

        override public void create()
        {
            base.create();

            wideMult = 1;
            speedMult = 1;

            FlxSprite bg = new FlxSprite(0, 0);
            bg.createGraphic(FlxG.width, FlxG.height, Color.Black);
            bg.setScrollFactors(0, 0);

            add(bg);



            grids = new FlxGroup();
            tiles = new FlxGroup();



            for (int i = 0; i < 25; i++)
            {
                grid = new FlxSprite(i * 200, 0);
                grid.createGraphic(4, 10, Color.Purple);
                grids.add(grid);
                grid.debugName = "w";


            }

            for (int i = 0; i < 25; i++)
            {
                grid = new FlxSprite(0, i * 200);
                grid.createGraphic(10, 4, Color.Purple);
                grids.add(grid);
                grid.debugName = "h";
            }

            //for (int x = 0; x < 25; x++)
            //{
            //    for (int y = 0; y < 25; y++)
            //    {
            //        FlxSprite tile = new FlxSprite(x * 200, y * 200);
            //        tile.createGraphic(200, 200, Color.LemonChiffon);
            //        tiles.add(tile);
            //    }
            //}

            add(tiles);
            add(grids);

            FlxG.bloom.Visible = true;
            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[3];

            player = new FlxSprite(150, 150);
            //player.createGraphic(30, 10, Color.HotPink);
            player.loadGraphic(FlxG.Content.Load<Texture2D>("frictionGrid/circle_40x40"), true, false, 40, 40);
            player.color = Color.Turquoise;
            player.maxAngular = 120;
            player.angularDrag = 400;
            player.maxThrust = 500;
            player.drag.X = 2480;
            player.drag.Y = 2480;
            player.angle = 180;
            player.maxVelocity = new Vector2(400, 400);
            add(player);

            enemies = new FlxGroup();

            for (int x = 0; x < 20; x++)
			{
                FlxSprite enemy = new FlxSprite(FlxU.random(0, 2000), FlxU.random(0, 2000));
                //player.createGraphic(30, 10, Color.HotPink);
                enemy.loadGraphic(FlxG.Content.Load<Texture2D>("frictionGrid/circle_40x40"), true, false, 40, 40);
                enemy.color = Color.Red;
                enemies.add(enemy);
			}
            add(enemies);

            bullets = new FlxGroup();
            for (int i = 0; i < 250; i++)
            {
                FlxSprite bullet = new FlxSprite(0, 0);
                bullet.createGraphic(4, 4, Color.AliceBlue);
                bullet.dead = true;
                bullets.add(bullet);
            }
            add(bullets);

            FlxG.follow(player, 11.0f);
            FlxG.followBounds(0, 0, 2500, 2500);


        }

        override public void update()
        {
            if (player.x < 0 || player.y < 0 || player.x > 2500 || player.y > 2500)
            {
                FlxG.quake.start(0.035f, 1.0f);

                //player.angle += 180;
                player.x = 1250;
                player.y = 1250;
            }

            foreach (FlxSprite item in grids.members)
            {
                if (FlxG.elapsedTotal < 335)
                {
                    if (item.debugName == "w")
                        item.height += 24;
                    if (item.debugName == "h")
                        item.width += 12;
                }
            }
            //player.thrust +=15;

            if (FlxG.keys.A)
            {
                player.velocity.X -= 150;
            }
            if (FlxG.keys.D)
            {
                player.velocity.X += 150;
            }
            if (FlxG.keys.W)
            {
                player.velocity.Y -= 150;
            }
            if (FlxG.keys.S)
            {
                player.velocity.Y += 150;
            }

            if (FlxG.keys.LEFT)
            {
                FlxSprite b = (FlxSprite)(bullets.getFirstDead());
                if (b != null)
                {
                    b.velocity.X = FlxU.random(-600 * speedMult, -500 * speedMult);
                    b.velocity.Y = FlxU.random(-20 * wideMult, 20 * wideMult);
                    b.x = player.x;
                    b.y = player.y;
                    b.dead = false;
                }

            }
            if (FlxG.keys.RIGHT)
            {
                FlxSprite b = (FlxSprite)(bullets.getFirstDead());
                if (b != null)
                {
                    b.velocity.X = FlxU.random(500 * speedMult, 600 * speedMult);
                    b.velocity.Y = FlxU.random(-20 * wideMult, 20 * wideMult);
                    b.velocity.Y = 0;
                    b.x = player.x;
                    b.y = player.y;
                    b.dead = false;
                }
            }
            if (FlxG.keys.UP)
            {
                FlxSprite b = (FlxSprite)(bullets.getFirstDead());
                if (b != null)
                {
                    b.velocity.X = 0;
                    b.velocity.Y = -600;
                    b.x = player.x;
                    b.y = player.y;
                    b.dead = false;
                }
            }
            if (FlxG.keys.DOWN)
            {
                FlxSprite b = (FlxSprite)(bullets.getFirstDead());
                if (b != null)
                {
                    b.velocity.X = 0;
                    b.velocity.Y = 600;
                    b.x = player.x;
                    b.y = player.y;
                    b.dead = false;
                }
            }

            foreach (var item in bullets.members)
            {
                if (item.x < 0 || item.y < 0 || item.x > 2500 || item.y > 2500)
                {
                    item.dead = true;
                }
            }

            if (FlxU.overlap(bullets, enemies, null))
            {
                speedMult++;
                wideMult++;
            }

            int live = enemies.countLiving();

            if (live == 0)
            {
                FlxG.angle += 5;
            }



            base.update();
        }

        protected bool genericOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxSprite)(e.Object1)).alpha = 0.05f;
            return true;
        }

    }
}

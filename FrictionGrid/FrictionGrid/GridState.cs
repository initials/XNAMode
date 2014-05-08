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
                grid = new FlxSprite(i * 8, 0);
                grid.createGraphic(1, 1, Color.Purple);
                grids.add(grid);
                grid.debugName = "w";


            }

            for (int i = 0; i < 25; i++)
            {
                grid = new FlxSprite(0, i * 8);
                grid.createGraphic(1, 1, Color.Purple);
                grids.add(grid);
                grid.debugName = "h";
            }

            add(tiles);
            add(grids);

            player = new FlxSprite(12, 12);
            player.createGraphic(1, 1, Color.HotPink);
            player.color = Color.Turquoise;
            player.maxAngular = 120;
            player.angularDrag = 400;
            player.maxThrust = 500;
            player.drag.X = 24;
            player.drag.Y = 24;
            player.angle = 180;
            player.maxVelocity = new Vector2(400, 400);
            add(player);

            enemies = new FlxGroup();

            for (int x = 0; x < 3; x++)
			{
                FlxSprite enemy = new FlxSprite(FlxU.random(0, 32), FlxU.random(0, 32));
                enemy.createGraphic(1, 1, Color.HotPink);
                enemies.add(enemy);
			}
            add(enemies);

            bullets = new FlxGroup();
            for (int i = 0; i < 250; i++)
            {
                FlxSprite bullet = new FlxSprite(0, 0);
                bullet.createGraphic(1, 1, Color.AliceBlue);
                bullet.dead = true;
                bullets.add(bullet);
            }
            add(bullets);

            FlxG.follow(player, 11.0f);
            FlxG.followBounds(0, 0, 97, 97);


        }

        override public void update()
        {

            if (player.x < 0 || player.y < 0 || player.x > 96 || player.y > 96)
            {
                FlxG.quake.start(0.035f, 1.0f);

                //player.angle += 180;
                player.x = 96 / 2;
                player.y = 96 / 2;
            }

            foreach (FlxSprite item in grids.members)
            {
                if (FlxG.elapsedTotal < 335)
                {
                    if (item.debugName == "w")
                        item.height += 1;
                    if (item.debugName == "h")
                        item.width += 1;
                }
            }
            //player.thrust +=15;

            if (FlxG.keys.A)
            {
                player.x -= 1;
            }
            if (FlxG.keys.D)
            {
                player.x += 1;
            }
            if (FlxG.keys.W)
            {
                player.y -= 1;
            }
            if (FlxG.keys.S)
            {
                player.y += 1;
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
                    b.velocity.Y = -13;
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
                    b.velocity.Y = 13;
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

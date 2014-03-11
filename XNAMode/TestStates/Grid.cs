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
    public class Grid : FlxState
    {

        FlxSprite grid;
        FlxGroup grids;
        FlxSprite player;
        FlxGroup bullets;
        FlxGroup tiles;

        override public void create()
        {
            base.create();

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
                grid.alpha = 0.75f;
                grids.add(grid);
                grid.debugName = "w";

                
            }

            for (int i = 0; i < 25; i++)
            {
                grid = new FlxSprite(0, i * 200);
                grid.createGraphic(10, 4, Color.Purple);
                grid.alpha = 0.75f;
                grids.add(grid);
                grid.debugName = "h";
            }

            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    FlxSprite tile = new FlxSprite(x * 200, y * 200);
                    tile.createGraphic(200, 200, Color.LemonChiffon);
                    tiles.add(tile);
                }
            }
            add(tiles);
            add(grids);

            FlxG.bloom.Visible = true;
            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[3];

            player = new FlxSprite(150, 150);
            //player.createGraphic(30, 10, Color.HotPink);
            player.loadGraphic(FlxG.Content.Load<Texture2D>("frictionGrid/oryx_roguelike_16x24"),true, false,16,24);
            player.frame = 100;
            player.maxAngular = 120;
            player.angularDrag = 400;
            player.maxThrust = 500;
            player.drag.X = 2480;
            player.drag.Y = 2480;
            player.angle = 180;
            player.maxVelocity = new Vector2(400, 400);
            add(player);

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
            if (player.x < 0 || player.y < 0 || player.x>2500 || player.y > 2500)
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
                    b.velocity.X = -600;
                    b.velocity.Y = 0;
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
                    b.velocity.X = 600;
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

            FlxU.overlap(tiles, player, genericOverlap);

            foreach (FlxSprite x in tiles.members)
            {
                if (x.alpha <= 0)
                    x.alpha =0;
                else
                    x.alpha -= 0.05f;
            }
            //if (FlxG.keys.M)
            //{
            //    player.angularVelocity = 350; 
            //    if (player.thrust > 0)
            //        player.thrust -= 50;
            //}
            //else if (FlxG.keys.Z)
            //{
            //    player.angularVelocity = -350; 
            //    if (player.thrust > 0)
            //        player.thrust -= 50;
            //}
            //else if (FlxG.keys.SPACE)
            //{
            //    player.thrust = 0;
            //}
            //else
            //{
            //    //player.thrust = 0;
            //}
            base.update();
        }

        protected bool genericOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxSprite)(e.Object1)).alpha = 1.0f;



            return true;
        }

    }
}

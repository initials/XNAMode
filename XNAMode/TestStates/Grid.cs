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

        override public void create()
        {
            base.create();

            FlxSprite bg = new FlxSprite(0, 0);
            bg.createGraphic(FlxG.width, FlxG.height, Color.Black);
            bg.setScrollFactors(0, 0);

            add(bg);

            

            grids = new FlxGroup();

            for (int i = 0; i < 50; i++)
            {
                grid = new FlxSprite(i * 50, 0);
                grid.createGraphic(2, 10, Color.Purple);
                grids.add(grid);
                grid.debugName = "w";
            }

            for (int i = 0; i < 50; i++)
            {
                grid = new FlxSprite(0, i * 50);
                grid.createGraphic(10, 2, Color.Purple);
                grids.add(grid);
                grid.debugName = "h";
            }

            add(grids);

            FlxG.bloom.Visible = true;
            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1];

            player = new FlxSprite(150, 150);
            player.createGraphic(30, 10, Color.HotPink);
            player.thrust = 300;
            player.maxAngular = 120;
            player.angularDrag = 400;
            player.maxThrust = 500;
            player.drag.X = 480;
            player.drag.Y = 480;
            player.angle = 180;
            add(player);

            FlxG.follow(player, 11.0f);
            FlxG.followBounds(0, 0, int.MaxValue, int.MaxValue);


        }

        override public void update()
        {
            if (player.x < 0 || player.y < 0)
            {
                //player.angle += 180;
                player.x += 20;
                player.y += 20;
            }

            foreach (FlxSprite item in grids.members)
            {
                if (FlxG.elapsedTotal < 35)
                {
                    if (item.debugName == "w")
                        item.height += 10;
                    if (item.debugName == "h")
                        item.width += 5;
                }
            }
            player.thrust +=15;
            if (FlxG.keys.Z)
            {
                player.angle += 4.5f;
                if (player.thrust > 200)
                    player.thrust -= 200;
                else player.thrust = 0;
            }
            else if (FlxG.keys.M)
            {
                player.angle -= 4.5f;
                if (player.thrust > 200)
                    player.thrust -= 200;
                else player.thrust = 0;
            }
            else if (FlxG.keys.SPACE)
            {
                player.thrust = 0;
            }
            else
            {
                //player.thrust = 0;
            }
            base.update();
        }


    }
}

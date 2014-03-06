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

            player = new FlxSprite(50, 50);
            player.createGraphic(10, 10, Color.HotPink);
            add(player);

        }

        override public void update()
        {

            foreach (FlxSprite item in grids.members)
            {
                if (FlxG.elapsedTotal < 5)
                {
                    if (item.debugName == "w")
                        item.height += 5;
                    if (item.debugName == "h")
                        item.width += 10;
                }
            }

            if (FlxG.keys.Z)
            {
                player.angle += 2.5f;
                player.thrust = 510;
            }
            else if (FlxG.keys.M)
            {
                player.angle -= 2.5f;
                player.thrust = 510;
            }
            else if (FlxG.keys.SPACE)
            {
                player.thrust = 5510;
            }
            else
            {
                player.thrust = 0;
            }
            base.update();
        }


    }
}

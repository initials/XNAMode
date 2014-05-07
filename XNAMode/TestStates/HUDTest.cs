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
    public class HUDTest : FlxState
    {
        FlxSprite grid;
        FlxGroup grids;
        FlxSprite player;
        FlxGroup bullets;
        FlxGroup tiles;


        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));


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

            add(grids);



            player = new FlxSprite(40, 40);
            //player.createGraphic(30, 10, Color.HotPink);
            player.loadGraphic(FlxG.Content.Load<Texture2D>("frictionGrid/circle_40x40"), true, false, 40, 40);
            player.maxAngular = 120;
            player.angularDrag = 400;
            player.maxThrust = 500;
            player.drag.X = 2480;
            player.drag.Y = 2480;
            player.angle = 180;
            player.maxVelocity = new Vector2(400, 400);
            add(player);


            FlxG.follow(player, 11.0f);
            FlxG.followBounds(0, 0, 2500, 2500);

        }

        override public void update()
        {
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

            FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_KEYBOARD_DIRECTION, FlxHud.Keyboard_Arrow_Down, player.getScreenXY().X * FlxG.zoom, player.getScreenXY().Y * FlxG.zoom);


            FlxG.setHudText(1, "Mouse Screen X/Y: " + FlxG.mouse.screenX.ToString() + " " + FlxG.mouse.screenY.ToString());
            FlxG.setHudText(2, "Mouse  X/Y: " + FlxG.mouse.x.ToString() + " " + FlxG.mouse.y.ToString());
            FlxG.setHudText(3, "Zoom: " + FlxG.zoom);

            base.update();
        }


    }
}

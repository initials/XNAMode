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
    public class RotateState : FlxState
    {

        FlxSprite bg;
        FlxSprite bg1;
        FlxSprite bg2;
        FlxSprite bg3;


        override public void create()
        {
            base.create();

            bg = new FlxSprite(40, 40);
            bg.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg.play("first");
            //bg.angularVelocity = 110;
            bg.width = 28;
            bg.height = 28;
            bg.setOffset(2, 2);
            bg.debugName = "toybox";
            add(bg);

            bg1 = new FlxSprite(140, 40);
            bg1.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg1.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg1.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg1.play("first");
            bg1.width = 4;
            bg1.height = 20;
            bg1.setOffset(14, 12);
            //bg1.angularVelocity = 110;
            add(bg1);


            bg2 = new FlxSprite(140, 140);
            bg2.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg2.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg2.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg2.play("first");
            bg2.width = 4;
            bg2.height = 20;
            bg2.setOffset(0, 0);
            add(bg2);

            bg3 = new FlxSprite(40, 140);
            bg3.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg3.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg3.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg3.play("first");
            bg3.width = 4;
            bg3.height = 20;
            bg3.setOffset(4, 4);
            add(bg3);



            FlxG.showBounds = true;

            FlxG.showHud();



        }

        override public void update()
        {
            string values = String.Format("W:{0} H: {1} Offset.X {2} Offset.Y {3}", bg.width, bg.height, bg.offset.X, bg.offset.Y);

            FlxG._game.hud.p1HudText.text = values;

            if (FlxG.keys.justPressed(Keys.F))
            {
                bg.width /= 2;
                bg.adjustOrigin();
            }
            if (FlxG.keys.justPressed(Keys.H))
            {
                bg.width *= 2;
                bg.adjustOrigin();
            }
            if (FlxG.keys.justPressed(Keys.G))
            {
                bg.height /= 2;
                bg.adjustOrigin();
            }
            if (FlxG.keys.justPressed(Keys.T))
            {
                bg.height *= 2;
                bg.adjustOrigin();
            }


            if (FlxG.keys.D)
            {
                bg.offset.X++;
                bg1.offset.X++;
            }
            if (FlxG.keys.A)
            {
                bg.offset.X--;
                bg1.offset.X--;
            }
            if (FlxG.keys.W)
            {
                bg.offset.Y++;
                bg1.offset.Y++;
            }
            if (FlxG.keys.S)
            {
                bg.offset.Y--;
                bg1.offset.Y--;
            }
            if (FlxG.keys.justPressed(Keys.E))
            {
                bg.angularVelocity = 0;
                bg1.angularVelocity = 0;
                bg2.angularVelocity = 0;
                bg3.angularVelocity = 0;

                bg.angle = 0;
                bg1.angle = 0;
                bg2.angle = 0;
                bg3.angle = 0;
            }
            if (FlxG.keys.justPressed(Keys.R))
            {
                bg.angularVelocity = 100;
                bg1.angularVelocity = 100;
                bg2.angularVelocity = 100;
                bg3.angularVelocity = 100;
            }



            base.update();
        }


    }
}

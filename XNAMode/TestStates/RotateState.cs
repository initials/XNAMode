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
            bg.angularVelocity = 110;
            bg.debugName = "toybox";
            add(bg);

            bg1 = new FlxSprite(140, 40);
            bg1.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg1.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg1.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg1.play("first");
            bg1.width = 12;
            bg1.height = 20;
            bg1.offset.X = 10;
            bg1.offset.Y = 12;
            add(bg1);


            //bg2 = new FlxSprite(140, 90);
            //bg2.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            //bg2.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            //bg2.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            //bg2.play("first");
            //bg2.angularVelocity = 110;
            //bg2.width = 32;
            //bg2.height = 32;
            //bg2.offset.X = 22;
            //bg2.offset.Y = 22;
            //add(bg2);

            //bg3 = new FlxSprite(40, 90);
            //bg3.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            //bg3.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            //bg3.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            //bg3.play("first");
            //bg3.angularVelocity = 110;
            //bg3.width = 10;
            //bg3.height = 10;
            ////bg.origin.X -= 10;
            ////bg.origin.Y -= 10;
            
            //add(bg3);

            FlxG.showBounds = true;

            FlxG.showHud();



        }

        override public void update()
        {
            string values = String.Format("W:{0} H: {1} Offset.X {2} Offset.Y {3}", bg.width, bg.height, bg.offset.X, bg.offset.Y);

            FlxG._game.hud.p1HudText.text = values;

            if (FlxG.keys.Q)
            {
                bg.width++;
            }
            if (FlxG.keys.W)
            {
                bg.width--;
            }
            if (FlxG.keys.A)
            {
                bg.height++;
            }
            if (FlxG.keys.S)
            {
                bg.height--;
            }


            if (FlxG.keys.E)
            {
                bg.offset.X++;
            }
            if (FlxG.keys.R)
            {
                bg.offset.X--;
            }
            if (FlxG.keys.D)
            {
                bg.offset.Y++;
            }
            if (FlxG.keys.F)
            {
                bg.offset.Y--;
            }
            if (FlxG.keys.T)
            {
                bg.width = 32;
                bg.height = 32;
                bg.offset.X = 0;
                bg.offset.Y = 0;
            }



            base.update();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;


namespace FourChambers
{
    public class FlxBarTestState : FlxState
    {
        
        public FlxSprite bg;
        public FlxSprite bg2;
        public FlxSprite bg3;

        public FlxBar bgh;
        public FlxBar bg2h;
        public FlxBar bg3h;

        
        public FlxSprite spaceman;

        override public void create()
        {
            base.create();
            FlxG.hideHud();

            bg = new FlxSprite(0, 0);
            bg.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/texture_placement_small"), true, false, 32, 32);
            bg.addAnimation("first", new int[] { 0,1,2,0,1,2,0,1,2}, 3);
            bg.addAnimation("second", new int[] { 0, 11, 12, 22, 32, 45,1,2,3,4 }, 3);
            bg.play("second");
            //bg.velocity.Y = 20;
            bg.x = 0;
            bg.y = 100;
            bg.health = 10;
            add(bg);
            bg.color = Color.Red;

            bg2 = new FlxSprite(0, 0);
            bg2.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/texture_placement_small"), true, false, 32, 32);
            bg2.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg2.addAnimation("second", new int[] { 0, 11, 12, 22, 32, 45, 1, 2, 3, 4 }, 5);
            bg2.play("second");
            //bg.velocity.Y = 20;
            bg2.x = 100;
            bg2.y = 100;
            bg2.health = 100;
            add(bg2);

            bg3 = new FlxSprite(0, 0);
            bg3.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/texture_placement_small"), true, false, 32, 32);
            bg3.addAnimation("first", new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 }, 3);
            bg3.addAnimation("second", new int[] { 0, 11, 12, 22, 32, 45, 1, 2, 3, 4 }, 7);
            bg3.play("second");
            //bg.velocity.Y = 20;
            bg3.x = 200;
            bg3.y = 100;
            bg3.health = 34;
            add(bg3);

            bgh = new FlxBar(0, 0, FlxBar.FILL_LEFT_TO_RIGHT, 20, 2, bg, "health", 0, bg.health, true);
            add(bgh);
            bg2h = new FlxBar(0, 0, FlxBar.FILL_LEFT_TO_RIGHT, 20, 2, bg2, "health", 0, bg2.health, true);
            add(bg2h);
            bg3h = new FlxBar(0, 0, FlxBar.FILL_LEFT_TO_RIGHT, 20, 2, bg3, "health", 0, bg3.health, true);
            add(bg3h);

        }
        

        override public void update()
        {

            base.update();

            if (FlxG.keys.justPressed(Keys.A))
            {
                bg.hurt(1);
                bg2.hurt(1);
                bg3.hurt(1);
                bg.color = Color.Red;
            }
            if (FlxG.keys.justPressed(Keys.S))
            {
                bg.hurt(11);
                bg2.hurt(11);
                bg3.hurt(11);
            }
            if (FlxG.keys.justPressed(Keys.D))
            {
                bg.velocity.X = 100;
                bg2.velocity.X = 100;
                bg3.velocity.X = 100;
            }
            if (FlxG.keys.justPressed(Keys.F))
            {
                bg.health += 5;
                bg.dead = false;
                bg.exists = true;
            }
            if (FlxG.keys.justPressed(Keys.G))
            {
                bg.colorFlicker(2);
            } 

        }
    }
}

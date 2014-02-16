using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemonade
{
    class Andre : Actor
    {
        public Andre(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("piggyback_run", new int [] {72,73,74,75,76,77} ,12, true);
            addAnimation("piggyback_idle", new int [] {78} , 0 );
            addAnimation("piggyback_jump", new int [] {76,77,76} ,4, true);
            addAnimation("piggyback_dash", new int [] {80} ,0);
        
            addAnimation("run", new int [] {10,11,6,7,8,9} ,16);
            addAnimation("run_push_crate", new int [] {46,47,42,43,44,45} ,16, true);
            addAnimation("dash", new int [] {79} ,0);
            addAnimation("idle", new int [] {51} ,0);
            addAnimation("talk", new int [] {51,48,51,49,51,50} ,12);
            addAnimation("jump", new int [] {46,47,46} ,4 , true);
            addAnimation("death", new int [] {60,60,61,61,62,62,63,63} ,12 , false);

            play("idle");

            runSpeed = 50;

            width = 10;
            height = 41;
            setOffset(20, 39);
            setDrags(1251, 0);

            maxVelocity.X = 530;
            maxVelocity.Y = 2830;
            
            

        }

        override public void update()
        {
            base.update();
        }


        override public void overlapped(FlxObject obj)
        {
            string overlappedWith = obj.GetType().ToString();

            if (overlappedWith == "Lemonade.Army")
            {
                //velocity.Y = -30;
                colorFlicker(2);
            }
            else if (overlappedWith == "Lemonade.Trampoline")
            {
                velocity.Y = -1000;
                trampolineTimer = 0.0f;
            }

        }


    }
}


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
    class Liselot : Actor
    {
        public Liselot(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("piggyback_run", new int [] {72,73,74,75,76,77} ,12, true);
            addAnimation("piggyback_idle", new int [] {78} , 0 );
            addAnimation("piggyback_jump", new int [] {76,77,76} ,4, true);
            addAnimation("piggyback_dash", new int [] {80} ,0);
        
            addAnimation("run", new int [] {12,13,14,15,16,17} ,16);
            addAnimation("run_push_crate", new int [] {69,70,71,81,82,83} ,16, true);
            addAnimation("idle", new int [] {2} ,0);
            addAnimation("talk", new int [] {2,55} ,12);
            addAnimation("jump", new int [] {15,16,17} ,4 , true);
            addAnimation("death", new int [] {64,64,65,65,66,66,67,67} ,12 , false);

            play("idle");

        }

        override public void update()
        {
            base.update();
        }
    }
}

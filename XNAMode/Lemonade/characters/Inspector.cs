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
    class Inspector : Actor
    {
        public Inspector(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 24,25,26,27,28,29 }, 14);
            addAnimation("idle", new int[] { 3 }, 0);
            addAnimation("talk", new int[] { 3,56 }, 8);
            addAnimation("death", new int[] { 96,97,98,99,100,100,100,100 }, 12, false);

            play("idle");


        }

        override public void update()
        {
            base.update();
        }
    }
}

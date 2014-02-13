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
    class Chef : Actor
    {
        public Chef(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 30,31,32,33,34,35 }, 14);
            addAnimation("idle", new int[] { 5 }, 0);
            addAnimation("talk", new int[] { 5, 58 }, 12);
            addAnimation("death", new int[] {101,102,103,104,105,106,107,107 }, 12, false);

        }

        override public void update()
        {
            base.update();
        }
    }
}

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
    class Worker : Actor
    {
        public Worker(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 18,19,20,21,22,23 }, 32);
            addAnimation("idle", new int[] { 0 }, 0);
            addAnimation("talk", new int[] { 0, 53, 52, 54 }, 6);
            addAnimation("death", new int[] { 91, 92, 93, 94, 95, 95, 95, 95 }, 12, false);


        }

        override public void update()
        {
            base.update();
        }
    }
}

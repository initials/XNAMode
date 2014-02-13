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
    class Army : Actor
    {
        public Army(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 36,37,38,39,40,41 }, 16);
            addAnimation("idle", new int[] { 4 }, 0);
            addAnimation("talk", new int[] { 4,57 }, 12);
            addAnimation("death", new int[] { 84,85,86,87,88,89,90,88 }, 12, false);
        
        }

        override public void update()
        {
            base.update();
        }
    }
}

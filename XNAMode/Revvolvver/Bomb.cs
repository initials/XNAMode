using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Revvolvver
{
    class Bomb : FlxSprite
    {

        private float explodeAfter = 3.0f;
        public float explodeTimer = 55.0f;

        public Bomb(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/bomb"), false, false, 120, 120);

            scale = 0;
        }

        override public void update()
        {
            explodeTimer += FlxG.elapsed;

            if (explodeTimer > explodeAfter)
            {
                scale += 0.2f;
            }

            if (scale > 1.5f)
            {
                x = 1000000;
                y = 1000000;
                scale = 0;
            }

            base.update();
        }
    }
}

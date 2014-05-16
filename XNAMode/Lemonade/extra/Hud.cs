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
    class Hud : FlxSprite
    {

        public Hud(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/currentChar"), true, false, 14, 28);

            setScrollFactors(0, 0);

            addAnimation("andre", new int[] { 0 }, 0, true);
            addAnimation("liselot", new int[] { 1 }, 0, true);

            play("andre");
        }

        override public void update()
        {


            base.update();

        }


    }
}

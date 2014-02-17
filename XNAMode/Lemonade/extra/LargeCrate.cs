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
    class LargeCrate : FlxSprite
    {

        public LargeCrate(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/LargeCrate"), true, false, 80, 60);

            
        }

        override public void update()
        {


            base.update();

        }


    }
}

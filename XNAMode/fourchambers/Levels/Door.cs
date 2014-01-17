using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class Door : FlxSprite
    {

        public Door(int xPos, int yPos)
            : base(xPos, yPos)
        {
            width = 16;
            height = 16;

            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/door_24x24");

            loadGraphic(Img, false, false, 24, 24);

        }

        override public void update()
        {


            base.update();

        }


    }
}

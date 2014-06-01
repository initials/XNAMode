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

        public int levelToGoTo;
        static public int debug_GoToLevel = 0;


        public Door(int xPos, int yPos)
            : base(xPos, yPos)
        {
            width = 16;
            height = 16;

            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/door_24x24");

            loadGraphic(Img, false, false, 24, 24);

            levelToGoTo = 1;

        }

        override public void update()
        {


            base.update();

        }


    }
}

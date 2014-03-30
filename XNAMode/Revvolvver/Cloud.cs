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
    class Cloud : FlxSprite
    {

        public Cloud(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/cloud"), false, false, 128 , 71);

            //velocity.X = FlxU.random(50, 150);





        }

        override public void update()
        {

            if (x > FlxG.width) x = -50;
            if (x < -100) x = FlxG.width - 2 ;
            base.update();

        }


    }
}

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
    class FireBall : FlxSprite
    {

        public FireBall(int xPos, int yPos)
            : base(xPos, yPos)
        {

            Texture2D Img = FlxG.Content.Load<Texture2D>("initials/fire");

            loadGraphic(Img, false, false, 24, 24);



        }

        override public void update()
        {



            base.update();

        }


    }
}

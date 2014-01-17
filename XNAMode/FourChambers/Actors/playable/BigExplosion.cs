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
    class BigExplosion : FlxSprite
    {

        public BigExplosion(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/explosion_32x32"), true, false, 32, 32);


            addAnimation("explode", new int[] { 0,1,2,2,3,4,4,5,6,7,8 }, 30, false);
            play("explode");



        }

        override public void update()
        {



            base.update();

        }


    }
}

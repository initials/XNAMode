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
    class PointBurst : FlxSprite
    {
        /// <summary>
        /// 22. 4
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public PointBurst(int xPos, int yPos)
            : base(xPos, yPos)
        {
            width = 16;
            height = 16;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/icons16x16"),true, false,16,16);

            addAnimation("skull", new int[] { 355 }, 0);
            play("skull");


        }

        override public void update()
        {

            if(alpha >=0)
                alpha -= 0.05f;


            base.update();

        }


    }
}

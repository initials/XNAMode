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
    class SmallCrate : FlxSprite
    {

        public SmallCrate(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/smallCrateExplode"), true, false, 32, 32);

            acceleration.Y = Lemonade_Globals.GRAVITY;

            width = 30;
            height = 23;
            setOffset(1,9);
            addAnimation("blink", new int[] {0,1}, 2);
            addAnimation("explode", new int[] {2,3,4,5,6,7}, 12);
            addAnimation("reset", new int[] {8}, 0);

            play("blink");

            setDrags(340, 340);


        }

        override public void update()
        {


            base.update();

        }


    }
}

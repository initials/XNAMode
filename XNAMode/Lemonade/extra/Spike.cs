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
    class Spike : FlxSprite
    {

        public Spike(int xPos, int yPos, int direction)
            : base(xPos, yPos)
        {
            @fixed = true;


            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/spikes_"+Lemonade_Globals.location), true, false, 20, 20);

            addAnimation("up", new int[] { 0 }, 0);
            addAnimation("right", new int[] { 1 }, 0);
            addAnimation("down", new int[] { 2 }, 0);
            addAnimation("left", new int[] { 3 }, 0);

            

            if (direction == 0) { 
                play("up");
                setOffset(0, 10);
                width = 20;
                height = 10;
            }
            if (direction == 1)
            {
                play("right");
                setOffset(0, 0);
                width = 10;
                height = 20;
            }
            if (direction == 2)
            {
                play("down");
                setOffset(0, 0);
                width = 20;
                height = 10;

            }
            if (direction == 3)
            {
                play("left");
                setOffset(10, 0);
                width = 10;
                height = 20;
            }
        }

        override public void update()
        {


            base.update();

        }


    }
}

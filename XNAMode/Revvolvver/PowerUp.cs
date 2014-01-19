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
    class PowerUp : FlxSprite
    {

        //public float timerInvisible = 31.0f;
        //private float timeToStayInvisible = 30.0f;

        public float timerInvisible = 6.0f;
        private float timeToStayInvisible = 5.0f;

        public int powerup = 1;

        public PowerUp(int xPos, int yPos)
            : base(xPos, yPos)
        {

			loadGraphic(FlxG.Content.Load<Texture2D>("Revvolvver/flowersmall"), false,false,16,16);
            addAnimation("machinegun", new int[] { 0 });
            addAnimation("freeze", new int[] { 1 });

            play("freeze");

            angularVelocity = 50;



        }

        override public void update()
        {

            timerInvisible += FlxG.elapsed;

            if (timerInvisible < timeToStayInvisible)
            {
                alpha = 0.01f;
                //exists = false;
                dead = true;
            }
            else
            {
                alpha = 1;
                //exists = true;
                dead = false;
            }

            base.update();

        }


    }
}

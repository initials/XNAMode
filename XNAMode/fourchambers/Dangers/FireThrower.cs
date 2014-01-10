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
    class FireThrower : FlxSprite
    {

        public List<FlxObject> _fires;

        public FireThrower(int xPos, int yPos, List<FlxObject> Fires)
            : base(xPos, yPos)
        {

            Texture2D Img = FlxG.Content.Load<Texture2D>("initials/fire");

            loadGraphic(Img, false, false, 24, 24);

            _fires = Fires;


        }

        override public void update()
        {


            if (FlxU.random() < 0.025f)
            {
                //Console.WriteLine("Shooting");

                shoot();
            }
            base.update();

        }

        public void shoot()
        {
            _fires[(int)(FlxU.random() * 19)].x = x;
            _fires[(int)(FlxU.random() * 19)].y = y;
            _fires[(int)(FlxU.random() * 19)].velocity.X = -150;

        }


    }
}

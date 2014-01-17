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

            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/fire");

            loadGraphic(Img, false, false, 24, 24);

            scale = 0;

            //width = 12;
            //height = 12;
            //offset.X = 6;
            //offset.Y = 6;
            //centerOffsets();


        }

        override public void update()
        {
            scale += 0.0425f;

            if (scale > 1.0f) scale = 1.0f;

            double rot = Math.Atan2((float)velocity.Y, (float)velocity.X);
            double degrees = rot * 180 / Math.PI;

            angle = (float)degrees;


            base.update();

        }

        public override void kill()
        {
            x = 100000;
            y = 100000;
            //base.kill();
        }

        override public void hitSide(FlxObject Contact, float Velocity)
        {
            kill();
        }
        override public void hitBottom(FlxObject Contact, float Velocity)
        {
            kill();
        }
        override public void hitTop(FlxObject Contact, float Velocity)
        {
            kill();
        }


    }
}

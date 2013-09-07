using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class Zinger : FlyingEnemy
    {
        public Zinger(int xPos, int yPos)
            : base(xPos, yPos)
        {

            //actorName = "Zinger";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/zinger_ss_12x14"), true, false, 12, 14);

            addAnimation("fly", new int[] { 0, 1 }, 30);
            play("fly");

            //bounding box tweaks
            width = 10;
            height = 10;
            offset.X = 1;
            offset.Y = 4;

            chanceOfWingFlap += FlxU.random(0.005, 0.009);
            speedOfWingFlapVelocity += FlxU.random(0,3);
            

        }

        override public void update()
        {
            base.update();
        }
    }
}

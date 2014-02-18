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
    class LargeCrate : FlxSprite
    {
        public bool canExplode;

        public LargeCrate(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/LargeCrate"), true, false, 80, 60);

            @fixed = true;
            solid = true;

            canExplode = false;
        }

        override public void update()
        {


            base.update();

            if (dead){
                canExplode = false;
                x = -1000;
            }

        }

        //public override void kill()
        //{
        //    Console.WriteLine("Large Crate Kill() ");

        //    if (canExplode)
        //        base.kill();

        //    canExplode = true;
        //}
    }
}

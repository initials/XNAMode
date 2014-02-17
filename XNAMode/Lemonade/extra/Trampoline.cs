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
    class Trampoline : FlxSprite
    {

        public Trampoline(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/trampoline"), true, false, 24, 24);

            addAnimation("stuck", new int[] { 0 }, 0, true);
            addAnimation("boing", new int[] {1,2,0,0}, 12, false);

            play("stuck");



        }

        override public void update()
        {


            base.update();

        }

        public override void overlapped(FlxObject obj)
        {
            string overlappedWith = obj.GetType().ToString();

            if (overlappedWith == "Lemonade.Andre")
            {
                play("boing", true);
            }

        }

    }
}

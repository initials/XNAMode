using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace XNAMode
{
    class Vampire : Actor
    {
        private Texture2D ImgVampire;

        public Vampire(int xPos, int yPos)
            : base(xPos, yPos)
        {
            ImgVampire = FlxG.Content.Load<Texture2D>("initials/vampire_ss_14x19");

            loadGraphic(ImgVampire, true, false, 14, 19);

            //bounding box tweaks
            width = 12;
            height = 14;
            offset.X = 1;
            offset.Y = 5;

            //animations
            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

        }

        override public void update()
        {



            base.update();

        }


    }
}

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
    class Gloom : Actor
    {

        public Gloom(int xPos, int yPos)
            : base(xPos, yPos)
        {

            
            loadGraphic(FlxG.Content.Load<Texture2D>("initials/gloom_spritesheet"), true, false, 13, 26);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5,6,7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);


        }

        override public void update()
        {



            base.update();

        }


    }
}

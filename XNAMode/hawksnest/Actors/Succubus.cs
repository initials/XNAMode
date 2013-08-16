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
    class Succubus : Actor
    {

        public Succubus(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/succubus_ss_35x30"), true, false, 35, 22);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);


        }

        override public void update()
        {



            base.update();

        }


    }
}

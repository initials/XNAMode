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
    class Harvester : Actor
    {

        public Harvester(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/harvester_ss_14x27"), true, false, 14, 27);

            addAnimation("run", new int[] { 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0,1,2 }, 18);



        }

        override public void update()
        {



            base.update();

        }


    }
}

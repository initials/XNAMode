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
    class Nightmare : EnemyActor
    {
        public Nightmare(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Nightmare";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/allActors"), true, false, 26, 26);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { FR_nightmare }, 0);
            //addAnimation("attack", new int[] { 2, 4 }, 18);

        }

        override public void update()
        {
            base.update();
        }
    }
}

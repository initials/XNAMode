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
    class Snake : EnemyActor
    {
        public Snake(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Snake";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/Snake_20x20"), true, false, 20, 20);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0 }, 0);
            addAnimation("death", new int[] { 1 }, 4, false);




        }

        override public void update()
        {
            base.update();
        }
    }
}

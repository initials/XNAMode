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
    class Chicken : EnemyActor
    {

        public Chicken(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Chicken";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/chicken_20x20"), true, false, 20, 20);

            addAnimation("death", new int[] { 1 }, 0, false);
            addAnimation("idle", new int[] { 0 }, 12);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);

            //addAnimation("idleMelee", new int[] { 28 }, 12);
            //addAnimation("attack", new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 60, true);
            //addAnimation("attackMelee", new int[] { 0, 24, 24, 25, 26, 27, 27, 26, 26, 26, 26, 26, 26 }, 60, true);

            //addAnimation("jump", new int[] { 3, 4, 5, 6, 7, 8, 9 }, 3, true);
            //addAnimation("climb", new int[] { 20, 21 }, 6, true);
            //addAnimation("climbidle", new int[] { 20 }, 0, true);


        }

        override public void update()
        {
            base.update();
        }
    }
}

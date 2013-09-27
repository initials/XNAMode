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
    class Automaton : EnemyActor
    {
        public Automaton(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "The Automaton";

            loadGraphic(FlxG.Content.Load<Texture2D>("initials/automaton_ss_11x24"), true, false, 11, 24);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6,7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 2,4 }, 18);
            addAnimation("death", new int[] { 8,9 }, 12, false);


            //bounding box tweaks
            width = 7;
            height = 20;
            offset.X = 2;
            offset.Y = 4;

            //basic player physics
            int runSpeed = 30;
            //drag.X = runSpeed * 4;
            drag.X = 0;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;


            health = 1;

            velocity.X = FlxU.random(30, 50);

            score = 50;
        }

        override public void update()
        {



            base.update();
        }

    }
}

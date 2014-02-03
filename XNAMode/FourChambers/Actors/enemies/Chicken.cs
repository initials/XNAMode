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

            actorName = "Goldie";
            score = 25;
            health = 1;
            runSpeed = 30;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/chicken_20x20"), true, false, 20, 20);

            addAnimation("death", new int[] { 1 }, 0, false);
            addAnimation("idle", new int[] { 0 }, 12);


        }

        override public void update()
        {
            base.update();
        }
    }
}

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
    class Wolf : EnemyActor
    {
        public Wolf(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Husky Lifesavier";
            score = 250;
            health = 5;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/wolf.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "wolf";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/allActors"), true, false, 26, 26);

            addAnimation("idle", new int[] { FR_wolf }, 0);

        }

        override public void update()
        {
            base.update();
        }
    }
}

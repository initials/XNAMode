﻿using System;
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

            // Set up the stats for this actor.
            actorName = "Cowan";
            score = 150;
            health = 2;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/snake.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "snake";


            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

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

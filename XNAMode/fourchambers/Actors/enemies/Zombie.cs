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
    class Zombie : EnemyActor
    {

        public Zombie(int xPos, int yPos)
            : base(xPos, yPos)
        {
            // Set up the stats for this actor.
            actorName = "Fred";
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
            playbackFile = "FourChambers/ActorRecording/zombie.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "zombie";

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/zombie_ss_13x19"), true, false, 13, 19);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 18);
            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);
            addAnimation("death", new int[] { 10, 11, 12, 13, 14, 15, 15, 15, 14, 13, 13, 14, 13, 14, 14, 13, 12, 13, 14, 15 }, 6, false);

            //bounding box tweaks
            width = 9;
            height = 15;
            offset.X = 2;
            offset.Y = 4;
        }

        override public void update()
        {

            base.update();

        }


    }
}

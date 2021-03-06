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
    class Medusa : EnemyActor
    {

        public Medusa(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Jenny";
            score = 250;
            health = 15;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/medusa.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "medusa";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Medusa_ss_35x30"), true, false, 35, 30);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 12);

            //bounding box tweaks
            width = 7;
            height = 20;
            offset.X = 9;
            offset.Y = 10;



        }

        override public void update()
        {



            base.update();

        }


    }
}

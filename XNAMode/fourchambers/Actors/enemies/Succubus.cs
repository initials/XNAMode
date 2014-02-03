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
    class Succubus : EnemyActor
    {

        public Succubus(int xPos, int yPos)
            : base(xPos, yPos)
        {
            // Set up the stats for this actor.
            actorName = "Josie";
            score = 250;
            health = 5;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/succubus_ss_35x30"), true, false, 35, 20);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 18);
            addAnimation("walk", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            //bounding box tweaks
            width = 9;
            height = 20;
            offset.X = 8;
            offset.Y = 0;

        }

        override public void update()
        {



            base.update();

        }


    }
}

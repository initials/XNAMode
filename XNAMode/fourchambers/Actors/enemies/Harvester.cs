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
    class Harvester : EnemyActor
    {

        public Harvester(int xPos, int yPos)
            : base(xPos, yPos)
        {
            // Set up the stats for this actor.
            actorName = "Creeping Death";
            score = 50000;
            health = 100;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/harvester.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "harvester";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Harvester_ss_14x27"), true, false, 14, 27);

            addAnimation("run", new int[] { 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("walk", new int[] { 2, 3, 4, 5, 6, 7 }, 8);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0,1,2 }, 18);

            //bounding box tweaks
            width = 8;
            height = 20;
            offset.X = 3;
            offset.Y = 7;



        }

        override public void update()
        {



            base.update();

        }


    }
}



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
    class Corsair : EnemyActor
    {

        public Corsair(int xPos, int yPos)
            : base(xPos, yPos)
        {
            
            // Set up the stats for this actor.
            actorName = "Sierra";
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
            playbackFile = "FourChambers/ActorRecording/corsair.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "corsair";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Corsair_18x21"), true, false, 18, 21);

            addAnimation("run", new int[] { 1, 2, 3, 4, 5}, 12);
            addAnimation("walk", new int[] { 1, 2, 3, 4, 5 }, 8);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("jump", new int[] { 1, 1, 1, 2, 3 }, 6, false);
            addAnimation("attack", new int[] { 0,1,2}, 12);
            addAnimation("death", new int[] { 6 }, 12, false);


            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 4;
            offset.Y = 1;

        }

        override public void update()
        {



            base.update();

        }


    }
}

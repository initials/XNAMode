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
    class Automaton : EnemyActor
    {
        public Automaton(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Automatic Gerry";
            actorType = "automaton";
            score = 250;
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
            playbackFile = "FourChambers/ActorRecording/automaton.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "automaton";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Automaton_ss_11x24"), true, false, 11, 24);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6,7 }, 18);
            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 2,4 }, 18);
            addAnimation("hurt", new int[] { 8,9 }, 12, false);
            addAnimation("death", new int[] { 8, 9 }, 12, false);

            //bounding box tweaks
            width = 7;
            height = 20;
            offset.X = 2;
            offset.Y = 4;

        }

        override public void update()
        {
            base.update();
        }

    }
}

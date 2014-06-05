using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace FourChambers
{
    public class Vampire : EnemyActor
    {
        private Texture2D ImgVampire;

        public Vampire(int xPos, int yPos)
            : base(xPos, yPos)
        {
            
            // Set up the stats for this actor.
            actorName = "Count Esperanza";
            score = 250;
            health = 5;
            runSpeed = 40;
            _jumpPower = -210.0f;
            _jumpInitialPower = -310.0f;
            _jumpMaxTime = 0.25f;
            _jumpInitialTime = 0.095f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/vampire.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "vampire";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            ImgVampire = FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Vampire_ss_14x19");

            loadGraphic(ImgVampire, true, false, 14, 19);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 16);
            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 8);
            addAnimation("idle", new int[] { 0 }, 16);
            addAnimation("attack", new int[] { 0, 1, 2 }, 16);
            addAnimation("hurt", new int[] { 7,0,1,2 }, 8, false);
            addAnimation("death", new int[] { 7 }, 8, false);

            //bounding box tweaks
            width = 6;
            height = 18;
            offset.X = 4;
            offset.Y = 1;
        }

        override public void update()
        {
            base.update();
        }


    }
}

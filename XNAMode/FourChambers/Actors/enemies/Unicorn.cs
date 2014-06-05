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
    public class Unicorn : EnemyActor
    {

        public Unicorn(int xPos, int yPos)
            : base(xPos, yPos)
        {
            //FlxG.write("3 New Unicorn");

            // Set up the stats for this actor.
            actorName = "Fabulous Diamond Joe";
            score = 250;
            health = 25; //25
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/unicorn.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "unicorn";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump
            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Unicorn_ss_20x40"), true, false, 20, 40);

            addAnimation("walk", new int[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 10);
            addAnimation("run", new int[] {2, 3, 4, 5, 6, 7,8,9 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0 }, 12);
            addAnimation("death", new int[] { 10, 11, 12, 13, 14, 14, 14, 14, 14, 14, 13, 14, 11, 12, 13, 14, 14, 14, 14, 14, 14, 13, 14, 15, 15, 14, 14, 15, 15, 14, 14, 15, 15, 14, 14, 15, 14, 15, 14, 15, 14, 15, 14, 15, 16 }, 12, false);
            addAnimation("hurt", new int[] { 17, 1, 10, 11, 12, 13, 14, 13, 14, 13, 14, 13, 14, 13,14,15,15,15 }, 8, false);
            addAnimation("jump", new int[] { 17, 2,3,4 }, 8,false);
            
            addAnimationCallback(finishedHurt);

            //bounding box tweaks
            width = 10;
            height = 20;
            offset.X = 5;
            offset.Y = 20;

            
        }

        public void finishedHurt(string Name, uint Frame, int FrameIndex) 
        {
            //Console.WriteLine("Callback {0} {1} {2}", Name, Frame, FrameIndex);

            //if (Name == "hurt" && Frame == 0)
            //{
            //    velocity.X = 0;
            //}
            if (Name == "hurt" && Frame == _curAnim.frames.Length - 1)
            {
                //velocity.X = 32;
                //hurtTimer += 55.0f;
                //Console.WriteLine("Callback {0} {1} {2}", Name, Frame, FrameIndex);
                //color = Color.White;

                if (dead)
                {
                    velocity.X = 0;
                }
                else if (facing == Flx2DFacing.Right)
                    velocity.X = 3300;
                else if (facing == Flx2DFacing.Left)
                    velocity.X = -3300;
                else velocity.X = -3300;

            }
        }

        override public void update()
        {
            base.update();
        }

        public override void hurt(float Damage)
        {
            play("hurt");

            hurtTimer = 0;

            velocity.X = 0;
            //color = Color.PaleVioletRed;

            FlxG.play("fourchambers/sfx/horseHurt", 1.0f, false);

            base.hurt(Damage);
        }
    }
}

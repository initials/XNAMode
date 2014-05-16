using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemonade
{
    class Andre : Actor
    {

        public Follower f;

        public Andre(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("piggyback_run", new int [] {72,73,74,75,76,77} ,12, true);
            addAnimation("piggyback_idle", new int [] {78} , 0 );
            addAnimation("piggyback_jump", new int [] {76,77,76} ,4, true);
            addAnimation("piggyback_dash", new int [] {80} ,0);
        
            addAnimation("run", new int [] {10,11,6,7,8,9} ,16);
            addAnimation("run_push_crate", new int [] {46,47,42,43,44,45} ,16, true);
            addAnimation("dash", new int [] {79} ,0);
            addAnimation("idle", new int [] {51} ,0);
            addAnimation("talk", new int [] {51,48,51,49,51,50} ,12);
            addAnimation("jump", new int [] {46,47,46,46} ,4 , true);
            addAnimation("death", new int [] {60,60,61,61,62,62,63,63} ,12 , false);

            play("idle");

            addAnimationCallback(resetAfterDeath);


            width = 10;
            height = 41;
            setOffset(20, 39);
            setDrags(1251, 0);

            maxVelocity.X = 530;
            maxVelocity.Y = 2830;

			#if __ANDROID__
			maxVelocity.X/=1.75f;
			maxVelocity.Y/=1.75f;
			#endif

            runSpeed = 50;
            setJumpValues(-310.0f, -410.0f, 0.35f, 0.075f);
            numberOfJumps = 1;

            _runningMax = maxVelocity.X;
            

        }

        public void resetAfterDeath(string Name, uint Frame, int FrameIndex)
        {
            //Console.WriteLine("Name {0} Frame {1}",Name, Frame);
            
            if (Name == "death" && Frame >= _curAnim.frames.Length - 1)
            {
                reset(originalPosition.X, originalPosition.Y);
                dead = false;
                control = Controls.player;
                play("idle");

            }
        }

        override public void update()
        {
            //Console.WriteLine(velocity.Y);

            if (control == Controls.none) color = new Color(0.321f, 0.321f, 0.321f);
            else color = Color.White;

            if (FlxG.keys.justPressed(Keys.B) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            {
                piggyBacking = false;
                
            }


            base.update();
        }

        

        override public void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            string overlappedWith = obj.GetType().ToString();

            if ((overlappedWith == "Lemonade.Army" || 
                overlappedWith == "Lemonade.Inspector" ||
                overlappedWith == "Lemonade.Chef" ||
                overlappedWith == "Lemonade.Worker" ) && !flickering() )
            {
                if (obj.dead == false && control == Controls.player)
                {
                    if (dead == false) FlxG.play("Lemonade/sfx/deathSFX", 0.8f, false);
                    flicker(2);
                    kill();
                }

            }
            else if (overlappedWith == "Lemonade.Liselot")
            {
                if (piggyBacking == false && dead == false && flickering() == false)
                {
                    FlxG.play("Lemonade/sfx/SndOnShoulders", 0.8f, false);

                    //FlxG.follow(this, 11.0f);
                    f.currentFollow = 1;

                    Console.WriteLine("Piggybacking is GO!");
                    control = Controls.player;
                    piggyBacking = true;
                    ((Liselot)(obj)).piggyBacking = true;
                    ((Liselot)(obj)).parent = this;
                    ((Liselot)(obj)).control = Controls.none;
                }
            }
            else if (overlappedWith == "Lemonade.FilingCabinet")
            {
                originalPosition.X = obj.x+30;
                originalPosition.Y = obj.y;

            }
            else if (overlappedWith == "Lemonade.LargeCrate")
            {
                //Console.WriteLine("crate overlapp");

                ((LargeCrate)(obj)).canExplode = true;

                //if (dashTimer > dashMaxLimit)
                //{
                //    obj.kill();
                //    obj.x = -1000;
                //    obj.y = -1000;
                //}
            }
            //else if (overlappedWith == "Lemonade.Spike")
            //{
            //    //Console.WriteLine("Spike overlapp");
            //    hurt(1);
            //}
        }
        public override void kill()
        {
            control = Controls.none;
            dead = true;

            piggyBacking = false;
            liselot.piggyBacking = false;
            liselot.parent = null;

            //base.kill();
        }

    }
}

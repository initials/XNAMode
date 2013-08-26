using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class Actor : FlxSprite
    {
        // A bunch of helpers for the allActors sprite sheet.

        // Row:  0
        //public const int FR_artist = 0;
        //public const int FR_assassin = 1;
        public const int FR_executor = 2;
        //public const int FR_bat = 3;
        //public const int FR_blight = 4;
        //public const int FR_bloatedzombie = 5;
        public const int FR_assassin = 6;
        public const int FR_corsair = 7; 
        // Row:  1
        //public const int FR_centaur = 8;
        //public const int FR_chicken = 9;
        //public const int FR_chimaera = 10;
        public const int FR_druid = 11; 
        //public const int FR_cow = 12;
        //public const int FR_cyclops = 13;
        public const int FR_artist = 14; 
        //public const int FR_deer = 15;
        // Row:  2
        public const int FR_chicken = 16;
        //public const int FR_djinn = 17;
        //public const int FR_drone = 18;
        public const int FR_feline = 19;
        //public const int FR_dwarf = 20;
        public const int FR_cow = 21;
        //public const int FR_executor = 22;
        public const int FR_dear = 23;
        // Row:  3
        //public const int FR_floatingeye = 24;
        public const int FR_bat = 25;
        //public const int FR_gelatine = 26;
        //public const int FR_gloom = 27;
        //public const int FR_glutton = 28;
        //public const int FR_goblin = 29;
        //public const int FR_golem = 30;
        //public const int FR_gorgon = 31;
        // Row:  4
        //public const int FR_gourmet = 32;
        //public const int FR_cyclops = 33;
        public const int FR_dwarf = 34;
        //public const int FR_harvester = 35;
        //public const int FR_horse = 36;
        //public const int FR_ifrit = 37;
        //public const int FR_imp = 38;
        public const int FR_centaur = 39; 
        // Row:  5
        //public const int FR_lich = 40;
        //public const int FR_lion = 41;
        //public const int FR_fungant = 42;
        //public const int FR_mechanic = 43;
        //public const int FR_mephisto = 44;
        public const int FR_deathclaw = 45; 
        public const int FR_bloatedzombie = 46; 
        //public const int FR_mimick = 47;
        // Row:  6
        public const int FR_bogbeast = 48; 
        //public const int FR_blight = 49;
        //public const int FR_nightmare = 50;
        //public const int FR_nymph = 51;
        //public const int FR_ogre = 52;
        public const int FR_gloom = 53; 
        //public const int FR_phantom = 54;
        //public const int FR_priest = 55;
        // Row:  7
        //public const int FR_prism = 56;
        //public const int FR_rat = 57;
        //public const int FR_savage = 58;
        //public const int FR_devil = 59;
        //public const int FR_sheep = 60;
        //public const int FR_skeleton = 61;
        //public const int FR_snake = 62;
        //public const int FR_soldier = 63;
        // Row:  8
        //public const int FR_sphinx = 64;
        //public const int FR_spider = 65;
        //public const int FR_succubus = 66;
        public const int FR_embersteed = 67; 
        //public const int FR_toad = 68;
        //public const int FR_bombling = 69;
        public const int FR_drone = 70; 
        //public const int FR_troll = 71;
        // Row:  9
        public const int FR_automaton = 72; 
        //public const int FR_vampire = 73;
        public const int FR_gelantine = 74; 
        public const int FR_floatingeye = 75; 
        //public const int FR_wizard = 76;
        public const int FR_djinn = 77;
        //public const int FR_zinger = 78;
        public const int FR_chimera = 79;


        public bool canClimbLadder = false;

        /// <summary>
        /// The score to recieve when killing this actor
        /// </summary>
        public int score = 50;

        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;

        /// <summary>
        /// How high the character will jump.
        /// </summary>
        public float jumpPower;

        /// <summary>
        /// How many frames have passed since the character left the ground.
        /// </summary>
        public float framesSinceLeftGround;
        public const float DEADZONE = 0.5f;
        
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        public int runSpeed = 120;


        public List<FlxObject> _bullets;
        public int _curBullet;

        public bool attacking = false;

        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        public Actor(int xPos, int yPos)
            : base(xPos,yPos)
		{

            isPlayerControlled = false;


            //basic player physics
            
            drag.X = runSpeed * 4;
            acceleration.Y = 820;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            jumpPower = -305;

        }

        public void stopAttacking(string Name, uint Frame, int FrameIndex)
        {
            //attacking = false;
        }   

        override public void update()
        {
            if (dead) timeDead += FlxG.elapsed;
            else timeDead = 0;


            PlayerIndex pi;

            // Calculate how many frames since the player left the ground

            if (velocity.Y == 0) framesSinceLeftGround = 0;

            else
            {
                framesSinceLeftGround++;


            }

            //MOVEMENT

            if (isPlayerControlled)
            {

                if (FlxG.gamepads.isButtonDown(Buttons.RightTrigger, FlxG.controllingPlayer, out pi))
                {
                    maxVelocity.X = runSpeed * 2;
                }
                else
                {
                    maxVelocity.X = runSpeed;
                }


                acceleration.X = 0;
                if (FlxG.keys.LEFT || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, FlxG.controllingPlayer, out pi))
                {
                    attacking = false;
                    facing = Flx2DFacing.Left;
                    acceleration.X -= drag.X;


                }
                else if (FlxG.keys.RIGHT || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    attacking = false;
                    facing = Flx2DFacing.Right;
                    acceleration.X += drag.X;


                }
                if ((FlxG.keys.UP || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp, FlxG.controllingPlayer, out pi)) && canClimbLadder)
                {
                    velocity.Y = -100;
                }
                else
                {
                    canClimbLadder = false;
                }

                // && velocity.Y == 0
                if ((FlxG.keys.justPressed(Keys.X) || FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi)) && framesSinceLeftGround < 10)
                {
                    attacking = false;
                    velocity.Y = jumpPower;

                }
                if ((FlxG.keys.justPressed(Keys.C) || FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, FlxG.controllingPlayer, out pi)))
                {
                    attacking = true;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < DEADZONE * -1.0f ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < DEADZONE * -1.0f ||
                    FlxG.gamepads.isButtonDown(Buttons.X, PlayerIndex.One, out pi) ||
                    FlxG.keys.C)

                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }

                if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Left;
                }
                if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    facing = Flx2DFacing.Right;
                }

    //            if (!flickering() && 
    //    (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickDown, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi) ||
    //FlxG.gamepads.isButtonDown(Buttons.RightThumbstickUp, FlxG.controllingPlayer, out pi)))
    //            {

    //                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

    //                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

    //                if (rightY < -0.75)
    //                {
    //                    velocity.Y -= 36;
    //                }

    //                float rotation = (float)Math.Atan2(rightX, rightY);
    //                rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);



    //            }
            }


            //ANIMATION
            if (dead)
            {
                play("death");
            }
            else if (attacking)
            {
                play("attack");
            }
            else if (velocity.Y != 0)
            {
                play("jump");
            }
            else if (velocity.X == 0)
            {
                play("idle");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                play("run");
            }
            else
            {
                play("idle");
            }

            base.update();



            //canClimbLadder = false;
        }

        public override void kill()
        {
            //base.kill();

            play("death");


            dead = true;


            FlxG.score += score;
            
            
            //FlxG.bloom.Visible = true;

        }


    }

}

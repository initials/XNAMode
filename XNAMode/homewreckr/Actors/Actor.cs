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

        #region Constants for frame numbers
        // Row:  0
        public const int FR_soldier = 0;
        public const int FR_paladin = 1;
        public const int FR_executor = 2;
        public const int FR_savage = 3;
        public const int FR_marksman = 4;
        public const int FR_monk = 5;
        public const int FR_assassin = 6;
        public const int FR_corsair = 7; 
        // Row:  1
        public const int FR_wizard = 8;
        public const int FR_priest = 9;
        public const int FR_warlock = 10;
        public const int FR_druid = 11; 
        public const int FR_merchant = 12;
        public const int FR_mechanic = 13;
        public const int FR_artist = 14; 
        public const int FR_gormet = 15;
        // Row:  2
        public const int FR_chicken = 16;
        public const int FR_rat = 17;
        public const int FR_toad = 18;
        public const int FR_feline = 19;
        public const int FR_sheep = 20;
        public const int FR_cow = 21;
        public const int FR_horse = 22;
        public const int FR_deer = 23;
        // Row:  3
        public const int FR_snake = 24;
        public const int FR_bat = 25;
        public const int FR_zinger = 26;
        public const int FR_spider = 27;
        public const int FR_wolf = 28;
        public const int FR_grizzly = 29;
        public const int FR_lion = 30;
        public const int FR_unicorn = 31;
        // Row:  4
        public const int FR_troll = 32;
        public const int FR_cyclops = 33;
        public const int FR_dwarf = 34;
        public const int FR_goblin = 35;
        public const int FR_ogre = 36;
        public const int FR_nymph = 37;
        public const int FR_mermaid = 38;
        public const int FR_centaur = 39; 
        // Row:  5
        public const int FR_tauro = 40;
        public const int FR_treant = 41;
        public const int FR_fungant = 42;
        public const int FR_zombie = 43;
        public const int FR_mummy = 44;
        public const int FR_deathclaw = 45; 
        public const int FR_bloatedzombie = 46; 
        public const int FR_vampire = 47;
        // Row:  6
        public const int FR_bogbeast = 48; 
        public const int FR_blight = 49;
        public const int FR_willowisp = 50;
        public const int FR_phantom = 51;
        public const int FR_gloom = 52;
        public const int FR_nightmare = 53; 
        public const int FR_skeleton = 54;
        public const int FR_grimwarrior = 55;
        // Row:  7
        public const int FR_harvester = 56;
        public const int FR_lich = 57;
        public const int FR_imp = 58;
        public const int FR_devil = 59;
        public const int FR_mephisto = 60;
        public const int FR_kerberos = 61;
        public const int FR_glutton = 62;
        public const int FR_tormentor = 63;
        // Row:  8
        public const int FR_succubus = 64;
        public const int FR_gorgon = 65;
        public const int FR_ifrit = 66;
        public const int FR_embersteed = 67; 
        public const int FR_seraphine = 68;
        public const int FR_bombling = 69;
        public const int FR_drone = 70; 
        public const int FR_mimick = 71;
        // Row:  9
        public const int FR_automaton = 72; 
        public const int FR_golem = 73;
        public const int FR_gelantine = 74; 
        public const int FR_floatingeye = 75; 
        public const int FR_prism = 76;
        public const int FR_djinn = 77;
        public const int FR_sphinx = 78;
        public const int FR_chimera = 79;
        #endregion


        public bool canClimbLadder = false;
        private bool isClimbingLadder = false;

        /// <summary>
        /// The score to recieve when killing this actor
        /// </summary>
        public int score = 50;

        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;



        /// <summary>
        /// How many frames have passed since the character left the ground.
        /// </summary>
        public float framesSinceLeftGround;

        public const float GRAVITY = 820.0f;
        public const float DEADZONE = 0.5f;
        
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        public int runSpeed = 120;


        public List<FlxObject> _bullets;
        public int _curBullet;

        
        
        /// <summary>
        /// Tells the Actor whether to play the attack animation
        /// </summary>
        public bool attackingMouse = false;

        /// <summary>
        /// Tells the Actor whether to play the attack animation
        /// </summary>
        public bool attackingJoystick = false;


        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        /// <summary>
        /// holds the amount of time character has been jumping.
        /// </summary>
        private float _jump = 0.0f;
        /// <summary>
        /// How high the character will jump.
        /// </summary>
        private float _jumpPower = -180.0f;

        private float _jumpInitialPower = -140.0f;

        private float _jumpMaxTime = 0.25f;

        private float _jumpInitialTime = 0.065f;


        public Actor(int xPos, int yPos)
            : base(xPos,yPos)
		{
            //basic player physics
            
            drag.X = runSpeed * 4;
            acceleration.Y = GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            isPlayerControlled = false;

        }

        public void stopAttacking(string Name, uint Frame, int FrameIndex)
        {
            //attacking = false;
        }   

        override public void update()
        {
            if (dead) timeDead += FlxG.elapsed;
            else timeDead = 0;

            //canClimbLadder = false;

            PlayerIndex pi;

            // Calculate how many frames since the player left the ground

            if (onFloor)
            {
                framesSinceLeftGround = 0;
            }
            else
            {
                framesSinceLeftGround++;
            }

            //MOVEMENT

            if (isPlayerControlled)
            {
                acceleration.Y = GRAVITY;

                if (FlxG.gamepads.isButtonDown(Buttons.RightTrigger, FlxG.controllingPlayer, out pi))
                {
                    maxVelocity.X = runSpeed * 2;
                }
                else
                {
                    maxVelocity.X = runSpeed;
                }


                acceleration.X = 0;

                if (FlxG.keys.A || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, FlxG.controllingPlayer, out pi))
                {
                    attackingJoystick = false;
                    attackingMouse = false;
                    facing = Flx2DFacing.Left;
                    acceleration.X -= drag.X;


                }
                else if (FlxG.keys.D  || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, FlxG.controllingPlayer, out pi))
                {
                    attackingJoystick = false;
                    attackingMouse = false;
                    facing = Flx2DFacing.Right;
                    acceleration.X += drag.X;


                }

                // ladders
                if ((FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp, FlxG.controllingPlayer, out pi)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi))
                {
                    velocity.Y = -100;
                    //int newX = (int)(x / 16 ) * 16;
                    //x = newX;
                    isClimbingLadder = true;
                }
                if ((FlxG.keys.S || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown, FlxG.controllingPlayer, out pi)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi))
                {
                    velocity.Y = 100;
                    //int newX = (int)(x / 16) * 16;
                    //x = newX;
                    isClimbingLadder = true;
                }


                // Jumping.
                
                if ((_jump >= 0 || framesSinceLeftGround < 10 || canClimbLadder) && (FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi))) 
                {
                    if (framesSinceLeftGround < 10)
                    {
                        _jump = 0.0f;
                        framesSinceLeftGround = 10000;
                    }
                    if (canClimbLadder)
                    {
                        _jump = 0.0f;
                    }

                    attackingJoystick = false;
                    attackingMouse = false;

                    _jump += FlxG.elapsed;
                    if (_jump > _jumpMaxTime) _jump = -1; 
                }
                else
                {
                    _jump = -1;
                }
                if (_jump > 0)
                {
                    if (_jump < _jumpInitialTime)
                        velocity.Y = _jumpInitialPower; 
                    else
                        velocity.Y = _jumpPower ; 
                }


                if (FlxG.keys.justPressed(Keys.C))
                {
                    attackingMouse = true;
                }
                if (FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, FlxG.controllingPlayer, out pi))
                {
                    attackingJoystick = true;
                }




                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > DEADZONE ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < DEADZONE * -1.0f ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < DEADZONE * -1.0f ||
                    FlxG.gamepads.isButtonDown(Buttons.X, PlayerIndex.One, out pi)
                    )
                {
                    attackingJoystick = true;
                }
                else
                {
                    attackingJoystick = false;
                }


                if (FlxG.mouse.pressedLeftButton())
                {
                    attackingMouse = true;
                }
                else
                {
                    attackingMouse = false;
                }


                if (FlxG.keys.C) {
                    attackingMouse = true;
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
            else // is not player controlled.
            {
                acceleration.Y = 0;

            }


            //ANIMATION
            if (dead)
            {
                play("death");
            }
            else if (isClimbingLadder)
            {
                play("climb");
            }
            else if (attackingMouse || attackingJoystick)
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


            canClimbLadder = false;
            isClimbingLadder = false;

        }

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            _jump = 0.0f;

            base.hitBottom(Contact, Velocity);
        }

        public override void kill()
        {
            //base.kill();

            play("death");


            dead = true;


            FlxG.score += score;

            Console.WriteLine(actorName + " is dead");

            base.kill();


            // -
            //flicker(10);

            //angle = 90;
            
            
            //FlxG.bloom.Visible = true;

        }


    }

}

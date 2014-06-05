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
    public class EnemyActor : BaseActor
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
        
        public bool readyForHarvester = false;
        public const float DEADZONE = 0.5f;
        
        public bool attackingMouse = false;
        public bool attackingJoystick = false;
        public bool attackingMelee = false;
        //public float timeDead;
        private float _jump = 0.0f;

        public float _jumpPower = -180.0f;
        public float _jumpInitialPower = -140.0f;
        public float _jumpMaxTime = 0.25f;
        public float _jumpInitialTime = 0.065f;

        public float framesSinceLeftGround;
        
        public string lastAttack = "range";

        /// <summary>
        /// Holds the recording of the actions
        /// </summary>
        private List<bool[]> _history = new List<bool[]>();

        /// <summary>
        /// Keeps track of where the actor is in the playback.
        /// </summary>
        public int frameCount;

        /// <summary>
        /// Keep track of the state.
        /// </summary>
        private Recording _rec = Recording.None;

        public FlxBar healthBar;

        public enum Recording
        {
            None = 0,
            Recording = 1,
            Playback = 2,
            Reverse = 3,
            RecordingController = 4
        }

        public EnemyActor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            //FlxG.write("2 New enemy ACTOR");

            acceleration.Y = FourChambers_Globals.GRAVITY;
            frameCount = 0;

            healthBar = new FlxBar(0, 0, FlxBar.FILL_LEFT_TO_RIGHT, 10, 2, this, "health", 0, health, true);
            
        }


        public override void render(SpriteBatch spriteBatch)
        {
            
            base.render(spriteBatch);
            //healthBar.outline.render(spriteBatch);
            healthBar.emptyBar.render(spriteBatch);
            healthBar.filledBar.render(spriteBatch);
            

        }

        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {

            if (hurtTimer >= timeDownAfterHurt && !dead )
            {
                //if (actorType=="unicorn") FlxG.write("Setting colorFlickerValues");

                setColorFlickerValues(1, 0.175f, 0.175f, 1, 0.2f, 0.2f);

            }

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else if (velocity.X < 0)
            {
                facing = Flx2DFacing.Left;
            }

            if (hurtTimer < timeDownAfterHurt)
            {
                play("hurt");
            }
            else if (dead)
            {
                play("death");
            }
            else if (attackingMouse || attackingJoystick || attackingMelee)
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
            else if (FlxU.abs(velocity.X) > 70)
            {
                play("run");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                play("walk");
            }
            else
            {
                play("idle");
            }
            //if (isPlayerControlled)
            //{
               
            //    updateInputs();
            //}
            
            updateInputs();

            //if (actorType=="unicorn") Console.WriteLine("hurtTimer : " + hurtTimer + " " + _colorFlickerTimer);

            if (hurtTimer >= timeDownAfterHurt && !dead) updateRecording();
            else
            {
                velocity.X = 0;
                //velocity.Y = 0;
                acceleration.X = 0;
            }
            base.update();

            healthBar.update();
        }

        public override void hurt(float Damage)
        {
            if (!colorFlickering())
            {
                setColorFlickerValues(0.98f, 0.98f, 0.98f, 0.99f, 0.99f, 0.99f);

                // Flicker for the time down and more after it, for an escape.
                colorFlicker(timeDownAfterHurt*2);

                hurtTimer = 0;
                base.hurt(Damage);
            }
        }

        public override void kill()
        {
            color = Color.White;

            FlxG.score += score * FourChambers_Globals.arrowCombo;

            play("death");
            velocity.X = 0;
            //velocity.Y = 0;
            dead = true;

            //base.kill();

            FlxG.write("Enemy "  + actorName + " is dead");

            //flicker(0.25f);

            acceleration.X = 0;
        }

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            _jump = 0.0f;
            isClimbingLadder = false;
            base.hitBottom(Contact, Velocity);
        }

        public void updateRecording()
        {
            
            if (_rec == Recording.Playback && !dead)
            {
                frameCount++;
                if (frameCount > _history.Count - 1)
                {
                    _rec = Recording.Reverse;
                    frameCount--;
                }
            }
            else if (_rec == Recording.Reverse && !dead)
            {
                frameCount--;
                if (frameCount < 1)
                {
                    _rec = Recording.Playback;
                    frameCount++;
                }
            }
        }

        public void startPlayingBack()
        {
            startPlayingBack(playbackFile);
        }

        public void startPlayingBack(string Filename)
        {

            _history = new List<bool[]>();
            
            string x = FlxU.loadFromDevice(Filename);

            string[] y = x.Split('\n');

            int line = 0;

            foreach (var item in y)
            {
                string[] item1 = item.Split(',');
                
                line++;
                if (item1.Length == 14)
                {
                    try
                    {
                        _history.Add(new bool[] { bool.Parse(item1[0]), 
                            bool.Parse(item1[1]), 
                            bool.Parse(item1[2]), 
                            bool.Parse(item1[3]), 
                            bool.Parse(item1[4]), 
                            bool.Parse(item1[5]), 
                            bool.Parse(item1[6]), 
                            bool.Parse(item1[7]),
                            bool.Parse(item1[8]), 
                            bool.Parse(item1[9]), 
                            bool.Parse(item1[10]), 
                            bool.Parse(item1[11]),
                            bool.Parse(item1[12]),
                            bool.Parse(item1[13])});
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("History Not Added " + item1.Length + " -- " + item1[1]);
                    }
                }
            }
            _rec = Recording.Playback;
            frameCount = 0;
        }



        public void updateInputs()
        {
            PlayerIndex pi;

            
            
            



            bool buttonLeft = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.Left]);
            bool buttonRight = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.Right]);
            bool buttonUp = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.Up]);
            bool buttonDown = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.Down]);
            bool buttonA = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.A]);
            bool buttonX = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.X]);
            bool mouseLeftButton = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.LeftMouse]);
            bool mouseRightButton = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.RightMouse]);
            bool buttonRightShoulder = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.RightShoulder]);
            bool buttonRightTrigger = ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][(int)FlxRecord.ButtonMap.RightTrigger]);



            bool rightTriggerControl = FlxG.gamepads.isButtonDown(Buttons.RightTrigger, FlxG.controllingPlayer, out pi) || FlxG.keys.CONTROL ;
            bool rightShoulderControl = FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, FlxG.controllingPlayer, out pi);
            bool leftControl = (
                (FlxG.keys.A 
                || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, FlxG.controllingPlayer, out pi) 
                || FlxG.gamepads.isButtonDown(Buttons.DPadLeft)) 
                && isClimbingLadder==false
                );
            bool rightControl = (
                (FlxG.keys.D 
                || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, FlxG.controllingPlayer, out pi) 
                || FlxG.gamepads.isButtonDown(Buttons.DPadRight)) 
                && !isClimbingLadder
                );
            bool upControl = (FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp, FlxG.controllingPlayer, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadUp)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi);
            bool downControl = (FlxG.keys.S || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown, FlxG.controllingPlayer, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadDown)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi);
            bool buttonAControl = (_jump >= 0 || framesSinceLeftGround < 10 || isClimbingLadder) && ( FlxG.keys.SPACE || FlxG.gamepads.isButtonDown(Buttons.A, FlxG.controllingPlayer, out pi));
            bool buttonXControl = FlxG.keys.K || FlxG.gamepads.isButtonDown(Buttons.X, FlxG.controllingPlayer, out pi) || FlxG.mouse.pressedRightButton() || mouseRightButton;
            bool mouseLeftControl = FlxG.mouse.justPressedLeftButton();

            if (isPlayerControlled == false)
            {
                rightTriggerControl = mouseLeftControl = rightShoulderControl = upControl = downControl = leftControl = rightControl = buttonAControl = buttonXControl = false;
            }


            // ----------------------------------------------------------------


            // Running pushes walk speed higher.
            if (buttonRightTrigger || rightTriggerControl)
            {
                lastAttack = "range";
                maxVelocity.X = runSpeed * 2;
                attackingMelee = false;
            }
            else
            {
                maxVelocity.X = runSpeed;
            }
            acceleration.X = 0;








            if (buttonLeft || leftControl)
            {
                lastAttack = "range";
                attackingJoystick = false;
                attackingMouse = false;
                facing = Flx2DFacing.Left;
                acceleration.X -= drag.X;
                attackingMelee = false;
            }

            //Walking right.
            else if (buttonRight || rightControl)
            {
                lastAttack = "range";
                attackingJoystick = false;
                attackingMouse = false;
                facing = Flx2DFacing.Right;
                acceleration.X += drag.X;
                attackingMelee = false;
            }

            // ladders
            if (buttonUp || upControl)
            {
                lastAttack = "range";
                x = ladderPosX + width;

                velocity.Y = -100;
                isClimbingLadder = true;
                attackingMelee = false;

                // on a ladder, snap to nearest 16
            }
            else if (buttonDown || downControl)
            {
                lastAttack = "range";
                x = ladderPosX + width;

                velocity.Y = 100;
                isClimbingLadder = true;
                attackingMelee = false;
            }
            else
            {
                //isClimbingLadder = false;
            }

            

            // Jumping.
            if ( (buttonA && color == Color.White) || buttonAControl)
            {
                lastAttack = "range";
                if (framesSinceLeftGround < 10)
                {
                    _jump = 0.0f;
                    framesSinceLeftGround = 10000;
                }
                if (isClimbingLadder)
                {
                    _jump = 0.0f;
                    isClimbingLadder = false;
                }

                attackingJoystick = false;
                attackingMouse = false;
                attackingMelee = false;

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
                    velocity.Y = _jumpPower;
            }

            //Console.WriteLine("jump= " + _jump + " " + canClimbLadder);

            // Attacking
            if (FlxG.keys.justPressed(Keys.M))
            {
                lastAttack = "range";

                attackingMouse = true;
                attackingMelee = false;
            }
            if (buttonRightShoulder || rightShoulderControl)
            {
                lastAttack = "range";

                attackingJoystick = true;
                attackingMelee = false;
            }


            //FlxG.gamepads.isButtonDown(Buttons.X, PlayerIndex.One, out pi)

            //if ((GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > DEADZONE ||
            //    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > DEADZONE ||
            //    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < DEADZONE * -1.0f ||
            //    GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < DEADZONE * -1.0f) &&

            //    FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder)

            //    )
            //{
            //    lastAttack = "range";
            //    attackingJoystick = true;
            //    attackingMelee = false;
            //}

            

            // Attacking using mouse.
            if (mouseLeftControl || mouseLeftButton)
            {
                lastAttack = "range";
                attackingMouse = true;
                attackingMelee = false;
            }



            if (buttonXControl || buttonX)
            {
                lastAttack = "melee";
                attackingMelee = true;
            }


            
        }


    }
}

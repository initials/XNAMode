using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

namespace Revvolvver
{
    /// <summary>
    /// The main player class.
    /// </summary>
    public class PlayerMulti : FlxSprite
    {
        private Texture2D ImgSpaceman;
        //private const string SndJump = "Mode/jump";
        //private const string SndLand = "Mode/land";
        //private const string SndExplode = "Mode/asplode";
        //private const string SndExplode2 = "Mode/menu_hit_2";
        //private const string SndHurt = "Mode/hurt";
        //private const string SndJam = "Mode/jam";

        private float _jumpPower = -180.0f;
        private List<FlxObject> _bullets;
        private int _curBullet;

        private List<FlxObject> _bombs;
        private int _curBomb;

        private int _bulletVel;
        private bool _up;
        private bool _down;
        //private float _restart = 0.0f;
        private FlxEmitter _gibs;
        public bool shoot = false;
        public float timeSinceLastShot = 0.0f;

        public PlayerIndex? controller;
        public int controllerAsInt;

        private const int BULLETS_PER_REVOLVER = 6;

        public int bulletsLeft = 6;

        private const string SndClick = "Revvolvver/sfx/gunclick";
        private const string SndGun1 = "Revvolvver/sfx/gunshot1";
        private const string SndGun2 = "Revvolvver/sfx/gunshot2";
        private const string SndGun3 = "Revvolvver/sfx/gunshot3";

        private float timeOnZeroBullets = 0.0f;
        private float maxTimeOnZeroBullets = Revvolvver_Globals.GameSettings[6].GameValue;

        public float machineGun = 12.0f;

        public float speed = 1.0f;

        /// <summary>
        /// [0] - x
        /// [1] - y
        /// [2] - shooting?
        /// [3] - facing 
        /// </summary>
        private List<bool[]> _history = new List<bool[]>();
        //private bool _playback;
        public int frameCount;

        private Recording _rec = Recording.None;

        /// <summary>
        /// holds the amount of time character has been jumping.
        /// </summary>
        private float _jump = 0.0f;

        private float _jumpInitialPower = -140.0f;

        private float _jumpMaxTime = 0.25f;

        private float _jumpInitialTime = 0.065f;
        /// <summary>
        /// How many frames have passed since the character left the ground.
        /// </summary>
        public float framesSinceLeftGround;
        public int runSpeed = 120;

        public enum Recording
        {
            None = 0,
            Recording = 1,
            Playback = 2,
            Reverse = 3,
            RecordingController = 4
        }


        public PlayerMulti(int X, int Y, List<FlxObject> Bullets, FlxEmitter Gibs, List<FlxObject> Bombs)
            : base(X, Y)
        {
            //_playback = false;
            frameCount = 0;

            ImgSpaceman = FlxG.Content.Load<Texture2D>("Revvolvver/spaceman_new");

            loadGraphic(ImgSpaceman, true, true, 23,23);
            //_restart = 0;

            //bounding box tweaks
            width = 17;
            height = 19;
            offset.X = 3;
            offset.Y = 4;

            //basic player physics
            runSpeed = (int)Revvolvver_Globals.GameSettings[5].GameValue;
            drag.X = runSpeed * 8;
            drag.Y = runSpeed * 8;

            acceleration.Y = 840;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;



            //bullet stuff
            _bullets = Bullets;
            _curBullet = 0;
            _bulletVel = (int)Revvolvver_Globals.GameSettings[8].GameValue;

            _bombs = Bombs;
            _curBomb = 0;

            //Gibs emitted upon death
            _gibs = Gibs;

            originalPosition = new Vector2(x, y);

            facing = Flx2DFacing.Right;
        }

        public void addAnims()
        {
            int offset = (controllerAsInt - 1) * 11;

            //animations
            addAnimation("idle", new int[] { 0+offset });
            addAnimation("run", new int[] { 7 + offset, 8 + offset, 9 + offset, 10 + offset }, 12);
            addAnimation("jump", new int[] { 1 + offset, 2 + offset, 3 + offset, 4 + offset });
            addAnimation("idle_up", new int[] { 0 + offset });
            addAnimation("run_up", new int[] { 7 + offset, 8 + offset, 9 + offset, 10 + offset }, 12);
            addAnimation("jump_up", new int[] { 1 + offset, 2 + offset, 3 + offset, 4 + offset });
            addAnimation("jump_down", new int[] { 1 + offset, 2 + offset, 3 + offset, 4 + offset });
        }

        override public void update()
        {
            PlayerIndex pi;

            if (onFloor)
            {
                framesSinceLeftGround = 0;
            }
            else
            {
                framesSinceLeftGround++;
            }


            if (bulletsLeft <= 0)
            {

                timeOnZeroBullets += FlxG.elapsed;

            }
            else
            {
                timeOnZeroBullets = 0;
            }

            if (timeOnZeroBullets > maxTimeOnZeroBullets)
            {
                bulletsLeft = 6;
                timeOnZeroBullets = 0.0f;
            }

            // Running pushes walk speed higher.
			if (FlxG.gamepads.isButtonDown(Buttons.RightShoulder, controller, out pi) || ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][11] == true))
            {
                
                maxVelocity.X = runSpeed * 2;
                
            }
            else
            {
                maxVelocity.X = runSpeed;

            }

            // Recording
            if (_rec == Recording.Playback && !dead)
            {

                frameCount++;

                //Console.WriteLine("Rec is Playback");

                if (frameCount > _history.Count - 1)
                {
                    //Console.WriteLine("FrameCount>>>>>>>>>>>>>>_history");
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




            if (FlxG.debug == true)
            {
                if (_rec == Recording.Recording)
                {


                }
                if (_rec == Recording.RecordingController)
                {

                    _history.Add(new bool[] { (FlxG.gamepads.isButtonDown(Buttons.DPadUp, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp)), //0
                    (FlxG.gamepads.isButtonDown(Buttons.DPadRight, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight)), //1
                    (FlxG.gamepads.isButtonDown(Buttons.DPadDown, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown)), //2
                    (FlxG.gamepads.isButtonDown(Buttons.DPadLeft, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft)), //3
                    FlxG.gamepads.isButtonDown(Buttons.A, controller, out pi), //4
                    FlxG.gamepads.isButtonDown(Buttons.B, controller, out pi), //5
                    FlxG.gamepads.isNewButtonPress(Buttons.X, controller, out pi), //6
                    FlxG.gamepads.isButtonDown(Buttons.Y, controller, out pi), //7
                    FlxG.gamepads.isButtonDown(Buttons.LeftShoulder, controller, out pi), //8
                    FlxG.gamepads.isButtonDown(Buttons.LeftTrigger, controller, out pi), //9
                    FlxG.gamepads.isButtonDown(Buttons.RightShoulder, controller, out pi), //10
                    FlxG.gamepads.isButtonDown(Buttons.RightTrigger, controller, out pi) //11
                });


                }


                //if (_rec == Recording.Playback && controllerAsInt==2)
                //{
                //    Console.Write(frameCount);
                //    for (int i = 0; i < 12; i++)
                //    {
                //        Console.Write(_history[frameCount][i] + "-" + i.ToString() + ",");
                //    }
                //    Console.WriteLine("_");
                //}

                if (FlxG.gamepads.isNewButtonPress(Buttons.LeftShoulder, controller, out pi))
                {
                    if (_rec == Recording.None)
                    {
                        _rec = Recording.RecordingController;
                        _history = null;
                        _history = new List<bool[]>();

                    }
                    else if (_rec == Recording.RecordingController)
                    {
                        string _historyString = "";
                        foreach (var item in _history)
                        {
                            _historyString += item[0].ToString() + "," + item[1].ToString() + "," + item[2].ToString() + "," + item[3].ToString() + "," +
                                item[4].ToString() + "," + item[5].ToString() + "," + item[6].ToString() + "," + item[7].ToString() + "," +
                                item[8].ToString() + "," + item[9].ToString() + "," + item[10].ToString() + "," + item[11].ToString() + "\n";
                        }

                        FlxU.saveToDevice(_historyString, ("Revvolvver/Level" + FlxG.level.ToString() + "_" + controller.ToString() + "PlayerData.txt"));

                        _rec = Recording.Playback;

                        startPlayingBack();

                    }

                }
            }


            //if (controller == PlayerIndex.One) Console.WriteLine("_recMode: " + _rec.ToString() + " frameCount: " + frameCount);
            /*
            if (FlxG.gamepads.isNewButtonPress(Buttons.LeftShoulder, controller, out pi))
            {
                if (_rec == Recording.None)
                {
                    _rec = Recording.Recording;
                }
                else if (_rec == Recording.Playback)
                {
                    _rec = Recording.Recording;
                }
                else if (_rec == Recording.Recording)
                {
                    string _historyString = "";
                    foreach (var item in _history)
                    {
                        _historyString += item[0].ToString() + "," + item[1].ToString() + "," + item[2].ToString() + "," + item[3].ToString() + "\n";
                    }

                    FlxU.saveToDevice(_historyString, (controller.ToString() + "PlayerData.txt"));

                    _rec = Recording.Playback;
                }
                else if (_rec == Recording.Reverse)
                {
                    _rec = Recording.None;
                }

                Console.WriteLine("Recording Mode is: " + _rec);


            }
            if (FlxG.gamepads.isButtonDown(Buttons.LeftStick, controller, out pi))
            {

                _rec = Recording.Recording;
                _history = null;
                _history = new List<float[]>();


            }

            if (FlxG.gamepads.isButtonDown(Buttons.RightStick, controller, out pi))
            {
                startPlayingBack();
            }
             */

            if (speed < 0.01f) speed += 0.00004f;
            else if (speed < 1.0f) { 
                speed += 0.15f;
                //return;
            }
            else speed = 1;

            


            if (!dead)
            {

                //MOVEMENT
                acceleration.X = 0;
                if (( (_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][3] == true) || (FlxG.keys.LEFT && controller == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadLeft, controller, out pi))
                {
                    facing = Flx2DFacing.Left;
                    acceleration.X -= drag.X * speed;
                }
                else if (( (_rec == Recording.Playback || _rec == Recording.Reverse ) && _history[frameCount][1] == true) || (FlxG.keys.RIGHT && controller == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadRight, controller, out pi))
                {
                    facing = Flx2DFacing.Right;
                    acceleration.X += drag.X * speed;
                }

                //Console.WriteLine(speed + "accel " + acceleration.X);
                //Jumping.

                //if ( ((FlxG.keys.justPressed(Keys.X) &&
                //    controller == PlayerIndex.One) || FlxG.gamepads.isNewButtonPress(Buttons.A, controller, out pi) || ((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][4] == true))
                //    && velocity.Y == 0)
                //{
                //    velocity.Y = -205;
                //    //FlxG.play(SndJump);
                //}

                // Mario style jumping

                if (speed> 0.2f &&  (_jump >= 0 || framesSinceLeftGround < 10) && (((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][4] == true) || (FlxG.keys.X && controller == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.A, controller, out pi)))
                {
                    
                    if (framesSinceLeftGround < 10)
                    {
                        _jump = 0.0f;
                        framesSinceLeftGround = 10000;
                    }

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



                //AIMING
                _up = false;
                _down = false;
				if (((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][0] == true) || (FlxG.keys.UP && controller == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadUp, controller, out pi)) _up = true;
				else if (((_rec == Recording.Playback || _rec == Recording.Reverse) && _history[frameCount][2] == true) || ((FlxG.keys.DOWN && controller == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp, controller, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadDown, controller, out pi)) && velocity.Y != 0) _down = true;

                //ANIMATION
                if (speed < 0.2f)
                {
                    play("idle");
                }
                else if (velocity.Y != 0)
                {
                    if (_up) play("jump_up");
                    else if (_down) play("jump_down");
                    else play("jump");
                }
                else if (velocity.X == 0)
                {
                    if (_up) play("idle_up");
                    else play("idle");
                }
                else
                {
                    if (_up) play("run_up");
                    else play("run");
                }

                //bool shootForPlayback = false;
                //if (_rec == Recording.Playback || _rec == Recording.Reverse)
                //{
                //    if (_history[frameCount] != null)
                //    {
                //        if (_history[frameCount][2] == true) shootForPlayback = true;
                //    }
                //}
                // ((_rec == Recording.Playback || _rec == Recording.Reverse) && shootForPlayback == true)
                //SHOOTING


                // !flickering()

                if (speed> 0.2f  && ((shoot && timeSinceLastShot > 0.45f) || ((_rec == Recording.Playback || _rec == Recording.Reverse)
                    && _history[frameCount][6] == true) || (FlxG.keys.C && controller == PlayerIndex.One) ||
                        FlxG.gamepads.isButtonDown(Buttons.X, controller, out pi)))
                {
                    if (machineGun < Revvolvver_Globals.GameSettings[7].GameValue)
                    {
                        int bXVel = 0;
                        int bYVel = 0;
                        int bX = (int)x;
                        int bY = (int)y;
                        if (_up)
                        {
                            bY -= (int)_bullets[_curBullet].height - 4;
                            bYVel = -_bulletVel;
                        }
                        else if (_down)
                        {
                            bY += (int)height - 4;
                            bYVel = _bulletVel;
                            velocity.Y -= 36;
                        }
                        else if (facing == Flx2DFacing.Right)
                        {
                            bX += (int)width - 4;
                            bXVel = _bulletVel;
                        }
                        else
                        {
                            bX -= (int)_bullets[_curBullet].width - 4;
                            bXVel = -_bulletVel;
                        }
                        ((BulletMulti)(_bullets[_curBullet])).shoot(bX, bY, bXVel, bYVel, color);
                        ((BulletMulti)(_bullets[_curBullet])).firedFromPlayer = controller.ToString() + "Machine";
                        ((BulletMulti)(_bullets[_curBullet])).bulletNumber = bulletsLeft;
                        //FlxG.play(SndGun1, 0.25f);

                        int notchToRender = 6 - bulletsLeft;
                        if (controller.ToString() == "Two")
                        {
                            notchToRender += 6;
                        }
                        if (controller.ToString() == "Three")
                        {
                            notchToRender += 12;
                        }
                        if (controller.ToString() == "Four")
                        {
                            notchToRender += 18;
                        }

                        bulletsLeft--;
                        if (++_curBullet >= _bullets.Count)
                            _curBullet = 0;
                    }
                }

				if (speed > 0.2f && ((shoot && timeSinceLastShot > 0.25f) || ((_rec == Recording.Playback || _rec == Recording.Reverse)
				                && _history [frameCount] [6] == true) || (FlxG.keys.justPressed (Keys.C) && controller == PlayerIndex.One) ||
				                FlxG.gamepads.isNewButtonPress (Buttons.X, controller, out pi))) {
					// && machineGun > 6.9999f
					// && controllerAsInt <= Revvolvver_Globals.PLAYERS;


					if (bulletsLeft <= 0 )
                    {
						if (_rec == Recording.Playback || _rec == Recording.Reverse) {

						} else {
							FlxG.play (SndClick, 0.25f);
						}
                        return;
                    }
                    int bXVel = 0;
                    int bYVel = 0;
                    int bX = (int)x;
                    int bY = (int)y;
                    if (_up)
                    {
                        bY -= (int)_bullets[_curBullet].height - 4;
                        bYVel = -_bulletVel;
                    }
                    else if (_down)
                    {
                        bY += (int)height - 4;
                        bYVel = _bulletVel;
                        velocity.Y -= 36;
                    }
                    else if (facing == Flx2DFacing.Right)
                    {
                        bX += (int)width - 4;
                        bXVel = _bulletVel;
                    }
                    else
                    {
                        bX -= (int)_bullets[_curBullet].width - 4;
                        bXVel = -_bulletVel;
                    }
                    ((BulletMulti)(_bullets[_curBullet])).shoot(bX, bY, bXVel, bYVel, color);
                    ((BulletMulti)(_bullets[_curBullet])).firedFromPlayer = controller.ToString();
                    ((BulletMulti)(_bullets[_curBullet])).bulletNumber = bulletsLeft;

                    if (controllerAsInt == 1)
                    {
                        FlxG.play(("Revvolvver/sfx/p1Shoot" + ((int)FlxU.random(1, 6)).ToString()), 0.35f);
                    }
                    else
                    {
                        FlxG.play(("Revvolvver/sfx/shoot" + ((int)FlxU.random(1, 11)).ToString()), 0.35f);
                    }

                    int notchToRender = 6 - bulletsLeft;

                    

                    if (controller.ToString() == "Two")
                    {
                        notchToRender += 6;
                    }
                    if (controller.ToString() == "Three")
                    {
                        notchToRender += 12;
                    }
                    if (controller.ToString() == "Four")
                    {
                        notchToRender += 18;
                    }

                    //Console.WriteLine((FlxG._game.hud.hudGroup.members.Count));

                    (FlxG._game.hud.hudGroup.members[notchToRender] as FlxSprite).play("missed");
                    (FlxG._game.hud.hudGroup.members[notchToRender] as FlxSprite).debugName = "missed";

                    bulletsLeft--;




                    if (++_curBullet >= _bullets.Count)
                        _curBullet = 0;
                }



                if (speed > 0.2f && ((shoot && timeSinceLastShot > 0.25f) || ((_rec == Recording.Playback || _rec == Recording.Reverse)
                    && (_history[frameCount][5] == true || FlxU.random()<0.0075f)) || (FlxG.keys.justPressed(Keys.V) && controller == PlayerIndex.One) ||
                        FlxG.gamepads.isNewButtonPress(Buttons.B, controller, out pi)))
                {
                    if ( ! ((Bomb)(_bombs[_curBomb])).onScreen() ) {
                        ((Bomb)(_bombs[_curBomb])).x = x-60;
                        ((Bomb)(_bombs[_curBomb])).y = y-60;
                        ((Bomb)(_bombs[_curBomb])).explodeTimer = 0.0f;
                        ((Bomb)(_bombs[_curBomb])).scale = 0.1f;
                        ((Bomb)(_bombs[_curBomb])).color = color;
                    }
                    if (++_curBomb >= _bombs.Count)
                        _curBomb = 0;
                }



            }

            machineGun += FlxG.elapsed;
            

            //UPDATE POSITION AND ANIMATION
            base.update();

            if (shoot) timeSinceLastShot = 0.0f;

            shoot = false;

            timeSinceLastShot += FlxG.elapsed;


            if (dead && angularVelocity == 0)
            {
                reset(originalPosition.X, originalPosition.Y);
                angle = 0;
                dead = false;

            }
        }

        public void startPlayingBack()
        {
            _history = new List<bool[]>();

            //string x = FlxU.loadFromDevice("Level" + FlxG.level.ToString() + "_" + controller.ToString() + "PlayerData.txt");
            string x = FlxU.loadFromDevice("Revvolvver/Level1_" + controller.ToString() + "PlayerData.txt");

            

            string[] y = x.Split('\n');

            int line = 0;

            foreach (var item in y)
            {
                string[] item1 = item.Split(',');

                //Console.WriteLine(float.Parse(item1[0]) + " + " + float.Parse(item1[1]) + " + " + float.Parse(item1[2]) + " + " + float.Parse(item1[3]));

                //Console.WriteLine(item1.Length + " -- " + item1[1] + " -- " + bool.Parse(item1[1]));
                //Console.WriteLine(controller.ToString() + " + Line: " + line + "Item: " + item);
                line++;
                if (item1.Length == 12  )
                {
                    try
                    {
                        _history.Add(new bool[] { bool.Parse(item1[0]), bool.Parse(item1[1]), bool.Parse(item1[2]), bool.Parse(item1[3]), 
                    bool.Parse(item1[4]), bool.Parse(item1[5]), bool.Parse(item1[6]), bool.Parse(item1[7]),
                    bool.Parse(item1[8]), bool.Parse(item1[9]), bool.Parse(item1[10]), bool.Parse(item1[11])});
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("History Not Added " + controller.ToString() + " " + item1.Length + " -- " + item1[1]);

                    }
                }

                //Console.WriteLine(item1.Length + " -- " + item1[1]);

            }

            //Console.WriteLine(_history.Count);

            _rec = Recording.Playback;
            frameCount = 0;


        }

        override public void hitBottom(FlxObject Contact, float Velocity)
        {
            _jump = 0.0f;

            //if (velocity.Y > 50)
            //    FlxG.play(SndLand);
            onFloor = true;
            base.hitBottom(Contact, Velocity);
        }

        override public void hurt(float Damage)
        {
            Damage = 0;
            if (flickering())
                return;
            //FlxG.play(SndHurt);
            flicker(1.3f);
            if (FlxG.score > 1000) FlxG.score -= 1000;
            if (velocity.X > 0)
                velocity.X = -maxVelocity.X;
            else
                velocity.X = maxVelocity.X;
            base.hurt(Damage);
        }

        override public void kill()
        {
            if (dead)
                return;
            solid = false;

            base.kill();
            flicker(-1);
            exists = true;
            visible = false;
            FlxG.quake.start(0.005f, 0.35f);
            FlxG.flash.start(new Color(0xd8, 0xeb, 0xa2), 0.35f, null, false);
            if (_gibs != null)
            {
                _gibs.at(this);
                _gibs.start(true, 0, 50);
            }
        }
    }
}

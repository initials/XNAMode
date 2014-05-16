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
    class SmallCrate : FlxSprite
    {
        public FlxObject parent;
        private float trampolineTimer;
        private float throwTimer;
        private bool canParent;

        public SmallCrate(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/smallCrateExplode"), true, false, 32, 32);

            acceleration.Y = Lemonade_Globals.GRAVITY;

            width = 30;
            height = 23;
            setOffset(1,9);
            addAnimation("blink", new int[] {0,1}, 2);
            addAnimation("explode", new int[] {2,3,4,5,6,7}, 9, false);
            addAnimation("reset", new int[] {8}, 0);

            addAnimationCallback(killAfterAnimation);

            play("blink");

            setDrags(340, 340);

            parent = null;
            trampolineTimer = float.MaxValue;
            throwTimer = float.MaxValue;
            canParent = false;
        }

        /// <summary>
        /// [animationCallback] - Animation call back resets the crate after exploding.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Frame"></param>
        /// <param name="FrameIndex"></param>
        public void killAfterAnimation(string Name, uint Frame, int FrameIndex) 
        {
            if (Name == "explode" && Frame == _curAnim.frames.Length - 1) 
            {
                reset(originalPosition.X, originalPosition.Y);
                play("blink");
            }
        }
        override public void update()
        {
            
            trampolineTimer += FlxG.elapsed;
            throwTimer += FlxG.elapsed;

            if ((FlxG.keys.justPressed(Keys.C) &&
                (FlxG.keys.DOWN || FlxG.keys.S))
                ||
                FlxG.gamepads.isNewButtonPress(Buttons.X) &&
                (FlxG.gamepads.isButtonDown(Buttons.DPadDown) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown))

                )
            {
                canParent = true;
                Console.WriteLine("Pressed!");
            }
            else
            {
                canParent = false;
            }

            if (parent != null)
            {
                @fixed = false;
                solid = false;
                

                if (((FlxSprite)(parent)).facing == Flx2DFacing.Right)
                {
                    x = (parent.x-width/2) + 24;
                    y = parent.y;

                    if (FlxG.keys.justPressed(Keys.C)|| FlxG.gamepads.isNewButtonPress(Buttons.X))
                    {
                        throwCrate("Right");
                        return;
                    }
                }
                else if (((FlxSprite)(parent)).facing == Flx2DFacing.Left)
                {
                    x = (parent.x - width / 2) - 12;
                    y = parent.y;

                    if (FlxG.keys.justPressed(Keys.C) || FlxG.gamepads.isNewButtonPress(Buttons.X))
                    {
                        throwCrate("Left");
                        return;
                    }

                }



                if (parent.dead == true) parent = null;
                acceleration.Y = 0;
            }
            else if (trampolineTimer < 0.0555f)
            {
                acceleration.Y = 0;
            }
            else
            {
                acceleration.Y = Lemonade_Globals.GRAVITY;
                @fixed = true;
                solid = true;
                
            }

            base.update();

        }

        /// <summary>
        /// Kill the small crate.
        /// </summary>
        public override void kill()
        {
            base.kill();
        }

        public void throwCrate(string Direction)
        {
            int velX = 500;
            int velY = -200;

            if (FlxG.keys.UP || FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.DPadUp) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp))
            {
                velY -= 400;
            }
            else if (FlxG.keys.DOWN || FlxG.keys.S || FlxG.gamepads.isButtonDown(Buttons.DPadDown) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown))
            {
                velY = 10;
                velX = 10;
            }

            parent = null;
            canParent = false;

            if (Direction == "Left")
            {
                velocity.X = velX * -1;
                velocity.Y = velY;
                x -= width;
            }
            else if (Direction == "Right")
            {
                x += width;
                velocity.X = velX;
                velocity.Y = velY;
            }
        }
        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            if (Math.Abs(velocity.X) > 1 || Math.Abs(velocity.Y) > 1)
            {
                if (obj.GetType().ToString() == "Lemonade.Worker" && obj.dead == false)
                {
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledWorker = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;
                    
                    FlxG.play("Lemonade/sfx/SndExp", 0.5f, false);
                    
                }
                else if (obj.GetType().ToString() == "Lemonade.Army" && obj.dead == false)
                {
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledArmy = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;

                    FlxG.play("Lemonade/sfx/SndExp", 0.5f, false);

                }
                else if (obj.GetType().ToString() == "Lemonade.Chef" && obj.dead == false)
                {
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledChef = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;

                    FlxG.play("Lemonade/sfx/SndExp", 0.5f, false);

                }
                else if (obj.GetType().ToString() == "Lemonade.Inspector" && obj.dead == false)
                {
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledInspector = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;

                    FlxG.play("Lemonade/sfx/SndExp", 0.5f, false);
                }
            }

            if (obj.GetType().ToString() == "Lemonade.Liselot" || obj.GetType().ToString() == "Lemonade.Andre")
            {
				if (((FlxPlatformActor)(obj)).control == FlxPlatformActor.Controls.player && parent == null) {
					FlxG.showHud ();

					//Console.WriteLine("Small crate is at {0} {1} collider is {2} {3} Zoom {4} ", x, y, this.getScreenXY().X, this.getScreenXY().Y, FlxG.zoom);


					if (FlxG.BUILD_TYPE == FlxG.BUILD_TYPE_PC)
					{

						if (FlxG.lastControlTypeUsed == FlxG.CONTROL_TYPE_KEYBOARD) 
                        {
                            FlxG._game.hud.setHudGamepadButton(
                                FlxHud.TYPE_KEYBOARD_DIRECTION, 
                                FlxHud.Keyboard_Arrow_Down, 
                                (this.getScreenXY().X * FlxG.zoom) - 80,
                                (this.getScreenXY().Y * FlxG.zoom) - 120);

							FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_KEYBOARD, 
                                FlxHud.Keyboard_C,
                                (this.getScreenXY().X * FlxG.zoom) + 20,
                                (this.getScreenXY().Y * FlxG.zoom) - 120);

						} 
                        else if (FlxG.lastControlTypeUsed == FlxG.CONTROL_TYPE_GAMEPAD) 
                        {
                            FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_XBOX_DIRECTION, 
                                FlxHud.xboxDPadDown, 
                                (this.getScreenXY().X * FlxG.zoom) - 80,
                                (this.getScreenXY().Y * FlxG.zoom) - 120);

							FlxG._game.hud.setHudGamepadButton (FlxHud.TYPE_XBOX, 
                                FlxHud.xboxButtonX,
                                (this.getScreenXY().X * FlxG.zoom) + 20,
                                (this.getScreenXY().Y * FlxG.zoom) - 120);
	                    
						}
					}
					if (FlxG.BUILD_TYPE == FlxG.BUILD_TYPE_OUYA) {
                        FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_OUYA_DIRECTION, 
                            FlxHud.ouyaDPadDown, 
							(this.getScreenXY().X * FlxG.zoom) - 80, 
							(this.getScreenXY().Y * FlxG.zoom) - 120);

                        FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_OUYA, 
                            FlxHud.ouyaButtonU,
							(this.getScreenXY().X * FlxG.zoom) + 20, 
							(this.getScreenXY().Y * FlxG.zoom) - 120);
					}

                    FlxG._game.hud.resetTime();
                    FlxG._game.hud.timeToShowButton = 0.05f;

                    throwTimer = 0;

                    if (canParent)
                    {
                        parent = obj;
                        Console.WriteLine("can parent == True);");

                    }

                    canParent = false;
                }
            }
            else
            {
                canParent = false;
            }

            if (obj.GetType().ToString() == "Lemonade.Trampoline")
            {
                trampolineTimer = 0;
                velocity.Y = -1000;
            }

            if (obj.GetType().ToString() == "Lemonade.Spike")
            {
                play("explode");
            }

            

        }
        // end
    }
}

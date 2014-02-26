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

        }

        /// <summary>
        /// Animation call back resets the crate after exploding.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Frame"></param>
        /// <param name="FrameIndex"></param>
        public void killAfterAnimation(string Name, uint Frame, int FrameIndex) 
        {
            if (Name == "explode" && Frame == _curAnim.frames.Length - 1) {
                //kill();
                reset(originalPosition.X, originalPosition.Y);
                play("blink");


            }
        }
        override public void update()
        {
            trampolineTimer += FlxG.elapsed;

            if (parent != null)
            {
                if (((FlxSprite)(parent)).facing == Flx2DFacing.Right)
                {

                    x = (parent.x-width/2);
                    y = parent.y;
                }
                else if (((FlxSprite)(parent)).facing == Flx2DFacing.Left)
                {

                    x = (parent.x - width / 2);
                    y = parent.y;
                }
                if (parent.dead == true) parent = null;
                acceleration.Y = 0;
            }
            else if (trampolineTimer < 0.5f)
            {
                acceleration.Y = 0;
            }
            else
            {
                acceleration.Y = Lemonade_Globals.GRAVITY;
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

        public override void overlapped(FlxObject obj)
        {


            if (Math.Abs(velocity.X) > 1)
            {
                if (obj.GetType().ToString() == "Lemonade.Worker" && obj.dead == false)
                {
                    Console.WriteLine(" Original X {0} {1}   dead {2}", originalPosition.X, originalPosition.Y, obj.dead);
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledWorker = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;
                }
                else if (obj.GetType().ToString() == "Lemonade.Army" && obj.dead == false)
                {
                    Console.WriteLine(" Original X {0} {1}   dead {2}", originalPosition.X, originalPosition.Y, obj.dead);
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledArmy = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;
                }
                else if (obj.GetType().ToString() == "Lemonade.Chef" && obj.dead == false)
                {
                    Console.WriteLine(" Original X {0} {1}   dead {2}", originalPosition.X, originalPosition.Y, obj.dead);
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledChef = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;
                }
                else if (obj.GetType().ToString() == "Lemonade.Inspector" && obj.dead == false)
                {
                    Console.WriteLine(" Original X {0} {1}   dead {2}", originalPosition.X, originalPosition.Y, obj.dead);
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledInspector = true;
                    obj.kill();
                    play("explode");
                    velocity.X = 0;
                }
            }

            if (obj.GetType().ToString() == "Lemonade.Andre" ||
                obj.GetType().ToString() == "Lemonade.Liselot")
            {
                if (parent == null)
                {
                    FlxG.showHud();
                    
                    //FlxG._game.hud.setHudGamepadButton(FlxButton.ControlPadX, x, y - 100);

                    FlxG._game.hud.hudGroup.visible = true;
                    
                    FlxG._game.hud.hudGroup.members[0].x = x+30;
                    FlxG._game.hud.hudGroup.members[0].y = y - 30;

                    FlxG._game.hud.hudGroup.members[1].x = x - 30;
                    FlxG._game.hud.hudGroup.members[1].y = y - 30;
                    
                    FlxG._game.hud.timeToShowButton = 2.0f;
                }

                if (
                    (FlxG.keys.justPressed(Keys.C) && FlxG.keys.DOWN) ||
                    (FlxG.gamepads.isNewButtonPress(Buttons.X) && (FlxG.gamepads.isButtonDown(Buttons.DPadDown) || (FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown))))
                    )
                {

                    if (parent == null)
                    {
                        Console.WriteLine("Parent == null " + FlxG.elapsedTotal);
                        parent = obj;
                    }
                }

                else if (FlxG.keys.justPressed(Keys.C) || (FlxG.gamepads.isNewButtonPress(Buttons.X)))
                {
                    int velY = -200;

                    if (FlxG.keys.UP || FlxG.keys.W || FlxG.gamepads.isButtonDown(Buttons.DPadUp) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp))
                    {
                        velY -= 400;
                    }
                    if (parent != null)
                    {
                        Console.WriteLine("Parent != null " + FlxG.elapsedTotal);
                        //velocity.X = 500;
                        parent = null;

                        if (((FlxSprite)(obj)).facing == Flx2DFacing.Left)
                        {
                            velocity.X = -500;
                            velocity.Y = velY;
                            x -= width;
                        }
                        else if (((FlxSprite)(obj)).facing == Flx2DFacing.Right)
                        {
                            x += width;
                            velocity.X = 500;
                            velocity.Y = velY;
                        }
                    }
                }
            }

            if (obj.GetType().ToString() == "Lemonade.Trampoline")
            {
                //Console.WriteLine("small craete is overlapping?? + + " + obj.GetType().ToString());
                
                trampolineTimer = 0;
                //velocity.X = 100;
                velocity.Y = -1000;
                
            }

            if (obj.GetType().ToString() == "Lemonade.Spike")
            {
                play("explode");
            }

            base.overlapped(obj);

        }
        // end
    }
}

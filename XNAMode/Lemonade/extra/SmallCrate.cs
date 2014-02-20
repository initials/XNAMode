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
            addAnimation("explode", new int[] {2,3,4,5,6,7}, 12);
            addAnimation("reset", new int[] {8}, 0);

            play("blink");

            setDrags(340, 340);

            parent = null;
            trampolineTimer = float.MaxValue;

        }

        override public void update()
        {
            trampolineTimer += FlxG.elapsed;

            if (parent != null)
            {
                if (((FlxSprite)(parent)).facing == Flx2DFacing.Right)
                {

                    x = (parent.x-width/2) + width;
                    y = parent.y;
                }
                else if (((FlxSprite)(parent)).facing == Flx2DFacing.Left)
                {

                    x = (parent.x - width / 2) - width;
                    y = parent.y;
                }

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

        public override void kill()
        {
            //base.kill();

            reset(originalPosition.X, originalPosition.Y);


        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            //
            if (obj.GetType().ToString() == "Lemonade.Worker" ||
                obj.GetType().ToString() == "Lemonade.Army" || 
                obj.GetType().ToString() == "Lemonade.Inspector" ||
                obj.GetType().ToString() == "Lemonade.Chef" )
            {
                kill();

            }
            if (obj.GetType().ToString()=="Lemonade.Andre" ||
                obj.GetType().ToString()=="Lemonade.Liselot")
            {
                if (
                    (FlxG.keys.justPressed(Keys.C) && FlxG.keys.DOWN) || 
                    (FlxG.gamepads.isNewButtonPress(Buttons.X) && (FlxG.gamepads.isButtonDown(Buttons.DPadDown) || (FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown)  )))
                    )
                {

                    if (parent == null)
                    {
                        //Console.WriteLine("Parent == null " + FlxG.elapsedTotal);
                        parent = obj;
                    }
                }

                else if (FlxG.keys.justPressed(Keys.C) || (FlxG.gamepads.isNewButtonPress(Buttons.X)))
                {

                    if (parent != null)
                    {
                        //Console.WriteLine("Parent != null " + FlxG.elapsedTotal);
                        //velocity.X = 500;
                        parent = null;
                    
                        if ( ((FlxSprite)(obj)).facing == Flx2DFacing.Left) {
                            velocity.X = -500;
                            velocity.Y = -200;
                            x -= width;
                        }
                        else if (((FlxSprite)(obj)).facing == Flx2DFacing.Right)
                        {
                            x += width;
                            velocity.X = 500;
                            velocity.Y = -200;
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
            

        }
        // end
    }
}

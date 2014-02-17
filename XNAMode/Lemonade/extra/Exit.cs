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
    class Exit : FlxSprite
    {

        public Exit(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/exit"), true, false, 66, 110);
        
            if ((FlxG.level>=1 && FlxG.level <= 12) ||  FlxG.level==37 || FlxG.level==38 || FlxG.level==39 || FlxG.level==40 ) {
                addAnimation("open", new int[] { 0,0,0,1,0,0,1,0,1,0,2 }, 12, false);
                addAnimation("closed", new int[] {2,2,2,3,2,2,3,2,3,2 }, 12, true);
            }
            else if ((FlxG.level>=13 && FlxG.level <= 24) ||  FlxG.level==41 || FlxG.level==42 || FlxG.level==43 || FlxG.level==44  ) {
                addAnimation("open", new int[] { 4, 4, 4, 5, 4, 4, 5, 4, 5, 4,6 }, 12, false);
                addAnimation("closed", new int[] {6,6,6,7,6,6,7,6,7,6 }, 12, true);
            }        
            else if ((FlxG.level>=25 && FlxG.level <= 36) ||  FlxG.level==45 || FlxG.level==46 || FlxG.level==47 || FlxG.level==48 ) {
                addAnimation("open", new int[] { 8, 8, 8, 9, 8, 8, 9, 8, 9, 8,10 }, 12, false);
                addAnimation("closed", new int[] {10,10,10,11,10,10,11,10,11,10 }, 12, true);

            }
            else  {
                addAnimation("open", new int[] { 0, 0, 0, 1, 0, 0, 1, 0, 1, 0 ,2 }, 12, false);
                addAnimation("closed", new int[] {2,2,2,3,2,2,3,2,3,2 }, 12, true);
            }

            play("closed");

            width = 44;
            setOffset(11, 0);
            

            
        }

        override public void update()
        {


            base.update();

        }


    }
}

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
    class FilingCabinet : FlxSprite
    {
        public float canClose;

        public FilingCabinet(int xPos, int yPos, int type)
            : base(xPos, yPos)
        {
			
            if (type==1)
                loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/FilingCab1"), true, false, 34, 90);

		    else if (type == 2)
                loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/FilingCab2"), true, false, 34, 60);
				
            addAnimation("open", new int[] { 4 }, 0, false);
            addAnimation("closed", new int[] { 4,3,3,2,2,1,1,0 }, 12, false);

            play("open");

            canClose = 100;
        }

        override public void update()
        {
            canClose += FlxG.elapsed;

            base.update();

        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            if (obj.GetType().ToString() == "Lemonade.Liselot" || obj.GetType().ToString() == "Lemonade.Andre")
            {
		        if (canClose > 4){

                    FlxG.play("Lemonade/sfx/checkPoint", 0.8f, false);

			        play("closed", true);
				
			        canClose = 0;
				
		        }
            }


        }


    }
}
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
    public class MeleeHitBox : FlxSprite
    {

        public string belongsTo;

        public MeleeHitBox(int xPos, int yPos)
            : base(xPos, yPos)
        {
            //alpha = 0.0f;

            if (FlxG.debug)
            {
                visible = true;
                alpha = 0.0175f;
            }
            else
            {
                visible = false;
            }

        }

        override public void update()
        {



            base.update();

        }

        public override void kill()
        {
            //base.kill();
        }


    }
}

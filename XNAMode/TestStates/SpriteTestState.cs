using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class SpriteTestState : FlxState
    {

        override public void create()
        {
            base.create();

            FlxG.resetHud();

            for (int i = 0; i < 50; i++)
            {
                Firefly f = new Firefly((int)FlxU.random(0, 200), (int)FlxU.random(0, 200));
                add(f);

            }



            for (int i = 0; i < 10; i++)
            {
                int xp = (int)FlxU.random(0, 200);
                int yp = (int)FlxU.random(0, 200);

                for (int j = 0; j < 10; j++)
                {
                    Firefly f = new Firefly(xp + (int)FlxU.random(-10, 10), yp - (int)FlxU.random(-10, 10));
                    f.color = Color.Red;
                    add(f);
                }

            }



        }

        override public void update()
        {




            base.update();
        }


    }
}

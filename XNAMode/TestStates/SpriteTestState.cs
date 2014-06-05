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

            FlxG.levelWidth = 501;
            FlxG.levelHeight = 501;

            for (int i = 0; i < 50; i++)
            {
                Firefly f = new Firefly((int)FlxU.random(0, 200), (int)FlxU.random(0, 200));
                f.color = FlxColor.ToColor("#ff0000");
                add(f);

            }



            for (int i = 0; i < 10; i++)
            {
                int xp = (int)FlxU.random(0, 200);
                int yp = (int)FlxU.random(0, 200);

                for (int j = 0; j < 10; j++)
                {
                    Firefly f = new Firefly(xp + (int)FlxU.random(-10, 10), yp - (int)FlxU.random(-10, 10));
                    f.color = FlxColor.ToColor("#00ff00");
                    add(f);
                }
            }



            int cx = 130;
            int cy = 130;
            int count = 0;

            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    PowerUp p = new PowerUp(cx, cy);
                    add(p);

                    p.velocity.X = -50 + (j * 25);
                    p.velocity.Y = ((-10 + i) * 10);

                    p.TypeOfPowerUp(count++);
                }

            }





        
        }

        override public void update()
        {




            base.update();
        }


    }
}

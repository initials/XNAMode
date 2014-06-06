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



            int cx = 120;
            int cy = 120;
            int count = 0;

            FourChambers_Globals.collectedTreasures[2] = 1;
            FourChambers_Globals.collectedTreasures[3] = 1;
            FourChambers_Globals.collectedTreasures[4] = 1;
            FourChambers_Globals.collectedTreasures[12] = 1;
            FourChambers_Globals.collectedTreasures[14] = 1;
            FourChambers_Globals.collectedTreasures[42] = 1;
            FourChambers_Globals.collectedTreasures[43] = 1;
            FourChambers_Globals.collectedTreasures[44] = 1;
            FourChambers_Globals.collectedTreasures[45] = 1;
            FourChambers_Globals.collectedTreasures[46] = 1;
            FourChambers_Globals.collectedTreasures[22] = 1;
            FourChambers_Globals.collectedTreasures[23] = 1;
            FourChambers_Globals.collectedTreasures[24] = 1;
            FourChambers_Globals.collectedTreasures[21] = 1;
            FourChambers_Globals.collectedTreasures[22] = 1;
            FourChambers_Globals.collectedTreasures[23] = 1;
            FourChambers_Globals.collectedTreasures[24] = 1;
            FourChambers_Globals.collectedTreasures[25] = 1;
            FourChambers_Globals.collectedTreasures[26] = 1;
            FourChambers_Globals.collectedTreasures[27] = 1;

            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    PowerUp p = new PowerUp(cx, cy);
                    add(p);

                    //p.velocity.X = -50 + (j * 25);
                    //p.velocity.Y = ((-10 + i) * 10);

                    p.velocity.X = ((j-7) * 15) ;
                    p.velocity.Y = ((i-12) * 15) ;
                    //p.setDrags(1011, 1011);

                    if (FourChambers_Globals.collectedTreasures[count]!=1)
                    {
                        //p.angularVelocity = 200;
                        p.scalesDown = true;
                    }

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

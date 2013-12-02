using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class CleanTestState : FlxState
    {
        List<int> slotNumbers = new List<int>() { 1, 2, 3, 4 };

        List<int> timesPressed = new List<int>() { 0,0,0,0};

        bool hasSlotted;

        FlxBar bar;

        override public void create()
        {
            base.create();

            //cheats//

            //doSlots();


            hasSlotted = false;

            bar = new FlxBar(20, 20, 10, 1);
            add(bar);

        }

        public void doSlots()
        {
            slotNumbers[0] = (int)FlxU.random(0, 10);
            slotNumbers[1] = (int)FlxU.random(0, 10);
            slotNumbers[2] = (int)FlxU.random(0, 10);
            slotNumbers[3] = (int)FlxU.random(0, 10);

            foreach (var item in slotNumbers) Console.Write(item + ",");
            Console.WriteLine("\n");

        }

        public void doLevels()
        {
            FourChambers_Globals.availableLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n");

            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n");

            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n");

            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n");

            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n");


            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));
            FourChambers_Globals.availableLevels.RemoveAt((int)FlxU.random(0, FourChambers_Globals.availableLevels.Count));



            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",");
            Console.WriteLine("\n + ... " + FourChambers_Globals.availableLevels.Count);
        }

        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.A))
            {
                timesPressed[0]++;
                bar.filledBar.width -= 1;
            }
            if (FlxG.keys.justPressed(Keys.S))
            {
                timesPressed[1]++;
                bar.filledBar.width += 1;
            }
            if (FlxG.keys.justPressed(Keys.D))
            {
                timesPressed[2]++;
            }
            if (FlxG.keys.justPressed(Keys.F))
            {
                timesPressed[3]++;
            }

            if (elapsedInState > 2.0f && !hasSlotted)
            {

                if (timesPressed[0] == 0 && 
                    timesPressed[1] == 0 && 
                    timesPressed[2] == 0 && 
                    timesPressed[3] == 0)
                {
                    doSlots();
                }
                else
                {
                    // has pressed some buttons.
                    slotNumbers = timesPressed;
                    foreach (var item in slotNumbers) Console.Write(item + ",");
                    Console.WriteLine("\n");
                }

                hasSlotted = true;
            }

            base.update();

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new CleanTestState();
            }
        }


    }
}

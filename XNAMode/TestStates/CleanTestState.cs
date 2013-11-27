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

        override public void create()
        {
            base.create();

            FourChambers_Globals.availableLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            foreach (var item in FourChambers_Globals.availableLevels) Console.Write(item + ",") ;
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




            base.update();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.flixel
{
    /// <summary>
    /// FlxGlobals stores a bunch of constants
    /// </summary>
    class FlxGlobal
    {
        //public const int xTILE_SIZE_X = 16;
        //public const int xTILE_SIZE_Y = 16;
    }


    class FourChambers_Globals
    {
        public const int TILE_SIZE_X = 16;
        public const int TILE_SIZE_Y = 16;

        public const float GRAVITY = 820.0f;

        public static int BUILD_TYPE = 0;

        public const int BUILD_TYPE_RELEASE = 0;
        public const int BUILD_TYPE_PRESS = 1;

        public static bool DEMO_VERSION = false;

        public static int PLAYER_ACTOR = 1;

        public const int PLAYER_ARCHER = 1;
        public const int PLAYER_MISTRESS = 2;
        public const int PLAYER_WARLOCK = 3;

        public static bool PIRATE_COPY = false;

        public static bool seraphineHasBeenKilled = false;

        /// <summary>
        /// Keeps track of how many things in a row you've hit with arrows.
        /// </summary>
        public static int arrowCombo = 0;

        public static int arrowsFired = 0;

        public static int arrowsHitTarget = 0;

        public static int arrowsToFire = 3;

        
        public static List<int> availableLevels ;

        //public static Dictionary<string, int> turnProgress;


        public static void startGame()
        {
            
            FlxG.score = 0;
            FourChambers_Globals.seraphineHasBeenKilled = false;
            FourChambers_Globals.availableLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            int newLevel = (int)FlxU.random(0, FourChambers_Globals.availableLevels.Count);
            FlxG.level = FourChambers_Globals.availableLevels[newLevel];
            FourChambers_Globals.availableLevels.RemoveAt(newLevel);

            Console.WriteLine("STARTGAME() " + FourChambers_Globals.availableLevels[newLevel] + "  New Level:  " + newLevel);

        }


    }

    class Mode_Globals
    {
        public static bool CREATE_FROM_OGMO_LEVEL = false;

    }
}

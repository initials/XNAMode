using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.flixel
{
    /// <summary>
    /// FlxGlobals stores a bunch of constants
    /// </summary>
    public class FlxGlobal
    {
        //public const int xTILE_SIZE_X = 16;
        //public const int xTILE_SIZE_Y = 16;
    }

    /// <summary>
    /// Globals for the game Four Chambers.
    /// </summary>
    public class FourChambers_Globals
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

        /// <summary>
        /// Releases a lot of Homing Zingers.
        /// </summary>
        public static bool PIRATE_COPY = false;

        /// <summary>
        /// Keeps track of whether you can use the Seraphine to fly in a level.
        /// </summary>
        public static bool seraphineHasBeenKilled = false;

        /// <summary>
        /// Keeps track of how many things in a row you've hit with arrows.
        /// </summary>
        public static int arrowCombo = 0;

        public static int arrowsFired = 0;

        public static int arrowsHitTarget = 0;

        /// <summary>
        /// 
        /// </summary>
        public static int arrowsToFire = 1;
        
        /// <summary>
        /// A list of available level numbers. 
        /// Levels are removed as they are played, making the List smaller.
        /// </summary>
        public static List<int> availableLevels ;

        /// <summary>
        /// Allows playstates to see what the current cheat to run is.
        /// </summary>
        public static string cheatString;

        /// <summary>
        /// 
        /// </summary>
        public static bool goldenRun  = false;

        /// <summary>
        /// This is called at the beginning of every game. Reset all globals here.
        /// </summary>
        public static void startGame()
        {
            
            FlxG.score = 0;
            FourChambers_Globals.seraphineHasBeenKilled = false;
            FourChambers_Globals.availableLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            int newLevel = (int)FlxU.random(0, FourChambers_Globals.availableLevels.Count);
            FlxG.level = FourChambers_Globals.availableLevels[newLevel];
            

            Console.WriteLine("startGame() " + FourChambers_Globals.availableLevels[newLevel] + "  New Level:  " + newLevel + " " + availableLevels.Count );

            FourChambers_Globals.availableLevels.RemoveAt(newLevel);

        }

        public static void advanceToNextLevel()
        {
            FourChambers_Globals.seraphineHasBeenKilled = false;

            if (availableLevels.Count == 0)
            {
                FourChambers_Globals.availableLevels = new List<int>() { 21, 22, 23, 24, 25, 26, 27 };

                FlxG.level = 1000;
                Console.WriteLine("advanceToNextLevel() " );
            }
            else
            {
                int newLevel = (int)FlxU.random(0, FourChambers_Globals.availableLevels.Count);
                FlxG.level = FourChambers_Globals.availableLevels[newLevel];
                Console.WriteLine("advanceToNextLevel() " + FourChambers_Globals.availableLevels[newLevel] + "  New Level:  " + newLevel + " " + availableLevels.Count);
                FourChambers_Globals.availableLevels.RemoveAt(newLevel);
            }
        }

        /// <summary>
        /// Allows the FlxConsole to run commands.
        /// </summary>
        /// <param name="Cheat">Name of the cheat you want to run.</param>
        public static void runCheat(string Cheat)
        {
            if (Cheat == "arrows") FlxG.log("Current Arrows to Fire: " + FourChambers_Globals.arrowsToFire);
            else if (Cheat.StartsWith("arrows")) FourChambers_Globals.arrowsToFire = Convert.ToInt32(Cheat[Cheat.Length-1].ToString());
            else if (Cheat.StartsWith("whatisgame")) FlxG.log("Four Chambers");
            else if (Cheat.StartsWith("liketheangels")) FourChambers_Globals.seraphineHasBeenKilled = false;
            else if (Cheat.StartsWith("bigmoney")) FlxG.score += 20000;

            cheatString = Cheat;

        }
    }

    public class Mode_Globals
    {

        public static bool CREATE_FROM_OGMO_LEVEL = false;

        /// <summary>
        /// Number of players for multiplayer.
        /// </summary>
        public static int PLAYERS = 0;



    }

    public class Revvolvver_Globals
    {
        /// <summary>
        /// Number of players for multiplayer.
        /// </summary>
        public static int PLAYERS = 1;

        public static int WINNING_SCORE = 10;


    }
}

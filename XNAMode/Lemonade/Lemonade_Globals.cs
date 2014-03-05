using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;

namespace Lemonade
{
    public class Lemonade_Globals
    {
        public static string location = "military";
        public static int game_version = 2;

        public const float GRAVITY = 2040.0f;


        public static string[] characterSelected;
        public static bool[] isPlayerControlled;

        public static Dictionary<string, GameProgress> gameProgress;

        public const int DEMO_MODE = 0;
        public const int FULL_MODE = 1;
        public const int PIRATE_MODE = 2;


        public static int PAID_VERSION;

        public static int LAST_LOCATION;
        public static int LAST_SELECTED_ON_MENU;

        public static bool restartMusic = true;

        public Lemonade_Globals()
        {

        }

        public  static  void writeGameProgressToFile()
        {
            string progress = "";
            foreach (var item in gameProgress)
            {
                progress += item.Key.ToString() + ","
                    + item.Value.KilledArmy.ToString().ToLower() + ","
                    + item.Value.KilledChef.ToString().ToLower() + ","
                    + item.Value.KilledInspector.ToString().ToLower() + ","
                    + item.Value.KilledWorker.ToString().ToLower() + ","
                    + item.Value.LevelComplete.ToString().ToLower() + "\n";
            }
            FlxU.saveToDevice(progress, "gameProgress.slf");

            
        }

    }
}

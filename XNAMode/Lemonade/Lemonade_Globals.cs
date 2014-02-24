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

        public const float GRAVITY = 2040.0f;


        public static string[] characterSelected;
        public static bool[] isPlayerControlled;

        public static Dictionary<string, GameProgress> gameProgress;

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

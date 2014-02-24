using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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



    }
}

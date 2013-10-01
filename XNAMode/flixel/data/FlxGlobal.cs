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

    }
}

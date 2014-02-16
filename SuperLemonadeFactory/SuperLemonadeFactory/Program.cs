#region File Description
//-----------------------------------------------------------------------------
// Flixel for XNA.
// Original repo : https://github.com/StAidan/X-flixel
// Extended and edited repo : https://github.com/initials/XNAMode
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;

namespace Loader_SuperLemonadeFactory
{
    /// <summary>
    /// Flixel enters here.
    /// <code>FlxFactory</code> refers to it as the "masterclass".
    /// </summary>
    public class FlixelEntryPoint2 : FlxGame
    {
        public FlixelEntryPoint2(Game game)
            : base(game)
        {

            Console.WriteLine("Flixel Entry Point");

            int w = FlxG.resolutionWidth / FlxG.zoom;
            int h = FlxG.resolutionHeight / FlxG.zoom;

            initGame(w, h, new Lemonade.Lemonade(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            FlxG.debug = true;
            //FlxG.zoom = FlxG.resolutionWidth / w;
            FlxG.level = -1;

            //FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            //FourChambers_Globals.DEMO_VERSION = false;
            //FourChambers_Globals.PIRATE_COPY = false;

        }
    }
}

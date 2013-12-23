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

namespace Loader_Four
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

            int w = 320;
            int h = 180;

            initGame(w, h, new FourChambers.GameSelectionMenuState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            //FlxG.debug = true;
            FlxG.zoom = 1280 / w;
            FlxG.level = -1;

            FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            FourChambers_Globals.DEMO_VERSION = false;
            FourChambers_Globals.PIRATE_COPY = false;

        }
    }
}

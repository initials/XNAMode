﻿#region File Description
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

namespace XNAMode
{
    /// <summary>
    /// Flixel enters here.
    /// <code>FlxFactory</code> refers to it as the "masterclass".
    /// </summary>
    /// 
    /*
     * AUTO ZIP CODE.
     * 
        cd C:\Users\Shane\Documents\Visual Studio 2010\Projects\XNAMode\Mode\Mode\bin\x86
        C:\_Files\programs\7-Zip\7z a -tzip Mode.zip Release\ -r
    */
 
    public class FlixelEntryPoint : FlxGame
    {
        public FlixelEntryPoint(Game game)
            : base(game)
        {

            int w = 320;
            int h = 180;

            initGame(w, h, new DebugMenuState(), new Color(15, 15, 15), true, new Color(5, 5, 5));
                
            
            //initGame(w, h, new EmptyIntroTestState(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            //initGame(w, h, new BasePlayStateFromOel(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            //initGame(w, h, new CharacterSelectionState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            //initGame(w, h, new BasePlayStateFromOel(), new Color(15, 15, 15), false, new Color(5, 5, 5));


            //initGame(w, h, new PathTestState(), new Color(15, 15, 15), false, new Color(5, 5, 5));


            //initGame(w, h, new MenuState(), new Color(15, 15, 15), false, new Color(5, 5, 5));



            FlxG.debug = true;
            FlxG.zoom = 1280 / w;
            FlxG.level = -1;

            FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            FourChambers_Globals.DEMO_VERSION = false;
            FourChambers_Globals.PIRATE_COPY = false;

        }
    }
}

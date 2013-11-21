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
    public class FlixelEntryPoint : FlxGame
    {
        public FlixelEntryPoint(Game game)
            : base(game)
        {

            int w = 320;
            int h = 180;

            //initGame(w, h, new GameSelectionMenuState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            
            
            //initGame(w, h, new EmptyIntroTestState(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            //initGame(w, h, new BasePlayStateFromOel(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            //initGame(w, h, new CharacterSelectionState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            //initGame(w, h, new BasePlayStateFromOel(), new Color(15, 15, 15), false, new Color(5, 5, 5));


            initGame(w, h, new LevelVisualizerState(), new Color(15, 15, 15), false, new Color(5, 5, 5));


            FlxG.debug = false;
            FlxG.zoom = 1280 / w;
            FlxG.level = -1;

            FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            FourChambers_Globals.DEMO_VERSION = false;
            FourChambers_Globals.PIRATE_COPY = false;

        }
    }
}

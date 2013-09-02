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
            
            initGame(w, h, new GameSelectionMenuState(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            //initGame(w, h, new EmptyIntroTestState(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            //initGame(w, h, new BasePlayState(), new Color(15, 15, 15), false, new Color(5, 5, 5));

            FlxG.debug = true;
            FlxG.zoom = 1280 / w;
            FlxG.level = 1;
        }
    }
}

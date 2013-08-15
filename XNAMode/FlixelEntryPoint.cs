using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;

namespace XNAMode
{
    public class FlixelEntryPoint : FlxGame
    {
        public FlixelEntryPoint(Game game)
            : base(game)
        {
            initGame(640, 360, new GameSelectionMenuState(), Color.DarkSlateGray, true, Color.DarkSlateGray);
            FlxG.debug = true;

        }
    }
}

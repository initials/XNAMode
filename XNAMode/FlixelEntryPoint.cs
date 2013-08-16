﻿using System;
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
            initGame(320, 180, new GameSelectionMenuState(), new Color(15, 15, 15), true, new Color(5, 5, 5));
            FlxG.debug = true;

        }
    }
}

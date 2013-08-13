using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;

namespace XNAMode
{
    public class HelloWorld : FlxGame
    {
        public HelloWorld(Game game)
            : base(game)
        {
            initGame(640, 360, new XMLPlayState(), Color.Fuchsia, false, Color.Azure);
            FlxG.debug = true;

        }
    }
}

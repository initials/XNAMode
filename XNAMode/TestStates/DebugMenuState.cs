using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class DebugMenuState : FlxState
    {

        override public void create()
        {
            base.create();

            FlxText t = new FlxText(10, 10, FlxG.width - 20);
            add(t);
            t.text = "Choose:\n";
            t.text += "1. Level Visualizer\n";
            t.text += "2. Mode\n";


        }

        override public void update()
        {

            if (FlxG.keys.ONE) FlxG.state = new LevelVisualizerState();
            if (FlxG.keys.TWO) FlxG.state = new MenuState();
            //if (FlxG.keys.THREE) FlxG.state = 


            base.update();
        }


    }
}

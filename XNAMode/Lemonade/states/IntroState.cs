using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Flixel.Lemonade.states
{
    public class IntroState : FlxState
    {

        override public void create()
        {
            base.create();

            //FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));
            /*
                map = [FlxTilemap mapWithX:0 y:0 ];
    
                [map generateTileSetInformation:@"newyork_intro.tmx"];
    
                [map autoLoadTmxMap:@"newyork_intro.tmx" withTileImage:@"bgtiles_newyork.png" withTileSize:20];
                [self add:map];
            */


        }

        override public void update()
        {




            base.update();
        }


    }
}

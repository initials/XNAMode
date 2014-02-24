using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Lemonade
{
    public class VictoryState : FlxState
    {

        override public void create()
        {
            base.create();

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/newyork/newyork_cutscene.tmx", "map", "bg", FlxXMLReader.TILES);
            FlxTilemap ny = new FlxTilemap();
            ny.auto = FlxTilemap.STRING;
            ny.indexOffset = -1;
            ny.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_newyork"), 20, 20);
            ny.boundingBoxOverride = true;
            ny.setScrollFactors(0, 0);
            ny.visible = false;
            add(ny);



        }

        override public void update()
        {




            base.update();
        }


    }
}

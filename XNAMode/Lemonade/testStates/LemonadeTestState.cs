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
    public class LemonadeTestState : FlxState
    {

        Dictionary<string, string> levelAttrs;
        List<Dictionary<string, string>> actorsAttrs;

        override public void create()
        {
            base.create();

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            levelAttrs = FlxXMLReader.readAttributesFromOelFile("Lemonade/levels/military_level1.tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            actorsAttrs = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/military_level1.tmx", "map/layer", "bg");
            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                Console.Write("Key = {0}, Value = {1}, ", nodes, nodes);
            }
        }

  

        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.A))
            {

            }
            

            base.update();

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new XNAMode.CleanTestState();
            }
        }


    }
}

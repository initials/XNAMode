using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class LevelBeginTextState : FlxState
    {
        //float counter;
        FlxText t;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            t = new LevelBeginText(0, (FlxG.height / 2) - 40, FlxG.width);
            t.text = "START OF LEVELS";
            add(t);

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromOelFile("Lemonade/levels/slf/level1.oel", "solids");

            foreach (Dictionary<string, string> nodes in bgString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                Console.Write("\r\n");
            }

        }

        override public void update()
        {
            



            base.update();
        }


    }
}

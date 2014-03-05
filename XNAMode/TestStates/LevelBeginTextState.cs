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

            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromOel1File("Lemonade/levels/slf/level1.oel", "level/solids");

            foreach (Dictionary<string, string> nodes in bgString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                    
                }
                FlxTileblock ta = new FlxTileblock(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), Convert.ToInt32(nodes["w"]), Convert.ToInt32(nodes["h"]));
                ta.loadTiles(FlxG.Content.Load<Texture2D>("Lemonade/slf1/level1/level1_tiles"),10,10,0);
                ta.auto = FlxTileblock.AUTO;
                add(ta);

                //Console.Write("\r\n");
            }


            List<Dictionary<string, string>> bgString2 = FlxXMLReader.readNodesFromOel1File("Lemonade/levels/slf/level1.oel", "level/characters");

            foreach (Dictionary<string, string> nodes in bgString2)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);

                }

                Console.Write("\r\n");
            }


            FlxG.username = "@mramsterdaam";

        }

        override public void update()
        {

            if (FlxG.keys.A)
            {
                FlxOnlineStatCounter.sendStats("SLF2", "level1", (int)FlxG.elapsedTotal);
            }

            if (FlxG.keys.S)
            {
                FlxOnlineStatCounter.getAllStats("SLF2");
                //FlxG.write(FlxOnlineStatCounter.lastRecievedStat.ToString());
                
            }


            if (FlxG.keys.D)
            {
                //foreach (var item in FlxOnlineStatCounter.currentOnlineStats)
                //{
                //    Console.WriteLine(item.Key.ToString() + " " + item.Value.ToString());

                //}

            }


            base.update();
        }


    }
}

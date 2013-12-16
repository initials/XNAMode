using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class VCRState : FlxState
    {
        string _data;
        int _frame;
        Marksman m;
        Mistress miss;

        Dictionary<string, string> levelAttrs;
        List<Dictionary<string, string>> actorsAttrs;
        private FlxTilemap destructableTilemap;
        private FlxSprite collider;

        override public void create()
        {
            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            base.create();

            levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/military_level1.tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            actorsAttrs = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/military_level1.tmx", "map", "bg");
            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            // TMX fixes. kill newlines.
            string newStringx = actorsAttrs[0]["csvData"].Replace(",\n", "\n");
            newStringx = newStringx.Remove(0, 1);
            newStringx = newStringx.Remove(newStringx.Length - 1);


            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            destructableTilemap.indexOffset = -1;
            destructableTilemap.loadMap(newStringx, FlxG.Content.Load<Texture2D>("Lemonade/tiles_sydney"), 20, 20);
            destructableTilemap.boundingBoxOverride = true;
            add(destructableTilemap);




            m = new Marksman(40, 40, null);
            m.isPlayerControlled = true;
            add(m);

            miss = new Mistress(100, 40);
            add(miss);

        }

        override public void update()
        {
            _frame++;
            _data += _frame.ToString() + "," + FlxG.mouse.x.ToString() + "," + FlxG.mouse.y.ToString() + "\n";

            if (FlxG.keys.S)
            {
                DateTime now = DateTime.Now;

                string nowTime = now.ToString().Replace("/", "_").Replace(":", "_") ;

                SaveToDevice(_data, "replay" + nowTime  + ".txt");
            }

            FlxU.collide(m, destructableTilemap);
            FlxU.collide(miss, destructableTilemap);

            base.update();

        }

        public void SaveToDevice(string Lines, string Filename)
        {
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(Filename);
            file.WriteLine(Lines);

            file.Close();
        }

        public string LoadFromDevice(string Filename)
        {
            string value1 = File.ReadAllText(Filename);

            return value1.Substring(0, value1.Length - 1) ;

        }
    }
}

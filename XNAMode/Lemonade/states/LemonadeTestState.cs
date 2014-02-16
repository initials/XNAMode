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
        List<Dictionary<string, string>> levelString;

        List<Dictionary<string, string>> actorsString;

        private FlxTilemap destructableTilemap;

        private FlxGroup actors;
        private FlxGroup trampolines;

        private Andre andre;
        private Liselot liselot;
        private Army army;
        private Worker worker;
        private Inspector inspector;
        private Chef chef;
        private Trampoline trampoline;

        public void buildTileset() //string LevelFile, string Tiles
        {
            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/bg" + Lemonade_Globals.location + ".tmx", "map", "bg", FlxXMLReader.TILES);

            // TMX fixes. kill newlines.
            //string ext = bgString[0]["csvData"].Replace(",\n", "\n");

            //ext = ext.Remove(0, 1);
            //ext = ext.Remove(ext.Length - 1);

            FlxTilemap bgMap = new FlxTilemap();
            bgMap.auto = FlxTilemap.STRING;
            bgMap.indexOffset = -1;
            bgMap.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_" + Lemonade_Globals.location), 20, 20);
            bgMap.boundingBoxOverride = true;
            bgMap.setScrollFactors(0, 0);
            add(bgMap);


            
            
            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            levelString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.TILES);
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            // TMX fixes. kill newlines.
            //string newStringx = levelString[0]["csvData"].Replace(",\n", "\n");

            //newStringx = newStringx.Remove(0, 1);
            //newStringx = newStringx.Remove(newStringx.Length - 1);

            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            destructableTilemap.indexOffset = -1;
            destructableTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            destructableTilemap.boundingBoxOverride = true;
            add(destructableTilemap);
        }

        public void buildActors()
        {

            actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "dontAutoLoad_sprites", FlxXMLReader.ACTORS);
            foreach (Dictionary<string, string> nodes in actorsString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            //string actorsStr = actorsString[0]["csvData"].Replace(",\n", ",");

            string[] actorsSpl = actorsString[0]["csvData"].Split(',');
            int count = 0;
            foreach (string item in actorsSpl)
            {
                int xPos = ((count) % ((Convert.ToInt32(levelAttrs["width"]))));
                int yPos = ((count) / ((Convert.ToInt32(levelAttrs["width"]))));

                xPos *= Convert.ToInt32(levelAttrs["tilewidth"]);
                yPos *= Convert.ToInt32(levelAttrs["tilewidth"]);

                //Console.WriteLine(" x{0} y{1}", xPos, yPos);

                if (item == "381")
                {
                    Console.WriteLine("OK HERES AN ANDRE {0} x{1} y{2}  {3} {4}   count {5}", 
                        andre, 
                        xPos, 
                        yPos, 
                        Convert.ToInt32(levelAttrs["tilewidth"]), 
                        Convert.ToInt32(levelAttrs["width"]) , count);
                    buildActor("andre", xPos, yPos);
                }
                if (item == "382")
                {
                    Console.WriteLine("OK HERES AN liselot {0} {1} " , xPos, yPos);
                    buildActor("liselot", xPos, yPos);
                }
                if (item == "383")
                {
                    Console.WriteLine("OK HERES AN army {0} {1} ", xPos, yPos);
                    buildActor("army", xPos, yPos);
                }
                if (item == "384")
                {
                    Console.WriteLine("OK HERES AN worker {0} {1} ", xPos, yPos);
                    buildActor("worker", xPos, yPos);
                }
                if (item == "385")
                {
                    Console.WriteLine("OK HERES AN inspector {0} {1} ", xPos, yPos);
                    buildActor("inspector", xPos, yPos);
                }
                if (item == "386")
                {
                    Console.WriteLine("OK HERES AN chef {0} {1} ", xPos, yPos);
                    buildActor("chef", xPos, yPos);
                }
                if (item == "388")
                {
                    buildActor("trampoline", xPos, yPos);
                }

                count++;
            }
        }

        public void buildActor(string actor, int xPos, int yPos)
        {
            if (actor == "andre")
            {
                andre = new Andre(xPos, yPos);
                andre.control = FlxPlatformActor.Controls.player;
                andre.ControllingPlayer = PlayerIndex.One;
                actors.add(andre);
            }
            else if (actor == "liselot")
            {
                liselot = new Liselot(xPos, yPos);
                actors.add(liselot);
            }
            else if (actor == "army")
            {
                army = new Army(xPos, yPos);
                actors.add(army);
            }
            else if (actor == "chef")
            {
                chef = new Chef(xPos, yPos);
                actors.add(chef);
            }
            else if (actor == "inspector")
            {
                inspector = new Inspector(xPos, yPos);
                actors.add(inspector);
            }
            else if (actor == "worker")
            {
                worker = new Worker(xPos, yPos);
                actors.add(worker);
            }
            else if (actor == "trampoline")
            {
                trampoline = new Trampoline(xPos, yPos);
                trampolines.add(trampoline);
            }

        }

        override public void create()
        {
            FlxG.level = 1;
            Lemonade_Globals.location = "military";

            base.create();

            FlxG.autoHandlePause = true;

            actors = new FlxGroup();
            trampolines = new FlxGroup(); 

            buildTileset();
            buildActors();

            add(actors);
            add(trampolines);

            // follow.
            FlxG.followBounds(0, 
                0, 
                (int)(Convert.ToInt32(levelAttrs["tilewidth"])) * (Convert.ToInt32(levelAttrs["width"])), 
                (int)(Convert.ToInt32(levelAttrs["tileheight"])) * (Convert.ToInt32(levelAttrs["height"])));

            FlxG.follow(andre, 11.0f);

        }

        override public void update()
        {
            FlxU.collide(destructableTilemap, actors);

            FlxU.overlap(actors, actors, actorOverlap);
            FlxU.overlap(actors, trampolines, trampolinesOverlap);

            base.update();

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new FourChambers.CleanTestState();
            }
        }



        protected bool trampolinesOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((Actor)(e.Object1)).overlapped(e.Object2);
            ((Trampoline)(e.Object2)).overlapped(e.Object1);
            return true;
        }

        protected bool actorOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((Actor)(e.Object1)).overlapped(e.Object2);
            ((Actor)(e.Object2)).overlapped(e.Object1);
            return true;
        }


    }
}

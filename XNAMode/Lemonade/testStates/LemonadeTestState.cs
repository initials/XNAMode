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

        public void buildTileset()
        {
            levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/slf2/military_level1.tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            levelString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/military_level1.tmx", "map", "bg");
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            // TMX fixes. kill newlines.
            string newStringx = levelString[0]["csvData"].Replace(",\n", "\n");
            newStringx = newStringx.Remove(0, 1);
            newStringx = newStringx.Remove(newStringx.Length - 1);


            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            destructableTilemap.indexOffset = -1;
            destructableTilemap.loadMap(newStringx, FlxG.Content.Load<Texture2D>("Lemonade/tiles_military"), 20, 20);
            destructableTilemap.boundingBoxOverride = true;
            add(destructableTilemap);
        }

        public void buildActors()
        {

            actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/military_level1.tmx", "map", "dontAutoLoad_sprites");
            foreach (Dictionary<string, string> nodes in actorsString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Actors Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            string actorsStr = actorsString[0]["csvData"].Replace(",\n", ",");
            //actorsStr = actorsStr.Remove(0, 1);
            //actorsStr = actorsStr.Remove(actorsStr.Length - 1);

            string[] actorsSpl = actorsStr.Split(',');
            int count = 0;
            foreach (string item in actorsSpl)
            {
                int xPos = ((count) % ((Convert.ToInt32(levelAttrs["width"]))));
                int yPos = ((count) / ((Convert.ToInt32(levelAttrs["width"]))));

                xPos *= Convert.ToInt32(levelAttrs["tilewidth"]);
                yPos *= Convert.ToInt32(levelAttrs["tilewidth"]);

                if (item == "381")
                {
                    //Console.WriteLine("OK HERES AN ANDRE {0} x{1} y{2}", andre, xPos, yPos);
                    buildActor("andre", xPos, yPos);
                }
                if (item == "382")
                {
                    buildActor("liselot", xPos, yPos);
                }
                if (item == "383")
                {
                    buildActor("army", xPos, yPos);
                }
                if (item == "384")
                {
                    buildActor("worker", xPos, yPos);
                }
                if (item == "385")
                {
                    buildActor("inspector", xPos, yPos);
                }
                if (item == "386")
                {
                    buildActor("chef", xPos, yPos);
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


        }

        override public void create()
        {
            base.create();

            FlxG.autoHandlePause = true;

            actors = new FlxGroup();

            buildTileset();
            buildActors();

            add(actors);

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

            base.update();

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new FourChambers.CleanTestState();
            }
        }

        protected bool actorOverlap(object Sender, FlxSpriteCollisionEvent e)
        {

            return true;
        }

    }
}

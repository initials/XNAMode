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
    public class PlayState : FlxState
    {

        Dictionary<string, string> levelAttrs;
        List<Dictionary<string, string>> levelString;

        List<Dictionary<string, string>> actorsString;

        private FlxTilemap collidableTilemap;
        private FlxTilemap bgElementsTilemap;

        private FlxGroup actors;
        private FlxGroup trampolines;
        private FlxGroup levelItems;
        private FlxGroup hazards;
        private FlxGroup ramps;
        private FlxGroup smallCrates;

        private Andre andre;
        private Liselot liselot;
        private Army army;
        private Worker worker;
        private Inspector inspector;
        private Chef chef;
        private Trampoline trampoline;
        private LargeCrate largeCrate;
        private SmallCrate smallCrate;
        private Exit exit;
        private bool levelComplete = false;
        private Spike spike;
        private Ramp ramp;

        private FlxEmitter bubbleParticle;
        private FlxEmitter crateParticle;

        private const float LERP = 6.0f;

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
            bgMap.boundingBoxOverride = false;
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

            bgElementsTilemap = new FlxTilemap();
            bgElementsTilemap.auto = FlxTilemap.STRING;
            bgElementsTilemap.indexOffset = -1;
            bgElementsTilemap.stringTileMin = 201;
            bgElementsTilemap.stringTileMax = 341;
            bgElementsTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            bgElementsTilemap.boundingBoxOverride = false;
            add(bgElementsTilemap);


            collidableTilemap = new FlxTilemap();
            collidableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            collidableTilemap.indexOffset = -1;
            collidableTilemap.stringTileMax = 201;
            collidableTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            collidableTilemap.boundingBoxOverride = false;
            add(collidableTilemap);
        }

        public void buildActors()
        {

            actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.ACTORS);
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

                if (item == "357")
                {
                    buildActor("largeCrate", xPos, yPos);
                }
                if (item == "381")
                {
                    //Console.WriteLine("OK HERES AN ANDRE {0} x{1} y{2}  {3} {4}   count {5}", 
                    //    andre, 
                    //    xPos, 
                    //    yPos, 
                    //    Convert.ToInt32(levelAttrs["tilewidth"]), 
                    //    Convert.ToInt32(levelAttrs["width"]) , count);
                    buildActor("andre", xPos, yPos);
                }
                if (item == "382")
                {
                    //Console.WriteLine("OK HERES AN liselot {0} {1} " , xPos, yPos);
                    buildActor("liselot", xPos, yPos);
                }
                if (item == "383")
                {
                    //Console.WriteLine("OK HERES AN army {0} {1} ", xPos, yPos);
                    buildActor("army", xPos, yPos);
                }
                if (item == "384")
                {
                    //Console.WriteLine("OK HERES AN worker {0} {1} ", xPos, yPos);
                    buildActor("worker", xPos, yPos);
                }
                if (item == "385")
                {
                    //Console.WriteLine("OK HERES AN inspector {0} {1} ", xPos, yPos);
                    buildActor("inspector", xPos, yPos);
                }
                if (item == "386")
                {
                    //Console.WriteLine("OK HERES AN chef {0} {1} ", xPos, yPos);
                    buildActor("chef", xPos, yPos);
                }
                if (item == "387")
                {
                    //Console.WriteLine("OK HERES AN chef {0} {1} ", xPos, yPos);
                    buildActor("exit", xPos, yPos);
                }
                if (item == "388")
                {
                    buildActor("trampoline", xPos, yPos);
                }
                if (item == "389")
                {
                    buildActor("smallCrate", xPos, yPos);
                }
                if (item == "391")
                {
                    buildActor("spike_up", xPos, yPos+10);
                }
                if (item == "392")
                {
                    buildActor("spike_right", xPos, yPos);
                }
                if (item == "393")
                {
                    buildActor("spike_down", xPos, yPos);
                }
                if (item == "394")
                {
                    buildActor("spike_left", xPos+10, yPos);
                }
                if (item == "341")
                {
                    buildActor("rampLeft", xPos , yPos);
                }
                if (item == "342")
                {
                    buildActor("rampRight", xPos, yPos);
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
                xPos -= 2;
                yPos -= 2;

                trampoline = new Trampoline(xPos, yPos);
                trampolines.add(trampoline);
            }
            else if (actor == "largeCrate")
            {
                largeCrate = new LargeCrate(xPos, yPos);
                levelItems.add(largeCrate);
            }
            else if (actor == "smallCrate")
            {
                smallCrate = new SmallCrate(xPos, yPos);
                smallCrates.add(smallCrate);
            }
            else if (actor == "exit")
            {
                yPos -= 90 ;
                exit = new Exit(xPos, yPos);
                add(exit);
            }
            else if (actor == "spike_up")
            {
                spike = new Spike(xPos, yPos,0);
                hazards.add(spike);
            }
            else if (actor == "spike_right")
            {
                spike = new Spike(xPos, yPos, 1);
                hazards.add(spike);
            }
            else if (actor == "spike_down")
            {
                spike = new Spike(xPos, yPos, 2);
                hazards.add(spike);
            }
            else if (actor == "spike_left")
            {
                spike = new Spike(xPos, yPos, 3);
                hazards.add(spike);
            }
            else if (actor == "rampLeft")
            {
                ramp = new Ramp(xPos, yPos);
                ramps.add(ramp);

            }
            else if (actor == "rampRight")
            {
                ramp = new Ramp(xPos, yPos);
                ramps.add(ramp);
            }
        }

        public void buildBoxes()
        {
            List<Dictionary<string, string>> levelString = FlxXMLReader.readObjectsFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", 
                "map", 
                "boxes", 
                FlxXMLReader.NONE);
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    Console.Write("Level String -- Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                Console.Write("\r\n");
            }

            foreach (var item in levelString)
            {
                FlxSprite movingPlatform = new FlxSprite(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                movingPlatform.createGraphic(40, 20, Color.Red);
                movingPlatform.solid = true;
                movingPlatform.@fixed = true;

                add(movingPlatform);

                FlxPath xpath = new FlxPath(null);
                //xpath.add(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                xpath.addPointsUsingStrings(item["pointsX"], item["pointsY"]);
                movingPlatform.followPath(xpath, 150, FlxSprite.PATH_LOOP_FORWARD, false);
                movingPlatform.pathCornering = 0.0f;


            }
        }

        override public void create()
        {

            base.create();

            FlxG.autoHandlePause = true;

            actors = new FlxGroup();
            trampolines = new FlxGroup();
            levelItems = new FlxGroup();
            hazards = new FlxGroup();
            ramps = new FlxGroup();
            smallCrates = new FlxGroup();

            buildTileset();
            buildActors();

            buildBoxes();

            add(actors);
            add(trampolines);
            add(levelItems);
            add(hazards);
            add(ramps);
            add(smallCrates);

            //set up a little bubble particle system.

            bubbleParticle = new FlxEmitter();
            bubbleParticle.delay = 3;
            bubbleParticle.setXSpeed(-150, 150);
            bubbleParticle.setYSpeed(-40, 100);
            bubbleParticle.setRotation(-720, 720);
            bubbleParticle.gravity = Lemonade_Globals.GRAVITY * -0.25f;
            bubbleParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/bubble"), 200, true, 1.0f, 0.65f);
            add(bubbleParticle);

            crateParticle = new FlxEmitter();
            crateParticle.delay = 0;
            crateParticle.setSize(80, 60);
            crateParticle.setXSpeed(-350, 350);
            crateParticle.setYSpeed(-200, 200);
            crateParticle.setRotation(-720, 720);
            crateParticle.gravity = Lemonade_Globals.GRAVITY;
            crateParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/crateShards"), 200, true, 1.0f, 0.65f);
            add(crateParticle);

            // follow.
            FlxG.followBounds(0, 
                0, 
                (int)(Convert.ToInt32(levelAttrs["tilewidth"])) * (Convert.ToInt32(levelAttrs["width"])), 
                (int)(Convert.ToInt32(levelAttrs["tileheight"])) * (Convert.ToInt32(levelAttrs["height"])));

            FlxG.follow(andre, LERP);

            playSong();

        }

        /// <summary>
        /// Play a song based on the location.
        /// </summary>
        public void playSong()
        {

            if (Lemonade_Globals.location == "sydney")
            {
                FlxG.playMp3("Lemonade/music/cave", 0.75f);
            }
            else if (Lemonade_Globals.location == "newyork")
            {
                FlxG.playMp3("Lemonade/music/here", 0.75f);
            }
            else if (Lemonade_Globals.location == "military")
            {
                FlxG.playMp3("Lemonade/music/join", 0.75f);
            }
            else
            {
                FlxG.playMp3("Lemonade/music/join", 0.75f);
            }

        }

        override public void update()
        {
            #region cheats
            // Run cheats.
            if (FlxG.debug == true)
            {
                if (FlxGlobal.cheatString == "exits")
                {
                    andre.at(exit);
                    liselot.at(exit);
                }

                if (FlxG.keys.justPressed(Keys.F9))
                {
                    FlxG.level++;

                    FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

                    FlxG.transition.startFadeIn(0.2f);

                    FlxG.state = new PlayState();

                    return;
                }
                else if (FlxG.keys.justPressed(Keys.F7) )
                {
                    FlxG.level--;
                    //if (FlxG.level < 1) FlxG.level = 25;

                    FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");

                    FlxG.transition.startFadeIn(0.2f);

                    FlxG.state = new PlayState();

                    return;
                }
                else if (FlxG.keys.justPressed(Keys.F8) )
                {
                    FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                    FlxG.transition.startFadeIn(0.2f);
                    FlxG.state = new PlayState();
                    return;
                }
            }
            #endregion



            FlxU.collide(collidableTilemap, actors);

            FlxU.overlap(actors, actors, genericOverlap);
            FlxU.overlap(actors, trampolines, trampolinesOverlap);
            FlxU.overlap(actors, levelItems, genericOverlap);
            FlxU.overlap(actors, hazards, genericOverlap);
            FlxU.overlap(actors, smallCrates, genericOverlap);
            FlxU.overlap(smallCrates, trampolines, trampolinesOverlap);
            FlxU.collide(crateParticle, collidableTilemap);
            FlxU.collide(levelItems, collidableTilemap);
            FlxU.collide(smallCrates, collidableTilemap);

            bool andreExit = FlxU.overlap(andre, exit, exitOverlap);
            bool liselotExit = FlxU.overlap(liselot, exit, exitOverlap);

            if (andreExit && liselotExit)
            {
                levelComplete = true;
            }

            FlxU.collide(actors, levelItems);

            foreach (FlxObject crate in levelItems.members)
            {
                if (crate.GetType().ToString() == "Lemonade.LargeCrate")
                {
                    if (((LargeCrate)(crate)).canExplode && !crate.dead)
                    {
                        crateParticle.at(crate);
                        crateParticle.start(true, 0.0f, 50);
                        crate.kill();
                    }
                }
            }


            base.update();
            //FlxU.overlap(actors, ramps, rampOverlap);



            // Switch Controlling Character.
            if (FlxG.keys.justPressed(Keys.V) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            {
                if (FlxG.followTarget.GetType().ToString() == "Lemonade.Liselot")
                {
                    FlxG.follow(andre, LERP);
                    andre.control = FlxPlatformActor.Controls.player;
                    liselot.control = FlxPlatformActor.Controls.none;
                    
                    bubbleParticle.at(andre);
                    bubbleParticle.start(true, 0, 30);

                }
                else if (FlxG.followTarget.GetType().ToString() == "Lemonade.Andre")
                {
                    FlxG.follow(liselot, LERP);
                    andre.control = FlxPlatformActor.Controls.none;
                    liselot.control = FlxPlatformActor.Controls.player;

                    bubbleParticle.at(liselot);
                    bubbleParticle.start(true, 0, 30);

                }
            }

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new MenuState();
            }

            if (levelComplete == true && ! FlxG.transition.hasStarted)
            {
                FlxG.transition.startFadeOut(0.05f, -90, 150);
            }
            if (FlxG.transition.complete)
            {
                FlxG.state = new MenuState();
            }

        }


        protected bool exitOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            ((Exit)(e.Object2)).play("open", true);
	        return true;
        }

        protected bool rampOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object1.overlapped(e.Object2);
            //e.Object2.overlapped(e.Object1);
            return true;
        }

        protected bool genericOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object1.overlapped(e.Object2);
            e.Object2.overlapped(e.Object1);
            return true;
        }

        protected bool trampolinesOverlap(object Sender, FlxSpriteCollisionEvent e)
        {
            bubbleParticle.at(e.Object1);
            bubbleParticle.start(true, 0, 30);
            e.Object1.overlapped(e.Object2);
            e.Object2.overlapped(e.Object1);
            return true;
        }


        

    }
}

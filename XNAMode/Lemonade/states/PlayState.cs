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
        private FlxGroup movingPlatforms;

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
            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/bg" + Lemonade_Globals.location + ".tmx", "map", "bg", FlxXMLReader.TILES);

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
            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]) * Convert.ToInt32(levelAttrs["tilewidth"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]) * Convert.ToInt32(levelAttrs["tileheight"]);


            levelString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.TILES);
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
            bgElementsTilemap.stringTileMin = 200;
            bgElementsTilemap.stringTileMax = 341;
            bgElementsTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            bgElementsTilemap.boundingBoxOverride = false;
            add(bgElementsTilemap);


            collidableTilemap = new FlxTilemap();
            collidableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            collidableTilemap.indexOffset = -1;
            collidableTilemap.stringTileMax = 200;
            collidableTilemap.loadMap(levelString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/tiles_" + Lemonade_Globals.location), 20, 20);
            collidableTilemap.boundingBoxOverride = false;
            add(collidableTilemap);
        }

        public void buildActors()
        {

            actorsString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", "map", "bg", FlxXMLReader.ACTORS);
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

                if (item == "357")
                {
                    buildActor("largeCrate", xPos, yPos);
                }
                if (item == "381")
                {
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
                if (item == "387")
                {
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
                if (item == "363")
                {
                    buildActor("plant1", xPos, yPos);
                }
                if (item == "364")
                {
                    buildActor("plant2", xPos, yPos);
                }
                if (item == "365")
                {
                    buildActor("filing1", xPos, yPos);
                }
                if (item == "366")
                {
                    buildActor("filing1", xPos, yPos);
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
                liselot.control = FlxPlatformActor.Controls.none;
                liselot.ControllingPlayer = PlayerIndex.One;
                actors.add(liselot);
            }
            else if (actor == "army")
            {
                army = new Army(xPos, yPos);
                actors.add(army);
                army.startPlayingBack();
            }
            else if (actor == "chef")
            {
                chef = new Chef(xPos, yPos);
                actors.add(chef);
                chef.startPlayingBack();

            }
            else if (actor == "inspector")
            {
                inspector = new Inspector(xPos, yPos);
                actors.add(inspector);
                inspector.startPlayingBack();
            }
            else if (actor == "worker")
            {
                worker = new Worker(xPos, yPos);
                actors.add(worker);
                worker.startPlayingBack();

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
                ramp = new Ramp(xPos, yPos, FlxRamp.LOW_SIDE_LEFT);
                ramps.add(ramp);

            }
            else if (actor == "rampRight")
            {
                ramp = new Ramp(xPos, yPos, FlxRamp.LOW_SIDE_RIGHT);
                ramps.add(ramp);
            }
            else if (actor == "plant1")
            {
                Plant plant = new Plant(xPos, yPos-90, 1);
                hazards.add(plant);
            }
            else if (actor == "plant2")
            {
                Plant plant = new Plant(xPos, yPos - 90, 2);
                hazards.add(plant);
            }
            else if (actor == "filing1")
            {
                FilingCabinet filing = new FilingCabinet(xPos, yPos - 70, 1);
                hazards.add(filing);
            }
            else if (actor == "filing2")
            {
                FilingCabinet filing = new FilingCabinet(xPos, yPos - 40, 2);
                hazards.add(filing);
            }
        }

        public void buildBoxes()
        {
            List<Dictionary<string, string>> levelString = FlxXMLReader.readObjectsFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_level" + FlxG.level.ToString() + ".tmx", 
                "map", 
                "boxes", 
                FlxXMLReader.NONE);
            foreach (Dictionary<string, string> nodes in levelString)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Level String -- Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            foreach (var item in levelString)
            {
                MovingPlatform movingPlatform = new MovingPlatform(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                movingPlatform.solid = true;
                movingPlatform.@fixed = true;

                movingPlatforms.add(movingPlatform);

                FlxPath xpath = new FlxPath(null);
                //xpath.add(Int32.Parse(item["x"]), Int32.Parse(item["y"]));
                xpath.addPointsUsingStrings(item["pointsX"], item["pointsY"]);
                movingPlatform.followPath(xpath, 150, FlxSprite.PATH_YOYO, false);
                movingPlatform.pathCornering = 0.0f;


            }
        }

        override public void create()
        {

            base.create();

            FlxSprite _gamePadButton = new FlxSprite(0, 0);
            _gamePadButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/BP3_SSTRIP_64"), true, false, 63, 64);
            _gamePadButton.width = 61;
            _gamePadButton.height = 62;
            _gamePadButton.offset.X = 1;
            _gamePadButton.offset.Y = 1;
            _gamePadButton.addAnimation("frame", new int[] { FlxButton.ControlPadX });
            _gamePadButton.play("frame");
            _gamePadButton.solid = false;
            _gamePadButton.visible = true;
            _gamePadButton.scrollFactor.X = 1;
            _gamePadButton.scrollFactor.Y = 1;
            _gamePadButton.boundingBoxOverride = false;
            FlxG._game.hud.hudGroup.add(_gamePadButton);

            //FlxSprite _gamePadDirection = new FlxSprite(0, 0);
            //_gamePadDirection.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/BP3_SSTRIP_32"), true, false, 63, 64);
            //_gamePadDirection.width = 61;
            //_gamePadDirection.height = 62;
            //_gamePadDirection.offset.X = 1;
            //_gamePadDirection.offset.Y = 1;
            //_gamePadDirection.addAnimation("frame", new int[] { FlxButton.ControlPadLStick });
            //_gamePadDirection.play("frame");
            //_gamePadDirection.solid = false;
            //_gamePadDirection.visible = true;
            //_gamePadDirection.scrollFactor.X = 1;
            //_gamePadDirection.scrollFactor.Y = 1;
            //_gamePadDirection.boundingBoxOverride = false;
            //FlxG._game.hud.hudGroup.add(_gamePadDirection);




            FlxG.mouse.hide();

            FlxG.autoHandlePause = true;

            actors = new FlxGroup();
            trampolines = new FlxGroup();
            levelItems = new FlxGroup();
            
            ramps = new FlxGroup();
            smallCrates = new FlxGroup();
            movingPlatforms = new FlxGroup();

            hazards = new FlxGroup();

            buildTileset();
            buildActors();

            buildBoxes();

            add(actors);
            add(trampolines);
            add(levelItems);
            add(hazards);
            add(ramps);
            add(smallCrates);
            add(movingPlatforms);

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
            crateParticle.delay = float.MaxValue;
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
            if (FlxG.debug == true && elapsedInState>0.2f)
            {
                if (FlxGlobal.cheatString == "exits")
                {
                    andre.at(exit);
                    liselot.at(exit);
                }
                if (FlxGlobal.cheatString == "pirate")
                {
                    Lemonade_Globals.PAID_VERSION = Lemonade_Globals.PIRATE_MODE;
                }
                if (FlxG.keys.justPressed(Keys.F9))
                {
                    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].LevelComplete = true;

                    Lemonade_Globals.writeGameProgressToFile();

                    if (FlxG.level == 12)
                    {
                        FlxG.state = new VictoryState();
                        return;
                    }
                    else
                    {
                        FlxG.level++;
                        FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                        FlxG.state = new PlayState();

                        return;
                    }
                }
                else if (FlxG.keys.justPressed(Keys.F7) )
                {
                    FlxG.level--;
                    FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                    FlxG.state = new PlayState();

                    return;
                }
                else if (FlxG.keys.justPressed(Keys.F8) )
                {
                    FlxG.write(FlxG.level.ToString() + " LEVEL STARTING");
                    FlxG.state = new PlayState();
                    return;
                }
            }
            #endregion

            #region pirateVersion

            if (Lemonade_Globals.PAID_VERSION == Lemonade_Globals.PIRATE_MODE)
            {
                if (elapsedInState > 3.0 && FlxG.level > 2)
                {
                    foreach (var item in actors.members)
                    {
                        ((FlxPlatformActor)(item)).maxVelocity.X += 0.5f;
                    }
                }
            }

            #endregion

            //FlxU.collideRamp(actors, ramps);

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
            FlxU.collide(actors, movingPlatforms);
            FlxU.collide(smallCrates, levelItems);

            //FlxU.collideOnY(smallCrates, andre);
            //FlxU.collideOnY(smallCrates, liselot);

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

            // Switch Controlling Character.
            if (FlxG.keys.justPressed(Keys.V) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            {
                if (andre.piggyBacking)
                {

                }
                else
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
            }

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new EasyMenuState();
            }
            if (levelComplete == true)
            {
                andre.alpha -= 0.1f;
                liselot.alpha -= 0.1f;
            }
            if (levelComplete == true && ! FlxG.transition.hasStarted)
            {

                andre.control = FlxPlatformActor.Controls.none;
                liselot.control = FlxPlatformActor.Controls.none;

                FlxG.transition.startFadeOut(0.05f, -90, 150);
            }
            if (FlxG.transition.complete)
            {

                Lemonade_Globals.gameProgress[Lemonade_Globals.location+"_"+FlxG.level.ToString()].LevelComplete = true;

                Lemonade_Globals.writeGameProgressToFile();

                if (FlxG.level != 12)
                {
                    FlxG.level++;
                    FlxG.state = new PlayState();
                    FlxG.transition.resetAndStop();
                    return;
                }
                else if (FlxG.level == 12)
                {
                    FlxG.state = new VictoryState();
                    FlxG.transition.resetAndStop();
                    return;
                }
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

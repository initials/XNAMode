using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using OgmoXNA;

namespace FourChambers
{
    public class EmptyIntroTestState : FlxState
    {
        int timeOfDay;
        public FlxSprite bg;
        int bloomSettingsIndex = 0;


        override public void create()
        {
            base.create();

            bg = new FlxSprite(0, 0);
            bg.loadGraphic(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), true, false, 32, 32);
            bg.addAnimation("first", new int[] { 0,1,2,0,1,2,0,1,2}, 3);
            bg.addAnimation("second", new int[] { 0, 11, 12, 0, 1, 12, 0, 1, 12 }, 3);
            bg.play("first");
            bg.velocity.Y = 20;
            add(bg);

            bg.addAnimationCallback(pr);

            FlxG.autoHandlePause = true;

            FlxG.playMusic("music/goat");

            FlxXMLReader.readOgmoProjectAndLevel("ogmoLevels/ProjectFile.oep", "ogmoLevels/testlevel2.oel");

            //FlxXMLReader.readOgmoV2Level("ogmoLevels/testlevel2.oel");

            /*
            // Load our level using the game's content manager.  The project specified in the content processor
            // properties for this level, along with all the texture assets, will be built and loaded.
            level = new Level(this.Content.Load<OgmoLevel>(@"levels\demo\demoLevel"));
            // Load the level's font so we can show off how many coins we have gathered.
            level.Load(this.Content);
             */

        }
        /*
        override public void create()
        {

            //FlxG.backColor = Color.Gray;

            base.create();

            FlxG.level = 1;

            FlxG.hideHud();


            bg = new FlxTileblock(0, FlxG.height - 256, 256 * 3, 256);
            bg.loadTiles(FlxG.Content.Load<Texture2D>("initials/texture_placement_small"), 256, 256, 0);

            add(bg);

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            timeOfDay = 0;

            Color xc = FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/envir_dusk"), 5, 5);

            string ints = "3,4,4,3,3,4,5,6,6,6,4,5,4,54,5";
            int[] ar = FlxU.convertStringToIntegerArray(ints);


            // build level in dict

            string currentLevel = "l" + FlxG.level.ToString();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            XElement xelement = XElement.Load("levelSettings.xml");

            foreach (XElement xEle in xelement.Descendants("settings").Elements())
            {
                XElement firstSpecificChildElement = xEle.Element(currentLevel);
                
                if (firstSpecificChildElement != null)
                {
                    levelAttrs.Add(xEle.Name.ToString(), firstSpecificChildElement.Value.ToString());
                }
                else
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                }
            }

            foreach (KeyValuePair<string, string> pair in levelAttrs)
            {
                //Console.WriteLine("dict -----> {0}, {1}",
                //pair.Key,
                //pair.Value);
            }

            int[] solidColumnsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidColumnsBeforeSmooth"]);
            int[] solidRowsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidRowsBeforeSmooth"]);

            int[] emptyColumnsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyColumnsBeforeSmooth"]);
            int[] emptyRowsBeforeSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyRowsBeforeSmooth"]);

            int[] solidColumnsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidColumnsAfterSmooth"]);
            int[] solidRowsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["solidRowsAfterSmooth"]);

            int[] emptyColumnsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyColumnsAfterSmooth"]);
            int[] emptyRowsAfterSmooth = FlxU.convertStringToIntegerArray(levelAttrs["emptyRowsAfterSmooth"]);

            Console.WriteLine(solidColumnsBeforeSmooth);





        }
        */
        public void pr(string Name, uint Frame, int FrameIndex)
        {
            Console.WriteLine("Changed animation {0} {1} {2}", Name, Frame, FrameIndex);

        }

        override public void update()
        {


            //FlxG.autoHandlePause = true;


            if (FlxG.keys.M)
            {
                bg.play("first");
                //FlxG.pause = true;
            }
            if (FlxG.keys.K)
            {
                bg.play("second");
                //FlxG.pause = false;
            }

            //Console.WriteLine(FlxG.pause);



            timeOfDay++;
            //FlxG.color(FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/palette"), timeOfDay % 70, timeOfDay / 70));

            base.update();

            if (FlxG.keys.justPressed(Keys.F3))
            {
                FlxG.bloom.Visible = !FlxG.bloom.Visible;
            }

            // Cycle through the intermediate buffer debug display modes?
            if  (FlxG.keys.justPressed(Keys.F4))
            {
                FlxG.bloom.Visible = true;
                FlxG.bloom.ShowBuffer++;

                if (FlxG.bloom.ShowBuffer > BloomPostprocess.BloomComponent.IntermediateBuffer.FinalResult)
                {
                    FlxG.bloom.ShowBuffer = 0;
                }

                FlxG.write(FlxG.bloom.ShowBuffer.ToString());
            }

            if (FlxG.keys.justPressed(Keys.F5))
            {
                bloomSettingsIndex = (bloomSettingsIndex + 1) %
                         BloomPostprocess.BloomSettings.PresetSettings.Length;

                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[bloomSettingsIndex];
                FlxG.bloom.Visible = true;

                FlxG.write(FlxG.bloom.Settings.Name.ToString() + "   " + bloomSettingsIndex);
            }

            if (FlxG.keys.justPressed(Keys.F6))
            {
                FlxG.bloom.usePresets = false;
                FlxG.bloom.bloomIntensity += 0.01f;
                FlxG.bloom.bloomSaturation += 0.01f;
                FlxG.bloom.baseIntensity += 0.01f;
                FlxG.bloom.baseSaturation += 0.01f;
                FlxG.bloom.blurAmount += 0.01f;
                FlxG.bloom.bloomThreshold += 0.01f;

            }
            if (FlxG.keys.justPressed(Keys.F7))
            {
                FlxG.bloom.usePresets = true;
                FlxG.bloom.bloomIntensity = 0.0f;
                FlxG.bloom.bloomSaturation = 0.0f;
                FlxG.bloom.baseIntensity = 0.0f;
                FlxG.bloom.baseSaturation = 0.0f;
                FlxG.bloom.blurAmount = 0.0f;
                FlxG.bloom.bloomThreshold = 0.0f;
            }

            if (FlxG.keys.Z) FlxG.bloom.bloomIntensity += 0.1f;
            if (FlxG.keys.A) FlxG.bloom.bloomIntensity -= 0.1f;
            if (FlxG.keys.X) FlxG.bloom.bloomSaturation += 0.1f;
            if (FlxG.keys.S) FlxG.bloom.bloomSaturation -= 0.1f;
            if (FlxG.keys.C) FlxG.bloom.baseIntensity += 0.1f;
            if (FlxG.keys.D) FlxG.bloom.baseIntensity -= 0.1f;
            if (FlxG.keys.V) FlxG.bloom.baseSaturation += 0.1f;
            if (FlxG.keys.F) FlxG.bloom.baseSaturation -= 0.1f;
            if (FlxG.keys.B) FlxG.bloom.blurAmount += 0.1f;
            if (FlxG.keys.G) FlxG.bloom.blurAmount -= 0.1f;
            if (FlxG.keys.N) FlxG.bloom.bloomThreshold += 0.1f;
            if (FlxG.keys.H) FlxG.bloom.bloomThreshold -= 0.1f;

            // exit.
            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new GameSelectionMenuState();
                return;
            }
            if (FlxG.keys.justPressed(Keys.F9))
            {
                FlxG.state = new CutsceneState();
                return;
            }



        }
    }
}

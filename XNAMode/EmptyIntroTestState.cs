﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;



namespace XNAMode
{
    public class EmptyIntroTestState : FlxState
    {
        int timeOfDay;

        override public void create()
        {

            //FlxG.backColor = Color.Gray;

            base.create();


            FlxTileblock bg = new FlxTileblock(0, FlxG.height - 256, 256 * 3, 256);
            bg.loadTiles(FlxG.Content.Load<Texture2D>("initials/Ambience"), 256, 256, 0);

            add(bg);

            //FlxG.backColor = Color.Gray;


            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            timeOfDay = 0;

            Color xc = FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/envir_dusk"), 5, 5);

            Console.WriteLine(xc);

            string ints = "3,4,4,3,3,4,5,6,6,6,4,5,4,54,5";
            int[] ar = FlxU.convertStringToIntegerArray(ints);
            //foreach (int ix in ar)
              //  Console.WriteLine(ix.ToString());


            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            XElement xelement = XElement.Load("levelDetails.xml");

            foreach (XElement xEle in xelement.Descendants("level_1").Elements())
            {

                if (xEle.Value.ToString() == "")
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                }
                else
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Value.ToString());
                }
                //levelAttrs.Add(xEle.Name.ToString(), xEle.Value.ToString());

                //Console.WriteLine("xelement ---->"+ xEle.Value.ToString() + "\n.." + xEle.Name.ToString());


            }

            //Console.WriteLine(dictionary.ToString() + "\n");

            foreach (KeyValuePair<string, string> pair in levelAttrs)
            {
                Console.WriteLine("dict -----> {0}, {1}",
                pair.Key,
                pair.Value);
            }
        }

        override public void update()
        {

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
                    FlxG.bloom.ShowBuffer = 0;
            }


            

        }

        public override void postProcess(SpriteBatch spriteBatch)
        {
            base.postProcess(spriteBatch);

            //FlxG.bloom.BeginDraw();

        }
    }
}
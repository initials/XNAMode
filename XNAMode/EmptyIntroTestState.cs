using System;
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

            FlxG.backColor = Color.Gray;

            base.create();

            FlxG.backColor = Color.Gray;


            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            timeOfDay = 0;

            Color xc = FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/envir_dusk"), 5, 5);

            Console.WriteLine(xc);

            string ints = "3,4,4,3,3,4,5,6,6,6,4,5,4,54,5";
            int[] ar = FlxU.convertStringToIntegerArray(ints);
            foreach (int ix in ar)
                Console.WriteLine(ix.ToString());

            XElement xelement = XElement.Load("levelDetails.xml");

            foreach (XElement xEle in xelement.Descendants("level_1"))
            {
                Console.WriteLine(xEle.Value.ToString() + "\n");

            }


            FlxG.color(Color.Tomato);
            



        }

        override public void update()
        {

            timeOfDay++;
            FlxG.color(FlxU.getColorFromBitmapAtPoint(FlxG.Content.Load<Texture2D>("initials/palette"), timeOfDay % 70, timeOfDay / 70));


            base.update();
        }


    }
}

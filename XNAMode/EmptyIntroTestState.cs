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

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));


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

            

            



        }

        override public void update()
        {




            base.update();
        }


    }
}

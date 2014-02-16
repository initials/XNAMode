using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Lemonade
{
    public class Lemonade : FlxState
    {


        override public void create()
        {
            base.create();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            XmlDocument xml = new XmlDocument();
            xml.Load("Lemonade/LineEndingsUnix.txt");

            Console.WriteLine(xml);


        }





        override public void update()
        {


            base.update();


        }


    }
}
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class VCRState : FlxState
    {
        string _data;
        int _frame;

        override public void create()
        {
            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            base.create();
        }

        override public void update()
        {
            _frame++;
            _data += _frame.ToString() + "," + FlxG.mouse.x.ToString() + "," + FlxG.mouse.y.ToString() + "\n";

            if (FlxG.keys.S)
            {
                DateTime now = DateTime.Now;
                Console.WriteLine(now.ToString() + " " + now.ToLongTimeString() );

                string nowTime = now.ToString().Replace("/", "_").Replace(":", "_") ;

                SaveToDevice(_data, "replay" + nowTime  + ".txt");
            }


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

            //Console.WriteLine("--- Contents of file.txt: ---");
            //Console.WriteLine(value1);

            return value1.Substring(0, value1.Length - 1) ;


        }
    }
}

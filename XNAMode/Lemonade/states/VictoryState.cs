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
    public class VictoryState : FlxState
    {
        FlxText characterText;

        FlxText talkText;
        List<Dictionary<string, string>> scripts;


        override public void create()
        {
            base.create();

            loadTilemap("bg");
            loadTilemap("bg2");
            loadTilemap("bg3");

            characterText = new FlxText(100, FlxG.height - 100, FlxG.width, " Char Text ");
            characterText.alignment = FlxJustification.Left;
            add(characterText);

            talkText = new FlxText(100, FlxG.height - 70, FlxG.width, " Talk Text ");
            talkText.alignment = FlxJustification.Left;
            add(talkText);

            scripts = FlxXMLReader.readCustomXML( Lemonade_Globals.location + "_scene13", "Lemonade/script/script.xml");
            
            //Console.WriteLine(scripts.ToString() + "  " + scripts.Count.ToString());

            //foreach (var item in scripts)
            //{
            //    Console.WriteLine("-------------------");
            //    foreach (var item2 in item)
            //    {
                    
            //        Console.WriteLine(item2.Key.ToString() + " " + item2.Value.ToString());
            //    }
            //}

            FlxG.hideHud();
            FlxG.resetHud();
            FlxG.hideHud();

        }

        public void loadTilemap(string Layer)
        {
            List<Dictionary<string, string>> bgString = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/slf2/" + Lemonade_Globals.location + "/" + Lemonade_Globals.location + "_cutscene.tmx", "map", Layer, FlxXMLReader.TILES);
            FlxTilemap bg = new FlxTilemap();
            bg.auto = FlxTilemap.STRING;
            bg.indexOffset = -1;
            bg.loadMap(bgString[0]["csvData"], FlxG.Content.Load<Texture2D>("Lemonade/bgtiles_" + Lemonade_Globals.location), 20, 20);
            bg.boundingBoxOverride = true;
            bg.setScrollFactors(0, 0);
            add(bg);
        }

        override public void update()
        {

            if ((int)elapsedInState < scripts.Count)
            {
                characterText.text = scripts[(int)elapsedInState]["character"].ToString();
                talkText.text = scripts[(int)elapsedInState]["text"].ToString();
            }
            else
            {
                FlxG.transition.startFadeOut(0.08f, -180, 220);
                elapsedInState = 0;
            }

            if (FlxG.transition.complete)
            {
                FlxG.transition.resetAndStop();
				#if __ANDROID__
				FlxG.state = new OuyaEasyMenuState();
				#endif
				#if !__ANDROID__
				FlxG.state = new EasyMenuState();
				#endif
                return;
            }

            base.update();
        }


    }
}

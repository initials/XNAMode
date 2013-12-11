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
        List<Dictionary<string, string>> actorsAttrs;
        private FlxTilemap destructableTilemap;
        private FlxSprite collider;
        private FlxSprite collider2;

        override public void create()
        {
            base.create();

            levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromTmxFile("Lemonade/levels/military_level1.tmx", "map");

            foreach (KeyValuePair<string, string> kvp in levelAttrs)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            actorsAttrs = FlxXMLReader.readNodesFromTmxFile("Lemonade/levels/military_level1.tmx", "map", "bg");
            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    //Console.Write("Key = {0}, Value = {1}, ", kvp.Key, kvp.Value);
                }
                //Console.Write("\r\n");
            }

            // TMX fixes. kill newlines.
            string newStringx = actorsAttrs[0]["csvData"].Replace(",\n", "\n");
            newStringx = newStringx.Remove(0,1);
            newStringx = newStringx.Remove(newStringx.Length - 1);


            destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;

            // TMX maps have indexOffset of -1;
            destructableTilemap.indexOffset = -1;
            destructableTilemap.loadMap(newStringx, FlxG.Content.Load<Texture2D>("Lemonade/tiles_sydney"), 20, 20);
            destructableTilemap.boundingBoxOverride = true;
            add(destructableTilemap);

            collider = new FlxSprite(40, 40).createGraphic(2, 2, new Color(255, 0, 0));
            add(collider);

            collider2 = new FlxSprite(70, 140).createGraphic(2, 2, new Color(2, 230, 40));
            add(collider2);

            FlxG.followBounds(0,0,20 * 110, 20 * 34);
            FlxG.follow(collider, 1.0f);

        }



  

        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.A))
            {
                collider.velocity.X = collider.velocity.Y = 55;
            }

            PlayerIndex pi;

            //if (FlxG.gamepads.isNewButtonPress(Buttons.DPadDown, PlayerIndex.One, pi))
            if (FlxG.gamepads.isNewButtonPress(Buttons.DPadDown, FlxG.controllingPlayer, out pi))
            {
                collider.velocity.Y = 55;
            }
            //if (FlxG.gamepads.isNewButtonPress(Buttons.DPadDown, PlayerIndex.One, pi))
            if (FlxG.gamepads.isNewButtonPress(Buttons.DPadDown, PlayerIndex.Two, out pi))
            {
                collider2.velocity.Y = 55;
            }

            base.update();

            if (FlxG.keys.justPressed(Keys.Escape))
            {
                FlxG.state = new XNAMode.CleanTestState();
            }
        }


    }
}

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
    /// <summary>
    /// Shows how to use a path object, and follow.
    /// </summary>
    public class PathTestState : FlxState
    {
        /// <summary>
        /// A sprite to follow the path.
        /// </summary>
        FlxSprite collider;
        FlxSprite pather;

        override public void create()
        {
            base.create();

            // Create the path.
            FlxPath path = new FlxPath(null);
            path.add(10, 45);
            path.add(45, 45);
            path.add(66, 66);
            path.add(77, 77);
            path.add(88, 0);


            // Create an object to follow the path.
            collider = new FlxSprite(0, 0);
            collider.loadGraphic(FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"), false, false, 8, 8);
            add(collider);

            // Tell the sprite to follow the path. 
            collider.followPath(path, 15.0f, FlxObject.PATH_YOYO, false);


            pather = new FlxSprite(0, 0);
            pather.loadGraphic(FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"), false, false, 8, 8);
            pather.color = new Color(1.0f, 0, 0);
            add(pather);




            // Loading a path from an Ogmo Level.

            List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("ogmoLevels/level3.oel", "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                foreach (KeyValuePair<string, string> kvp in nodes)
                {
                    Console.Write("Key = {0}, Value = {1}, ",
                        kvp.Key, kvp.Value);


                }
                Console.Write("\r\n");
            }

        }


        override public void update()
        {
            base.update();
        }
    }

}
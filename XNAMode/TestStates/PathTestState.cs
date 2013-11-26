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


            pather = new FlxSprite(16, 16);
            pather.loadGraphic(FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"), false, false, 8, 8);
            pather.color = new Color(1.0f, 0, 0);
            add(pather);




            // Loading a path from an Ogmo Level.

            // Create a list 
            List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();

            // load level 3
            /// TO DO: create a template test level.
            
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("ogmoLevels/level3.oel", "level/ActorsLayer");

            // loop through the actors.
            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {

                // Check for a path in the ogmo level attrs.
                if (nodes["pathNodesX"] != "" && nodes["pathNodesY"] != "")
                {
                    //create a Path object.
                    FlxPath xpath = new FlxPath(null);

                    // add the first point of the character.
                    xpath.add(float.Parse(nodes["x"]), float.Parse(nodes["y"]));

                    //simple check - can remove later.
                    if (nodes["Name"] == "bat")
                    {
                        // add all the points.
                        xpath.addPointsUsingStrings(nodes["pathNodesX"], nodes["pathNodesY"]);

                        //convert PathType to a uint (FlxObject.PATH_LOOP_FORWARD) etc
                        uint path_type = FlxPath.convertStringValueForPathType(nodes["pathType"]);

                        // 1.
                        // make the object follow the new path.
                        
                        //pather.followPath(xpath, float.Parse(nodes["pathSpeed"]), path_type, false);
                        
                        
                        // OR
                        // 2.
                        // just assign the path to follow at an event.
                        
                        pather.assignPath(xpath, float.Parse(nodes["pathSpeed"]), path_type, false);

                    }
                }

            }

        }


        override public void update()
        {
            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B) && FlxG.debug)
                FlxG.showBounds = !FlxG.showBounds;

            if (FlxG.mouse.justPressedLeftButton())
            {
                Console.WriteLine("Start following");

                pather.startFollowingPath();

            }
            if (FlxG.mouse.justPressedRightButton())
            {
                Console.WriteLine("Start following");

                pather.stopFollowingPath();

            }
            base.update();
        }
    }

}
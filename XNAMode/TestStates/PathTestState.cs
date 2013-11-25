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

        }


        override public void update()
        {
            base.update();
        }
    }

}
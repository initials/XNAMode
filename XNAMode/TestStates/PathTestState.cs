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
    public class PathTestState : FlxState
    {
        FlxSprite collider;

        override public void create()
        {
            base.create();

            FlxPath path = new FlxPath(null);
            path.add(10, 45);
            path.add(45, 45);
            path.add(66, 66);
            path.add(77, 77);
            path.add(88, 0);
            //collider.path = path;

            collider = new FlxSprite(0, 0);
            collider.loadGraphic(FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"), false, false, 8, 8);
            add(collider);
            //collider.acceleration.Y = 5;
            collider.followPath(path, 151.0f, FlxObject.PATH_YOYO, false);

        }


        override public void update()
        {
            //if (FlxG.keys.A) collider.thrust += 5;

            base.update();
        }
    }

}
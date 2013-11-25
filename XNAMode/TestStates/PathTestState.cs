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
            path.add(0, 45);
            path.add(4, 14);
            path.add(5, 15);
            path.add(41, 2);
            path.add(0, 0);
            //collider.path = path;

            collider = new FlxSprite(0, 0);
            collider.loadGraphic(FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"), false, false, 8, 8);
            add(collider);
            //collider.acceleration.Y = 5;
            collider.followPath(path, 51.0f, FlxObject.PATH_FORWARD, false);

            Console.WriteLine(collider.path.nodes[0] + "   " + collider.path.nodes[1]);
            //collider.flicker(5);



        }


        override public void update()
        {
            //if (FlxG.keys.A) collider.thrust += 5;

            base.update();
        }
    }

}
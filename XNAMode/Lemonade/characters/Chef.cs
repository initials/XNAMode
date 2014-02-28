using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lemonade
{
    class Chef : Actor
    {
        public Chef(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 30,31,32,33,34,35 }, 14);
            addAnimation("idle", new int[] { 5 }, 0);
            addAnimation("jump", new int[] { 5 }, 0);
            addAnimation("talk", new int[] { 5, 58 }, 6);
            addAnimation("death", new int[] {101,102,103,104,105,106,107,107 }, 12, false);

            play("idle");

            width = 30;
            height = 40;
            setOffset(10, 40);
            setDrags(1251, 0);

            maxVelocity.X = 230;
            maxVelocity.Y = 2830;

            runSpeed = 35;
            setJumpValues(-250.0f, -250.0f, 0.315f, 0.0715f);
            numberOfJumps = 1;

            controlFile = "Lemonade/characters/control/chef.txt";
            _runningMax = maxVelocity.X;

        }

        override public void update()
        {
            base.update();
        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            //if (obj.GetType().ToString() == "Lemonade.SmallCrate" && (Math.Abs(obj.velocity.X) > 1 || Math.Abs(obj.velocity.Y) > 1))
            //{
            //    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledChef = true;
            //    kill();
            //}

        }
    }
}

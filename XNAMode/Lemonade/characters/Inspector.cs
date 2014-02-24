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
    class Inspector : Actor
    {
        public Inspector(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 24,25,26,27,28,29 }, 14);
            addAnimation("idle", new int[] { 3 }, 0);
            addAnimation("jump", new int[] { 3 }, 0);
            addAnimation("talk", new int[] { 3,56 }, 8);
            addAnimation("death", new int[] { 96,97,98,99,100,100,100,100 }, 12, false);

            play("idle");

            width = 30;
            height = 40;
            setOffset(10, 40);
            setDrags(500, 0);

            maxVelocity.X = 130;
            maxVelocity.Y = 2830;

            runSpeed = 12;
            setJumpValues(-250.0f, -250.0f, 0.315f, 0.0715f);
            numberOfJumps = 1;

            controlFile = "Lemonade/characters/control/inspector.txt";
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
            //    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledInspector = true;
            //    kill();
            //}

        }
    }
}

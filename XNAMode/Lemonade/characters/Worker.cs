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
    class Worker : Actor
    {
        public Worker(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/chars_50x80"), true, false, 50, 80);

            addAnimation("run", new int[] { 18,19,20,21,22,23 }, 32);
            addAnimation("idle", new int[] { 0 }, 0);
            addAnimation("jump", new int[] { 0 }, 0);
            addAnimation("talk", new int[] { 0, 53, 52, 54 }, 6);
            addAnimation("death", new int[] { 91, 92, 93, 94, 95, 95, 95, 95 }, 12, false);

            play("idle");

            runSpeed = 22;

            width = 10;
            height = 41;
            setOffset(20, 39);
            setDrags(800, 0);

            maxVelocity.X = 130;
            maxVelocity.Y = 2830;

            setJumpValues(-340.0f, -410.0f, 0.35f, 0.075f);

            controlFile = "Lemonade/characters/control/worker.txt";
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
            //    Lemonade_Globals.gameProgress[Lemonade_Globals.location + "_" + FlxG.level.ToString()].KilledWorker = true;
            //    kill();
            //}

        }
    }
}

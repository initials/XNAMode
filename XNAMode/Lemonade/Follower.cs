using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATweener;

namespace Lemonade
{
    class Follower : FlxSprite
    {

        public FlxSprite follow1;
        public FlxSprite follow2;

        public Tweener tweenX;
        public Tweener tweenY;

        public int currentFollow;

        public Follower(int xPos, int yPos)
            : base(xPos, yPos)
        {
            createGraphic(8, 8, Color.Red);

            tweenX = new Tweener(0, 0, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);

            tweenY = new Tweener(0, 0, TimeSpan.FromSeconds(0.45f), Linear.EaseNone);

            currentFollow = 1;

            visible = false;
        }

        override public void update()
        {

            tweenX.Update(FlxG.elapsedAsGameTime);
            tweenY.Update(FlxG.elapsedAsGameTime);

            x = tweenX.Position;
            y = tweenY.Position;

            if (!tweenX.Running)
            {
                if (currentFollow == 1)
                {
                    x = follow1.x;
                    y = follow1.y;
                }
                if (currentFollow == 2)
                {
                    x = follow2.x;
                    y = follow2.y;
                }

            }

            base.update();

        }


    }
}

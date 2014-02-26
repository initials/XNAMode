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
    /// <summary>
    /// A Plant that can lose its leaves.
    /// </summary>
    class Plant : FlxSprite
    {
        public float canLoseLeaves;
        protected FlxEmitter _leaves;

        /// <summary>
        /// Plant that can lose leaves
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="type">Two types of plants. 1 and 2.</param>
        public Plant(int xPos, int yPos,int type)
            : base(xPos, yPos)
        {
            if (type == 1)
            {
                loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/plant1"), false, false, 10, 110);
            }
            else if (type == 2)
            {
                loadGraphic(FlxG.Content.Load<Texture2D>("Lemonade/plant2"), false, false, 13, 110);
            }

            _leaves = new FlxEmitter();
            _leaves.setSize(2, 110);
            _leaves.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/leaves"), 30, true, 0.0f, 0.0f);
            _leaves.setRotation(0, 360);
            _leaves.setYSpeed(15, 85);
            _leaves.setXSpeed(-40, 40);
            _leaves.gravity = 15;
            _leaves.at(this);
            
            
            canLoseLeaves = 110;

        }

        override public void update()
        {
            canLoseLeaves += FlxG.elapsed;
            base.update();

            _leaves.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
            _leaves.render(spriteBatch);
        }

        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);

            if (canLoseLeaves > 10)
            {
                _leaves.start(true, 4.0f, 0);
                canLoseLeaves = 0;
            }


        }


    }
}

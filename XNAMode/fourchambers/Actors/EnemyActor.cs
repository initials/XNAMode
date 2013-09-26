using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    class EnemyActor : FlxSprite
    {
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        /// <summary>
        /// The score to recieve when killing this actor
        /// </summary>
        public int score = 50;

        public EnemyActor(int xPos, int yPos)
            : base(xPos, yPos)
        {





        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {
            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;

            }

            //ANIMATION
            if (dead)
            {
                play("death");
            }
            else if (velocity.Y != 0)
            {
                play("jump");
            }
            else if (velocity.X == 0)
            {
                play("idle");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                play("run");
            }

            else
            {
                play("idle");
            }

            base.update();

        }

        public override void kill()
        {
            FlxG.score += score;

            play("death");
            velocity.X = 0;
            velocity.Y = 0;
            dead = true;

            base.kill();
        }
    }
}

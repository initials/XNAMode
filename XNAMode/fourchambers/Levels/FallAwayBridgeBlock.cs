using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class FallAwayBridgeBlock : FlxSprite
    {
        private float  amountOfTimePlayerHasHitTop = 0.0f;

        private float maxTimePlayerCanStandOnBlock = 0.0f;

        private float delay = 0.75f;

        private float delayCounter = 0.0f;


        public FallAwayBridgeBlock(int xPos, int yPos)
            : base(xPos, yPos)
        {



            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/fallAwayBridgeTiles_16x32"), false, false, 16,32);
            width = 16;
            height = 16;

            offset.Y = 16;
            @fixed = true;


        }

        override public void update()
        {

            if (amountOfTimePlayerHasHitTop > maxTimePlayerCanStandOnBlock)
            {
                delayCounter += FlxG.elapsed;
            }
            if (delayCounter > delay/1.5)
            {
                //x += FlxU.random(-0.5, 0.5);
                y += FlxU.random(-0.5, 0.5);
            }
            if (delayCounter > delay)
            {
                @fixed = false;
                acceleration.Y = FourChambers_Globals.GRAVITY;
                if (alpha >= 0)
                    alpha -= 0.05f;

            }

            base.update();

        }

        public override void hitTop(FlxObject Contact, float Velocity)
        {
            base.hitTop(Contact, Velocity);
            
            if (Contact is Actor)
                amountOfTimePlayerHasHitTop += FlxG.elapsed;

            //@fixed = false;

            //acceleration.Y = Actor.GRAVITY;

        }

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            //base.hitBottom(Contact, Velocity);
        }

        public override void hitLeft(FlxObject Contact, float Velocity)
        {
            //base.hitLeft(Contact, Velocity);
        }

        public override void hitRight(FlxObject Contact, float Velocity)
        {
            //base.hitRight(Contact, Velocity);
        }

        public override void hitSide(FlxObject Contact, float Velocity)
        {
            //base.hitSide(Contact, Velocity);
        }

    }
}

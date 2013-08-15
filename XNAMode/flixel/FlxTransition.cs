using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    public class FlxTransition : FlxGroup
    {

        public bool transitionForward ;
        public bool transitionBackward;


        public FlxTransition()
        {
            
        }

        public FlxTransition createSprites(Texture2D Graphics, Color color, int rows, int cols, int width, int height)
        {
            members = new List<FlxObject>();

            FlxSprite s;



            for (int _y = 0; _y < rows; _y++)
            {
                for (int _x = 0; _x < cols; _x++)
                {
                    s = new FlxSprite(width * _y, height * _x);

                    s.loadGraphic(FlxG.Content.Load<Texture2D>("flixel/transition_40x40"),false,false,width,height);

                    s.angle = 45;

                    //s.angularVelocity = 15;

                    s.scale = 0.0f;

                    add(s);


                }
            }


            return this;
        }


        public void startTransition() 
        {

        }



        override public void update()
        {

            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;
                o.scale += 0.01f;

            }

            base.update();
            
        }



    }
}

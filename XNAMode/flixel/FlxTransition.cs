﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    public class FlxTransition : FlxGroup
    {

        private bool transitionForward;
        private bool transitionBackward;

        private float _speed;

        public FlxTransition()
        {
            _speed = 0.05f;
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

                    //s.angle = 45;

                    //s.angularVelocity = 15;

                    s.scale = 0.0f;

                    add(s);


                }
            }


            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Graphics"></param>
        /// <param name="color"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="angle"></param>
        /// <param name="angularVelocity"></param>
        /// <param name="speed">Speed 0.001 is slow. 1 = transition over 1 frame.</param>
        /// <returns></returns>
        public FlxTransition createSprites(Texture2D Graphics, Color color, int rows, int cols, int width, int height, float angle, float angularVelocity, float speed)
        {
            members = new List<FlxObject>();

            FlxSprite s;

            _speed = speed;



            for (int _y = 0; _y < rows; _y++)
            {
                for (int _x = 0; _x < cols; _x++)
                {
                    s = new FlxSprite(width * _y, height * _x);

                    if (Graphics==null)
                        s.loadGraphic(FlxG.Content.Load<Texture2D>("flixel/transition_40x40"), false, false, width, height);
                    else
                        s.loadGraphic(Graphics, false, false, width, height);

                    s.angle = angle;

                    s.angularVelocity = angularVelocity * _y ;

                    s.scale = 0.0f;

                    

                    add(s);


                }
            }


            return this;
        }




        public void startFadeIn() 
        {
            transitionBackward = true;

            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;
                o.scale = 1.01f;

            }
        }

        public void startFadeOut()
        {
            transitionForward = true;

            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;
                o.scale = 0.0f;

            }
        }


        public void updateTransition()
        {
            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;

                if (transitionForward)
                    o.scale += _speed;
                else if (transitionBackward)
                {
                    o.scale -= _speed;
                    if (o.scale <= 0.0f) o.scale = 0;
                }
            }
        }

        override public void update()
        {

            if (transitionBackward || transitionForward)
            {
                updateTransition();
            }

            base.update();
            
        }



    }
}

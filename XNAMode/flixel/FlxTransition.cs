﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    /// <summary>
    /// Written by @initials_games
    /// Replicates the Mario diamond transitions.
    /// </summary>
    public class FlxTransition : FlxGroup
    {

        private bool transitionForward;
        private bool transitionBackward;

        private float _speed;

        public bool complete;

        public FlxTransition()
        {
            _speed = 0.05f;
            complete = false;
            //scrollFactor = Vector2.Zero;
            scrollFactor.X = 0;
            scrollFactor.Y = 0;

        }

        public FlxTransition createSprites(Texture2D Graphics, Color color, int rows, int cols, int width, int height)
        {
            members = new List<FlxObject>();

            FlxSprite s;



            for (int _y = 0; _y < rows; _y++)
            {
                for (int _x = 0; _x < cols; _x++)
                {
                    s = new FlxSprite(width * _y * FlxG.zoom, height * _x * FlxG.zoom);

                    s.loadGraphic(FlxG.Content.Load<Texture2D>("flixel/transition_30x30"),false,false,width,height);

                    //s.angle = 45;

                    s.scale = FlxG.zoom;

                    //s.angularVelocity = 15;
                    //s.scrollFactor = Vector2.Zero;
                    s.scrollFactor.X = 0;
                    s.scrollFactor.Y = 0;

                    s.solid = false;
                    s.boundingBoxOverride = false;

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
                    s = new FlxSprite(width * _y * FlxG.zoom, height * _x * FlxG.zoom);

                    if (Graphics==null)
                        s.loadGraphic(FlxG.Content.Load<Texture2D>("flixel/transition_40x40"), false, false, width, height);
                    else
                        s.loadGraphic(Graphics, false, false, width, height);

                    s.angle = angle;

                    s.angularVelocity = angularVelocity + (angularVelocity * _y);

                    s.scale = 0.0f;

                    s.scrollFactor.X = 0;
                    s.scrollFactor.Y = 0;

                    s.solid = false;
                    s.boundingBoxOverride = false;

                    add(s);


                }
            }


            return this;
        }




        public void startFadeIn() 
        {
            startFadeIn(_speed);
        }
        public void startFadeIn(float speed)
        {
            _speed = speed;
            complete = false;
            transitionBackward = true;

            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;
                o.scale = FlxG.zoom;

            }
        }
        
        public void startFadeOut()
        {
            startFadeOut(_speed);
        }

        public void startFadeOut(float speed)
        {
            startFadeOut(speed, 0, 0);
        }

        /// <summary>
        /// Starts the fade out process, all sprites start at zero scale.
        /// </summary>
        /// <param name="speed">Speed is the amount that is added per cycle, finishing at 1.0f</param>
        /// <param name="Angle">Starting angle of each tile.</param>
        /// <param name="AngularVelocity">Angle spin.</param>
        public void startFadeOut(float speed, float Angle, float AngularVelocity)
        {
            _speed = speed;
            complete = false;
            transitionForward = true;

            FlxSprite o;
            int i = 0;
            int l = members.Count;
            while (i < l)
            {
                o = members[i++] as FlxSprite;
                o.scale = 0.0f;
                o.angle = Angle;
                o.angularVelocity = AngularVelocity;
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
                {
                    o.scale += _speed;

                    if (o.scale > FlxG.zoom + 0.2f)
                    {
                        complete = true;
                        transitionForward = false;
                        transitionBackward = true;
                    }

                }
                else if (transitionBackward)
                {
                    o.scale -= _speed;
                    if (o.scale <= 0.0f)
                    {
                        o.scale = 0;
                        //complete = true;
                    }

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

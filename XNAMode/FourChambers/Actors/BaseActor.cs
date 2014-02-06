﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class BaseActor : FlxSprite
    {
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        /// <summary>
        /// A quick helper for finding the type of actor. Must be set up manually.
        /// </summary>
        public string actorType;
        
        /// <summary>
        /// Score for killing this creature.
        /// </summary>
        public int score;

        /// <summary>
        /// Location of the file that holds the attack data.
        /// </summary>
        public string playbackFile = "FourChambers/ActorRecording/file.txt";

        public bool flying = false;

        public bool canClimbLadder = false;

        public bool isClimbingLadder = false;

        public float timeDownAfterHurt = 1.0f;

        public float ladderPosX = 0;

        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;

        public float hurtTimer = 550.0f;

        
        /// <summary>
        /// The base for Actors. Should remain pretty empty.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public BaseActor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            score = 0;
            actorName="BaseActor";
            actorType = "BaseActor";
            acceleration.Y = FourChambers_Globals.GRAVITY;
            FlxG.write("1 New BASE ACTOR");




        }

        override public void update()
        {
            hurtTimer += FlxG.elapsed;


            base.update();

            if (FourChambers_Globals.cheatString != null)
            {
                if (FourChambers_Globals.cheatString.StartsWith("control" + actorType))
                {
                    FlxG.write("Controlling " + actorType);

                    isPlayerControlled = true;

                    FourChambers_Globals.cheatString = "";

                    FlxG.follow(this, 1.0f);
                }
            }

        }


    }
}

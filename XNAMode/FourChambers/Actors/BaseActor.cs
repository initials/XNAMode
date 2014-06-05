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
    public class BaseActor : FlxSprite
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
        public bool canFly = false;

        public bool canClimbLadder = false;

        public bool isClimbingLadder = false;

        public float timeDownAfterHurt = 1.0f;

        public float ladderPosX = 0;

        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;

        public float hurtTimer = 550.0f;

        public int runSpeed = 120;
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
            //FlxG.write("1 New BASE ACTOR");




        }

        override public void update()
        {
            hurtTimer += FlxG.elapsed;


            base.update();

            if (FlxGlobal.cheatString != null)
            {
                if (FlxGlobal.cheatString.StartsWith("control" + actorType))
                {
                    FlxG.write("Controlling " + actorType);

                    isPlayerControlled = true;

                    FlxGlobal.cheatString = "";

                    FlxG.follow(this, 7.0f);
                }
            }

        }


    }
}

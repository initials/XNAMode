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
    class Actor : FlxSprite
    {
        /// <summary>
        /// 1-4;
        /// </summary>
        public int playerControlledByController = 1;

        public float jump;
        public float jumpLimit;
        public bool ableToStartJump;
        public bool isPlayerControlled;
        public bool isMale;
        public bool isAirDashing;
        public bool dontDash;
        public bool dying;
        public bool readyToSwitchCharacters;
        public float jumpInitialMultiplier;
        public float jumpInitialTime;
        public float jumpSecondaryMultiplier;
        public float jumpTimer;
        public int jumpCounter;
        public float airDashTimer;
        public float moveSpeed;
        public float timeOnLeftArrow;
        public float timeOnRightArrow;
        public bool ability_AirDash;
        public bool ability_DoubleJump;
        public bool canDoubleJump;
        public bool canJump;
        public bool hasDoubleJumped;
        public bool isPiggyBacking;
        public bool cutSceneMode;
        public int startsFirst;
        public string levelName;
        public int andreInitialFlip;
        public int liselotInitialFlip;
        public int followWidth;
        public int followHeight;
        public float levelStartX;
        public float levelStartY;

        public Actor(int xPos, int yPos)
            : base(xPos, yPos)
        {

        }

        override public void update()
        {
            base.update();
        }
    }
}

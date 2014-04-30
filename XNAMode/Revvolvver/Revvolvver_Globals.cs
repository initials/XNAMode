using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Revvolvver
{
    public class Revvolvver_Globals
    {
        /// <summary>
        /// Number of players for multiplayer.
        /// </summary>
        public static int PLAYERS = 1;

        public static bool GAMES_PLAYS_ITSELF = false;

        public static int WINNING_SCORE = 10;

        public Revvolvver_Globals()
        {

        }

        public static Revvolvver.GameSettings[] GameSettings =
        {
            //                          Name                Default,    GameValue,  Min,   Max,     increment   //Number id
            new Revvolvver.GameSettings("Clouds",           10,         10,         1,      100,    1),         //0
            new Revvolvver.GameSettings("Bombs",            4,          4,          0,      20,     1),         //1
            new Revvolvver.GameSettings("Cave",             50,         50,         40,     60,     1),         //2
            new Revvolvver.GameSettings("Regen Time",       10,         10,         3,      100,    1),         //3
            new Revvolvver.GameSettings("Winning Score",    10,         10,         1,      100,    1),         //4
			new Revvolvver.GameSettings("Run Speed",        120,        120,        50,     200,    10),        //5
            new Revvolvver.GameSettings("Bullet Regen",     4,          4,          0,      100,    1),         //6
            new Revvolvver.GameSettings("Power Up Time",    7,          7,          1,      20,     1),         //7
            new Revvolvver.GameSettings("Bullet Velocity",  360,        360,        100,    600,    20),        //8
            new Revvolvver.GameSettings("Randomonium",      0,          0,          0,      0,      0),         //9
			new Revvolvver.GameSettings("Play Now",         0,          0,          0,      0,      0),         //10
        };

    }
}

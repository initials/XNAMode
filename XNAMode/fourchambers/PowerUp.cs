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
    class PowerUp : FlxSprite
    {
        

        public int typeOfPowerUp = 0;

        public PowerUp(int xPos, int yPos)
            : base(xPos, yPos)
        {

            width = 16;
            height = 16;

            //acceleration.Y = FourChambers_Globals.GRAVITY;
            dead = true;
            drag.X = 50;
            drag.Y = 50;
            Texture2D Img = FlxG.Content.Load<Texture2D>("initials/pickups_16x16");

            loadGraphic(Img, false, false, 16, 16);

            //22x14

            if (FourChambers_Globals.BUILD_TYPE == FourChambers_Globals.BUILD_TYPE_PRESS)
            {
                if (FlxU.random() > 0.5)
                {
                    typeOfPowerUp = 154;
                }
                else if (FlxU.random() > 0.4)
                {
                    typeOfPowerUp = 155;
                }
                else if (FlxU.random() > 0.3)
                {
                    typeOfPowerUp = 156;
                }
                else if (FlxU.random() > 0.2)
                {
                    typeOfPowerUp = 157;
                }
                else if (FlxU.random() > 0.1)
                {
                    typeOfPowerUp = 158;
                }
                else
                {
                    typeOfPowerUp = (int)FlxU.random(0, 22 * 14);
                }
            }
            else if (FourChambers_Globals.BUILD_TYPE == FourChambers_Globals.BUILD_TYPE_RELEASE)
            {
                if (FlxU.random() > 0.1)
                {
                    typeOfPowerUp = 154;
                }
                else
                {
                    typeOfPowerUp = (int)FlxU.random(0, 22 * 14);
                }
            }
            
            addAnimation("item", new int[] { typeOfPowerUp });
            play("item");
            
        }

        override public void update()
        {


            //_curFrame = 1;

            base.update();

        }

        public void powerUpType(int setToType)
        {
            if (FourChambers_Globals.BUILD_TYPE == FourChambers_Globals.BUILD_TYPE_PRESS)
            {

            }
            else if (FourChambers_Globals.BUILD_TYPE == FourChambers_Globals.BUILD_TYPE_RELEASE)
            {

            }
        }

    }
}


/*

// Row:  0
        //public const int FR_ = 0;
        //public const int FR_ = 1;
        //public const int FR_ = 2;
        //public const int FR_ = 3;
        //public const int FR_ = 4;
        //public const int FR_ = 5;
        //public const int FR_ = 6;
        //public const int FR_ = 7;
        //public const int FR_ = 8;
        //public const int FR_ = 9;
        //public const int FR_ = 10;
        //public const int FR_ = 11;
        //public const int FR_ = 12;
        //public const int FR_ = 13;
        //public const int FR_ = 14;
        //public const int FR_ = 15;
        // Row:  1
        //public const int FR_ = 16;
        //public const int FR_ = 17;
        //public const int FR_ = 18;
        //public const int FR_ = 19;
        //public const int FR_ = 20;
        //public const int FR_ = 21;
        //public const int FR_ = 22;
        //public const int FR_ = 23;
        //public const int FR_ = 24;
        //public const int FR_ = 25;
        //public const int FR_ = 26;
        //public const int FR_ = 27;
        //public const int FR_ = 28;
        //public const int FR_ = 29;
        //public const int FR_ = 30;
        //public const int FR_ = 31;
        // Row:  2
        //public const int FR_ = 32;
        //public const int FR_ = 33;
        //public const int FR_ = 34;
        //public const int FR_ = 35;
        //public const int FR_ = 36;
        //public const int FR_ = 37;
        //public const int FR_ = 38;
        //public const int FR_ = 39;
        //public const int FR_ = 40;
        //public const int FR_ = 41;
        //public const int FR_ = 42;
        //public const int FR_ = 43;
        //public const int FR_ = 44;
        //public const int FR_ = 45;
        //public const int FR_ = 46;
        //public const int FR_ = 47;
        // Row:  3
        //public const int FR_ = 48;
        //public const int FR_ = 49;
        //public const int FR_ = 50;
        //public const int FR_ = 51;
        //public const int FR_ = 52;
        //public const int FR_ = 53;
        //public const int FR_ = 54;
        //public const int FR_ = 55;
        //public const int FR_ = 56;
        //public const int FR_ = 57;
        //public const int FR_ = 58;
        //public const int FR_ = 59;
        //public const int FR_ = 60;
        //public const int FR_ = 61;
        //public const int FR_ = 62;
        //public const int FR_ = 63;
        // Row:  4
        //public const int FR_ = 64;
        //public const int FR_ = 65;
        //public const int FR_ = 66;
        //public const int FR_ = 67;
        //public const int FR_ = 68;
        //public const int FR_ = 69;
        //public const int FR_ = 70;
        //public const int FR_ = 71;
        //public const int FR_ = 72;
        //public const int FR_ = 73;
        //public const int FR_ = 74;
        //public const int FR_ = 75;
        //public const int FR_ = 76;
        //public const int FR_ = 77;
        //public const int FR_ = 78;
        //public const int FR_ = 79;
        // Row:  5
        //public const int FR_ = 80;
        //public const int FR_ = 81;
        //public const int FR_ = 82;
        //public const int FR_ = 83;
        //public const int FR_ = 84;
        //public const int FR_ = 85;
        //public const int FR_ = 86;
        //public const int FR_ = 87;
        //public const int FR_ = 88;
        //public const int FR_ = 89;
        //public const int FR_ = 90;
        //public const int FR_ = 91;
        //public const int FR_ = 92;
        //public const int FR_ = 93;
        //public const int FR_ = 94;
        //public const int FR_ = 95;
        // Row:  6
        //public const int FR_ = 96;
        //public const int FR_ = 97;
        //public const int FR_ = 98;
        //public const int FR_ = 99;
        //public const int FR_ = 100;
        //public const int FR_ = 101;
        //public const int FR_ = 102;
        //public const int FR_ = 103;
        //public const int FR_ = 104;
        //public const int FR_ = 105;
        //public const int FR_ = 106;
        //public const int FR_ = 107;
        //public const int FR_ = 108;
        //public const int FR_ = 109;
        //public const int FR_ = 110;
        //public const int FR_ = 111;
        // Row:  7
        //public const int FR_ = 112;
        //public const int FR_ = 113;
        //public const int FR_ = 114;
        //public const int FR_ = 115;
        //public const int FR_ = 116;
        //public const int FR_ = 117;
        //public const int FR_ = 118;
        //public const int FR_ = 119;
        //public const int FR_ = 120;
        //public const int FR_ = 121;
        //public const int FR_ = 122;
        //public const int FR_ = 123;
        //public const int FR_ = 124;
        //public const int FR_ = 125;
        //public const int FR_ = 126;
        //public const int FR_ = 127;
        // Row:  8
        //public const int FR_ = 128;
        //public const int FR_ = 129;
        //public const int FR_ = 130;
        //public const int FR_ = 131;
        //public const int FR_ = 132;
        //public const int FR_ = 133;
        //public const int FR_ = 134;
        //public const int FR_ = 135;
        //public const int FR_ = 136;
        //public const int FR_ = 137;
        //public const int FR_ = 138;
        //public const int FR_ = 139;
        //public const int FR_ = 140;
        //public const int FR_ = 141;
        //public const int FR_ = 142;
        //public const int FR_ = 143;
        // Row:  9
        //public const int FR_ = 144;
        //public const int FR_ = 145;
        //public const int FR_ = 146;
        //public const int FR_ = 147;
        //public const int FR_ = 148;
        //public const int FR_ = 149;
        //public const int FR_ = 150;
        //public const int FR_ = 151;
        //public const int FR_ = 152;
        //public const int FR_ = 153;
        //public const int FR_ = 154;
        //public const int FR_ = 155;
        //public const int FR_ = 156;
        //public const int FR_ = 157;
        //public const int FR_ = 158;
        //public const int FR_ = 159;
        // Row:  10
        //public const int FR_ = 160;
        //public const int FR_ = 161;
        //public const int FR_ = 162;
        //public const int FR_ = 163;
        //public const int FR_ = 164;
        //public const int FR_ = 165;
        //public const int FR_ = 166;
        //public const int FR_ = 167;
        //public const int FR_ = 168;
        //public const int FR_ = 169;
        //public const int FR_ = 170;
        //public const int FR_ = 171;
        //public const int FR_ = 172;
        //public const int FR_ = 173;
        //public const int FR_ = 174;
        //public const int FR_ = 175;
        // Row:  11
        //public const int FR_ = 176;
        //public const int FR_ = 177;
        //public const int FR_ = 178;
        //public const int FR_ = 179;
        //public const int FR_ = 180;
        //public const int FR_ = 181;
        //public const int FR_ = 182;
        //public const int FR_ = 183;
        //public const int FR_ = 184;
        //public const int FR_ = 185;
        //public const int FR_ = 186;
        //public const int FR_ = 187;
        //public const int FR_ = 188;
        //public const int FR_ = 189;
        //public const int FR_ = 190;
        //public const int FR_ = 191;
        // Row:  12
        //public const int FR_ = 192;
        //public const int FR_ = 193;
        //public const int FR_ = 194;
        //public const int FR_ = 195;
        //public const int FR_ = 196;
        //public const int FR_ = 197;
        //public const int FR_ = 198;
        //public const int FR_ = 199;
        //public const int FR_ = 200;
        //public const int FR_ = 201;
        //public const int FR_ = 202;
        //public const int FR_ = 203;
        //public const int FR_ = 204;
        //public const int FR_ = 205;
        //public const int FR_ = 206;
        //public const int FR_ = 207;
        // Row:  13
        //public const int FR_ = 208;
        //public const int FR_ = 209;
        //public const int FR_ = 210;
        //public const int FR_ = 211;
        //public const int FR_ = 212;
        //public const int FR_ = 213;
        //public const int FR_ = 214;
        //public const int FR_ = 215;
        //public const int FR_ = 216;
        //public const int FR_ = 217;
        //public const int FR_ = 218;
        //public const int FR_ = 219;
        //public const int FR_ = 220;
        //public const int FR_ = 221;
        //public const int FR_ = 222;
        //public const int FR_ = 223;
        // Row:  14
        //public const int FR_ = 224;
        //public const int FR_ = 225;
        //public const int FR_ = 226;
        //public const int FR_ = 227;
        //public const int FR_ = 228;
        //public const int FR_ = 229;
        //public const int FR_ = 230;
        //public const int FR_ = 231;
        //public const int FR_ = 232;
        //public const int FR_ = 233;
        //public const int FR_ = 234;
        //public const int FR_ = 235;
        //public const int FR_ = 236;
        //public const int FR_ = 237;
        //public const int FR_ = 238;
        //public const int FR_ = 239;
        // Row:  15
        public const int FR_drumstick = 240;
        public const int FR_steak = 241;
        public const int FR_skewer = 242;
        public const int FR_fish = 243;
        //public const int FR_ = 244;
        public const int FR_toast = 245;
        //public const int FR_ = 246;
        //public const int FR_ = 247;
        //public const int FR_ = 248;
        //public const int FR_ = 249;
        //public const int FR_ = 250;
        //public const int FR_ = 251;
        //public const int FR_ = 252;
        //public const int FR_ = 253;
        //public const int FR_ = 254;
        //public const int FR_ = 255;
        // Row:  16
        //public const int FR_ = 256;
        //public const int FR_ = 257;
        //public const int FR_ = 258;
        //public const int FR_ = 259;
        //public const int FR_ = 260;
        //public const int FR_ = 261;
        //public const int FR_ = 262;
        //public const int FR_ = 263;
        //public const int FR_ = 264;
        //public const int FR_ = 265;
        //public const int FR_ = 266;
        //public const int FR_ = 267;
        //public const int FR_ = 268;
        //public const int FR_ = 269;
        //public const int FR_ = 270;
        //public const int FR_ = 271;
        // Row:  17
        public const int FR_sword = 272;
        public const int FR_broadsword = 273;
        public const int FR_axe = 274;
        public const int FR_mace = 275;
        public const int FR_staff = 276;
        public const int FR_macelarge = 277;
        public const int FR_sledgehammer = 278;
        public const int FR_shield = 279;
        public const int FR_shieldlarge = 280;
        public const int FR_bowandarrow = 281;
        public const int FR_arrows = 282;
        public const int FR_helmet = 283;
        public const int FR_helmetlarge = 284;
        public const int FR_wizardhat = 285;
        public const int FR_gloves = 286;
        public const int FR_cuffs = 287;
        // Row:  18
        public const int FR_boots = 288;
        public const int FR_pants = 289;
        public const int FR_belt = 290;
        public const int FR_tunic = 291;
        //public const int FR_ = 292;
        //public const int FR_ = 293;
        //public const int FR_ = 294;
        //public const int FR_ = 295;
        //public const int FR_ = 296;
        //public const int FR_ = 297;
        //public const int FR_ = 298;
        //public const int FR_ = 299;
        //public const int FR_ = 300;
        //public const int FR_ = 301;
        //public const int FR_ = 302;
        //public const int FR_ = 303;
        // Row:  19
        //public const int FR_ = 304;
        //public const int FR_ = 305;
        //public const int FR_ = 306;
        //public const int FR_ = 307;
        //public const int FR_ = 308;
        //public const int FR_ = 309;
        //public const int FR_ = 310;
        //public const int FR_ = 311;
        //public const int FR_ = 312;
        //public const int FR_ = 313;
        //public const int FR_ = 314;
        //public const int FR_ = 315;
        //public const int FR_ = 316;
        //public const int FR_ = 317;
        //public const int FR_ = 318;
        //public const int FR_ = 319;
        // Row:  20
        //public const int FR_ = 320;
        //public const int FR_ = 321;
        //public const int FR_ = 322;
        //public const int FR_ = 323;
        //public const int FR_ = 324;
        //public const int FR_ = 325;
        //public const int FR_ = 326;
        //public const int FR_ = 327;
        //public const int FR_ = 328;
        //public const int FR_ = 329;
        //public const int FR_ = 330;
        //public const int FR_ = 331;
        //public const int FR_ = 332;
        //public const int FR_ = 333;
        //public const int FR_ = 334;
        //public const int FR_ = 335;
        // Row:  21
        //public const int FR_ = 336;
        //public const int FR_ = 337;
        //public const int FR_ = 338;
        //public const int FR_ = 339;
        //public const int FR_ = 340;
        //public const int FR_ = 341;
        //public const int FR_ = 342;
        //public const int FR_ = 343;
        //public const int FR_ = 344;
        //public const int FR_ = 345;
        //public const int FR_ = 346;
        //public const int FR_ = 347;
        //public const int FR_ = 348;
        //public const int FR_ = 349;
        //public const int FR_ = 350;
        //public const int FR_ = 351;
        // Row:  22
        //public const int FR_ = 352;
        //public const int FR_ = 353;
        //public const int FR_ = 354;
        //public const int FR_ = 355;
        //public const int FR_ = 356;
        //public const int FR_ = 357;
        //public const int FR_ = 358;
        //public const int FR_ = 359;
        //public const int FR_ = 360;
        //public const int FR_ = 361;
        //public const int FR_ = 362;
        //public const int FR_ = 363;
        //public const int FR_ = 364;
        //public const int FR_ = 365;
        //public const int FR_ = 366;
        //public const int FR_ = 367;
        // Row:  23
        //public const int FR_ = 368;
        //public const int FR_ = 369;
        //public const int FR_ = 370;
        //public const int FR_ = 371;
        //public const int FR_ = 372;
        //public const int FR_ = 373;
        //public const int FR_ = 374;
        //public const int FR_ = 375;
        //public const int FR_ = 376;
        //public const int FR_ = 377;
        //public const int FR_ = 378;
        //public const int FR_ = 379;
        //public const int FR_ = 380;
        //public const int FR_ = 381;
        //public const int FR_ = 382;
        //public const int FR_ = 383;
        // Row:  24
        //public const int FR_ = 384;
        //public const int FR_ = 385;
        //public const int FR_ = 386;
        //public const int FR_ = 387;
        //public const int FR_ = 388;
        //public const int FR_ = 389;
        //public const int FR_ = 390;
        //public const int FR_ = 391;
        //public const int FR_ = 392;
        //public const int FR_ = 393;
        //public const int FR_ = 394;
        //public const int FR_ = 395;
        //public const int FR_ = 396;
        //public const int FR_ = 397;
        //public const int FR_ = 398;
        //public const int FR_ = 399;
        // Row:  25
        //public const int FR_ = 400;
        //public const int FR_ = 401;
        //public const int FR_ = 402;
        //public const int FR_ = 403;
        //public const int FR_ = 404;
        //public const int FR_ = 405;
        //public const int FR_ = 406;
        //public const int FR_ = 407;
        //public const int FR_ = 408;
        //public const int FR_ = 409;
        //public const int FR_ = 410;
        //public const int FR_ = 411;
        //public const int FR_ = 412;
        //public const int FR_ = 413;
        //public const int FR_ = 414;
        //public const int FR_ = 415;

*/
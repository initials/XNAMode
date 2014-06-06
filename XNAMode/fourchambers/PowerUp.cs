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
    class PowerUp : FlxSprite
    {
        public int typeOfPowerUp = 0;
        public bool scalesDown = false;

        //--Line: 0
        public const int FR_PotionSquareBlue = 0;
        public const int FR_PotionSquarePurple = 1;
        public const int FR_PotionSquareRed = 2;
        public const int FR_PotionSquareGreen = 3;
        public const int FR_PotionSquareYellow = 4;
        public const int FR_PotionSquareWhite = 5;
        public const int FR_PotionRoundBlue = 6;
        public const int FR_PotionRoundPurple = 7;
        public const int FR_PotionRoundRed = 8;
        public const int FR_PotionRoundGreen = 9;
        public const int FR_PotionRoundYellow = 10;
        public const int FR_PotionRoundWhite = 11;
        public const int FR_PotionHandlesBlue = 12;
        public const int FR_PotionHandlesPurple = 13;
        public const int FR_PotionHandlesRed = 14;
        public const int FR_PotionHandlesGreen = 15;
        public const int FR_PotionHandlesYellow = 16;
        public const int FR_PotionHandlesWhite = 17;
        public const int FR_PotClay = 18;
        public const int FR_PotSilver = 19;
        public const int FR_PotGold = 20;
        public const int FR_PotPurple = 21;
        //--Line: 1
        public const int FR_NecklaceHeart = 22;
        public const int FR_NecklaceSilverRectangle = 23;
        public const int FR_NecklaceTurquoise = 24;
        public const int FR_NecklaceRedTriangle = 25;
        public const int FR_NecklaceRoundOpal = 26;
        public const int FR_NecklaceSkull = 27;
        public const int FR_RockSilver = 28;
        public const int FR_RockGreen = 29;
        public const int FR_RockGold = 30;
        public const int FR_RockIce = 31;
        public const int FR_RockRed = 32;
        public const int FR_RockWhite = 33;
        public const int FR_RockPurple = 34;
        public const int FR_PotionSquareGrey = 35;
        public const int FR_PotionRoundGrey = 36;
        public const int FR_PotionHandlesBlack = 37;
        public const int FR_TeardropRed = 38;
        public const int FR_TeardropPurple = 39;
        public const int FR_TeardropGreen = 40;
        public const int FR_TeardropYellow = 41;
        public const int FR_TeardropBlue = 42;
        public const int FR_TeardropSilver = 43;
        //--Line: 2
        public const int FR_PaperWhite = 44;
        public const int FR_PaperLightBrown = 45;
        public const int FR_PaperBrown = 46;
        public const int FR_PaperGold = 47;
        public const int FR_PaperPink = 48;
        public const int FR_Wand = 49;
        public const int FR_WandGreen = 50;
        public const int FR_WandRed = 51;
        public const int FR_WandSilver = 52;
        public const int FR_WandBlue = 53;
        public const int FR_DiamondGreen = 54;
        public const int FR_DiamondBlue = 55;
        public const int FR_DiamondRed = 56;
        public const int FR_DiamondYellow = 57;
        public const int FR_CoinGreen = 58;
        public const int FR_CoinOutlinedGreen = 59;
        public const int FR_CoinPurple = 60;
        public const int FR_CoinOutlinedPurple = 61;
        public const int FR_CoinRed = 62;
        public const int FR_CoinOutlinedRed = 63;
        public const int FR_CoinBlue = 64;
        public const int FR_CoinOutlinedBlue = 65;
        //--Line: 3
        public const int FR_Book1 = 66;
        public const int FR_Book2 = 67;
        public const int FR_Book3 = 68;
        public const int FR_Book4 = 69;
        public const int FR_Book5 = 70;
        public const int FR_MoneyBag = 71;
        public const int FR_CoinsBronze = 72;
        public const int FR_CoinsSilver = 73;
        public const int FR_CoinsGold = 74;
        public const int FR_RingSilver = 75;
        public const int FR_RingGold = 76;
        public const int FR_RingRuby = 77;
        public const int FR_RingDiamond = 78;
        public const int FR_RingSilverBig = 79;
        public const int FR_RingPurple = 80;
        public const int FR_RingBlue = 81;
        public const int FR_RingGreen = 82;
        public const int FR_RingSkull = 83;
        public const int FR_HeartFull = 84;
        public const int FR_HeartHalf = 85;
        public const int FR_HeartEmpty = 86;
        public const int FR_HeartBroken = 87;
        //--Line: 4
        public const int FR_OrbBlue = 88;
        public const int FR_OrbGreen = 89;
        public const int FR_OrbRed = 90;
        public const int FR_OrbPurple = 91;
        public const int FR_OrbYellow = 92;
        public const int FR_MushroomRed = 93;
        public const int FR_MushroomBlue = 94;
        public const int FR_FeatherSilver = 95;
        public const int FR_FeatherRed = 96;
        public const int FR_BonesPile = 97;
        public const int FR_Eyeball = 98;
        public const int FR_StarSilver = 99;
        public const int FR_StarGold = 100;
        public const int FR_Explosion = 101;
        public const int FR_SmallOrbGreen = 102;
        public const int FR_SmallOrbOutlinedGreen = 103;
        public const int FR_GoldMedallionOne = 104;
        public const int FR_GoldMedallionTwo = 105;
        public const int FR_GoldMedallionThree = 106;
        public const int FR_SilverMedallionOne = 107;
        public const int FR_SilverMedallionTwo = 108;
        public const int FR_SilverMedallionThree = 109;
        //--Line: 5
        public const int FR_DishJade = 110;
        public const int FR_DishSmallJade = 111;
        public const int FR_PuzzlePiece1 = 112;
        public const int FR_PuzzlePiece2 = 113;
        public const int FR_PuzzlePiece3 = 114;
        public const int FR_PuzzlePiece4 = 115;
        public const int FR_PuzzlePiece5 = 116;
        public const int FR_PuzzlePiece6 = 117;
        public const int FR_PuzzlePiece7 = 118;
        public const int FR_PuzzlePiece8 = 119;
        public const int FR_PuzzlePiece9 = 120;
        public const int FR_PuzzlePiece10 = 121;
        public const int FR_PuzzlePiece11 = 122;
        public const int FR_PuzzlePiece12 = 123;
        public const int FR_PuzzlePiece13 = 124;
        public const int FR_PuzzlePiece14 = 125;
        public const int FR_DishRed = 126;
        public const int FR_DishYellow = 127;
        public const int FR_DishGreen = 128;
        public const int FR_DishBlue = 129;
        public const int FR_DishDarkBlue = 130;
        public const int FR_DishPurple = 131;
        //--Line: 6
        public const int FR_Skull = 132;
        public const int FR_SkullRedEyes = 133;
        public const int FR_SkullGold = 134;
        public const int FR_Scroll = 135;
        public const int FR_ScrollUnrolled = 136;
        public const int FR_KeyGold = 137;
        public const int FR_KeySilver = 138;
        public const int FR_KeyBone = 139;
        public const int FR_KeyBlueCrystal = 140;
        public const int FR_KeySquareBlue = 141;
        public const int FR_KeySquareRed = 142;
        public const int FR_Clock = 143;
        public const int FR_CandleOn = 144;
        public const int FR_CandleOff = 145;
        public const int FR_LanternOff = 146;
        public const int FR_LanternOn = 147;
        public const int FR_CrownKing = 148;
        public const int FR_CrownPrince = 149;
        public const int FR_LanternBroken = 150;
        public const int FR_Stick = 151;
        public const int FR_StickOnFire = 152;
        public const int FR_StickBlue = 153;
        //--Line: 7
        public const int FR_Arrows = 154;
        public const int FR_ArrowsRed = 155;
        public const int FR_ArrowsYellow = 156;
        public const int FR_ArrowsGreen = 157;
        public const int FR_MaceWoodHandle = 158;
        public const int FR_MaceMetalHandle = 159;
        public const int FR_Flail = 160;
        public const int FR_FlailGold = 161;
        public const int FR_GlailBlue = 162;
        public const int FR_Hammer = 163;
        public const int FR_SledgeHammer = 164;
        public const int FR_SledgeHammerGold = 165;
        public const int FR_Axe = 166;
        public const int FR_AxeMedium = 167;
        public const int FR_AxeDual = 168;
        public const int FR_AxeDualMedium = 169;
        public const int FR_AxeDualGold = 170;
        public const int FR_AxeDualBlue = 171;
        public const int FR_AxeDualLarge = 172;
        public const int FR_AxeDualRed = 173;
        public const int FR_AxeDualSharp = 174;
        public const int FR_AxeBlue = 175;
        //--Line: 8
        public const int FR_WalkingStick = 176;
        public const int FR_WalkingCane = 177;
        public const int FR_WalkingStickEnchanted = 178;
        public const int FR_Staff = 179;
        public const int FR_StaffRed = 180;
        public const int FR_StaffWithHandle = 181;
        public const int FR_StaffCrossHandle = 182;
        public const int FR_StaffGoldCrossHandle = 183;
        public const int FR_StaffPurpleHandle = 184;
        public const int FR_StaffRedHandle = 185;
        public const int FR_StaffBlueHandle = 186;
        public const int FR_StaffBlue = 187;
        public const int FR_StaffGreen = 188;
        public const int FR_Mace = 189;
        public const int FR_Bow = 190;
        public const int FR_BowMetalHandle = 191;
        public const int FR_BowMetal = 192;
        public const int FR_BowBlue = 193;
        public const int FR_BowGold = 194;
        public const int FR_CrossBow = 195;
        public const int FR_CrossBowSilver = 196;
        public const int FR_CrossBowGold = 197;
        //--Line: 9
        public const int FR_Knife = 198;
        public const int FR_Sword = 199;
        public const int FR_SwordGold = 200;
        public const int FR_KnifeButcher = 201;
        public const int FR_SwordSilver = 202;
        public const int FR_SwordBlue = 203;
        public const int FR_SwordFlame = 204;
        public const int FR_SwordThick = 205;
        public const int FR_SwordCurved = 206;
        public const int FR_SwordCurvedGreen = 207;
        public const int FR_SwordLong = 208;
        public const int FR_SwordLongThick = 209;
        public const int FR_SwordLongThickGoldHandle = 210;
        public const int FR_SwordLongThickGold = 211;
        public const int FR_SwordLongThickBlue = 212;
        public const int FR_SwordLongThickFire = 213;
        public const int FR_SwordLongThickSerated = 214;
        public const int FR_SwordLongThickRed = 215;
        public const int FR_SwordLongThickSeratedDual = 216;
        public const int FR_SwordLongThickEye = 217;
        public const int FR_Machete = 218;
        public const int FR_SwordLongThickBlack = 219;
        //--Line: 10
        public const int FR_ShieldWood = 220;
        public const int FR_ShieldWoodRound = 221;
        public const int FR_ShieldMetal = 222;
        public const int FR_ShieldMetalStudded = 223;
        public const int FR_ShieldWoodDiamond = 224;
        public const int FR_ShieldMetalDiamond = 225;
        public const int FR_ShieldGoldDiamond = 226;
        public const int FR_ShieldRedCheck = 227;
        public const int FR_ShieldBlueWhite = 228;
        public const int FR_ShieldRed = 229;
        public const int FR_ShieldGreenWhite = 230;
        public const int FR_ShieldRedYellowCheck = 231;
        public const int FR_ShieldSkull = 232;
        public const int FR_ShieldCracked = 233;
        public const int FR_Band = 234;
        public const int FR_BandBuckle = 235;
        public const int FR_BandStudded = 236;
        public const int FR_BandSilver = 237;
        public const int FR_BandGold = 238;
        public const int FR_BandRed = 239;
        public const int FR_BandBlue = 240;
        public const int FR_BandBlack = 241;
        //--Line: 11
        public const int FR_Cape1 = 242;
        public const int FR_Cape2 = 243;
        public const int FR_Cape3 = 244;
        public const int FR_Cape4 = 245;
        public const int FR_Cape5 = 246;
        public const int FR_Cape6 = 247;
        public const int FR_Cape7 = 248;
        public const int FR_Hood1 = 249;
        public const int FR_Hood2 = 250;
        public const int FR_Hood3 = 251;
        public const int FR_Hood4 = 252;
        public const int FR_Hood5 = 253;
        public const int FR_Hood6 = 254;
        public const int FR_Hood7 = 255;
        public const int FR_Hood8 = 256;
        public const int FR_Glove1 = 257;
        public const int FR_Glove2 = 258;
        public const int FR_Glove3 = 259;
        public const int FR_Glove4 = 260;
        public const int FR_Glove5 = 261;
        public const int FR_Glove6 = 262;
        public const int FR_Glove7 = 263;
        //--Line: 12
        public const int FR_Armour1 = 264;
        public const int FR_Armour2 = 265;
        public const int FR_Armour3 = 266;
        public const int FR_Armour4 = 267;
        public const int FR_Armour5 = 268;
        public const int FR_Armour6 = 269;
        public const int FR_Armour7 = 270;
        public const int FR_Armour8 = 271;
        public const int FR_Armour9 = 272;
        public const int FR_Armour10 = 273;
        public const int FR_Helmet1 = 274;
        public const int FR_Helmet2 = 275;
        public const int FR_Helmet3 = 276;
        public const int FR_Helmet4 = 277;
        public const int FR_Helmet5 = 278;
        public const int FR_Helmet6 = 279;
        public const int FR_Helmet7 = 280;
        public const int FR_Helmet8 = 281;
        public const int FR_WizardsHat1 = 282;
        public const int FR_WizardsHat2 = 283;
        public const int FR_WizardsHat3 = 284;
        public const int FR_WizardsHat4 = 285;
        //--Line: 13
        public const int FR_Boot1 = 286;
        public const int FR_Boot2 = 287;
        public const int FR_Boot3 = 288;
        public const int FR_Boot4 = 289;
        public const int FR_Boot5 = 290;
        public const int FR_Boot6 = 291;
        public const int FR_Boot7 = 292;
        public const int FR_Boot8 = 293;
        public const int FR_Boot9 = 294;
        public const int FR_Boot10 = 295;
        public const int FR_Boot11 = 296;
        public const int FR_Pants1 = 297;
        public const int FR_Pants2 = 298;
        public const int FR_Pants3 = 299;
        public const int FR_Pants4 = 300;
        public const int FR_Pants5 = 301;
        public const int FR_Pants6 = 302;
        public const int FR_Pants7 = 303;
        public const int FR_Pants8 = 304;
        public const int FR_Pants9 = 305;
        public const int FR_Pants10 = 306;
        public const int FR_Pants11 = 307;
        
         

        public PowerUp(int xPos, int yPos)
            : base(xPos, yPos)
        {

            width = 16;
            height = 16;

            //acceleration.Y = FourChambers_Globals.GRAVITY;
            dead = true;
            drag.X = 50;
            drag.Y = 50;
            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/pickups_16x16");

            loadGraphic(Img, false, false, 16, 16);

            //22x14

            if (FourChambers_Globals.BUILD_TYPE == FourChambers_Globals.BUILD_TYPE_PRESS)
            {
                if (FlxU.random() > 0.5)
                {
                    typeOfPowerUp = FR_Arrows;
                }
                else if (FlxU.random() > 0.48)
                {
                    typeOfPowerUp = FR_ArrowsGreen;
                }
                else if (FlxU.random() > 0.47)
                {
                    typeOfPowerUp = FR_ArrowsRed;
                }
                else if (FlxU.random() > 0.46)
                {
                    typeOfPowerUp = FR_ArrowsYellow;
                }
                else if (FlxU.random() > 0.45)
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
                    typeOfPowerUp = FR_Arrows;
                }
                else
                {
                    typeOfPowerUp = (int)FlxU.random(0, 22 * 14);
                }
            }
            
            addAnimation("item", new int[] { typeOfPowerUp });
            play("item");
            
        }

        public void TypeOfPowerUp(int Typ)
        {
            typeOfPowerUp = Typ;

            addAnimation("item"+Typ.ToString(), new int[] { Typ });
            play("item" + Typ.ToString());
        }

        override public void update()
        {
            if (scalesDown && scale > 0.001)
            {
                scale -= 0.005f;

            }
            //if (FlxG.keys.F2)
            //{
            //    Console.WriteLine("type {0} ", typeOfPowerUp);
            //    TypeOfPowerUp(typeOfPowerUp + 1);
            //}

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
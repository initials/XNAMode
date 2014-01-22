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
    class EnemyActor : FlxSprite
    {

        // A bunch of helpers for the allActors sprite sheet.

        #region Constants for frame numbers
        // Row:  0
        public const int FR_soldier = 0;
        public const int FR_paladin = 1;
        public const int FR_executor = 2;
        public const int FR_savage = 3;
        public const int FR_marksman = 4;
        public const int FR_monk = 5;
        public const int FR_assassin = 6;
        public const int FR_corsair = 7;
        // Row:  1
        public const int FR_wizard = 8;
        public const int FR_priest = 9;
        public const int FR_warlock = 10;
        public const int FR_druid = 11;
        public const int FR_merchant = 12;
        public const int FR_mechanic = 13;
        public const int FR_artist = 14;
        public const int FR_gormet = 15;
        // Row:  2
        public const int FR_chicken = 16;
        public const int FR_rat = 17;
        public const int FR_toad = 18;
        public const int FR_feline = 19;
        public const int FR_sheep = 20;
        public const int FR_cow = 21;
        public const int FR_horse = 22;
        public const int FR_deer = 23;
        // Row:  3
        public const int FR_snake = 24;
        public const int FR_bat = 25;
        public const int FR_zinger = 26;
        public const int FR_spider = 27;
        public const int FR_wolf = 28;
        public const int FR_grizzly = 29;
        public const int FR_lion = 30;
        public const int FR_unicorn = 31;
        // Row:  4
        public const int FR_troll = 32;
        public const int FR_cyclops = 33;
        public const int FR_dwarf = 34;
        public const int FR_goblin = 35;
        public const int FR_ogre = 36;
        public const int FR_nymph = 37;
        public const int FR_mermaid = 38;
        public const int FR_centaur = 39;
        // Row:  5
        public const int FR_tauro = 40;
        public const int FR_treant = 41;
        public const int FR_fungant = 42;
        public const int FR_zombie = 43;
        public const int FR_mummy = 44;
        public const int FR_deathclaw = 45;
        public const int FR_bloatedzombie = 46;
        public const int FR_vampire = 47;
        // Row:  6
        public const int FR_bogbeast = 48;
        public const int FR_blight = 49;
        public const int FR_willowisp = 50;
        public const int FR_phantom = 51;
        public const int FR_gloom = 52;
        public const int FR_nightmare = 53;
        public const int FR_skeleton = 54;
        public const int FR_grimwarrior = 55;
        // Row:  7
        public const int FR_harvester = 56;
        public const int FR_lich = 57;
        public const int FR_imp = 58;
        public const int FR_devil = 59;
        public const int FR_mephisto = 60;
        public const int FR_kerberos = 61;
        public const int FR_glutton = 62;
        public const int FR_tormentor = 63;
        // Row:  8
        public const int FR_succubus = 64;
        public const int FR_gorgon = 65;
        public const int FR_ifrit = 66;
        public const int FR_embersteed = 67;
        public const int FR_seraphine = 68;
        public const int FR_bombling = 69;
        public const int FR_drone = 70;
        public const int FR_mimick = 71;
        // Row:  9
        public const int FR_automaton = 72;
        public const int FR_golem = 73;
        public const int FR_gelantine = 74;
        public const int FR_floatingeye = 75;
        public const int FR_prism = 76;
        public const int FR_djinn = 77;
        public const int FR_sphinx = 78;
        public const int FR_chimera = 79;
        #endregion


        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        /// <summary>
        /// The score to recieve when killing this actor
        /// </summary>
        public int score = 50;

        public float hurtTimer = 550.0f;

        public bool readyForHarvester = false;

        public EnemyActor(int xPos, int yPos)
            : base(xPos, yPos)
        {

            acceleration.Y = FourChambers_Globals.GRAVITY;



        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {
            hurtTimer += FlxG.elapsed;

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else if (velocity.X < 0)
            {
                facing = Flx2DFacing.Left;

            }

            //ANIMATION
            //if (_curAnim.name == "hurt")
            //{

            //}
            if (hurtTimer < 1.0f)
            {
                play("hurt");
            }
            else if (dead)
            {
                play("death");
            }
            else if (velocity.Y != 0)
            {
                play("jump");
            }
            else if (velocity.X == 0)
            {
                play("idle");
            }
            else if (FlxU.abs(velocity.X) > 70)
            {
                play("run");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                play("walk");
            }
            else
            {
                play("idle");
            }

            base.update();

        }

        public override void hurt(float Damage)
        {
            color = Color.PaleVioletRed;

            base.hurt(Damage);
        }

        public override void kill()
        {
            color = Color.White;

            FlxG.score += score;

            play("death");
            velocity.X = 0;
            velocity.Y = 0;
            dead = true;

            //base.kill();

            FlxG.write("Enemy "  + actorName + " is dead");

            //flicker(0.25f);

            acceleration.X = 0;
        }
    }
}

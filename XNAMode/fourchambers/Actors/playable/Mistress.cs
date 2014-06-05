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
    public class Mistress : EnemyActor
    {
        public MeleeHitBox whipHitBox;

        public Mistress(int xPos, int yPos)
            : base(xPos, yPos)
        {
            // Set up the stats for this actor.
            actorName = "Linda Lee";
            score = 250;
            health = 12;
            runSpeed = 120;
            _jumpPower = -180.0f;
            _jumpInitialPower = -140.0f;
            _jumpMaxTime = 0.256225f;
            _jumpInitialTime = 0.035f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            //drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/mistress_Attack.txt";
            timeDownAfterHurt = 0.525f;
            actorType = "mistress";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Mistress_ss_35x22"), true, false, 35, 22);

            addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
            addAnimation("walk", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 8);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2, 3, 4, 5, 6, 6,6,0 }, 30, true);
            addAnimation("hurt", new int[] { 17 }, 12);
            addAnimation("death", new int[] { 17 }, 12);

            play("idle");

            //bounding box tweaks
            width = 9;
            height = 16;
            offset.X = 13;
            offset.Y = 6;


            whipHitBox = new MeleeHitBox(xPos, yPos);
            whipHitBox.width = 5;
            whipHitBox.height = 5;
            whipHitBox.belongsTo = "mistress";

            //custom stuff
            attackingJoystick = false;

            hurtTimer = 10000;

        }

        override public void update()
        {
            //hurtTimer += FlxG.elapsed;

            if ((attackingJoystick || attackingMouse || attackingMelee) && !dead)
            {
                whipHitBox.width = 5;
                whipHitBox.height = 5;
                // position the hit box of the whip.

                if (facing == Flx2DFacing.Right)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            whipHitBox.dead = false;
                            whipHitBox.x = x + 14;
                            whipHitBox.y = y;
                            break;
                        case 5:
                            whipHitBox.dead = false;
                            whipHitBox.x = x + 16;
                            whipHitBox.y = y + 2;
                            break;
                        case 6:
                            whipHitBox.dead = false;
                            whipHitBox.width = 7;
                            whipHitBox.height = 7;
                            whipHitBox.x = x + 18;
                            whipHitBox.y = y + 3;
                            break;
                        case 7:
                            attackingMelee = false;
                            break;
                        default:
                            whipHitBox.dead = true;
                            whipHitBox.width = 10;
                            whipHitBox.height = 10;
                            whipHitBox.x = x;
                            whipHitBox.y = y;
                            break;
                    }
                }
                if (facing == Flx2DFacing.Left)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            whipHitBox.dead = false;
                            whipHitBox.x = x - 10;
                            whipHitBox.y = y;
                            break;
                        case 5:
                            whipHitBox.dead = false;
                            whipHitBox.x = x - 12;
                            whipHitBox.y = y + 2;
                            break;
                        case 6:
                            whipHitBox.dead = false;
                            whipHitBox.width = 7;
                            whipHitBox.height = 7;
                            whipHitBox.x = x - 14;
                            whipHitBox.y = y + 3;
                            break;
                        case 7:
                            attackingMelee = false;
                            break;
                        default:
                            whipHitBox.dead = true;
                            whipHitBox.width = 10;
                            whipHitBox.height = 10;
                            whipHitBox.x = x;
                            whipHitBox.y = y;
                            break;
                    }
                }
            }
            else
            {
                whipHitBox.dead = true;
                whipHitBox.x = x;
                whipHitBox.y = y;
                whipHitBox.width = 0;
                whipHitBox.height = 0;
            }

            

            base.update();



        }



    }
}


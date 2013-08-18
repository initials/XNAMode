﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class CaveState : FlxState
    {
        protected Texture2D ImgDirt;

        private const float FOLLOW_LERP = 3.0f;

        private Texture2D Bubbles;
        private Texture2D DecorTex;
        private FlxEmitter _bubbles;
        private FlxTileblock rotatore;
        private FlxTilemap tiles;
        private FlxTilemap decorations;


        protected FlxGroup _bullets;
        

        private Automaton automaton;
        private Corsair corsair;
        private Executor executor;
        private Gloom gloom;
        private Harvester harvester;
        private Marksman marksman;
        private Medusa medusa;
        private Mistress mistress;
        private Mummy mummy;
        private Nymph nymph;
        private Paladin paladin;
        private Seraphine seraphine;
        private Succubus succubus;
        private Tormentor tormentor;
        private Unicorn unicorn;
        private Warlock warlock;
        private Vampire vampire;
        private Zombie zombie;

        private FlxText p1CurrentActor;



        private FlxGroup actors;

        override public void create()
        {

            
            FlxG.backColor = new Color(0xFF, 0xC6, 0x5E);

            //FlxG.backColor = Color.Black;

            base.create();





            string levelData = FlxU.randomString(10);
            FlxG.log("levelData: " + levelData);

            //bg
            FlxTileblock ti = new FlxTileblock(0, 0, FlxG.width + 48, FlxG.height / 2);
            ti.loadTiles(FlxG.Content.Load<Texture2D>("initials/envir_dusk"), 48, 64, 0);
            ti.scrollFactor.X = 0.02f;
            ti.scrollFactor.Y = 0.02f;
            ti.boundingBoxOverride = false;
            //ti.scale = 2;
            add(ti);


            FlxCaveGenerator cav = new FlxCaveGenerator(50, 40);
            cav.initWallRatio = 0.48f;
            cav.numSmoothingIterations = 5;
            cav.genInitMatrix(50, 40);
            int[,] matr = cav.generateCaveLevel(3, 0, 2, 0, 1, 0, 1, 0);
            string newMap = cav.convertMultiArrayToString(matr);

            tiles = new FlxTilemap();
            tiles.auto = FlxTilemap.AUTO;
            tiles.loadMap(newMap, FlxG.Content.Load<Texture2D>("initials/autotiles_16x16"));
            add(tiles);


            int[,] decr = cav.createDecorationsMap(matr);
            string newDec = cav.convertMultiArrayToString(decr);
            DecorTex = FlxG.Content.Load<Texture2D>("initials/decorations_16x16");

            decorations = new FlxTilemap();
            decorations.auto = FlxTilemap.RANDOM;
            decorations.randomLimit = (int)DecorTex.Width / 16;

            decorations.loadMap(newDec, DecorTex, 16, 16);
            add(decorations);


            actors = new FlxGroup();
            _bullets = new FlxGroup();

            int i = 0;

            for (i = 0; i < 2000; i++)
                _bullets.add(new Fireball());

            add(_bullets);




            int[] p = cav.findRandomSolid(decr);
            vampire = new Vampire(p[1] * 16, p[0] * 16);
            actors.add(vampire);

            p = cav.findRandomSolid(decr);
            warlock = new Warlock(p[1] * 16, p[0] * 16, _bullets.members);
            actors.add(warlock);

            p = cav.findRandomSolid(decr);
            zombie = new Zombie(p[1] * 16, p[0] * 16);
            actors.add(zombie);


            p = cav.findRandomSolid(decr);
            automaton = new Automaton(p[1] * 16, p[0] * 16);
            actors.add(automaton);

            p = cav.findRandomSolid(decr);
            corsair = new Corsair(p[1] * 16, p[0] * 16);
            actors.add(corsair);

            p = cav.findRandomSolid(decr);
            executor = new Executor(p[1] * 16, p[0] * 16);
            actors.add(executor);

            p = cav.findRandomSolid(decr);
            gloom = new Gloom(p[1] * 16, p[0] * 16);
            actors.add(gloom);

            p = cav.findRandomSolid(decr);
            harvester = new Harvester(p[1] * 16, p[0] * 16);
            actors.add(harvester);

            p = cav.findRandomSolid(decr);
            marksman = new Marksman(p[1] * 16, p[0] * 16);
            actors.add(marksman);

            p = cav.findRandomSolid(decr);
            mistress = new Mistress(p[1] * 16, p[0] * 16);
            actors.add(mistress);

            p = cav.findRandomSolid(decr);
            medusa = new Medusa(p[1] * 16, p[0] * 16);
            actors.add(medusa);


            p = cav.findRandomSolid(decr);
            mummy = new Mummy(p[1] * 16, p[0] * 16);
            actors.add(mummy);

            p = cav.findRandomSolid(decr);
            nymph = new Nymph(p[1] * 16, p[0] * 16);
            actors.add(nymph);

            p = cav.findRandomSolid(decr);
            paladin = new Paladin(p[1] * 16, p[0] * 16);
            actors.add(paladin);

            p = cav.findRandomSolid(decr);
            seraphine = new Seraphine(p[1] * 16, p[0] * 16);
            actors.add(seraphine);

            p = cav.findRandomSolid(decr);
            succubus = new Succubus(p[1] * 16, p[0] * 16);
            actors.add(succubus);

            p = cav.findRandomSolid(decr);
            tormentor = new Tormentor(p[1] * 16, p[0] * 16);
            actors.add(tormentor);

            p = cav.findRandomSolid(decr);
            unicorn = new Unicorn(p[1] * 16, p[0] * 16);
            actors.add(unicorn);

            add(actors);

            FlxG.follow(vampire, FOLLOW_LERP);
            //FlxG.followAdjust(0.5f, 0.0f);
            FlxG.followBounds(0, 0, 50*16, 40*16);

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            p1CurrentActor = new FlxText(2, 2, FlxG.width);
            p1CurrentActor.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            p1CurrentActor.text = "";
            p1CurrentActor.scrollFactor = Vector2.Zero;
            add(p1CurrentActor);


        }

        override public void update()
        {

            FlxU.collide(actors, tiles);

            FlxU.overlap(actors, _bullets, overlapped);
            FlxU.collide(tiles, _bullets);



            /*
            //Console.WriteLine(FlxG.gamepads.isNewThumbstickDown(FlxG.controllingPlayer));

            float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

            float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

            float rotation = (float)Math.Atan2(rightX, rightY);
            rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);

            //Console.WriteLine(rotation);
            */



            /*
            if (!flickering() && (_bulletInterval > 8) &&
                    (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickDown, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, FlxG.controllingPlayer, out pi) ||
                FlxG.gamepads.isButtonDown(Buttons.RightThumbstickUp, FlxG.controllingPlayer, out pi)))
            {

                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;

                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (rightY < -0.75)
                {
                    velocity.Y -= 36;
                }

                float rotation = (float)Math.Atan2(rightX, rightY);
                rotation = (rotation < 0) ? MathHelper.ToDegrees(rotation + MathHelper.TwoPi) : MathHelper.ToDegrees(rotation);


                _bulletInterval = 0;
                rightX *= 100;
                rightY *= -100;



                int bXVel = 0;
                int bYVel = 0;
                int bX = (int)x;
                int bY = (int)y;

                bY -= (int)_bullets[_curBullet].height - 4;
                bYVel = (int)rightY;
                bX -= (int)_bullets[_curBullet].width - 4;
                bXVel = (int)rightX;



                ((Bullet)(_bullets[_curBullet])).shoot(bX, bY, bXVel, bYVel);
                ((Bullet)(_bullets[_curBullet])).angle = rotation;

                //System.Diagnostics.Trace.WriteLine(rightX + " .. ry " + rightY + " " + rotation + " " + MathHelper.ToDegrees(rotation));

                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;
            }
             * 
             */

            if (FlxG.keys.A)
            {
                FlxG.transition.startFadeIn(0.025f);

                FlxG.state = new CaveState();
            }


            if (FlxG.keys.justPressed(Keys.S) || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            {
                int i = 0;
                int l = actors.members.Count;
                int active = 0;

                while (i < l)
                {
                    if ((actors.members[i] as Actor).isPlayerControlled == true) active = i;

                    (actors.members[i] as Actor).isPlayerControlled = false;
                    i++;
                }

                //int x = (int)(FlxU.random() * actors.members.Count);

                int x = active + 1;

                if (x >= actors.members.Count) x = 0;


                (actors.members[x] as Actor).isPlayerControlled = true;
                FlxG.follow(actors.members[x], FOLLOW_LERP);
                p1CurrentActor.text = actors.members[x].ToString();

                //(actors.members[x] as Actor).ToString()


            }



            /*
             * 			
            highlightBox.x = Math.floor(FlxG.mouse.x / TILE_WIDTH) * TILE_WIDTH;
			highlightBox.y = Math.floor(FlxG.mouse.y / TILE_HEIGHT) * TILE_HEIGHT;

			if (FlxG.mouse.pressed())
			{
				// FlxTilemaps can be manually edited at runtime as well.
				// Setting a tile to 0 removes it, and setting it to anything else will place a tile.
				// If auto map is on, the map will automatically update all surrounding tiles.
				collisionMap.setTile(FlxG.mouse.x / TILE_WIDTH, FlxG.mouse.y / TILE_HEIGHT, FlxG.keys.SHIFT?0:1);
			}
             * 
             */

            //Toggle the bounding box visibility
            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B))
                FlxG.showBounds = !FlxG.showBounds;


            if (FlxG.mouse.pressed())
            {

                tiles.setTile((int)FlxG.mouse.x / 16, (int)FlxG.mouse.y / 16, 0, true);
                decorations.setTile((int)FlxG.mouse.x / 16, ((int)FlxG.mouse.y / 16) - 1, 0, true);

            }

            if (FlxG.keys.justPressed(Keys.K))
            {
                FlxOnlineStatCounter.sendStats("hawksnest", "marksman", 1);

                Console.WriteLine(FlxOnlineStatCounter.lastRecievedStat);
            }
            if (FlxG.keys.justPressed(Keys.L))
            {
                FlxOnlineStatCounter.getStatsForLevel("hawksnest", "marksman", 1);

                Console.WriteLine(FlxOnlineStatCounter.lastRecievedStat);

            }
            if (FlxG.keys.justPressed(Keys.J))
            {
                FlxOnlineStatCounter.getAllStats("hawksnest");

            }


            base.update();
        }

        protected bool killSecond(object Sender, FlxSpriteCollisionEvent e)
        {
            e.Object2.kill();
            return true;
        }

        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {
            /*
            if ((e.Object1 is BotBullet) || (e.Object1 is Bullet))
                e.Object1.kill();
            e.Object2.hurt(1);
            return true;
             */

            if (!(e.Object1 is Warlock) && (e.Object2 is Fireball))
            {
                e.Object1.kill();
                e.Object2.kill();
            }

            return true;

        }


    }
}

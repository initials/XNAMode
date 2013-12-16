//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using org.flixel;

//using System.Linq;
//using System.Xml.Linq;

//namespace FourChambers
//{
//    public class CharacterTestState : FlxState
//    {

//        protected Texture2D ImgDirt;

//        private const float FOLLOW_LERP = 3.0f;

//        //private Texture2D Bubbles;
//        private Texture2D DecorTex;
//        //private FlxEmitter _bubbles;
//        private FlxTileblock rotatore;
//        //private FlxTilemap tiles;
//        //private FlxTilemap decorations;


//        protected FlxGroup _bullets;


//        private Automaton automaton;
//        private Corsair corsair;
//        private Executor executor;
//        private Gloom gloom;
//        private Harvester harvester;
//        private Marksman marksman;
//        private Medusa medusa;
//        private Mistress mistress;
//        private Mummy mummy;
//        private Nymph nymph;
//        private Paladin paladin;
//        private Seraphine seraphine;
//        private Succubus succubus;
//        private Tormentor tormentor;
//        private Unicorn unicorn;
//        private Warlock warlock;
//        private Vampire vampire;
//        private Zombie zombie;
//        private FlxGroup actors;

//        override public void create()
//        {
//            base.create();

//            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

//            actors = new FlxGroup();
//            _bullets = new FlxGroup();

//            int i = 0;

//            for (i = 0; i < 20; i++)
//                _bullets.add(new Fireball());

//            add(_bullets);



//            int[] p = new int[] { 50, 50 };

//            vampire = new Vampire(p[1] * 16, p[0] * 16);
//            actors.add(vampire);

//            warlock = new Warlock(p[1] * 16, p[0] * 16, _bullets.members);
//            actors.add(warlock);

//            zombie = new Zombie(p[1] * 16, p[0] * 16);
//            actors.add(zombie);

//            automaton = new Automaton(p[1] * 16, p[0] * 16);
//            actors.add(automaton);

//            corsair = new Corsair(p[1] * 16, p[0] * 16);
//            actors.add(corsair);

//            executor = new Executor(p[1] * 16, p[0] * 16);
//            actors.add(executor);

//            gloom = new Gloom(p[1] * 16, p[0] * 16);
//            actors.add(gloom);

//            harvester = new Harvester(p[1] * 16, p[0] * 16);
//            actors.add(harvester);

//            marksman = new Marksman(p[1] * 16, p[0] * 16);
//            actors.add(marksman);

//            mistress = new Mistress(p[1] * 16, p[0] * 16);
//            actors.add(mistress);

//            medusa = new Medusa(p[1] * 16, p[0] * 16);
//            actors.add(medusa);

//            mummy = new Mummy(p[1] * 16, p[0] * 16);
//            actors.add(mummy);

//            nymph = new Nymph(p[1] * 16, p[0] * 16);
//            actors.add(nymph);

//            paladin = new Paladin(p[1] * 16, p[0] * 16);
//            actors.add(paladin);

//            seraphine = new Seraphine(p[1] * 16, p[0] * 16);
//            actors.add(seraphine);

//            succubus = new Succubus(p[1] * 16, p[0] * 16);
//            actors.add(succubus);

//            tormentor = new Tormentor(p[1] * 16, p[0] * 16);
//            actors.add(tormentor);

//            unicorn = new Unicorn(p[1] * 16, p[0] * 16);
//            actors.add(unicorn);

//            add(actors);

//            ImgDirt = FlxG.Content.Load<Texture2D>("Mode/dirt");

//            rotatore = new FlxTileblock(0, 140, FlxG.width, 20);
//            rotatore.loadTiles(ImgDirt, 16, 16, 0);

//            add(rotatore);

//            FlxG.follow(vampire, FOLLOW_LERP);
//            //FlxG.followAdjust(0.5f, 0.0f);
//            FlxG.followBounds(0, 0, 50 * 16, 40 * 16);


//        }

//        override public void update()
//        {

//            FlxU.collide(actors, rotatore);

//            if (FlxG.keys.justPressed(Microsoft.Xna.Framework.Input.Keys.B))
//                FlxG.showBounds = !FlxG.showBounds;

//            base.update();
//        }


//    }
//}

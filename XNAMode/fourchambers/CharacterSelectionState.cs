 using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using System.Reflection;

namespace XNAMode
{
    public class CharacterSelectionState : FlxState
    {
        private FlxSprite bgSprite;

        private FlxSprite marksmanOn;
        private FlxSprite marksmanOff;

        private FlxSprite mistressOn;
        private FlxSprite mistressOff;

        private FlxSprite warlockOn;
        private FlxSprite warlockOff;

        private FlxSprite corsairOn;
        private FlxSprite corsairOff;

        private FlxSprite paladinOn;
        private FlxSprite paladinOff;
        
        private FlxSprite unicornOn;
        private FlxSprite unicornOff;

        private FlxButton btnmarksman;
        private FlxButton btnmistress;
        private FlxButton btnwarlock;
        private FlxButton btncorsair;
        private FlxButton btnpaladin;
        private FlxButton btnunicorn;

        private FlxGroup buttonsGrp;



        /// <summary>
        /// Creates the scene.
        /// </summary>
        override public void create()
        {
            FlxG.backColor = Color.Black;

            base.create();

            buttonsGrp = new FlxGroup();


            // HUD - use P1 for the character name.
            // P3 for the text.
            FlxG.resetHud();
            FlxG.showHud();
            FlxG.setHudText(1, "Choose A Real Human Being");
            FlxG.setHudText(3, "");

            FlxG.setHudTextPosition(1, 80, 30);
            FlxG.setHudTextScale(1, 2);
            FlxG.setHudTextPosition(3, 80, 150);
            FlxG.setHudTextScale(3, 2);

            FlxG.setHudGamepadButton(FlxButton.ControlPadA, FlxG.width - 30, 160);




            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            Texture2D bgGraphic = FlxG.Content.Load<Texture2D>("initials/room");
            bgSprite = new FlxSprite(0, 0, bgGraphic);
            bgSprite.loadGraphic(bgGraphic);
            bgSprite.x = 80;
            bgSprite.y = 65;
            bgSprite.boundingBoxOverride = false;
            add(bgSprite);
            bgSprite.color = Color.DimGray;



            corsairOff = new FlxSprite(0, 0, null);
            corsairOff.loadGraphic(FlxG.Content.Load<Texture2D>("initials/corsair_18x21"), true, false, 18, 21);
            corsairOff.addAnimation("idle", new int[] { 0 }, 12);

            corsairOn = new FlxSprite(0, 0, null);
            corsairOn.loadGraphic(FlxG.Content.Load<Texture2D>("initials/corsair_18x21"), true, false, 18, 21);
            corsairOn.addAnimation("run", new int[] { 1, 2, 3, 4, 5}, 12);
            corsairOn.play("run");

            btncorsair = new FlxButton(85, 90, onCorsair,FlxButton.ControlPadA);
            btncorsair.loadGraphic(corsairOff, corsairOn);
            buttonsGrp.add(btncorsair);

            btncorsair.on = true;

            FlxG.setHudGamepadButton(FlxButton.ControlPadA, btncorsair.x,(btncorsair.y+corsairOff.height*1.5f));



            
            //Marksma
            marksmanOff = new FlxSprite(0, 0, null);
            marksmanOff.loadGraphic(FlxG.Content.Load<Texture2D>("initials/marksman_ss_31x24"), true, false, 31, 24);
            marksmanOff.addAnimation("idle", new int[] { 0 }, 12);

            marksmanOn = new FlxSprite(0, 0, null);
            marksmanOn.loadGraphic(FlxG.Content.Load<Texture2D>("initials/marksman_ss_31x24"), true, false, 31, 24);
            marksmanOn.addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
            marksmanOn.play("run");

            btnmarksman = new FlxButton(100, 90, onMarksman);
            btnmarksman.loadGraphic(marksmanOff, marksmanOn);
            buttonsGrp.add(btnmarksman); 
            btnmarksman.on = false;


            add(buttonsGrp);


        }


        private void onMarksman()
        {
            Console.WriteLine("On Marksman");
            FlxG.setHudText(3, "Marksman");
            FlxG.transition.startFadeOut(0.05f);

        }

        private void onCorsair()
        {
            Console.WriteLine("On Corsair");
            FlxG.setHudText(3, "Corsair");
            FlxG.transition.startFadeOut(0.05f);
        }

        override public void update()
        {

            // exit.
            if (FlxG.keys.ESCAPE)
            {
                FlxG.state = new GameSelectionMenuState();
                return;
            }


            if (FlxG.transition.complete)
            {
                FlxG.state = new BasePlayState();
            }

            base.update();
        }




    }
}

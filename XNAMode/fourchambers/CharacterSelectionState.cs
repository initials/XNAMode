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

        private Marksman marksmanOn;
        private Marksman marksmanOff;

        private Mistress mistress;
        private Warlock warlock;
        private Corsair corsair;
        private Paladin paladin;
        private Unicorn unicorn;

        private FlxButton btnmarksman;
        private FlxButton btnmistress;
        private FlxButton btnwarlock;
        private FlxButton btncorsair;
        private FlxButton btnpaladin;
        private FlxButton btnunicorn;


        private List<Dictionary<string, string>> scriptList;
        private int totalScriptTexts;
        private int currentScriptText;

        /// <summary>
        /// Creates the scene.
        /// </summary>
        override public void create()
        {
            FlxG.backColor = Color.Black;

            base.create();


            // HUD - use P1 for the character name.
            // P3 for the text.
            FlxG.resetHud();
            FlxG.showHud();
            FlxG.setHudText(1, "x");
            FlxG.setHudText(3, "y");

            FlxG.setHudTextPosition(1, 80, 140);
            FlxG.setHudTextScale(1, 2);
            FlxG.setHudTextPosition(3, 80, 150);
            FlxG.setHudTextScale(3, 1);

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

            marksmanOff = new Marksman(110, 95, null);
            marksmanOff.isPlayerControlled = false;

            marksmanOn = new Marksman(110, 95, null);
            marksmanOn.isPlayerControlled = false;
            marksmanOn.play("run");

            //add(marksman);

            //mistress = new Mistress(130, 95);
            //mistress.isPlayerControlled = false;
            //add(mistress);

            //warlock = new Warlock(150, 95, null);
            //warlock.isPlayerControlled = false;
            //add(warlock);

            btncorsair = new FlxButton(70, 95, onCorsair);
            btncorsair.loadGraphic((new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("initials/corsair_18x21"), false, false, 18, 21)), (new FlxSprite().loadGraphic(FlxG.Content.Load<Texture2D>("initials/corsair_18x21"), false, false, 18, 21)));
            add(btncorsair);

            btnmarksman = new FlxButton(100, 95, onCorsair);
            btnmarksman.loadGraphic(marksmanOff, marksmanOn);
            add(btnmarksman);

            scriptList = FlxXMLReader.readCustomXML("script", "levelSettings.xml");

            totalScriptTexts = scriptList.Count();

        }



        private void onCorsair()
        {
            
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

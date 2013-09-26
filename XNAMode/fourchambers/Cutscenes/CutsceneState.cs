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
    public class CutsceneState : FlxState
    {

        private FlxSprite bgSprite;

        private Marksman marksman;
        private Mistress mistress;
        private Warlock warlock;

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
            FlxG.setHudText(1, "Linda Lee");
            FlxG.setHudText(3, "Hey hey hey");

            FlxG.setHudTextPosition(1, 80, 140);
            FlxG.setHudTextScale(1, 2);
            FlxG.setHudTextPosition(3, 80, 150);
            FlxG.setHudTextScale(3,1);

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

            marksman = new Marksman(110,95,null);
            marksman.isPlayerControlled = false;
            add(marksman);

            mistress = new Mistress(130, 95);
            mistress.isPlayerControlled = false;
            add(mistress);

            warlock = new Warlock(150, 95,null);
            warlock.isPlayerControlled = false;
            add(warlock);

            scriptList = FlxXMLReader.readCustomXML("script", "levelSettings.xml");

            totalScriptTexts = scriptList.Count();

            //Console.WriteLine("------------------------------------------------------" + x[0]);

            
            setForScriptContents();


        }



        private void setForScriptContents()
        {
            // First find the actor

            string actor = scriptList[currentScriptText]["actor"];

            // Next find the text 

            string text = scriptList[currentScriptText]["text"];
            text = text.Replace("\n", Environment.NewLine);
            
            FlxG.setHudText(1, actor);
            FlxG.setHudText(3, text);

            foreach (var item in scriptList[currentScriptText])
            {
                //Console.WriteLine(item.ToString());
                

            }
        }

        override public void update()
        {

            PlayerIndex pi;

            if (FlxG.keys.justPressed(Keys.Enter) || (FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi)))
            {
                if (currentScriptText < totalScriptTexts - 1) {
                    currentScriptText ++;
                    setForScriptContents();

                }
                else 
                {
                    FlxG.fade.start(Color.Black);
                    FlxG.setHudText(1, "");
                    FlxG.setHudText(3, "");
                    FlxG.transition.startFadeOut(0.1f, 0, 120);
                }
            }

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

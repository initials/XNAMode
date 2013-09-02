using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class CutsceneState : FlxState
    {

        private FlxSprite bgSprite;

        private Marksman marksman;
        private Mistress mistress;
        private Warlock warlock;

        /// <summary>
        /// Creates the scene.
        /// </summary>
        override public void create()
        {
            FlxG.backColor = Color.Black;

            base.create();

            
            // HUD - use P1 for the character name.
            // P2 for the text.
            FlxG.resetHud();

            FlxG.setHudText(1, "Linda Lee");
            FlxG.setHudText(3, "Hey hey hey");

            FlxG.setHudTextPosition(1, 80, 140);
            FlxG.setHudTextScale(1, 2);
            FlxG.setHudTextPosition(3, 80, 150);
            FlxG.setHudTextScale(3, 2);

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


            /*

            // Build a list of the script

            levelAttrs = new Dictionary<string, string>();

            // get the level to parse using FlxG.level

            string currentLevel = "l" + FlxG.level.ToString();

            XElement xelement = XElement.Load("levelSettings.xml");

            foreach (XElement xEle in xelement.Descendants("settings").Elements())
            {
                XElement firstSpecificChildElement = xEle.Element(currentLevel);

                if (firstSpecificChildElement != null)
                {
                    if (firstSpecificChildElement.Value.ToString() == "")
                    {
                        levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());

                        //Console.WriteLine(" empty string " + xEle.Name.ToString() + "  " +  xEle.Attribute("default").Value.ToString());
                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");

                        if (playerControlled != null)
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }

                    }
                    else
                    {
                        levelAttrs.Add(xEle.Name.ToString(), firstSpecificChildElement.Value.ToString());

                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");

                        if (playerControlled != null)
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }
                    }
                }
                else
                {

                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());

                }
            }
            */


        }

        override public void update()
        {


            // exit.
            if (FlxG.keys.ESCAPE)
            {
                FlxG.state = new GameSelectionMenuState();
                return;
            }

            base.update();
        }


    }
}

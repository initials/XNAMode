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
    public class DebugMenuState : FlxState
    {

        override public void create()
        {
            base.create();

            string textInfo = "";
            textInfo = "Choose:\n";
            textInfo += "F5. Sprite Test State\n";
            textInfo += "F6. Open initialsgames.com/FourChambers\n";
            textInfo += "F7. FlxBar Test\n";
            textInfo += "F8. Victory State\n";
            textInfo += "F9. Grid\n";
            textInfo += "F10. HUD Test.\n";
            textInfo += "1. Level Visualizer\n";
            textInfo += "2. Mode\n";
            textInfo += "3. Path Test\n";
            textInfo += "4. Level Begin Text Test\n";
            textInfo += "5. Clean Test State\n";
            textInfo += "6. Lemonade Test\n";
            textInfo += "7. VCR Test\n";
            textInfo += "8. Rotate Test\n";
            textInfo += "9. Empty Intro\n";
            textInfo += "\n\n GAMES! \n\n";
            textInfo += "Q. Mode\n";
            textInfo += "W. Revvolvver\n";
            textInfo += "E. Four Chambers of the Human Heart\n";
            textInfo += "R. Super Lemonade Factory Part Two\n";





            FlxG.showHud();

            FlxG.setHudText(1, textInfo );
            FlxG.setHudGamepadButton(FlxHud.TYPE_KEYBOARD, FlxButton.ControlPadA, 120, 120);


        }

        override public void update()
        {
//			if ((FlxG.gamepads.isButtonDown(Buttons.RightStick) && FlxG.gamepads.isButtonDown(Buttons.LeftStick)))
//			{
//				FlxG.state = new HUDTest();
//			}

#if !__ANDROID__
            if (FlxG.keys.justPressed(Keys.F5))
            {
                FlxG.state = new FourChambers.SpriteTestState();

            }
            if (FlxG.keys.justPressed(Keys.F6))
            {
                FlxU.openURL("http://initialsgames.com/fourchambers/purchasecopy.php");
                
                FlxG.Game.Exit();
                    
            }

            if (FlxG.keys.justPressed(Keys.F7))
            {
                FlxG.state = new FourChambers.FlxBarTestState();

            }
            if (FlxG.keys.justPressed(Keys.F8))
            {
                Lemonade.Lemonade_Globals.location = "military";
                FlxG.level = 12;
                FlxG.state = new Lemonade.VictoryState();

            }
            if (FlxG.keys.justPressed(Keys.F9))
            {
                FlxG.state = new Grid();
            }
            if (FlxG.keys.justPressed(Keys.F10))
            {
                FlxG.state = new HUDTest();
            }
            if (FlxG.keys.ONE)
            {
                FlxG.state = new FourChambers.LevelVisualizerState();
                FlxG.hideHud();
            }
            if (FlxG.keys.TWO)
            {
                FlxG.state = new MenuState();
                FlxG.hideHud();
            }
            if (FlxG.keys.THREE)
            {
                FlxG.state = new FourChambers.PathTestState();
                //FlxG.hideHud();
            }
            if (FlxG.keys.FOUR)
            {
                FlxG.state = new FourChambers.LevelBeginTextState();
                FlxG.hideHud();
            }
            if (FlxG.keys.FIVE)
            {
                FlxG.state = new FourChambers.CleanTestState();
                FlxG.hideHud();
            }
            if (FlxG.keys.SIX)
            {
                FlxG.state = new Lemonade.MenuState();
                FlxG.hideHud();
            }
            if (FlxG.keys.SEVEN)
            {
                FlxG.state = new FourChambers.VCRState();
                
            }
            if (FlxG.keys.EIGHT)
            {
                FlxG.state = new RotateState();
                //FlxG.hideHud();
            }
            if (FlxG.keys.NINE)
            {
                FlxG.state = new FourChambers.EmptyIntroTestState();
                //FlxG.hideHud();
            }
            if (FlxG.keys.Q)
            {
                FlxG.state = new MenuState();
            }
            if (FlxG.keys.W)
            {
                FlxG.state = new Revvolvver.MenuState();
            }
            if (FlxG.keys.E)
            {
                FlxG.state = new FourChambers.GameSelectionMenuState();
            }
            if (FlxG.keys.R)
            {
                FlxG.state = new Lemonade.EasyMenuState();
            }












#endif
            base.update();
        }


    }
}

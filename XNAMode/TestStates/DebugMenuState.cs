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
    public class DebugMenuState : FlxState
    {

        override public void create()
        {
            base.create();

            string textInfo = "";
            textInfo = "Choose:\n";
            textInfo += "1. Level Visualizer\n";
            textInfo += "2. Mode\n";
            textInfo += "3. Path Test\n";
            textInfo += "4. Level Begin Text Test\n";
            textInfo += "5. Clean Test State\n";
            textInfo += "6. Lemonade Test\n";

            FlxG.showHud();

            FlxG.setHudText(1, textInfo );
            FlxG.setHudGamepadButton(FlxButton.ControlPadA, 120, 120);


        }

        override public void update()
        {

            if (FlxG.keys.ONE)
            {
                FlxG.state = new LevelVisualizerState();
                FlxG.hideHud();
            }
            if (FlxG.keys.TWO)
            {
                FlxG.state = new MenuState();
                FlxG.hideHud();
            }
            if (FlxG.keys.THREE)
            {
                FlxG.state = new PathTestState();
                //FlxG.hideHud();
            }
            if (FlxG.keys.FOUR)
            {
                FlxG.state = new LevelBeginTextState();
                FlxG.hideHud();
            }
            if (FlxG.keys.FIVE)
            {
                FlxG.state = new CleanTestState();
                FlxG.hideHud();
            }
            if (FlxG.keys.SIX)
            {
                FlxG.state = new Lemonade.LemonadeTestState();
                FlxG.hideHud();
            }
            base.update();
        }


    }
}

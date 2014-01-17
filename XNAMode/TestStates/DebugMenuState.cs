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
            textInfo += "7. VCR Test\n";
            textInfo += "8. Rotate Test\n";

            FlxG.showHud();

            FlxG.setHudText(1, textInfo );
            FlxG.setHudGamepadButton(FlxButton.ControlPadA, 120, 120);


        }

        override public void update()
        {
#if !__ANDROID__

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
                FlxG.state = new Lemonade.LemonadeTestState();
                FlxG.hideHud();
            }
            if (FlxG.keys.SEVEN)
            {
                FlxG.state = new FourChambers.VCRState();
                FlxG.hideHud();
            }
            if (FlxG.keys.EIGHT)
            {
                FlxG.state = new RotateState();
                //FlxG.hideHud();
            }
#endif
            base.update();
        }


    }
}

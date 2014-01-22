using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class BasePlayStateFromOelTutorial : BasePlayStateFromOel
    {
        override public void create()
        {
            base.create();

            //FlxG.playMusic("music/" + FourChambers_Globals.MUSIC_TUTORIAL, 1.0f);
        }

        override public void update()
        {


            base.update();
        }
    }

}

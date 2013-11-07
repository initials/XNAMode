using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAMode
{
    public delegate void EventCallback();

    class EventSprite : FlxSprite
    {
        private EventCallback _callback;
        private int _repeats;


        public EventSprite(int xPos, int yPos, EventCallback Callback, int Repeats)
            : base(xPos, yPos)
        {
            _callback = Callback;
            _repeats = Repeats;





        }

        override public void update()
        {



            base.update();

        }

        public void runCallback()
        {
            _callback();

        }


    }
}

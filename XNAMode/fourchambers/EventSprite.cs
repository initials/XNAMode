using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    public delegate void EventCallback(string value);

    class EventSprite : FlxSprite
    {
        private EventCallback _callback;
        public int repeats;
        public string command;
        


        public EventSprite(int xPos, int yPos, EventCallback Callback, int Repeats, string Command)
            : base(xPos, yPos)
        {
            _callback = Callback;
            repeats = Repeats;
            health = Repeats;
            command = Command;

            if (FlxG.debug)
            {
                visible = false;
                alpha = 0.1f;
            }
            else
            {
                visible = false;
            }


        }

        override public void update()
        {



            base.update();

        }

        public void runCallback()
        {
            _callback(command);

        }


    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace org.flixel
{
    public class FlxMenuState : FlxState
    {

        public FlxGroup buttons;

        override public void create()
        {
            base.create();

            buttons = new FlxGroup();
            


        }

        public void addButtons()
        {
            add(buttons);

            ((FlxButton)(buttons.members[0])).on = true;
        }

        public void moveSelected(string direction)
        {
            int cur = getCurrentSelected();

            if (direction == "forward")
            {
                ((FlxButton)(buttons.members[cur])).on = false;
                ((FlxButton)(buttons.members[cur + 1])).on = true;
            }
            else if (direction == "backward")
            {
                ((FlxButton)(buttons.members[cur])).on = false;
                ((FlxButton)(buttons.members[cur - 1])).on = true;
            }
        }

        public int getCurrentSelected()
        {
            int count = 0;
            foreach (var item in buttons.members)
            {
                if (((FlxButton)(buttons.members[count])).on == true)
                {
                    FlxG.write(count.ToString());

                    return count;
                }
                count++;
            }
            return -1;
        }

        override public void update()
        {
            
            if (FlxG.keys.justPressed(Keys.Left))
            {
                moveSelected("backward");
            }

            if (FlxG.keys.justPressed(Keys.Right))
            {
                moveSelected("forward");
            }


            base.update();
        }


    }
}

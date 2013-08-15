using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;
using Microsoft.Xna.Framework.GamerServices;

using System.Linq;
using System.Xml.Linq;

namespace XNAMode
{
    public class GameSelectionMenuState : FlxState
    {

        FlxText _menuItems;

        FlxText _nameEntry;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            _menuItems = new FlxText(10, 30, FlxG.width);

            _menuItems.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _menuItems.text = "1. Mode\n2. Hawksnest\n\nEnter name, use @ symbol to specify Twitter handle.";

            add(_menuItems);




            _nameEntry = new FlxText(10, 200, FlxG.width);

            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _nameEntry.text = "";

            add(_nameEntry);


        }

        override public void update()
        {

            if (FlxG.keys.F1)
            {
                FlxG.state = new MenuState();

            }
            if (FlxG.keys.F2)
            {
                FlxG.state = new CaveState();

            }

            PlayerIndex pi;

            //_nameEntry.text = FlxG.keys.trackingString;

            bool shift = FlxG.keys.SHIFT;

            if (FlxG.keys.isNewKeyPress( Keys.A, null, out pi))  _nameEntry.text += shift ? "A" : "a"; 
            if (FlxG.keys.isNewKeyPress( Keys.B, null, out pi))  _nameEntry.text += shift ? "B" : "b"; 
            if (FlxG.keys.isNewKeyPress( Keys.C, null, out pi))  _nameEntry.text += shift ? "C" : "c"; 
            if (FlxG.keys.isNewKeyPress( Keys.D, null, out pi))  _nameEntry.text += shift ? "D" : "d"; 
            if (FlxG.keys.isNewKeyPress( Keys.E, null, out pi))  _nameEntry.text += shift ? "E" : "e"; 
            if (FlxG.keys.isNewKeyPress( Keys.F, null, out pi))  _nameEntry.text += shift ? "F" : "f"; 
            if (FlxG.keys.isNewKeyPress( Keys.G, null, out pi))  _nameEntry.text += shift ? "G" : "g"; 
            if (FlxG.keys.isNewKeyPress( Keys.H, null, out pi))  _nameEntry.text += shift ? "H" : "h"; 
            if (FlxG.keys.isNewKeyPress( Keys.I, null, out pi))  _nameEntry.text += shift ? "I" : "i"; 
            if (FlxG.keys.isNewKeyPress( Keys.J, null, out pi))  _nameEntry.text += shift ? "J" : "j"; 
            if (FlxG.keys.isNewKeyPress( Keys.K, null, out pi))  _nameEntry.text += shift ? "K" : "k"; 
            if (FlxG.keys.isNewKeyPress( Keys.L, null, out pi))  _nameEntry.text += shift ? "L" : "l"; 
            if (FlxG.keys.isNewKeyPress( Keys.M, null, out pi))  _nameEntry.text += shift ? "M" : "m"; 
            if (FlxG.keys.isNewKeyPress( Keys.N, null, out pi))  _nameEntry.text += shift ? "N" : "n"; 
            if (FlxG.keys.isNewKeyPress( Keys.O, null, out pi))  _nameEntry.text += shift ? "O" : "o"; 
            if (FlxG.keys.isNewKeyPress( Keys.P, null, out pi))  _nameEntry.text += shift ? "P" : "p"; 
            if (FlxG.keys.isNewKeyPress( Keys.Q, null, out pi))  _nameEntry.text += shift ? "Q" : "q"; 
            if (FlxG.keys.isNewKeyPress( Keys.R, null, out pi))  _nameEntry.text += shift ? "R" : "r"; 
            if (FlxG.keys.isNewKeyPress( Keys.S, null, out pi))  _nameEntry.text += shift ? "S" : "s"; 
            if (FlxG.keys.isNewKeyPress( Keys.T, null, out pi))  _nameEntry.text += shift ? "T" : "t"; 
            if (FlxG.keys.isNewKeyPress( Keys.U, null, out pi))  _nameEntry.text += shift ? "U" : "u"; 
            if (FlxG.keys.isNewKeyPress( Keys.V, null, out pi))  _nameEntry.text += shift ? "V" : "v"; 
            if (FlxG.keys.isNewKeyPress( Keys.W, null, out pi))  _nameEntry.text += shift ? "W" : "w"; 
            if (FlxG.keys.isNewKeyPress( Keys.X, null, out pi))  _nameEntry.text += shift ? "X" : "x"; 
            if (FlxG.keys.isNewKeyPress( Keys.Y, null, out pi))  _nameEntry.text += shift ? "Y" : "y"; 
            if (FlxG.keys.isNewKeyPress( Keys.Z, null, out pi))  _nameEntry.text += shift ? "Z" : "z"; 


            base.update();
        }


    }
}

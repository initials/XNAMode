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

        FlxSave save;
        bool hasCheckedSave ;

        //FlxTransition transition;


        override public void create()
        {

            FlxG.backColor = new Color(0xc2, 0x88, 0x83);

            base.create();

            //FlxSprite bg = new FlxSprite(0, 0);
            //bg.loadGraphic(FlxG.Content.Load<Texture2D>("initials/Ambience"));
            //bg.scale = 2;
            //add(bg);



            //c28883


            FlxTileblock bg = new FlxTileblock(0, FlxG.height - 256, 256 * 3, 256);
            
            bg.loadTiles(FlxG.Content.Load<Texture2D>("initials/Ambience"), 256, 256, 0);

            add(bg);




            hasCheckedSave = false;

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("Mode/cursor"));

            _menuItems = new FlxText(10, 10, FlxG.width);

            _menuItems.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _menuItems.text = "F1. Mode\nF2. Hawksnest\n\nEnter name, use @ symbol to specify Twitter handle. Press enter when complete.";

            add(_menuItems);


            save = new FlxSave();
            //save.bind("Mode");
            //Console.WriteLine(save.data["player_name"]) ;


            _nameEntry = new FlxText(10, 80, FlxG.width);

            _nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);

            _nameEntry.text = "";

            add(_nameEntry);


            FlxG.flash.start(Color.Black,0.5f);


            


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

            if (FlxG.keys.isNewKeyPress(Keys.D1, null, out pi)) _nameEntry.text += shift ? "!" : "1";
            if (FlxG.keys.isNewKeyPress(Keys.D2, null, out pi)) _nameEntry.text += shift ? "@" : "2";
            if (FlxG.keys.isNewKeyPress(Keys.D3, null, out pi)) _nameEntry.text += shift ? "#" : "3";
            if (FlxG.keys.isNewKeyPress(Keys.D4, null, out pi)) _nameEntry.text += shift ? "$" : "4";
            if (FlxG.keys.isNewKeyPress(Keys.D5, null, out pi)) _nameEntry.text += shift ? "%" : "5";
            if (FlxG.keys.isNewKeyPress(Keys.D6, null, out pi)) _nameEntry.text += shift ? "^" : "6";
            if (FlxG.keys.isNewKeyPress(Keys.D7, null, out pi)) _nameEntry.text += shift ? "&" : "7";
            if (FlxG.keys.isNewKeyPress(Keys.D8, null, out pi)) _nameEntry.text += shift ? "*" : "8"; 
            if (FlxG.keys.isNewKeyPress(Keys.D9, null, out pi)) _nameEntry.text += shift ? "(" : "9";
            if (FlxG.keys.isNewKeyPress(Keys.D0, null, out pi)) _nameEntry.text += shift ? ")" : "0";
            if (FlxG.keys.isNewKeyPress(Keys.OemMinus, null, out pi)) _nameEntry.text += shift ? "_" : "-";

            if (FlxG.keys.isNewKeyPress(Keys.Back, null, out pi))
            {

                if (_nameEntry.text.Length <= 0)
                {
                    return;
                }

                string backspaced = _nameEntry.text;

                _nameEntry.text = backspaced.Remove(backspaced.Length - 1);
            }


            if (!hasCheckedSave)
            {
                
                if (save.waitingOnDeviceSelector)
                {
                    //Console.WriteLine("enter waitingOnDeviceSelector");
                    return;
                }
                else if (save.canSave)
                {
                    hasCheckedSave = true;
                    Console.WriteLine("enter can save");
                    if (save.bind("Mode"))
                    {
                        //Console.WriteLine("bound");
                        if (save.data["player_name"] == null)
                            _nameEntry.text = "";
                        else
                            _nameEntry.text = save.data["player_name"];
                        FlxG.log("Player name is: " + save.data["player_name"]);
                        FlxG.username = save.data["player_name"];
                        save.forceSave(0);

                        //FlxG.transition.startFadeIn();
                    }
                }
            }
            if (FlxG.keys.isNewKeyPress(Keys.Enter, null, out pi))
            {

                if (save.waitingOnDeviceSelector)
                {
                    //Console.WriteLine("enter waitingOnDeviceSelector");
                    return;
                }
                else if (save.canSave)
                {
                    //Console.WriteLine("enter can save");
                    if (save.bind("Mode"))
                    {
                        save.data["player_name"] = _nameEntry.text;
                        FlxG.log("Player name is: " + save.data["player_name"]);
                        FlxG.username = save.data["player_name"];

                        save.forceSave(0);
                    }
                }

                FlxG.transition.startFadeOut();
            }

            if (FlxG.transition.complete)
            {
                FlxG.state = new CaveState();
            }


            base.update();
        }


    }
}

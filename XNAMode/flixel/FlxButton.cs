﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace org.flixel
{
    public delegate void FlxButtonClick();

    /**
     * A simple button class that calls a function when clicked by the mouse.
     * Supports labels, highlight states, and parallax scrolling.
     */
    public class FlxButton : FlxGroup
    {

        public const int ControlPadA = 0;
        public const int ControlPadB = 1;
        public const int ControlPadX = 2;
        public const int ControlPadY = 3;
        public const int ControlPadRStick = 4;
        public const int ControlPadLStick = 5;
        public const int ControlPadDPad = 6;
        public const int ControlPadRB = 7;
        public const int ControlPadLB = 8;
        public const int ControlPadRT = 9;
        public const int ControlPadLT = 10;
        public const int ControlPadBack = 11;
        public const int ControlPadStart = 12;



        /**
         * Set this to true if you want this button to function even while the game is paused.
         */
        public bool pauseProof;

        /**
         * Used for checkbox-style behavior.
         */
        protected bool _onToggle;

        /**
         * Stores the 'off' or normal button state graphic.
         */
        protected FlxSprite _off;
        /**
         * Stores the 'on' or highlighted button state graphic.
         */
        protected FlxSprite _on;
        /**
         * Stores the 'off' or normal button state label.
         */
        protected FlxText _offT;
        /**
         * Stores the 'on' or highlighted button state label.
         */
        protected FlxText _onT;

        /// <summary>
        /// holds the controller button graphic
        /// </summary>
        protected FlxSprite _controllerButton;

        /// <summary>
        /// Holds the controller button that can activate the button.
        /// </summary>
        protected int _controllerButtonIndex;
        /**
         * This function is called when the button is clicked.
         */
        protected FlxButtonClick _callback;
        /**
         * Tracks whether or not the button is currently pressed.
         */
        protected bool _pressed;
        /**
         * Whether or not the button has initialized itself yet.
         */
        protected bool _initialized;
        /**
         * Helper variable for correcting its members' <code>scrollFactor</code> objects.
         */
        protected Vector2 _sf;

        /**
         * Creates a new <code>FlxButton</code> object with a gray background
         * and a callback function on the UI thread.
         * 
         * @param	X			The X position of the button.
         * @param	Y			The Y position of the button.
         * @param	Callback	The function to call whenever the button is clicked.
         */
        public FlxButton(int X, int Y, FlxButtonClick Callback)
            : base()
        {
            x = X;
            y = Y;
            width = 100;
            height = 20;
            _off = new FlxSprite().createGraphic((int)width, (int)height, new Color(0x7f, 0x7f, 0x7f));
            _off.solid = false;
            add(_off, true);
            _on = new FlxSprite().createGraphic((int)width, (int)height, Color.White);
            _on.solid = false;
            add(_on, true);
            _offT = null;
            _onT = null;
            _callback = Callback;
            _onToggle = false;
            _pressed = false;
            _initialized = false;
            _sf = Vector2.Zero;
            pauseProof = false;

            _controllerButtonIndex = -1;
            //_controllerButton = new FlxSprite((int)width+5,0);
            //_controllerButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/BP3_SSTRIP_32"),true,false,32,32);
            //_controllerButton.solid = false;
            ////_controllerButton.scale
            //add(_controllerButton, true);



        }


        public FlxButton(int X, int Y, FlxButtonClick Callback, int Button)
            : base()
        {
            x = X;
            y = Y;
            width = 100;
            height = 20;
            _off = new FlxSprite().createGraphic((int)width, (int)height, new Color(0x7f, 0x7f, 0x7f));
            _off.solid = false;
            add(_off, true);
            _on = new FlxSprite().createGraphic((int)width, (int)height, Color.White);
            _on.solid = false;
            add(_on, true);
            _offT = null;
            _onT = null;
            _callback = Callback;
            _onToggle = false;
            _pressed = false;
            _initialized = false;
            _sf = Vector2.Zero;
            pauseProof = false;
            _controllerButtonIndex = Button;

            _controllerButton = new FlxSprite((int)width + 5, 0);
            _controllerButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/BP3_SSTRIP_32"), true, false, 31, 32);
            _controllerButton.width = 29;
            _controllerButton.height = 30;
            _controllerButton.offset.X = 1;
            _controllerButton.offset.Y = 1;
            _controllerButton.addAnimation("frame", new int[] {Button});
            _controllerButton.play("frame");
            _controllerButton.solid = false;
            add(_controllerButton, true);
        }


        /**
         * Set your own image as the button background.
         * 
         * @param	Image				A FlxSprite object to use for the button background.
         * @param	ImageHighlight		A FlxSprite object to use for the button background when highlighted (optional).
         * 
         * @return	This FlxButton instance (nice for chaining stuff together, if you're into that).
         */
        public FlxButton loadGraphic(FlxSprite Image, FlxSprite ImageHighlight)
        {
            _off = replace(_off, Image) as FlxSprite;
            if (ImageHighlight == null)
            {
                if (_on != _off)
                    remove(_on);
                _on = _off;
            }
            else
                _on = replace(_on, ImageHighlight) as FlxSprite;
            _on.solid = _off.solid = false;
            _off.scrollFactor = scrollFactor;
            _on.scrollFactor = scrollFactor;
            width = _off.width;
            height = _off.height;
            refreshHulls();
            return this;
        }

        /**
         * Add a text label to the button.
         * 
         * @param	Text				A FlxText object to use to display text on this button (optional).
         * @param	TextHighlight		A FlxText object that is used when the button is highlighted (optional).
         * 
         * @return	This FlxButton instance (nice for chaining stuff together, if you're into that).
         */
        public FlxButton loadText(FlxText Text, FlxText TextHighlight)
        {
            if (Text != null)
            {
                if (_offT == null)
                {
                    _offT = Text;
                    add(_offT);
                }
                else
                    _offT = replace(_offT, Text) as FlxText;
            }
            if (TextHighlight == null)
                _onT = _offT;
            else
            {
                if (_onT == null)
                {
                    _onT = TextHighlight;
                    add(_onT);
                }
                else
                    _onT = replace(_onT, TextHighlight) as FlxText;
            }
            _offT.scrollFactor = scrollFactor;
            _onT.scrollFactor = scrollFactor;
            return this;
        }


        /**
         * Called by the game loop automatically, handles mouseover and click detection.
         */
        override public void update()
        {
            if (!_initialized)
            {
                if (FlxG.state == null) return;
                FlxG.mouse.addMouseListener(onMouseUp);
                _initialized = true;
            }

            if (_controllerButtonIndex != -1)
            {
                PlayerIndex pi;

                if (FlxG.gamepads.isNewButtonPress(Buttons.A, FlxG.controllingPlayer, out pi) && _controllerButtonIndex == 0)
                {
                    Console.WriteLine("Button A has been pressed");

                    _callback();
                }
                if (FlxG.gamepads.isNewButtonPress(Buttons.B, FlxG.controllingPlayer, out pi) && _controllerButtonIndex == 1)
                {
                    _callback();
                }
                if (FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi) && _controllerButtonIndex == 2)
                {
                    _callback();
                }
                if (FlxG.gamepads.isNewButtonPress(Buttons.Y, FlxG.controllingPlayer, out pi) && _controllerButtonIndex == 3)
                {
                    _callback();
                }
            }

            base.update();

            visibility(false);
            if (overlapsPoint(FlxG.mouse.x, FlxG.mouse.y))
            {
                if (!FlxG.mouse.pressed())
                    _pressed = false;
                else if (!_pressed)
                    _pressed = true;
                visibility(!_pressed);
            }
            if (_onToggle) visibility(_off.visible);
        }

        /**
         * Use this to toggle checkbox-style behavior.
         */
        public bool on
        {
            get
            {
                return _onToggle;
            }
            set
            {
                _onToggle = value;
            }
        }

        /**
         * Called by the game state when state is changed (if this object belongs to the state)
         */
        override public void destroy()
        {
            if (FlxG.mouse != null)
                FlxG.mouse.removeMouseListener(onMouseUp);
        }

        override public void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
            if ((_off != null) && _off.exists && _off.visible) _off.render(spriteBatch);
            if ((_on != null) && _on.exists && _on.visible) _on.render(spriteBatch);
            if (_offT != null)
            {
                if ((_offT != null) && _offT.exists && _offT.visible) _offT.render(spriteBatch);
                if ((_onT != null) && _onT.exists && _onT.visible) _onT.render(spriteBatch);
            }
        }

        /**
         * Internal function for handling the visibility of the off and on graphics.
         * 
         * @param	On		Whether the button should be on or off.
         */
        protected void visibility(bool On)
        {
            if (On)
            {
                _off.visible = false;
                if (_offT != null) _offT.visible = false;
                _on.visible = true;
                if (_onT != null) _onT.visible = true;
            }
            else
            {
                _on.visible = false;
                if (_onT != null) _onT.visible = false;
                _off.visible = true;
                if (_offT != null) _offT.visible = true;
            }
        }

        /**
         * Internal function for handling the actual callback call (for UI thread dependent calls like <code>FlxU.openURL()</code>).
         */
        private void onMouseUp(object Sender, FlxMouseEvent MouseEvent)
        {

            if (!exists || !visible || !active || !FlxG.mouse.justReleased() || (FlxG.pause && !pauseProof) || (_callback == null)) return;
            if (overlapsPoint(FlxG.mouse.x, FlxG.mouse.y)) _callback();

            


        }

    }
}

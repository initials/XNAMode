using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    /// <summary>
    /// Contains the hud that is non scaled.
    /// Can be used to displayed non scaled graphics.
    /// </summary>
    public class FlxHud
    {
        public bool visible = false;

        private Rectangle _srcRect = new Rectangle(1, 1, 1, 1);
        private Rectangle _consoleRect;
        private Color _consoleColor;

        

        /// <summary>
        /// A text box that appears in top left
        /// </summary>
        public FlxText p1HudText;
        /// <summary>
        /// A text box that appears in top right
        /// </summary>
        public FlxText p2HudText;
        /// <summary>
        /// A text box that appears in bottom left
        /// </summary>
        public FlxText p3HudText;
        /// <summary>
        /// A text box that appears in buttom right
        /// </summary>
        public FlxText p4HudText;

        /// <summary>
        /// The hudGroup can be used to push a whole FlxGroup into the hud and have control of it.
        /// </summary>
        public FlxGroup hudGroup;
        /// <summary>
        /// Set this for how long you want to leave up the hud Button.
        /// </summary>
        public float timeToShowButton;
        private float _elapsedSinceLastButtonNeeded;

        /// <summary>
        /// used to display a game pad button at regular resolution.
        /// </summary>
        private FlxSprite _gamePadButton;


        public FlxSprite xboxButton;
        public FlxSprite ouyaButton;
        public FlxSprite keyboardButton;

        public FlxSprite xboxDirection;
        public FlxSprite ouyaDirection;
        public FlxSprite keyboardDirection;

        /// <summary>
        /// Xbox Button A
        /// </summary>
        public const int xboxButtonA = 0;
        /// <summary>
        /// Xbox Button B
        /// </summary>
        public const int xboxButtonB = 1;
        public const int xboxButtonBack1 = 2;
        public const int xboxButtonBack2 = 3;
        public const int xboxDPad = 4;
        public const int xboxDPadDown = 5;
        public const int xboxDPadLeft = 6;
        public const int xboxDPadRight = 7;
        public const int xboxDPadUp = 8;
        public const int xboxButtonLeftBumper = 9;
        public const int xboxButtonLeftTrigger = 10;
        public const int xboxJoystickLeftStick = 11;
        public const int xboxButtonRightBumper = 12;
        public const int xboxButtonRightTrigger = 13;
        public const int xboxJoystickRightStick = 14;
        public const int xboxButtonStart1 = 15;
        public const int xboxButtonStart2 = 16;
        public const int xboxButtonX = 17;
        public const int xboxButtonY = 18;

        public const int ouyaButtonA = 0;
        public const int ouyaDPad = 1;
        public const int ouyaDPadDown = 2;
        public const int ouyaDPadLeft = 3;
        public const int ouyaDPadRight = 4;
        public const int ouyaDPadUp = 5;
        public const int ouyaButtonLeftBumper = 6;
        public const int ouyaButtonLeftTrigger = 7;
        public const int ouyaJoystickLeftStick = 8;
        public const int ouyaButtonStart = 9;
        public const int ouyaButtonO = 10;
        public const int ouyaButtonRightBumper = 11;
        public const int ouyaButtonRightTrigger = 12;
        public const int ouyaJoystickRightStick = 13;
        public const int ouyaPad = 14;
        public const int ouyaButtonU = 15;
        public const int ouyaButtonY = 16;

        public const int Keyboard_Blank_Enter = 0;
        public const int Keyboard_Blank_Mouse = 1;
        public const int Keyboard_Blank_Normal = 2;
        public const int Keyboard_Blank_Super_Wide = 3;
        public const int Keyboard_Blank_Tall = 4;
        public const int Keyboard_Blank_Wide = 5;
        public const int Keyboard_0 = 6;
        public const int Keyboard_1 = 7;
        public const int Keyboard_2 = 8;
        public const int Keyboard_3 = 9;
        public const int Keyboard_4 = 10;
        public const int Keyboard_5 = 11;
        public const int Keyboard_6 = 12;
        public const int Keyboard_7 = 13;
        public const int Keyboard_8 = 14;
        public const int Keyboard_9 = 15;
        public const int Keyboard_A = 16;
        public const int Keyboard_Alt = 17;
        public const int Keyboard_Arrow_Down = 18;
        public const int Keyboard_Arrow_Left = 19;
        public const int Keyboard_Arrow_Right = 20;
        public const int Keyboard_Arrow_Up = 21;
        public const int Keyboard_Asterisk = 22;
        public const int Keyboard_B = 23;
        public const int Keyboard_Backspace = 24;
        public const int Keyboard_Backspace_Alt = 25;
        public const int Keyboard_Bracket_Left = 26;
        public const int Keyboard_Bracket_Right = 27;
        public const int Keyboard_C = 28;
        public const int Keyboard_Caps_Lock = 29;
        public const int Keyboard_Command = 30;
        public const int Keyboard_Ctrl = 31;
        public const int Keyboard_D = 32;
        public const int Keyboard_Del = 33;
        public const int Keyboard_E = 34;
        public const int Keyboard_End = 35;
        public const int Keyboard_Enter = 36;
        public const int Keyboard_Enter_Alt = 37;
        public const int Keyboard_Enter_Tall = 38;
        public const int Keyboard_Esc = 39;
        public const int Keyboard_F = 40;
        public const int Keyboard_G = 41;
        public const int Keyboard_H = 42;
        public const int Keyboard_Home = 43;
        public const int Keyboard_I = 44;
        public const int Keyboard_Insert = 45;
        public const int Keyboard_J = 46;
        public const int Keyboard_K = 47;
        public const int Keyboard_L = 48;
        public const int Keyboard_M = 49;
        public const int Keyboard_Mark_Left = 50;
        public const int Keyboard_Mark_Right = 51;
        public const int Keyboard_Minus = 52;
        public const int Keyboard_Mouse_Left = 53;
        public const int Keyboard_Mouse_Middle = 54;
        public const int Keyboard_Mouse_Right = 55;
        public const int Keyboard_Mouse_Simple = 56;
        public const int Keyboard_N = 57;
        public const int Keyboard_Num_Lock = 58;
        public const int Keyboard_O = 59;
        public const int Keyboard_P = 60;
        public const int Keyboard_Page_Down = 61;
        public const int Keyboard_Page_Up = 62;
        public const int Keyboard_Plus = 63;
        public const int Keyboard_Plus_Tall = 64;
        public const int Keyboard_Print_Screen = 65;
        public const int Keyboard_Q = 66;
        public const int Keyboard_Question = 67;
        public const int Keyboard_Quote = 68;
        public const int Keyboard_R = 69;
        public const int Keyboard_S = 70;
        public const int Keyboard_Semicolon = 71;
        public const int Keyboard_Shift = 72;
        public const int Keyboard_Shift_Alt = 73;
        public const int Keyboard_Slash = 74;
        public const int Keyboard_Space = 75;
        public const int Keyboard_T = 76;
        public const int Keyboard_Tab = 77;
        public const int Keyboard_Tilda = 78;
        public const int Keyboard_U = 79;
        public const int Keyboard_V = 80;
        public const int Keyboard_W = 81;
        public const int Keyboard_Win = 82;
        public const int Keyboard_X = 83;
        public const int Keyboard_Y = 84;
        public const int Keyboard_Z = 85;



        public const int TYPE_KEYBOARD = 0;
        public const int TYPE_XBOX= 1;
        public const int TYPE_OUYA = 2;
        

        /// <summary>
        /// Original positions are used in the <code>reset()</code>
        /// method to reset positions of the text boxes if moved.
        /// </summary>
        public Vector2 p1OriginalPosition;
        public Vector2 p2OriginalPosition;
        public Vector2 p3OriginalPosition;
        public Vector2 p4OriginalPosition;

        //private FlxSprite p1HudSprite;
        //private FlxSprite p2HudSprite;
        //private FlxSprite p3HudSprite;
        //private FlxSprite p4HudSprite;

        public Color color
        {
            get { return _consoleColor; }
        }

        /// <summary>
        /// FlxHud
        /// </summary>
        /// <param name="targetLeft"></param>
        /// <param name="targetWidth"></param>
        public FlxHud(int targetLeft, int targetWidth)
        {
            _consoleRect = new Rectangle(0, 0, FlxG.spriteBatch.GraphicsDevice.Viewport.Width, FlxG.spriteBatch.GraphicsDevice.Viewport.Height);
            _consoleColor = new Color(0, 0, 0, 0x00);

            visible = false;

            //hudGraphic = new FlxSprite(targetLeft+76, FlxG.spriteBatch.GraphicsDevice.Viewport.Height - 36, FlxG.Content.Load<Texture2D>("fourchambers/hudElements"));
            //hudGraphic = new FlxSprite();
            //hudGraphic.visible = false;
            //hudGraphic.scrollFactor.X = 0;
            //hudGraphic.scrollFactor.Y = 0;
            //hudGraphic.scale = 2;

            hudGroup = new FlxGroup();
            hudGroup.scrollFactor.X = 0;
            hudGroup.scrollFactor.Y = 0;

            p1OriginalPosition = new Vector2(targetLeft, 0);
            p2OriginalPosition = new Vector2(targetLeft, 0);
            p3OriginalPosition = new Vector2(targetLeft, FlxG.spriteBatch.GraphicsDevice.Viewport.Height - 20);
            p4OriginalPosition = new Vector2(targetLeft, FlxG.spriteBatch.GraphicsDevice.Viewport.Height - 20);

            p1HudText = new FlxText(targetLeft, 0, targetWidth, "p1HudText").setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            p2HudText = new FlxText(targetLeft, 0, targetWidth, "p2HudText").setFormat(null, 1, Color.White, FlxJustification.Right, Color.White);
            p3HudText = new FlxText(targetLeft, FlxG.spriteBatch.GraphicsDevice.Viewport.Height - 20, targetWidth, "p3HudText").setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            p4HudText = new FlxText(targetLeft, FlxG.spriteBatch.GraphicsDevice.Viewport.Height - 20, targetWidth, "p4HudText").setFormat(null, 1, Color.White, FlxJustification.Right, Color.White);

            _gamePadButton = new FlxSprite(targetLeft,0);
            _gamePadButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/BP3_SSTRIP_64"), true, false, 63, 64);
            _gamePadButton.width = 61;
            _gamePadButton.height = 62;
            _gamePadButton.offset.X = 1;
            _gamePadButton.offset.Y = 1;
            _gamePadButton.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            _gamePadButton.play("frame");
            _gamePadButton.solid = false;
            _gamePadButton.visible = true;
            _gamePadButton.scrollFactor.X = 0;
            _gamePadButton.scrollFactor.Y = 0;
            _gamePadButton.boundingBoxOverride = false;


            keyboardButton = new FlxSprite(targetLeft, 0);
            keyboardButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/MapWhite"), true, false, 100, 100);
            keyboardButton.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            keyboardButton.play("frame");
            keyboardButton.solid = false;
            keyboardButton.visible = true;
            keyboardButton.scrollFactor.X = 1;
            keyboardButton.scrollFactor.Y = 1;
            keyboardButton.boundingBoxOverride = false;


            xboxButton = new FlxSprite(targetLeft, 0);
            xboxButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/Map360"), true, false, 100, 100);
            xboxButton.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            xboxButton.play("frame");
            xboxButton.solid = false;
            xboxButton.visible = true;
            xboxButton.scrollFactor.X = 1;
            xboxButton.scrollFactor.Y = 1;
            xboxButton.boundingBoxOverride = false;


            ouyaButton = new FlxSprite(targetLeft, 0);
            ouyaButton.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/MapOuya"), true, false, 100, 100);
            ouyaButton.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            ouyaButton.play("frame");
            ouyaButton.solid = false;
            ouyaButton.visible = true;
            ouyaButton.scrollFactor.X = 1;
            ouyaButton.scrollFactor.Y = 1;
            ouyaButton.boundingBoxOverride = false;


            keyboardDirection = new FlxSprite(targetLeft, 0);
            keyboardDirection.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/MapWhite"), true, false, 100, 100);
            keyboardDirection.addAnimation("frame", new int[] { Keyboard_Arrow_Up });
            keyboardDirection.play("frame");
            keyboardDirection.solid = false;
            keyboardDirection.visible = true;
            keyboardDirection.scrollFactor.X = 1;
            keyboardDirection.scrollFactor.Y = 1;
            keyboardDirection.boundingBoxOverride = false;


            xboxDirection= new FlxSprite(targetLeft, 0);
            xboxDirection.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/Map360"), true, false, 100, 100);
            xboxDirection.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            xboxDirection.play("frame");
            xboxDirection.solid = false;
            xboxDirection.visible = true;
            xboxDirection.scrollFactor.X = 1;
            xboxDirection.scrollFactor.Y = 1;
            xboxDirection.boundingBoxOverride = false;


            ouyaDirection = new FlxSprite(targetLeft, 0);
            ouyaDirection.loadGraphic(FlxG.Content.Load<Texture2D>("buttons/MapOuya"), true, false, 100, 100);
            ouyaDirection.addAnimation("frame", new int[] { FlxButton.ControlPadA });
            ouyaDirection.play("frame");
            ouyaDirection.solid = false;
            ouyaDirection.visible = true;
            ouyaDirection.scrollFactor.X = 1;
            ouyaDirection.scrollFactor.Y = 1;
            ouyaDirection.boundingBoxOverride = false;



            //add(_gamePadButton, true);

            timeToShowButton = float.MaxValue;
            _elapsedSinceLastButtonNeeded = 0.0f;

        }

        /// <summary>
        /// Sets the hud text
        /// </summary>
        /// <param name="player"></param>
        /// <param name="Data"></param>
        public void setHudText(int player, string Data)
        {
            if (Data == null)
                Data = "ERROR: NULL HUD MESSAGE";

            if (player == 1)
                p1HudText.text = Data;
            if (player == 2)
                p2HudText.text = Data;
            if (player == 3)
                p3HudText.text = Data;
            if (player == 4)
                p4HudText.text = Data;

        }

        /// <summary>
        /// Sets the non scaled gamePad button to an image and a position.
        /// </summary>
        /// <param name="Button">Use FlxButton.gamePad**</param>
        /// <param name="X">x pos</param>
        /// <param name="Y">y pos</param>
        public void setHudGamepadButton(int Button, float X, float Y)
        {
            _gamePadButton.frame = Button;
            _gamePadButton.x = X * FlxG.zoom ;
            _gamePadButton.y = Y * FlxG.zoom ;
            _gamePadButton.visible = true;
        }

        public void setHudGamepadButton(int Type, int Button, float X, float Y)
        {
            if (Type == TYPE_KEYBOARD)
            {
                keyboardButton.frame = Button;
                keyboardButton.x = X * FlxG.zoom;
                keyboardButton.y = Y * FlxG.zoom;
                keyboardButton.visible = true;
            }
            else if (Type == TYPE_XBOX)
            {
                xboxButton.frame = Button;
                xboxButton.x = X * FlxG.zoom;
                xboxButton.y = Y * FlxG.zoom;
                xboxButton.visible = true;
            }
            else if (Type == TYPE_OUYA)
            {
                ouyaButton.frame = Button;
                ouyaButton.x = X * FlxG.zoom;
                ouyaButton.y = Y * FlxG.zoom;
                ouyaButton.visible = true;
                
            }
            
        }

        /// <summary>
        /// Shows the Hud
        /// </summary>
        public void showHud()
        {
            visible = true;
            //_gamePadButton.visible = true;
        }

        /// <summary>
        /// Hides the Hud
        /// /// </summary>
        public void hideHud()
        {
            visible = false;

        }

        public void resetTime()
        {
            _elapsedSinceLastButtonNeeded = 0.0f;
        }
        /// <summary>
        /// Left over from FlxConsole
        /// </summary>
        public void update()
        {
            _elapsedSinceLastButtonNeeded += FlxG.elapsed;

            if ( _elapsedSinceLastButtonNeeded > timeToShowButton )
            {
                //hudGroup.members[0].visible = false;
                xboxButton.visible = false;
                keyboardButton.visible = false;
                ouyaButton.visible = false;

                xboxDirection.visible = false;
                keyboardDirection.visible = false;
                ouyaDirection.visible = false;
            }

            if (visible)
            {
                hudGroup.update();
            }
        }

        public void reset()
        {
            p1HudText.text = "";
            p1HudText.x = p1OriginalPosition.X;
            p1HudText.y = p1OriginalPosition.Y;
            p1HudText.scale = 1;

            p2HudText.text = "";
            p2HudText.x = p2OriginalPosition.X;
            p2HudText.y = p2OriginalPosition.Y;
            p2HudText.scale = 1;

            p3HudText.text = "";
            p3HudText.x = p3OriginalPosition.X;
            p3HudText.y = p3OriginalPosition.Y;
            p3HudText.scale = 1;

            p4HudText.text = "";
            p4HudText.x = p4OriginalPosition.X;
            p4HudText.y = p4OriginalPosition.Y;
            p4HudText.scale = 1;


            _gamePadButton.visible = false;
        }

        public void showHudGraphic()
        {
            //hudGraphic.visible = true;
        }
        public void hideHudGraphic()
        {
            //hudGraphic.visible = false;
        }


        public void render(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(FlxG.XnaSheet, _consoleRect, _srcRect, _consoleColor);

            hudGroup.render(spriteBatch);
            
            p1HudText.render(spriteBatch);
            p2HudText.render(spriteBatch);
            p3HudText.render(spriteBatch);
            p4HudText.render(spriteBatch);

            _gamePadButton.render(spriteBatch);

            keyboardButton.render(spriteBatch);
            ouyaButton.render(spriteBatch);
            xboxButton.render(spriteBatch);

            keyboardDirection.render(spriteBatch);
            ouyaDirection.render(spriteBatch);
            xboxDirection.render(spriteBatch);

        }

    }
}

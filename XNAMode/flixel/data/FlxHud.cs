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

        public FlxText p1HudText;
        public FlxText p2HudText;
        public FlxText p3HudText;
        public FlxText p4HudText;

        /// <summary>
        /// used to display a game pad button at regular resolution.
        /// </summary>
        private FlxSprite _gamePadButton;

        /// <summary>
        /// Original positions are used in the <code>reset()</code>
        /// method to reset positions of the text boxes if moved.
        /// </summary>
        private Vector2 p1OriginalPosition;
        private Vector2 p2OriginalPosition;
        private Vector2 p3OriginalPosition;
        private Vector2 p4OriginalPosition;

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
            _gamePadButton.boundingBoxOverride = false;

            //add(_gamePadButton, true);

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
        }

        /// <summary>
        /// Shows the Hud
        /// </summary>
        public void showHud()
        {
            visible = true;

        }

        /// <summary>
        /// Hides the Hud
        /// /// </summary>
        public void hideHud()
        {
            visible = false;

        }

        /// <summary>
        /// Left over from FlxConsole
        /// </summary>
        public void update()
        {
            if (visible)
            {

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

        public void render(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(FlxG.XnaSheet, _consoleRect, _srcRect, _consoleColor);

            p1HudText.render(spriteBatch);
            p2HudText.render(spriteBatch);
            p3HudText.render(spriteBatch);
            p4HudText.render(spriteBatch);

            _gamePadButton.render(spriteBatch);

        }

    }
}

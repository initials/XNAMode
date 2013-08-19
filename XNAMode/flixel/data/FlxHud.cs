using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace org.flixel
{
    /// <summary>
    /// Contains the hud that is non scaled.
    /// </summary>
    public class FlxHud
    {
        public bool visible = false;

        private Rectangle _srcRect = new Rectangle(1, 1, 1, 1);
        private Rectangle _consoleRect;
        private Color _consoleColor;

        private FlxText p1HudText;
        private FlxText p2HudText;
        private FlxText p3HudText;
        private FlxText p4HudText;

        private FlxSprite p1HudSprite;
        private FlxSprite p2HudSprite;
        private FlxSprite p3HudSprite;
        private FlxSprite p4HudSprite;



        public Color color
        {
            get { return _consoleColor; }
        }

        public FlxHud(int targetLeft, int targetWidth)
        {
            visible = false;
            p1HudText = new FlxText(targetLeft, 0, targetWidth, "p1HudText").setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            p2HudText = new FlxText(targetLeft, 0, targetWidth, "p2HudText").setFormat(null, 1, Color.White, FlxJustification.Right, Color.White);
            p3HudText = new FlxText(targetLeft, FlxG.height - 20, targetWidth, "p3HudText").setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            p4HudText = new FlxText(targetLeft, FlxG.height - 20, targetWidth, "p4HudText").setFormat(null, 1, Color.White, FlxJustification.Right, Color.White);           
            
        }

        /// <summary>
        /// 
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

        public void render(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(FlxG.XnaSheet, _consoleRect,
                _srcRect, _consoleColor);
            p1HudText.render(spriteBatch);
            p2HudText.render(spriteBatch);
            p3HudText.render(spriteBatch);
            p4HudText.render(spriteBatch);
        }

    }
}

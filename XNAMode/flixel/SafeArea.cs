#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

/// <summary>
/// A helper class to indicate the "safe area" for rendering to a 
/// television. The inner 90% of the viewport is the "action safe" area, 
/// meaning all important "action" should be shown within this area. The 
/// inner 80% of the viewport is the "title safe area", meaning all text 
/// and other key information should be shown within in this area. This 
/// class shows the area that is not "title safe" in yellow, and the area 
/// that is not "action safe" in red.
/// </summary>
public class SafeArea
{
    GraphicsDevice graphicsDevice;
    SpriteBatch spriteBatch;
    Texture2D tex; // Holds a 1x1 texture containing a single white texel
    int width; // Viewport width
    int height; // Viewport height
    int dx; // 5% of width
    int dy; // 5% of height
    Color notActionSafeColor = new Color(255, 0, 0, 127); // Red, 50% opacity
    Color notTitleSafeColor = new Color(255, 255, 0, 127); // Yellow, 50% opacity

    public void LoadGraphicsContent(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
        spriteBatch = new SpriteBatch(graphicsDevice);
        //tex = new Texture2D(graphicsDevice, 1, 1, 1, ResourceUsage.None, SurfaceFormat.Color);
        Color[] texData = new Color[1];
        texData[0] = Color.White;
        tex.SetData<Color>(texData);
        width = graphicsDevice.Viewport.Width;
        height = graphicsDevice.Viewport.Height;
        dx = (int)(width * 0.05);
        dy = (int)(height * 0.05);
    }

    public void Draw()
    {
        //spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

        // Tint the non-action-safe area red
        spriteBatch.Draw(tex, new Rectangle(0, 0, width, dy), notActionSafeColor);
        spriteBatch.Draw(tex, new Rectangle(0, height - dy, width, dy), notActionSafeColor);
        spriteBatch.Draw(tex, new Rectangle(0, dy, dx, height - 2 * dy), notActionSafeColor);
        spriteBatch.Draw(tex, new Rectangle(width - dx, dy, dx, height - 2 * dy), notActionSafeColor);

        // Tint the non-title-safe area yellow
        spriteBatch.Draw(tex, new Rectangle(dx, dy, width - 2 * dx, dy), notTitleSafeColor);
        spriteBatch.Draw(tex, new Rectangle(dx, height - 2 * dy, width - 2 * dx, dy), notTitleSafeColor);
        spriteBatch.Draw(tex, new Rectangle(dx, 2 * dy, dx, height - 4 * dy), notTitleSafeColor);
        spriteBatch.Draw(tex, new Rectangle(width - 2 * dx, 2 * dy, dx, height - 4 * dy), notTitleSafeColor);
        spriteBatch.End();
    }
}
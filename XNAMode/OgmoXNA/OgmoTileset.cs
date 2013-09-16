using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data about an Ogmo Editor tileset.
    /// </summary>
    public class OgmoTileset
    {
        List<Rectangle> sources = new List<Rectangle>();

        internal OgmoTileset(ContentReader reader)
        {
            this.Name = reader.ReadString();
            this.Texture = reader.ReadExternalReference<Texture2D>();
            this.TextureFile = reader.ReadString();
            this.TileHeight = reader.ReadInt32();
            this.TileWidth = reader.ReadInt32();
            if (this.Texture != null)
                this.CreateSources();
        }

        /// <summary>
        /// Gets the name of the tileset.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the rectangle sources for the tileset texture.
        /// </summary>
        public List<Rectangle> Sources
        {
            get { return sources; }
        }

        /// <summary>
        /// Gets the tileset texture.
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Gets the filename of the tileset texture, relative to 
        /// <see cref="OgmoProjectSettings.WorkingDirectory"/>.
        /// </summary>
        public string TextureFile { get; private set; }

        /// <summary>
        /// Gets the height (in pixels) of each tile texture in the tileset.
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// Gets the width (in pixels) of each tile texture in the tileset.
        /// </summary>
        public int TileWidth { get; private set; }

        internal void CreateSources()
        {
            for (int y = 0; y < this.Texture.Height; y += this.TileHeight)
                for (int x = 0; x < this.Texture.Width; x += this.TileWidth)
                    sources.Add(new Rectangle(x, y, this.TileWidth, this.TileHeight));
        }
    }
}

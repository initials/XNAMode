using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using OgmoXNA.Layers.Settings;

namespace OgmoXNA.Layers
{
    /// <summary>
    /// Contains data about an Ogmo Editor tile.
    /// </summary>
    public sealed class OgmoTile
    {
        internal OgmoTile(ContentReader reader, OgmoTileLayer layer)
        {
            this.Height = reader.ReadInt32();
            this.Position = reader.ReadVector2();
            this.SourceIndex = reader.ReadInt32();
            Vector2 offset = reader.ReadVector2();
            Point point = new Point((int)offset.X, (int)offset.Y);
            this.TextureOffset = point;
            string tilesetName = reader.ReadString();
            this.Tileset = layer.GetTileset(tilesetName);
            this.Width = reader.ReadInt32();
        }

        /// <summary>
        /// Gets the height (in pixels) of the tile.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the position of the tile in the layer.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Gets the <see cref="OgmoTileset"/> associated with the tile.
        /// </summary>
        public OgmoTileset Tileset { get; private set; }

        /// <summary>
        /// Gets the texture offset of the tile texture in its tileset.  Only valid if 
        /// <see cref="OgmoTileLayerSettings.ExportTileIDs"/> is <c>false</c>.
        /// </summary>
        public Point TextureOffset { get; private set; }

        /// <summary>
        /// Gets the rectangle source index of the tile texture in its tileset.  Only valid if 
        /// <see cref="OgmoTileLayerSettings.ExportTileIDs"/> is <c>true</c>.
        /// </summary>
        public int SourceIndex { get; private set; }

        /// <summary>
        /// Gets the width (in pixels) of the tile.
        /// </summary>
        public int Width { get; private set; }
    }
}

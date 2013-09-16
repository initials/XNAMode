using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoXNA.Layers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNA;

namespace OgmoXNADemo.Levels
{
    class Tile
    {
        OgmoTileset tileset;

        public Tile(OgmoTile tile, bool useSourceIndex)
        {
            this.Tint = Color.White;
            this.Position = tile.Position;
            tileset = tile.Tileset;
            this.Texture = tileset.Texture;
            if (useSourceIndex)
                this.Source = tileset.Sources[tile.SourceIndex];
            else
                this.Source = new Rectangle(tile.TextureOffset.X,
                    tile.TextureOffset.Y,
                    tileset.TileWidth,
                    tileset.TileHeight);
        }

        public Vector2 Position { get; set; }

        public Rectangle Source { get; set; }

        public Texture2D Texture { get; set; }

        public Color Tint { get; set; }
    }
}

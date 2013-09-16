using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OgmoXNA.Layers.Settings;

namespace OgmoXNA.Layers
{
    /// <summary>
    /// Contains data about an Ogmo Editor tile layer.
    /// </summary>
    public class OgmoTileLayer : OgmoLayer
    {
        List<OgmoTile> tiles = new List<OgmoTile>();
        Dictionary<string, OgmoTileset> tilesets = new Dictionary<string, OgmoTileset>();

        internal OgmoTileLayer(ContentReader reader, OgmoLevel level) 
            : base(reader)
        {            
            this.TileHeight = reader.ReadInt32();
            this.TileWidth = reader.ReadInt32();
            int tilesetCount = reader.ReadInt32();
            if (tilesetCount > 0)
            {
                for (int i = 0; i < tilesetCount; i++)
                {
                    string tilesetName = reader.ReadString();
                    OgmoTileset tileset = level.Project.GetTileset(tilesetName);
                    tilesets.Add(tileset.Name, tileset);
                }
            }
            int tileCount = reader.ReadInt32();
            if (tileCount > 0)
            {
                for (int i = 0; i < tileCount; i++)
                    tiles.Add(new OgmoTile(reader, this));
            }
        }

        /// <summary>
        /// Gets the height (in pixels) of tiles in the layer.  Only valid if 
        /// <see cref="OgmoTileLayerSettings.ExportTileSize"/> is <c>true</c>, and 
        /// <see cref="OgmoTileLayerSettings.MultipleTilesets"/> is <c>false</c>.
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// Gets the layer's tiles.
        /// </summary>
        public OgmoTile[] Tiles
        {
            get { return tiles.ToArray(); }
        }

        /// <summary>
        /// Gets the layer's tilesets.
        /// </summary>
        public OgmoTileset[] Tilesets
        {
            get { return tilesets.Values.ToArray<OgmoTileset>(); }
        }

        /// <summary>
        /// Gets the width (in pixels) of tiles in the layer.  Only valid if 
        /// <see cref="OgmoTileLayerSettings.ExportTileSize"/> is <c>true</c>, and 
        /// <see cref="OgmoTileLayerSettings.MultipleTilesets"/> is <c>false</c>.
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// Gets the specified tileset.
        /// </summary>
        /// <param name="name">The name of the tileset.</param>
        /// <returns>Returns the specified tileset if it exists; otherwise, <c>null</c>.</returns>
        public OgmoTileset GetTileset(string name)
        {
            OgmoTileset tileset = null;
            if (tilesets.TryGetValue(name, out tileset))
                return tileset;
            return null;
        }
    }
}

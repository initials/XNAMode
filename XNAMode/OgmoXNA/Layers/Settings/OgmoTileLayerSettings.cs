using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Layers.Settings
{
    /// <summary>
    /// Contains settings for an Ogmo Editor tile layer.
    /// </summary>
    public sealed class OgmoTileLayerSettings : OgmoLayerSettings
    {
        internal OgmoTileLayerSettings(ContentReader reader)
            : base(reader)
        {
            this.ExportTileIDs = reader.ReadBoolean();
            this.ExportTileSize = reader.ReadBoolean();
            this.MultipleTilesets = reader.ReadBoolean();
        }

        /// <summary>
        /// Gets whether to export the the source texture ID of each tile (true) or to export the 
        /// texture offset (false).
        /// </summary>
        public bool ExportTileIDs { get; private set; }

        /// <summary>
        /// Gets whether to export the size of the layer's tiles.  If <see cref="MultipleTilesets"/> is 
        /// <c>true</c> then each tile will hold this data; otherwise, the layer will.
        /// </summary>
        public bool ExportTileSize { get; private set; }

        /// <summary>
        /// Gets whether the layer can use multiple tilesets.
        /// </summary>
        public bool MultipleTilesets { get; private set; }
    }
}

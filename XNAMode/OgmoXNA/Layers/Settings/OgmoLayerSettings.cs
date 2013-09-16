using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OgmoXNA.Layers.Settings
{
    /// <summary>
    /// Contains settings for an Ogmo Editor layer.
    /// </summary>
    public abstract class OgmoLayerSettings
    {
        internal OgmoLayerSettings(ContentReader reader)
        {
            this.GridColor = reader.ReadColor();
            this.GridDrawSize = reader.ReadInt32();
            this.GridSize = reader.ReadInt32();
            this.Name = reader.ReadString();
        }

        /// <summary>
        /// Gets the color of the layer's grid.  Cosmetic only.
        /// </summary>
        public Color GridColor { get; private set; }

        /// <summary>
        /// Gets the cell size of the layer's grid when rendering.  Cosmetic only.
        /// </summary>
        public int GridDrawSize { get; private set; }

        /// <summary>
        /// Gets the cell size of the layer's grid.
        /// </summary>
        public int GridSize { get; private set; }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        public string Name { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNA.Layers.Settings;

namespace OgmoXNA.Layers
{
    /// <summary>
    /// Contains data about an Ogmo Editor layer.
    /// </summary>
    public abstract class OgmoLayer
    {
        internal OgmoLayer(ContentReader reader)
        {
            this.Name = reader.ReadString();
        }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        public string Name { get; private set; }
    }
}

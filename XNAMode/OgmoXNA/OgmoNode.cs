using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data about an Ogmo Editor object node.
    /// </summary>
    public class OgmoNode
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoNode"/>.
        /// </summary>
        /// <param name="position">The position of the node.</param>
        public OgmoNode(Vector2 position)
        {
            this.Position = position;
        }

        internal OgmoNode(ContentReader reader)
        {
            this.Position = reader.ReadVector2();
        }

        /// <summary>
        /// Gets or sets the position of the node.
        /// </summary>
        public Vector2 Position { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data for an Ogmo Editor value object.
    /// </summary>
    public abstract class OgmoValue
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoValue"/>.
        /// </summary>
        protected OgmoValue()
        {
        }

        internal OgmoValue(ContentReader reader)
        {
            this.Name = reader.ReadString();
        }

        /// <summary>
        /// Gets the name of the value.
        /// </summary>
        public string Name { get; private set; }
    }
}

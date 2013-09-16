using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data for an Ogmo Editor string value object.
    /// </summary>
    public class OgmoStringValue : OgmoValue<string>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoStringValue"/>.
        /// </summary>
        public OgmoStringValue()
        {
        }

        internal OgmoStringValue(ContentReader reader)
            : base(reader)
        {
            this.Value = reader.ReadString();
        }
    }
}

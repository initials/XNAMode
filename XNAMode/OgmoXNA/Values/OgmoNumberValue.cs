using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data for an Ogmo Editor number (float) value object.
    /// </summary>
    public class OgmoNumberValue : OgmoValue<float>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoNumberValue"/>.
        /// </summary>
        public OgmoNumberValue()
        {
        }

        internal OgmoNumberValue(ContentReader reader)
            : base(reader)
        {
            this.Value = reader.ReadSingle();
        }
    }
}

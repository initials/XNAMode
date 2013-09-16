using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data for an Ogmo Editor integer value object.
    /// </summary>
    public class OgmoIntegerValue : OgmoValue<int>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoIntegerValue"/>.
        /// </summary>
        public OgmoIntegerValue()
        {
        }

        internal OgmoIntegerValue(ContentReader reader)
            : base(reader)
        {
            this.Value = reader.ReadInt32();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data for an Ogmo Editor boolean value.
    /// </summary>
    public class OgmoBooleanValue : OgmoValue<bool>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoBooleanValue"/>.
        /// </summary>
        public OgmoBooleanValue()
        {
        }

        internal OgmoBooleanValue(ContentReader reader)
            : base(reader)
        {
            this.Value = reader.ReadBoolean();
        }
    }
}

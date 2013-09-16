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
    public class OgmoIntegerValueTemplate : OgmoValueTemplate<int>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoIntegerValue"/>.
        /// </summary>
        public OgmoIntegerValueTemplate() 
            : base()
        {
        }

        internal OgmoIntegerValueTemplate(ContentReader reader)
            : base(reader)
        {
            this.Default = reader.ReadInt32();
            this.Max = reader.ReadInt32();
            this.Min = reader.ReadInt32();
        }

        /// <summary>
        /// Gets the max integer value.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Gets the min integer value.
        /// </summary>
        public int Min { get; private set; }
    }
}

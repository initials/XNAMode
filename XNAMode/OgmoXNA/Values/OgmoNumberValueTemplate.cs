using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data about Ogmo Editor number (float) value objects.
    /// </summary>
    public class OgmoNumberValueTemplate : OgmoValueTemplate<float>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoNumberValue"/>.
        /// </summary>
        public OgmoNumberValueTemplate()
        {
        }

        internal OgmoNumberValueTemplate(ContentReader reader)
            : base(reader)
        {
            this.Default = reader.ReadSingle();
            this.Max = reader.ReadSingle();
            this.Min = reader.ReadSingle();
        }

        /// <summary>
        /// Gets the max number (float) value.
        /// </summary>
        public float Max { get; private set; }

        /// <summary>
        /// Gets the min number (float) value.
        /// </summary>
        public float Min { get; private set; }
    }
}

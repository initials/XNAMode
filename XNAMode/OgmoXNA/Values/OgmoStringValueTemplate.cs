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
    public class OgmoStringValueTemplate : OgmoValueTemplate<string>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoStringValue"/>.
        /// </summary>
        public OgmoStringValueTemplate()
        {
        }

        internal OgmoStringValueTemplate(ContentReader reader)
            : base(reader)
        {
            this.Default = reader.ReadString();
            this.MaxChars = reader.ReadInt32();
        }

        /// <summary>
        /// Gets the max number of chars in the string value.
        /// </summary>
        public int MaxChars { get; private set; }
    }
}

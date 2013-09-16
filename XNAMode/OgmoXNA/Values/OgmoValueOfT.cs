using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains strongly-typed data for an Ogmo Editor value object.
    /// </summary>
    /// <typeparam name="T">The value type of the object.</typeparam>
    public abstract class OgmoValue<T> : OgmoValue
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoValue"/>.
        /// </summary>
        protected OgmoValue()
        {
        }

        internal OgmoValue(ContentReader reader)
            : base(reader)
        {
        }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        public T Value { get; protected set; }
    }
}

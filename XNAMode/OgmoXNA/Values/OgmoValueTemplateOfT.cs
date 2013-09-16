using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains strongly-typed data for an Ogmo Editor value object.
    /// </summary>
    /// <typeparam name="T">The value type of the object.</typeparam>
    public abstract class OgmoValueTemplate<T> : OgmoValueTemplate
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoValue"/>.
        /// </summary>
        protected OgmoValueTemplate()
        {
        }

        internal OgmoValueTemplate(ContentReader reader)
            : base(reader)
        {
        }

        /// <summary>
        /// Gets the default value of the object.
        /// </summary>
        public T Default { get; protected set; }
    }
}

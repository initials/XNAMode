using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    /// <summary>
    /// Contains data about Ogmo Editor boolean value objects.
    /// </summary>
    public class OgmoBooleanValueTemplate : OgmoValueTemplate<bool>
    {
        /// <summary>
        /// Creates an instance of <see cref="OgmoBooleanValue"/>.
        /// </summary>
        public OgmoBooleanValueTemplate() 
            : base()
        {
        }

        internal OgmoBooleanValueTemplate(ContentReader reader)
            : base(reader)
        {
            this.Default = reader.ReadBoolean();
        }
    }
}

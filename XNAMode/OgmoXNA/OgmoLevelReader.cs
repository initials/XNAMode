using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoXNA
{
    /// <summary>
    /// Reads <see cref="OgmoLevel"/> objects from the content pipeline.
    /// </summary>
    public class OgmoLevelReader : ContentTypeReader<OgmoLevel>
    {
        /// <summary>
        /// Reads binary data into an <see cref="OgmoLevel"/> object.
        /// </summary>
        /// <param name="input">The <see cref="ContentReader"/> to read from.</param>
        /// <param name="existingInstance">An existing instance of <see cref="OgmoLevel"/>.</param>
        /// <returns>Returns a configured <see cref="OgmoLevel"/> object.</returns>
        protected override OgmoLevel Read(ContentReader input, OgmoLevel existingInstance)
        {
            return new OgmoLevel(input);
        }
    }
}

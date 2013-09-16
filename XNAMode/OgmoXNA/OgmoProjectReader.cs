using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoXNA
{
    /// <summary>
    /// Reads <see cref="OgmoProject"/> objects from the content pipeline.
    /// </summary>
    public class OgmoProjectReader : ContentTypeReader<OgmoProject>
    {
        /// <summary>
        /// Reads binary data into an <see cref="OgmoProject"/> object.
        /// </summary>
        /// <param name="input">The <see cref="ContentReader"/> to read from.</param>
        /// <param name="existingInstance">An existing instance of <see cref="OgmoProject"/>.</param>
        /// <returns>Returns a configured <see cref="OgmoLevel"/> object.</returns>
        protected override OgmoProject Read(ContentReader input, OgmoProject existingInstance)
        {
            return new OgmoProject(input);
        }
    }
}

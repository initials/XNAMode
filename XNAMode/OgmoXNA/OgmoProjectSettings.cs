using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA
{
    /// <summary>
    /// Contains settings for an Ogmo Editor project.
    /// </summary>
    public class OgmoProjectSettings
    {
        internal OgmoProjectSettings(ContentReader reader)
        {
            this.Height = reader.ReadInt32();
            this.MaxHeight = reader.ReadInt32();
            this.MaxWidth = reader.ReadInt32();
            this.MinHeight = reader.ReadInt32();
            this.MinWidth = reader.ReadInt32();
            this.Width = reader.ReadInt32();
            this.WorkingDirectory = reader.ReadString();
        }

        /// <summary>
        /// Gets the default height (in pixels) of project levels.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the max height (in pixels) of project levels.
        /// </summary>
        public int MaxHeight { get; private set; }

        /// <summary>
        /// Gets the max width (in pixels) of project levels.
        /// </summary>
        public int MaxWidth { get; private set; }

        /// <summary>
        /// Gets the min height (in pixels) of project levels.
        /// </summary>
        public int MinHeight { get; private set; }

        /// <summary>
        /// Gets the min width (in pixels) of project levels.
        /// </summary>
        public int MinWidth { get; private set; }

        /// <summary>
        /// Gets the default width (in pixels) of project levels.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the working directory of project assets relative to the project file.
        /// </summary>
        public string WorkingDirectory { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNA.Values;
using System.IO;
using System.Xml.Serialization;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data about an Ogmo Editor object.
    /// </summary>
    public class OgmoObjectTemplate
    {
        Dictionary<string, OgmoValueTemplate> values = new Dictionary<string, OgmoValueTemplate>();

        /// <summary>
        /// Creates an instance of <see cref="OgmoObjectTemplate"/>.
        /// </summary>
        public OgmoObjectTemplate()
        {
        }

        internal OgmoObjectTemplate(ContentReader reader)
        {
            this.Height = reader.ReadInt32();
            this.IsResizableX = reader.ReadBoolean();
            this.IsResizableX = reader.ReadBoolean();
            this.IsTiled = reader.ReadBoolean();
            this.Name = reader.ReadString();
            this.Origin = reader.ReadVector2();
            Rectangle source = Rectangle.Empty;
            source.X = reader.ReadInt32();
            source.Y = reader.ReadInt32();
            source.Width = reader.ReadInt32();
            source.Height = reader.ReadInt32();
            this.Source = source;
            this.Texture = reader.ReadExternalReference<Texture2D>();
            this.TextureFile = reader.ReadString();
            this.Width = reader.ReadInt32();
            // Values
            int valueCount = reader.ReadInt32();
            if (valueCount > 0)
            {
                for (int i = 0; i < valueCount; i++)
                {
                    OgmoValueTemplate value = OgmoValueTemplateReader.Read(reader);
                    if (value != null)
                        values.Add(value.Name, value);
                }
            }
        }

        /// <summary>
        /// Gets the height (in pixels) of the object.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets whether the object can be resized horizontally.
        /// </summary>
        public bool IsResizableX { get; private set; }

        /// <summary>
        /// Gets whether the object can be resized vertically.
        /// </summary>
        public bool IsResizableY { get; private set; }

        /// <summary>
        /// Gets whether the object should tile its texture if resized along an axis.
        /// </summary>
        public bool IsTiled { get; private set; }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the origin of the object.
        /// </summary>
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// Gets the texture source rectangle of the object.
        /// </summary>
        public Rectangle Source { get; private set; }

        /// <summary>
        /// Gets the filename of the object's texture.
        /// </summary>
        public string TextureFile { get; private set; }

        /// <summary>
        /// Gets the object's texture.
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Gets the object's values.
        /// </summary>
        public OgmoValueTemplate[] Values
        {
            get { return values.Values.ToArray<OgmoValueTemplate>(); }
        }

        /// <summary>
        /// Gets the width (in pixels) of the object.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the specified object value template.
        /// </summary>
        /// <typeparam name="T">The type of value template to retrieve.</typeparam>
        /// <param name="name">The name of the value template.</param>
        /// <returns>Returns the specified value template if found; otherwise, <c>null</c>.</returns>
        public T GetValueTemplate<T>(string name) where T : OgmoValueTemplate
        {
            OgmoValueTemplate valueTemplate = null;
            if (values.TryGetValue(name, out valueTemplate))
                return valueTemplate as T;
            return null;
        }
    }
}

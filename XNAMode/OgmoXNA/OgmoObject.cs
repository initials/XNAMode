using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OgmoXNA.Values;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data for an Ogmo Editor object.
    /// </summary>
    public class OgmoObject
    {
        List<OgmoNode> nodes = new List<OgmoNode>();
        Dictionary<string, OgmoValue> values = new Dictionary<string, OgmoValue>();

        internal OgmoObject(ContentReader reader)
        {
            this.Name = reader.ReadString();
            this.Origin = reader.ReadVector2();
            this.Position = reader.ReadVector2();
            this.Rotation = reader.ReadSingle();
            this.Width = reader.ReadInt32();
            this.Height = reader.ReadInt32();
            Rectangle source = Rectangle.Empty;
            source.X = reader.ReadInt32();
            source.Y = reader.ReadInt32();
            source.Width = reader.ReadInt32();
            source.Height = reader.ReadInt32();
            this.Source = source;
            this.IsTiled = reader.ReadBoolean();
            this.Texture = reader.ReadExternalReference<Texture2D>();
            int valueCount = reader.ReadInt32();
            if (valueCount > 0)
            {
                for (int i = 0; i < valueCount; i++)
                {
                    OgmoValue value = OgmoValueReader.Read(reader);
                    if (value != null)
                        values.Add(value.Name, value);
                }
            }
            int nodeCount = reader.ReadInt32();
            if (nodeCount > 0)
            {
                for (int i = 0; i < nodeCount; i++)
                    nodes.Add(new OgmoNode(reader));
            }
        }

        /// <summary>
        /// Gets the height (in pixels) of the object.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets whether the object's texture should be tiled or stretched if its source rectangle is larger 
        /// than its texture.
        /// </summary>
        public bool IsTiled { get; private set; }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the object's nodes.
        /// </summary>
        public OgmoNode[] Nodes
        {
            get { return nodes.ToArray(); }
        }

        /// <summary>
        /// Gets the origin of the object.
        /// </summary>
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// Gets the position of the object.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Gets the rotation angle of the object.
        /// </summary>
        public float Rotation { get; private set; }

        /// <summary>
        /// Gets the source rectangle of the object.
        /// </summary>
        public Rectangle Source { get; private set; }

        /// <summary>
        /// Gets the texture of the object.
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Gets the object's values.
        /// </summary>
        public OgmoValue[] Values
        {
            get { return values.Values.ToArray<OgmoValue>(); }
        }

        /// <summary>
        /// Gets the width (in pixels) of the object.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="name">The name of the value.</param>
        /// <returns>Returns the specified value if it exists; otherwise <c>null</c>.</returns>
        public T GetValue<T>(string name) where T : OgmoValue
        {
            OgmoValue value = null;
            if (values.TryGetValue(name, out value))
                return value as T;
            return null;
        }
    }
}

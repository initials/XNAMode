using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OgmoXNA.Layers;
using OgmoXNA.Values;
using OgmoXNA.Layers.Settings;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data about an Ogmo Editor level.
    /// </summary>
    public class OgmoLevel
    {
        Dictionary<string, OgmoLayer> layers = new Dictionary<string, OgmoLayer>();
        Dictionary<string, OgmoValue> values = new Dictionary<string, OgmoValue>();

        internal OgmoLevel(ContentReader reader)
        {
            // Project
            this.Project = reader.ReadExternalReference<OgmoProject>();
            // Values
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
            // Height
            this.Height = reader.ReadInt32();
            // Width
            this.Width = reader.ReadInt32();
            // Layers
            int layerCount = reader.ReadInt32();
            if (layerCount > 0)
            {
                for (int i = 0; i < layerCount; i++)
                {
                    OgmoLayer layer = OgmoLayerReader.Read(reader, this);
                    if (layer != null)
                        this.layers.Add(layer.Name, layer);
                }
            }
        }

        /// <summary>
        /// Gets the height (in pixels) of the level.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the level's layers.
        /// </summary>
        public OgmoLayer[] Layers
        {
            get { return layers.Values.ToArray<OgmoLayer>(); }
        }

        /// <summary>
        /// Gets the <see cref="OgmoProject"/> associated with the level.
        /// </summary>
        public OgmoProject Project { get; private set; }

        /// <summary>
        /// Gets the level's value objects.
        /// </summary>
        public OgmoValue[] Values
        {
            get { return values.Values.ToArray<OgmoValue>(); }
        }

        /// <summary>
        /// Gets the width (in pixels) of the level.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the specified level layer.
        /// </summary>
        /// <typeparam name="T">The type of layer to retrieve.</typeparam>
        /// <param name="name">The name of the layer.</param>
        /// <returns>Returns the specified layer if found; otherwise, <c>null</c>.</returns>
        public T GetLayer<T>(string name) where T : OgmoLayer
        {
            OgmoLayer value = null;
            if (layers.TryGetValue(name, out value))
                return value as T;
            return null;
        }        

        /// <summary>
        /// Gets the specified level value.
        /// </summary>
        /// <typeparam name="T">The type of value to retrieve.</typeparam>
        /// <param name="name">The name of the value.</param>
        /// <returns>Returns the specified value if found; otherwise, <c>null</c>.</returns>
        public T GetValue<T>(string name) where T : OgmoValue
        {
            OgmoValue value = null;
            if (values.TryGetValue(name, out value))
                return value as T;
            return null;
        }
    }
}

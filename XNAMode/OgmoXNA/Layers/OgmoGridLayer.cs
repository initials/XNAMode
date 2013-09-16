using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using OgmoXNA.Layers.Settings;
using System.IO;
using System.IO.Compression;
using System.Globalization;

namespace OgmoXNA.Layers
{
    /// <summary>
    /// Contains data about an Ogmo Editor grid layer.
    /// </summary>
    public sealed class OgmoGridLayer : OgmoLayer
    {
        int[,] rawData;
        List<Rectangle> rectData;

        internal OgmoGridLayer(ContentReader reader, OgmoLevel level)
            : base(reader)
        {
            OgmoGridLayerSettings settings = level.Project.GetLayerSettings<OgmoGridLayerSettings>(this.Name);
            if (settings.ExportAsObjects)
            {
                rectData = new List<Rectangle>();
                int objectCount = reader.ReadInt32();
                if (objectCount > 0)
                {
                    for (int i = 0; i < objectCount; i++)
                    {
                        Rectangle rect = Rectangle.Empty;
                        rect.X = reader.ReadInt32();
                        rect.Y = reader.ReadInt32();
                        rect.Width = reader.ReadInt32();
                        rect.Height = reader.ReadInt32();
                        rectData.Add(rect);
                    }
                }
            }
            else
            {
                byte[] data = Convert.FromBase64String(reader.ReadString());
                string stringData = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                int tx = level.Width / settings.GridSize;
                int ty = level.Height / settings.GridSize;
                rawData = new int[tx, ty];
                for (int y = 0; y < ty; y++)
                    for (int x = 0; x < tx; x++)
                        rawData[x, y] = int.Parse(stringData[y * tx + x].ToString(), CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the raw grid data for the layer.  This property is only populated when 
        /// <see cref="OgmoGridLayerSettings.ExportAsObjects"/> is <c>false</c>; otherwise, it returns <c>null</c>.
        /// </summary>
        public int[,] RawData
        {
            get { return rawData; }
        }

        /// <summary>
        /// Gets the rectangle data for the layer.  This propery is only populated when 
        /// <see cref="OgmoGridLayerSettings.ExportAsObjects"/> is <c>true</c>; otherwise, it returns <c>null</c>.
        /// </summary>
        public Rectangle[] RectangleData
        {
            get { return rectData.ToArray(); }
        }
    }
}

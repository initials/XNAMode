using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Layers
{
    static class OgmoLayerReader
    {
        internal static OgmoLayer Read(ContentReader reader, OgmoLevel level)
        {
            OgmoLayer layer = null;
            string type = reader.ReadString();
            switch (type)
            {
                case "g":
                    layer = new OgmoGridLayer(reader, level);
                    break;
                case "t":
                    layer = new OgmoTileLayer(reader, level);
                    break;
                case "o":
                    layer = new OgmoObjectLayer(reader, level);
                    break;
            }
            return layer;
        }
    }
}

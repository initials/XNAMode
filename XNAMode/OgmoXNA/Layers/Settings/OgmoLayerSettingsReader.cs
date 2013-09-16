using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Layers.Settings
{
    static class OgmoLayerSettingsReader
    {
        internal static OgmoLayerSettings Read(ContentReader reader)
        {
            OgmoLayerSettings layerSettings = null;
            string settingsType = reader.ReadString();
            switch (settingsType)
            {
                case "g":
                    layerSettings = new OgmoGridLayerSettings(reader);
                    break;
                case "o":
                    layerSettings = new OgmoObjectLayerSettings(reader);
                    break;
                case "t":
                    layerSettings = new OgmoTileLayerSettings(reader);
                    break;
            }
            return layerSettings;
        }
    }
}

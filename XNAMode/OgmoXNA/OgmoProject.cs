using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OgmoXNA.Layers.Settings;
using OgmoXNA.Values;

namespace OgmoXNA
{
    /// <summary>
    /// Contains data about an Ogmo Editor project.
    /// </summary>
    public class OgmoProject
    {
        Dictionary<string, OgmoLayerSettings> layerSettings = new Dictionary<string, OgmoLayerSettings>();
        Dictionary<string, OgmoObjectTemplate> objects = new Dictionary<string, OgmoObjectTemplate>();
        Dictionary<string, OgmoTileset> tilesets = new Dictionary<string, OgmoTileset>();
        Dictionary<string, OgmoValueTemplate> values = new Dictionary<string, OgmoValueTemplate>();

        internal OgmoProject(ContentReader reader)
        {
            // Name
            this.Name = reader.ReadString();
            // Settings
            this.Settings = new OgmoProjectSettings(reader);
            // Values
            int valueCount = reader.ReadInt32();
            if (valueCount > 0)
            {
                for (int i = 0; i < valueCount; i++)
                {
                    OgmoValueTemplate ogmoValue = OgmoValueTemplateReader.Read(reader);
                    if (ogmoValue != null)
                        values.Add(ogmoValue.Name, ogmoValue);
                }
            }
            // Tilesets
            int tilesetCount = reader.ReadInt32();
            if (tilesetCount > 0)
            {
                for (int i = 0; i < tilesetCount; i++)
                {
                    OgmoTileset tileset = new OgmoTileset(reader);
                    if (tileset != null)
                        tilesets.Add(tileset.Name, tileset);
                }
            }
            // Objects
            int objectCount = reader.ReadInt32();
            if (objectCount > 0)
            {
                for (int i = 0; i < objectCount; i++)
                {
                    OgmoObjectTemplate obj = new OgmoObjectTemplate(reader);
                    if (obj != null)
                        objects.Add(obj.Name, obj);
                }
            }
            // Layer Settings
            int layerSettingsCount = reader.ReadInt32();
            if (layerSettingsCount > 0)
            {
                for (int i = 0; i < layerSettingsCount; i++)
                {
                    OgmoLayerSettings layerSettings = OgmoLayerSettingsReader.Read(reader);
                    if (layerSettings != null)
                        this.layerSettings.Add(layerSettings.Name, layerSettings);
                }
            }
        }

        /// <summary>
        /// Gets the project layer settings.
        /// </summary>
        public OgmoLayerSettings[] LayerSettings
        {
            get { return layerSettings.Values.ToArray<OgmoLayerSettings>(); }
        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the project objects.
        /// </summary>
        public OgmoObjectTemplate[] ObjectTemplates
        {
            get { return objects.Values.ToArray<OgmoObjectTemplate>(); }
        }

        /// <summary>
        /// Gets the settings for the project.
        /// </summary>
        public OgmoProjectSettings Settings { get; private set; }

        /// <summary>
        /// Gets the project tilesets.
        /// </summary>
        public OgmoTileset[] Tilesets
        {
            get { return tilesets.Values.ToArray<OgmoTileset>(); }
        }

        /// <summary>
        /// Gets the project values.
        /// </summary>
        public OgmoValueTemplate[] ValueTemplates
        {
            get { return values.Values.ToArray<OgmoValueTemplate>(); }
        }

        /// <summary>
        /// Gets the specified layer settings.
        /// </summary>
        /// <typeparam name="T">The type of layer settings to retrieve.</typeparam>
        /// <param name="name">The name of the layer settings.</param>
        /// <returns>Returns the specified layer settings if found; otherwise, <c>null</c>.</returns>
        public T GetLayerSettings<T>(string name) where T : OgmoLayerSettings
        {
            OgmoLayerSettings value = null;
            if (layerSettings.TryGetValue(name, out value))
                return value as T;
            return null;
        }

        /// <summary>
        /// Gets the specified object template.
        /// </summary>
        /// <param name="name">The name of the object template.</param>
        /// <returns>Returns the specified object template if found; otherwise, <c>null</c>.</returns>
        public OgmoObjectTemplate GetObjectTemplate(string name)
        {
            OgmoObjectTemplate objectTemplate = null;
            if (objects.TryGetValue(name, out objectTemplate))
                return objectTemplate;
            return null;
        }

        /// <summary>
        /// Gets the specified tileset.
        /// </summary>
        /// <param name="name">The name of the tileset.</param>
        /// <returns>Returns the specified tileset if found; otherwise, <c>null</c>.</returns>
        public OgmoTileset GetTileset(string name)
        {
            OgmoTileset value = null;
            if (tilesets.TryGetValue(name, out value))
                return value;
            return null;
        }

        /// <summary>
        /// Gets the specified project value template.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using OgmoXNA.Values;

namespace OgmoXNA.Layers
{
    /// <summary>
    /// Contains data about an Ogmo Editor object layer.
    /// </summary>
    public sealed class OgmoObjectLayer : OgmoLayer
    {
        Dictionary<string, List<OgmoObject>> objects = new Dictionary<string, List<OgmoObject>>();
        List<OgmoObject> allObjects = new List<OgmoObject>();

        internal OgmoObjectLayer(ContentReader reader, OgmoLevel level)
            : base(reader)
        {
            int objectCount = reader.ReadInt32();
            if (objectCount > 0)
            {
                for (int i = 0; i < objectCount; i++)
                {
                    OgmoObject obj = new OgmoObject(reader);
                    if(obj != null)
                    if (objects.ContainsKey(obj.Name))
                        objects[obj.Name].Add(obj);
                    else
                        objects.Add(obj.Name, new List<OgmoObject>() { obj });
                    allObjects.Add(obj);
                }
            }
        }

        /// <summary>
        /// Gets all of the layer's objects.
        /// </summary>
        public OgmoObject[] Objects
        {
            get { return allObjects.ToArray(); }
        }

        /// <summary>
        /// Gets the first found object with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>Returns the first found object with the specified name; otherwise, <c>null</c>.</returns>
        public OgmoObject GetObject(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            List<OgmoObject> collection = null;
            if (objects.TryGetValue(name, out collection))
                return collection.First<OgmoObject>((x) =>
                {
                    return x.Name.Equals(name);
                });
            return null;
        }

        /// <summary>
        /// Gets all objects with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>Returns an array of objects with the specified name.</returns>
        public OgmoObject[] GetObjects(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            List<OgmoObject> collection = null;
            if (objects.TryGetValue(name, out collection))
                return collection.ToArray();
            return null;
        }
    }
}

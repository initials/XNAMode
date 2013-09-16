using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    static class OgmoValueReader
    {
        internal static OgmoValue Read(ContentReader reader)
        {
            OgmoValue value = null;
            string valueType = reader.ReadString();
            switch (valueType)
            {
                case "b":
                    value = new OgmoBooleanValue(reader);
                    break;
                case "i":
                    value = new OgmoIntegerValue(reader);
                    break;
                case "n":
                    value = new OgmoNumberValue(reader);
                    break;
                case "s":
                    value = new OgmoStringValue(reader);
                    break;
            }
            return value;
        }
    }
}

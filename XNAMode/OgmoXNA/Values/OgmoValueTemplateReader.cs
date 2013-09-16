using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace OgmoXNA.Values
{
    static class OgmoValueTemplateReader
    {
        internal static OgmoValueTemplate Read(ContentReader reader)
        {
            OgmoValueTemplate value = null;
            string valueType = reader.ReadString();
            switch (valueType)
            {
                case "b":
                    value = new OgmoBooleanValueTemplate(reader);
                    break;
                case "i":
                    value = new OgmoIntegerValueTemplate(reader);
                    break;
                case "n":
                    value = new OgmoNumberValueTemplate(reader);
                    break;
                case "s":
                    value = new OgmoStringValueTemplate(reader);
                    break;
            }
            return value;
        }
    }
}

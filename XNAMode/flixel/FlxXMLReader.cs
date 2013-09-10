using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Linq;
using System.Xml.Linq;

namespace org.flixel
{
    /// <summary>
    /// FlxXMLReader will read an XML file.
    /// Eventually will include levels from Tiled, Ogmo.
    /// </summary>
    public class FlxXMLReader
    {
        /// <summary>
        /// Reads a custom XML document
        /// FlxXMLReader.readCustomXMLLevelsAttrs("levelSettings.xml");
        /// </summary>
        /// <param name="filename">The file to read. e.g. levelSettings.xml</param>
        /// <returns>A string, string Dictionary of settings and values.</returns>
        public static Dictionary<string, string> readCustomXMLLevelsAttrs(string filename)
        {

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            string currentLevel = "l" + FlxG.level.ToString();

            XElement xelement = XElement.Load(filename);

            foreach (XElement xEle in xelement.Descendants("settings").Elements())
            {
                XElement firstSpecificChildElement = xEle.Element(currentLevel);
                if (firstSpecificChildElement != null)
                {
                    if (firstSpecificChildElement.Value.ToString() == "")
                    {
                        levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");
                        if (playerControlled != null)
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }
                    }
                    else
                    {
                        levelAttrs.Add(xEle.Name.ToString(), firstSpecificChildElement.Value.ToString());
                        XAttribute playerControlled = firstSpecificChildElement.Attribute("playerControlled");
                        if (playerControlled != null)
                        {
                            levelAttrs.Add("playerControlled", xEle.Name.ToString());
                        }
                    }
                }
                else
                {
                    levelAttrs.Add(xEle.Name.ToString(), xEle.Attribute("default").Value.ToString());
                }
            }

            return levelAttrs;
        }





    }

}
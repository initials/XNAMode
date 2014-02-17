﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace org.flixel
{
    /// <summary>
    /// FlxXMLReader will read an XML file.
    /// Eventually will include levels from Tiled, Ogmo.
    /// </summary>
    public class FlxXMLReader
    {

        public const int TILES = 0;
        public const int ACTORS = 1;

        public static void readOgmoProjectAndLevel(string projectFilename, string levelFilename)
        {
            XDocument project = XDocument.Load(projectFilename);
            foreach (XElement xEle in project.Descendants("LayerDefinitions").Elements("LayerDefinition"))
            {
                Console.WriteLine(xEle.Descendants("Name").ToString() );
            }
        }

        public static List<Dictionary<string, string>> readOgmoV2Level(string filename)
        {
            List<Dictionary<string, string>> completeSet = new List<Dictionary<string, string>>();

            string currentLevel = "l" + FlxG.level.ToString();

            XDocument xdoc = XDocument.Load(filename);

            // Load level main stats.
            XElement xelement = XElement.Load(filename);
            IEnumerable<XAttribute> attList =
            from at in xelement.Attributes()
            select at;

            foreach (XAttribute xAttr in attList)
            {
                Console.WriteLine(xAttr.Name.ToString() + "  " + xAttr.Value.ToString());
                //levelAttrs.Add(xAttr.Name.ToString(), xAttr.Value.ToString());
            }

            foreach (XElement xEle in xdoc.Descendants("level").Elements())
            {
                Console.WriteLine(xEle.Name.ToString());
            }
            return completeSet;
        }

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

        /// <summary>
        /// Returns the attributes on the node/element specified.
        /// NOTE: Test for Android.
        /// </summary>
        /// <param name="filename">Filename, example: "ogmoLevels/level1.oel"</param>
        /// <param name="element">Element from XML file, example: "level/ActorsLayer" or "level"</param>
        /// <returns></returns>
        public static Dictionary<string, string> readAttributesFromOelFile(string filename, string element)
        {

            XmlDocument xml = new XmlDocument();
            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

			#if __ANDROID__

			string content;
			using (StreamReader sr = new StreamReader (Game.Activity.Assets.Open(filename)))
			{
				content = sr.ReadToEnd();
			}
			xml.LoadXml(content);

			#endif
			#if !__ANDROID__
            xml.Load(filename);
			#endif

            //Console.WriteLine("Node Name: {0} ", element);
            XmlNodeList xnList = xml.SelectNodes(element);
            foreach (XmlNode xn in xnList)
            {
                levelAttrs.Add(xn.Name.ToString(), xn.InnerText.ToString() );

                //Console.WriteLine("Name: {0} -- {1}", xn.Name.ToString(), xn.Attributes.ToString());
                
                foreach (XmlAttribute item in xn.Attributes)
                {
                    //Console.WriteLine("attr: {0}", item.Name.ToString());
                    levelAttrs.Add(item.Name.ToString(), item.Value.ToString() );
                }
            }
            return levelAttrs;
        }


        /// <summary>
        /// Returns the attributes on the node/element specified.
        /// </summary>
        /// <param name="filename">Filename, example: "ogmoLevels/level1.oel"</param>
        /// <param name="element">Element from XML file, example: "level/ActorsLayer" or "level"</param>
        /// <returns></returns>
        public static Dictionary<string, string> readAttributesFromTmxFile(string filename, string element)
        {

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            XmlDocument xml = new XmlDocument();
            xml.Load(filename);
            //Console.WriteLine("Node Name: {0} ", element);
            XmlNodeList xnList = xml.SelectNodes(element);
            foreach (XmlNode xn in xnList)
            {
                //levelAttrs.Add(xn.Name.ToString(), xn.InnerText.ToString());

                //Console.WriteLine("Name: {0} -- {1}", xn.Name.ToString(), xn.Attributes.ToString());

                foreach (XmlAttribute item in xn.Attributes)
                {
                    //Console.WriteLine("attr: {0}", item.Name.ToString());
                    levelAttrs.Add(item.Name.ToString(), item.Value.ToString());
                }
            }
            return levelAttrs;
        }

        public static List<Dictionary<string, string>> readNodesFromOelFile(string filename, string element)
        {

			XmlDocument xml = new XmlDocument();
			//Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

			#if __ANDROID__

			string content;
			using (StreamReader sr = new StreamReader (Game.Activity.Assets.Open(filename)))
			{
				content = sr.ReadToEnd();
			}
			xml.LoadXml(content);

			#endif
			#if !__ANDROID__
			xml.Load(filename);
			#endif


            List<Dictionary<string, string>> nodeList = new List<Dictionary<string, string>>();
            
			//XmlDocument xml = new XmlDocument();
			//xml.Load(filename);
            
            XmlNodeList xnList = xml.SelectNodes(element);
            
            foreach (XmlNode xn in xnList)
            {
                // cycle through characters.

                foreach (XmlNode xn2 in xn) {

                    Dictionary<string, string> levelAttrs = new Dictionary<string, string>();
                    //Console.WriteLine("xn2 Name: {0} -- {1}", xn2.Name.ToString(), xn2.Attributes.ToString());

                    //add characters name
                    levelAttrs.Add("Name", xn2.Name.ToString());

                    //cycle attributes.
                    foreach (XmlAttribute item in xn2.Attributes)
                    {
                        //Console.WriteLine("attr: {0}", item.Name.ToString());
                        levelAttrs.Add(item.Name.ToString(), item.Value.ToString());

                    }
                    // character may have path nodes:
                    
                    string pointX = "";
                    string pointY = "";

                    foreach (XmlNode pathnode in xn2)
                    {
                        foreach (XmlAttribute item in pathnode.Attributes)
                        {
                            if (item.Name.ToString() == "x")
                            {
                                pointX += item.Value.ToString()+",";
                            }
                            else if (item.Name.ToString() == "y")
                            {
                                pointY += item.Value.ToString() + ",";
                            }
                        }
                    }

                    //Console.WriteLine("XML READER: {0} {1}", pointX, pointY);

                    levelAttrs.Add("pathNodesX", pointX);
                    levelAttrs.Add("pathNodesY", pointY);

                    nodeList.Add(levelAttrs);
                }
            }
            return nodeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="type">Use either FlxXMLReader.TILES or FlxXMLReader.ACTORS</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> readNodesFromTmxFile(string filename, string element, string name, int type)
        {
            List<Dictionary<string, string>> nodeList = new List<Dictionary<string, string>>();

            XmlDocument xml = new XmlDocument();
            xml.Load(filename);

            XmlNodeList xnList = xml.SelectNodes(element);

            foreach (XmlNode xn in xnList)
            {
                // cycle through characters.

                string nameToHoldDataString = "";

                foreach (XmlNode xn2 in xn)
                {

                    Dictionary<string, string> levelAttrs = new Dictionary<string, string>();
                    //Console.WriteLine("xn2 Name: {0} -- {1}", xn2.Name.ToString(), xn2.Attributes.ToString());

                    //add characters name
                    levelAttrs.Add("Name", xn2.Name.ToString());

                    //cycle attributes.
                    foreach (XmlAttribute item in xn2.Attributes)
                    {

                        levelAttrs.Add(item.Name.ToString(), item.Value.ToString());

                        if (item.Value.ToString() == name)
                        {
                            //Console.WriteLine("attr: {0} {1}", item.Name.ToString(), item.Value.ToString());

                            //levelAttrs.Add(item.Name.ToString(), item.Value.ToString());


                            nameToHoldDataString = item.Value.ToString();

                            //XmlNodeList xnData = xn2.SelectNodes("data");
                            //foreach (XmlNode xn2 in xnData)
                            //levelAttrs.Add(item.Name.ToString(), xnData2.InnerText.ToString());

                            foreach (XmlNode xn3 in xn2)
                            {
                                //Console.WriteLine("xn3 Name: {0} -- {1}", xn3.Name.ToString(), xn3.InnerText.ToString());

                                string ext = "";

                                if (xn3.InnerText.ToString().Contains('\r'))
                                {
                                    Console.WriteLine("!! - ERROR - !! This XML file contains Windows style new lines: File-> "+ filename + "Solution: Convert to UNIX style line endings");
                                }

                                if (type == TILES)
                                {
                                    ext = xn3.InnerText.ToString().Replace(",\n", "\n");
                                    ext = ext.Remove(0, 1);
                                    ext = ext.Remove(ext.Length - 1);
                                }
                                else if (type == ACTORS)
                                {
                                    ext = xn3.InnerText.ToString().Replace(",\n", ",");
                                    ext = ext.Remove(0, 1);
                                    ext = ext.Remove(ext.Length - 1);
                                }



                                levelAttrs.Add("csvData", ext);
                            }

                            nodeList.Add(levelAttrs);
                        }


                    }

                    
                }


            }
            return nodeList;
        }
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseNode"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> readCustomXML(string baseNode, string filename)
        {
            List<Dictionary<string, string>> completeSet = new List<Dictionary<string, string>>();

            string currentLevel = "l" + FlxG.level.ToString();

            XElement xelement = XElement.Load(filename);

            foreach (XElement xEle in xelement.Descendants(baseNode).Elements(currentLevel))
            {
                foreach (XElement xEle2 in xEle.Descendants())
                {
                    Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

                    if (xEle2.Value.ToString() != null)
                        levelAttrs.Add("text", xEle2.Value.ToString());

                    IEnumerable<XAttribute> attList =
                    from at in xEle2.Attributes()
                    select at;

                    foreach (XAttribute xAttr in attList)
                    {
                        levelAttrs.Add(xAttr.Name.ToString(), xAttr.Value.ToString());
                    }
                    completeSet.Add(levelAttrs);
                }
            }
            return completeSet;
        }

        // END
    }

}
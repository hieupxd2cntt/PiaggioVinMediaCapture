using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TwainScan
{
    public class XMLProcess
    {
        public string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
        public string GetXMLFromObject(object o, XmlSerializerNamespaces nameSpace)
        {
            StringWriterUtf8 sw = new StringWriterUtf8();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());                
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o,nameSpace);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
        public string ToXML(object o, Type type)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(type);
            serializer.Serialize(stringwriter, o);
            return stringwriter.ToString();
        }
        public void SaveXmlToFile(string xml, string path)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(xml))
                {
                    File.Delete(path);
                    return;
                }

                xmlDoc.LoadXml(xml);
                var parent=Directory.GetParent(path);
                bool exists =Directory.Exists(parent.ToString());

                if (!exists)
                    Directory.CreateDirectory(parent.ToString());
                xmlDoc.Save(path);
            }
            catch (Exception ex)
            {
               
            }
            
        }
        /// <summary>
        /// Chuyển xml sang object
        /// </summary>
        /// <param name="xmlText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object LoadObjectFromXMLString(string xmlText, Type type)
        {
            try
            {
                var stringReader = new System.IO.StringReader(xmlText);
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy string xml từ file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string LoadXmlFromFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return string.Empty;
                if (String.IsNullOrEmpty(File.ReadAllText(path)))
                    return string.Empty;
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                return xml.InnerXml;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            
        }
    }
}

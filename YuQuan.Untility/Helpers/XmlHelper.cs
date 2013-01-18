using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace YuQuan.Helpers
{
    public static class XmlHelper
    {
        public static string FormatXml(string unformatted)
        {
            if(string.IsNullOrEmpty(unformatted))
                return "";

            var sb = new StringBuilder();

            // We will use stringWriter to push the formated xml into our StringBuilder 
            using (StringWriter stringWriter = new StringWriter(sb))
            {
                // We will use the Formatting of our xmlTextWriter to provide our indentation. 
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(unformatted);
                        doc.WriteTo(xmlTextWriter);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        return "";
                    }
                }
            }
            
            return sb.ToString();
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Windows.Resources;
using System.Windows;
using System.Text;

namespace YuQuan.Helpers
{
    public sealed partial class ResourceHelper
    {
        public static String GetResourceFileAsString(String relativeUri)
        {
            if (String.IsNullOrEmpty(relativeUri) == true)
                return String.Empty;

            StreamResourceInfo sr = Application.GetResourceStream(
     new Uri(relativeUri, UriKind.Relative));
            Byte[] buffer = new Byte[sr.Stream.Length];
            sr.Stream.Read(buffer, 0, (int)sr.Stream.Length);
            return Encoding.UTF8.GetString(buffer, 0, buffer.GetLength(0));
        }

        public static String GetEmbededResourceFileAsString(String uri)
        {
            if (String.IsNullOrEmpty(uri) == true)
                return String.Empty;

            // After moving this class to YuQuan.Utility,  Assembly.GetExecutingAssembly() no longer worked as expected. 
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(uri);
            if(stream == null)
                stream = Assembly.GetCallingAssembly().GetManifestResourceStream(uri);
            if(stream != null)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }

            return String.Empty;
        }
    }
}

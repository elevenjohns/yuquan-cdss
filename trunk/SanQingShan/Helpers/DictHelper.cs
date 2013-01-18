using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using YuQuan.Helpers;

namespace SanQingShan.Helpers
{
    public static class DictHelper
    {
        public static IDictionary<string, string> GetCaseDict()
        {
            var dict = new Dictionary<string, string>();

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string name in names)
            {
                if (name.Contains(@"CaseDict.txt"))
                {
                    String str = ResourceHelper.GetEmbededResourceFileAsString(name);

                    string[] lines = str.Replace("\r", "").Split('\n');
                    int lineCount = lines.Count();
                    char[] spliter = { '\t' };
                    for (int i = 0; i < lineCount; i++)
                    {
                        String str_Line = lines[i];
                        str_Line = str_Line.Replace(" ", "");
                        if (!str_Line.Equals(String.Empty))
                        {
                            string[] strResult = str_Line.Split(spliter);
                            dict.Add(strResult[0].Replace(" ", ""), strResult[1].Replace(" ", ""));
                        }
                    }
                }
            }
            return dict;
        }

        public static IList<string> GetOrderHeaders()
        {
            var list = new List<string>();

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string name in names)
            {
                if (name.Contains(@"OrderHeader.txt"))
                {
                    String str = ResourceHelper.GetEmbededResourceFileAsString(name);

                    string[] lines = str.Replace("\r", "").Split('\n');

                    int lineCount = lines.Count();
                    for (int i = 0; i < lineCount; i++)
                    {
                        String str_Line = lines[i];
                        str_Line = str_Line.Replace(" ", "");
                        if (!str_Line.Equals(String.Empty))
                            list.Add(str_Line);
                    }
                }
            }
            return list;
        }
    }
}
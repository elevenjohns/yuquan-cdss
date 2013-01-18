using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YuQuan.Untility
{
    public static class SimpleLog
    {
        public static void WriteLog(string message)
        {
            using (StreamWriter sw = File.CreateText(@"d:\" + Guid.NewGuid() + ".txt"))
            {
                sw.Write(message);
            }
        }
        
    }
}

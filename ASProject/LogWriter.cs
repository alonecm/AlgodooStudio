using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject
{
    internal static class LogWriter
    {
        public static void Write(string message)
        {
            using (StreamWriter sw = File.AppendText(".\\logs.txt"))
            {
                sw.WriteLine($"{DateTime.Now.TimeOfDay}:{message}");
            }
        }
    }
}

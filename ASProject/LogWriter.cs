using System;
using System.IO;

namespace AlgodooStudio.ASProject
{
    internal static class LogWriter
    {
        public static void Write(string message)
        {
            if (!Directory.Exists(".\\Logs"))
            {
                Directory.CreateDirectory(".\\Logs");
            }
            using (StreamWriter sw = File.AppendText($".\\Logs\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt"))
            {
                sw.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]{message}");
            }
        }

        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public static void WriteInfo(string message)
        {
            Write($"[INFO] {message}");
        }

        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public static void WriteWarn(string message)
        {
            Write($"[WARN] {message}");
        }

        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        public static void WriteError(string message)
        {
            Write($"[ERROR] {message}");
        }

        /// <summary>
        /// 写致命错误
        /// </summary>
        /// <param name="message"></param>
        public static void WriteFatal(string message)
        {
            Write($"[FATAL] {message}");
        }
    }
}
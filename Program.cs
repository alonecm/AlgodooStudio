using AlgodooStudio.ASProject.Support;
using Dex.IO.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AlgodooStudio
{
    internal static class Program
    {
        private static ASProject.MainWindow mainWindow;
        private static Setting setting = new Setting();
        /// <summary>
        /// 当前设置文件的所在路径
        /// </summary>
        private static readonly string settingPath = Directory.GetCurrentDirectory() + "\\settings.ini";

        /// <summary>
        /// 主窗口
        /// </summary>
        public static ASProject.MainWindow MainWindow => mainWindow;
        /// <summary>
        /// 设定信息
        /// </summary>
        public static Setting Setting => setting;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //加载设置
            ReadSetting();

            //启用窗体系统
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainWindow = new ASProject.MainWindow());
        }


        /// <summary>
        /// 保存设置
        /// </summary>
        public static void SaveSetting()
        {
            //直接写入
            new SimpleConfig(setting).Write(settingPath);
        }
        /// <summary>
        /// 读取设置
        /// </summary>
        public static void ReadSetting()
        {
            //如果文件不存在则先保存创建一个文件
            if (!File.Exists(settingPath))
            {
                SaveSetting();
            }
            //读取文件
            var sc = new SimpleConfig();
            sc.Read(settingPath);
            setting = (Setting)sc.Objects[0];
        }
    }
}
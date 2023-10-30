using AlgodooStudio.ASProject.Support;
using AlgodooStudio.PluginSystem;
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
        /// <summary>
        /// 软件设置
        /// </summary>
        private static Setting setting = new Setting();
        /// <summary>
        /// 当前设置文件的所在路径
        /// </summary>
        private static readonly string settingPath = ".\\settings.ini";
        /// <summary>
        /// 设定信息
        /// </summary>
        public static Setting Setting => setting;
        /// <summary>
        /// 状态栏消息
        /// </summary>
        public static string StatusMessage
        {
            get => mainWindow.StatusMessage;
            set => mainWindow.StatusMessage = value;
        }

        [STAThread]
        private static void Main()
        {
            //加载设置
            ReadSetting();
            //加载插件
            LoadPlugins();
            //启用窗体系统
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainWindow = new ASProject.MainWindow());
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private static void LoadPlugins()
        {
            //如果文件夹不存在则创建文件夹
            if (!Directory.Exists(".\\Plugins"))
            {
                Directory.CreateDirectory(".\\Plugins");
            }
            //加载插件
            var files = new DirectoryInfo(".\\Plugins").GetFiles("*.dll");
            foreach (var item in files)
            {
                Loader.Regist(item.FullName);
            }
            Loader.EnabledAllPlugin();
        }
        /// <summary>
        /// 设定可编辑属性对象的对象
        /// </summary>
        /// <param name="obj"></param>
        public static void SetPropertyEditObject(object obj)
        {
            mainWindow.SetPropertyEditObject(obj);
        }
        /// <summary>
        /// 设定可编辑属性对象的对象
        /// </summary>
        /// <param name="obj"></param>
        public static void SetPropertyEditObject(object[] obj)
        {
            mainWindow.SetPropertyEditObject(obj);
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
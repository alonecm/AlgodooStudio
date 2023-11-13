using AlgodooStudio.ASProject;
using AlgodooStudio.ASProject.Support;
using AlgodooStudio.PluginSystem;
using Dex.IO.Config;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlgodooStudio
{
    internal static class Program
    {
        [DllImport("user32.dll")]//取设备场景
        internal static extern IntPtr GetDC(IntPtr hwnd);//返回设备场景句柄
        [DllImport("gdi32.dll")]//取指定点颜色
        internal static extern int GetPixel(IntPtr hdc, Point p);

        /// <summary>
        /// 当前设置文件的所在路径
        /// </summary>
        private const string settingPath = ".\\settings.ini";

        private static MainWindow mainWindow;

        /// <summary>
        /// 软件设置
        /// </summary>
        private static Settings setting = new Settings();
        /// <summary>
        /// 设定信息
        /// </summary>
        public static Settings Setting => setting;

        /// <summary>
        /// 状态栏消息
        /// </summary>
        public static string StatusMessage
        {
            get => mainWindow.StatusMessage;
            set => mainWindow.StatusMessage = value;
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        public static void ReadSetting()
        {
            //如果文件不存在则先用保存功能创建一个文件
            if (!File.Exists(settingPath))
            {
                SaveSetting();
            }
            //读取设置
            var sc = new SimpleConfig();
            sc.Read(settingPath);
            setting = (Settings)sc.Objects[0];
            //检查相关路径是否存在
            CheckPath();
            //假设路径发生了变动设置则需要再保存一次，没变动就保存一次以防万一
            SaveSetting();
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
        /// 检查路径信息
        /// </summary>
        private static void CheckPath()
        {
            if (setting.ScenePath == string.Empty)
            {
                setting.ScenePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Algodoo\\scenes";
            }
            if (setting.AlgodooPath == string.Empty)
            {
                if (MBox.ShowWarningYesNoCancel("您似乎没有设置Algodoo根目录的位置，是否需要设置？") == DialogResult.Yes)
                {
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            setting.AlgodooPath = fbd.SelectedPath;
                        }
                    }
                }
            }
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
                try
                {
                    Loader.Regist(item.FullName);
                }
                catch (Exception e)
                {
                    MBox.ShowError($"加载插件 \"{item.Name}\" 时出现了 \"{e.Message}\"异常");/*，异常信息已经输出到\"{Environment.CurrentDirectory}\\Logs\"文件夹中");*/
                }
            }
            //启用全部插件
            Loader.EnabledAllPlugin();
        }

        [STAThread]
        private static void Main()
        {
            //启用窗体系统
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //加载设置
            ReadSetting();
            //加载插件
            LoadPlugins();
            //启动工作室
            Application.Run(mainWindow = new MainWindow());
        }
    }
}
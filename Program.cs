using AlgodooStudio.ASProject;
using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Support;
using AlgodooStudio.PluginSystem;
using Dex.Analysis.Parse;
using Dex.Common;
using Dex.IO;
using Dex.IO.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        /// <summary>
        /// 主窗口
        /// </summary>
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
            sc.Read(settingPath,Encoding.Default);
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
            new SimpleConfig(setting).Write(settingPath, Encoding.Default);
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
        /// 更新错误信息
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="diagnostics"></param>
        public static void UpdateErrors(string filename, DiagnosticsCollection diagnostics)
        {
            mainWindow.UpdateErrors(filename, diagnostics);
        }
        /// <summary>
        /// 选中错误
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="range"></param>
        public static void SelectError(string windowName, Range range)
        {
            mainWindow.SelectError(windowName, range);
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
                Loader.Load(item.FullName);
            }
            //跟随记录启用插件
            Loader.EnablePlugins();
        }
        /// <summary>
        /// 生成代码片段文件夹
        /// </summary>
        private static void CreateClipsFolder()
        {
            //如果文件夹不存在则创建文件夹
            if (!Directory.Exists(".\\Clips"))
            {
                Directory.CreateDirectory(".\\Clips");
            }
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
            //生成代码片段文件夹
            CreateClipsFolder();
            //启动工作室
            Application.Run(mainWindow = new MainWindow());
        }
    }
}
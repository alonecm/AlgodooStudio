﻿using AlgodooStudio.ASProject;
using AlgodooStudio.ASProject.Script;
using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Script.Parse.Expr;
using AlgodooStudio.ASProject.Support;
using AlgodooStudio.PluginSystem;
using Dex.Analysis.Parse;
using Dex.Common;
using Dex.IO;
using Dex.IO.Config;
using System;
using System.Drawing;
using System.IO;
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
        /// Algodoo根目录设置是否正确
        /// </summary>
        public static bool IsTrueAlgodooPath => File.Exists(setting.AlgodooExecuteFilePath);
        
        
        /// <summary>
        /// 读取设置
        /// </summary>
        public static void ReadSetting()
        {
            LogWriter.WriteInfo("读取设置...");
            //如果文件不存在则先用保存功能创建一个文件
            if (!File.Exists(settingPath))
            {
                SaveSetting();
            }
            //读取设置
            var sc = new SimpleConfig();
            sc.Read(settingPath, Encoding.Default);
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
            LogWriter.WriteInfo("保存设置...");
        }
        /// <summary>
        /// 设定可编辑属性对象的对象
        /// </summary>
        /// <param name="obj"></param>
        public static void SetPropertyEditObject(object obj)
        {
            mainWindow.SetPropertyEditObject(obj);
            LogWriter.WriteInfo("设定可编辑属性对象的对象：" + obj.ToString());
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
                LogWriter.WriteInfo("获取场景文件夹路径：" + setting.ScenePath);
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
                            LogWriter.WriteInfo("设置algodoo根目录为：" + fbd.SelectedPath);
                        }
                    }
                }
            }
            else if (!IsTrueAlgodooPath)
            {
                MBox.ShowError("根目录设置不正确，请重新设置！");
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        setting.AlgodooPath = fbd.SelectedPath;
                        LogWriter.WriteInfo("设置algodoo根目录为：" + fbd.SelectedPath);
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
                LogWriter.WriteInfo("创建Plugins文件夹...");
            }
            //加载插件
            var files = new DirectoryInfo(".\\Plugins").GetFiles("*.dll");
            foreach (var item in files)
            {
                Loader.Load(item.FullName);
                LogWriter.WriteInfo("加载插件：" + item.Name);
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
                LogWriter.WriteInfo("创建Clips文件夹...");
                Directory.CreateDirectory(".\\Clips");
            }
        }
        /// <summary>
        /// 创建托管文件夹
        /// </summary>
        private static void CreateManageFolder()
        {
            if (!Directory.Exists(".\\Manage"))
            {
                LogWriter.WriteInfo("创建Manage文件夹...");
                Directory.CreateDirectory(".\\Manage");
            }
        }
        /// <summary>
        /// 创建临时文件夹
        /// </summary>
        private static void CreateTempFolder()
        {
            if (!Directory.Exists(".\\Temp"))
            {
                LogWriter.WriteInfo("创建临时文件夹...");
                var temp = Directory.CreateDirectory(".\\Temp");
                temp.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }
        /// <summary>
        /// 删除临时文件夹
        /// </summary>
        private static void DeleteTempFolder()
        {
            if (Directory.Exists(".\\Temp"))
            {
                LogWriter.WriteInfo("删除临时文件夹...");
                Directory.Delete(".\\Temp");
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
            //创建临时文件夹
            CreateTempFolder();
            //生成托管文件夹
            CreateManageFolder();
            //生成代码片段文件夹
            CreateClipsFolder();
            //启动工作室
            Application.Run(mainWindow = new MainWindow());
            //删除临时文件夹
            DeleteTempFolder();
        }
    }
}
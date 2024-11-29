using AlgodooStudio.ASProject;
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
using System.CodeDom.Compiler;
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
        /// 自启动项
        /// </summary>
        private static AutoExecuteItemCollection autoExecuteItems = new AutoExecuteItemCollection();

        /// <summary>
        /// 自启动项集合
        /// </summary>
        public static AutoExecuteItemCollection AutoExecuteItems => autoExecuteItems;
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
        /// 读取自启动项
        /// </summary>
        private static void ReadAutoExecuteFile()
        {
            if (!Directory.Exists(setting.AlgodooPath))
            {
                MBox.ShowError("Algodoo根目录未设置正确！请到设置中进行设置！");
                LogWriter.WriteError("Algodoo根目录路径异常");
                return;
            }
            var path = setting.AlgodooPath + "\\autoexec.cfg";
            if (!File.Exists(path))
            {
                MBox.ShowError("找不到Algodoo自启动文件！");
                LogWriter.WriteError("Algodoo自启动文件丢失");
                return;
            }
            LogWriter.WriteInfo("解析自启动文件...");
            ThymeParser parserActive = new ThymeParser(
                new ThymeTokenizer(
                    FileHandler.GetTextFileContent(path, Encoding.UTF8)//NOTE:自启动文件可以表示为启用项，我再创建一个禁用项文件用来存放禁用项即可，启用就是从禁用项文件中移动到自启动文件中，禁用则是相反
                    ).Tokenize());
            var rootActive = parserActive.Parse() as Root;
            if (parserActive.Diagnostics.Count>0)
            {
                MBox.ShowWarning("自启动文件中存在语法错误，已输出到日志文件中，请查看");
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in parserActive.Diagnostics)
                {
                    stringBuilder.AppendLine($"{item.Range}[{item.Type}] {item.Message}");
                }
                LogWriter.WriteWarn(stringBuilder.ToString());
            }

            //添加启用项
            var rgEnable = new ThymeReGenerator();
            foreach (var item in rootActive.Nodes)
            {
                var content = rgEnable.ReGenerate(item);
                if (content.Contains("reflection.executefile"))
                {
                    autoExecuteItems.Add(new AutoExecuteItem(true, AutoExecuteItemType.File, content));
                }
                else
                {
                    autoExecuteItems.Add(new AutoExecuteItem(true, AutoExecuteItemType.Code, content));
                }
            }

            //检查是否存在禁用项托管文件
            if (File.Exists(".\\Manage\\disabled_execute_item.manage"))
            {
                LogWriter.WriteInfo("解析托管文件...");
                ThymeParser parserInactive = new ThymeParser(
                    new ThymeTokenizer(
                        FileHandler.GetTextFileContent(".\\Manage\\disabled_execute_item.manage", Encoding.UTF8)//NOTE:自启动文件可以表示为启用项，我再创建一个禁用项文件用来存放禁用项即可，启用就是从禁用项文件中移动到自启动文件中，禁用则是相反
                        ).Tokenize());
                var rootInactive = parserInactive.Parse() as Root;
                //添加禁用项
                var egEnable = new ThymeReGenerator();
                foreach (var item in rootInactive.Nodes)
                {
                    var content = egEnable.ReGenerate(item);
                    if (content.Contains("reflection.executefile"))
                    {
                        autoExecuteItems.Add(new AutoExecuteItem(false, AutoExecuteItemType.File, content));
                    }
                    else
                    {
                        autoExecuteItems.Add(new AutoExecuteItem(false, AutoExecuteItemType.Code, content));
                    }
                }
            }
        }
        /// <summary>
        /// 保存自启动项
        /// </summary>
        public static void SaveAutoExecuteItem()
        {
            if (!Directory.Exists(setting.AlgodooPath))
            {
                MBox.ShowError("Algodoo根目录未设置正确！请到设置中进行设置！");
                return;
            }
        }
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
                            LogWriter.WriteInfo("设置algodoo根目录为："+ fbd.SelectedPath);
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
                LogWriter.WriteInfo("创建Plugins文件夹...");
            }
            //加载插件
            var files = new DirectoryInfo(".\\Plugins").GetFiles("*.dll");
            foreach (var item in files)
            {
                Loader.Load(item.FullName);
                LogWriter.WriteInfo("加载插件："+ item.Name);
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
                var temp= Directory.CreateDirectory(".\\Temp");
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
            //读取自启动文件
            ReadAutoExecuteFile();
            //启动工作室
            Application.Run(mainWindow = new MainWindow());
            //删除临时文件夹
            DeleteTempFolder();
        }
    }
}
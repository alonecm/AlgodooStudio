using AlgodooStudio.Manager;
using AlgodooStudio.Utils;
using AlgodooStudio.Window;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AlgodooStudio
{
    internal static class Program
    {
        /// <summary>
        /// 当前设置文件的所在路径
        /// </summary>
        private static readonly string settingPath = Directory.GetCurrentDirectory() + "\\Settings.xml";

        /// <summary>
        /// 启动用时计时时钟
        /// </summary>
        internal static Stopwatch loadTimer = new Stopwatch();

        /// <summary>
        /// 主窗口
        /// </summary>
        private static MainWindow m;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //开始计时
            loadTimer.Start();
            //测试1
            TestMethod1();
            //启用窗体系统
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //测试2
            TestMethod2();
            //启动装载项目
            StartingEquipItems();
            //加载配置文件
            SettingAutoOperate();
            //测试3
            TestMethod3();
            Application.Run(m = new MainWindow());
        }

        /// <summary>
        /// 启动装载项目
        /// </summary>
        private static void StartingEquipItems()
        {
            //初始化布局管理器
            LayoutManager.LoadXMLFiles();
            //初始化主题管理器
            ThemeManager.LoadXMLFiles();
            //初始化插件管理器
            PluginManager.Load();
            //取得当前项目的全部类型
            XmlSerializer.GetProjectTypes();
        }

        /// <summary>
        /// 设置的启动读取与不存在时的保存
        /// </summary>
        private static void SettingAutoOperate()
        {
            using (XmlSerializer xs = new XmlSerializer(settingPath))
            {
                //检查文件是否存在，如果存在则读取，否则就创建原始设置文件
                if (File.Exists(settingPath))
                {
                    xs.UnSerializeStaticClass(typeof(Setting));
                }
                else
                {
                    xs.SerializeStaticClass(typeof(Setting));
                }
            }
        }

        /// <summary>
        /// 设置保存
        /// </summary>
        internal static void SettingSave()
        {
            XmlSerializer.SerializeStaticClassToXml(typeof(Setting), settingPath);
            m.TipsText = "配置已保存完成";
        }

        /// <summary>
        /// 设置加载
        /// </summary>
        internal static void SettingLoad()
        {
            XmlSerializer.UnSerializeXmlToStaticClass(typeof(Setting), settingPath);
            m.TipsText = "配置已加载完成";
        }

        #region Debug

        /// <summary>
        /// 测试方法1
        /// </summary>
        private static void TestMethod1()
        {
            //Theme t = new Theme(0, "bbt");
            //t.SetBorderColor(Color.Red);
            //t.SetBackColor(Color.Green);
            //t.SetItemBackColor(Color.Yellow);
            //t.SetVarNameColor(Color.Black);
            //t.SetClassNameColor(Color.Black);
            //t.SetKeywordsColor(Color.Black);
            //t.SetOperatorColor(Color.Black);
            //t.SetNumberColor(Color.Black);
            //t.SetStringColor(Color.Black);
            //t.SetMethodColor(Color.Black);
            //t.SetParameterColor(Color.Black);
            //t.SetWeifenluoTheme(new VS2015BlueTheme());//输出的是自身名称也就是VS2015BlueTheme
            //ThemeManager.SaveToXMLFile(t);
        }

        /// <summary>
        /// 测试方法2
        /// </summary>
        private static void TestMethod2()
        {
            //Image i = new Bitmap(100, 100); 可以用这种方式创建图片
            //Graphics gc = Graphics.FromImage(i);
        }

        /// <summary>
        /// 测试方法3
        /// </summary>
        private static void TestMethod3()
        {
            //SyntaxAnalyzer a = new SyntaxAnalyzer(new FileInfo(@"D:\Algodoo\tLib\extendMath.thm"));
            //
            //a.Analyze();
            //
            //Test.Start();
            //StringBuilder sb = new StringBuilder();
            //using (FileStream fs = new FileStream(@"D:\Algodoo\tLib\test.thm", FileMode.Open))
            //{
            //    while (true)
            //    {
            //        byte[] buffer = new byte[1024 * 1024];
            //        if (fs.Read(buffer, 0, buffer.Length) == 0)
            //        {
            //            break;
            //        }
            //        sb.Append(Encoding.UTF8.GetString(buffer));
            //    }
            //}
            //Container<Token> e = new Container<Token>();
            //ThymeParser ts = new ThymeParser(sb.ToString());
            //AST a = ts.Parse();
            ////ts.IsDebug = true;
            ////while (!ts.EndOfRead())
            ////{
            ////    e.Add(ts.Next());
            ////}
            ////TreeNode<string> a = new TreeNode<string>("");
            ////a.AddRange(new TreeNode<string>(), new TreeNode<string>(), new TreeNode<string>());
            //float fae = Test.Stop();
        }

        #endregion Debug
    }
}
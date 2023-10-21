using AlgodooStudio.Base;
using AlgodooStudio.Window;
using AlgodooStudio.Window.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using Zero.Core.FileAndDirectory.FileTools;
using Zero.Core.XML;

namespace AlgodooStudio.Manager
{
    /// <summary>
    /// 布局管理器
    /// </summary>
    internal static class LayoutManager
    {
        /// <summary>
        /// xml文件对应的Layout文件窗口索引值
        /// </summary>
        private static int currentLayoutFormsIndex = 0;

        /// <summary>
        /// 布局文件夹(自带斜杠哈)
        /// </summary>
        private const string layoutFolder = layoutFolderNo + "\\";

        /// <summary>
        /// 布局文件夹(不带斜杠哈)
        /// </summary>
        private const string layoutFolderNo = "Layouts";

        /// <summary>
        /// 字符串合并器
        /// </summary>
        private static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 文件查询器
        /// </summary>
        private static FileSearcher fs = new FileSearcher(layoutFolderNo);

        /// <summary>
        /// XML文件
        /// </summary>
        private static EasyXml xml;

        /// <summary>
        /// layoutForms文件
        /// </summary>
        private static EasyXml layoutForms;

        /// <summary>
        /// 当前准备加载的布局
        /// </summary>
        private static Layout currentLayout;

        /// <summary>
        /// XML文件读取进来的全部布局
        /// </summary>
        private static Dictionary<string, Layout> layouts = new Dictionary<string, Layout>();

        /// <summary>
        /// XML文件读取进来的全部布局
        /// </summary>
        internal static Dictionary<string, Layout>.KeyCollection LayoutNames { get => layouts.Keys; }

        /// <summary>
        /// 当前加载好的布局名
        /// </summary>
        internal static string CurrentLayoutName
        {
            get
            {
                if (currentLayout == null)
                {
                    return "";
                }
                else
                {
                    return currentLayout.Name;
                }
            }
        }

        #region 辅助性方法

        /// <summary>
        /// 创建xml文件时获取文件路径的方法
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="extention">文件扩展名</param>
        private static string GetFilePath(string name, string extention)
        {
            sb.Clear();
            sb.Append(layoutFolder);
            sb.Append(name);
            sb.Append(".");
            sb.Append(extention);
            return sb.ToString();
        }

        /// <summary>
        /// 设定currentLayout
        /// </summary>
        /// <param name="name">给定的布局名</param>
        private static void SetCurrentLayout(string name)
        {
            currentLayout = layouts[name];
        }

        /// <summary>
        /// 设定窗口布局
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static IDockContent SetDockContent(string str)
        {
            //将读取到的配置文件的属性值提取出来作为索引值
            int index = int.Parse(xml.RootNode.ChildNodes[0].ChildNodes[currentLayoutFormsIndex].Attribute[0].Content);
            //根据布局键创建窗体
            IDockContent dc = CreateFormByLayoutKey(index);
            currentLayoutFormsIndex++;
            return dc;
        }

        /// <summary>
        /// 通过布局键创建指定的窗体
        /// </summary>
        /// <param name="index">布局键</param>
        /// <returns>指定的窗体</returns>
        private static IDockContent CreateFormByLayoutKey(int index)
        {
            //新加入的窗体需要在此处注册
            switch (currentLayout[index])
            {
                case "ResourceExplorerWindow":
                    return new ResourceExplorerWindow();

                case "ScriptEditorWindow":
                    return new ScriptEditorWindow();

                case "ScriptManagerWindow":
                    return new ScriptManagerWindow();

                default:
                    return new DockContent();
            }
        }

        /// <summary>
        /// 保存XML前生成窗口列表
        /// </summary>
        /// <param name="panel">停靠板</param>
        /// <param name="name">布局名</param>
        private static void CreateLayoutFormsFile(DockPanel panel, string name)
        {
            layoutForms = EasyXml.Create(GetFilePath(name, "layoutForms"), "Forms");
            foreach (var item in panel.Contents)
            {
                layoutForms.RootNode.ChildNodes.Add(new Xml_Node("Form", item.GetType().Name, new Xml_Attb("ID", panel.Contents.IndexOf(item).ToString())));
            }
            layoutForms.Save();
            layoutForms.Dispose();
        }

        /// <summary>
        /// 从布局名获取文件中的布局
        /// </summary>
        /// <param name="index">布局编号</param>
        /// <param name="name">布局名</param>
        /// <returns>布局</returns>
        private static Layout GetLayout(int index, string name)
        {
            //读取布局窗体文件
            layoutForms = new EasyXml(GetFilePath(name, "layoutForms"), false);
            layoutForms.Load();
            //生成一个布局
            Layout layout = new Layout(index, name);
            int i = 0;
            //当XML文件中的子节点索引大于布局子节点数时结束
            while (true)
            {
                if (i >= layoutForms.RootNode.ChildNodes.Count)
                {
                    break;
                }
                else
                {
                    //添加布局
                    layout.AddForm(
                        int.Parse(layoutForms.RootNode.ChildNodes[i].Attribute[0].Content),
                        layoutForms.RootNode.ChildNodes[i].Content
                    );
                }
                i++;
            }
            layoutForms.Dispose();
            return layout;
        }

        #endregion 辅助性方法

        /*
         如果有需要打开文件之类的窗口可能需要从中排除或者重新调整
         */

        /*
            其实无需LayoutForms分配窗体，只需要让xml读取器从persistString中读取窗体类名进行比对
            然后作用于switch即可，不过鉴于已经成这样了，就先不改了(^▽^);
         */

        /// <summary>
        /// 置空currentLayout
        /// </summary>
        internal static void EmptyCurrentLayout()
        {
            currentLayout = null;
        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        internal static void CloseAllForms(DockPanel panel)
        {
            DockContentCollection contents = panel.Contents;
            while (true)
            {
                if (contents.Count == 0)
                {
                    break;
                }
                else
                {
                    contents[0].DockHandler.Close();
                }
            }
        }

        /// <summary>
        /// 加载全部布局文件
        /// </summary>
        internal static void LoadXMLFiles()
        {
            /*
             1.获取文件夹下的.layoutForms类型文件
             2.消除扩展名用于创建键
             3.创建布局的值
             4.添加
             */
            //获取所有布局文件
            List<FileInfo> layoutFormsFiles = fs.GetFilesFromThisFolder("*.layoutForms").ToList();
            int index = 0;
            foreach (var item in layoutFormsFiles)
            {
                string name = Path.GetFileNameWithoutExtension(item.Name);
                //添加消除扩展名的布局文件
                layouts.Add(name, GetLayout(index, name));
                index++;
            }
        }

        /// <summary>
        /// 应用指定的布局
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="name"></param>
        internal static void ApplyLayout(DockPanel panel, string name)
        {
            //为空则直接布局
            if (currentLayout == null)
            {
                //关闭所有窗体
                CloseAllForms(panel);
                //装载前清零
                currentLayoutFormsIndex = 0;
                //获取一个xml文件路径
                string path = GetFilePath(name, "xml");
                //读取XML文件用于比对
                xml = new EasyXml(path);
                xml.Load();
                //从加载好的所有布局集合中获取有指定名称的值
                SetCurrentLayout(name);
                //加载布局
                panel.LoadFromXml(path, new DeserializeDockContent(SetDockContent));
                xml.Dispose();
            }
            else
            {
                //不为空则观察是否需要更改布局
                if (currentLayout.Name != name)
                {
                    //关闭所有窗体
                    CloseAllForms(panel);
                    //装载前清零
                    currentLayoutFormsIndex = 0;
                    //获取一个xml文件路径
                    string path = GetFilePath(name, "xml");
                    //读取XML文件用于比对
                    xml = new EasyXml(path);
                    xml.Load();
                    //从加载好的所有布局集合中获取有指定名称的值
                    SetCurrentLayout(name);
                    //加载布局
                    panel.LoadFromXml(path, new DeserializeDockContent(SetDockContent));
                    xml.Dispose();
                }
                else
                {
                    MBox.ShowWarning("请勿重复设置布局！");
                }
            }
        }

        /// <summary>
        /// 保存XML文件
        /// </summary>
        /// <param name="panel">停靠板</param>
        /// <param name="name">布局名</param>
        internal static void SaveXMLFile(DockPanel panel, string name)
        {
            //检查布局是否存在
            if (layouts.ContainsKey(name))
            {
                if (MBox.Showlog("布局已存在是否覆盖？") == System.Windows.Forms.DialogResult.OK)
                {
                    //生成窗口名列表
                    CreateLayoutFormsFile(panel, name);
                    //保存布局文件
                    panel.SaveAsXml(GetFilePath(name, "xml"));
                    //覆盖布局
                    layouts.Remove(name);
                    layouts.Add(name, GetLayout(layouts.Count, name));
                    MBox.ShowInfo("保存成功");
                }
            }
            else
            {
                //生成窗口名列表
                CreateLayoutFormsFile(panel, name);
                //保存布局文件
                panel.SaveAsXml(GetFilePath(name, "xml"));
                //添加布局
                layouts.Add(name, GetLayout(layouts.Count, name));
                MBox.ShowInfo("保存成功");
            }
            SetCurrentLayout(name);
        }

        /// <summary>
        /// 删除指定名称的布局
        /// </summary>
        /// <param name="name"></param>
        internal static void DeleteLayout(string name)
        {
            //删除布局文件
            File.Delete(GetFilePath(name, "xml"));
            File.Delete(GetFilePath(name, "layoutForms"));
            //从列表中移除布局
            layouts.Remove(name);
            MBox.ShowInfo(name + "布局已删除");
        }

        /// <summary>
        /// 重命名布局
        /// </summary>
        /// <param name="name">原始名称</param>
        /// <param name="newName">新名称</param>
        internal static void ReNameLayout(string name, string newName)
        {
            //保存布局
            Layout layout = layouts[name];
            //移除布局
            layouts.Remove(name);
            //添加布局
            layouts.Add(newName, layout);
            FileManager.RenameFile(new FileInfo(GetFilePath(name, "xml")), newName, "xml");
            FileManager.RenameFile(new FileInfo(GetFilePath(name, "layoutForms")), newName, "layoutForms");
        }
    }
}
using AlgodooStudio.Attribute;
using AlgodooStudio.Base;
using AlgodooStudio.Window.Dialogs;
using AlgodooStudio.Window.Style;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using Zero.Core.FileAndDirectory.FileTools;
using Zero.Core.XML;

namespace AlgodooStudio.Manager
{
    /// <summary>
    /// 主题管理器
    /// </summary>
    internal static class ThemeManager
    {
        /// <summary>
        /// 当前可以直接用于使用的主题
        /// </summary>
        private static Theme currentTheme;

        /// <summary>
        /// 用于控制读写现有主题的XML文件
        /// </summary>
        private static EasyXml themeXML;

        /// <summary>
        /// 主题文件夹(自带斜杠哈)
        /// </summary>
        private const string themeFolder = themeFolderNo + "\\";

        /// <summary>
        /// 主题文件夹(不带斜杠哈)
        /// </summary>
        private const string themeFolderNo = "Themes";

        /// <summary>
        /// 字符串合并器
        /// </summary>
        private static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 读取进来的全部主题
        /// </summary>
        private static Dictionary<string, Theme> themes = new Dictionary<string, Theme>();

        /// <summary>
        /// 文件查询器
        /// </summary>
        private static FileSearcher fs = new FileSearcher(themeFolderNo);

        /// <summary>
        /// 当前可以用于应用的主题
        /// </summary>
        internal static Theme CurrentTheme { get => currentTheme; }

        /// <summary>
        /// 默认主题
        /// </summary>
        [XmlSerialize]
        internal static Theme DefaultTheme
        {
            get
            {
                Theme t = new Theme(-1, "Default");
                t.SetBorderColor(Color.FromArgb(70, 70, 70));
                t.SetBackColor1(Color.FromArgb(25, 25, 25));
                t.SetBackColor2(Color.FromArgb(45, 45, 45));
                t.SetBackColor3(Color.FromArgb(35, 35, 35));
                t.SetItemBackColor(Color.FromArgb(100, 100, 100));
                t.SetVarNameColor(Color.White);
                t.SetClassNameColor(Color.Red);
                t.SetKeywordsColor(Color.Yellow);
                t.SetOperatorColor(Color.FromArgb(150, 150, 150));
                t.SetNumberColor(Color.CadetBlue);
                t.SetStringColor(Color.Orange);
                t.SetMethodColor(Color.LawnGreen);
                t.SetParameterColor(Color.Aqua);
                t.SetWeifenluoTheme(new DarkAlgodooTheme());
                return t;
            }
        }

        #region 辅助性方法

        /// <summary>
        /// 从XML节点内容获取Weifenluo主题对象(一旦有新的主题记得来此注册)
        /// </summary>
        /// <param name="xmlContent">XML节点内容</param>
        /// <returns>weifenluo主题对象</returns>
        private static ThemeBase GetThemeBaseFromXML(string xmlContent)
        {
            switch (xmlContent)
            {
                case "VS2015BlueTheme":
                    return new VS2015BlueTheme();

                case "VS2015DarkTheme":
                    return new VS2015DarkTheme();

                case "VS2015LightTheme":
                    return new VS2015LightTheme();

                default:
                    return new VS2015DarkTheme();
            }
        }//√

        /// <summary>
        /// 从文件名获取主题名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>主题名称</returns>
        private static string GetThemeNameFromFileName(string fileName)
        {
            return fileName.Substring(0, fileName.Length - 8);
        }//√

        /// <summary>
        /// 获取指定名称的主题文件路径
        /// </summary>
        /// <param name="themeName">主题名称</param>
        private static string GetThemeFilePath(string themeName)
        {
            sb.Clear();
            sb.Append(themeFolder);
            sb.Append(themeName);
            sb.Append(".astheme");
            return sb.ToString();
        }//√

        /// <summary>
        /// 将指定Color颜色转换成XML属性列表
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>XML属性列表</returns>
        private static List<Xml_Attb> GetColorRGBAToAttributeList(Color color)
        {
            List<Xml_Attb> attbs = new List<Xml_Attb>();
            attbs.Add(new Xml_Attb("R", color.R.ToString()));
            attbs.Add(new Xml_Attb("G", color.G.ToString()));
            attbs.Add(new Xml_Attb("B", color.B.ToString()));
            attbs.Add(new Xml_Attb("A", color.A.ToString()));
            return attbs;
        }//√

        /// <summary>
        /// 从XML文件中获取主题文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>主题</returns>
        private static Theme GetThemeFromXMLFile(string filePath)
        {
            Theme t = new Theme(0);
            themeXML = new EasyXml(filePath, false);
            foreach (var item in themeXML.RootNode.ChildNodes)
            {
                switch (item.Name)
                {
                    case "identites":
                        t = new Theme(themes.Count + 1, item.ChildNodes[1].Content);
                        break;

                    case "properties":
                        foreach (var prop in item.ChildNodes)
                        {
                            switch (prop.Name)
                            {
                                case "borderColor":
                                    t.SetBorderColor(GetColorFormXMLNode(prop));
                                    break;

                                case "backColor1":
                                    t.SetBackColor1(GetColorFormXMLNode(prop));
                                    break;

                                case "backColor2":
                                    t.SetBackColor2(GetColorFormXMLNode(prop));
                                    break;

                                case "backColor3":
                                    t.SetBackColor3(GetColorFormXMLNode(prop));
                                    break;

                                case "itemBackColor":
                                    t.SetItemBackColor(GetColorFormXMLNode(prop));
                                    break;

                                case "varNameColor":
                                    t.SetVarNameColor(GetColorFormXMLNode(prop));
                                    break;

                                case "classNameColor":
                                    t.SetClassNameColor(GetColorFormXMLNode(prop));
                                    break;

                                case "keywordsColor":
                                    t.SetKeywordsColor(GetColorFormXMLNode(prop));
                                    break;

                                case "operatorColor":
                                    t.SetOperatorColor(GetColorFormXMLNode(prop));
                                    break;

                                case "numberColor":
                                    t.SetNumberColor(GetColorFormXMLNode(prop));
                                    break;

                                case "stringColor":
                                    t.SetStringColor(GetColorFormXMLNode(prop));
                                    break;

                                case "methodColor":
                                    t.SetMethodColor(GetColorFormXMLNode(prop));
                                    break;

                                case "parameterColor":
                                    t.SetParameterColor(GetColorFormXMLNode(prop));
                                    break;

                                case "weifenluoTheme":
                                    t.SetWeifenluoTheme(GetThemeBaseFromXML(prop.Content));//目前还在测试，如果仍为黑色则说明尚未修改
                                    break;
                            }
                        }
                        break;
                }
            }
            return t;
        }//√

        /// <summary>
        /// 从XML节点获取颜色
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static Color GetColorFormXMLNode(Xml_Node node)
        {
            byte R = byte.Parse(node.Attribute[0].Content);
            byte G = byte.Parse(node.Attribute[1].Content);
            byte B = byte.Parse(node.Attribute[2].Content);
            byte A = byte.Parse(node.Attribute[3].Content);
            return Color.FromArgb(A, R, G, B);
        }//√

        #endregion 辅助性方法

        /// <summary>
        /// 加载全部XML主题文件到集合中
        /// </summary>
        internal static void LoadXMLFiles()
        {
            //获取主题文件夹内的所有主题文件
            FileInfo[] files = fs.GetFilesFromThisFolder();
            foreach (var item in files)
            {
                themes.Add(GetThemeNameFromFileName(item.Name), GetThemeFromXMLFile(item.FullName));
            }
        }

        /// <summary>
        /// 应用主题
        /// </summary>
        internal static void ApplyTheme(string themeName)
        {
            //检查主题是否存在
            if (themes.ContainsKey(themeName))
            {
                //存在则将其应用到currentTheme中
                currentTheme = themes[themeName];
            }
            else
            {
                currentTheme = DefaultTheme;
                MBox.ShowError("具有该名称的主题不存在");
            }
        }

        /// <summary>
        /// 保存指定主题成XML文件
        /// </summary>
        internal static void SaveToXMLFile(Theme theme)
        {
            themeXML = EasyXml.Create(GetThemeFilePath(theme.Name), "theme");
            //主题身份属性
            Xml_Node identites = new Xml_Node("identites");
            identites.AddNodeToChild("id", theme.ID.ToString());//ID
            identites.AddNodeToChild("name", theme.Name);//名称
            //主题属性
            Xml_Node properties = new Xml_Node("properties");
            properties.AddNodeToChild("borderColor", "", GetColorRGBAToAttributeList(theme.BorderColor));
            properties.AddNodeToChild("backColor1", "", GetColorRGBAToAttributeList(theme.BackColor1));
            properties.AddNodeToChild("backColor2", "", GetColorRGBAToAttributeList(theme.BackColor2));
            properties.AddNodeToChild("backColor3", "", GetColorRGBAToAttributeList(theme.BackColor3));
            properties.AddNodeToChild("itemBackColor", "", GetColorRGBAToAttributeList(theme.ItemBackColor));
            properties.AddNodeToChild("varNameColor", "", GetColorRGBAToAttributeList(theme.VarNameColor));
            properties.AddNodeToChild("classNameColor", "", GetColorRGBAToAttributeList(theme.ClassNameColor));
            properties.AddNodeToChild("keywordsColor", "", GetColorRGBAToAttributeList(theme.KeywordsColor));
            properties.AddNodeToChild("operatorColor", "", GetColorRGBAToAttributeList(theme.OperatorColor));
            properties.AddNodeToChild("numberColor", "", GetColorRGBAToAttributeList(theme.NumberColor));
            properties.AddNodeToChild("stringColor", "", GetColorRGBAToAttributeList(theme.StringColor));
            properties.AddNodeToChild("methodColor", "", GetColorRGBAToAttributeList(theme.MethodColor));
            properties.AddNodeToChild("parameterColor", "", GetColorRGBAToAttributeList(theme.ParameterColor));
            properties.AddNodeToChild("weifenluoTheme", theme.WeifenluoTheme.GetType().Name);
            themeXML.RootNode.AddNodeToChild(identites);
            themeXML.RootNode.AddNodeToChild(properties);
            themeXML.Save();
            themeXML.Dispose();
        }//√

        /// <summary>
        /// 保存所有主题成XML文件
        /// </summary>
        internal static void SaveAllThemeToXMLFiles()
        {
            foreach (var item in themes)
            {
                SaveToXMLFile(item.Value);
            }
        }
    }
}
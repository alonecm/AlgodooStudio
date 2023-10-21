using AlgodooStudio.Attribute;
using AlgodooStudio.Base;
using AlgodooStudio.Manager;
using System.IO;

namespace AlgodooStudio
{
    /// <summary>
    /// 设置
    /// </summary>
    [XmlSerialize]
    internal static class Setting
    {
        /*
         * 规范性设定:
         * 1.地址结尾都是无反斜杠的
         * **/


        #region 辅助性字段
        /// <summary>
        /// 剪贴操作完成
        /// </summary>
        internal static bool isCopyFinished = true;
        /// <summary>
        /// 操作是否为剪切
        /// </summary>
        internal static bool isCutting = false;
        #endregion

        #region 需保存字段
        /// <summary>
        /// 主题
        /// </summary>
        [XmlSerialize]
        internal static Theme theme = ThemeManager.DefaultTheme;
        /// <summary>
        /// 布局名称
        /// </summary>
        [XmlSerialize]
        internal static string layoutName = "Default";
        /// <summary>
        /// Algodoo根目录
        /// </summary>
        [XmlSerialize]
        internal static string algodooRootPath = "D:\\Algodoo";
        /// <summary>
        /// 工作室目录
        /// </summary>
        [XmlSerialize]
        internal static string studioPath = Directory.GetCurrentDirectory();
        /// <summary>
        /// 用于启动的应用程序集合
        /// </summary>
        [XmlSerialize]
        internal static Container<App> executeApp = new Container<App>(new App(algodooRootPath + "\\Algodoo.exe"));
        /// <summary>
        /// 是否需要重置algodoo
        /// </summary>
        [XmlSerialize]
        internal static string isResetAlgodoo = "false";
        #endregion

        #region 无需保存的字段
        /// <summary>
        /// 脚本文件注册路径
        /// </summary>
        internal static string scriptPathRegistFile = algodooRootPath + "reg.asm";
        /// <summary>
        /// 自启动文件路径
        /// </summary>
        internal static string autoexecFile = algodooRootPath + "autoexec.cfg";
        #endregion

        #region 为不能序列化的值类型字段创建的属性
        /// <summary>
        /// 是否需要重置algodoo
        /// </summary>
        internal static bool IsResetAlgodoo
        {
            get
            {
                return bool.Parse(isResetAlgodoo);
            }
            set
            {
                //如果配置发生变动则保存，未变动则不保存
                if (bool.Parse(isResetAlgodoo) != value)
                {
                    isResetAlgodoo = value.ToString();
                    Program.SettingSave();
                }
            }
        }
        #endregion
    }
}
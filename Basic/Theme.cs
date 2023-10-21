using AlgodooStudio.Interface;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Basic
{
    /// <summary>
    /// 主题
    /// </summary>
    public sealed class Theme : IBasicInformation
    {
        /// <summary>
        /// 主题编号
        /// </summary>
        private int id;

        /// <summary>
        /// 主题名称
        /// </summary>
        private string name;

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color borderColor;

        /// <summary>
        /// 背景色1
        /// </summary>
        private Color backColor1;

        /// <summary>
        /// 背景色2
        /// </summary>
        private Color backColor2;

        /// <summary>
        /// 背景色2
        /// </summary>
        private Color backColor3;

        /// <summary>
        /// 部件背景色
        /// </summary>
        private Color itemBackColor;

        /// <summary>
        /// 变量名颜色
        /// </summary>
        private Color varNameColor;

        /// <summary>
        /// 类名颜色
        /// </summary>
        private Color classNameColor;

        /// <summary>
        /// 关键词颜色
        /// </summary>
        private Color keywordsColor;

        /// <summary>
        /// 运算符颜色
        /// </summary>
        private Color operatorColor;

        /// <summary>
        /// 数字颜色
        /// </summary>
        private Color numberColor;

        /// <summary>
        /// 字符串颜色
        /// </summary>
        private Color stringColor;

        /// <summary>
        /// 方法名颜色
        /// </summary>
        private Color methodColor;

        /// <summary>
        /// 方法参数颜色
        /// </summary>
        private Color parameterColor;

        /// <summary>
        /// 主题
        /// </summary>
        private ThemeBase weifenluoTheme;

        /// <summary>
        /// 新建一个基本主题
        /// </summary>
        /// <param name="id">布局编号</param>
        public Theme(int id)
        {
            this.id = id;
            this.name = "Theme" + id;
        }

        /// <summary>
        /// 新建一个基本主题
        /// </summary>
        /// <param name="id">布局编号</param>
        /// <param name="name">布局名称</param>
        public Theme(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// 主题编号
        /// </summary>
        public int ID { get => id; }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get => borderColor; }

        /// <summary>
        /// 部件背景色
        /// </summary>
        public Color ItemBackColor { get => itemBackColor; }

        /// <summary>
        /// 变量名颜色
        /// </summary>
        public Color VarNameColor { get => varNameColor; }

        /// <summary>
        /// 类名颜色
        /// </summary>
        public Color ClassNameColor { get => classNameColor; }

        /// <summary>
        /// 关键词颜色
        /// </summary>
        public Color KeywordsColor { get => keywordsColor; }

        /// <summary>
        /// 运算符颜色
        /// </summary>
        public Color OperatorColor { get => operatorColor; }

        /// <summary>
        /// 数字颜色
        /// </summary>
        public Color NumberColor { get => numberColor; }

        /// <summary>
        /// 字符串颜色
        /// </summary>
        public Color StringColor { get => stringColor; }

        /// <summary>
        /// 方法名颜色
        /// </summary>
        public Color MethodColor { get => methodColor; }

        /// <summary>
        /// 方法参数颜色
        /// </summary>
        public Color ParameterColor { get => parameterColor; }

        /// <summary>
        /// Weifenluo主题
        /// </summary>
        public ThemeBase WeifenluoTheme { get => weifenluoTheme; }

        /// <summary>
        /// 背景色1
        /// </summary>
        public Color BackColor1 { get => backColor1; }
        /// <summary>
        /// 背景色2
        /// </summary>
        public Color BackColor2 { get => backColor2; }
        /// <summary>
        /// 背景色3
        /// </summary>
        public Color BackColor3 { get => backColor3; }

        /// <summary>
        /// 设定边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetBorderColor(Color color)
        {
            borderColor = color;
        }

        /// <summary>
        /// 设定背景色1
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetBackColor1(Color color)
        {
            backColor1 = color;
        }

        /// <summary>
        /// 设定背景色2
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetBackColor2(Color color)
        {
            backColor2 = color;
        }

        /// <summary>
        /// 设定背景色3
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetBackColor3(Color color)
        {
            backColor3 = color;
        }

        /// <summary>
        /// 设定部件背景色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetItemBackColor(Color color)
        {
            itemBackColor = color;
        }

        /// <summary>
        /// 设定变量名颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetVarNameColor(Color color)
        {
            varNameColor = color;
        }

        /// <summary>
        /// 设定类名颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetClassNameColor(Color color)
        {
            classNameColor = color;
        }

        /// <summary>
        /// 设定关键词颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetKeywordsColor(Color color)
        {
            keywordsColor = color;
        }

        /// <summary>
        /// 设定运算符颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetOperatorColor(Color color)
        {
            operatorColor = color;
        }

        /// <summary>
        /// 设定数字颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetNumberColor(Color color)
        {
            numberColor = color;
        }

        /// <summary>
        /// 设定字符串颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetStringColor(Color color)
        {
            stringColor = color;
        }

        /// <summary>
        /// 设定方法名颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetMethodColor(Color color)
        {
            methodColor = color;
        }

        /// <summary>
        /// 设定方法参数颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetParameterColor(Color color)
        {
            parameterColor = color;
        }

        /// <summary>
        /// 设定weifenluo主题
        /// </summary>
        /// <param name="weifenluoTheme"></param>
        public void SetWeifenluoTheme(ThemeBase weifenluoTheme)
        {
            this.weifenluoTheme = weifenluoTheme;
        }
    }
}
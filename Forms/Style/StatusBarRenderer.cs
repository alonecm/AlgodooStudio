using AlgodooStudio.Basic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgodooStudio.Forms.Style
{
    /// <summary>
    /// StatusBar渲染器
    /// </summary>
    internal class StatusBarRenderer : ToolStripRenderer
    {
        /// <summary>
        /// 文字颜色1
        /// </summary>
        private Color textColor1;
        /// <summary>
        /// 文字颜色2
        /// </summary>
        private Color textColor2;
        /// <summary>
        /// 按钮底色
        /// </summary>
        private Color buttonColor;
        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color borderColor;


        /// <summary>
        /// 文字颜色1
        /// </summary>
        internal Color TextColor1 { get => textColor1; }
        /// <summary>
        /// 文字颜色2
        /// </summary>
        internal Color TextColor2 { get => textColor2; }
        /// <summary>
        /// 按钮底色
        /// </summary>
        internal Color ButtonColor { get => buttonColor; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        internal Color BorderColor { get => borderColor; }


        /// <summary>
        /// 设置文字颜色1
        /// </summary>
        /// <param name="color"></param>
        internal void SetTextColor1(Color color)
        {
            textColor1 = color;
        }
        /// <summary>
        /// 设置文字颜色2
        /// </summary>
        /// <param name="color"></param>
        internal void SetTextColor2(Color color)
        {
            textColor2 = color;
        }
        /// <summary>
        /// 设定按钮底色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetButtonColor(Color color)
        {
            buttonColor = color;
        }
        /// <summary>
        /// 设定边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetBorderColor(Color color)
        {
            borderColor = color;
        }

        /// <summary>
        /// 绘制选项文字颜色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.TextColor = textColor1;
            }
            else
            {
                e.TextColor = textColor2;
            }
            base.OnRenderItemText(e);
        }
        /// <summary>
        /// 绘制按钮背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            SolidBrush br;
            //如果鼠标在按钮上
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(br = new SolidBrush(buttonColor), e.Item.ContentRectangle);
            }
            else
            {
                e.Graphics.FillRectangle(br = new SolidBrush(Color.Transparent), e.Item.ContentRectangle);
            }
            br.Dispose();

            base.OnRenderButtonBackground(e);
        }
        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Rectangle tmp = e.AffectedBounds;
            tmp.X--;
            tmp.Width++;
            Pen p = new Pen(borderColor);
            e.Graphics.DrawRectangle(p, tmp);
            p.Dispose();
            base.OnRenderToolStripBorder(e);
        }


        /// <summary>
        /// 根据指定主题获取渲染器
        /// </summary>
        /// <param name="theme">主题</param>
        /// <returns>生成的渲染器</returns>
        public static StatusBarRenderer GetRenderer(Theme theme)
        {
            StatusBarRenderer sbr = new StatusBarRenderer();
            sbr.SetTextColor1(theme.KeywordsColor);
            sbr.SetTextColor2(theme.VarNameColor);
            sbr.SetBorderColor(theme.BorderColor);
            sbr.SetButtonColor(theme.ItemBackColor);
            return sbr;
        }
        /// <summary>
        /// 根据设置里的主题获取渲染器
        /// </summary>
        /// <returns>生成的渲染器</returns>
        public static StatusBarRenderer GetRenderer()
        {
            return GetRenderer(Setting.theme);
        }
    }
}

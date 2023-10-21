using AlgodooStudio.Base;
using System.Drawing;
using System.Windows.Forms;

namespace AlgodooStudio.Window.Style
{
    /// <summary>
    /// 主题用工具菜单渲染器
    /// </summary>
    internal class ThemeToolStripRenderer : ToolStripRenderer
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
        /// 边框颜色
        /// </summary>
        private Color borderColor;

        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color backgroundColor;

        /// <summary>
        /// 图片背景色
        /// </summary>
        private Color imageBackgroundColor;

        /// <summary>
        /// 菜单项颜色
        /// </summary>
        private Color menuItemColor;

        /// <summary>
        /// 文字颜色1
        /// </summary>
        internal Color TextColor1 { get => textColor1; }

        /// <summary>
        /// 文字颜色2
        /// </summary>
        internal Color TextColor2 { get => textColor2; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        internal Color BorderColor { get => borderColor; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        internal Color BackgroundColor { get => backgroundColor; }

        /// <summary>
        /// 图片背景色
        /// </summary>
        internal Color ImageBackgroundColor { get => imageBackgroundColor; }

        /// <summary>
        /// 菜单项颜色
        /// </summary>
        internal Color MenuItemColor { get => menuItemColor; }

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
        /// 设置边框颜色
        /// </summary>
        /// <param name="color"></param>
        internal void SetBorderColor(Color color)
        {
            borderColor = color;
        }

        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="color"></param>
        internal void SetBackgroundColor(Color color)
        {
            backgroundColor = color;
        }

        /// <summary>
        /// 设置图片背景色
        /// </summary>
        /// <param name="color"></param>
        internal void SetImageBackgroundColor(Color color)
        {
            imageBackgroundColor = color;
        }

        /// <summary>
        /// 设置菜单项颜色
        /// </summary>
        /// <param name="color"></param>
        internal void SetMenuItemColor(Color color)
        {
            menuItemColor = color;
        }

        /// <summary>
        /// 绘制表单边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Pen p;
            Rectangle r = e.AffectedBounds;
            r.Height -= 1;
            r.Width -= 1;
            //如果鼠标在按钮上
            e.Graphics.DrawRectangle(p = new Pen(borderColor), r);
            p.Dispose();
            base.OnRenderToolStripBorder(e);
        }

        /// <summary>
        /// 绘制表单背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            SolidBrush br;
            //如果鼠标在按钮上
            e.Graphics.FillRectangle(br = new SolidBrush(backgroundColor), e.AffectedBounds);
            br.Dispose();
            base.OnRenderToolStripBackground(e);
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
        /// 绘制图标
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Pen p;
            Rectangle r = e.ImageRectangle;
            //如果鼠标在按钮上
            e.Graphics.DrawRectangle(p = new Pen(imageBackgroundColor), r);
            p.Dispose();
            base.OnRenderItemImage(e);
        }

        /// <summary>
        /// 绘制菜单内容
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            SolidBrush sb;
            if (e.Item.Selected)
            {
                Rectangle r = e.Item.ContentRectangle;
                //如果鼠标在按钮上
                e.Graphics.FillRectangle(sb = new SolidBrush(menuItemColor), r);
            }
            else
            {
                Rectangle r = e.Item.Bounds;
                //如果鼠标在按钮上
                e.Graphics.FillRectangle(sb = new SolidBrush(Color.Transparent), r);
            }
            sb.Dispose();
            base.OnRenderMenuItemBackground(e);
        }

        /// <summary>
        /// 绘制分离器
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            SolidBrush br;
            Rectangle rt = e.Item.ContentRectangle;
            int length = 5;
            rt.X += length;
            rt.Width -= length * 2;
            rt.Height /= 2;
            //如果鼠标在按钮上
            e.Graphics.FillRectangle(br = new SolidBrush(borderColor), rt);
            br.Dispose();
        }

        internal static ThemeToolStripRenderer GetRenderer(Theme theme)
        {
            ThemeToolStripRenderer ttsr = new ThemeToolStripRenderer();
            ttsr.SetTextColor1(theme.KeywordsColor);
            ttsr.SetTextColor2(theme.VarNameColor);
            ttsr.SetBorderColor(theme.BorderColor);
            ttsr.SetBackgroundColor(theme.BackColor3);
            ttsr.SetMenuItemColor(theme.BackColor1);
            ttsr.SetImageBackgroundColor(theme.StringColor);
            return ttsr;
        }

        internal static ThemeToolStripRenderer GetRenderer()
        {
            return GetRenderer(Setting.theme);
        }

        #region 无效方法

        //protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        //{无效
        //    base.OnRenderItemBackground(e);
        //}

        //protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        //{无效
        //    base.OnRenderItemCheck(e);
        //}

        //protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        //{无效
        //    base.OnRenderButtonBackground(e);
        //}

        //protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        //{无效
        //    base.OnRenderDropDownButtonBackground(e);
        //}

        //protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        //{无效
        //    base.OnRenderOverflowButtonBackground(e);
        //}

        //protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        //{无效
        //    base.OnRenderToolStripContentPanelBackground(e);
        //}

        #endregion 无效方法
    }
}
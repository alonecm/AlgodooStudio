using AlgodooStudio.Basic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AlgodooStudio.Forms.Style
{
    /// <summary>
    /// QuickTools渲染器
    /// </summary>
    internal class QuickToolsRenderer : ToolStripRenderer
    {
        /// <summary>
        /// 手柄颜色/按钮底色
        /// </summary>
        private Color gripColor;
        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color borderColor;


        /// <summary>
        /// 手柄颜色/按钮底色
        /// </summary>
        internal Color GripColor { get => gripColor; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        internal Color BorderColor { get => borderColor; }


        /// <summary>
        /// 设定手柄、按钮底色
        /// </summary>
        /// <param name="color">颜色</param>
        internal void SetGripColor(Color color)
        {
            gripColor = color;
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
        /// 绘制手柄
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            Pen p = new Pen(gripColor);
            p.Width = e.GripBounds.Width/1.5f;
            p.DashStyle = DashStyle.Dot;
            e.Graphics.DrawLine(p, e.GripBounds.X, e.GripBounds.Y, e.GripBounds.X, e.GripBounds.Bottom);
            p.Dispose();
            base.OnRenderGrip(e);
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
                e.Graphics.FillRectangle(br = new SolidBrush(gripColor), e.Item.ContentRectangle);
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
            tmp.Y--;
            tmp.Width++;
            Pen p = new Pen(borderColor);
            e.Graphics.DrawRectangle(p, tmp);
            p.Dispose();
            base.OnRenderToolStripBorder(e);
        }
        /// <summary>
        /// 绘制分离器
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            SolidBrush br;
            Rectangle rt = e.Item.ContentRectangle;
            rt.Width = (int)(rt.Width / 1.2f);
            rt.Height = (int)(rt.Height / 1.2f);
            rt.Y += 2;
            //如果鼠标在按钮上
            e.Graphics.FillRectangle(br = new SolidBrush(gripColor), rt);
            rt.X -= 1;
            e.Graphics.FillRectangle(br = new SolidBrush(borderColor), rt);
            br.Dispose();
        }

        /// <summary>
        /// 根据指定主题获取渲染器
        /// </summary>
        /// <param name="theme">主题</param>
        /// <returns>生成的渲染器</returns>
        internal static QuickToolsRenderer GetRenderer(Theme theme)
        {
            QuickToolsRenderer qtr = new QuickToolsRenderer();
            qtr.SetBorderColor(theme.BorderColor);
            qtr.SetGripColor(theme.ItemBackColor);
            return qtr;
        }
        /// <summary>
        /// 根据设置里的主题获取渲染器
        /// </summary>
        /// <returns>生成的渲染器</returns>
        internal static QuickToolsRenderer GetRenderer()
        {
            return GetRenderer(Setting.theme);
        }
    }
}

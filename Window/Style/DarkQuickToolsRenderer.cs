using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgodooStudio.Forms.Style
{
    /// <summary>
    /// 暗色QuickTools渲染器
    /// </summary>
    internal class DarkQuickToolsRenderer : ToolStripRenderer
    {
        /// <summary>
        /// 绘制手柄
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {

            Pen p = new Pen(Color.FromArgb(100, 100, 100));
            p.Width = e.GripBounds.Width;
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
                e.Graphics.FillRectangle(br = new SolidBrush(Color.FromArgb(100,100,100)), e.Item.ContentRectangle);
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
            Pen p = new Pen(Color.FromArgb(70, 70, 70));
            e.Graphics.DrawRectangle(p, tmp);
            p.Dispose();
            base.OnRenderToolStripBorder(e);
        }
    }
}

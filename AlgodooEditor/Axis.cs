using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 坐标轴
    /// </summary>
    public class Axis : IDrawable
    {
        private DexCanvas canvas;

        public Axis(DexCanvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="g"></param>
        public void Drawing(Graphics g, DexCanvas canvas)
        {
            var v = canvas.Camera.Viewport;
            var vP1 = new Point(0, v.Y);
            var vP2 = new Point(0, v.Y + v.Height);
            g.DrawLine(canvas.PaintFactory.GetPen(Color.White,2), canvas.Camera.WorldToScreen(vP1), canvas.Camera.WorldToScreen(vP2));
            var hP1 = new Point(v.Left, 0);
            var hP2 = new Point(v.Left + v.Width, 0);
            g.DrawLine(canvas.PaintFactory.GetPen(Color.White, 2), canvas.Camera.WorldToScreen(hP1), canvas.Camera.WorldToScreen(hP2));
        }

    }
}

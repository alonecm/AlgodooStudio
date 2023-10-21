using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 绘图工具类
    /// </summary>
    public static class DrawTools
    {
        /// <summary>
        /// 整数系统矩形转换成浮点数系统矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectangleF ToRectangleF(Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }
        /// <summary>
        /// 浮点数系统矩形转换成整数系统矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rectangle ToRectangle(RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
        /// <summary>
        /// 屏幕坐标转世界坐标
        /// </summary>
        /// <param name="zeroPoint">零点坐标在屏幕上的位置</param>
        /// <param name="screenPoint">屏幕坐标</param>
        /// <param name="zoom">缩放比</param>
        /// <returns></returns>
        public static PointF ScreenToWorld(Point zeroPoint, Point screenPoint, float zoom)
        {
            return new PointF((float)Math.Round((screenPoint.X - zeroPoint.X) / zoom, 3), (float)Math.Round((screenPoint.Y - zeroPoint.Y) / zoom, 3));
        }
        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        /// <param name="zeroPoint">零点坐标在屏幕上的位置</param>
        /// <param name="worldPoint">世界坐标</param>
        /// <param name="zoom">缩放比</param>
        /// <returns></returns>
        public static Point WorldToScreen(Point zeroPoint, PointF worldPoint, float zoom)
        {
            return new Point((int)(worldPoint.X * zoom) + zeroPoint.X, (int)(worldPoint.Y * zoom) + zeroPoint.Y);
        }
        /// <summary>
        /// 世界矩形转换为屏幕矩形
        /// </summary>
        public static Rectangle WorldToScreen(Point zeroPoint, Rectangle rect, float zoom)
        {
            //要清晰的确定本地和世界的关系
            var r = new Rectangle(rect.Location, rect.Size);

            //计算缩放坐标
            r.Location = WorldToScreen(zeroPoint, r.Location, zoom);
            
            //调整矩形大小
            r.Width = (int)Math.Round(rect.Width * zoom, 0);
            r.Height = (int)Math.Round(rect.Height * zoom, 0);

            return r;
        }
    }
}

using AlgodooStudio.Base;
using System;
using System.Drawing;
using System.Linq.Expressions;
using Zero.Core.Mathematics;

namespace AlgodooStudio.Utils
{
    /// <summary>
    /// 绘图助手
    /// </summary>
    internal static class DrawHelper
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
        /// 将平面点转换成绘图点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point ToPoint(Point2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
        /// <summary>
        /// 将平面点转换成绘图点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static PointF ToPointF(Point2 point)
        {
            return new PointF(point.X, point.Y);
        }
        /// <summary>
        /// 整数点转浮点数点
        /// </summary>
        /// <param name="point">整数点</param>
        /// <returns>浮点数点</returns>
        public static PointF ToPointF(Point point)
        {
            return new PointF(point.X, point.Y);
        }
        /// <summary>
        /// 浮点数点转整数点
        /// </summary>
        /// <param name="point">浮点数点</param>
        /// <returns>整数点</returns>
        public static Point ToPoint(PointF point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
        /// <summary>
        /// 将绘图点转换成平面点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point2 ToPoint2(Point point)
        {
            return new Point2(point.X, point.Y);
        }
        /// <summary>
        /// 将绘图点转换成平面点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point2 ToPoint2(PointF point)
        {
            return new Point2(point.X, point.Y);
        }
        /// <summary>
        /// 浮点数点集转整数点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point[] ToPoints(PointF[] points)
        {
            Container<Point> tmp = new Container<Point>();
            foreach (var item in points)
            {
                tmp.Add(new Point((int)item.X, (int)item.Y));
            }
            return tmp;
        }
        /// <summary>
        /// 整数点集转浮点数点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF[] ToPointFs(Point[] points)
        {
            Container<PointF> tmp = new Container<PointF>();
            foreach (var item in points)
            {
                tmp.Add(new PointF(item.X, item.Y));
            }
            return tmp;
        }
        /// <summary>
        /// 将平面点集转换成绘图点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point[] ToPoints(Point2[] points)
        {
            Container<Point> tmp = new Container<Point>();
            foreach (var item in points)
            {
                tmp.Add(new Point((int)item.X, (int)item.Y));
            }
            return tmp;
        }
        /// <summary>
        /// 将平面点集转换成绘图点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF[] ToPointFs(Point2[] points)
        {
            Container<PointF> tmp = new Container<PointF>();
            foreach (var item in points)
            {
                tmp.Add(new PointF(item.X, item.Y));
            }
            return tmp;
        }
        /// <summary>
        /// 将绘图点集转换成平面点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2[] ToPoint2s(Point[] points)
        {
            Container<Point2> tmp = new Container<Point2>();
            foreach (var item in points)
            {
                tmp.Add(new Point((int)item.X, (int)item.Y));
            }
            return tmp;
        }
        /// <summary>
        /// 将绘图点集转换成平面点集
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2[] ToPoint2s(PointF[] points)
        {
            Container<Point2> tmp = new Container<Point2>();
            foreach (var item in points)
            {
                tmp.Add(new Point2(item.X, item.Y));
            }
            return tmp;
        }


        /// <summary>
        /// 将点集移动到<paramref name="newCenter"/>的周围
        /// </summary>
        /// <param name="newCenter">新的中心点</param>
        /// <param name="points">点集</param>
        /// <returns>移动后的点集</returns>
        public static Point[] MovePoints(Point newCenter, Point[] points)
        {
            Point[] ps = points;
            //获取点集的中心点
            Point center = GetCenter(points);
            //移动点集
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i] += new Size(newCenter.X-center.X,newCenter.Y-center.Y);
            }
            return ps;
        }
        /// <summary>
        /// 获取指定点集的中心点
        /// </summary>
        /// <param name="points">指定点集</param>
        /// <returns>点集中心点</returns>
        public static Point GetCenter(Point[] points)
        {
            Rectangle rectangle = GetRectangle(points);
            //开始定义的那会y坐标是加的，这里就得用减的
            return rectangle.Location + new Size(rectangle.Width >> 1,-rectangle.Height >> 1);
        }
        /// <summary>
        /// 获取指定点集的外包矩形
        /// </summary>
        /// <param name="points">需要求外包矩形的点集</param>
        /// <returns>外包矩形</returns>
        public static Rectangle GetRectangle(Point[] points)
        {
            //获取边框最大值点和最小值点
            var maxX = points[0].X;
            var maxY = points[0].Y;
            var minX = points[0].X;
            var minY = points[0].Y;
            foreach (var vertex in points)
            {
                maxX = Math.Max(maxX, vertex.X);
                maxY = Math.Max(maxY, vertex.Y);
                minX = Math.Min(minX, vertex.X);
                minY = Math.Min(minY, vertex.Y);
            }
            int width = maxX - minX;
            int height = maxY - minY;
            //取maxY是为了让矩形左上角为起点向下展开
            return new Rectangle(minX, maxY, width, height);
        }


        /// <summary>
        /// 将世界矩形转换为屏幕矩形
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="worldRect">世界矩形</param>
        /// <returns>屏幕矩形</returns>
        public static Rectangle WorldToScreen(Point center, Rectangle worldRect, float zoom)
        {
            //世界坐标系使用笛卡尔坐标系
            return new Rectangle(WorldToScreen(center, worldRect.Location, zoom), new Size((int)(worldRect.Width * zoom), (int)(worldRect.Height * zoom)));
        }
        /// <summary>
        /// 将<paramref name="worldPoint"/>以<paramref name="center"/>为中心的世界坐标转换为屏幕坐标
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="worldPoint">世界坐标</param>
        /// <returns>世界坐标对应的屏幕坐标</returns>
        public static Point WorldToScreen(Point center, Point worldPoint, float zoom)
        {
            //世界坐标系使用笛卡尔坐标系
            return new Point((int)(center.X + worldPoint.X * zoom), (int)(center.Y - worldPoint.Y * zoom));
        }
        /// <summary>
        /// 将<paramref name="worldPoint"/>以<paramref name="center"/>为中心的世界坐标转换为屏幕坐标
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="worldPoint">世界坐标</param>
        /// <returns>世界坐标对应的屏幕坐标</returns>
        public static PointF WorldToScreen(PointF center, PointF worldPoint, float zoom)
        {
            //世界坐标系使用笛卡尔坐标系
            return new PointF(center.X + worldPoint.X * zoom, center.Y - worldPoint.Y * zoom);
        }
        /// <summary>
        /// 将<paramref name="worldPoints"/>以<paramref name="center"/>为中心的世界坐标转换为屏幕坐标点集
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="worldPoints">世界坐标点集</param>
        /// <returns>世界坐标点集对应的屏幕坐标点集</returns>
        public static Point[] WorldToScreen(Point center, Point[] worldPoints, float zoom)
        {
            Container<Point> ps = new Container<Point>();
            foreach (var item in worldPoints)
            {
                ps.Add(WorldToScreen(center, item, zoom));
            }
            return ps;
        }


        /// <summary>
        /// 将屏幕坐标<paramref name="screenPoint"/>转换为以<paramref name="center"/>为中心的世界坐标
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="screenPoint">屏幕坐标</param>
        /// <returns>屏幕坐标世对应的界坐标</returns>
        public static PointF ScreenToWorld(PointF center, PointF screenPoint, float zoom)
        {
            //世界坐标系使用笛卡尔坐标系
            return new PointF((screenPoint.X - center.X) / zoom, (center.Y - screenPoint.Y) / zoom);
        }
        /// <summary>
        /// 将屏幕坐标点集<paramref name="points"/>转换为以<paramref name="center"/>为中心的世界坐标点集
        /// </summary>
        /// <param name="center">世界中心点在屏幕上的坐标</param>
        /// <param name="points">屏幕坐标点集</param>
        /// <returns>屏幕坐标点集世对应的界坐标点集</returns>
        public static PointF[] ScreenToWorld(Point center, Point[] points, float zoom)
        {
            //世界坐标系使用笛卡尔坐标系
            Container<PointF> ps = new Container<PointF>();
            foreach (var item in points)
            {
                ps.Add(ScreenToWorld(center, item, zoom));
            }
            return ps;
        }


        /// <summary>
        /// 获取剪辑区域
        /// </summary>
        /// <param name="last">先前的矩形</param>
        /// <param name="now">现在的矩形</param>
        /// <returns>剪辑区域</returns>
        public static Rectangle GetCilpArea(Rectangle last, Rectangle now)
        {
            var x = Math.Min(last.X, now.X);
            var maxY = Math.Max(last.Y, now.Y);
            var minY = Math.Min(last.Y, now.Y);
            var width = Math.Max(last.Right, now.Right) - x + 10;
            var height = Math.Max(last.Bottom, now.Bottom) - minY + 10;
            return new Rectangle(x, maxY, width, height);
        }

        /// <summary>
        /// 获取以某点为中心的矩形
        /// </summary>
        /// <param name="pos">矩形中心点</param>
        /// <returns>创建的矩形</returns>
        public static Rectangle GetRectangleFromCenter(Point pos, Size size)
        {
            return new Rectangle(pos.X - size.Width / 2, pos.Y + size.Height / 2, size.Width, size.Height);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dex.Canvas
{
    public class Camera
    {
        /// <summary>
        /// 使用当前摄像机的场景编辑器
        /// </summary>
        private DexCanvas canvas;
        /// <summary>
        /// 最小比例
        /// </summary>
        private float MinZoom = 0.5f;
        /// <summary>
        /// 最大比例
        /// </summary>
        private float MaxZoom = 20;
        /// <summary>
        /// 上一个鼠标位置
        /// </summary>
        private Point lastMousePoint;

        /// <summary>
        /// 世界零点坐标（默认为画板中间）
        /// </summary>
        public Point WorldZero;//不能用属性，不然没法使用Offset之类函数
        /// <summary>
        /// 视口，当前用户可以看到的区域
        /// </summary>
        public Rectangle Viewport;//不能用属性，不然没法使用Offset之类函数
        /// <summary>
        /// 缩放比
        /// </summary>
        public float Zoom = 1;
        
        /// <summary>
        /// 建议创建摄像机之后在画板的Resize事件内重新调整摄像机视口的大小，并重新设置世界零点的位置
        /// </summary>
        /// <param name="editor"></param>
        public Camera(DexCanvas editor)
        {
            this.canvas = editor;
            //默认图纸坐标
            WorldZero = new Point(editor.Width / 2, editor.Height / 2);
            Viewport = new Rectangle(0 - WorldZero.X, 0 - WorldZero.Y, editor.Width, editor.Height);
        }
        
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="e"></param>
        internal void MouseDown(MouseEventArgs e)
        {
            lastMousePoint = e.Location;//记录用于移动摄像机的鼠标位置
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="e"></param>
        internal void MouseMove(MouseEventArgs e)
        {
            if (canvas.PressedButton==MouseButtons.Middle)
            {
                //鼠标中键移动摄像机(因为其坐标系是以窗口为基准所以用e.location)
                MoveCamera(e.Location - ((Size)lastMousePoint));
                lastMousePoint = e.Location;
            }
        }
        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        /// <param name="e"></param>
        internal void MouseUp(MouseEventArgs e)
        {
            lastMousePoint = Point.Empty;//清空上个点位
        }


        /// <summary>
        /// 可编辑对象否在摄像机的显示范围中（用于优化性能）
        /// </summary>
        /// <returns>在范围内返回true,不在则返回false</returns>
        public bool IsInView(EditableObject obj)
        {
            return WorldToScreen(Viewport).IntersectsWith(DrawTools.ToRectangle(obj.Mesh.GetBounds()));
        }
        /// <summary>
        /// 文字区域否在摄像机的显示范围中（用于优化性能）
        /// </summary>
        /// <returns>在范围内返回true,不在则返回false</returns>
        public bool IsInView(TextArea area)
        {
            return WorldToScreen(Viewport).IntersectsWith(DrawTools.ToRectangle(area.Area));
        }
        /// <summary>
        /// 设置缩放
        /// </summary>
        /// <param name="zoomMulti">缩放乘数</param>
        /// <param name="mousePos">鼠标位置</param>
        public void SetZoom(float zoomMulti, Point mousePos)
        {
            if (Zoom < MinZoom) Zoom = MinZoom;
            else if (Zoom > MaxZoom) Zoom = MaxZoom;
            else Zoom *= zoomMulti;

            //获取平移向量
            Point transZero = new Point((int)((mousePos.X - WorldZero.X) * (1 - zoomMulti)), (int)((mousePos.Y - WorldZero.Y) * (1 - zoomMulti)));

            //移动摄像机
            MoveCamera(transZero);

            //调整视口大小
            Viewport.Width = (int)(canvas.Width / Zoom);
            Viewport.Height = (int)(canvas.Height / Zoom);

            //刷新控件
            canvas.Refresh();
        }
        /// <summary>
        /// 移动摄像机
        /// </summary>
        /// <param name="vector">移动向量，一般是由移动目的地位置减去上次的所在位置</param>
        public void MoveCamera(Point vector)
        {
            //平移零点
            WorldZero.Offset(vector.X, vector.Y);
            //设定视口
            Viewport.X = (int)(-WorldZero.X / Zoom);
            Viewport.Y = (int)(-WorldZero.Y / Zoom);
        }
        /// <summary>
        /// 设置零点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetZero(int x, int y)
        {
            WorldZero.X = x;
            WorldZero.Y = y;
            //调整视口位置
            Viewport.X = (int)(-WorldZero.X / Zoom);
            Viewport.Y = (int)(-WorldZero.Y / Zoom);
        }
        /// <summary>
        /// 屏幕坐标转世界坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标</param>
        /// <returns>世界坐标</returns>
        public PointF ScreenToWorld(Point screenPoint)
        {
           return DrawTools.ScreenToWorld(WorldZero, screenPoint, Zoom);
        }
        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        /// <param name="worldPoint">世界坐标</param>
        /// <returns>屏幕坐标</returns>
        public Point WorldToScreen(Point worldPoint)
        {
            return DrawTools.WorldToScreen(WorldZero, worldPoint, Zoom);
        }
        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        /// <param name="worldPoint">世界坐标</param>
        /// <returns>屏幕坐标</returns>
        public PointF WorldToScreen(PointF worldPoint)
        {
            return DrawTools.WorldToScreen(WorldZero, worldPoint, Zoom);
        }
        /// <summary>
        /// 本地（图纸）矩形变换到显示矩形
        /// </summary>
        public Rectangle WorldToScreen(Rectangle rect)
        {
            //要清晰的确定本地和世界的关系
            var r = new Rectangle(rect.Location, rect.Size);

            //计算缩放坐标
            r.X = (int)(r.X * Zoom) + WorldZero.X;
            r.Y = (int)(r.Y * Zoom) + WorldZero.Y;

            //调整矩形大小
            r.Width = (int)Math.Round(rect.Width * Zoom, 0);
            r.Height = (int)Math.Round(rect.Height * Zoom, 0);

            return r;
        }
        /// <summary>
        /// 本地（图纸）矩形变换到显示矩形
        /// </summary>
        public RectangleF WorldToScreen(RectangleF rect)
        {
            //要清晰的确定本地和世界的关系
            var r = new RectangleF(rect.Location, rect.Size);

            //计算缩放坐标
            r.X = (r.X * Zoom) + WorldZero.X;
            r.Y = (r.Y * Zoom) + WorldZero.Y;

            //调整矩形大小
            r.Width = (float)Math.Round(rect.Width * Zoom, 0);
            r.Height = (float)Math.Round(rect.Height * Zoom, 0);

            return r;
        }
    }
}
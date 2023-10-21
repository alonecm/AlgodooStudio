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
    /// 可编辑对象基类
    /// </summary>
    public abstract class EditableObject : IDrawable, IDisposable
    {
        /// <summary>
        /// 最后一次创建的对象ID
        /// </summary>
        private static int lastObjectID = 0;
        private float angle = 0;
        private int id;
        private bool isImage = true;
        private bool isSelected = false;
        private bool isImageVisible = true;
        private Color color;
        private PointF pos;
        private DrawingType type;
        private Layer layer;
        private GraphicsPath shape = new GraphicsPath();
        private GraphicsPath mesh = new GraphicsPath();
        private Dictionary<string, IExtraProperty> properties = new Dictionary<string, IExtraProperty>();
        private Image image;


        /// <summary>
        /// 创建可编辑对象
        /// </summary>
        /// <param name="layer">需要添加到的图层</param>
        /// <param name="color">颜色</param>
        /// <param name="pos">对象中心点坐标，是场景坐标</param>
        /// <param name="type">类别，如果有图片则会自动判断</param>
        /// <param name="img">图片</param>
        /// <param name="properties">额外属性</param>
        protected EditableObject(Layer layer, Color color, PointF pos, DrawingType type, Image img = null, params IExtraProperty[] properties)
        {
            id = lastObjectID++;
            this.Layer = layer;
            this.Color = color;
            this.Pos = pos;
            this.Type = type;
            this.Image = img;
            shape.Reset();//重设形状
            foreach (var item in properties)
            {
                if (this.properties.ContainsKey(item.name))
                {
                    this.properties[item.name] = item;
                }
                else
                {
                    this.properties.Add(item.name, item);
                }
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get => id; }
        /// <summary>
        /// 获取或设置是否被选择
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!layer.IsLocked)
                {
                    isSelected = value;
                }
            }
        }
        /// <summary>
        /// 是否是图片
        /// </summary>
        public bool IsImage { get => isImage; }
        /// <summary>
        /// 获取或设置是否显示图片
        /// </summary>
        public bool IsImageVisible
        {
            get
            {
                return isImageVisible;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!layer.IsLocked)
                {
                    isImageVisible = value;
                }
            }
        }
        /// <summary>
        /// 获取或设置绘制类别
        /// </summary>
        public DrawingType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (!layer.IsLocked)
                {
                    this.type = value;
                    //类别是图片则标识是图片
                    if (type == DrawingType.Image)
                    {
                        isImage = true;
                    }
                    else
                    {
                        isImage = false;
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置颜色
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!layer.IsLocked)
                {
                    color = value;
                }
            }
        }
        /// <summary>
        /// 获取或设置对象中心位置
        /// </summary>
        public PointF Pos
        {
            get
            {
                return pos;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!layer.IsLocked)
                {
                    pos = value;
                }
            }
        }
        /// <summary>
        /// 可编辑对象所在图层
        /// </summary>
        public Layer Layer { get => layer; internal set => layer = value; }
        /// <summary>
        /// 外形，其中心点均为【0,0】不进行任何更改，任何实体都要手动校准到以该点为中心
        /// </summary>
        public GraphicsPath Shape { get => shape; }
        /// <summary>
        /// 可编辑对象
        /// </summary>
        public Scene Scene { get => layer.Scene; }
        /// <summary>
        /// 获取或设置图片
        /// </summary>
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                if (!layer.IsLocked)
                {
                    image = value;
                    if (value != null)
                    {

                        Type = DrawingType.Image;
                    }
                    else
                    {
                        Type = DrawingType.Rectangle;
                    }
                }
            }
        }
        /// <summary>
        /// 网格外形，用于在屏幕上进行显示的外形，所见大小与实际形状不一致
        /// </summary>
        public GraphicsPath Mesh { get => mesh; }
        /// <summary>
        /// 角度(360*N，记录了圈数和正反转)
        /// </summary>
        public float Angle { get => angle; }

        /// <summary>
        /// 移动对象
        /// </summary>
        /// <param name="vec">移动向量</param>
        public void Move(PointF vec)
        {
            pos += new SizeF(vec.X, vec.Y);
        }
        /// <summary>
        /// 移动对象
        /// </summary>
        /// <param name="vec">移动向量</param>
        public void Move(float dx, float dy)
        {
            pos += new SizeF(dx, dy);
        }
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="delta">大小变化量</param>
        /// <returns>缩放成功则返回true，否则返回false</returns>
        public bool Scale(SizeF delta)
        {
            return Scale(delta.Width, delta.Height);
        }
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="d_width">宽度变化量</param>
        /// <param name="d_height">高度变化量</param>
        /// <returns>缩放成功则返回true，否则返回false</returns>
        public bool Scale(float d_width, float d_height)
        {
            //圆形会变动为椭圆形
            if (type == DrawingType.Circle)
            {
                type = DrawingType.Ellipse;
            }
            //矩形有倾角则变成多边形
            if (type == DrawingType.Rectangle || type == DrawingType.Image)
            {
                if (angle != 0 && Math.Abs(angle) - 1.57079632679>0.01)
                {
                    type = DrawingType.Polygon;
                }
            }
            //获取外边框
            RectangleF r = shape.GetBounds();
            //定义新矩形的宽度
            float nWidth = d_width + r.Width;
            float nHeight = d_height + r.Height;
            if (type==DrawingType.Rectangle||type==DrawingType.Image)
            {
                ((Box)this).Size = new SizeF(nWidth, nHeight);
            }
            //比例
            float scaleX = nWidth / r.Width;
            float scaleY = nHeight / r.Height;

            if (nWidth > 2 && nHeight > 2)
            {
                using (Matrix m = new Matrix())
                {
                    m.Scale(scaleX, scaleY);//此时尚未变动中心点
                    shape.Transform(m);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="angle">转动角</param>
        /// <param name="center">旋转中心（对象坐标系下的）</param>
        public void Rotate(float angle, PointF center)
        {
            using (Matrix m = new Matrix())
            {
                //float reg = (float)(angle / 180 * Math.PI);
                this.angle -= angle;
                m.RotateAt(angle, center);
                //if (this.angle > 3.14159265f)
                //{
                //    this.angle = -3.14159265f;
                //}
                //else if (this.angle < -3.14159265f)
                //{
                //    this.angle = 3.14159265f;
                //}
                //else
                //{
                //    this.angle -= reg;
                //}
                shape.Transform(m);
            }
        }


        /// <summary>
        /// 添加额外属性
        /// </summary>
        /// <param name="properties"></param>
        public void AddProperties(params IExtraProperty[] properties)
        {
            if (!layer.IsLocked)
            {
                foreach (var item in properties)
                {
                    if (this.properties.ContainsKey(item.name))
                    {
                        this.properties[item.name] = item;
                    }
                    else
                    {
                        this.properties.Add(item.name, item);
                    }
                }
            }
        }
        /// <summary>
        /// 由名称获取额外属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>对应的额外属性</returns>
        public IExtraProperty GetProperty(string propertyName)
        {
            return properties[propertyName];
        }
        /// <summary>
        /// 移除对应名称的属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>如果移除成功返回true，否则返回false</returns>
        public bool RemoveProperty(string propertyName)
        {
            if (!layer.IsLocked)
            {
                return properties.Remove(propertyName);
            }
            return false;
        }



        /// <summary>
        /// 检查某点是否位于当前实体所表示范围内（能看见的地方就是表示范围）
        /// </summary>
        /// <param name="point">提供检查的点，是屏幕点</param>
        public bool IsPointInShape(Point point)
        {
            return this.mesh.IsVisible(point);
        }

        /// <summary>
        /// 转换shape到摄像机屏幕坐标用于给mesh提供顶点调整
        /// </summary>
        private void ShapeToScreenPointForMesh(Camera cam)
        {
            PointF[] ps = shape.PathPoints;
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i] = cam.WorldToScreen(new PointF(ps[i].X + pos.X, ps[i].Y + pos.Y));
            }
            //调整网格层
            mesh = new GraphicsPath(ps, shape.PathTypes);
        }

        public virtual void Drawing(Graphics g, DexCanvas canvas)
        {
            //调整网格
            ShapeToScreenPointForMesh(canvas.Camera);
            //只绘制在视野内的实体
            if (canvas.Camera.IsInView(this))
            {
                //绘制填充
                if (isImage)
                {
                    //图片
                    if (isImageVisible)
                    {
                        g.DrawImage(image, canvas.Camera.WorldToScreen(new RectangleF(pos, mesh.GetBounds().Size)));
                    }
                    else
                    {
                        //普通填充
                        g.FillPath(canvas.PaintFactory.GetBrush(Color.FromArgb(layer.Alpha, color)), mesh);
                    }
                }
                else
                {
                    //普通填充
                    g.FillPath(canvas.PaintFactory.GetBrush(Color.FromArgb(layer.Alpha, color)), mesh);
                }

                //绘制边框
                if (isSelected)
                {
                    g.DrawPath(canvas.PaintFactory.GetPen(Color.FromArgb(layer.Alpha, canvas.DrawingSetting.SelectedColor)), mesh);
                }
                else
                {
                    g.DrawPath(canvas.PaintFactory.GetPen(Color.FromArgb(layer.Alpha, canvas.DrawingSetting.BorderColor)), mesh);
                }
            }
        }

        public void Dispose()
        {
            if (image != null)
            {
                image.Dispose();
            }
            shape.Dispose();
            mesh.Dispose();
            properties.Clear();
        }
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Dex.Canvas
{
    /// <summary>
    /// 用于选中对象和选中操作
    /// </summary>
    public sealed class Selection : IDrawable
    {
        /// <summary>
        /// 拖动柄
        /// </summary>
        private Joysticks joysticks;
        /// <summary>
        /// 所属画布
        /// </summary>
        private DexCanvas canvas;
        /// <summary>
        /// 被选中的对象集合
        /// </summary>
        private Dictionary<int, EditableObject> selectedObjects = new Dictionary<int, EditableObject>();
        /// <summary>
        /// 选中框
        /// </summary>
        private Rectangle selectionBox = Rectangle.Empty;
        /// <summary>
        /// 开始选中
        /// </summary>
        private bool startSelect;
        /// <summary>
        /// 记录下来的鼠标在上次的位置
        /// </summary>
        private PointF LastMPoint = Point.Empty;
        /// <summary>
        /// 选中起点
        /// </summary>
        private Point start;


        public Selection(DexCanvas canvas)
        {
            this.canvas = canvas;
            joysticks = new Joysticks(this);
        }


        /// <summary>
        /// 选中的对象
        /// </summary>
        public Dictionary<int, EditableObject> SelectedObjects { get => selectedObjects; }
        /// <summary>
        /// 开始选中
        /// </summary>
        public bool StartSelect
        {
            get
            {
                return startSelect;
            }
            set
            {
                startSelect = value;
                //未开始则选中框是个空的
                if (!value)
                {
                    selectionBox = Rectangle.Empty;
                }
            }
        }
        /// <summary>
        /// 选中起点
        /// </summary>
        public Point Start { get => start; }
        /// <summary>
        /// 选中项个数
        /// </summary>
        public int SelectCount { get => selectedObjects.Count; }
        /// <summary>
        /// 是否在拖动柄上按下了
        /// </summary>
        public bool IsDownInJoystick { get => joysticks.IsDownInJoystick; }


        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e"></param>
        internal void MouseDown(MouseEventArgs e)
        {
            if (canvas.PressedButton == MouseButtons.Left)
            {
                //标记起点
                this.start = e.Location;
                //如果有选中项
                if (SelectCount>0)
                {
                    //执行鼠标按下检查
                    joysticks.MouseDown(e);
                }
                //如果不是按在拖动柄上则看看是否选中实体了
                if (!IsDownInJoystick)
                {
                    //检查是否在对象上
                    EditableObject obj = canvas.CurrentLayer.GetObjectInSceneByPoint(e.Location);
                    if (obj == null)
                    {
                        //框选开始
                        this.startSelect = true;
                        //清除先前的内容
                        DeSelectObjects();
                    }
                    else
                    {
                        //为空时只选中这个实体
                        if (SelectCount == 0)
                        {
                            SelectObject(obj);
                        }
                        else
                        {
                            //当前不是被选中实体则清空其他选项然后选中这个实体
                            if (!selectedObjects.ContainsKey(obj.ID))
                            {
                                DeSelectObjects();
                                //重新选定当前项
                                SelectObject(obj);
                            }
                        }
                        //记录初始位置
                        LastMPoint = canvas.MousePos;
                    }
                }
                else
                {
                    //按在拖动柄上了
                    LastMPoint = canvas.MousePos;
                }
            }
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e"></param>
        internal void MouseMove(MouseEventArgs e)
        {
            if (startSelect)
            {
                this.SetSelectionBox(e.Location);
            }
            if (canvas.PressedButton == MouseButtons.Left)
            {
                //在有选中对象的情况下
                if (SelectCount > 0)
                {
                    //未按在拖动柄上则移动
                    if (!IsDownInJoystick)
                    {

                        PointF vec = new PointF(canvas.MousePos.X - LastMPoint.X, canvas.MousePos.Y - LastMPoint.Y);//获取世界坐标系中的平移向量
                        foreach (var item in selectedObjects.Values)
                        {
                            item.Move(vec);
                        }
                    }
                    else
                    {
                        PointF vec = new PointF(canvas.MousePos.X - LastMPoint.X, canvas.MousePos.Y - LastMPoint.Y);//获取世界坐标系中的平移向量
                        //按在拖动柄上则缩放
                        foreach (var item in selectedObjects.Values)
                        {
                            switch (joysticks.JoysticksChoice)
                            {
                                case JoysticksChoice.Rotate:
                                    item.Rotate(vec.X * canvas.Camera.Zoom, new PointF(0, 0));
                                    break;
                                case JoysticksChoice.Up:
                                    if (item.Scale(0, -vec.Y))
                                    {
                                        item.Move(0, vec.Y / 2);
                                    }
                                    break;
                                case JoysticksChoice.LeftUp:
                                    if (item.Scale(-vec.X, -vec.Y))
                                    {
                                        item.Move(vec.X / 2, vec.Y / 2);
                                    }
                                    break;
                                case JoysticksChoice.Left:
                                    if (item.Scale(-vec.X, 0))
                                    {
                                        item.Move(vec.X / 2, 0);
                                    }
                                    break;
                                case JoysticksChoice.LeftDown:
                                    if (item.Scale(-vec.X, vec.Y))
                                    {
                                        item.Move(vec.X / 2, vec.Y / 2);
                                    }
                                    break;
                                case JoysticksChoice.Down:
                                    if (item.Scale(0, vec.Y))
                                    {
                                        item.Move(0, vec.Y / 2);
                                    }
                                    break;
                                case JoysticksChoice.RightDown:
                                    if (item.Scale(vec.X, vec.Y))
                                    {
                                        item.Move(vec.X / 2, vec.Y / 2);
                                    }
                                    break;
                                case JoysticksChoice.Right:
                                    if (item.Scale(vec.X, 0))
                                    {
                                        item.Move(vec.X / 2, 0);
                                    }
                                    break;
                                case JoysticksChoice.RightUp:
                                    if (item.Scale(vec.X, -vec.Y))
                                    {
                                        item.Move(vec.X / 2, vec.Y / 2);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    LastMPoint = canvas.MousePos;//记录鼠标位置用于下次求向量
                }
            }
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="e"></param>
        internal void MouseUp(MouseEventArgs e)
        {
            BoxSelect();//方框选择
            //重置框选条件
            this.LastMPoint = Point.Empty;//初始化鼠标记录坐标
            this.start = Point.Empty;//初始化框选坐标
            this.selectionBox = Rectangle.Empty;//重置选中框
            this.startSelect = false;
            //鼠标抬起后执行填充柄鼠标抬起指令
            this.joysticks.MouseUp(e);
            this.canvas.Refresh();
        }


        /// <summary>
        /// 获取选中对象集合的外包矩形
        /// </summary>
        /// <returns>选中对象集合的外包矩形</returns>
        public Rectangle GetSelectedObjectsBounds()
        {
            Rectangle selectRect;
            using (GraphicsPath shapeCombine = new GraphicsPath())
            {
                foreach (var item in selectedObjects.Values)
                {
                    shapeCombine.AddPath(item.Mesh, false);
                }
                RectangleF tmp = shapeCombine.GetBounds();
                selectRect = new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height);
            }
            return selectRect;
        }
        /// <summary>
        /// 获取选定矩形
        /// </summary>
        /// <param name="end"></param>
        /// <returns>选定矩形</returns>
        public void SetSelectionBox(Point end)
        {
            if (end.X < start.X)
            {
                //在起点的左上方
                if (end.Y < start.Y)
                {
                    selectionBox = new Rectangle(end.X, end.Y, start.X - end.X, start.Y - end.Y);
                }
                else
                {
                    //在起点的左下方
                    selectionBox = new Rectangle(end.X, start.Y, start.X - end.X, end.Y - start.Y);
                }
            }
            else
            {
                //在起点的右上方
                if (end.Y < start.Y)
                {
                    selectionBox = new Rectangle(start.X, end.Y, end.X - start.X, start.Y - end.Y);
                }
                else
                {
                    //在起点的右下方
                    selectionBox = new Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y);
                }
            }
        }
        /// <summary>
        /// 选中给定实体
        /// </summary>
        /// <param name="obj"></param>
        public void SelectObject(EditableObject obj)
        {
            obj.IsSelected = true;
            if (!selectedObjects.ContainsKey(obj.ID))
            {
                selectedObjects.Add(obj.ID, obj);
            }
        }
        /// <summary>
        /// 取消给定实体的选中
        /// </summary>
        /// <param name="obj"></param>
        public void DeSelectObject(EditableObject obj)
        {
            obj.IsSelected = false;
            selectedObjects.Remove(obj.ID);
        }
        /// <summary>
        /// 取消所有对象的选中
        /// </summary>
        public void DeSelectObjects()
        {
            //清除所有选中项
            foreach (var item in this.selectedObjects.Values)
            {
                item.IsSelected = false;
            }
            this.selectedObjects.Clear();
        }


        /// <summary>
        /// 框选
        /// </summary>
        private void BoxSelect()
        {
            foreach (var item in canvas.CurrentLayer.Objects.Values)
            {
                //有交集则选中
                if (item.Mesh.GetBounds().IntersectsWith(this.selectionBox))
                {
                    SelectObject(item);
                }
            }
        }
        public void Drawing(Graphics g, DexCanvas canvas)
        {
            if (startSelect)
            {
                //选中框
                g.FillRectangle(canvas.PaintFactory.GetBrush(Color.FromArgb(100, 0, 100, 0)), selectionBox);
                g.DrawRectangle(canvas.PaintFactory.GetPen(Color.FromArgb(255, 0, 100, 0)), selectionBox);
            }
            if (SelectCount>0)
            {
                //包围框
                g.DrawRectangle(canvas.PaintFactory.GetPen(Color.White, 2), GetSelectedObjectsBounds());
            }
            //绘制手柄
            joysticks.Drawing(g, canvas);
        }
    }
}
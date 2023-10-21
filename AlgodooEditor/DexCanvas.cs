using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dex.Canvas
{
    /// <summary>
    /// 场景编辑器
    /// </summary>
    public class DexCanvas : PictureBox, IDisposable
    {
        /// <summary>
        /// 鼠标当前按下的按键
        /// </summary>
        private MouseButtons pressedButton;
        /// <summary>
        /// 当前正在使用的层
        /// </summary>
        private Layer currentLayer;
        /// <summary>
        /// 场景编辑器自带的摄像机
        /// </summary>
        private Camera camera;
        /// <summary>
        /// 坐标轴
        /// </summary>
        private Axis axis;
        /// <summary>
        /// 正在使用的场景
        /// </summary>
        private Scene scene;
        /// <summary>
        /// 绘图设定
        /// </summary>
        private DrawingSetting drawingSetting;
        /// <summary>
        /// 画笔工厂
        /// </summary>
        private PaintFactory paintFactory;
        /// <summary>
        /// 选中器
        /// </summary>
        private Selection selection;

        /// <summary>
        /// 文本展示器
        /// </summary>
        private TextDraw textDisplayer;

        /// <summary>
        /// 无摄像机提示文本区域
        /// </summary>
        private TextArea noCamera;
        /// <summary>
        /// 鼠标信息文本区域
        /// </summary>
        private TextArea mouseInfo;

        /// <summary>
        /// 创建场景编辑器
        /// </summary>
        public DexCanvas()
        {
            InitializeComponent();
            //初始化
            camera = new Camera(this);
            textDisplayer = new TextDraw(this);
            axis = new Axis(this);
            drawingSetting = new DrawingSetting(this);
            selection = new Selection(this);
            paintFactory = new PaintFactory();

            //注册事件
            this.MouseWheel += DexCanvas_MouseWheel;
            //启用双缓冲
            this.DoubleBuffered = true;
            //允许拖拽
            this.AllowDrop = true;

            //存入并设置基本的字符区域
            noCamera = new TextArea(this, "No Camera", drawingSetting.FontFamily, new RectangleF(camera.WorldZero,new SizeF()), drawingSetting.FontColor, drawingSetting.FontSize, 0);
            mouseInfo = new TextArea(this, "鼠标在场景中的位置：" + MousePos.ToString() + "\n鼠标在窗口中的位置：" + this.PointToClient(MousePosition).ToString() + "\n鼠标在屏幕中的位置：" + MousePosition.ToString() + "\n缩放比：" + camera.Zoom.ToString(),drawingSetting.FontFamily, new RectangleF(new PointF(10, 10), new SizeF()), drawingSetting.FontColor, drawingSetting.FontSize, 0);
            noCamera.IsAutoSizing = true;
            mouseInfo.IsAutoSizing = true;
            mouseInfo.IsFixedSize = true;
            textDisplayer.AddTextArea(noCamera);
            textDisplayer.AddTextArea(mouseInfo);
            textDisplayer.ChangeShowTextArea(noCamera, true);
            textDisplayer.ChangeShowTextArea(mouseInfo, false);
        }


        /// <summary>
        /// 当前所在层
        /// </summary>
        public Layer CurrentLayer { get => currentLayer; }
        /// <summary>
        /// 显示摄像机
        /// </summary>
        public Camera Camera { get => camera; }
        /// <summary>
        /// 正在使用的场景
        /// </summary>
        public Scene Scene { get => scene; }
        /// <summary>
        /// 绘图设定
        /// </summary>
        public DrawingSetting DrawingSetting { get => drawingSetting; }
        /// <summary>
        /// 画笔工厂，用于获取当前画板中已有的画笔
        /// </summary>
        public PaintFactory PaintFactory { get => paintFactory; }
        /// <summary>
        /// 已经按下的按键
        /// </summary>
        public MouseButtons PressedButton { get => pressedButton; }
        /// <summary>
        /// 鼠标在场景内的位置
        /// </summary>
        public PointF MousePos { get; set; }
        /// <summary>
        /// 文字显示器
        /// </summary>
        public TextDraw TextDisplayer { get => textDisplayer; }


        /// <summary>
        /// 释放所占用资源
        /// </summary>
        public new void Dispose()
        {
            paintFactory.Dispose();//释放画笔工厂
            drawingSetting.Dispose();//释方绘画设置
            textDisplayer.Dispose();
            if (scene!=null)
            {
                scene.Dispose();//释放场景
            }
            base.Dispose(true);
        }

        /// <summary>
        /// 设定场景，同时将场景中第一个值作为当前图层
        /// </summary>
        public void SetScene(Scene scene)
        {
            this.scene = scene;
            this.currentLayer = scene.Layers.Values.First();
            textDisplayer.ChangeShowTextArea(noCamera, false);
            textDisplayer.ChangeShowTextArea(mouseInfo, true);
            this.Refresh();
        }
        /// <summary>
        /// 设定当前可以操作的图层
        /// </summary>
        /// <param name="layerID">当前正在编辑的场景所包含的图层的编号</param>
        public void SetCurrentLayer(int layerID)
        {
            if (scene.Layers.ContainsKey(layerID))
            {
                currentLayer = scene.Layers[layerID];
            }
        }
        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DexCanvas
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DexCanvas_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DexCanvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DexCanvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DexCanvas_MouseUp);
            this.Resize += new System.EventHandler(this.DexCanvas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }


        #region 事件
        #region 原生事件
        private void DexCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                camera.SetZoom(1.1f, e.Location);
            }
            else
            {
                camera.SetZoom(0.9f, e.Location);
            }
        }
        private void DexCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            pressedButton = e.Button;//记录按下的按钮
            camera.MouseDown(e);//摄像机按下事件
            if (scene!=null)
            {
                selection.MouseDown(e);//选择器按下事件激活
            }
            this.Refresh();//刷新视图
        }
        private void DexCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //如果场景不为空
            if (scene!=null)
            {
                MousePos = camera.ScreenToWorld(e.Location);//记录鼠标在场景中按下的位置
                //鼠标按下
                if (pressedButton != MouseButtons.None)
                {
                    //摄像机在鼠标移动时执行
                    camera.MouseMove(e);
                    //选择器在鼠标移动时执行
                    selection.MouseMove(e);
                    this.Refresh();
                }
            }
            else
            {
                //摄像机在鼠标移动时执行
                camera.MouseMove(e);
                this.Refresh();
            }
        }
        private void DexCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            pressedButton = MouseButtons.None;//清空鼠标操作
            //摄像机执行鼠标抬起动作
            camera.MouseUp(e);
            if (scene != null)
            {
                //选择器执行鼠标抬起动作
                selection.MouseUp(e);
            }
            this.Refresh();
        }
        private void DexCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.CornflowerBlue);//绘制背景
            axis.Drawing(e.Graphics, this);//绘制坐标轴
            if (scene!=null)//场景不为空
            {
                //绘制图层
                foreach (var item in scene.Layers.Values)
                {
                    item.Drawing(e.Graphics, this);
                }
                //绘制选择框
                if (selection.StartSelect)
                {
                    selection.Drawing(e.Graphics, this);
                }
                //未按下任何按键且按在填充柄上则允许绘制选中框
                if (pressedButton==MouseButtons.None||selection.IsDownInJoystick)
                {
                  
                    //选中个数存在则绘制选中框
                    if (selection.SelectedObjects.Count > 0)
                    {
                        selection.Drawing(e.Graphics, this);
                    }
                }
                //变动文本区域的文字
                mouseInfo.Text = "鼠标在场景中的位置：" + MousePos.ToString() + "\n鼠标在窗口中的位置：" + this.PointToClient(MousePosition).ToString() + "\n鼠标在屏幕中的位置：" + MousePosition.ToString() + "\n缩放比：" + camera.Zoom.ToString();
            }
            else
            {
                //调整字符位置
                noCamera.Area = new RectangleF(camera.WorldZero, new SizeF());
            }
            textDisplayer.Drawing(e.Graphics, this);
        }
        /// <summary>
        /// 调整大小的时候需要重设原点和缩放大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DexCanvas_Resize(object sender, EventArgs e)
        {
            camera.SetZero(this.Width / 2, this.Height / 2);
            //调整视口大小
            camera.Viewport.Size = new Size((int)(this.Width / camera.Zoom), (int)(this.Height / camera.Zoom));
            //调整字符位置
            mouseInfo.Area = new RectangleF(new PointF(10,10),new SizeF());
        }

        #endregion

        #endregion
    }
}

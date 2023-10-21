using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    public class Box : EditableObject
    {
        private int textID = -1;//文本区域ID，未给定前是-1
        private bool canDisplayText = true;
        private string text;
        private bool isTextVisible = true;
        private float textSize = 5;
        private SizeF size;


        /// <summary>
        /// 获取或设置是否显示文字
        /// </summary>
        public bool IsTextVisible
        {
            get
            {
                return isTextVisible;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!this.Layer.IsLocked)
                {
                    isTextVisible = value;
                }
            }
        }
        /// <summary>
        /// 是否可以显示文字
        /// </summary>
        public bool CanDisplayText { get => canDisplayText; }
        /// <summary>
        /// 获取或设置文字大小
        /// </summary>
        public float TextSize
        {
            get
            {
                return textSize;
            }
            set
            {
                if (!this.Layer.IsLocked)
                {
                    textSize = value;
                }
            }
        }
        /// <summary>
        /// 获取或设置文字
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                //图层未锁定则可以变动位置
                if (!this.Layer.IsLocked)
                {
                    text = value;
                }
            }
        }


        /// <summary>
        /// 方盒
        /// </summary>
        /// <param name="layer">所在图层</param>
        /// <param name="color">色彩</param>
        /// <param name="pos">矩形中心点位于场景中的坐标</param>
        /// <param name="size">矩形大小</param>
        /// <param name="text">矩形中带有的文字</param>
        /// <param name="img">矩形带有的图片</param>
        /// <param name="properties">属性</param>
        public Box(Layer layer, Color color, PointF pos, SizeF size, string text = "", Image img = null, params IExtraProperty[] properties) : base(layer, color, pos, DrawingType.Rectangle, img, properties)
        {
            this.text = text;
            this.size = size;
            this.Shape.AddRectangle(new RectangleF(-size.Width / 2, -size.Height / 2, size.Width, size.Height));//这个是场景坐标点)
        }
        /// <summary>
        /// 大小
        /// </summary>
        public SizeF Size { get => size; set => size = value; }

        public override void Drawing(Graphics g, DexCanvas canvas)
        {
            base.Drawing(g, canvas);
            //是否可以绘制文字
            if (canDisplayText)
            {
                //可以绘制但是能否显示
                if (isTextVisible)
                {
                    if (text != null && text != "")
                    {
                        if (textID == -1)
                        {
                            textID = canvas.TextDisplayer.AddTextArea(text + "\n" + this.Angle + "\n" + size + "\n" + Pos, canvas.Camera.WorldToScreen(new RectangleF(Pos.X-size.Width / 2, Pos.Y-size.Height / 2, size.Width, size.Height)), textSize, this.Angle);
                        }
                        else
                        {
                            var area = canvas.TextDisplayer.GetTextAreaById(textID);//获取对应的区域
                            area.Area = canvas.Camera.WorldToScreen(new RectangleF(Pos.X-size.Width/2, Pos.Y-size.Height / 2,size.Width,size.Height));//将矩形与自身的位置关联起来
                            area.TextSize = textSize;//变动字体大小
                            area.Text = text + "\n" + this.Angle + "\n" + size+"\n"+Pos;//变动文字
                            area.Angle = this.Angle;//变动角度
                            area.Margin = Mesh.GetBounds();
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// DexCanvas专用文字
    /// </summary>
    public class DexText : IDisposable
    {
        private TextArea area;
        private Bitmap textImage;
        /// <summary>
        /// 表示的文字
        /// </summary>
        public string Text
        {
            get
            {
                return area.Text;
            }
            set
            {
                area.Text = value;
                using (SolidBrush sb = new SolidBrush(area.TextColor))
                {
                    textImage = GetTextImage(Angle, sb);
                }
            }
        }
        /// <summary>
        /// 旋转的角度
        /// </summary>
        public float Angle
        {
            get
            {
                return area.Angle;
            }
            set
            {
                area.Angle = value;
                using (SolidBrush sb = new SolidBrush(area.TextColor))
                {
                    textImage = GetTextImage(Angle, sb);
                }
            }
        }
        /// <summary>
        /// 最近的一次更新过的文字的图像
        /// </summary>
        public Bitmap TextImage => textImage;

        /// <summary>
        /// 为文本区域创建专用文字
        /// </summary>
        /// <param name="area">指定的文本区域</param>
        public DexText(TextArea area)
        {
            this.area = area;
            using (SolidBrush sb = new SolidBrush(area.TextColor))
            {
                textImage = GetTextImage(Angle, sb);
            }
        }
        /// <summary>
        /// 单独获取当前文本区域的图像，不影响所在文本区域的情况，换句话说，既不更改文字也不更改角度，而是原原本本的输出一个独立的图片
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="brush">画笔</param>
        /// <returns>文本的图像</returns>
        public Bitmap GetTextImage(float angle, Brush brush)
        {
            if (area.Area.Size == SizeF.Empty)//如果是适应的文本域则要独立处理
            {
                using (Graphics tmp = Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = MeasureString(tmp);//测量字符串应有的大小
                    Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height);//以此创建一个图片
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        //设置文本输出质量
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        RotateGraph(angle, g);
                        DrawString(brush, g);
                        return textImage = bmp;
                    }
                }
            }
            else
            {
                //创建1层画布
                Bitmap bmp = new Bitmap((int)area.Margin.Width, (int)area.Margin.Height);
                //旋转画文字
                using (Graphics g1 = Graphics.FromImage(bmp))
                {
                    //设置文本输出质量
                    g1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g1.SmoothingMode = SmoothingMode.AntiAlias;
                    SizeF size = MeasureString(g1);
                    RotateGraph(angle, g1);
                    DrawString(brush, g1);
                }
                return textImage = bmp;
            }
        }
        /// <summary>
        /// 在给定画布上用指定颜色的画笔画出字符串
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="g"></param>
        private void DrawString(Brush brush, Graphics g)
        {
            if (area.IsFixedSize)
            {
                if (area.IsAutoSizing)
                {
                    //绘制文字
                    g.DrawString(Text, area.FixedFont, brush, new RectangleF(0, 0, area.Area.Width, area.Area.Height));
                }
                else
                {
                    //绘制文字
                    g.DrawString(Text, area.FixedFont, brush, 0, 0);
                }
            }
            else
            {
                if (area.IsAutoSizing)
                {
                    //绘制文字
                    g.DrawString(Text, area.Font, brush, new RectangleF(0, 0, area.Area.Width, area.Area.Height));
                }
                else
                {
                    //绘制文字
                    g.DrawString(Text, area.Font, brush, 0, 0);
                }
            }
        }
        /// <summary>
        /// 旋转给定画布
        /// </summary>
        /// <param name="angle">旋转角度</param>
        /// <param name="g">需要旋转的画布</param>
        private void RotateGraph(float angle, Graphics g)
        {
            //调整画布平移到中心点
            g.TranslateTransform(area.Area.Width / 2, area.Area.Height /2);
            //旋转画布
            g.RotateTransform(-angle);
            //将画布移动回原点
            g.TranslateTransform(-area.Area.Width / 2, -area.Area.Height / 2);
        }
        /// <summary>
        /// 测量字符串大小
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private SizeF MeasureString(Graphics g)
        {
            SizeF size;
            if (area.IsFixedSize)
            {
                //测量文字长度并记录
                size = g.MeasureString(Text, area.FixedFont);
            }
            else
            {
                //测量文字长度并记录
                size = g.MeasureString(Text, area.Font);
            }

            return size;
        }

        public void Dispose()
        {
            textImage.Dispose();
        }
    }
}

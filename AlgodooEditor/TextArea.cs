using System;
using System.Drawing;
using Zero.Core.Interface;

namespace Dex.Canvas
{
    /// <summary>
    /// 文本区域，不仅可以用于文本存储，同时也可以用于为文本提供一个基本的平移、<br/>
    /// 缩放、旋转、换行、颜色等的信息块。其本身可以看作为一个矩形，这意味着文字<br/>
    /// 只能在这个区域内绘制，并且文字本身的特点也将因为处于同一个文本区域而变得<br/>
    /// 一致起来，因此不会受到外界影响。【文本区域使用的是屏幕矩形，所以一切都按<br/>
    /// 屏幕来看】。
    /// </summary>
    public class TextArea : IBasicIdentity
    {
        private static int lastId = 0;
        /// <summary>
        /// 该文本区域的所属画布
        /// </summary>
        private DexCanvas canvas;
        private DexText dexText;
        private int id;

        public TextArea(DexCanvas canvas, string text, FontFamily fontFamily, RectangleF area, Color textColor, float textSize = 12, float angle = 0)
        {
            this.canvas = canvas;
            Text = text;
            TextSize = textSize;
            Angle = angle;
            TextColor = textColor;
            Area = area;
            Margin = area;
            FontFamily = fontFamily;
            dexText = new DexText(this);
            id = lastId++;
        }


        /// <summary>
        /// 文本区域的编号
        /// </summary>
        public int ID => id;
        /// <summary>
        /// 是否自适应文本框大小【为true则是跟随area变动，为false则是跟随location变动】
        /// </summary>
        public bool IsAutoSizing { get; set; } = true;
        /// <summary>
        /// 是否显示当前文本区域
        /// </summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>
        /// 是否固定字体大小
        /// </summary>
        public bool IsFixedSize { get; set; } = false;
        /// <summary>
        /// 表示一个字体变动比例的值，可以用于缩放
        /// </summary>
        public float Ratio { get; set; } = 1;
        /// <summary>
        /// 文本区域中存储的文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 文本大小
        /// </summary>
        public float TextSize { get; set; }
        /// <summary>
        /// 文本的弧度角度
        /// </summary>
        public float Angle { get; set; }
        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color TextColor { get; set; }
        /// <summary>
        /// 表示当前文本区域旋转的边缘屏幕矩形
        /// </summary>
        public RectangleF Margin { get; set; }
        /// <summary>
        /// 表示当前文本区域的屏幕矩形
        /// </summary>
        public RectangleF Area { get; set; }
        /// <summary>
        /// 由可变比例控制大小的字体
        /// </summary>
        public Font Font
        {
            get
            {
                return canvas.DrawingSetting.GetFont(FontFamily, TextSize * Ratio);
            }
        }
        /// <summary>
        /// 使用当前文本区域字体大小控制的字体(固定大小)
        /// </summary>
        public Font FixedFont
        {
            get
            {
                return canvas.DrawingSetting.GetFont(FontFamily, TextSize);
            }
        }
        /// <summary>
        /// 字体家族
        /// </summary>
        public FontFamily FontFamily { get; set; }
        /// <summary>
        /// 文本区域中的绘图专用文字
        /// </summary>
        public DexText DexText { get => dexText; }
    }
}
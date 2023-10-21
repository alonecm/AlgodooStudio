using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// DexCanvas绘图设定
    /// </summary>
    public class DrawingSetting : IDisposable
    {
        private DexCanvas canvas;
        private Dictionary<Tuple<float,FontFamily>, Font> fontContainer = new Dictionary<Tuple<float, FontFamily>, Font>();
        public DrawingSetting(DexCanvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// 字体家族
        /// </summary>
        public FontFamily FontFamily { get; set; } = FontFamily.GenericMonospace;
        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; } = 12;
        /// <summary>
        /// 跟随摄像机缩放变化的字体
        /// </summary>
        public Font Font
        {
            get
            {
                return GetFont(FontFamily, FontSize * canvas.Camera.Zoom);
            }
        }
        /// <summary>
        /// 使用当前绘图设置所使用的具有指定大小的字体
        /// </summary>
        public Font FixedFont
        {
            get
            {
                return GetFont(FontFamily, FontSize);
            }
        }
        /// <summary>
        /// 边线颜色
        /// </summary>
        public Color BorderColor { get; set; } = Color.Black;
        /// <summary>
        /// 选中颜色
        /// </summary>
        public Color SelectedColor { get; set; } = Color.White;
        /// <summary>
        /// 全局字体颜色
        /// </summary>
        public Color FontColor { get; set; } = Color.Black;

        /// <summary>
        /// 使用绘图设置所使用的字体家族获取字体
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        public Font GetFont(float fontSize)
        {
            return GetFont(this.FontFamily, fontSize);
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontFamily">字体家族</param>
        /// <returns></returns>
        public Font GetFont(FontFamily fontFamily, float fontSize)
        {
            AutoCleanFontContainer(100);
            if (!fontContainer.ContainsKey(new Tuple<float, FontFamily>(fontSize, fontFamily)))
            {
                fontContainer.Add(new Tuple<float, FontFamily>(fontSize, fontFamily), new Font(fontFamily, fontSize));
            }
            return fontContainer[new Tuple<float, FontFamily>(fontSize, fontFamily)];
        }
        /// <summary>
        /// 自动清理字体容器
        /// </summary>
        /// <param name="cleanCount">启动清理的上限值</param>
        private void AutoCleanFontContainer(int cleanCount)
        {
            if (fontContainer.Count>=cleanCount)
            {
                int count = fontContainer.Count / 2;//折半清
                int c = 0;
                while (c>count)
                {
                    var v = fontContainer.First();
                    //先移除
                    fontContainer.Remove(v.Key);
                    //后释放
                    v.Value.Dispose();
                    v.Key.Item2.Dispose();
                    c++;
                }
            }
        }
        public void Dispose()
        {
            FontFamily.Dispose();
            //释放所有字体
            foreach (var item in this.fontContainer)
            {
                item.Key.Item2.Dispose();
                item.Value.Dispose();
            }
        }
    }
}

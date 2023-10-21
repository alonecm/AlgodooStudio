using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 画图工厂，用于获取和管理画笔和画刷
    /// </summary>
    public  class PaintFactory : IDisposable
    {
        private Dictionary<Tuple<Color, float>, Pen> pens = new Dictionary<Tuple<Color, float>, Pen>();
        private Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();

        /// <summary>
        /// 获取画笔
        /// </summary>
        /// <param name="color">画笔颜色</param>
        /// <returns>指定颜色的画笔</returns>
        public Pen GetPen(Color color)
        {
            return GetPen(color, 1);
        }
        /// <summary>
        /// 获取画笔
        /// </summary>
        /// <param name="color">画笔颜色</param>
        /// <param name="size">画笔大小</param>
        /// <returns>指定颜色的画笔</returns>
        public Pen GetPen(Color color, float size)
        {
            Tuple<Color, float> tmp = new Tuple<Color, float>(color, size);//创建元组当作键
            //如果当前字典包含指定颜色的画笔则直接返回相应画笔并且不创建新的画笔
            if (pens.ContainsKey(tmp))
            {
                return pens[tmp];
            }
            //否则就创建新颜色的画笔并将其放入画笔字典中同时返回画笔
            var pen = new Pen(color, size);
            pens.Add(tmp, pen);
            return pen;
        }
        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <param name="color">画刷颜色</param>
        /// <returns>指定颜色的画刷</returns>
        public Brush GetBrush(Color color)
        {
            //如果当前字典包含指定颜色的画刷则直接返回相应画刷并且不创建新的画刷
            if (brushes.ContainsKey(color))
                return brushes[color];
            //否则就创建新颜色的画刷并将其放入画刷字典中同时返回画刷
            var brush = new SolidBrush(color);
            brushes.Add(color, brush);
            return brush;
        }
        /// <summary>
        /// 释放画笔包
        /// </summary>
        public void DisposePens()
        {
            foreach (var item in pens)
            {
                item.Value.Dispose();
            }
            pens.Clear();
        }
        /// <summary>
        /// 释放画刷包
        /// </summary>
        public void DisposeBrushes()
        {
            foreach (var item in brushes)
            {
                item.Value.Dispose();
            }
            pens.Clear();
        }
        /// <summary>
        /// 释放全部工具
        /// </summary>
        public void Dispose()
        {
            DisposePens();
            DisposeBrushes();
        }
    }
}

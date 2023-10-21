using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 绘制接口
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g">位于<paramref name="canvas"/>上的画板</param>
        /// <param name="canvas">画布（应当与使用当前接口的类存在于相同位置）</param>
        void Drawing(Graphics g, DexCanvas canvas);
    }
}

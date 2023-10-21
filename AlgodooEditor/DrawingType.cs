using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    /// <summary>
    /// 绘制类别
    /// </summary>
    public enum DrawingType
    {
        Null=0,
        /// <summary>
        /// 线段
        /// </summary>
        Line,
        /// <summary>
        /// 路径
        /// </summary>
        Path,
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle,
        /// <summary>
        /// 多边形
        /// </summary>
        Polygon,
        /// <summary>
        /// 圆形
        /// </summary>
        Circle,
        /// <summary>
        /// 椭圆形
        /// </summary>
        Ellipse,
        /// <summary>
        /// 平面
        /// </summary>
        Plane,
        /// <summary>
        /// 图片
        /// </summary>
        Image
    }
}

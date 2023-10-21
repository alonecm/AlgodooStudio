using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Phun.SceneObject
{
    /// <summary>
    /// 可用的场景对象
    /// </summary>
    internal enum SceneObject
    {
        /// <summary>
        /// 空对象
        /// </summary>
        None,
        /// <summary>
        /// 矩形
        /// </summary>
        Box,
        /// <summary>
        /// 圆
        /// </summary>
        Circle,
        /// <summary>
        /// 
        /// </summary>
        Contact,
        /// <summary>
        /// 固定点
        /// </summary>
        Fixjoint,
        /// <summary>
        /// 群组
        /// </summary>
        Group,
        /// <summary>
        /// 轴承
        /// </summary>
        Hinge,
        /// <summary>
        /// 镭射笔
        /// </summary>
        LaserPen,
        /// <summary>
        /// 场景图层
        /// </summary>
        Layer,
        /// <summary>
        /// 线的端点
        /// </summary>
        LineEndPoint,
        /// <summary>
        /// 轨迹追踪器
        /// </summary>
        Pen,
        /// <summary>
        /// 平面
        /// </summary>
        Plane,
        /// <summary>
        /// 多边形
        /// </summary>
        Polygon,
        /// <summary>
        /// 弹簧
        /// </summary>
        Spring,
        /// <summary>
        /// 推进器
        /// </summary>
        Thruster,
        /// <summary>
        /// 水
        /// </summary>
        Water,
        /// <summary>
        /// 小部件
        /// </summary>
        Widget
    }
}

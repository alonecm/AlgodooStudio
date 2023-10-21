using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.Canvas
{
    public class Circle : EditableObject
    {
        private float radius;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="color"></param>
        /// <param name="pos">这个是场景坐标</param>
        /// <param name="radius"></param>
        /// <param name="img"></param>
        /// <param name="properties"></param>
        public Circle(Layer layer, Color color, PointF pos, float radius, Image img = null, params IExtraProperty[] properties) : base(layer, color, pos, DrawingType.Circle, img, properties)
        {
            this.radius = radius;
            this.Shape.AddEllipse(- radius, - radius, radius * 2, radius * 2);//这个是场景坐标点
        }
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius { get => radius; set => radius = value; }
    }
}

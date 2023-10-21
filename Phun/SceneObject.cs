using AlgodooStudio.Base;
using AlgodooStudio.Interface;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 场景对象,表示一个来自Algodoo软件中的场景对象
    /// </summary>
    public sealed class SceneObject : IPropertyItem<ObjectType>, IBasicInformation
    {
        private int id;
        private string name = "SceneObject";

        /// <summary>
        /// 场景对象的添加代码
        /// </summary>
        private string value;

        private ObjectType type;

        /// <summary>
        /// 属性集
        /// </summary>
        private Container<ObjectItem> items = new Container<ObjectItem>();

        /// <summary>
        /// 创建一个场景对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public SceneObject(int id)
        {
            this.id = id;
        }

        public int ID { get => id; }
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 场景对象的添加代码
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                type = GetType(value);
            }
        }

        public ObjectType Type { get => type; }

        /// <summary>
        /// 属性集
        /// </summary>
        public Container<ObjectItem> Items { get => items; set => items = value; }

        /// <summary>
        /// 由<paramref name="value"/>获取<see cref="ObjectType"/>
        /// </summary>
        /// <param name="value">场景对象的添加代码</param>
        /// <returns>值对应的<see cref="ObjectType"/></returns>
        public ObjectType GetType(string value)
        {
            string tmp = value.Substring(9, value.Length - 9);
            switch (tmp)
            {
                case "Box":
                    return ObjectType.Box;

                case "Circle":
                    return ObjectType.Circle;

                case "Contact":
                    return ObjectType.Contact;

                case "Fixjoint":
                    return ObjectType.Fixjoint;

                case "Group":
                    return ObjectType.Group;

                case "Hinge":
                    return ObjectType.Hinge;

                case "LaserPen":
                    return ObjectType.LaserPen;

                case "Layer":
                    return ObjectType.Layer;

                case "LineEndPoint":
                    return ObjectType.LineEndPoint;

                case "Pen":
                    return ObjectType.Pen;

                case "Plane":
                    return ObjectType.Plane;

                case "Polygon":
                    return ObjectType.Polygon;

                case "Spring":
                    return ObjectType.Spring;

                case "Thruster":
                    return ObjectType.Thruster;

                case "Water":
                    return ObjectType.Water;

                case "Widget":
                    return ObjectType.Widget;

                default:
                    return ObjectType.None;
            }
        }

        public override string ToString()
        {
            string tmp = value + " {";
            foreach (var item in items)
            {
                tmp += "\n    " + item.ToString() + ";";
            }
            tmp += "\n};";
            return tmp;
        }
    }
}
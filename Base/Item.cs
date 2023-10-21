using AlgodooStudio.Interface;

namespace AlgodooStudio.Base
{
    /// <summary>
    /// 一个可用于作为<typeparamref name="TypeEnum"/>的简易项基类，可以被继承
    /// </summary>
    /// <typeparam name="TypeEnum">此处应放枚举类型</typeparam>
    public class Item<TypeEnum> : IPropertyItem<TypeEnum>
    {
        private string name;
        private string value;
        private TypeEnum type;

        public string Name { get => name; set => name = value; }

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

        public TypeEnum Type { get => type; }

        public Item(string name, string value)
        {
            this.name = name;
            this.Value = value;
        }

        /// <summary>
        /// 由<paramref name="value"/>获取<typeparamref name="TypeEnum"/>
        /// </summary>
        /// <param name="value">给定的值</param>
        /// <returns>值对应的<typeparamref name="TypeEnum"/></returns>
        public virtual TypeEnum GetType(string value)
        {
            return default;
        }

        /// <summary>
        /// 返回项的字符串形式
        /// </summary>
        /// <param name="c">分隔字符串</param>
        /// <returns>项的字符串</returns>
        public string ToString(string c)
        {
            return name + c + value;
        }
    }
}
namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 用于对场景对象的条目描述的类
    /// </summary>
    public sealed class ObjectItem
    {
        private string name;
        private bool isFixed;

        public ObjectItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 是否为固定属性
        /// </summary>
        public bool IsFixed { get => isFixed; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (value[0] == '_')
                {
                    isFixed = true;
                }
                else
                {
                    isFixed = false;
                }
            }
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return Name + " := " + Value;
        }
    }
}
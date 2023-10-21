namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 提供属性项的应有功能的模板<br/>
    /// <typeparamref name="T"/>是属性项的类型
    /// </summary>
    internal interface IPropertyItem<T>
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        T Type { get; }

        /// <summary>
        /// 由<paramref name="value"/>获取<typeparamref name="T"/>
        /// </summary>
        /// <param name="value">给定的值</param>
        /// <returns>值对应的<typeparamref name="T"/></returns>
        T GetType(string value);
    }
}
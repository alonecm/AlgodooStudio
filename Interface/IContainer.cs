namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 用于提供容器应有的功能模板
    /// </summary>
    internal interface IContainer<T>
    {
        /// <summary>
        /// 存放用的容器
        /// </summary>
        T[] Contents { get; }

        /// <summary>
        /// 容器内存放对象的数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 向指定<paramref name="container"/>添加内容并使指其自动扩容
        /// </summary>
        /// <param name="content">需要添加的内容</param>
        /// <param name="size">需要扩容的大小，如果小于等于零则默认不修改大小直接添加,需要提供一个类中的全局变量</param>
        void Add(T content, int size = 0);

        /// <summary>
        /// 向指定容器追加集合
        /// </summary>
        /// <param name="target">目标容器</param>
        /// <param name="source">源集合</param>
        void AddRange(params T[] source);

        /// <summary>
        /// 移除指定索引处的内容，建议执行统一复制
        /// </summary>
        /// <param name="index">需要移除的索引</param>
        void RemoveAt(int index);

        /// <summary>
        /// 清空容器
        /// </summary>
        void Clear();
    }
}
namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 用于提供树节点应有的功能模板
    /// </summary>
    internal interface ITreeNode<T>
    {
        /// <summary>
        /// 子节点
        /// </summary>
        T[] ChildNode { get; }

        /// <summary>
        /// 向子节点中追加节点
        /// </summary>
        /// <param name="node">需要添加的节点</param>
        void Add(T node);

        /// <summary>
        /// 清空当前节点的所有子节点
        /// </summary>
        void Clear();

        /// <summary>
        /// 移除指定位置的子节点
        /// </summary>
        /// <param name="index">移除索引</param>
        void RemoveAt(int index);
    }
}
namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 用于提供树节点应有的功能模板，具有扩展的插入功能
    /// </summary>
    internal interface ITreeNodeEx<T> : ITreeNode<T>
    {
        /// <summary>
        /// 向子节点的指定索引后插入节点
        /// </summary>
        /// <param name="index">需要在后方插入节点的前方索引</param>
        /// <param name="node">准备插入的节点</param>
        void InsertAt(int index, T node);
    }
}
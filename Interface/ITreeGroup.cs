namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 用于提供树型组应有的功能模板<br/>
    /// <typeparamref name="T"/>是组的类型
    /// </summary>
    internal interface ITreeGroup<T>
    {
        /// <summary>
        /// 子组
        /// </summary>
        T[] ChildGroup { get; }

        /// <summary>
        /// 向子组中追加组
        /// </summary>
        /// <param name="group">需要添加的组</param>
        void AddGroup(T group);

        /// <summary>
        /// 清空当前组中所有子组
        /// </summary>
        void ClearGroup();

        /// <summary>
        /// 移除指定位置的子组
        /// </summary>
        /// <param name="index">移除索引</param>
        void RemoveGroupAt(int index);
    }
}
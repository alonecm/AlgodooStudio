namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// 单元项
    /// </summary>
    internal sealed class UnitItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal readonly string name;
        /// <summary>
        /// 内容
        /// </summary>
        internal readonly string content;
        /// <summary>
        /// 创建单元项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="content">内容</param>
        public UnitItem(string name, string content)
        {
            this.name = name;
            this.content = content;
        }
    }
}
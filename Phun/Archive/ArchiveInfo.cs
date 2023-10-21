namespace AlgodooStudio.Phun.Archive
{
    /// <summary>
    /// 存档信息
    /// </summary>
    public sealed class ArchiveInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// AlgoBox作品ID
        /// </summary>
        public string AlgoboxID { get; set; }

        /// <summary>
        /// 作品描述
        /// </summary>
        public string Description { get; set; }
    }
}
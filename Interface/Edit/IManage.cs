namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 管理接口，用于提供复制，剪切，粘贴，删除四种操作的功能
    /// </summary>
    internal interface IManage : IEditable
    {
        /// <summary>
        /// 复制
        /// </summary>
        void Copy();

        /// <summary>
        /// 剪切
        /// </summary>
        void Cut();

        /// <summary>
        /// 粘贴
        /// </summary>
        void Paste();

        /// <summary>
        /// 删除
        /// </summary>
        void Delete();
    }
}
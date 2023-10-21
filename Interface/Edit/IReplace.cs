namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 替换接口，用于提供简单的替换功能
    /// </summary>
    internal interface IReplace : IEditable
    {
        /// <summary>
        /// 替换
        /// </summary>
        void Replace();
    }
}
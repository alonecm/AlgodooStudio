namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 全选接口，用于提供简单的全选的功能
    /// </summary>
    internal interface ISelectAll : IEditable
    {
        /// <summary>
        /// 全选
        /// </summary>
        void SelectAll();
    }
}
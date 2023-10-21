namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 查找(查询)接口，用于提供简单的查询功能
    /// </summary>
    internal interface ISearch : IEditable
    {
        /// <summary>
        /// 查找(查询)
        /// </summary>
        void Search();
    }
}
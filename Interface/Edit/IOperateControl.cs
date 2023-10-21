namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 操作控制接口，用于提供简单的重做和撤销两种基本操作的功能
    /// </summary>
    internal interface IOperateControl : IEditable
    {
        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();

        /// <summary>
        /// 重做
        /// </summary>
        void Redo();
    }
}
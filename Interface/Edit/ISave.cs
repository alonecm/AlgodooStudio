namespace AlgodooStudio.Interface.Edit
{
    /// <summary>
    /// 保存接口，用于提供简单的保存和另存为的功能(主要用于私有方法)
    /// </summary>
    /// <typeparam name="ReturnType">保存需要返回的值的类型</typeparam>
    internal interface ISave<ReturnType> : IEditable
    {
        /// <summary>
        /// 保存当前正在编辑的文件
        /// </summary>
        /// <returns>保存成功后返回类型为<typeparamref name="ReturnType"/>的正常值，
        /// 否则则返回类型为<typeparamref name="ReturnType"/>的其他值</returns>
        ReturnType Save();

        /// <summary>
        /// 当前正在编辑的文件另存为
        /// </summary>
        /// <returns>保存成功后返回类型为<typeparamref name="ReturnType"/> 的正常值，
        /// 否则则返回类型为<typeparamref name="ReturnType"/>的其他值</returns>
        ReturnType SaveAs();
    }

    /// <summary>
    /// 保存接口，用于提供简单的保存和另存为的功能(主要用于公开方法)
    /// </summary>
    internal interface ISave : IEditable
    {
        /// <summary>
        /// 保存当前正在编辑的文件
        /// </summary>
        void Save();

        /// <summary>
        /// 当前正在编辑的文件另存为
        /// </summary>
        void SaveAs();
    }
}
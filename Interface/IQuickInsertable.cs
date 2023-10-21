namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 提供快速输入字符串功能的接口
    /// </summary>
    internal interface IQuickInsertable
    {
        /// <summary>
        /// 快速输入
        /// </summary>
        /// <param name="str">输入的内容</param>
        /// <param name="pos">输入的位置，如果为默认的-1则插入到默认的位置，如果不为-1则插入到指定的位置</param>
        void Insert(string str, int pos = -1);
    }
}
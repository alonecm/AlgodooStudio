using Zero.Core.Interface;

namespace AlgodooStudio.Interface
{
    /// <summary>
    /// 提供基本信息
    /// </summary>
    internal interface IBasicInformation : IBasicIdentity
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
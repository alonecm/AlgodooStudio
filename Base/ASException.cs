namespace AlgodooStudio.Base
{
    /// <summary>
    /// AS本身的异常基类，用于创建基于AS的异常（不同于VS的异常）
    /// </summary>
    public abstract class ASException
    {
        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// 异常错误码
        /// </summary>
        public string ErrorCode { get; protected set; }
    }
}
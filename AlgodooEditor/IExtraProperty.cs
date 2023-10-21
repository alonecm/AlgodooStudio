namespace Dex.Canvas
{

    /// <summary>
    /// 非主要额外属性
    /// </summary>
    public interface IExtraProperty
    {
        /// <summary>
        /// 名称
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        string value { get; set; }
    }
}
using AlgodooStudio.Base;

namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// 字符集，用于存放从流中读取到的字符
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// 字符集所表示的范围
        /// </summary>
        public Range range;

        /// <summary>
        /// 字符集的类型
        /// </summary>
        public readonly string type;

        /// <summary>
        /// 字符集的值
        /// </summary>
        public readonly string value;

        /// <summary>
        /// 创建一个字符集
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        internal Token(Range range, string type, string value)
        {
            this.range = range;
            this.type = type;
            this.value = value;
        }

        /// <summary>
        /// 创建一个字符集
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        internal Token(Range range, string type, char value)
        {
            this.range = range;
            this.type = type;
            this.value = value.ToString();
        }

        /// <summary>
        /// 返回字符集的字符类型和值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return range.ToString() + " " + type + ", " + value;
        }
    }
}
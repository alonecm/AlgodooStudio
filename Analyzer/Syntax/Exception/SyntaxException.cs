using AlgodooStudio.Base;

namespace AlgodooStudio.Analyzer.Syntax.Exception
{
    /// <summary>
    /// 语法异常
    /// </summary>
    public class SyntaxException : ASException
    {
        internal readonly Range Range;

        /// <summary>
        /// 创建一个语法异常
        /// </summary>
        /// <param name="position">出现位置</param>
        /// <param name="description">异常描述</param>
        /// <param name="errorCode">异常错误码</param>
        internal SyntaxException(int position, string errorCode, string description)
        {
            Range = new Range(position, position);
            this.Description = description;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 创建一个语法异常
        /// </summary>
        /// <param name="position">出现位置</param>
        /// <param name="description">异常描述</param>
        internal SyntaxException(int position, string description)
        {
            Range = new Range(position, position);
            this.Description = description;
        }

        /// <summary>
        /// 创建一个语法异常
        /// </summary>
        /// <param name="range">异常范围</param>
        /// <param name="description">异常描述</param>
        /// <param name="errorCode">异常错误码</param>
        internal SyntaxException(Range range, string errorCode, string description)
        {
            this.Range = range;
            this.Description = description;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 创建一个语法异常
        /// </summary>
        /// <param name="range">异常范围</param>
        /// <param name="description">异常描述</param>
        internal SyntaxException(Range range, string description)
        {
            this.Range = range;
            this.Description = description;
        }

        public override string ToString()
        {
            return "位置：" + Range.ToString() + " 错误代码：" + ErrorCode + " 异常描述：" + Description + "\r\n";
        }
    }
}
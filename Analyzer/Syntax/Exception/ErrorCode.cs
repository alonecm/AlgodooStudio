namespace AlgodooStudio.Analyzer.Syntax.Exception
{
    /// <summary>
    /// 错误码
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        /// 无法处理字符
        /// </summary>
        public const string charhandleError = "E090";

        /// <summary>
        /// 定义错误
        /// </summary>
        public const string defineError = "E100";

        /// <summary>
        /// 关键词使用错误
        /// </summary>
        public const string keywordError = "E101";

        /// <summary>
        /// 括号错误
        /// </summary>
        public const string bracesError = "E200";

        /// <summary>
        /// 括号缺失
        /// </summary>
        public const string bracesMissing = "E201";

        /// <summary>
        /// 括号中断
        /// </summary>
        public const string bracesCut = "E202";

        /// <summary>
        /// 引用错误
        /// </summary>
        public const string referenceError = "E303";

        /// <summary>
        /// 字符串异常
        /// </summary>
        public const string stringError = "E400";

        /// <summary>
        /// 缺失块开始
        /// </summary>
        public const string missingBranchStart = "E501";

        /// <summary>
        /// 缺失块结束
        /// </summary>
        public const string missingBranchEnd = "E502";

        /// <summary>
        /// 缺少箭头号
        /// </summary>
        public const string missingArrow = "E601";

        /// <summary>
        /// 缺少指向箭头号
        /// </summary>
        public const string missingDirectArrow = "E701";

        /// <summary>
        /// 不期待的字符集
        /// </summary>
        public const string UnexpectedToken = "E800";

        /// <summary>
        /// 表达式异常
        /// </summary>
        public const string ExpressionError = "E900";
    }
}
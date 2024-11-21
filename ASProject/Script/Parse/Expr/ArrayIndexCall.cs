using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 数组索引调用
    /// </summary>
    public sealed class ArrayIndexCall : ThymeSyntaxNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">调用对象</param>
        /// <param name="source">调用参数</param>
        public ArrayIndexCall(ThymeSyntaxNode arr, ThymeSyntaxNode index)
        {
            Array = arr;
            Index = index;
        }

        public override string Type => "ArrayIndexCall";

        public ThymeSyntaxNode Array { get; }
        public ThymeSyntaxNode Index { get; }


        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array;
            yield return Index;
        }
    }
}

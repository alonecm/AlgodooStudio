using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 数组索引组调用
    /// </summary>
    public sealed class ArrayIndexGroupCall : ThymeSyntaxNode
    {
        public ArrayIndexGroupCall(ThymeSyntaxNode array, ThymeSyntaxNode indexGroup)
        {
            Array = array;
            IndexGroup = indexGroup;
        }

        public override string Type => "ArrayIndexGroupCall";

        public ThymeSyntaxNode Array { get; }
        public ThymeSyntaxNode IndexGroup { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array;
            yield return IndexGroup;
        }
    }
}

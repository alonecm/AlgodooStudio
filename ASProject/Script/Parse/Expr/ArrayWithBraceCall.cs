using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{

    /// <summary>
    /// 数组和小括号调用
    /// </summary>
    public sealed class ArrayWithBraceCall : ThymeSyntaxNode
    {
        public ArrayWithBraceCall(ThymeSyntaxNode arr, ThymeSyntaxNode brace)
        {
            Array = arr;
            Brace = brace;
        }

        public override string Type => "ArrayWithBraceCall";

        public ThymeSyntaxNode Array { get; }
        public ThymeSyntaxNode Brace { get; }


        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array;
            yield return Brace;
        }
    }
}

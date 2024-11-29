using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 小括号和数组调用
    /// </summary>
    public sealed class BraceWithArrayCall : ThymeSyntaxNode
    {
        public BraceWithArrayCall(ThymeSyntaxNode brace, ThymeSyntaxNode arr)
        {
            Array = arr;
            Brace = brace;
        }

        public override string Type => "BraceWithArrayCall";

        public ThymeSyntaxNode Array { get; }
        public ThymeSyntaxNode Brace { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Brace;
            yield return Array;
        }
    }
}
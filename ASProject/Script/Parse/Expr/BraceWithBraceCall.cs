using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 小括号和小括号调用
    /// </summary>
    public sealed class BraceWithBraceCall : ThymeSyntaxNode
    {
        public BraceWithBraceCall(ThymeSyntaxNode brace1, ThymeSyntaxNode brace2)
        {
            Brace1 = brace1;
            Brace2 = brace2;
        }

        public override string Type => "BraceWithBraceCall";

        public ThymeSyntaxNode Brace1 { get; }
        public ThymeSyntaxNode Brace2 { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Brace1;
            yield return Brace2;
        }
    }
}

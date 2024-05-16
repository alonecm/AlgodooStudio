using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class UnaryExpression : ThymeSyntaxNode
    {
        public UnaryExpression(ThymeToken op, ThymeSyntaxNode right)
        {
            Op = op;
            Right = right;
        }

        public override string Type => "UnaryExpression";

        public ThymeToken Op { get; }

        public ThymeSyntaxNode Right { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Right;
        }
    }
}

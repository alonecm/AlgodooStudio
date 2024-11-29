using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class UnaryExpression : ThymeSyntaxNode
    {
        public UnaryExpression(ThymeToken op, ThymeSyntaxNode right, Range range)
        {
            Op = op;
            Right = right;
            Range = range;
        }

        public override string Type => "UnaryExpression " + Op.Value;

        public ThymeToken Op { get; }

        public ThymeSyntaxNode Right { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Right;
        }
    }
}
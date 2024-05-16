using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class BinaryExpression : ThymeSyntaxNode
    {
        public BinaryExpression(ThymeSyntaxNode left, ThymeToken op, ThymeSyntaxNode right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public override string Type => "BinaryExpression " +Op.Value;

        public ThymeSyntaxNode Left { get; }
        
        public ThymeToken Op { get; }

        public ThymeSyntaxNode Right { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Left;
            yield return Right;
        }
    }
}

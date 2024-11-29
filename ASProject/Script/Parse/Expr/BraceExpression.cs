using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class BraceExpression : ThymeSyntaxNode
    {
        public BraceExpression(ThymeSyntaxNode node, Range range)
        {
            Node = node;
            Range = range;
        }

        public override string Type => "BraceExpression";

        public ThymeSyntaxNode Node { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Node;
        }
    }
}
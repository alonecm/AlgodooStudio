using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class BraceExpression : ThymeSyntaxNode
    {
        public BraceExpression(ThymeSyntaxNode node)
        {
            Node = node;
        }

        public override string Type => "BraceExpression";

        public ThymeSyntaxNode Node { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Node;
        }
    }
}

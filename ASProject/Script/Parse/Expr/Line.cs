using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Line : ThymeSyntaxNode
    {
        public Line(ThymeSyntaxNode node)
        {
            this.node = node;
        }

        public override string Type => "Line";

        public ThymeSyntaxNode node { get; }
        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return node;
        }
    }
}

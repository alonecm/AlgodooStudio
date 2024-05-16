using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Redirect : ThymeSyntaxNode
    {
        public Redirect(ThymeSyntaxNode name, ThymeSyntaxNode node)
        {
            Name = name;
            Node = node;
        }

        public override string Type => "Redirect";

        public ThymeSyntaxNode Name { get; }

        public ThymeSyntaxNode Node { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Name;
            yield return Node;
        }
    }
}

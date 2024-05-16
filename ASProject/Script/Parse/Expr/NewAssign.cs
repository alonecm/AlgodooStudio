using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class NewAssign : ThymeSyntaxNode
    {
        public NewAssign(ThymeSyntaxNode name, ThymeSyntaxNode node)
        {
            Name = name;
            Node = node;
        }

        public override string Type => "NewAssign";

        public ThymeSyntaxNode Name { get; }

        public ThymeSyntaxNode Node { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Name;
            yield return Node;
        }
    }
}

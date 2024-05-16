using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Array : ThymeSyntaxNode
    {
        public Array(ThymeSyntaxNode[] nodes)
        {
            Nodes = nodes;
        }

        public override string Type => "Array";

        public ThymeSyntaxNode[] Nodes { get; set; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var node in Nodes)
            {
                yield return node;
            }
        }
    }
}

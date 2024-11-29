using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public class Root : ThymeSyntaxNode
    {
        public Root(ThymeSyntaxNode[] nodes)
        {
            Nodes = nodes;
        }

        public override string Type => "Root";

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
using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public class Root : ThymeSyntaxNode
    {
        public Root(ThymeSyntaxNode[] nodes,Range range)
        {
            Nodes = nodes;
            Range = range;
        }

        public override string Type => "Root";

        public ThymeSyntaxNode[] Nodes { get; set; }
        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var node in Nodes)
            {
                yield return node;
            }
        }
    }
}
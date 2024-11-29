using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class RealParams : ThymeSyntaxNode
    {
        public RealParams(params ThymeSyntaxNode[] nodes)
        {
            Nodes = nodes;
        }

        public override string Type => "RealParams";

        public ThymeSyntaxNode[] Nodes { get; set; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var item in Nodes)
            {
                yield return item;
            }
        }
    }
}
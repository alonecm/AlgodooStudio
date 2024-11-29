using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class RealParams : ThymeSyntaxNode
    {
        public RealParams(Range range, params ThymeSyntaxNode[] nodes)
        {
            Range = range;
            Nodes = nodes;
        }

        public override string Type => "RealParams";

        public ThymeSyntaxNode[] Nodes { get; set; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var item in Nodes)
            {
                yield return item;
            }
        }
    }
}
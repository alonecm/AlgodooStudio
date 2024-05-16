using Dex.Analysis.Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Block : ThymeSyntaxNode
    {
        public Block(ThymeSyntaxNode[] nodes)
        {
            Nodes = nodes;
        }

        public override string Type => "Block";

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

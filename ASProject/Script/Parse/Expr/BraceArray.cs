using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 小括号数组
    /// </summary>
    public sealed class BraceArray : ThymeSyntaxNode
    {
        public BraceArray(params ThymeSyntaxNode[] nodes)
        {
            Nodes = nodes;
        }

        public override string Type => "BraceArray";

        public ThymeSyntaxNode[] Nodes { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var node in Nodes)
            {
                yield return node;
            }
        }
    }
}

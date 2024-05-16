using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 空表达式节点
    /// </summary>
    public sealed class EmptyExpression : ThymeSyntaxNode
    {
        public override string Type => "EmptyExpression";

        public ThymeToken Token { get; }

        public EmptyExpression(ThymeToken token)
        {
            Token = token;
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}

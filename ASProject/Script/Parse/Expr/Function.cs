using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Function : ThymeSyntaxNode
    {
        public Function(ThymeSyntaxNode @params, ThymeSyntaxNode block)
        {
            Params = @params;
            Block = block;
        }

        public override string Type => "Function";

        public ThymeSyntaxNode Params { get; }

        public ThymeSyntaxNode Block { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Params;
            yield return Block;
        }
    }
}
using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Function : ThymeSyntaxNode
    {
        public Function(ThymeSyntaxNode @params, ThymeSyntaxNode block, Range range)
        {
            Params = @params;
            Block = block;
            Range = range;
        }

        public override string Type => "Function";

        public ThymeSyntaxNode Params { get; }

        public ThymeSyntaxNode Block { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Params;
            yield return Block;
        }
    }
}
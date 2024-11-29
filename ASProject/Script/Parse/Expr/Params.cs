using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Params : ThymeSyntaxNode
    {
        public Params(Range range, params ThymeSyntaxNode[] tokens)
        {
            Tokens = tokens;
            Range = range;
        }

        public override string Type => "Params";

        public ThymeSyntaxNode[] Tokens { get; set; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var item in Tokens)
            {
                yield return item;
            }
        }
    }
}
using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Inf : ThymeSyntaxNode
    {
        public override string Type => "Inf";

        public Inf(ThymeToken value)
        {
            Range=value.Range;
        }
        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
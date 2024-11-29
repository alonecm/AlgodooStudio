using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Null : ThymeSyntaxNode
    {
        public override string Type => "Null";

        public override Range Range { get; }

        public Null(ThymeToken value)
        {
            Range = value.Range;
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Identifier : ThymeSyntaxNode
    {
        public override string Type => "Identifier " + Value.Value;

        public ThymeToken Value { get; }

        public override Range Range { get; }

        public Identifier(ThymeToken value)
        {
            Value = value;
            Range = value.Range;
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
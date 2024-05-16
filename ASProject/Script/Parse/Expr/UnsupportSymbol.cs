using Dex.Analysis.Parse;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class UnsupportSymbol : ThymeSyntaxNode
    {
        public override string Type => "UnsupportSymbol";

        public UnsupportSymbol(ThymeToken value)
        {
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}

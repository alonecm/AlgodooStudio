using Dex.Analysis.Parse;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Inf : ThymeSyntaxNode
    {
        public override string Type => "Inf";


        public Inf(ThymeToken value)
        {
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}

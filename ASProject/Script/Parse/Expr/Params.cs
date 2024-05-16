using Dex.Analysis.Parse;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Params : ThymeSyntaxNode
    {
        public Params(ThymeSyntaxNode[] tokens)
        {
            Tokens = tokens;
        }

        public override string Type => "Params";

        public ThymeSyntaxNode[] Tokens { get; set; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            foreach (var item in Tokens)
            {
                yield return item;
            }
        }
    }
}

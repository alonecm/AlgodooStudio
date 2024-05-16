using Dex.Analysis.Parse;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Ifstatement : ThymeSyntaxNode
    {
        public Ifstatement(ThymeSyntaxNode expr1, ThymeSyntaxNode expr2, ThymeSyntaxNode expr3)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Expr3 = expr3;
        }

        public override string Type => "Ifstatement";

        public ThymeSyntaxNode Expr1 { get; }
        public ThymeSyntaxNode Expr2 { get; }
        public ThymeSyntaxNode Expr3 { get; }


        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Expr1;
            yield return Expr2;
            yield return Expr3;
        }
    }

    public sealed class Alloc : ThymeSyntaxNode
    {
        public override string Type => "Alloc";

        public Alloc(ThymeToken value)
        {
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}

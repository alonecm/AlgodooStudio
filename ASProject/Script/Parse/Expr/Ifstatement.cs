using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Ifstatement : ThymeSyntaxNode
    {
        public Ifstatement(ThymeSyntaxNode expr1, ThymeSyntaxNode expr2, ThymeSyntaxNode expr3, Range range)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Expr3 = expr3;
            Range = range;
        }

        public override string Type => "Ifstatement";

        public ThymeSyntaxNode Expr1 { get; }
        public ThymeSyntaxNode Expr2 { get; }
        public ThymeSyntaxNode Expr3 { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Expr1;
            yield return Expr2;
            yield return Expr3;
        }
    }
}
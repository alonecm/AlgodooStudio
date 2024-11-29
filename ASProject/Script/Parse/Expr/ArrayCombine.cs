using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class ArrayCombine : ThymeSyntaxNode
    {
        public override Range Range { get; }

        public ArrayCombine(ThymeSyntaxNode array1, ThymeSyntaxNode array2, Range range)
        {
            Array1 = array1;
            Array2 = array2;
            Range = range;
        }
        public override string Type => "ArrayCombine";
        public ThymeSyntaxNode Array1 { get; }
        public ThymeSyntaxNode Array2 { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array1;
            yield return Array2;
        }
    }
}
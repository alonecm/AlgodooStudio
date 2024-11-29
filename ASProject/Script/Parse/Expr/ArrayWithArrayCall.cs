using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 数组和数组调用
    /// </summary>
    public sealed class ArrayWithArrayCall : ThymeSyntaxNode
    {

        public ArrayWithArrayCall(ThymeSyntaxNode array1, ThymeSyntaxNode array2, Range range)
        {
            Array1 = array1;
            Array2 = array2;
            Range = range;
        }

        public override string Type => "ArrayWithArrayCall";

        public ThymeSyntaxNode Array1 { get; }
        public ThymeSyntaxNode Array2 { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array1;
            yield return Array2;
        }
    }
}
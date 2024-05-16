using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class ArrayCall : ThymeSyntaxNode
    {
        public ArrayCall(ThymeSyntaxNode array, ThymeSyntaxNode callArray)
        {
            Array = array;
            CallArray = callArray;
        }

        public override string Type => "ArrayCall";

        public ThymeSyntaxNode Array { get; }
        public ThymeSyntaxNode CallArray { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Array;
            yield return CallArray;
        }
    }
}

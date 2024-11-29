using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class IdentifierCall : ThymeSyntaxNode
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="identifier">标识符</param>
        /// <param name="realParams">实参</param>
        public IdentifierCall(ThymeSyntaxNode identifier, ThymeSyntaxNode realParams, Range range)
        {
            Identifier = identifier;
            RealParams = realParams;
            Range = range;
        }
        public override string Type => "IdentifierCall";

        public ThymeSyntaxNode Identifier { get; }
        public ThymeSyntaxNode RealParams { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Identifier;
            yield return RealParams;
        }
    }
}
using Dex.Analysis.Parse;
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
        public IdentifierCall(ThymeSyntaxNode identifier, ThymeToken  startToken, ThymeSyntaxNode realParams, ThymeToken endToken)
        {
            Identifier = identifier;
            StartToken = startToken;
            RealParams = realParams;
            EndToken = endToken;
        }
        public override string Type => "IdentifierCall";

        public ThymeSyntaxNode Identifier { get; }
        public ThymeToken StartToken { get; }
        public ThymeSyntaxNode RealParams { get; }
        public ThymeToken EndToken { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Identifier;
            yield return RealParams;
        }
    }
}

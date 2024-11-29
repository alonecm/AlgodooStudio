using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Symbol : ThymeSyntaxNode
    {
        public Symbol(ThymeToken token)
        {
            Token = token;
            Range = token.Range;
        }

        public override string Type => Token.Value;

        public ThymeToken Token { get; }

        public override Range Range { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
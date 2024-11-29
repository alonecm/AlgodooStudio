using Dex.Analysis.Parse;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Symbol : ThymeSyntaxNode
    {
        public Symbol(ThymeToken token)
        {
            Token = token;
        }

        public override string Type => Token.Value;

        public ThymeToken Token { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }

    public sealed class Null : ThymeSyntaxNode
    {
        public override string Type => "Null";

        public Null(ThymeToken value)
        {
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class MemberCall : ThymeSyntaxNode
    {
        public override string Type => "MemberCall";

        public ThymeSyntaxNode Name { get; }
        public ThymeSyntaxNode Member { get; }

        public MemberCall(ThymeSyntaxNode name, ThymeSyntaxNode member)
        {
            Member = member;
            Name = name;
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Name;
            yield return Member;
        }
    }
}

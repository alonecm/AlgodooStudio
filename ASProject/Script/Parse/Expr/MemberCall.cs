using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class MemberCall : ThymeSyntaxNode
    {
        public override string Type => "MemberCall";

        public ThymeSyntaxNode Name { get; }
        public ThymeSyntaxNode Member { get; }

        public override Range Range { get; }

        public MemberCall(ThymeSyntaxNode name, ThymeSyntaxNode member, Range range)
        {
            Member = member;
            Range = range;
            Name = name;
        }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Name;
            yield return Member;
        }
    }
}
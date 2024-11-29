using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public abstract class ThymeSyntaxNode : ISyntaxNode
    {
        public abstract string Type { get; }

        public abstract IEnumerable<ISyntaxNode> GetChildren();
    }
}
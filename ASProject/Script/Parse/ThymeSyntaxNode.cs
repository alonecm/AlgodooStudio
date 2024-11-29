using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public abstract class ThymeSyntaxNode : ISyntaxNode
    {
        /// <summary>
        /// 节点范围
        /// </summary>
        public abstract Range Range { get; }

        public abstract string Type { get; }

        public abstract IEnumerable<ISyntaxNode> GetChildren();
    }
}
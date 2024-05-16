using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Call : ThymeSyntaxNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">调用对象</param>
        /// <param name="source">调用参数</param>
        public Call(ThymeSyntaxNode target, ThymeSyntaxNode source)
        {
            Target = target;
            Source = source;
        }

        public override string Type => "Call";

        public ThymeSyntaxNode Target { get; }
        public ThymeSyntaxNode Source { get; }

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            yield return Target;
            yield return Source;
        }
    }
}

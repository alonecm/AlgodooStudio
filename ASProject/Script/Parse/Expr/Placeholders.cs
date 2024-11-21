using Dex.Analysis.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    /// <summary>
    /// 异常占位符
    /// </summary>
    public sealed class Placeholders : ThymeSyntaxNode
    {
        public override string Type => "[Error]Placeholders[Error]";

        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}

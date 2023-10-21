using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    internal sealed class ArrayTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建数组节点
        /// </summary>
        /// <param name="methodName">数组节点所属方法的名称</param>
        /// <param name="range">数组节点表示的范围</param>
        internal ArrayTreeNode(string methodName, Range range) : base(range)
        {
            base.name = methodName;
            base.type = SyntaxNode_Type.Array;
        }
    }
}

using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 方法节点，用于存放一个方法的所有部分和内部定义所表示的范围
    /// </summary>
    internal sealed class MethodTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建方法节点
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="range">方法范围</param>
        internal MethodTreeNode(string methodName, Range range) : base(range)
        {
            base.name = methodName;
            base.type = SyntaxNode_Type.Method;
        }
    }
}

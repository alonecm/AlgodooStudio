using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgodooStudio.Basic;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 类节点，用于存放一个类的所有部分和内部定义所表示的范围
    /// </summary>
    internal sealed class ClassTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建类节点
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="range">类表示的范围</param>
        internal ClassTreeNode(string className, Range range) : base(range)
        {
            base.name = className;
            base.type = SyntaxNode_Type.Class;
        }

    }
}

using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 循环节点，用于存放一个判断的所有部分和内部定义所表示的范围
    /// </summary>
    internal sealed class LoopTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建循环节点
        /// </summary>
        /// <param name="methodName">循环节点所属方法的名称</param>
        /// <param name="range">判断节点表示的范围</param>
        public LoopTreeNode(string methodName, Range range) : base(range)
        {
            base.name = methodName;
            base.type = SyntaxNode_Type.Loop;
        }
    }
}

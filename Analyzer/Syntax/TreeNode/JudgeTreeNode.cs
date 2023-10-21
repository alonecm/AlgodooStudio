using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 判断节点，用于存放一个判断的所有部分和内部定义所表示的范围
    /// </summary>
    internal sealed class JudgeTreeNode : SyntaxTreeNode
    {

        /// <summary>
        /// 创建判断节点
        /// </summary>
        /// <param name="methodName">判断节点所属方法的名称</param>
        /// <param name="range">判断节点表示的范围</param>
        internal JudgeTreeNode(string methodName, Range range) : base(range)
        {
            base.name = methodName;
            base.type = SyntaxNode_Type.Judge;
        }
    }
}

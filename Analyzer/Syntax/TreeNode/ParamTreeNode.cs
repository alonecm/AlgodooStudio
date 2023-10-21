using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgodooStudio.Basic;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 参数节点，用于存放一个方法自身参数所表示的范围
    /// </summary>
    internal sealed class ParamTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建参数节点
        /// </summary>
        /// <param name="methodName">参数节点对应的方法名</param>
        /// <param name="range">参数括号表示的范围</param>
        internal ParamTreeNode(string methodName, Range range) : base(range)
        {
            base.name = methodName;
            base.type = SyntaxNode_Type.Parameter;
        }
    }
}

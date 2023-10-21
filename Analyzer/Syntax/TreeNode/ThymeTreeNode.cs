using AlgodooStudio.Attribute;
using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 脚本节点，用于存放当前脚本文件中所有部分和内部定义所表示的范围
    /// </summary>
    internal sealed class ThymeTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建Thyme树节点，用于表示脚本文件本身
        /// </summary>
        /// <param name="thymeName">当前脚本的名称</param>
        /// <param name="range">整个脚本文件的总字符数</param>
        public ThymeTreeNode(string thymeName, Range range) : base(range)
        {
            base.name = thymeName;
            base.type = SyntaxNode_Type.Thyme;
        }
    }
}

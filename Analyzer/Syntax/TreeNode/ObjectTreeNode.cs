using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgodooStudio.Basic;
using AlgodooStudio.Phun.SceneObject;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 场景对象节点，表示一个物体的
    /// </summary>
    internal sealed class ObjectTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建场景对象节点
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <param name="range">对象节点表示的范围</param>
        internal ObjectTreeNode(string objectType, Range range) : base(range)
        {
            base.name = objectType;
            base.type = SyntaxNode_Type.Object;
        }
    }
}

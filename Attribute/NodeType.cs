using AlgodooStudio.Base;
using System;

namespace AlgodooStudio.Attribute
{
    /// <summary>
    /// 用于描述一个节点类型的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class NodeType : ASAttribute
    {
        internal readonly SyntaxNode_Type type;

        /// <summary>
        /// 描述当前节点的类型
        /// </summary>
        internal NodeType(SyntaxNode_Type type)
        {
            this.type = type;
        }
    }
}
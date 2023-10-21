using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgodooStudio.Attribute;
using AlgodooStudio.Interface;

namespace AlgodooStudio.Basic
{
    /// <summary>
    /// 语法树节点抽象基类，用于构建语法树节点
    /// </summary>
    internal abstract class SyntaxTreeNode : ITreeNode<SyntaxTreeNode>
    {
        /// <summary>
        /// 子节点
        /// </summary>
        private SyntaxTreeNode[] childNode = new SyntaxTreeNode[0];
        /// <summary>
        /// 当前语法节点表示的一对括号所在的总字符位置
        /// </summary>
        private Range range;
        /// <summary>
        /// 节点名称
        /// </summary>
        protected string name;
        /// <summary>
        /// 节点描述的对象类型
        /// </summary>
        protected SyntaxNode_Type type = SyntaxNode_Type.None;


        /// <summary>
        /// 当前语法节点表示的一对括号所在的总字符位置
        /// </summary>
        public Range Range { get => range; }
        public SyntaxTreeNode[] ChildNode { get => childNode; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get => name; }
        /// <summary>
        /// 节点描述的对象类型
        /// </summary>
        public SyntaxNode_Type Type { get => type; }


        /// <summary>
        /// 创建语法树节点
        /// </summary>
        /// <param name="range">花括号所在字符位置</param>
        internal SyntaxTreeNode(Range range)
        {
            this.range = range;
        }


        public void Add(SyntaxTreeNode node)
        {
            //扩容
            SyntaxTreeNode[] tmp = new SyntaxTreeNode[childNode.Length + 1];
            Array.Copy(childNode, 0, tmp, 0,childNode.Length);
            tmp[tmp.Length - 1] = node;
            childNode = tmp;
            //扩展当前节点的范围，当前节点最后的范围就是最后一个节点大括号后一个字符
            range.max = node.range.max + 1;
        }
        public void Clear()
        {
            childNode = new SyntaxTreeNode[0];
        }
        public void RemoveAt(int index)
        {
            if (index < childNode.Length && index > -1)
            {
                SyntaxTreeNode[] tmp = new SyntaxTreeNode[childNode.Length - 1];
                Array.Copy(childNode, 0, tmp, 0, index);
                //判断不是最后一位
                if (index + 1 < childNode.Length)
                {
                    //更改表示范围，将前项的范围传给后项
                    for (int i = childNode.Length - 1; i > index; i--)
                    {
                        childNode[i].range = childNode[i - 1].range;
                    }
                    //复制内容
                    Array.Copy(childNode, index + 1, tmp, index, childNode.Length - index - 1);
                }
                //如果依然存在子节点则修改当前范围
                if (tmp.Length>0)
                {
                    //修改当前节点的范围
                    range.max = tmp[tmp.Length - 1].range.max + 1;
                }
                else
                {
                    //修改当前节点的范围
                    range.max = range.min + 1;
                }
                childNode = tmp;
            }
            else
            {
                throw new IndexOutOfRangeException("给定的索引值不在子节点数组的范围内");
            }
        }
    }
}

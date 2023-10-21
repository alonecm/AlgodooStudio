namespace AlgodooStudio.Base
{
    /// <summary>
    /// 内部可存放<typeparamref name="Object"/>的树形节点
    /// </summary>
    public class TreeNode<Object>
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        protected string name;

        /// <summary>
        /// 子节点
        /// </summary>
        protected Container<TreeNode<Object>> childNode = new Container<TreeNode<Object>>();

        /// <summary>
        /// 当前节点表示的对象
        /// </summary>
        protected Object nodeObj;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 当前节点表示的对象
        /// </summary>
        public Object NodeObj { get => nodeObj; set => nodeObj = value; }

        /// <summary>
        /// 子节点
        /// </summary>
        public Container<TreeNode<Object>> ChildNode { get => childNode; set => childNode = value; }

        /// <summary>
        /// 创建语法树节点
        /// </summary>
        public TreeNode(params TreeNode<Object>[] objects)
        {
            ChildNode.AddRange(objects);
        }

        /// <summary>
        /// 创建语法树节点
        /// </summary>
        public TreeNode(Object nodeObj, params TreeNode<Object>[] objects)
        {
            this.NodeObj = nodeObj;
            ChildNode.AddRange(objects);
        }

        /// <summary>
        /// 创建语法树节点
        /// </summary>
        public TreeNode(string name, params TreeNode<Object>[] objects)
        {
            this.name = name;
            ChildNode.AddRange(objects);
        }

        /// <summary>
        /// 创建语法树节点
        /// </summary>
        public TreeNode(string name, Object nodeObj, params TreeNode<Object>[] objects)
        {
            this.name = name;
            this.NodeObj = nodeObj;
            ChildNode.AddRange(objects);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void Add(TreeNode<Object> node)
        {
            ChildNode.Add(node);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddRange(params TreeNode<Object>[] objects)
        {
            ChildNode.AddRange(objects);
        }

        /// <summary>
        /// 清空子节点
        /// </summary>
        public void Clear()
        {
            ChildNode.Clear();
        }

        /// <summary>
        /// 移除指定索引处的子节点
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            ChildNode.RemoveAt(index);
        }
    }
}
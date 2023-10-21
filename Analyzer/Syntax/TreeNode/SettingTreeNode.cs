using AlgodooStudio.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Syntax.TreeNode
{
    /// <summary>
    /// 软件设置节点，用于存放软件设置的相关内容所表示的范围
    /// </summary>
    internal sealed class SettingTreeNode : SyntaxTreeNode
    {
        /// <summary>
        /// 创建一个设置树节点对象
        /// </summary>
        /// <param name="settingName">设置名</param>
        /// <param name="range">表示范围</param>
        internal SettingTreeNode(string settingName, Range range) : base(range)
        {
            base.name = settingName;
            base.type = SyntaxNode_Type.Setting;
        }
    }
}

using AlgodooStudio.ASProject.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    internal partial class ToolBoxWindow : DockContent
    {
        private Dictionary<string, ToolBase> tools;

        internal ToolBoxWindow()
        {
            InitializeComponent();

            tools = new Dictionary<string, ToolBase>();
        }

        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="tools"></param>
        public void AddTools(params ToolBase[] tools)
        {
            foreach (var item in tools)
            {
                if (!this.tools.ContainsKey(item.ToString()))
                {
                    this.tools.Add(item.ToString(), item);
                }
            }
            this.toolList.DataSource = this.tools.Values.ToArray();
        }
        /// <summary>
        /// 点击某项时直接执行OnActive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolList_Click(object sender, EventArgs e)
        {
            if (this.toolList.SelectedItem != null)
            {
                (this.toolList.SelectedItem as ToolBase).OnUse();
            }
        }
    }
}

using AlgodooStudio.ASProject.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    public partial class ToolBoxWindow : DockContent
    {
        public ToolBoxWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="tools"></param>
        public void AddTools(params ToolBase[] tools)
        {
            this.toolList.Items.AddRange(tools);
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
                (this.toolList.SelectedItem as ToolBase).OnActive();
            }
        }
    }
}

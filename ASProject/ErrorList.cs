using AlgodooStudio.ASProject.Script.Parse;
using Dex.Analysis.Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 错误列表窗口
    /// </summary>
    public partial class ErrorListWindow : DockContent
    {
        public ErrorListWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新显示错误信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="diagnostic"></param>
        public void UpdateErrors(string fileName, DiagnosticsCollection diagnostic)
        {
            errorList.BeginUpdate();
            //清理
            errorList.Items.Clear();
            foreach (var item in diagnostic)
            {
                var i = new ListViewItem();
                i.Text = (errorList.Items.Count + 1).ToString();
                i.SubItems.Add(item.Range.Min.ToString());
                i.SubItems.Add(item.Range.Max.ToString());
                i.SubItems.Add(item.Message);
                i.SubItems.Add(Path.GetFileName(fileName));

                errorList.Items.Add(i);
            }
            errorList.EndUpdate();
        }

        private void errorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errorList.SelectedItems.Count>0)
            {
                var item = errorList.SelectedItems[0];
                Program.SelectError(item.SubItems[4].Text, new Dex.Common.Range(int.Parse(item.SubItems[1].Text), int.Parse(item.SubItems[2].Text)));
            }
        }
    }
}

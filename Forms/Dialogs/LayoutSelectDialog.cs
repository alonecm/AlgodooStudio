using System;
using System.Windows.Forms;

using AlgodooStudio.Manager;

namespace AlgodooStudio.Forms.Dialogs
{
    public partial class LayoutSelectDialog : Form
    {
        /// <summary>
        /// 应用的布局
        /// </summary>
        private string applyLayoutName;
        /// <summary>
        /// 上一个选中项
        /// </summary>
        private ListViewItem lastItem;

        /// <summary>
        /// 创建布局管理对话框
        /// </summary>
        public LayoutSelectDialog()
        {
            InitializeComponent();
            //显示已经载入的所有布局
            ShowLayouts();
        }

        /// <summary>
        /// 应用的布局
        /// </summary>
        internal string ApplyLayoutName { get => applyLayoutName; }

        /// <summary>
        /// 显示载入的所有布局
        /// </summary>
        private void ShowLayouts()
        {
            layoutList.Items.Clear();
            foreach (var item in LayoutManager.LayoutNames)
            {
                if (LayoutManager.CurrentLayoutName!=item)
                {
                    layoutList.Items.Add(new ListViewItem(new string[2] { item, "" }));
                }
                else
                {
                    ListViewItem tmp = new ListViewItem(new string[2] { item, "√" });
                    layoutList.Items.Add(tmp);
                    lastItem = tmp;
                }
            }
        }
        /// <summary>
        /// 应用布局
        /// </summary>
        private void ApplyLayout()
        {
            //如果有上一项
            if (lastItem!=null)
            {
                //清空上一项
                lastItem.SubItems[1].Text = "";
            }
            //应用这一项
            applyLayoutName = layoutList.SelectedItems[0].Text;
            layoutList.SelectedItems[0].SubItems[1].Text = "√";
            lastItem = layoutList.SelectedItems[0];
            equip.Enabled = false;
        }


        /// <summary>
        /// 重命名布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rename_Click(object sender, EventArgs e)
        {
            TextGetDialog tgd = new TextGetDialog();
            tgd.Title = "重命名布局";
            tgd.IsNameValidCheck = true;
            if (tgd.ShowDialog()==DialogResult.OK)
            {
                LayoutManager.ReNameLayout(layoutList.SelectedItems[0].Text, tgd.InputText);
                layoutList.SelectedItems[0].SubItems[0].Text = tgd.InputText;
            }
            tgd.Dispose();
        }
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equip_Click(object sender, EventArgs e)
        {
            ApplyLayout();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Click(object sender, EventArgs e)
        {
            if (layoutList.SelectedItems.Count>0)
            {
                LayoutManager.DeleteLayout(layoutList.SelectedItems[0].Text);
                layoutList.Items.RemoveAt(layoutList.SelectedItems[0].Index);
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, EventArgs e)
        {
            if (applyLayoutName==null)
            {
                MBox.Showlog("未应用任何布局");
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                
                if (applyLayoutName==LayoutManager.CurrentLayoutName)
                {
                    MBox.Showlog("布局未发生改变");
                    DialogResult = DialogResult.Cancel;
                }
                DialogResult = DialogResult.OK;
            }
        }


        /// <summary>
        /// 检查所选项是否为已启用的项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //删除与重命名按钮启用与禁用
            if (e.Item.Text == LayoutManager.CurrentLayoutName || e.Item.Text == "Default")
            {
                delete.Enabled = rename.Enabled = false;
            }
            else
            {
                delete.Enabled = rename.Enabled = true;
            }

            //应用按钮启用与禁用
            if (e.Item.SubItems[1].Text!="√")
            {
                equip.Enabled = true;
            }
            else
            {
                equip.Enabled = false;
            }
        }
    }
}

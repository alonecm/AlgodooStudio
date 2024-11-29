using AlgodooStudio.ASProject.Support;
using System;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    public partial class AutoExecuteItemManageDialog : Form
    {
        public AutoExecuteItemManageDialog()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //载入已载入项
            foreach (var item in Program.AutoExecuteItems)
            {
                AddItem(item);
            }
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            using (var ted = new TextEditDialog())
            {
                if (ted.ShowDialog() == DialogResult.Yes)
                {
                }
            }
        }

        private void switchItemState_Click(object sender, EventArgs e)
        {
        }

        private void removeItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="item"></param>
        private void AddItem(AutoExecuteItem item)
        {
            var listItem = new ListViewItem(itemList.Items.Count.ToString());
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.IsEnabled ? "√" : "x" });
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.Type.ToString() });
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.Content.ToString() });
            itemList.Items.Add(listItem);
        }

        /// <summary>
        /// 移除指定位置的一项
        /// </summary>
        private void RemoveItem(int index)
        {
        }
    }
}
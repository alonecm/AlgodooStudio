using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Support;
using System;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    //TODO:自启动文件应该在打开对话框时进行加载，在关闭对话框时更新
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
            string content = "";
            while (true)
            {
                using (var ted = new TextEditDialog(content))
                {
                    var result = ted.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.Yes:
                            //存在异常则拒绝添加并重新编辑
                            if (ThymeParser.GetAST(ted.EditedText).Item2.Count > 0)
                            {
                                MBox.ShowWarning("检查到所添加代码中出现异常，即将返回...");
                                content = ted.EditedText;
                                continue;
                            }

                            if (ted.EditedText!=string.Empty)//空字符串不添加
                            {
                                //添加成功则更新列表
                                AddItem(Program.AutoExecuteItems.Add(true,
                                     ted.EditedText.IndexOf("reflection.executefile") == 0 ?
                                    AutoExecuteItemType.File : AutoExecuteItemType.Code, ted.EditedText, new Dex.Common.Range()));
                            }
                            return;
                        default:
                            return;
                    }
                }
            }
        }

        private void switchItemState_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in itemList.SelectedItems)
                {
                    Program.AutoExecuteItems[item.Index].IsEnabled = !Program.AutoExecuteItems[item.Index].IsEnabled;
                    item.SubItems[0].Text = Program.AutoExecuteItems[item.Index].IsEnabled ? "√" : "x";
                }
            }
        }

        private void removeItem_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItems.Count>0)
            {
                if (MBox.ShowWarningOKCancel("确定移除所选项吗？")==DialogResult.OK)
                {
                    while (true)
                    {
                        var itemIndex = itemList.SelectedItems[0].Index;
                        RemoveItem(itemIndex);
                        Program.AutoExecuteItems.RemoveAt(itemIndex);
                        if (itemList.SelectedItems.Count<=0) break;
                    }
                }
            }
        }

        private void itemList_ItemActivate(object sender, EventArgs e)
        {
            var editingItem = itemList.SelectedItems[0];
            string content = editingItem.SubItems[2].Text;
            while (true)
            {
                using (var ted = new TextEditDialog(content))
                {
                    var result = ted.ShowDialog();
                    switch (result)
                    {
                        case DialogResult.Yes:
                            //存在异常拒绝添加并重新编辑
                            if (ThymeParser.GetAST(ted.EditedText).Item2.Count > 0)
                            {
                                MBox.ShowWarning("检查到所添加代码中出现异常，即将返回...");
                                content = ted.EditedText;
                                continue;
                            }

                            //不存在则优先移除这个项然后重新添加
                            var idx = editingItem.Index;
                            RemoveItem(idx);
                            Program.AutoExecuteItems.RemoveAt(idx);

                            if (ted.EditedText != string.Empty)//空字符串就不加了
                            {
                                AddItem(Program.AutoExecuteItems.Add(true,
                                 ted.EditedText.IndexOf("reflection.executefile") == 0 ?
                                AutoExecuteItemType.File : AutoExecuteItemType.Code, ted.EditedText, new Dex.Common.Range()));
                            }
                            return;
                        default:
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="item"></param>
        private void AddItem(AutoExecuteItem item)
        {
            var listItem = new ListViewItem(item.IsEnabled ? "√" : "x");
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.Type.ToString() });
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = item.Content.ToString() });
            itemList.Items.Add(listItem);
        }

        /// <summary>
        /// 移除指定位置的一项
        /// </summary>
        private void RemoveItem(int index)
        {
            itemList.Items.RemoveAt(index);
        }

        
    }
}
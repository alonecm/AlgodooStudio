using AlgodooStudio.ASProject.Script;
using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Support;
using Dex.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    public partial class AutoExecuteItemManageDialog : Form
    {
        private AutoExecuteItemCollection autoExecuteItems = new AutoExecuteItemCollection();
        public AutoExecuteItemManageDialog()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //读取自启动项
            if (!Program.IsTrueAlgodooPath)
            {
                MBox.ShowError("Algodoo根目录未设置正确！请到设置中进行设置！");
                LogWriter.WriteError("Algodoo根目录路径异常");
                Close();
                return;
            }
            var path = Program.Setting.AlgodooPath + "\\autoexec.cfg";
            if (!File.Exists(path))
            {
                MBox.ShowError("找不到Algodoo自启动文件！");
                LogWriter.WriteError("Algodoo自启动文件丢失");
                Close();
                return;
            }
            LogWriter.WriteInfo("解析自启动文件...");
            var parserActive = ThymeParser.GetAST(File.ReadAllText(path, Encoding.UTF8), false);
            var rootActive = parserActive.Item1;
            if (parserActive.Item2.Count > 0)
            {
                MBox.ShowWarning("自启动文件中存在语法错误，已输出到日志文件中，请查看");
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in parserActive.Item2)
                {
                    stringBuilder.AppendLine($"{item.Range}[{item.Type}] {item.Message}");
                }
                LogWriter.WriteWarn(stringBuilder.ToString());
            }

            //添加启用项
            var rgEnable = new ThymeReGenerator();
            foreach (var item in rootActive.Nodes)
            {
                var content = rgEnable.ReGenerate(item);
                if (content.IndexOf("reflection.executefile") == 0)
                {
                    autoExecuteItems.Add(true, AutoExecuteItemType.File, content, item.Range);
                }
                else
                {
                    autoExecuteItems.Add(true, AutoExecuteItemType.Code, content, item.Range);
                }
            }

            //检查是否存在禁用项托管文件
            if (File.Exists(".\\Manage\\disabled_execute_item.manage"))
            {
                LogWriter.WriteInfo("解析托管文件...");

                var parserInactive = ThymeParser.GetAST(File.ReadAllText(".\\Manage\\disabled_execute_item.manage", Encoding.UTF8),false);
                var rootInactive = parserInactive.Item1;
                //添加禁用项
                var egEnable = new ThymeReGenerator();
                foreach (var item in rootInactive.Nodes)
                {
                    var content = egEnable.ReGenerate(item);
                    if (content.IndexOf("reflection.executefile") == 0)
                    {
                        autoExecuteItems.Add(false, AutoExecuteItemType.File, content, item.Range);
                    }
                    else
                    {
                        autoExecuteItems.Add(false, AutoExecuteItemType.Code, content, item.Range);
                    }
                }
            }

            //载入已载入项
            foreach (var item in autoExecuteItems)
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
                                AddItem(autoExecuteItems.Add(true,
                                     ted.EditedText.IndexOf("reflection.executefile") == 0 ?
                                    AutoExecuteItemType.File : AutoExecuteItemType.Code, ted.EditedText, new Dex.Common.Range()));
                                autoExecuteItems.UpdateByIndex(itemList.Items.Count - 1);
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
                List<int> index = new List<int>();
                foreach (ListViewItem item in itemList.SelectedItems)
                {
                    autoExecuteItems[item.Index].IsEnabled = !autoExecuteItems[item.Index].IsEnabled;
                    item.SubItems[0].Text = autoExecuteItems[item.Index].IsEnabled ? "√" : "x";
                    index.Add(item.Index);
                }
                autoExecuteItems.UpdateByIndex(index.ToArray());
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
                        autoExecuteItems.RemoveAt(itemIndex);
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
                            autoExecuteItems.RemoveAt(idx);

                            if (ted.EditedText != string.Empty)//空字符串就不加了
                            {
                                AddItem(autoExecuteItems.Add(true,
                                 ted.EditedText.IndexOf("reflection.executefile") == 0 ?
                                AutoExecuteItemType.File : AutoExecuteItemType.Code, ted.EditedText, new Dex.Common.Range()));
                                autoExecuteItems.UpdateByIndex(itemList.Items.Count - 1);
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
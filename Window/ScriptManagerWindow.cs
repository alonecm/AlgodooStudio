using AlgodooStudio.Window.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Window
{
    /// <summary>
    /// 脚本管理器
    /// </summary>
    internal partial class ScriptManagerWindow : DockContent
    {
        /// <summary>
        /// 是否就绪可以管理
        /// </summary>
        private bool isReadyToManage = false;

        /// <summary>
        /// 加入到管理的脚本集合(路径，管理项)
        /// </summary>
        private Dictionary<string, ScriptManageItem> scripts = new Dictionary<string, ScriptManageItem>();

        internal ScriptManagerWindow()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            //主题设定
            scriptList.BackColor = Setting.theme.BackColor1;
            scriptList.ForeColor = Setting.theme.VarNameColor;
            split.BackColor = Setting.theme.BackColor2;
            BackColor = Setting.theme.BackColor2;
            toolbar.BackColor = Setting.theme.BackColor2;
            toolbar.Renderer = QuickToolsRenderer.GetRenderer();
            rightMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            //列宽设定
            scriptList.Columns[1].Width = scriptList.ClientSize.Width - scriptList.Columns[0].Width;
            //检查文件存在
            CheckRegFileExist();
            //加载内容
            LoadScriptItems();
            //显示内容
            ShowScripts();
        }

        /// <summary>
        /// 检查注册文件是否存在
        /// </summary>
        private void CheckRegFileExist()
        {
            //检查注册文件是否存在，不存在则创建文件
            if (!File.Exists(Setting.scriptPathRegistFile))
            {
                using (FileStream fs = new FileStream(Setting.scriptPathRegistFile, FileMode.Create))
                {
                }
            }
        }

        /// <summary>
        /// 加载所有的管理项
        /// </summary>
        private void LoadScriptItems()
        {
            using (StreamReader sr = new StreamReader(Setting.scriptPathRegistFile))
            {
                while (true)
                {
                    string item = sr.ReadLine();
                    if (item == null || item.Length == 0)
                    {
                        break;
                    }
                    //获取全部的信息
                    FileInfo file = new FileInfo(item.Substring(item.IndexOf('\"') + 1, item.LastIndexOf('\"') - item.IndexOf('\"') - 1));
                    scripts.Add(file.FullName, new ScriptManageItem(file, !item.StartsWith("//")));
                }
            }
        }

        /// <summary>
        /// 显示所有脚本
        /// </summary>
        private void ShowScripts()
        {
            scriptList.BeginUpdate();
            //scriptList.Items.Clear();
            foreach (var item in scripts.Values)
            {
                Add(item);
            }
            scriptList.EndUpdate();
        }

        /// <summary>
        /// 输出管理内容
        /// </summary>
        private void WriteItems()
        {
            using (StreamWriter sw = new StreamWriter(Setting.scriptPathRegistFile))
            {
                foreach (var item in scripts.Values)
                {
                    string tmp = "";
                    if (!item.IsEnabled)
                    {
                        tmp = "//";
                    }
                    tmp += $"Reflection.ExecuteFile \"{item.Path}\";";
                    sw.WriteLine(tmp);
                }
            }
        }

        /*
        添加：选择性(右键菜单，工具栏)，从外部添加，从资管添加
        启用：全部(按钮)、选择性(复选框)
        停用：全部(按钮)、选择性(复选框)
        移除：全部(按钮)、选择性(选中后右键菜单，选中后工具栏)

        所有操作完成后要及时导出到注册文件中
        在algodoo中创建文件管理目录和注册式文件
        */

        /// <summary>
        /// 添加需要使用的脚本
        /// </summary>
        /// <param name="item">需要管理的项</param>
        private void Add(ScriptManageItem item)
        {
            isReadyToManage = false;
            ListViewItem l = new ListViewItem();
            l.Checked = item.IsEnabled;
            l.Text = Path.GetFileName(item.FileName);
            l.Tag = item.Path;
            l.SubItems.Add(new ListViewItem.ListViewSubItem(l, item.Path));
            scriptList.Items.Add(l);
        }

        /// <summary>
        /// 添加脚本
        /// </summary>
        private void Add()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "添加脚本";
                ofd.Multiselect = true;
                ofd.Filter = "Thyme脚本|*.thm|其他文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Setting.IsResetAlgodoo = true;
                    scriptList.BeginUpdate();
                    foreach (var item in ofd.FileNames)
                    {
                        ScriptManageItem smi = new ScriptManageItem(new FileInfo(item));
                        if (!scripts.ContainsKey(smi.Path))
                        {
                            scripts.Add(smi.Path, smi);//先向字典中添加本体
                            Add(smi);//随后才能向列表中添加项目(否则复选框那会出问题)
                        }
                    }
                    scriptList.EndUpdate();
                    WriteItems();
                    isReadyToManage = true;
                }
            }
        }

        /// <summary>
        /// 移除指定项
        /// </summary>
        private void Remove(ListViewItem l)
        {
            isReadyToManage = false;
            scriptList.Items.Remove(l);
            scripts.Remove(l.Tag.ToString());
        }

        /// <summary>
        /// 移除所有选定的脚本
        /// </summary>
        private void Remove()
        {
            scriptList.BeginUpdate();
            Setting.IsResetAlgodoo = true;
            foreach (ListViewItem item in scriptList.SelectedItems)
            {
                Remove(item);
            }
            scriptList.EndUpdate();
            WriteItems();
            isReadyToManage = true;
        }

        /// <summary>
        /// 设定全部项的启用状态
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        private void SetEnableState(bool isEnabled)
        {
            //禁用也需要重置
            if (!isEnabled)
            {
                Setting.IsResetAlgodoo = true;
            }
            isReadyToManage = false;//开启以防止频繁选中
            foreach (ListViewItem item in scriptList.Items)
            {
                item.Checked = isEnabled;
                scripts[item.Tag.ToString()].IsEnabled = isEnabled;
            }
            isReadyToManage = true;
            WriteItems();
        }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTo_Click(object sender, EventArgs e)
        {
            Add();
        }

        /// <summary>
        /// 复选框启用与禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (isReadyToManage)
            {
                Setting.IsResetAlgodoo = true;//启用和关闭库都需要重置
                //如果选中项是选中的则启用否则则禁用
                scripts[e.Item.Tag.ToString()].IsEnabled = e.Item.Checked;
                WriteItems();
            }
        }

        /// <summary>
        /// 窗口彻底显示完毕才可进入管理模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptManagerWindow_Shown(object sender, EventArgs e)
        {
            isReadyToManage = true;
        }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        /// <summary>
        /// 移除选定项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelected_Click(object sender, EventArgs e)
        {
            Remove();
        }

        /// <summary>
        /// 移除选定项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        /// <summary>
        /// 选定后改变工具栏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            toolStripSeparator3.Visible =
                移除ToolStripMenuItem.Visible =
                    toolStripSeparator4.Visible =
                    在资源管理器中打开ToolStripMenuItem.Visible =
                    toolStripSeparator2.Visible = removeSelected.Visible = (scriptList.SelectedItems.Count > 0);
            if (scriptList.SelectedItems.Count > 1)
            {
                toolStripSeparator4.Visible = 在资源管理器中打开ToolStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 启用全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableAll_Click(object sender, EventArgs e)
        {
            SetEnableState(true);
        }

        /// <summary>
        /// 禁用全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disableAll_Click(object sender, EventArgs e)
        {
            SetEnableState(false);
        }

        /// <summary>
        /// 移除全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeAll_Click(object sender, EventArgs e)
        {
            Setting.IsResetAlgodoo = true;
            scriptList.Items.Clear();
            scripts.Clear();
            toolStripSeparator3.Visible =
                toolStripSeparator4.Visible =
                在资源管理器中打开ToolStripMenuItem.Visible =
                toolStripSeparator2.Visible =
                removeSelected.Visible = false;
            WriteItems();
        }

        /// <summary>
        /// 在内置资源管理器打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 在资源管理器中打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResourceExplorerWindow re = new ResourceExplorerWindow(scriptList.SelectedItems[0].Tag.ToString());
            re.Show(this.DockPanel, DockState.Document);
        }

        private void scriptList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                //水平和垂直居中
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                //创建笔刷
                using (SolidBrush bc = new SolidBrush(Setting.theme.BackColor2))
                {
                    e.Graphics.FillRectangle(bc, e.Bounds);
                    bc.Color = Setting.theme.VarNameColor;
                    e.Graphics.DrawString(e.Header.Text, e.Font, bc, e.Bounds, sf);
                }
            }
        }

        private void scriptList_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; //采用系统默认方式绘制项
        }

        private void scriptList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; //采用系统默认方式绘制项
        }

        /// <summary>
        /// 列宽自适应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptManagerWindow_SizeChanged(object sender, EventArgs e)
        {
            scriptList.Columns[1].Width = scriptList.ClientSize.Width - scriptList.Columns[0].Width;
        }

        /// <summary>
        /// 列宽自适应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptList_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            scriptList.Columns[1].Width = scriptList.ClientSize.Width - scriptList.Columns[0].Width;
        }
    }
}
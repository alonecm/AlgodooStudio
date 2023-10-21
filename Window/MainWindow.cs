using AlgodooStudio.Base;
using AlgodooStudio.Interface;
using AlgodooStudio.Interface.Edit;
using AlgodooStudio.Manager;
using AlgodooStudio.Window.Dialogs;
using AlgodooStudio.Window.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Window
{
    internal partial class MainWindow : Form
    {
        /// <summary>
        /// 软件消息
        /// </summary>
        internal string TipsText
        {
            get
            {
                return tips.Text.Substring(tips.Text.IndexOf('：') + 1);
            }
            set
            {
                tips.Text = "消息：" + value;
            }
        }

        /// <summary>
        /// 生成主窗体
        /// </summary>
        internal MainWindow()
        {
            InitializeComponent();
            //设定先前设定好的主题
            SetTheme(Setting.theme);
            //设定先前设定好的布局
            SetLayout(Setting.layoutName);
            //检查启用与禁用状态
            CheckStatus();
        }

        /// <summary>
        /// 设定主题
        /// </summary>
        /// <param name="theme"></param>
        private void SetTheme(Theme theme)
        {
            //关闭已开启的所有窗体
            LayoutManager.CloseAllForms(this.workPlace);
            //设置初始主题
            this.workPlace.Theme = theme.WeifenluoTheme;
            this.workPlace.BackColor = theme.BackColor1;
            this.statusBar.BackColor = theme.BackColor2;
            this.quickTools.BackColor = theme.BackColor2;
            this.tableLayout.BackColor = theme.BackColor2;
            //传递自定义渲染器
            this.statusBar.Renderer = StatusBarRenderer.GetRenderer();
            this.quickTools.Renderer = QuickToolsRenderer.GetRenderer();
            this.mainMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            //设置主题
            Setting.theme = theme;
        }

        /// <summary>
        /// 设定布局
        /// </summary>
        /// <param name="layoutName">布局名</param>
        private void SetLayout(string layoutName)
        {
            LayoutManager.ApplyLayout(this.workPlace, layoutName);
            Setting.layoutName = layoutName;
        }

        /// <summary>
        /// 选项启用与禁用检查
        /// </summary>
        private void CheckStatus()
        {
            //浮动按钮启用与禁用
            if (this.workPlace.Contents.Count > 0)
            {
                浮动ToolStripMenuItem.Enabled = true;
            }
            else
            {
                浮动ToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// 编辑菜单栏显示控制
        /// </summary>
        private void EditToolStripShow()
        {
            //控制按钮显示
            替换ToolStripMenuItem.Visible = (workPlace.ActiveContent is IReplace);
            查找ToolStripMenuItem.Visible = (workPlace.ActiveContent is ISearch);
            toolStripSeparator5.Visible = (workPlace.ActiveContent is IReplace || workPlace.ActiveContent is ISearch);
            toolStripSeparator4.Visible = 复制ToolStripMenuItem.Visible = 剪切ToolStripMenuItem.Visible = 粘贴ToolStripMenuItem.Visible = 删除ToolStripMenuItem.Visible = (workPlace.ActiveContent is IManage);
            toolStripSeparator6.Visible = 撤消ToolStripMenuItem.Visible = 重做ToolStripMenuItem.Visible = (workPlace.ActiveContent is IOperateControl);
            全选ToolStripMenuItem.Visible = (workPlace.ActiveContent is ISelectAll);
        }

        #region 主菜单按钮文字颜色设置

        private void 文件ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            文件ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 编辑ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            编辑ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 工具ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            工具ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 窗口ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            窗口ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 扩展ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            插件ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 帮助ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 视图ToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            视图ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
        }

        private void 文件ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            文件ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 编辑ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            编辑ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 视图ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            视图ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 工具ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            工具ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 窗口ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            窗口ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 扩展ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            插件ToolStripMenuItem.ForeColor = Color.Black;
        }

        private void 帮助ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem.ForeColor = Color.Black;
        }

        #endregion 主菜单按钮文字颜色设置

        #region 窗口

        /// <summary>
        /// 新建(脚本编辑器)窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            //查找已经存在的，未保存成文件的编辑器数量
            foreach (var item in workPlace.Contents)
            {
                //是新文档则设定为末尾序号
                if (item.DockHandler.TabText.StartsWith("NewDocument"))
                {
                    int sIndex = item.DockHandler.TabText.LastIndexOf("*");//获取星花的位置
                    int pIndex = item.DockHandler.TabText.LastIndexOf(" ") + 1;//获取数字的开始位置
                    //如果星花不存在则默认是最后一位
                    if (sIndex == -1)
                    {
                        sIndex = item.DockHandler.TabText.Length;
                    }
                    i = int.Parse(item.DockHandler.TabText.Substring(pIndex, sIndex - pIndex));
                }
            }
            ScriptEditorWindow se = new ScriptEditorWindow("", "NewDocument " + (i + 1));
            se.Show(this.workPlace, DockState.Document);
            //置空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        /// <summary>
        /// 浮动选中的窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 浮动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果激活的窗体不是空的则可以启用
            if (this.workPlace.ActiveContent != null)
            {
                this.workPlace.ActiveContent.DockHandler.IsFloat = true;
            }
            else
            {
                浮动ToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// 浮动所有窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 全部浮动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContentCollection contents = this.workPlace.Contents;
            foreach (var item in contents)
            {
                item.DockHandler.IsFloat = true;
            }
        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭所有窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutManager.CloseAllForms(this.workPlace);
        }

        /// <summary>
        /// 关闭除选中窗口以外的所有窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭除选中窗口以外的所有窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContentCollection contents = this.workPlace.Contents;
            //获取除选中窗体以外的其他窗体
            List<IDockContent> temp = new List<IDockContent>();
            foreach (var item in contents)
            {
                if (item != this.workPlace.ActiveContent)
                {
                    temp.Add(item);
                }
            }
            //移除其他窗体
            while (true)
            {
                if (temp.Count == 0)
                {
                    break;
                }
                else
                {
                    temp[0].DockHandler.Close();
                    temp.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// 保存布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextGetDialog tgd = new TextGetDialog();
            tgd.Title = "设定保存的布局名称";
            tgd.IsNameValidCheck = true;
            if (tgd.ShowDialog() == DialogResult.OK)
            {
                LayoutManager.SaveXMLFile(this.workPlace, tgd.InputText);
            }
            tgd.Dispose();
            tgd = null;
        }

        /// <summary>
        /// 管理布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 管理布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LayoutSelectDialog lsd = new LayoutSelectDialog())
            {
                if (lsd.ShowDialog() == DialogResult.OK)
                {
                    if (MBox.ShowWarningOKCancel("即将切换布局，请确保必要信息已经保存") == DialogResult.OK)
                    {
                        LayoutManager.ApplyLayout(this.workPlace, lsd.ApplyLayoutName);
                    }
                }
            }
        }

        /// <summary>
        /// 重置布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 重置布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MBox.ShowWarningOKCancel("确定要重置布局吗？（请确保必要信息已保存）") == DialogResult.OK)
            {
                LayoutManager.ApplyLayout(this.workPlace, "Default");
            }
        }

        #region 浮动按钮启用与禁用

        /// <summary>
        /// 添加窗体时启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workPlace_ContentAdded(object sender, DockContentEventArgs e)
        {
            浮动ToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// 移除窗体时检查后禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workPlace_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (this.workPlace.Contents.Count == 0)
            {
                浮动ToolStripMenuItem.Enabled = false;
            }
            //清空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        /// <summary>
        /// 激活后的窗体发生变动时启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workPlace_ActiveContentChanged(object sender, EventArgs e)
        {
            浮动ToolStripMenuItem.Enabled = true;
            //看当前窗口是否可以被编辑
            if (workPlace.ActiveContent is IEditable)
            {
                EditToolStripShow();
                编辑ToolStripMenuItem.Visible = true;
            }
            else
            {
                编辑ToolStripMenuItem.Visible = false;
            }
            //看当前窗口是否可以快速输入
            toolStripSeparator14.Visible = 快速输入ToolStripMenuItem.Visible = (workPlace.ActiveContent is IQuickInsertable);
        }

        #endregion 浮动按钮启用与禁用

        #endregion 窗口

        #region 主窗口

        /// <summary>
        /// 启动时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            //启动计时
            Program.loadTimer.Stop();
            float t = (float)Program.loadTimer.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            TipsText = $"已就绪，本次启动用时：{t:F2}秒";
        }
        /// <summary>
        /// 获取主窗体中存在的子窗体
        /// </summary>
        /// <typeparam name="T">需要查找的子窗体类型</typeparam>
        /// <returns>查找到子窗体返回子窗体对象，未找到返回NULL</returns>
        internal T GetForm<T>() where T : IDockContent
        {
            DockContentCollection contents = this.workPlace.Contents;
            foreach (var item in contents)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return default(T);
        }
        #endregion 主窗口

        #region 文件

        private void 脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.IsNameValidCheck = true;
                tgd.Title = "新建脚本";
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    ScriptEditorWindow se = new ScriptEditorWindow("", tgd.InputText);
                    se.Show(this.workPlace, DockState.Document);
                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "选择需要编辑的脚本";
                ofd.Multiselect = true;
                ofd.Filter = "Thyme脚本|*.thm|cfg配置文件|*.cfg|其他文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in ofd.FileNames)
                    {
                        ScriptEditorWindow se = new ScriptEditorWindow(new FileInfo(item));
                        se.Show(this.workPlace, DockState.Document);
                    }
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存当前激活的且可以保存的窗口
            if (workPlace.ActiveContent is ISave)
            {
                (workPlace.ActiveContent as ISave).Save();
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //另存为当前激活的且可以保存的窗口
            if (workPlace.ActiveContent is ISave)
            {
                (workPlace.ActiveContent as ISave).SaveAs();
            }
        }

        private void 全部保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //对当前获得焦点的页面执行保存功能（如果有保存功能）
            foreach (var item in workPlace.Contents)
            {
                //是可以保存的窗口则保存
                if (item is ISave)
                {
                    (item as ISave).Save();
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion 文件

        #region 编辑

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以查找则执行查找
            if (workPlace.ActiveContent is ISearch)
            {
                (workPlace.ActiveContent as ISearch).Search();
            }
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以替换则执行替换
            if (workPlace.ActiveContent is IReplace)
            {
                (workPlace.ActiveContent as IReplace).Replace();
            }
        }

        private void 撤消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以撤销则执行撤销
            if (workPlace.ActiveContent is IOperateControl)
            {
                (workPlace.ActiveContent as IOperateControl).Undo();
            }
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以重做则执行重做
            if (workPlace.ActiveContent is IOperateControl)
            {
                (workPlace.ActiveContent as IOperateControl).Redo();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以复制则执行复制
            if (workPlace.ActiveContent is IManage)
            {
                (workPlace.ActiveContent as IManage).Copy();
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以剪切则执行剪切
            if (workPlace.ActiveContent is IManage)
            {
                (workPlace.ActiveContent as IManage).Cut();
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以粘贴则执行粘贴
            if (workPlace.ActiveContent is IManage)
            {
                (workPlace.ActiveContent as IManage).Paste();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以删除则执行删除
            if (workPlace.ActiveContent is IManage)
            {
                (workPlace.ActiveContent as IManage).Delete();
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果可以全选则执行全选
            if (workPlace.ActiveContent is ISelectAll)
            {
                (workPlace.ActiveContent as ISelectAll).SelectAll();
            }
        }

        #endregion 编辑

        #region 视图

        private void 资源管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResourceExplorerWindow f = new ResourceExplorerWindow(Setting.algodooRootPath);
            f.Show(this.workPlace, DockState.DockLeft);
            //置空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        private void 脚本编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptEditorWindow se = new ScriptEditorWindow();
            se.Show(this.workPlace, DockState.Document);
            //置空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        private void 脚本管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptManagerWindow smw = new ScriptManagerWindow();
            smw.Show(this.workPlace, DockState.DockLeft);
            //置空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        private void 场景编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneEditorWindow se = new SceneEditorWindow();
            se.Show(this.workPlace, DockState.Document);
            //置空当前布局
            LayoutManager.EmptyCurrentLayout();
        }

        #endregion 视图

        #region 工具

        private void 启动AlgodooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //看是否需要重置式启动
            if (Setting.IsResetAlgodoo)
            {
                if (MBox.ShowWarningOKCancel("因为一些设置需要使Algodoo重置，请确保当前存档已经保存！\n选择\"OK\"进行重置") == DialogResult.OK)
                {
                    Process p = Process.Start(Setting.algodooRootPath + "algodoo.exe", "-reset");
                    TipsText = "正在执行重置操作，您无需进行任何操作";
                    Thread.Sleep(500);
                    if (!p.HasExited)
                    {
                        p.Kill();
                    }
                    Process.Start(Setting.algodooRootPath + "algodoo.exe");
                    Setting.IsResetAlgodoo = false;
                    TipsText += " | Algodoo已启动";
                }
            }
            else
            {
                Process.Start(Setting.algodooRootPath + "algodoo.exe");
                TipsText = "Algodoo已启动";
            }
        }

        private void 重置AlgodooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MBox.ShowWarningOKCancel("确定要进行重置吗？请确保当前存档已经保存！\n选择\"OK\"进行重置") == DialogResult.OK)
            {
                Process.Start(Setting.algodooRootPath + "algodoo.exe", "-reset");
                Setting.IsResetAlgodoo = false;
            }
        }

        private void 快速输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuickInsertDialog qid = new QuickInsertDialog();
            if (qid.ShowDialog() == DialogResult.OK)
            {
                ((IQuickInsertable)workPlace.ActiveContent).Insert(qid.Content);
            }
        }

        private void 场景分析器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion 工具

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About window = new About();
            window.ShowDialog();
        }
    }
}
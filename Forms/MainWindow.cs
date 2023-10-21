using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
 
using AlgodooStudio.Manager;
using AlgodooStudio.Forms.Style;
using AlgodooStudio.Forms.Dialogs;
using AlgodooStudio.Basic;

namespace AlgodooStudio.Forms
{
    internal partial class MainWindow : Form
    {
        /// <summary>
        /// 生成主窗体
        /// </summary>
        internal MainWindow()
        {
            InitializeComponent();
            //设定先前设定好的主题
            SetTheme(Setting.theme);
            //设定先前设定好的布局
            //SetLayout(Setting.layoutName);
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
            扩展ToolStripMenuItem.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
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
            扩展ToolStripMenuItem.ForeColor = Color.Black;
        }
        private void 帮助ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            帮助ToolStripMenuItem.ForeColor = Color.Black;
        }

        #endregion

        #region 窗口选项卡
        /// <summary>
        /// 新建窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建窗口示例
            ResourceExplorer f = new ResourceExplorer();
            f.Show(this.workPlace, DockState.Document);
            //只用来新建文本编辑器窗口
            //if (MBox.ShowWarningTips("切换主题需要关闭所有已开启窗体，请确保必要信息已保存")==DialogResult.OK)
            //{
            //    Theme t = new Theme(0, "ab");
            //    t.SetWeifenluoTheme(new VS2015BlueTheme());
            //    SetTheme(t);
            //}
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
                if (item!=this.workPlace.ActiveContent)
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
            if (tgd.ShowDialog()==DialogResult.OK)
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
            LayoutSelectDialog lsd = new LayoutSelectDialog();
            if (lsd.ShowDialog()==DialogResult.OK)
            {
                if (MBox.ShowWarningOKCancel("即将切换布局，请确保必要信息已经保存")==DialogResult.OK)
                {
                    LayoutManager.ApplyLayout(this.workPlace, lsd.ApplyLayoutName);
                }
            }
            lsd.Dispose();
            lsd = null;
        }
        /// <summary>
        /// 重置布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 重置布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MBox.ShowWarningOKCancel("确定要重置布局吗？（请确保必要信息已保存）")==DialogResult.OK)
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
        }
        #endregion
        #endregion

        #region 文件选项卡
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 扩展选项卡

        #endregion

        #region 其他内容
        /// <summary>
        /// 启动时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            //启动计时
            Program.loadTimer.Stop();
            float t =(float)Program.loadTimer.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            this.tips.Text = $"本次启动用时：{t:F1}秒";
        }
        #endregion
    }
}

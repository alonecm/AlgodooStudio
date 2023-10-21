using AlgodooStudio.Basic;
using AlgodooStudio.Forms.Dialogs;
using AlgodooStudio.Forms.Style;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using System.Threading;

namespace AlgodooStudio.Forms
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    internal partial class ResourceExplorer : DockContent
    {
        /// <summary>
        /// 搜索框文字提示
        /// </summary>
        private const string searchBoxTip = "从文件树中搜索文件(Enter)";
        /// <summary>
        /// 生成资源管理器
        /// </summary>
        internal ResourceExplorer()
        {
            InitializeComponent();
            //初始化窗体属性
            Initialize();
        }
        /// <summary>
        /// 初始化窗体属性
        /// </summary>
        private void Initialize()
        {
            //设置窗体背景色
            BackColor = Setting.theme.BackColor2;
            //设置标签文字
            TabText = Text;
            //设置边框样式
            folderTree.BorderStyle = BorderStyle.FixedSingle;
            flPanel.BorderStyle = BorderStyle.FixedSingle;
            //设置工具栏渲染器
            toolBar.Renderer = QuickToolsRenderer.GetRenderer();
            //设置部件背景色
            folderTree.BackColor = Setting.theme.BackColor3;
            flPanel.BackColor = Setting.theme.BackColor1;
            searchBox.BackColor = Setting.theme.BackColor1;
            searchBox.ForeColor = ColorTool.OffsetColor(searchBox.BackColor,80);
            //注册事件
            this.searchBox.GotFocus += SearchBox_GotFocus;
            this.searchBox.LostFocus += SearchBox_LostFocus; ;
        }
        /// <summary>
        /// 恢复搜索框文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            if (searchBox.Text =="")
            {
                searchBox.Text = searchBoxTip;
            }
        }
        /// <summary>
        /// 搜索框文字清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            if (searchBox.Text == searchBoxTip)
            {
                searchBox.Text = "";
            }
        }
        /// <summary>
        /// 当悬停状态发生改变时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceExplorer_DockStateChanged(object sender, EventArgs e)
        {
            //改变窗口分离手柄的位置
            if (DockState != DockState.Document &&
                DockState != DockState.DockTop &&
                DockState != DockState.DockBottom)
            {
                contentSpilter.Orientation = Orientation.Horizontal;
                contentSpilter.SplitterDistance = contentSpilter.Size.Height / 2;
            }
            else
            {
                contentSpilter.Orientation = Orientation.Vertical;
                contentSpilter.SplitterDistance = contentSpilter.Size.Width / 4;
            }
        }

        /// <summary>
        /// 测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //CommonOpenFileDialog ofd = new CommonOpenFileDialog();
            //ofd.Multiselect = true;
            //ofd.IsFolderPicker = true;
            //if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    Thread th = new Thread(new ParameterizedThreadStart(AddToControl));
            //    th.Start(ofd.FileNames);
            //}
            //ofd.Dispose();
            //ofd = null;
            //GC.Collect(2, GCCollectionMode.Forced);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                flPanel.Cursor = Cursors.WaitCursor;
                Thread th = new Thread(new ParameterizedThreadStart(AddToControl));
                th.Start(ofd.FileNames);
            }
            ofd.Dispose();
            ofd = null;
            GC.Collect(2, GCCollectionMode.Forced);
        }
        /// <summary>
        /// 子线程向预览窗口添加管理单元
        /// </summary>
        /// <param name="files"></param>
        private void AddToControl(object files)
        {
            List<ResourceUnit> rus = new List<ResourceUnit>();
            string[] s = (string[])files;
            //IEnumerable<string> s = (IEnumerable<string>)files;
            foreach (var item in s)
            {
                rus.Add(new ResourceUnit(item, true));
            }
            this.flPanel.BeginInvoke((Action)delegate
            {
                this.flPanel.Controls.AddRange(rus.ToArray());
                flPanel.Cursor = Cursors.Default;
            });
        }
        /// <summary>
        /// 让窗口里的所有子窗口实现双缓冲绘制
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}

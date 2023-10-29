using AlgodooStudio.ASProject.Interface;
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
    /// 主界面
    /// </summary>
    internal partial class MainWindow : Form
    {
        /// <summary>
        /// 是否保存当前布局
        /// </summary>
        private bool m_bSaveLayout = true;
        /// <summary>
        /// 反序列化悬停内容
        /// </summary>
        private DeserializeDockContent m_deserializeDockContent;
        /// <summary>
        /// 三个固定窗口
        /// </summary>
        private FileExploreWindow fileExploreWindow;
        private PropertyWindow propertyWindow;
        private ToolBoxWindow toolBoxWindow;
        /// <summary>
        /// 工具栏渲染
        /// </summary>
        private readonly ToolStripRenderer _toolStripProfessionalRenderer = new ToolStripProfessionalRenderer();
       
        public MainWindow()
        {
            InitializeComponent();
            //初始化窗口布局
            CreateStandardWindow();

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            vsToolStripExtender.DefaultRenderer = _toolStripProfessionalRenderer;
        }


        #region 主题辅助
        /// <summary>
        /// 创建标准窗口
        /// </summary>
        private void CreateStandardWindow()
        {
            fileExploreWindow = new FileExploreWindow();
            propertyWindow = new PropertyWindow();
            toolBoxWindow = new ToolBoxWindow();
        }
        /// <summary>
        /// 从记录字符串中获取停靠内容
        /// </summary>
        /// <param name="persistString">记录字符串也就是所谓的类型名+文件名</param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FileExploreWindow).ToString())
                return fileExploreWindow;
            else if (persistString == typeof(PropertyWindow).ToString())
                return propertyWindow;
            else if (persistString == typeof(ToolBoxWindow).ToString())
                return toolBoxWindow;
            else
            {
                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 2)
                    return null;
                if (parsedStrings[0] != typeof(TextEditWindow).ToString())
                    return null;
                TextEditWindow textEdit = new TextEditWindow();
                if (parsedStrings[1] != string.Empty)
                    textEdit.FilePath = parsedStrings[1];//设置文件名时会直接读取文件内容并加载
                return textEdit;
            }
        }
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSchema(object sender, EventArgs e)
        {
            // 设置主题会重启界面
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");

            dockPanel.SaveAsXml(configFile);
            CloseAllContents();
            if (sender == this.蓝色ToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015BlueTheme1;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015BlueTheme1);
            }
            else if (sender == this.白色ToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015LightTheme1;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015LightTheme1);
            }
            else if (sender == this.黑色ToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vS2015DarkTheme1;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015DarkTheme1);
            }

            白色ToolStripMenuItem.Checked = (sender == 白色ToolStripMenuItem);
            蓝色ToolStripMenuItem.Checked = (sender == 蓝色ToolStripMenuItem);
            黑色ToolStripMenuItem.Checked = (sender == 黑色ToolStripMenuItem);

            if (dockPanel.Theme.ColorPalette != null)
            {
                statusBar.BackColor = dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
                mainMenu.BackColor = dockPanel.Theme.ColorPalette.CommandBarMenuDefault.Background;
            }

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }
        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        private void CloseAllContents()
        {
            // 因为不需要创建其他窗口则值设置为Null就好
            fileExploreWindow.DockPanel = null;
            propertyWindow.DockPanel = null;
            toolBoxWindow.DockPanel = null;

            // 关闭所有文档窗口
            CloseAllDocuments();

            // 释放所有浮动窗口
            foreach (var window in dockPanel.FloatWindows.ToList())
                window.Dispose();
        }
        /// <summary>
        /// 关闭所有文档窗口
        /// </summary>
        private void CloseAllDocuments()
        {
            foreach (IDockContent document in dockPanel.DocumentsToArray())
            {
                // 释放并关闭所有面板
                document.DockHandler.DockPanel = null;
                document.DockHandler.Close();
            }
        }
        /// <summary>
        /// 启用VS渲染
        /// </summary>
        /// <param name="version"></param>
        /// <param name="theme"></param>
        private void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            vsToolStripExtender.SetStyle(mainMenu, version, theme);
            vsToolStripExtender.SetStyle(quickTools, version, theme);
            vsToolStripExtender.SetStyle(statusBar, version, theme);
        }
        #endregion


        #region 事件
        private void MainWindow_Load(object sender, EventArgs e)
        {
            SetSchema(this.蓝色ToolStripMenuItem, null);

            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }
        /// <summary>
        /// 活动窗口变动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            编辑ToolStripMenuItem.Visible = (dockPanel.ActiveContent is IEditable);
            替换ToolStripMenuItem.Visible = (dockPanel.ActiveContent is IReplaceable);
            查找ToolStripMenuItem.Visible = (dockPanel.ActiveContent is ISearchable);
            toolStripSeparator5.Visible = (dockPanel.ActiveContent is IEditable) && ((dockPanel.ActiveContent is IReplaceable) || (dockPanel.ActiveContent is ISearchable));
            toolStripSeparator4.Visible = toolStripSeparator6.Visible = (dockPanel.ActiveContent is IEditable);
        }

        #region 文件
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
                        TextEditWindow se = new TextEditWindow();
                        se.FilePath = item;
                        se.Show(this.dockPanel, DockState.Document);
                    }
                }
            }
        }
        private void 文本文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 场景ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void 全部保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //对当前获得焦点的页面执行保存功能（如果有保存功能）
            foreach (var item in dockPanel.Contents)
            {
                //是可以保存的窗口则保存
                if (item is ISaveable)
                {
                    (item as ISaveable).Save();
                }
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存当前激活的且可以保存的窗口
            if (dockPanel.ActiveContent is ISaveable)
            {
                (dockPanel.ActiveContent as ISaveable).SaveAs();
            }
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存当前激活的且可以保存的窗口
            if (dockPanel.ActiveContent is ISaveable)
            {
                (dockPanel.ActiveContent as ISaveable).Save();
            }
        }
        #endregion
        #region 编辑
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is ISearchable)
            {
                (dockPanel.ActiveContent as ISearchable).Search();
            }
        }
        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IReplaceable)
            {
                (dockPanel.ActiveContent as IReplaceable).Replace();
            }
        }
        private void 撤消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Undo();
            }
        }
        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Redo();
            }
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Copy();
            }
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Cut();
            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Paste();
            }
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).Delete();
            }
        }
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveContent is IEditable)
            {
                (dockPanel.ActiveContent as IEditable).SelectAll();
            }
        }

        #endregion
        #region 视图
        private void 文本编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditWindow textEdit = new TextEditWindow();
            textEdit.Show(this.dockPanel, DockState.Document);
        }
        private void 场景视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 文件浏览器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileExploreWindow.Show(this.dockPanel, DockState.DockLeft);
        }
        private void 工具箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 属性窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 自启动管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region 工具
        private void 启动AlgodooToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 重置AlgodooToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 取色器ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 插件管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region 窗口
        private void 新建窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 浮动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果激活的窗体不是空的则可以启用
            if (this.dockPanel.ActiveContent != null)
            {
                this.dockPanel.ActiveContent.DockHandler.IsFloat = true;
            }
            else
            {
                浮动ToolStripMenuItem.Enabled = false;
            }
        }
        private void 全部浮动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContentCollection contents = this.dockPanel.Contents;
            foreach (var item in contents)
            {
                item.DockHandler.IsFloat = true;
            }
        }
        private void 关闭所有窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllContents();
        }
        #endregion
        #region 插件
        #endregion
        #region 关于
        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About window = new About();
            window.ShowDialog();
        }
        #endregion
        #endregion
    }
}

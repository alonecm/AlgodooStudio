using AlgodooStudio.ASProject.Dialogs;
using Dex.Common;
using Dex.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Zero.Core.FileAndDirectory.DirectoryTools;
using Zero.Core.FileAndDirectory.FileTools;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    internal partial class FileExploreWindow : DockContent
    {
        /// <summary>
        /// 搜索框文字提示
        /// </summary>
        private const string searchBoxTip = "从当前文件夹中搜索文件(Enter)";

        /// <summary>
        /// 鼠标是否点击了
        /// </summary>
        private bool isMouseDown = false;

        /// <summary>
        /// 当前的目录路径
        /// </summary>
        private string directoryPath = "";

        /// <summary>
        /// 用来展示的文件扩展名
        /// </summary>
        private string displayExtension = "*.png|*.cfg|*.thm|*.phz|*.phn";

        /// <summary>
        /// Phun存档缓冲，用于控制输出与写入情况
        /// </summary>
        private Dictionary<string, PhunSharp.Archive.ArchiveZip> saveCache = new Dictionary<string, PhunSharp.Archive.ArchiveZip>();

        /// <summary>
        /// 生成资源管理器
        /// </summary>
        internal FileExploreWindow()
        {
            InitializeComponent();
            //初始化窗体属性
            Initialize();
        }

        /// <summary>
        /// 生成资源管理器并定位到指定路径的目录下
        /// </summary>
        /// <param name="path">需要定位的路径</param>
        internal FileExploreWindow(string path)
        {
            InitializeComponent();
            //初始化窗体属性
            Initialize(path);
        }

        /// <summary>
        /// 初始化窗体属性
        /// </summary>
        private void Initialize()
        {
            //设置窗体背景色
            //BackColor = Setting.theme.BackColor2;
            //设置标签文字
            TabText = Text;
            //设置边框样式
            folderTree.BorderStyle = BorderStyle.FixedSingle;
            fViewer.BorderStyle = BorderStyle.FixedSingle;
            ////设置工具栏渲染器
            //toolBar.Renderer = QuickToolsRenderer.GetRenderer();
            ////右键菜单渲染器
            //treeContextMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            //fViewerContextMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            ////设置部件背景色
            //folderTree.BackColor = Setting.theme.BackColor3;
            //fViewer.BackColor = Setting.theme.BackColor1;
            //fViewer.ForeColor = Setting.theme.VarNameColor;
            //searchBox.BackColor = Setting.theme.BackColor1;
            //searchBox.ForeColor = ColorTools.OffsetColor(Setting.theme.BackColor1, 80);
            //folderTree.LineColor = Setting.theme.VarNameColor;
            //folderTree.ForeColor = Setting.theme.VarNameColor;
            //注册事件
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.LostFocus += SearchBox_LostFocus;
            //启动生成驱动器树
            GetDriveTree();
            //直接定位到设置的资源树目录
            ArriveFolder(Program.Setting.AlgodooPath);
            //载入文件夹的文件信息到旁侧列表中
            DisplayFilesAndFolders(Program.Setting.AlgodooPath, "*", displayExtension);
        }

        /// <summary>
        /// 初始化窗体属性
        /// </summary>
        private void Initialize(string path)
        {
            //设置窗体背景色
            //BackColor = Setting.theme.BackColor2;
            //设置标签文字
            TabText = Text;
            //设置边框样式
            folderTree.BorderStyle = BorderStyle.FixedSingle;
            fViewer.BorderStyle = BorderStyle.FixedSingle;
            ////设置工具栏渲染器
            //toolBar.Renderer = QuickToolsRenderer.GetRenderer();
            ////右键菜单渲染器
            //treeContextMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            //fViewerContextMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
            ////设置部件背景色
            //folderTree.BackColor = Setting.theme.BackColor3;
            //fViewer.BackColor = Setting.theme.BackColor1;
            //fViewer.ForeColor = Setting.theme.VarNameColor;
            //searchBox.BackColor = Setting.theme.BackColor1;
            //searchBox.ForeColor = ColorTools.OffsetColor(Setting.theme.BackColor1, 80);
            //folderTree.LineColor = Setting.theme.VarNameColor;
            //folderTree.ForeColor = Setting.theme.VarNameColor;
            //注册事件
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.LostFocus += SearchBox_LostFocus;
            //启动生成驱动器树
            GetDriveTree();
            string np = path;
            //存在则使用目标文件的所在目录
            if (File.Exists(path))
            {
                np = Path.GetDirectoryName(path) + "\\";
            }
            //定位到设置的目录
            ArriveFolder(np);
            //载入文件夹的文件信息到旁侧列表中
            DisplayFilesAndFolders(np, "*", displayExtension);
        }

        /// <summary>
        /// 内存管理，防止因存档打开的过多在内存中造成堆积占用大量内存
        /// </summary>
        /// <param name="cleanNumber">清理的数量(此值只能小于等于检查值)</param>
        /// <param name="checkNumber">检查的数量</param>
        private void MemoryManage(byte cleanNumber, byte checkNumber)
        {
            byte clean = cleanNumber;
            //如果清理值大于检查值则按检查值清理
            if (cleanNumber > checkNumber)
            {
                cleanNumber = checkNumber;
            }
            if (saveCache.Count >= checkNumber)
            {
                Container<string> ps = new Container<string>();
                byte count = 0;
                //记录前n个值的地址
                foreach (var item in saveCache.Keys)
                {
                    if (count < cleanNumber)
                    {
                        ps.Add(item);
                    }
                    else
                    {
                        break;
                    }
                    count++;
                }
                //删除内容
                foreach (var item in ps)
                {
                    saveCache.Remove(item);
                }
                GC.Collect(2);
            }
        }

        /// <summary>
        /// 打开指定路径的文件或文件夹
        /// </summary>
        /// <param name="path"></param>
        private void Open(string path)
        {
            //如果是文件夹就打开文件夹并更改树的节点
            if (Directory.Exists(path))
            {
                ArriveFolder(path + "\\");
                DisplayFilesAndFolders(path, "*", displayExtension);
            }
            else if (File.Exists(path))
            {
                try
                {
                    TextEditWindow se = new TextEditWindow();
                    se.FilePath = path;
                    se.Show(this.DockPanel, DockState.Document);
                }
                catch (Exception e)
                {
                    MBox.ShowError(e.Message);
                }
            }
            else
            {
                DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                //MBox.ShowError("打开失败，请重试");
            }
        }

        /// <summary>
        /// 在指定路径内创建一个文件夹
        /// </summary>
        /// <param name="path">指的路径</param>
        private DirectoryInfo CreateFolder(string path)
        {
            DirectoryManager dm = new DirectoryManager(path);
            TextGetDialog tgd = new TextGetDialog();
            tgd.Title = "新建文件夹";
            if (tgd.ShowDialog() == DialogResult.OK)
            {
                if (tgd.InputText != "")
                {
                    try
                    {
                        dm.CreateChildFolder(tgd.InputText);
                        return new DirectoryInfo(path + "\\" + tgd.InputText);
                    }
                    catch (Exception e)
                    {
                        MBox.ShowError(e.Message);
                        return null;
                    }
                }
                else
                {
                    dm.CreateChildFolder("新建文件夹");
                    return new DirectoryInfo(path + "\\新建文件夹");
                }
            }
            else
            {
                tgd.Dispose();
                tgd = null;
                return null;
            }
        }

        /// <summary>
        /// 删除指定路径的文件夹
        /// </summary>
        /// <param name="path">需要删除的文件夹路径</param>
        /// <returns>被删文件夹的父文件夹</returns>
        private DirectoryInfo DeleteFolder(string path)
        {
            DirectoryManager dm = new DirectoryManager(path);
            if (MBox.ShowWarningOKCancel("是否要删除" + Path.GetFileName(path)) == DialogResult.OK)
            {
                return new DirectoryInfo(dm.DeleteThisFolderSafety()).Parent;
            }
            else
            {
                return new DirectoryInfo(path);
            }
        }

        /// <summary>
        /// 剪切指定路径的文件夹
        /// </summary>
        /// <param name="path">需要剪切的文件夹路径</param>
        /// <returns>剪下来的文件夹信息</returns>
        private DirectoryInfo CutFolder(string path)
        {
            folderTree.CollapseAll();
            Clipboard.SetText(path);
            Program.Setting.IsCutting = true;
            Program.Setting.IsCopyFinished = false;
            粘贴ToolStripMenuItem.Enabled = true;
            粘贴ToolStripMenuItem1.Enabled = true;
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// 复制指定路径的文件夹
        /// </summary>
        /// <param name="path">需要剪切的文件夹路径</param>
        /// <returns>剪下来的文件夹信息</returns>
        private DirectoryInfo CopyFolder(string path)
        {
            Clipboard.SetText(path);
            Program.Setting.IsCutting = false;
            Program.Setting.IsCopyFinished = false;
            粘贴ToolStripMenuItem.Enabled = true;
            粘贴ToolStripMenuItem1.Enabled = true;
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// 粘贴文件夹到指定的路径下
        /// </summary>
        /// <param name="targetPath">指定的文件路径</param>
        private void PasteFolder(string targetPath)
        {
            DirectoryManager dm = new DirectoryManager(targetPath + "\\");
            //保存数据
            string sourcePath = Clipboard.GetText();//提取源路径
            if (IsSourceFolder(targetPath, sourcePath))
            {
                MBox.ShowInfo("目标文件夹是源文件夹，粘贴终止");
                粘贴ToolStripMenuItem.Enabled = false;
                粘贴ToolStripMenuItem1.Enabled = false;
                //清空剪贴板
                Clipboard.Clear();
                Program.Setting.IsCutting = false;
                Program.Setting.IsCopyFinished = true;
                return;
            }
            //如果是剪切
            if (!Program.Setting.IsCopyFinished)
            {
                //清空剪贴板
                Clipboard.Clear();
                //移动文件夹到指定位置
                dm.MoveTo(new DirectoryInfo(sourcePath));
                粘贴ToolStripMenuItem.Enabled = false;
                粘贴ToolStripMenuItem1.Enabled = false;
                Program.Setting.IsCutting = false;
                Program.Setting.IsCopyFinished = true;
            }
            else
            {
                //复制文件夹到指定位置
                dm.CopyFolderToChild(new DirectoryInfo(sourcePath));
            }
        }

        /// <summary>
        /// 文件夹是否未源文件夹
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        private bool IsSourceFolder(string targetPath, string sourcePath)
        {
            string[] tp = targetPath.Split('\\');
            string tmp = sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1, sourcePath.Length - sourcePath.LastIndexOf('\\') - 1);
            foreach (var item in tp)
            {
                if (item == tmp)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 重命名指定路径的文件夹
        /// </summary>
        /// <param name="path">指定路径</param>
        /// <returns>新的文件夹路径</returns>
        private DirectoryInfo RenameFolder(string path)
        {
            DirectoryManager dm = new DirectoryManager(path);
            TextGetDialog tgd = new TextGetDialog();
            tgd.Title = "重命名文件夹";
            if (tgd.ShowDialog() == DialogResult.OK)
            {
                if (tgd.InputText != "")
                {
                    try
                    {
                        return new DirectoryInfo(dm.RenameThisFolder(tgd.InputText));
                    }
                    catch (Exception e)
                    {
                        MBox.ShowError(e.Message);
                        return new DirectoryInfo(path);
                    }
                }
                else
                {
                    MBox.ShowInfo("重命名已取消");
                    return new DirectoryInfo(path);
                }
            }
            else
            {
                tgd.Dispose();
                tgd = null;
                return new DirectoryInfo(path);
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

        #region 文件树

        /// <summary>
        /// 获取驱动器树
        /// </summary>
        private void GetDriveTree()
        {
            folderTree.Nodes.Clear();
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            foreach (var item in driveInfo)
            {
                TreeNode node = new TreeNode();
                node.Text = item.Name;
                node.Tag = item.Name;
                node.ImageIndex = 3;
                node.SelectedImageIndex = 2;
                node.Nodes.Add("");
                folderTree.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 获取指定节点下的所有文件夹节点
        /// </summary>
        /// <param name="node"></param>
        private void GetPathTree(TreeNode node)
        {
            //先清空节点
            node.Nodes.Clear();
            if (Directory.Exists(node.Tag.ToString()))
            {
                //查找当前目录下的所有子文件夹
                DirectoryInfo[] cds = new DirectorySearcher(node.Tag.ToString()).GetDirectoriesFromChildLevel();
                foreach (var item in cds)
                {
                    //跳过这两个系统文件夹
                    if (item.Name == "System Volume Information" || item.Name.ToUpper() == "$RECYCLE.BIN")
                    {
                        continue;
                    }
                    TreeNode c = new TreeNode(item.Name);
                    c.Tag = item.FullName;
                    c.Nodes.Add("");
                    node.Nodes.Add(c);
                }
            }
            else
            {
                //如果这个节点对应的文件夹已经被删除了
                DisplayFilesAndFolders(directoryPath[0] + ":\\", "*", displayExtension);
                ArriveFolder(directoryPath[0] + ":\\");
            }
        }

        /// <summary>
        /// 抵达指定目录
        /// </summary>
        /// <param name="path">文件夹路径</param>
        private void ArriveFolder(string path)
        {
            folderTree.BeginUpdate();
            //然后根据路径依次找到指定位置
            string[] dirs = path.Split('\\');
            foreach (TreeNode item in folderTree.Nodes)
            {
                if (dirs[0] == item.Text.Substring(0, item.Text.IndexOf('\\')))
                {
                    GetPathTree(item);
                    item.Expand();
                    ArriveFolder(dirs, item, 1);
                    break;
                }
            }
            folderTree.EndUpdate();
        }

        private void ArriveFolder(string[] dirs, TreeNode node, int i)
        {
            foreach (TreeNode v in node.Nodes)
            {
                if (v.Text == dirs[i])
                {
                    i++;
                    if (i == dirs.Length)
                    {
                        break;
                    }
                    else
                    {
                        GetPathTree(v);
                        v.Expand();
                        ArriveFolder(dirs, v, i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 展开节点前检查展开是否是通过鼠标展开的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isMouseDown)
            {
                GetPathTree(e.Node);
            }
        }

        /// <summary>
        /// 合并时清理合并的节点并添加空节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add("");
        }

        /// <summary>
        /// 鼠标是否按下了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = e.Button == MouseButtons.Left;
        }

        /// <summary>
        /// 更改选定内容时更改右侧显示内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            DisplayFilesAndFolders(e.Node.Tag.ToString(), "*", displayExtension);
            if (Regex.IsMatch(e.Node.Text, @"[A-Z]:\\"))
            {
                剪切ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
                复制ToolStripMenuItem.Enabled = false;
            }
            else
            {
                剪切ToolStripMenuItem.Enabled = true;
                复制ToolStripMenuItem.Enabled = true;
                删除ToolStripMenuItem.Enabled = true;
            }
        }

        #region 文件树右键菜单

        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo newDir = CreateFolder(folderTree.SelectedNode.Tag.ToString());
            if (newDir != null)
            {
                ArriveFolder(newDir.FullName);
                DisplayFilesAndFolders(newDir.FullName, "*", displayExtension);
            }
            else
            {
                ArriveFolder(folderTree.SelectedNode.Tag.ToString());
                DisplayFilesAndFolders(folderTree.SelectedNode.Tag.ToString(), "*", displayExtension);
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutFolder(folderTree.SelectedNode.Tag.ToString());
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyFolder(folderTree.SelectedNode.Tag.ToString());
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = folderTree.SelectedNode.Tag.ToString();
            PasteFolder(path);
            ArriveFolder(path);
            DisplayFilesAndFolders(path, "*", displayExtension);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo pdir = DeleteFolder(folderTree.SelectedNode.Tag.ToString());
            ArriveFolder(pdir.FullName + "\\");
            DisplayFilesAndFolders(pdir.FullName, "*", displayExtension);
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = RenameFolder(folderTree.SelectedNode.Tag.ToString());
            ArriveFolder(dir.FullName);
            DisplayFilesAndFolders(dir.Parent.FullName, "*", displayExtension);
        }

        private void 复制完整路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(folderTree.SelectedNode.Tag.ToString());
        }

        private void 在资源管理器中打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Explorer.exe", folderTree.SelectedNode.Tag.ToString());
        }

        /// <summary>
        /// 菜单出现时动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeContextMenu_VisibleChanged(object sender, EventArgs e)
        {
            //检查是否可以粘贴
            if (Clipboard.ContainsText())
            {
                //如果是文件路径就启用粘贴功能
                if (Directory.Exists(Clipboard.GetText()))
                {
                    粘贴ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    粘贴ToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                粘贴ToolStripMenuItem.Enabled = false;
            }
        }

        #endregion 文件树右键菜单

        #endregion 文件树

        #region 工具栏

        /// <summary>
        /// 刷新文件树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshTree_Click(object sender, EventArgs e)
        {
            //记录已展开的所有节点
            List<TreeNode> openedNode = new List<TreeNode>();
            foreach (TreeNode item in folderTree.Nodes)
            {
                if (item.IsExpanded)
                {
                    openedNode.Add(item);
                }
            }
            //收起并重新展开
            folderTree.CollapseAll();
            foreach (TreeNode item in folderTree.Nodes)
            {
                foreach (TreeNode o in openedNode)
                {
                    if (item == o)
                    {
                        ArriveFolder(item.FullPath);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 定位到Algodoo根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void algodooPosition_Click(object sender, EventArgs e)
        {
            ArriveFolder(Program.Setting.AlgodooPath);
            DisplayFilesAndFolders(Program.Setting.AlgodooPath, "*", displayExtension);
        }

        /// <summary>
        /// 定位到工作室根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void studioPosition_Click(object sender, EventArgs e)
        {
            ArriveFolder(Program.Setting.StudioPath);
            DisplayFilesAndFolders(Program.Setting.StudioPath, "*", displayExtension);
        }

        /// <summary>
        /// 全部折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void collapse_Click(object sender, EventArgs e)
        {
            folderTree.CollapseAll();
        }

        /// <summary>
        /// 返回上级目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnUp_Click(object sender, EventArgs e)
        {
            //确保文件夹路径不是空的并且不是磁盘根目录
            if (directoryPath != "" && directoryPath.Length > 3)
            {
                directoryPath = new DirectoryInfo(directoryPath).Parent.FullName;
                DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                ArriveFolder(directoryPath);
            }
            else
            {
                directoryPath = "";
                DisplayFilesAndFolders(directoryPath, "*", displayExtension);
            }
        }

        #endregion 工具栏

        #region 搜索框

        /// <summary>
        /// 搜索框点击时全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_Click(object sender, EventArgs e)
        {
            searchBox.SelectAll();
        }

        /// <summary>
        /// 恢复搜索框文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
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

        #endregion 搜索框

        #region 文件展示区

        /// <summary>
        /// 展示文件夹中的文件和子目录
        /// </summary>
        /// <param name="fileName">需要展示的文件名</param>
        private void DisplayFilesAndFolders(string path, string dirName, string fileName)
        {
            fViewer.Items.Clear();
            if (path != "")
            {
                //如果这个路径的文件夹还存在就加载内容，否则
                if (Directory.Exists(path))
                {
                    directoryPath = path;
                    fViewer.BeginUpdate();
                    //添加文件夹
                    DirectoryInfo[] dirs = new DirectorySearcher(path).GetDirectoriesFromChildLevel(dirName);
                    foreach (var item in dirs)
                    {
                        //跳过这两个系统文件夹
                        if (item.Name == "System Volume Information" || item.Name.ToUpper() == "$RECYCLE.BIN")
                        {
                            continue;
                        }
                        ListViewItem l = new ListViewItem();
                        l.Tag = item.FullName;
                        l.Text = item.Name;
                        l.ImageIndex = 0;
                        fViewer.Items.Add(l);
                    }
                    FileSearcher fs = new FileSearcher(path);
                    FileInfo[] files = fs.GetFilesFromThisFolder(fileName, SearchOption.TopDirectoryOnly);
                    //删除ImageList中的exe文件的图标，释放ilstIcons的空间
                    foreach (ListViewItem item in fViewer.Items)
                    {
                        if (item.Text.EndsWith(".exe"))
                        {
                            imageListForfViewer_B.Images.RemoveByKey(item.Text);
                            imageListForfViewer_S.Images.RemoveByKey(item.Text);
                        }
                    }
                    try
                    {
                        foreach (var item in files)
                        {
                            ListViewItem l = new ListViewItem();
                            //如果为exe文件和空文件则按名称替换图标
                            if (item.Extension == ".exe" || item.Extension == "")
                            {
                                Icon icon = Icon.ExtractAssociatedIcon(item.FullName);
                                imageListForfViewer_B.Images.Add(item.Name, icon);
                                imageListForfViewer_S.Images.Add(item.Name, icon);
                                l.ImageKey = item.Name;
                            }
                            else
                            {
                                if (!imageListForfViewer_B.Images.ContainsKey(item.Extension))
                                {
                                    Icon icon;
                                    if (item.Extension == ".url" || item.Extension == ".lnk")
                                    {
                                        icon = IconTools.GetIconByFileType(item.Extension, true);
                                    }
                                    else
                                    {
                                        icon = IconTools.GetFileIcon(item.FullName, false);
                                    }
                                    //因为类型（除了exe）相同的文件，图标相同，所以可以按拓展名存取图标
                                    imageListForfViewer_B.Images.Add(item.Extension, icon);
                                    imageListForfViewer_S.Images.Add(item.Extension, icon);
                                }
                                l.ImageKey = item.Extension;
                            }
                            l.Tag = item.FullName;
                            l.Text = item.Name;
                            fViewer.Items.Add(l);
                        }
                    }
                    catch (Exception e)
                    {
                        MBox.ShowError(e.Message);
                    }
                }
                else
                {
                    MBox.ShowError("该文件夹不存在或已删除，请刷新再试");
                }
            }
            else
            {
                //驱动器目录
                DriveInfo[] driveInfos = DriveInfo.GetDrives();
                foreach (var item in driveInfos)
                {
                    ListViewItem l = new ListViewItem();
                    if (!imageListForfViewer_B.Images.ContainsKey(item.Name))
                    {
                        Icon icon = IconTools.GetDriverIcon(item.Name[0], false);
                        imageListForfViewer_B.Images.Add(item.Name, icon);
                        imageListForfViewer_S.Images.Add(item.Name, icon);
                    }
                    l.ImageKey = item.Name;
                    l.Tag = item.Name;
                    l.Text = item.VolumeLabel + $"({item.Name[0]})";
                    fViewer.Items.Add(l);
                }
            }
            fViewer.EndUpdate();
        }

        /// <summary>
        /// 大图预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bigView_Click(object sender, EventArgs e)
        {
            fViewer.View = View.Tile;
        }

        /// <summary>
        /// 列表预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Click(object sender, EventArgs e)
        {
            fViewer.View = View.List;
        }

        /// <summary>
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fViewer_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                //在高速刷新的过程中需要确保打开不受影响。。。
                Open(fViewer.SelectedItems[0].Tag.ToString());
            }
            catch (Exception b)
            {
                MBox.ShowInfo(b.Message);
            }
        }

        #region 文件展示区右键菜单

        /// <summary>
        /// 重置所有菜单项
        /// </summary>
        private void ResetAllItems()
        {
            解析phn文件ToolStripMenuItem.Visible =
            解析phz文件ToolStripMenuItem.Visible =
            刷新ToolStripMenuItem.Visible =
                查看ToolStripMenuItem.Visible =
                新建ToolStripMenuItem1.Visible =
                打开ToolStripMenuItem.Visible =
                剪切ToolStripMenuItem1.Visible =
                复制ToolStripMenuItem1.Visible =
                粘贴ToolStripMenuItem1.Visible =
                删除ToolStripMenuItem1.Visible =
                重命名ToolStripMenuItem1.Visible =
                复制完整路径ToolStripMenuItem1.Visible =
                在新管理器中打开ToolStripMenuItem1.Visible =
                在资源管理器中打开ToolStripMenuItem1.Visible = false;
        }

        private void fViewerContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ResetAllItems();
            //将获取的鼠标当前坐标进行转换（屏幕坐标转换成工作区坐标）
            Point curPoint = fViewer.PointToClient(Cursor.Position);
            ListViewItem item = fViewer.GetItemAt(curPoint.X, curPoint.Y);
            if (item != null)
            {
                if (Directory.Exists(item.Tag.ToString()))
                {
                    打开ToolStripMenuItem.Visible = true;
                    在新管理器中打开ToolStripMenuItem1.Visible = true;
                    在资源管理器中打开ToolStripMenuItem1.Visible = true;
                    复制完整路径ToolStripMenuItem1.Visible = true;
                    复制ToolStripMenuItem1.Visible = true;
                    剪切ToolStripMenuItem1.Visible = true;
                    删除ToolStripMenuItem1.Visible = true;
                    重命名ToolStripMenuItem1.Visible = true;
                }
                else if (Path.GetExtension(item.Tag.ToString()) == ".phz")
                {
                    //如果这个文件是.phz文件则特殊对待
                    打开ToolStripMenuItem.Visible = true;
                    复制ToolStripMenuItem1.Visible = true;
                    剪切ToolStripMenuItem1.Visible = true;
                    删除ToolStripMenuItem1.Visible = true;
                    重命名ToolStripMenuItem1.Visible = true;
                    复制完整路径ToolStripMenuItem1.Visible = true;
                    解析phz文件ToolStripMenuItem.Visible = true;
                }
                else if (Path.GetExtension(item.Tag.ToString()) == ".phn")
                {
                    //如果这个文件是.phn文件则特殊对待
                    打开ToolStripMenuItem.Visible = true;
                    复制ToolStripMenuItem1.Visible = true;
                    剪切ToolStripMenuItem1.Visible = true;
                    删除ToolStripMenuItem1.Visible = true;
                    重命名ToolStripMenuItem1.Visible = true;
                    复制完整路径ToolStripMenuItem1.Visible = true;
                    解析phn文件ToolStripMenuItem.Visible = true;
                }
                else
                {
                    //如果这个文件是.phz文件则特殊对待
                    打开ToolStripMenuItem.Visible = true;
                    复制ToolStripMenuItem1.Visible = true;
                    剪切ToolStripMenuItem1.Visible = true;
                    删除ToolStripMenuItem1.Visible = true;
                    重命名ToolStripMenuItem1.Visible = true;
                    复制完整路径ToolStripMenuItem1.Visible = true;
                }
            }
            else
            {
                刷新ToolStripMenuItem.Visible = true;
                新建ToolStripMenuItem1.Visible = true;
                查看ToolStripMenuItem.Visible = true;
                粘贴ToolStripMenuItem1.Visible = true;
                if (!Program.Setting.IsCopyFinished)
                {
                    粘贴ToolStripMenuItem1.Enabled = true;
                }
                else
                {
                    粘贴ToolStripMenuItem1.Enabled = false;
                }
                复制完整路径ToolStripMenuItem1.Visible = true;
                在资源管理器中打开ToolStripMenuItem1.Visible = true;
            }
        }

        private void 列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fViewer.View = View.List;
        }

        private void 平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fViewer.View = View.Tile;
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayFilesAndFolders(directoryPath, "*", displayExtension);
        }

        private void 空文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolder(directoryPath);
            DisplayFilesAndFolders(directoryPath, "*", displayExtension);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open(fViewer.SelectedItems[0].Tag.ToString());
        }

        private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = fViewer.SelectedItems[0].Tag.ToString();
            if (Directory.Exists(path))
            {
                CutFolder(path);
            }
            else
            {
                //只要剪切就要收起来以防万一
                folderTree.CollapseAll();
                Clipboard.SetText(path);
                //启用剪切
                Program.Setting.IsCutting = true;
                //启用复制功能
                Program.Setting.IsCopyFinished = false;
                粘贴ToolStripMenuItem1.Enabled = true;
                粘贴ToolStripMenuItem.Enabled = true;
            }
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = fViewer.SelectedItems[0].Tag.ToString();
            if (Directory.Exists(path))
            {
                CopyFolder(path);
            }
            else
            {
                Clipboard.SetText(path);
                //启用剪切
                Program.Setting.IsCutting = false;
                //启用复制功能
                Program.Setting.IsCopyFinished = false;
                粘贴ToolStripMenuItem1.Enabled = true;
                粘贴ToolStripMenuItem.Enabled = true;
            }
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //如果全局剪贴操作尚未完成则进行剪贴
            if (!Program.Setting.IsCopyFinished)
            {
                string path = Clipboard.GetText();
                //检查粘贴的是文件还是文件夹
                if (Directory.Exists(path))
                {
                    PasteFolder(directoryPath);
                    ArriveFolder(directoryPath);
                    DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                }
                else
                {
                    if (File.Exists(path))
                    {
                        //如果是剪切
                        if (Program.Setting.IsCutting)
                        {
                            FileManager.MoveFileTo(new FileInfo(path), directoryPath, Path.GetFileNameWithoutExtension(path) + " - 副本" + Path.GetExtension(path));
                            Program.Setting.IsCutting = false;
                            Program.Setting.IsCopyFinished = true;
                            粘贴ToolStripMenuItem1.Enabled = false;
                            DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                        }
                        else
                        {
                            FileManager.CopyFileTo(new FileInfo(path), new DirectoryInfo(directoryPath), Path.GetFileNameWithoutExtension(path) + " - 副本" + Path.GetExtension(path));
                            DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                        }
                    }
                }
            }
            else
            {
                粘贴ToolStripMenuItem1.Enabled = false;
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = fViewer.SelectedItems[0].Tag.ToString();
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = DeleteFolder(path);
                if (dir.FullName != path)
                {
                    ArriveFolder(dir.Parent.FullName + "\\");
                    DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                }
            }
            else
            {
                if (MBox.ShowWarningOKCancel("是否要删除" + Path.GetFileName(path)) == DialogResult.OK)
                {
                    FileManager.DeleteFileSafety(new FileInfo(path));
                    DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                }
            }
        }

        private void 重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = fViewer.SelectedItems[0].Tag.ToString();
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = RenameFolder(path);
                ArriveFolder(dir.FullName);
                DisplayFilesAndFolders(directoryPath, "*", displayExtension);
            }
            else
            {
                TextGetDialog tgd = new TextGetDialog();
                tgd.Title = "重命名文件";
                tgd.IsNameValidCheck = true;
                tgd.IsExtraMode = true;
                tgd.InputText = Path.GetFileNameWithoutExtension(path);
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    if (tgd.IsGetAll)
                    {
                        string name = Path.GetFileName(tgd.InputText);
                        string extension = Path.GetExtension(tgd.InputText);
                        FileManager.RenameFile(new FileInfo(path), name, extension);
                        DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                    }
                    else
                    {
                        FileManager.RenameFile(new FileInfo(path), tgd.InputText);
                        DisplayFilesAndFolders(directoryPath, "*", displayExtension);
                    }
                }
                tgd.Dispose();
                tgd = null;
            }
        }

        private void 复制完整路径ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //将获取的鼠标当前坐标进行转换（屏幕坐标转换成工作区坐标）
            ListView.SelectedListViewItemCollection item = fViewer.SelectedItems;
            if (item.Count > 0)
            {
                Clipboard.SetText(fViewer.SelectedItems[0].Tag.ToString());
            }
            else
            {
                Clipboard.SetText(directoryPath);
            }
        }

        private void 在新管理器中打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileExploreWindow r = new FileExploreWindow();
            r.directoryPath = fViewer.SelectedItems[0].Tag.ToString();
            r.ArriveFolder(r.directoryPath + "\\");
            r.DisplayFilesAndFolders(r.directoryPath, "*", displayExtension);
            r.Show(this.DockPanel, DockState.Float);
        }

        private void 在资源管理器中打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //将获取的鼠标当前坐标进行转换（屏幕坐标转换成工作区坐标）
            ListView.SelectedListViewItemCollection item = fViewer.SelectedItems;
            if (item.Count > 0)
            {
                Process.Start("Explorer.exe", item[0].Tag.ToString());
            }
            else
            {
                Process.Start("Explorer.exe", directoryPath);
            }
        }

        private void 解析phz文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 解析phn文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion 文件展示区右键菜单

        /// <summary>
        /// 选中项后弹出提示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fViewer.SelectedItems.Count > 0)
            {
                //将获取的鼠标当前坐标进行转换（屏幕坐标转换成工作区坐标）
                Point curPoint = fViewer.PointToClient(Cursor.Position) + new Size(20, 20);
                string path = fViewer.SelectedItems[0].Tag.ToString();
                if (!Directory.Exists(path))
                {
                    fViewer.Cursor = Cursors.WaitCursor;
                    string str = "";
                    if (Path.GetExtension(path) == ".phz")
                    {
                        //优先进行内存检查
                        MemoryManage(10, 20);
                        //如果存档不存在则优先添加到其中
                        if (!saveCache.ContainsKey(path))
                        {
                            saveCache.Add(path, PhunSharp.ArchiveTools.DeCompress(path));
                        }
                        try
                        {
                            dynamic fileinfo = saveCache[path].Phn.Settings["FileInfo"];
                            str += "存档作者：" + fileinfo.author + "\n";
                        }
                        catch 
                        {
                            str += "存档作者：无\n";
                        }
                    }
                    str += "项目名称：" + fViewer.SelectedItems[0].Text +
                        "\n创建时间：" + new FileInfo(path).CreationTime.ToString();
                    fileTip.Show(str, fViewer, curPoint);
                    fViewer.Cursor = Cursors.Default;
                }
            }
            else
            {
                fileTip.Hide(fViewer);
            }
        }

        /// <summary>
        /// 鼠标离开时弹出隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fViewer_MouseLeave(object sender, EventArgs e)
        {
            fileTip.Hide(fViewer);
        }

        #endregion 文件展示区

        /// <summary>
        /// 退出时清空缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveCache.Clear();
            GC.Collect(2);
        }
    }
}
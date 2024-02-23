using AlgodooStudio.ASProject.Dialogs;
using AlgodooStudio.ASProject.Support;
using Dex.IO;
using Dex.Utils;
using Microsoft.VisualBasic.FileIO;
using PhunSharp;
using PhunSharp.Archive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    internal partial class FileExploreWindow : DockContent
    {
        /// <summary>
        /// 当前文件夹
        /// </summary>
        private string currentFolder = string.Empty;
        /// <summary>
        /// 复制中
        /// </summary>
        private bool isCopying = false;
        /// <summary>
        /// 移动中
        /// </summary>
        private bool isMoving = false;
        /// <summary>
        /// 复制中的路径
        /// </summary>
        private string copyingPath = string.Empty;
        /// <summary>
        /// 用来展示的文件扩展名
        /// </summary>
        private string extension = "*.png|*.cfg|*.thm|*.phz|*.phn";
        /// <summary>
        /// Phun存档缓冲，用于控制输出与写入情况
        /// </summary>
        private Dictionary<string, ArchiveZip> archiveCache = new Dictionary<string, ArchiveZip>();

        /// <summary>
        /// 生成资源管理器
        /// </summary>
        internal FileExploreWindow()
        {
            InitializeComponent();
            //初始化窗体属性
            Initialize();
        }


        #region 辅助性方法

        /// <summary>
        /// 启用VS渲染
        /// </summary>
        /// <param name="version"></param>
        /// <param name="theme"></param>
        public void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            vsToolStripExtender.SetStyle(toolBar, version, theme);
        }

        /// <summary>
        /// 初始化窗体属性
        /// </summary>
        private void Initialize()
        {
            //设置标签文字
            TabText = Text;
            //启动生成驱动器树
            LoadDriveForTreeView();
            //转移至Studio目录
            LoadFoldersAndFilesToListView(Program.Setting.StudioPath);
        }

        /// <summary>
        /// 内存管理，防止因存档打开的过多在内存中造成堆积占用大量内存
        /// </summary>
        /// <param name="maxContain">最大容量</param>
        private void MemoryManage(byte maxContain)
        {
            //如果缓存数量大于检查值则清理
            if (archiveCache.Count > maxContain)
            {
                //获取并移除第一个
                var first = archiveCache.First();
                archiveCache.Remove(first.Key);
                GC.Collect(2, GCCollectionMode.Optimized, false, false);
            }
        }

        /// <summary>
        /// 加载驱动器到树表中
        /// </summary>
        private void LoadDriveForTreeView()
        {
            folderTree.Nodes.Clear();//清空树
            DriveInfo[] driveInfos = DriveInfo.GetDrives();//获取驱动器信息
            foreach (var driveinfo in driveInfos)
            {
                //创建驱动器节点并利用空节点创建加号
                folderTree.Nodes.Add(new TreeNode(driveinfo.Name, 3, 3, new TreeNode[1] { new TreeNode("") }) { Tag = driveinfo });
            }
        }

        /// <summary>
        /// 沿路径展开
        /// </summary>
        /// <param name="path"></param>
        private void ExpandToPathForTreeView(string path)
        {
            string[] pathNodes = path.Split(Path.DirectorySeparatorChar);//拆分路径
            //准备递归
            foreach (TreeNode node in folderTree.Nodes)
            {
                if (node.Text.Contains(pathNodes[0]))
                {
                    ExpandToPathForTreeView(node, pathNodes, 1);
                    return;
                }
            }
        }

        /// <summary>
        /// 展示指定路径下的文件夹和文件
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        private void LoadFoldersAndFilesToListView(string dirPath)
        {
            if (dirPath!="")
            {
                //保留当前列表
                currentFolder = dirPath;
                //清空列表
                fViewer.Clear();
                var fsm = new FileSystemManager(new DirectoryInfo(dirPath));
                //加载子文件夹
                var dirs = fsm.GetChildDirectories(FileAttributes.System|FileAttributes.Hidden);
                foreach (var dir in dirs)
                {
                    fViewer.Items.Add(new ListViewItem(dir.Name) { Tag = dir, ImageIndex = 0 });
                }
                //筛选格式
                var patterns = extension.Split('|');
                foreach (var pattern in patterns)
                {
                    //加载文件
                    var files = fsm.GetFiles(searchPattern: pattern);
                    foreach (var file in files)
                    {
                        //NOTE:以图片扩展名来获取图标这里可能会造成内存浪费
                        //装载特定图标
                        if (!imageListForfViewer_B.Images.ContainsKey(file.Extension))
                        {
                            imageListForfViewer_B.Images.Add(file.Extension, IconTools.GetFileIcon(file.FullName, false));
                            imageListForfViewer_S.Images.Add(file.Extension, IconTools.GetFileIcon(file.FullName, true));
                        }
                        fViewer.Items.Add(new ListViewItem(file.Name) { Tag = file, ImageKey = file.Extension });
                    }
                }
                text_currentPath.Text = currentFolder;
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="path"></param>
        private void Open(string path)
        {
            fViewer.Cursor = Cursors.WaitCursor;
            //如果是文件夹就打开文件夹并定位树节点
            if (Directory.Exists(path))
            {
                ExpandToPathForTreeView(path);
                LoadFoldersAndFilesToListView(path);
            }
            //如果是文件则打开文件
            if (File.Exists(path))
            {
                switch (Path.GetExtension(path))
                {
                    case ".phz":
                        var zip = archiveCache[path];
                        //这里因为在选中phz时已经读取了所以无需再读取直接解析文件即可
                        var textEditor = new TextEditWindow(Path.GetFileName(path), path, ArchiveTools.GetPhnContent(zip));
                        textEditor.Show(this.DockPanel, DockState.Document);
                        break;
                    case ".phn":
                    case ".cfg":
                    case ".thm":
                        var otherTextEditor = new TextEditWindow();
                        otherTextEditor.FilePath = path;
                        otherTextEditor.Show(this.DockPanel, DockState.Document);
                        break;
                    default:
                    case ".png":
                        Process.Start("explorer.exe",path);
                        break;
                }
            }
            fViewer.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="path"></param>
        private void Copy(string path)
        {
            copyingPath = path;
            isCopying = true;
            isMoving = false;
            粘贴ToolStripMenuItem1.Visible = true;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="path"></param>
        private void Cut(string path)
        {
            copyingPath = path;
            isMoving = true;
            isCopying = false;
            粘贴ToolStripMenuItem1.Visible = true;
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="destination">粘贴目的地</param>
        private void Paste(string destination)
        {
            FileSystemManager fsm;
            //检查是文件夹还是文件
            if (Directory.Exists(copyingPath))
            {
                fsm = new FileSystemManager(new DirectoryInfo(copyingPath));
            }
            else if (File.Exists(copyingPath))
            {
                fsm = new FileSystemManager(new FileInfo(copyingPath));
            }
            else
            {
                return;
            }
            //如果是移动
            if (isMoving)
            {
                fsm.MoveTo(destination);
                isMoving = false;
                copyingPath = string.Empty;
            }
            //如果是复制
            if (isCopying)
            {
                fsm.CopyTo(destination);
                isCopying = false;
                copyingPath = string.Empty;
            }
            粘贴ToolStripMenuItem1.Visible = false;
            粘贴ToolStripMenuItem.Visible = false;
        }

        /// <summary>
        /// 按指定路径刷新全部
        /// </summary>
        private void RefreshAll(string path)
        {
            RefreshTree(path);
            RefreshView(path);
        }

        /// <summary>
        /// 刷新视图
        /// </summary>
        /// <param name="path"></param>
        private void RefreshView(string path)
        {
            LoadFoldersAndFilesToListView(path);
        }

        /// <summary>
        /// 刷新树
        /// </summary>
        /// <param name="path"></param>
        private void RefreshTree(string path)
        {
            ExpandToPathForTreeView(path);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="path"></param>
        private void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                var fsm=new FileSystemManager(new DirectoryInfo(path));
                fsm.Delete(true);
            }
            if (File.Exists(path))
            {
                var fsm = new FileSystemManager(new FileInfo(path));
                fsm.Delete(true);
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newName"></param>
        /// <param name="withExtension"></param>
        /// <returns>返回一个<see cref="FileSystemManager"/>对象，如果异常则返回null</returns>
        private FileSystemManager Rename(string path, string newName, bool withExtension = false)
        {
            FileSystemManager fsm = null;
            if (Directory.Exists(path))
            {
                fsm = new FileSystemManager(new DirectoryInfo(path));
                fsm.Rename(newName, withExtension);
               
            }
            if (File.Exists(path))
            {
                fsm = new FileSystemManager(new FileInfo(path));
                fsm.Rename(newName, withExtension);
                
            }
            return fsm;
        }

        /// <summary>
        /// 加载指定节点的子节点
        /// </summary>
        /// <param name="node">被查看的节点</param>
        /// <returns>子节点数量</returns>
        private static int LoadChildNodeToTreeView(TreeNode node)
        {
            node.Nodes.Clear();//先清空之前的
            var dirs = new FileSystemManager(new DirectoryInfo(node.FullPath)).GetChildDirectories(FileAttributes.System);
            foreach (var dir in dirs)
            {
                node.Nodes.Add(new TreeNode(dir.Name, 0, 0, new TreeNode[1] { new TreeNode("") }) { Tag = dir });
            }
            return dirs.Length;
        }

        /// <summary>
        /// 沿路径展开辅助
        /// </summary>
        private static void ExpandToPathForTreeView(TreeNode node, string[] pathNodes, int index)
        {
            //展开这个节点
            node.Expand();

            //超出索引则返回
            if (index >= pathNodes.Length) return;

            //子节点为0则返回
            if (LoadChildNodeToTreeView(node) == 0) return;

            //遍历递归
            foreach (TreeNode childnode in node.Nodes)
            {
                if (childnode.Text == pathNodes[index])
                {
                    ExpandToPathForTreeView(childnode, pathNodes, ++index);
                    return;
                }
            }
        }

        /// <summary>
        /// 展示指定节点的展开柄
        /// </summary>
        /// <param name="node">需要展示展开柄的节点</param>
        private static void ShowNodePlusHandleForTreeView(TreeNode node)
        {
            node.Nodes.Clear();
            node.Nodes.Add("");
        }
       

        #endregion 辅助性方法

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

        #region 文件树

        /// <summary>
        /// 展开节点（添加子节点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //根据子节点数量和类型展示图标
            if (LoadChildNodeToTreeView(e.Node) > 0)
            {
                if (e.Node.Tag is DriveInfo)
                    e.Node.ImageIndex = 2;
                else
                    e.Node.ImageIndex = 1;
            }
            if (e.Node.Tag is DriveInfo)
                LoadFoldersAndFilesToListView((e.Node.Tag as DriveInfo).Name);
            else
                LoadFoldersAndFilesToListView((e.Node.Tag as FileSystemInfo).FullName);
        }

        /// <summary>
        /// 折叠节点（清理子节点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is DriveInfo)
                e.Node.ImageIndex = 3;
            else
                e.Node.ImageIndex = 0;
            ShowNodePlusHandleForTreeView(e.Node);
        }

        /// <summary>
        /// 选中保持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            folderTree.HideSelection=false;
        }


        #region 文件树右键菜单

        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "创建文件夹";
                tgd.IsNameValidCheck = true;
                tgd.InputText = "New Folder";
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    if (folderTree.SelectedNode.Tag is DriveInfo)
                    {
                        string path = (folderTree.SelectedNode.Tag as DriveInfo).Name;
                        Directory.CreateDirectory($"{path}\\{tgd.InputText}");
                        RefreshAll($"{path}");
                    }
                    if (folderTree.SelectedNode.Tag is FileSystemInfo)
                    {
                        string path = (folderTree.SelectedNode.Tag as FileSystemInfo).FullName;
                        Directory.CreateDirectory($"{path}\\{tgd.InputText}");
                        RefreshAll($"{path}");
                    }
                }
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderTree.SelectedNode.Tag is FileSystemInfo)
            {
                Copy((folderTree.SelectedNode.Tag as FileSystemInfo).FullName);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isCopying || isMoving)
            {
                if (folderTree.SelectedNode.Tag is DriveInfo)
                {
                    Paste((folderTree.SelectedNode.Tag as DriveInfo).Name);
                    RefreshTree((folderTree.SelectedNode.Tag as DriveInfo).Name);
                }
                if (folderTree.SelectedNode.Tag is FileSystemInfo)
                {
                    Paste((folderTree.SelectedNode.Tag as FileSystemInfo).FullName);
                    RefreshTree((folderTree.SelectedNode.Tag as FileSystemInfo).FullName);
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderTree.SelectedNode.Tag is FileSystemInfo)
            {
                var fsm = new FileSystemManager(folderTree.SelectedNode.Tag as FileSystemInfo);
                fsm.Delete(true);
                RefreshAll(fsm.GetLocalDirectory().FullName);
            }
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "重命名";
                tgd.IsNameValidCheck = true;
                var path = (folderTree.SelectedNode.Tag as FileSystemInfo).FullName;
                tgd.InputText = Path.GetFileNameWithoutExtension(path);
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    var fsm = Rename(path, tgd.InputText, tgd.IsGlobal);
                    RefreshAll(fsm.GetLocalDirectory().FullName);
                }
            }
        }

        private void 复制完整路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderTree.SelectedNode.Tag is DriveInfo)
            {
                Clipboard.SetText((folderTree.SelectedNode.Tag as DriveInfo).Name);
            }
            if (folderTree.SelectedNode.Tag is FileSystemInfo)
            {
                Clipboard.SetText((folderTree.SelectedNode.Tag as FileSystemInfo).FullName);
            }
        }

        private void 在资源管理器中打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", new FileSystemManager(folderTree.SelectedNode.Tag as FileSystemInfo).FileSystemObject.FullName);
        }

        /// <summary>
        /// 文件树菜单打开时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //选择项有
            if (folderTree.SelectedNode != null)
            {
                toolStripSeparator5.Visible =
                    重命名ToolStripMenuItem.Visible =
                   删除ToolStripMenuItem.Visible =
                   复制ToolStripMenuItem.Visible =
                   folderTree.SelectedNode.Tag is FileSystemInfo;
                //复制项有和复制项无
                粘贴ToolStripMenuItem.Visible = (isCopying || isMoving);
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
            RefreshAll(currentFolder);
        }

        /// <summary>
        /// 定位到Algodoo根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void algodooPosition_Click(object sender, EventArgs e)
        {
            RefreshAll(Program.Setting.AlgodooPath);
        }

        /// <summary>
        /// 定位到工作室根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void studioPosition_Click(object sender, EventArgs e)
        {
            RefreshAll(Program.Setting.StudioPath);
        }

        /// <summary>
        /// 定位到场景存档根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scenePostition_Click(object sender, EventArgs e)
        {
            RefreshAll(Program.Setting.ScenePath);
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
            var currentDir = new DirectoryInfo(currentFolder);
            if (currentDir.Root.FullName!=currentDir.FullName)
            {
                currentFolder = currentDir.Parent.FullName;
            }
            RefreshAll(currentFolder);
        }

        #endregion 工具栏

        #region 文件展示区

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
            Open((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
        }

        #region 文件展示区右键菜单

        private void fViewerContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //选中和未选中
            if (fViewer.SelectedItems.Count>0)
            {
                //选中的是什么
                if (fViewer.SelectedItems[0].Tag is FileInfo)//是文件则按文件来
                {
                    var obj = fViewer.SelectedItems[0].Tag as FileInfo;
                    switch (obj.Extension)
                    {
                        case ".phz":
                        case ".phn":
                            在Algodoo中打开ToolStripMenuItem.Visible = true;
                            break;
                        default:
                            在Algodoo中打开ToolStripMenuItem.Visible = false;
                            break;
                    }
                }
                else
                {
                    在Algodoo中打开ToolStripMenuItem.Visible = false;
                }

                toolStripSeparator9.Visible =   //这个是关闭时就不显示
                复制完整路径ToolStripMenuItem1.Visible =
                重命名ToolStripMenuItem1.Visible =
                删除ToolStripMenuItem1.Visible =
                复制ToolStripMenuItem1.Visible =
                剪切ToolStripMenuItem1.Visible =
                打开ToolStripMenuItem.Visible = true;

                在资源管理器中打开ToolStripMenuItem1.Visible =
                刷新ToolStripMenuItem.Visible =
                查看ToolStripMenuItem.Visible =
                新建ToolStripMenuItem1.Visible = 
                粘贴ToolStripMenuItem1.Visible = false;
            }
            else
            {
                在资源管理器中打开ToolStripMenuItem1.Visible =
                刷新ToolStripMenuItem.Visible =
                查看ToolStripMenuItem.Visible =
                新建ToolStripMenuItem1.Visible = true;

                toolStripSeparator9.Visible =   //这个是关闭时就不显示
                复制完整路径ToolStripMenuItem1.Visible =
                重命名ToolStripMenuItem1.Visible =
                删除ToolStripMenuItem1.Visible =
                复制ToolStripMenuItem1.Visible =
                剪切ToolStripMenuItem1.Visible =
                在Algodoo中打开ToolStripMenuItem.Visible =
                打开ToolStripMenuItem.Visible = false;

                //复制和未复制
                toolStripSeparator9.Visible = //一旦遇到粘贴就跟粘贴混
                粘贴ToolStripMenuItem1.Visible = isCopying || isMoving;
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
            RefreshAll(currentFolder);
        }

        private void 空文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd=new TextGetDialog())
            {
                tgd.Title = "创建文件夹";
                tgd.IsNameValidCheck = true;
                tgd.InputText = "New Folder";
                if (tgd.ShowDialog()==DialogResult.OK)
                {
                    Directory.CreateDirectory($"{currentFolder}\\{tgd.InputText}");
                    RefreshAll(currentFolder);
                }
            }
        }

        private void pHN文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "创建PHN文件";
                tgd.IsNameValidCheck = true;
                tgd.InputText = "New File";
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    using (File.Create($"{currentFolder}\\{tgd.InputText}.phn")) ;
                    RefreshView(currentFolder);
                }
            }
        }

        private void tHM文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "创建thm文件";
                tgd.IsNameValidCheck = true;
                tgd.InputText = "New File";
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    using (File.Create($"{currentFolder}\\{tgd.InputText}.thm")) ;
                    RefreshView(currentFolder);
                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
        }
        
        private void 在Algodoo中打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fViewer.SelectedItems.Count>0)
            {
                var item = fViewer.SelectedItems[0].Tag as FileSystemInfo;
                if (item.Extension == ".phn" || item.Extension == ".phz")
                    if (File.Exists($"{Program.Setting.AlgodooPath}\\algodoo.exe"))
                        Process.Start($"{Program.Setting.AlgodooPath}\\algodoo.exe", item.FullName);
            }
        }
        
        private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Copy((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isCopying||isMoving)
            {
                Paste(currentFolder);
                RefreshAll(currentFolder);
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Delete((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
            RefreshAll(currentFolder);
        }

        private void 重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "重命名";
                tgd.IsGlobalMode = true;
                tgd.IsNameValidCheck = true;
                var path = (fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName;
                tgd.InputText = Path.GetFileNameWithoutExtension(path);
                if (tgd.ShowDialog() == DialogResult.OK)
                {
                    Rename(path, tgd.InputText, tgd.IsGlobal);
                    RefreshAll(currentFolder);
                }
            }
        }

        private void 复制完整路径ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText((fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName);
        }

        private void 在资源管理器中打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", currentFolder);
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
                string path = (fViewer.SelectedItems[0].Tag as FileSystemInfo).FullName;
                if (!Directory.Exists(path))
                {
                    fViewer.Cursor = Cursors.WaitCursor;
                    string str = "";
                    if (Path.GetExtension(path) == ".phz")
                    {
                        //优先进行内存检查
                        MemoryManage(10);
                        
                        //如果存档不存在则优先添加到其中
                        if (!archiveCache.ContainsKey(path))
                        {
                            archiveCache.Add(path, ArchiveTools.DeCompress(path));
                        }
                        //加载存档信息
                        var info = new LoadedFileInfo(archiveCache[path].Phn);
                        str += "存档作者：" + info.Author + "\n";
                        Program.SetPropertyEditObject(info);
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
            archiveCache.Clear();
            GC.Collect(2);
        }

        
    }
}

namespace AlgodooStudio.Window
{
    partial class ResourceExplorerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceExplorerWindow));
            this.menuSpliter = new System.Windows.Forms.SplitContainer();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.algodooPosition = new System.Windows.Forms.ToolStripButton();
            this.studioPosition = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshTree = new System.Windows.Forms.ToolStripButton();
            this.collapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.listView = new System.Windows.Forms.ToolStripButton();
            this.bigView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.returnUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.debugButton = new System.Windows.Forms.ToolStripButton();
            this.contentSpilter = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.folderTree = new System.Windows.Forms.TreeView();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.复制完整路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在资源管理器中打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListForTree = new System.Windows.Forms.ImageList(this.components);
            this.fViewer = new System.Windows.Forms.ListView();
            this.fViewerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.空文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平铺ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解析phz文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解析phn文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用指定应用打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.复制完整路径ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.在新管理器中打开ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.在资源管理器中打开ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListForfViewer_B = new System.Windows.Forms.ImageList(this.components);
            this.imageListForfViewer_S = new System.Windows.Forms.ImageList(this.components);
            this.fileTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.menuSpliter)).BeginInit();
            this.menuSpliter.Panel1.SuspendLayout();
            this.menuSpliter.Panel2.SuspendLayout();
            this.menuSpliter.SuspendLayout();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contentSpilter)).BeginInit();
            this.contentSpilter.Panel1.SuspendLayout();
            this.contentSpilter.Panel2.SuspendLayout();
            this.contentSpilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.treeContextMenu.SuspendLayout();
            this.fViewerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSpliter
            // 
            this.menuSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuSpliter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.menuSpliter.IsSplitterFixed = true;
            this.menuSpliter.Location = new System.Drawing.Point(0, 0);
            this.menuSpliter.Name = "menuSpliter";
            this.menuSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // menuSpliter.Panel1
            // 
            this.menuSpliter.Panel1.Controls.Add(this.toolBar);
            // 
            // menuSpliter.Panel2
            // 
            this.menuSpliter.Panel2.Controls.Add(this.contentSpilter);
            this.menuSpliter.Size = new System.Drawing.Size(733, 450);
            this.menuSpliter.SplitterDistance = 28;
            this.menuSpliter.SplitterWidth = 1;
            this.menuSpliter.TabIndex = 0;
            // 
            // toolBar
            // 
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algodooPosition,
            this.studioPosition,
            this.toolStripSeparator1,
            this.refreshTree,
            this.collapse,
            this.toolStripSeparator2,
            this.listView,
            this.bigView,
            this.toolStripSeparator3,
            this.returnUp,
            this.toolStripSeparator4,
            this.debugButton});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolBar.Size = new System.Drawing.Size(733, 28);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            // 
            // algodooPosition
            // 
            this.algodooPosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.algodooPosition.Image = ((System.Drawing.Image)(resources.GetObject("algodooPosition.Image")));
            this.algodooPosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.algodooPosition.Name = "algodooPosition";
            this.algodooPosition.Size = new System.Drawing.Size(23, 25);
            this.algodooPosition.Text = "定位到Algodoo根目录";
            this.algodooPosition.Click += new System.EventHandler(this.algodooPosition_Click);
            // 
            // studioPosition
            // 
            this.studioPosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.studioPosition.Image = ((System.Drawing.Image)(resources.GetObject("studioPosition.Image")));
            this.studioPosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.studioPosition.Name = "studioPosition";
            this.studioPosition.Size = new System.Drawing.Size(23, 25);
            this.studioPosition.Text = "定位到工作室目录";
            this.studioPosition.Click += new System.EventHandler(this.studioPosition_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // refreshTree
            // 
            this.refreshTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshTree.Image = ((System.Drawing.Image)(resources.GetObject("refreshTree.Image")));
            this.refreshTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshTree.Name = "refreshTree";
            this.refreshTree.Size = new System.Drawing.Size(23, 25);
            this.refreshTree.Text = "刷新文件树";
            this.refreshTree.Click += new System.EventHandler(this.refreshTree_Click);
            // 
            // collapse
            // 
            this.collapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collapse.Image = ((System.Drawing.Image)(resources.GetObject("collapse.Image")));
            this.collapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.collapse.Name = "collapse";
            this.collapse.Size = new System.Drawing.Size(23, 25);
            this.collapse.Text = "全部折叠";
            this.collapse.Click += new System.EventHandler(this.collapse_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // listView
            // 
            this.listView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.listView.Image = ((System.Drawing.Image)(resources.GetObject("listView.Image")));
            this.listView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(23, 25);
            this.listView.Text = "列表预览";
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // bigView
            // 
            this.bigView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bigView.Image = ((System.Drawing.Image)(resources.GetObject("bigView.Image")));
            this.bigView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bigView.Name = "bigView";
            this.bigView.Size = new System.Drawing.Size(23, 25);
            this.bigView.Text = "大图预览";
            this.bigView.Click += new System.EventHandler(this.bigView_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // returnUp
            // 
            this.returnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.returnUp.Image = ((System.Drawing.Image)(resources.GetObject("returnUp.Image")));
            this.returnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.returnUp.Name = "returnUp";
            this.returnUp.Size = new System.Drawing.Size(23, 25);
            this.returnUp.Text = "返回上一级";
            this.returnUp.Click += new System.EventHandler(this.returnUp_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // debugButton
            // 
            this.debugButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.debugButton.Image = ((System.Drawing.Image)(resources.GetObject("debugButton.Image")));
            this.debugButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(23, 25);
            this.debugButton.Text = "DebugButton";
            // 
            // contentSpilter
            // 
            this.contentSpilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentSpilter.Location = new System.Drawing.Point(0, 0);
            this.contentSpilter.Name = "contentSpilter";
            // 
            // contentSpilter.Panel1
            // 
            this.contentSpilter.Panel1.Controls.Add(this.splitContainer1);
            // 
            // contentSpilter.Panel2
            // 
            this.contentSpilter.Panel2.Controls.Add(this.fViewer);
            this.contentSpilter.Size = new System.Drawing.Size(733, 421);
            this.contentSpilter.SplitterDistance = 244;
            this.contentSpilter.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.searchBox);
            this.splitContainer1.Panel1MinSize = 20;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.folderTree);
            this.splitContainer1.Panel2MinSize = 20;
            this.splitContainer1.Size = new System.Drawing.Size(244, 421);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // searchBox
            // 
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox.Font = new System.Drawing.Font("宋体", 10F);
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(244, 23);
            this.searchBox.TabIndex = 1;
            this.searchBox.Text = "从文件夹中搜索文件(Enter)";
            this.searchBox.WordWrap = false;
            this.searchBox.Click += new System.EventHandler(this.searchBox_Click);
            // 
            // folderTree
            // 
            this.folderTree.ContextMenuStrip = this.treeContextMenu;
            this.folderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTree.ForeColor = System.Drawing.SystemColors.WindowText;
            this.folderTree.HotTracking = true;
            this.folderTree.ImageIndex = 0;
            this.folderTree.ImageList = this.imageListForTree;
            this.folderTree.Location = new System.Drawing.Point(0, 0);
            this.folderTree.Name = "folderTree";
            this.folderTree.SelectedImageIndex = 0;
            this.folderTree.Size = new System.Drawing.Size(244, 395);
            this.folderTree.TabIndex = 0;
            this.folderTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.folderTree_AfterCollapse);
            this.folderTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.folderTree_BeforeExpand);
            this.folderTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.folderTree_BeforeSelect);
            this.folderTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.folderTree_MouseDown);
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建文件夹ToolStripMenuItem,
            this.toolStripSeparator5,
            this.剪切ToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.toolStripSeparator6,
            this.复制完整路径ToolStripMenuItem,
            this.在资源管理器中打开ToolStripMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(185, 192);
            this.treeContextMenu.VisibleChanged += new System.EventHandler(this.treeContextMenu_VisibleChanged);
            // 
            // 新建文件夹ToolStripMenuItem
            // 
            this.新建文件夹ToolStripMenuItem.Name = "新建文件夹ToolStripMenuItem";
            this.新建文件夹ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.新建文件夹ToolStripMenuItem.Text = "新建文件夹...";
            this.新建文件夹ToolStripMenuItem.Click += new System.EventHandler(this.新建文件夹ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.重命名ToolStripMenuItem.Text = "重命名...";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            // 
            // 复制完整路径ToolStripMenuItem
            // 
            this.复制完整路径ToolStripMenuItem.Name = "复制完整路径ToolStripMenuItem";
            this.复制完整路径ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.复制完整路径ToolStripMenuItem.Text = "复制完整路径";
            this.复制完整路径ToolStripMenuItem.Click += new System.EventHandler(this.复制完整路径ToolStripMenuItem_Click);
            // 
            // 在资源管理器中打开ToolStripMenuItem
            // 
            this.在资源管理器中打开ToolStripMenuItem.Name = "在资源管理器中打开ToolStripMenuItem";
            this.在资源管理器中打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.在资源管理器中打开ToolStripMenuItem.Text = "在资源管理器中打开";
            this.在资源管理器中打开ToolStripMenuItem.Click += new System.EventHandler(this.在资源管理器中打开ToolStripMenuItem_Click);
            // 
            // imageListForTree
            // 
            this.imageListForTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForTree.ImageStream")));
            this.imageListForTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForTree.Images.SetKeyName(0, "_ri_0.png");
            this.imageListForTree.Images.SetKeyName(1, "_ri_1.png");
            this.imageListForTree.Images.SetKeyName(2, "_ri_disk_bl.png");
            this.imageListForTree.Images.SetKeyName(3, "_ri_disk_d.png");
            // 
            // fViewer
            // 
            this.fViewer.ContextMenuStrip = this.fViewerContextMenu;
            this.fViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fViewer.HideSelection = false;
            this.fViewer.LargeImageList = this.imageListForfViewer_B;
            this.fViewer.Location = new System.Drawing.Point(0, 0);
            this.fViewer.MultiSelect = false;
            this.fViewer.Name = "fViewer";
            this.fViewer.Size = new System.Drawing.Size(485, 421);
            this.fViewer.SmallImageList = this.imageListForfViewer_S;
            this.fViewer.TabIndex = 0;
            this.fViewer.UseCompatibleStateImageBehavior = false;
            this.fViewer.View = System.Windows.Forms.View.Tile;
            this.fViewer.ItemActivate += new System.EventHandler(this.fViewer_ItemActivate);
            this.fViewer.SelectedIndexChanged += new System.EventHandler(this.fViewer_SelectedIndexChanged);
            this.fViewer.MouseLeave += new System.EventHandler(this.fViewer_MouseLeave);
            // 
            // fViewerContextMenu
            // 
            this.fViewerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem1,
            this.查看ToolStripMenuItem,
            this.刷新ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.解析phz文件ToolStripMenuItem,
            this.解析phn文件ToolStripMenuItem,
            this.使用指定应用打开ToolStripMenuItem,
            this.toolStripSeparator9,
            this.剪切ToolStripMenuItem1,
            this.复制ToolStripMenuItem1,
            this.粘贴ToolStripMenuItem1,
            this.删除ToolStripMenuItem1,
            this.重命名ToolStripMenuItem1,
            this.toolStripSeparator8,
            this.复制完整路径ToolStripMenuItem1,
            this.在新管理器中打开ToolStripMenuItem1,
            this.在资源管理器中打开ToolStripMenuItem1});
            this.fViewerContextMenu.Name = "treeContextMenu";
            this.fViewerContextMenu.Size = new System.Drawing.Size(185, 346);
            this.fViewerContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.fViewerContextMenu_Opening);
            // 
            // 新建ToolStripMenuItem1
            // 
            this.新建ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空文件夹ToolStripMenuItem});
            this.新建ToolStripMenuItem1.Name = "新建ToolStripMenuItem1";
            this.新建ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.新建ToolStripMenuItem1.Text = "新建";
            // 
            // 空文件夹ToolStripMenuItem
            // 
            this.空文件夹ToolStripMenuItem.Name = "空文件夹ToolStripMenuItem";
            this.空文件夹ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.空文件夹ToolStripMenuItem.Text = "空文件夹";
            this.空文件夹ToolStripMenuItem.Click += new System.EventHandler(this.空文件夹ToolStripMenuItem_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.列表ToolStripMenuItem,
            this.平铺ToolStripMenuItem});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // 列表ToolStripMenuItem
            // 
            this.列表ToolStripMenuItem.Name = "列表ToolStripMenuItem";
            this.列表ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.列表ToolStripMenuItem.Text = "列表";
            this.列表ToolStripMenuItem.Click += new System.EventHandler(this.列表ToolStripMenuItem_Click);
            // 
            // 平铺ToolStripMenuItem
            // 
            this.平铺ToolStripMenuItem.Name = "平铺ToolStripMenuItem";
            this.平铺ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.平铺ToolStripMenuItem.Text = "平铺";
            this.平铺ToolStripMenuItem.Click += new System.EventHandler(this.平铺ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 解析phz文件ToolStripMenuItem
            // 
            this.解析phz文件ToolStripMenuItem.Name = "解析phz文件ToolStripMenuItem";
            this.解析phz文件ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.解析phz文件ToolStripMenuItem.Text = "解析phz文件";
            this.解析phz文件ToolStripMenuItem.Click += new System.EventHandler(this.解析phz文件ToolStripMenuItem_Click);
            // 
            // 解析phn文件ToolStripMenuItem
            // 
            this.解析phn文件ToolStripMenuItem.Name = "解析phn文件ToolStripMenuItem";
            this.解析phn文件ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.解析phn文件ToolStripMenuItem.Text = "解析phn文件";
            this.解析phn文件ToolStripMenuItem.Click += new System.EventHandler(this.解析phn文件ToolStripMenuItem_Click);
            // 
            // 使用指定应用打开ToolStripMenuItem
            // 
            this.使用指定应用打开ToolStripMenuItem.Name = "使用指定应用打开ToolStripMenuItem";
            this.使用指定应用打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.使用指定应用打开ToolStripMenuItem.Text = "使用指定应用打开...";
            this.使用指定应用打开ToolStripMenuItem.Click += new System.EventHandler(this.使用指定应用打开ToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(181, 6);
            // 
            // 剪切ToolStripMenuItem1
            // 
            this.剪切ToolStripMenuItem1.Name = "剪切ToolStripMenuItem1";
            this.剪切ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.剪切ToolStripMenuItem1.Text = "剪切";
            this.剪切ToolStripMenuItem1.Click += new System.EventHandler(this.剪切ToolStripMenuItem1_Click);
            // 
            // 复制ToolStripMenuItem1
            // 
            this.复制ToolStripMenuItem1.Name = "复制ToolStripMenuItem1";
            this.复制ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.复制ToolStripMenuItem1.Text = "复制";
            this.复制ToolStripMenuItem1.Click += new System.EventHandler(this.复制ToolStripMenuItem1_Click);
            // 
            // 粘贴ToolStripMenuItem1
            // 
            this.粘贴ToolStripMenuItem1.Name = "粘贴ToolStripMenuItem1";
            this.粘贴ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.粘贴ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.粘贴ToolStripMenuItem1.Text = "粘贴";
            this.粘贴ToolStripMenuItem1.Click += new System.EventHandler(this.粘贴ToolStripMenuItem1_Click);
            // 
            // 删除ToolStripMenuItem1
            // 
            this.删除ToolStripMenuItem1.Name = "删除ToolStripMenuItem1";
            this.删除ToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.删除ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.删除ToolStripMenuItem1.Text = "删除";
            this.删除ToolStripMenuItem1.Click += new System.EventHandler(this.删除ToolStripMenuItem1_Click);
            // 
            // 重命名ToolStripMenuItem1
            // 
            this.重命名ToolStripMenuItem1.Name = "重命名ToolStripMenuItem1";
            this.重命名ToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.重命名ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.重命名ToolStripMenuItem1.Text = "重命名...";
            this.重命名ToolStripMenuItem1.Click += new System.EventHandler(this.重命名ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(181, 6);
            // 
            // 复制完整路径ToolStripMenuItem1
            // 
            this.复制完整路径ToolStripMenuItem1.Name = "复制完整路径ToolStripMenuItem1";
            this.复制完整路径ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.复制完整路径ToolStripMenuItem1.Text = "复制完整路径";
            this.复制完整路径ToolStripMenuItem1.Click += new System.EventHandler(this.复制完整路径ToolStripMenuItem1_Click);
            // 
            // 在新管理器中打开ToolStripMenuItem1
            // 
            this.在新管理器中打开ToolStripMenuItem1.Name = "在新管理器中打开ToolStripMenuItem1";
            this.在新管理器中打开ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.在新管理器中打开ToolStripMenuItem1.Text = "在新管理器中打开";
            this.在新管理器中打开ToolStripMenuItem1.Click += new System.EventHandler(this.在新管理器中打开ToolStripMenuItem1_Click);
            // 
            // 在资源管理器中打开ToolStripMenuItem1
            // 
            this.在资源管理器中打开ToolStripMenuItem1.Name = "在资源管理器中打开ToolStripMenuItem1";
            this.在资源管理器中打开ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.在资源管理器中打开ToolStripMenuItem1.Text = "在资源管理器中打开";
            this.在资源管理器中打开ToolStripMenuItem1.Click += new System.EventHandler(this.在资源管理器中打开ToolStripMenuItem1_Click);
            // 
            // imageListForfViewer_B
            // 
            this.imageListForfViewer_B.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForfViewer_B.ImageStream")));
            this.imageListForfViewer_B.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForfViewer_B.Images.SetKeyName(0, "_ri_0.png");
            this.imageListForfViewer_B.Images.SetKeyName(1, "_ri_1.png");
            // 
            // imageListForfViewer_S
            // 
            this.imageListForfViewer_S.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForfViewer_S.ImageStream")));
            this.imageListForfViewer_S.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForfViewer_S.Images.SetKeyName(0, "_ri_0.png");
            this.imageListForfViewer_S.Images.SetKeyName(1, "_ri_1.png");
            // 
            // fileTip
            // 
            this.fileTip.AutoPopDelay = 1000;
            this.fileTip.InitialDelay = 500;
            this.fileTip.ReshowDelay = 100;
            this.fileTip.ToolTipTitle = "文件信息:";
            // 
            // ResourceExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.menuSpliter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResourceExplorerWindow";
            this.Text = "资源管理器";
            this.DockStateChanged += new System.EventHandler(this.ResourceExplorer_DockStateChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ResourceExplorer_FormClosed);
            this.menuSpliter.Panel1.ResumeLayout(false);
            this.menuSpliter.Panel1.PerformLayout();
            this.menuSpliter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menuSpliter)).EndInit();
            this.menuSpliter.ResumeLayout(false);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.contentSpilter.Panel1.ResumeLayout(false);
            this.contentSpilter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contentSpilter)).EndInit();
            this.contentSpilter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.treeContextMenu.ResumeLayout(false);
            this.fViewerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer menuSpliter;
        private System.Windows.Forms.SplitContainer contentSpilter;
        private System.Windows.Forms.TreeView folderTree;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton algodooPosition;
        private System.Windows.Forms.ToolStripButton studioPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton refreshTree;
        private System.Windows.Forms.ToolStripButton collapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton listView;
        private System.Windows.Forms.ToolStripButton bigView;
        private System.Windows.Forms.ToolStripButton debugButton;
        private System.Windows.Forms.ImageList imageListForTree;
        private System.Windows.Forms.ListView fViewer;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ImageList imageListForfViewer_B;
        private System.Windows.Forms.ImageList imageListForfViewer_S;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton returnUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ContextMenuStrip fViewerContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 复制完整路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在资源管理器中打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用指定应用打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem 复制完整路径ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平铺ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 空文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在新管理器中打开ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 在资源管理器中打开ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 解析phz文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解析phn文件ToolStripMenuItem;
        private System.Windows.Forms.ToolTip fileTip;
    }
}
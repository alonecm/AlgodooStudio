
namespace AlgodooStudio.Forms
{
    partial class ResourceExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceExplorer));
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.contentSpilter = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.folderTree = new System.Windows.Forms.TreeView();
            this.flPanel = new System.Windows.Forms.FlowLayoutPanel();
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
            this.toolStripButton1});
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
            // 
            // studioPosition
            // 
            this.studioPosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.studioPosition.Image = ((System.Drawing.Image)(resources.GetObject("studioPosition.Image")));
            this.studioPosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.studioPosition.Name = "studioPosition";
            this.studioPosition.Size = new System.Drawing.Size(23, 25);
            this.studioPosition.Text = "定位到工作室目录";
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
            // 
            // collapse
            // 
            this.collapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collapse.Image = ((System.Drawing.Image)(resources.GetObject("collapse.Image")));
            this.collapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.collapse.Name = "collapse";
            this.collapse.Size = new System.Drawing.Size(23, 25);
            this.collapse.Text = "全部折叠";
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
            // 
            // bigView
            // 
            this.bigView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bigView.Image = ((System.Drawing.Image)(resources.GetObject("bigView.Image")));
            this.bigView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bigView.Name = "bigView";
            this.bigView.Size = new System.Drawing.Size(23, 25);
            this.bigView.Text = "大图预览";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 25);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            this.contentSpilter.Panel2.Controls.Add(this.flPanel);
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
            this.searchBox.TabIndex = 0;
            this.searchBox.Text = "从文件树中搜索文件(Enter)";
            this.searchBox.WordWrap = false;
            // 
            // folderTree
            // 
            this.folderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTree.Location = new System.Drawing.Point(0, 0);
            this.folderTree.Name = "folderTree";
            this.folderTree.Size = new System.Drawing.Size(244, 395);
            this.folderTree.TabIndex = 0;
            // 
            // flPanel
            // 
            this.flPanel.AutoScroll = true;
            this.flPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flPanel.Location = new System.Drawing.Point(0, 0);
            this.flPanel.Name = "flPanel";
            this.flPanel.Size = new System.Drawing.Size(485, 421);
            this.flPanel.TabIndex = 0;
            // 
            // ResourceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.Controls.Add(this.menuSpliter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResourceExplorer";
            this.Text = "资源管理器";
            this.DockStateChanged += new System.EventHandler(this.ResourceExplorer_DockStateChanged);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer menuSpliter;
        private System.Windows.Forms.SplitContainer contentSpilter;
        private System.Windows.Forms.TreeView folderTree;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ToolStripButton algodooPosition;
        private System.Windows.Forms.ToolStripButton studioPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton refreshTree;
        private System.Windows.Forms.ToolStripButton collapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton listView;
        private System.Windows.Forms.ToolStripButton bigView;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.FlowLayoutPanel flPanel;
    }
}
namespace AlgodooStudio.Window
{
    partial class ScriptManagerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptManagerWindow));
            this.scriptList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.在资源管理器中打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.addTo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.enableAll = new System.Windows.Forms.ToolStripButton();
            this.disableAll = new System.Windows.Forms.ToolStripButton();
            this.removeAll = new System.Windows.Forms.ToolStripButton();
            this.split = new System.Windows.Forms.SplitContainer();
            this.rightMenu.SuspendLayout();
            this.toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // scriptList
            // 
            this.scriptList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scriptList.CheckBoxes = true;
            this.scriptList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.scriptList.ContextMenuStrip = this.rightMenu;
            this.scriptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptList.FullRowSelect = true;
            this.scriptList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.scriptList.HideSelection = false;
            this.scriptList.Location = new System.Drawing.Point(0, 0);
            this.scriptList.Name = "scriptList";
            this.scriptList.OwnerDraw = true;
            this.scriptList.ShowItemToolTips = true;
            this.scriptList.Size = new System.Drawing.Size(235, 497);
            this.scriptList.TabIndex = 0;
            this.scriptList.UseCompatibleStateImageBehavior = false;
            this.scriptList.View = System.Windows.Forms.View.Details;
            this.scriptList.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.scriptList_ColumnWidthChanged);
            this.scriptList.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.scriptList_DrawColumnHeader);
            this.scriptList.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.scriptList_DrawItem);
            this.scriptList.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.scriptList_DrawSubItem);
            this.scriptList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.scriptList_ItemChecked);
            this.scriptList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.scriptList_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "脚本名";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "所在位置";
            this.columnHeader2.Width = 144;
            // 
            // rightMenu
            // 
            this.rightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.toolStripSeparator3,
            this.移除ToolStripMenuItem,
            this.toolStripSeparator4,
            this.在资源管理器中打开ToolStripMenuItem});
            this.rightMenu.Name = "rightMenu";
            this.rightMenu.Size = new System.Drawing.Size(185, 82);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.添加ToolStripMenuItem.Text = "添加...";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // 移除ToolStripMenuItem
            // 
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.移除ToolStripMenuItem.Text = "移除";
            this.移除ToolStripMenuItem.Visible = false;
            this.移除ToolStripMenuItem.Click += new System.EventHandler(this.移除ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
            this.toolStripSeparator4.Visible = false;
            // 
            // 在资源管理器中打开ToolStripMenuItem
            // 
            this.在资源管理器中打开ToolStripMenuItem.Name = "在资源管理器中打开ToolStripMenuItem";
            this.在资源管理器中打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.在资源管理器中打开ToolStripMenuItem.Text = "在资源管理器中打开";
            this.在资源管理器中打开ToolStripMenuItem.Visible = false;
            this.在资源管理器中打开ToolStripMenuItem.Click += new System.EventHandler(this.在资源管理器中打开ToolStripMenuItem_Click);
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTo,
            this.toolStripSeparator1,
            this.removeSelected,
            this.toolStripSeparator2,
            this.enableAll,
            this.disableAll,
            this.removeAll});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(235, 25);
            this.toolbar.TabIndex = 1;
            this.toolbar.Text = "toolStrip1";
            // 
            // addTo
            // 
            this.addTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTo.Image = ((System.Drawing.Image)(resources.GetObject("addTo.Image")));
            this.addTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTo.Name = "addTo";
            this.addTo.Size = new System.Drawing.Size(23, 22);
            this.addTo.Text = "添加";
            this.addTo.Click += new System.EventHandler(this.addTo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // removeSelected
            // 
            this.removeSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeSelected.Image = ((System.Drawing.Image)(resources.GetObject("removeSelected.Image")));
            this.removeSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSelected.Name = "removeSelected";
            this.removeSelected.Size = new System.Drawing.Size(23, 22);
            this.removeSelected.Text = "移除选定项";
            this.removeSelected.Visible = false;
            this.removeSelected.Click += new System.EventHandler(this.removeSelected_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // enableAll
            // 
            this.enableAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enableAll.Image = ((System.Drawing.Image)(resources.GetObject("enableAll.Image")));
            this.enableAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableAll.Name = "enableAll";
            this.enableAll.Size = new System.Drawing.Size(23, 22);
            this.enableAll.Text = "启用全部";
            this.enableAll.Click += new System.EventHandler(this.enableAll_Click);
            // 
            // disableAll
            // 
            this.disableAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.disableAll.Image = ((System.Drawing.Image)(resources.GetObject("disableAll.Image")));
            this.disableAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.disableAll.Name = "disableAll";
            this.disableAll.Size = new System.Drawing.Size(23, 22);
            this.disableAll.Text = "停用全部";
            this.disableAll.Click += new System.EventHandler(this.disableAll_Click);
            // 
            // removeAll
            // 
            this.removeAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeAll.Image = ((System.Drawing.Image)(resources.GetObject("removeAll.Image")));
            this.removeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeAll.Name = "removeAll";
            this.removeAll.Size = new System.Drawing.Size(23, 22);
            this.removeAll.Text = "移除全部";
            this.removeAll.Click += new System.EventHandler(this.removeAll_Click);
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.split.IsSplitterFixed = true;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.toolbar);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.scriptList);
            this.split.Size = new System.Drawing.Size(235, 525);
            this.split.SplitterDistance = 27;
            this.split.SplitterWidth = 1;
            this.split.TabIndex = 2;
            // 
            // ScriptManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 525);
            this.Controls.Add(this.split);
            this.Name = "ScriptManagerWindow";
            this.Text = "脚本管理器";
            this.Shown += new System.EventHandler(this.ScriptManagerWindow_Shown);
            this.SizeChanged += new System.EventHandler(this.ScriptManagerWindow_SizeChanged);
            this.rightMenu.ResumeLayout(false);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel1.PerformLayout();
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView scriptList;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton addTo;
        private System.Windows.Forms.ToolStripButton removeAll;
        private System.Windows.Forms.ToolStripButton removeSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton enableAll;
        private System.Windows.Forms.ToolStripButton disableAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 在资源管理器中打开ToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}
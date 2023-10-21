
namespace AlgodooStudio.Basic
{
    partial class ResourceUnit
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.icon = new System.Windows.Forms.PictureBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.复制文件路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在资源管理器中打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileName = new System.Windows.Forms.Label();
            this.viewInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.icon);
            this.splitContainer1.Panel1MinSize = 10;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileName);
            this.splitContainer1.Panel2MinSize = 10;
            this.splitContainer1.Size = new System.Drawing.Size(100, 130);
            this.splitContainer1.SplitterDistance = 89;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.contextMenu;
            this.icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.icon.Location = new System.Drawing.Point(0, 0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(100, 89);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon.TabIndex = 0;
            this.icon.TabStop = false;
            this.viewInfo.SetToolTip(this.icon, "...");
            this.icon.Click += new System.EventHandler(this.icon_Click);
            this.icon.MouseEnter += new System.EventHandler(this.icon_MouseEnter);
            this.icon.MouseLeave += new System.EventHandler(this.icon_MouseLeave);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.toolStripSeparator1,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.toolStripSeparator2,
            this.复制文件路径ToolStripMenuItem,
            this.在资源管理器中打开ToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(185, 192);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.重命名ToolStripMenuItem.Text = "重命名";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // 复制文件路径ToolStripMenuItem
            // 
            this.复制文件路径ToolStripMenuItem.Name = "复制文件路径ToolStripMenuItem";
            this.复制文件路径ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.复制文件路径ToolStripMenuItem.Text = "复制文件路径";
            // 
            // 在资源管理器中打开ToolStripMenuItem
            // 
            this.在资源管理器中打开ToolStripMenuItem.Name = "在资源管理器中打开ToolStripMenuItem";
            this.在资源管理器中打开ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.在资源管理器中打开ToolStripMenuItem.Text = "在资源管理器中打开";
            // 
            // fileName
            // 
            this.fileName.ContextMenuStrip = this.contextMenu;
            this.fileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileName.Location = new System.Drawing.Point(0, 0);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(100, 40);
            this.fileName.TabIndex = 0;
            this.fileName.Text = "[fileName]";
            this.fileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.viewInfo.SetToolTip(this.fileName, "...");
            this.fileName.Click += new System.EventHandler(this.fileName_Click);
            this.fileName.MouseEnter += new System.EventHandler(this.fileName_MouseEnter);
            this.fileName.MouseLeave += new System.EventHandler(this.fileName_MouseLeave);
            // 
            // viewInfo
            // 
            this.viewInfo.ToolTipTitle = "文件信息";
            this.viewInfo.Popup += new System.Windows.Forms.PopupEventHandler(this.viewInfo_Popup);
            // 
            // ResourceUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ResourceUnit";
            this.Size = new System.Drawing.Size(100, 130);
            this.Load += new System.EventHandler(this.ResourceUnit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 复制文件路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在资源管理器中打开ToolStripMenuItem;
        private System.Windows.Forms.ToolTip viewInfo;
    }
}

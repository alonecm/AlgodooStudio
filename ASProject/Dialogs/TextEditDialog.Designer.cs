namespace AlgodooStudio.ASProject.Dialogs
{
    partial class TextEditDialog
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
            this.sp_ud = new System.Windows.Forms.SplitContainer();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.sp_lr = new System.Windows.Forms.SplitContainer();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.快速输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.sp_ud)).BeginInit();
            this.sp_ud.Panel1.SuspendLayout();
            this.sp_ud.Panel2.SuspendLayout();
            this.sp_ud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp_lr)).BeginInit();
            this.sp_lr.Panel2.SuspendLayout();
            this.sp_lr.SuspendLayout();
            this.rightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp_ud
            // 
            this.sp_ud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp_ud.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp_ud.Location = new System.Drawing.Point(0, 0);
            this.sp_ud.Name = "sp_ud";
            this.sp_ud.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp_ud.Panel1
            // 
            this.sp_ud.Panel1.Controls.Add(this.elementHost);
            // 
            // sp_ud.Panel2
            // 
            this.sp_ud.Panel2.Controls.Add(this.sp_lr);
            this.sp_ud.Size = new System.Drawing.Size(800, 450);
            this.sp_ud.SplitterDistance = 407;
            this.sp_ud.SplitterWidth = 1;
            this.sp_ud.TabIndex = 0;
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 0);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(800, 407);
            this.elementHost.TabIndex = 0;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = null;
            // 
            // sp_lr
            // 
            this.sp_lr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp_lr.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp_lr.Location = new System.Drawing.Point(0, 0);
            this.sp_lr.Name = "sp_lr";
            // 
            // sp_lr.Panel2
            // 
            this.sp_lr.Panel2.Controls.Add(this.cancel_button);
            this.sp_lr.Panel2.Controls.Add(this.ok_button);
            this.sp_lr.Size = new System.Drawing.Size(800, 42);
            this.sp_lr.SplitterDistance = 543;
            this.sp_lr.SplitterWidth = 1;
            this.sp_lr.TabIndex = 0;
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(129, 2);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(120, 38);
            this.cancel_button.TabIndex = 0;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ok_button.Location = new System.Drawing.Point(3, 2);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(120, 38);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "确定";
            this.ok_button.UseVisualStyleBackColor = true;
            // 
            // rightMenu
            // 
            this.rightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查找ToolStripMenuItem,
            this.替换ToolStripMenuItem,
            this.toolStripSeparator1,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.toolStripSeparator3,
            this.快速输入ToolStripMenuItem});
            this.rightMenu.Name = "rightMenu";
            this.rightMenu.Size = new System.Drawing.Size(147, 148);
            this.rightMenu.Opening += new System.ComponentModel.CancelEventHandler(this.rightMenu_Opening);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.查找ToolStripMenuItem.Text = "查找";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
            // 
            // 替换ToolStripMenuItem
            // 
            this.替换ToolStripMenuItem.Name = "替换ToolStripMenuItem";
            this.替换ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.替换ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.替换ToolStripMenuItem.Text = "替换";
            this.替换ToolStripMenuItem.Click += new System.EventHandler(this.替换ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // 快速输入ToolStripMenuItem
            // 
            this.快速输入ToolStripMenuItem.Name = "快速输入ToolStripMenuItem";
            this.快速输入ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.快速输入ToolStripMenuItem.Text = "快速输入...";
            this.快速输入ToolStripMenuItem.Click += new System.EventHandler(this.快速输入ToolStripMenuItem_Click);
            // 
            // TextEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sp_ud);
            this.Name = "TextEditDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文本编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditDialog_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TextEditDialog_FormClosed);
            this.sp_ud.Panel1.ResumeLayout(false);
            this.sp_ud.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sp_ud)).EndInit();
            this.sp_ud.ResumeLayout(false);
            this.sp_lr.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sp_lr)).EndInit();
            this.sp_lr.ResumeLayout(false);
            this.rightMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sp_ud;
        private System.Windows.Forms.SplitContainer sp_lr;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 替换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 快速输入ToolStripMenuItem;
    }
}
namespace AlgodooStudio.ASProject.Dialogs
{
    partial class ClipEditDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.t_description = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.b_clear = new System.Windows.Forms.Button();
            this.b_save = new System.Windows.Forms.Button();
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.快速输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.p_element = new System.Windows.Forms.Panel();
            this.rightMenu.SuspendLayout();
            this.p_element.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "描述";
            // 
            // t_description
            // 
            this.t_description.Location = new System.Drawing.Point(47, 12);
            this.t_description.Name = "t_description";
            this.t_description.Size = new System.Drawing.Size(631, 21);
            this.t_description.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "代码片段";
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 0);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(580, 326);
            this.elementHost.TabIndex = 2;
            this.elementHost.Text = "elementHost1";
            this.elementHost.Child = null;
            // 
            // b_clear
            // 
            this.b_clear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_clear.Location = new System.Drawing.Point(635, 41);
            this.b_clear.Name = "b_clear";
            this.b_clear.Size = new System.Drawing.Size(43, 57);
            this.b_clear.TabIndex = 3;
            this.b_clear.Text = "清空";
            this.b_clear.UseVisualStyleBackColor = true;
            this.b_clear.Click += new System.EventHandler(this.b_clear_Click);
            // 
            // b_save
            // 
            this.b_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_save.Location = new System.Drawing.Point(635, 309);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(43, 60);
            this.b_save.TabIndex = 3;
            this.b_save.Text = "保存  并  返回";
            this.b_save.UseVisualStyleBackColor = true;
            this.b_save.Click += new System.EventHandler(this.b_saveAndReturn_Click);
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
            // p_element
            // 
            this.p_element.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_element.ContextMenuStrip = this.rightMenu;
            this.p_element.Controls.Add(this.elementHost);
            this.p_element.Location = new System.Drawing.Point(47, 41);
            this.p_element.Name = "p_element";
            this.p_element.Size = new System.Drawing.Size(582, 328);
            this.p_element.TabIndex = 4;
            // 
            // ClipEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_save;
            this.ClientSize = new System.Drawing.Size(690, 381);
            this.Controls.Add(this.p_element);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.b_clear);
            this.Controls.Add(this.t_description);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClipEditDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "片段编辑器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClipEditDialog_FormClosed);
            this.rightMenu.ResumeLayout(false);
            this.p_element.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_description;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Forms.Button b_clear;
        private System.Windows.Forms.Button b_save;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 快速输入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 替换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.Panel p_element;
    }
}
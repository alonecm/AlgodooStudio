
namespace AlgodooStudio.ASProject
{
    partial class TextEditWindow
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
            this.sp = new System.Windows.Forms.SplitContainer();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.statusBar = new System.Windows.Forms.ToolStrip();
            this.col = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.line = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pos = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectlength = new System.Windows.Forms.ToolStripLabel();
            this.scale = new System.Windows.Forms.ToolStripLabel();
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.快速输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).BeginInit();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.rightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            this.sp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.elementHost);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.statusBar);
            this.sp.Size = new System.Drawing.Size(800, 450);
            this.sp.SplitterDistance = 424;
            this.sp.SplitterWidth = 1;
            this.sp.TabIndex = 3;
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 0);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(800, 424);
            this.elementHost.TabIndex = 1;
            this.elementHost.Text = "Element";
            this.elementHost.Child = null;
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.col,
            this.toolStripSeparator1,
            this.line,
            this.toolStripSeparator2,
            this.pos,
            this.toolStripSeparator4,
            this.selectlength,
            this.scale});
            this.statusBar.Location = new System.Drawing.Point(0, 0);
            this.statusBar.Name = "statusBar";
            this.statusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.statusBar.Size = new System.Drawing.Size(800, 25);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusBar";
            // 
            // col
            // 
            this.col.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.col.Name = "col";
            this.col.Size = new System.Drawing.Size(33, 22);
            this.col.Text = "(col)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // line
            // 
            this.line.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(36, 22);
            this.line.Text = "(line)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pos
            // 
            this.pos.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pos.Name = "pos";
            this.pos.Size = new System.Drawing.Size(38, 22);
            this.pos.Text = "(pos)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // selectlength
            // 
            this.selectlength.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.selectlength.Name = "selectlength";
            this.selectlength.Size = new System.Drawing.Size(85, 22);
            this.selectlength.Text = "(selectlength)";
            this.selectlength.Visible = false;
            // 
            // scale
            // 
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(45, 22);
            this.scale.Text = "(scale)";
            // 
            // rightMenu
            // 
            this.rightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.toolStripSeparator3,
            this.快速输入ToolStripMenuItem});
            this.rightMenu.Name = "rightMenu";
            this.rightMenu.Size = new System.Drawing.Size(146, 98);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(142, 6);
            // 
            // 快速输入ToolStripMenuItem
            // 
            this.快速输入ToolStripMenuItem.Name = "快速输入ToolStripMenuItem";
            this.快速输入ToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.快速输入ToolStripMenuItem.Text = "快速输入...";
            this.快速输入ToolStripMenuItem.Click += new System.EventHandler(this.快速输入ToolStripMenuItem_Click);
            // 
            // TextEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.rightMenu;
            this.Controls.Add(this.sp);
            this.Name = "TextEditWindow";
            this.Text = "文本编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptEditor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScriptEditor_FormClosed);
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            this.sp.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).EndInit();
            this.sp.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.rightMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Forms.ToolStrip statusBar;
        private System.Windows.Forms.ToolStripLabel col;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel line;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel pos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel selectlength;
        private System.Windows.Forms.ToolStripLabel scale;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 快速输入ToolStripMenuItem;
    }
}
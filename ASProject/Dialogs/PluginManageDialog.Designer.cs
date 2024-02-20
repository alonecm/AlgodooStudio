namespace AlgodooStudio.ASProject.Dialogs
{
    partial class PluginManageDialog
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
            this.list_plugins = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sp = new System.Windows.Forms.SplitContainer();
            this.l_status = new System.Windows.Forms.Label();
            this.l_describe = new System.Windows.Forms.Label();
            this.b_close = new System.Windows.Forms.Button();
            this.b_enabled = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).BeginInit();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_plugins
            // 
            this.list_plugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.version,
            this.author});
            this.list_plugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_plugins.GridLines = true;
            this.list_plugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.list_plugins.HideSelection = false;
            this.list_plugins.LabelWrap = false;
            this.list_plugins.Location = new System.Drawing.Point(0, 0);
            this.list_plugins.MultiSelect = false;
            this.list_plugins.Name = "list_plugins";
            this.list_plugins.Size = new System.Drawing.Size(328, 292);
            this.list_plugins.TabIndex = 0;
            this.list_plugins.UseCompatibleStateImageBehavior = false;
            this.list_plugins.View = System.Windows.Forms.View.Details;
            this.list_plugins.SelectedIndexChanged += new System.EventHandler(this.list_plugins_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "插件名";
            this.name.Width = 120;
            // 
            // version
            // 
            this.version.Text = "插件版本";
            this.version.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.version.Width = 80;
            // 
            // author
            // 
            this.author.Text = "插件作者";
            this.author.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.author.Width = 140;
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.list_plugins);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.l_status);
            this.sp.Panel2.Controls.Add(this.l_describe);
            this.sp.Panel2.Controls.Add(this.b_close);
            this.sp.Panel2.Controls.Add(this.b_enabled);
            this.sp.Size = new System.Drawing.Size(406, 292);
            this.sp.SplitterDistance = 328;
            this.sp.SplitterWidth = 1;
            this.sp.TabIndex = 1;
            // 
            // l_status
            // 
            this.l_status.Location = new System.Drawing.Point(5, 133);
            this.l_status.Name = "l_status";
            this.l_status.Size = new System.Drawing.Size(68, 68);
            this.l_status.TabIndex = 1;
            this.l_status.Text = "未选定";
            this.l_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l_describe
            // 
            this.l_describe.Location = new System.Drawing.Point(5, 114);
            this.l_describe.Name = "l_describe";
            this.l_describe.Size = new System.Drawing.Size(68, 19);
            this.l_describe.TabIndex = 1;
            this.l_describe.Text = "插件状态";
            this.l_describe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // b_close
            // 
            this.b_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_close.Location = new System.Drawing.Point(5, 252);
            this.b_close.Name = "b_close";
            this.b_close.Size = new System.Drawing.Size(68, 37);
            this.b_close.TabIndex = 0;
            this.b_close.Text = "关闭";
            this.b_close.UseVisualStyleBackColor = true;
            this.b_close.Click += new System.EventHandler(this.b_close_Click);
            // 
            // b_enabled
            // 
            this.b_enabled.Location = new System.Drawing.Point(5, 8);
            this.b_enabled.Name = "b_enabled";
            this.b_enabled.Size = new System.Drawing.Size(68, 67);
            this.b_enabled.TabIndex = 0;
            this.b_enabled.Text = "未选定";
            this.b_enabled.UseVisualStyleBackColor = true;
            this.b_enabled.Click += new System.EventHandler(this.b_enabled_Click);
            // 
            // PluginManageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_close;
            this.ClientSize = new System.Drawing.Size(406, 292);
            this.Controls.Add(this.sp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManageDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件管理";
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sp)).EndInit();
            this.sp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView list_plugins;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Button b_enabled;
        private System.Windows.Forms.Label l_describe;
        private System.Windows.Forms.Label l_status;
        private System.Windows.Forms.Button b_close;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader version;
        private System.Windows.Forms.ColumnHeader author;
    }
}
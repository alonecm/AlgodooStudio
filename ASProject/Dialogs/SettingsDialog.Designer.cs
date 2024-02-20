namespace AlgodooStudio.ASProject.Dialogs
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.sp = new System.Windows.Forms.SplitContainer();
            this.settingTabs = new System.Windows.Forms.TabControl();
            this.tab_basic = new System.Windows.Forms.TabPage();
            this.g_layout = new System.Windows.Forms.GroupBox();
            this.check_closeSaveLayout = new System.Windows.Forms.CheckBox();
            this.g_path = new System.Windows.Forms.GroupBox();
            this.b_algodooPath = new System.Windows.Forms.Button();
            this.b_scenePath = new System.Windows.Forms.Button();
            this.l_algodooPath = new System.Windows.Forms.Label();
            this.l_scenePath = new System.Windows.Forms.Label();
            this.text_algodooPath = new System.Windows.Forms.TextBox();
            this.text_scenePath = new System.Windows.Forms.TextBox();
            this.apply = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sp)).BeginInit();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.settingTabs.SuspendLayout();
            this.tab_basic.SuspendLayout();
            this.g_layout.SuspendLayout();
            this.g_path.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            this.sp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.settingTabs);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.apply);
            this.sp.Panel2.Controls.Add(this.cancel);
            this.sp.Panel2.Controls.Add(this.ok);
            this.sp.Size = new System.Drawing.Size(365, 267);
            this.sp.SplitterDistance = 230;
            this.sp.TabIndex = 0;
            // 
            // settingTabs
            // 
            this.settingTabs.Controls.Add(this.tab_basic);
            this.settingTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingTabs.Location = new System.Drawing.Point(0, 0);
            this.settingTabs.Name = "settingTabs";
            this.settingTabs.SelectedIndex = 0;
            this.settingTabs.Size = new System.Drawing.Size(365, 230);
            this.settingTabs.TabIndex = 0;
            // 
            // tab_basic
            // 
            this.tab_basic.Controls.Add(this.g_layout);
            this.tab_basic.Controls.Add(this.g_path);
            this.tab_basic.Location = new System.Drawing.Point(4, 22);
            this.tab_basic.Name = "tab_basic";
            this.tab_basic.Padding = new System.Windows.Forms.Padding(3);
            this.tab_basic.Size = new System.Drawing.Size(357, 204);
            this.tab_basic.TabIndex = 0;
            this.tab_basic.Text = "基本";
            this.tab_basic.UseVisualStyleBackColor = true;
            // 
            // g_layout
            // 
            this.g_layout.Controls.Add(this.check_closeSaveLayout);
            this.g_layout.Location = new System.Drawing.Point(6, 126);
            this.g_layout.Name = "g_layout";
            this.g_layout.Size = new System.Drawing.Size(343, 68);
            this.g_layout.TabIndex = 1;
            this.g_layout.TabStop = false;
            this.g_layout.Text = "布局";
            // 
            // check_closeSaveLayout
            // 
            this.check_closeSaveLayout.AutoSize = true;
            this.check_closeSaveLayout.Location = new System.Drawing.Point(8, 30);
            this.check_closeSaveLayout.Name = "check_closeSaveLayout";
            this.check_closeSaveLayout.Size = new System.Drawing.Size(132, 16);
            this.check_closeSaveLayout.TabIndex = 1;
            this.check_closeSaveLayout.Text = "关闭时保存窗口布局";
            this.check_closeSaveLayout.UseVisualStyleBackColor = true;
            this.check_closeSaveLayout.CheckedChanged += new System.EventHandler(this.check_closeSaveLayout_CheckedChanged);
            // 
            // g_path
            // 
            this.g_path.Controls.Add(this.b_algodooPath);
            this.g_path.Controls.Add(this.b_scenePath);
            this.g_path.Controls.Add(this.l_algodooPath);
            this.g_path.Controls.Add(this.l_scenePath);
            this.g_path.Controls.Add(this.text_algodooPath);
            this.g_path.Controls.Add(this.text_scenePath);
            this.g_path.Location = new System.Drawing.Point(6, 6);
            this.g_path.Name = "g_path";
            this.g_path.Size = new System.Drawing.Size(343, 114);
            this.g_path.TabIndex = 0;
            this.g_path.TabStop = false;
            this.g_path.Text = "路径";
            // 
            // b_algodooPath
            // 
            this.b_algodooPath.Location = new System.Drawing.Point(306, 87);
            this.b_algodooPath.Name = "b_algodooPath";
            this.b_algodooPath.Size = new System.Drawing.Size(31, 21);
            this.b_algodooPath.TabIndex = 2;
            this.b_algodooPath.Text = "...";
            this.b_algodooPath.UseVisualStyleBackColor = true;
            this.b_algodooPath.Click += new System.EventHandler(this.b_algodooPath_Click);
            // 
            // b_scenePath
            // 
            this.b_scenePath.Location = new System.Drawing.Point(306, 32);
            this.b_scenePath.Name = "b_scenePath";
            this.b_scenePath.Size = new System.Drawing.Size(31, 21);
            this.b_scenePath.TabIndex = 2;
            this.b_scenePath.Text = "...";
            this.b_scenePath.UseVisualStyleBackColor = true;
            this.b_scenePath.Click += new System.EventHandler(this.b_scenePath_Click);
            // 
            // l_algodooPath
            // 
            this.l_algodooPath.AutoSize = true;
            this.l_algodooPath.Location = new System.Drawing.Point(6, 72);
            this.l_algodooPath.Name = "l_algodooPath";
            this.l_algodooPath.Size = new System.Drawing.Size(83, 12);
            this.l_algodooPath.TabIndex = 1;
            this.l_algodooPath.Text = "Algodoo根目录";
            // 
            // l_scenePath
            // 
            this.l_scenePath.AutoSize = true;
            this.l_scenePath.Location = new System.Drawing.Point(4, 17);
            this.l_scenePath.Name = "l_scenePath";
            this.l_scenePath.Size = new System.Drawing.Size(89, 12);
            this.l_scenePath.TabIndex = 1;
            this.l_scenePath.Text = "场景文件夹路径";
            // 
            // text_algodooPath
            // 
            this.text_algodooPath.Location = new System.Drawing.Point(6, 87);
            this.text_algodooPath.Name = "text_algodooPath";
            this.text_algodooPath.ReadOnly = true;
            this.text_algodooPath.Size = new System.Drawing.Size(294, 21);
            this.text_algodooPath.TabIndex = 0;
            // 
            // text_scenePath
            // 
            this.text_scenePath.Location = new System.Drawing.Point(6, 32);
            this.text_scenePath.Name = "text_scenePath";
            this.text_scenePath.ReadOnly = true;
            this.text_scenePath.Size = new System.Drawing.Size(294, 21);
            this.text_scenePath.TabIndex = 0;
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(285, 3);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(76, 27);
            this.apply.TabIndex = 5;
            this.apply.Text = "应用";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(203, 3);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(76, 27);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(121, 3);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(76, 27);
            this.ok.TabIndex = 4;
            this.ok.Text = "确认";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(365, 267);
            this.Controls.Add(this.sp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.Text = "设置";
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sp)).EndInit();
            this.sp.ResumeLayout(false);
            this.settingTabs.ResumeLayout(false);
            this.tab_basic.ResumeLayout(false);
            this.g_layout.ResumeLayout(false);
            this.g_layout.PerformLayout();
            this.g_path.ResumeLayout(false);
            this.g_path.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.TabControl settingTabs;
        private System.Windows.Forms.TabPage tab_basic;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.GroupBox g_path;
        private System.Windows.Forms.Button b_scenePath;
        private System.Windows.Forms.Label l_scenePath;
        private System.Windows.Forms.TextBox text_scenePath;
        private System.Windows.Forms.Button b_algodooPath;
        private System.Windows.Forms.Label l_algodooPath;
        private System.Windows.Forms.TextBox text_algodooPath;
        private System.Windows.Forms.GroupBox g_layout;
        private System.Windows.Forms.CheckBox check_closeSaveLayout;
    }
}

namespace AlgodooStudio.Window.Dialogs
{
    partial class AppSelectorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSelectorDialog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.appList = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.selectb = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.appList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.selectb);
            this.splitContainer1.Panel2.Controls.Add(this.cancel_button);
            this.splitContainer1.Panel2.Controls.Add(this.ok_button);
            this.splitContainer1.Size = new System.Drawing.Size(381, 377);
            this.splitContainer1.SplitterDistance = 280;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // appList
            // 
            this.appList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.appList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appList.FullRowSelect = true;
            this.appList.GridLines = true;
            this.appList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.appList.HideSelection = false;
            this.appList.LargeImageList = this.imageList;
            this.appList.Location = new System.Drawing.Point(0, 0);
            this.appList.MultiSelect = false;
            this.appList.Name = "appList";
            this.appList.Size = new System.Drawing.Size(280, 377);
            this.appList.SmallImageList = this.imageList;
            this.appList.TabIndex = 0;
            this.appList.UseCompatibleStateImageBehavior = false;
            this.appList.View = System.Windows.Forms.View.Details;
            this.appList.ItemActivate += new System.EventHandler(this.appList_ItemActivate);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // selectb
            // 
            this.selectb.Location = new System.Drawing.Point(5, 331);
            this.selectb.Name = "selectb";
            this.selectb.Size = new System.Drawing.Size(83, 34);
            this.selectb.TabIndex = 2;
            this.selectb.Text = "选择其他...";
            this.selectb.UseVisualStyleBackColor = true;
            this.selectb.Click += new System.EventHandler(this.selectb_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(5, 52);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(83, 34);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(5, 12);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(83, 34);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "确定";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 150;
            // 
            // AppSelectorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 377);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppSelectorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "应用选择器";
            this.Load += new System.EventHandler(this.AppSelectorDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView appList;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button selectb;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
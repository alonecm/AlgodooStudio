
namespace AlgodooStudio.Window.Dialogs
{
    partial class LayoutSelectDialog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.layoutList = new System.Windows.Forms.ListView();
            this.col_names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_isEquip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.close = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.equip = new System.Windows.Forms.Button();
            this.rename = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.layoutList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rename);
            this.splitContainer1.Panel2.Controls.Add(this.close);
            this.splitContainer1.Panel2.Controls.Add(this.ok);
            this.splitContainer1.Panel2.Controls.Add(this.delete);
            this.splitContainer1.Panel2.Controls.Add(this.equip);
            this.splitContainer1.Size = new System.Drawing.Size(349, 348);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // layoutList
            // 
            this.layoutList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layoutList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_names,
            this.col_isEquip});
            this.layoutList.FullRowSelect = true;
            this.layoutList.GridLines = true;
            this.layoutList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.layoutList.HideSelection = false;
            this.layoutList.LabelWrap = false;
            this.layoutList.Location = new System.Drawing.Point(5, 6);
            this.layoutList.MultiSelect = false;
            this.layoutList.Name = "layoutList";
            this.layoutList.Size = new System.Drawing.Size(233, 337);
            this.layoutList.TabIndex = 0;
            this.layoutList.UseCompatibleStateImageBehavior = false;
            this.layoutList.View = System.Windows.Forms.View.Details;
            this.layoutList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.layoutList_ItemSelectionChanged);
            // 
            // col_names
            // 
            this.col_names.Text = "布局名";
            this.col_names.Width = 160;
            // 
            // col_isEquip
            // 
            this.col_isEquip.Text = "是否启用";
            this.col_isEquip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_isEquip.Width = 70;
            // 
            // close
            // 
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Location = new System.Drawing.Point(5, 304);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(94, 32);
            this.close.TabIndex = 4;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(5, 88);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(94, 32);
            this.delete.TabIndex = 2;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // equip
            // 
            this.equip.Location = new System.Drawing.Point(5, 12);
            this.equip.Name = "equip";
            this.equip.Size = new System.Drawing.Size(94, 32);
            this.equip.TabIndex = 1;
            this.equip.Text = "应用";
            this.equip.UseVisualStyleBackColor = true;
            this.equip.Click += new System.EventHandler(this.equip_Click);
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(5, 50);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(94, 32);
            this.rename.TabIndex = 5;
            this.rename.Text = "重命名";
            this.rename.UseVisualStyleBackColor = true;
            this.rename.Click += new System.EventHandler(this.rename_Click);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(5, 266);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(94, 32);
            this.ok.TabIndex = 3;
            this.ok.Text = "确定";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // LayoutSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 348);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayoutSelectDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "布局管理";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView layoutList;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button equip;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ColumnHeader col_names;
        private System.Windows.Forms.ColumnHeader col_isEquip;
        private System.Windows.Forms.Button rename;
        private System.Windows.Forms.Button ok;
    }
}
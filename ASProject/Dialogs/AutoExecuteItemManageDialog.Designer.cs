namespace AlgodooStudio.ASProject.Dialogs
{
    partial class AutoExecuteItemManageDialog
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
            this.addItem = new System.Windows.Forms.Button();
            this.switchItemState = new System.Windows.Forms.Button();
            this.removeItem = new System.Windows.Forms.Button();
            this.itemList = new System.Windows.Forms.ListView();
            this.itemStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.return_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addItem
            // 
            this.addItem.Location = new System.Drawing.Point(475, 12);
            this.addItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(117, 54);
            this.addItem.TabIndex = 1;
            this.addItem.Text = "添加项";
            this.addItem.UseVisualStyleBackColor = true;
            this.addItem.Click += new System.EventHandler(this.addItem_Click);
            // 
            // switchItemState
            // 
            this.switchItemState.Location = new System.Drawing.Point(475, 72);
            this.switchItemState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.switchItemState.Name = "switchItemState";
            this.switchItemState.Size = new System.Drawing.Size(117, 54);
            this.switchItemState.TabIndex = 1;
            this.switchItemState.Text = "启用/禁用项";
            this.switchItemState.UseVisualStyleBackColor = true;
            this.switchItemState.Click += new System.EventHandler(this.switchItemState_Click);
            // 
            // removeItem
            // 
            this.removeItem.Location = new System.Drawing.Point(475, 132);
            this.removeItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(117, 54);
            this.removeItem.TabIndex = 1;
            this.removeItem.Text = "移除项";
            this.removeItem.UseVisualStyleBackColor = true;
            this.removeItem.Click += new System.EventHandler(this.removeItem_Click);
            // 
            // itemList
            // 
            this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemStatus,
            this.itemType,
            this.itemContent});
            this.itemList.FullRowSelect = true;
            this.itemList.GridLines = true;
            this.itemList.HideSelection = false;
            this.itemList.Location = new System.Drawing.Point(12, 12);
            this.itemList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(457, 426);
            this.itemList.TabIndex = 2;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.View = System.Windows.Forms.View.Details;
            this.itemList.ItemActivate += new System.EventHandler(this.itemList_ItemActivate);
            // 
            // itemStatus
            // 
            this.itemStatus.Text = "状态";
            // 
            // itemType
            // 
            this.itemType.Text = "类别";
            this.itemType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // itemContent
            // 
            this.itemContent.Text = "内容";
            this.itemContent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.itemContent.Width = 240;
            // 
            // return_button
            // 
            this.return_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.return_button.Location = new System.Drawing.Point(475, 384);
            this.return_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.return_button.Name = "return_button";
            this.return_button.Size = new System.Drawing.Size(117, 54);
            this.return_button.TabIndex = 1;
            this.return_button.Text = "返回";
            this.return_button.UseVisualStyleBackColor = true;
            // 
            // AutoExecuteItemManageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 450);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.return_button);
            this.Controls.Add(this.removeItem);
            this.Controls.Add(this.switchItemState);
            this.Controls.Add(this.addItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "AutoExecuteItemManageDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自启动项管理";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addItem;
        private System.Windows.Forms.Button switchItemState;
        private System.Windows.Forms.Button removeItem;
        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.ColumnHeader itemType;
        private System.Windows.Forms.ColumnHeader itemContent;
        private System.Windows.Forms.ColumnHeader itemStatus;
        private System.Windows.Forms.Button return_button;
    }
}
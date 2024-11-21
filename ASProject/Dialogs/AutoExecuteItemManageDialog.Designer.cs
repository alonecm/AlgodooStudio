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
            this.close = new System.Windows.Forms.Button();
            this.itemList = new System.Windows.Forms.ListView();
            this.itemIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // addItem
            // 
            this.addItem.Location = new System.Drawing.Point(475, 12);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(117, 54);
            this.addItem.TabIndex = 1;
            this.addItem.Text = "添加项";
            this.addItem.UseVisualStyleBackColor = true;
            // 
            // switchItemState
            // 
            this.switchItemState.Location = new System.Drawing.Point(475, 72);
            this.switchItemState.Name = "switchItemState";
            this.switchItemState.Size = new System.Drawing.Size(117, 54);
            this.switchItemState.TabIndex = 1;
            this.switchItemState.Text = "启用/禁用项";
            this.switchItemState.UseVisualStyleBackColor = true;
            // 
            // removeItem
            // 
            this.removeItem.Location = new System.Drawing.Point(475, 132);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(117, 54);
            this.removeItem.TabIndex = 1;
            this.removeItem.Text = "移除项";
            this.removeItem.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Location = new System.Drawing.Point(475, 384);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(117, 54);
            this.close.TabIndex = 1;
            this.close.Text = "返回";
            this.close.UseVisualStyleBackColor = true;
            // 
            // itemList
            // 
            this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemIndex,
            this.itemStatus,
            this.itemType,
            this.itemContent});
            this.itemList.FullRowSelect = true;
            this.itemList.GridLines = true;
            this.itemList.HideSelection = false;
            this.itemList.Location = new System.Drawing.Point(12, 12);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(457, 426);
            this.itemList.TabIndex = 2;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.View = System.Windows.Forms.View.Details;
            // 
            // itemIndex
            // 
            this.itemIndex.Text = "编号";
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
            // itemStatus
            // 
            this.itemStatus.Text = "状态";
            // 
            // AutoExecuteItemManageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 450);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.close);
            this.Controls.Add(this.removeItem);
            this.Controls.Add(this.switchItemState);
            this.Controls.Add(this.addItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.ColumnHeader itemIndex;
        private System.Windows.Forms.ColumnHeader itemType;
        private System.Windows.Forms.ColumnHeader itemContent;
        private System.Windows.Forms.ColumnHeader itemStatus;
    }
}
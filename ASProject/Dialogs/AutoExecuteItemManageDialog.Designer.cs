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
            this.itemStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // addItem
            // 
            this.addItem.Location = new System.Drawing.Point(356, 10);
            this.addItem.Margin = new System.Windows.Forms.Padding(2);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(88, 43);
            this.addItem.TabIndex = 1;
            this.addItem.Text = "添加项";
            this.addItem.UseVisualStyleBackColor = true;
            this.addItem.Click += new System.EventHandler(this.addItem_Click);
            // 
            // switchItemState
            // 
            this.switchItemState.Location = new System.Drawing.Point(356, 58);
            this.switchItemState.Margin = new System.Windows.Forms.Padding(2);
            this.switchItemState.Name = "switchItemState";
            this.switchItemState.Size = new System.Drawing.Size(88, 43);
            this.switchItemState.TabIndex = 1;
            this.switchItemState.Text = "启用/禁用项";
            this.switchItemState.UseVisualStyleBackColor = true;
            this.switchItemState.Click += new System.EventHandler(this.switchItemState_Click);
            // 
            // removeItem
            // 
            this.removeItem.Location = new System.Drawing.Point(356, 106);
            this.removeItem.Margin = new System.Windows.Forms.Padding(2);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(88, 43);
            this.removeItem.TabIndex = 1;
            this.removeItem.Text = "移除项";
            this.removeItem.UseVisualStyleBackColor = true;
            this.removeItem.Click += new System.EventHandler(this.removeItem_Click);
            // 
            // close
            // 
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Location = new System.Drawing.Point(356, 307);
            this.close.Margin = new System.Windows.Forms.Padding(2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(88, 43);
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
            this.itemList.Location = new System.Drawing.Point(9, 10);
            this.itemList.Margin = new System.Windows.Forms.Padding(2);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(344, 342);
            this.itemList.TabIndex = 2;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.View = System.Windows.Forms.View.Details;
            // 
            // itemIndex
            // 
            this.itemIndex.Text = "编号";
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
            // AutoExecuteItemManageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.close);
            this.Controls.Add(this.removeItem);
            this.Controls.Add(this.switchItemState);
            this.Controls.Add(this.addItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
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
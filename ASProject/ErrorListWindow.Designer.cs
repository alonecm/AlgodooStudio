namespace AlgodooStudio.ASProject
{
    partial class ErrorListWindow
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
            this.errorList = new System.Windows.Forms.ListView();
            this.errorNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorPosStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorPosEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // errorList
            // 
            this.errorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.errorNum,
            this.errorPosStart,
            this.errorPosEnd,
            this.errorDescription,
            this.errorFile});
            this.errorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.errorList.FullRowSelect = true;
            this.errorList.HideSelection = false;
            this.errorList.Location = new System.Drawing.Point(0, 0);
            this.errorList.MultiSelect = false;
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(588, 190);
            this.errorList.TabIndex = 0;
            this.errorList.UseCompatibleStateImageBehavior = false;
            this.errorList.View = System.Windows.Forms.View.Details;
            this.errorList.SelectedIndexChanged += new System.EventHandler(this.errorList_SelectedIndexChanged);
            // 
            // errorNum
            // 
            this.errorNum.Text = "问题编号";
            this.errorNum.Width = 85;
            // 
            // errorPosStart
            // 
            this.errorPosStart.Text = "起始位置";
            this.errorPosStart.Width = 69;
            // 
            // errorPosEnd
            // 
            this.errorPosEnd.Text = "结束位置";
            this.errorPosEnd.Width = 73;
            // 
            // errorDescription
            // 
            this.errorDescription.Text = "描述";
            this.errorDescription.Width = 181;
            // 
            // errorFile
            // 
            this.errorFile.Text = "文件";
            this.errorFile.Width = 195;
            // 
            // ErrorListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 190);
            this.Controls.Add(this.errorList);
            this.Name = "ErrorListWindow";
            this.ShowIcon = false;
            this.Text = "错误列表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView errorList;
        private System.Windows.Forms.ColumnHeader errorPosStart;
        private System.Windows.Forms.ColumnHeader errorDescription;
        private System.Windows.Forms.ColumnHeader errorFile;
        private System.Windows.Forms.ColumnHeader errorNum;
        private System.Windows.Forms.ColumnHeader errorPosEnd;
    }
}
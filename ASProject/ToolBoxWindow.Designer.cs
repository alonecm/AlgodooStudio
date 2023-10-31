
namespace AlgodooStudio.ASProject
{
    partial class ToolBoxWindow
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
            this.toolList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // toolList
            // 
            this.toolList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolList.FormattingEnabled = true;
            this.toolList.ItemHeight = 12;
            this.toolList.Location = new System.Drawing.Point(0, 0);
            this.toolList.Name = "toolList";
            this.toolList.Size = new System.Drawing.Size(245, 450);
            this.toolList.TabIndex = 0;
            this.toolList.Click += new System.EventHandler(this.toolList_Click);
            // 
            // ToolBoxWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 450);
            this.Controls.Add(this.toolList);
            this.Name = "ToolBoxWindow";
            this.Text = "工具箱";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox toolList;
    }
}
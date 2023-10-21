namespace AlgodooStudio.Window
{
    partial class ReplaceWindow
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
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.replaceBox = new System.Windows.Forms.TextBox();
            this.nextOne = new System.Windows.Forms.Button();
            this.lastOne = new System.Windows.Forms.Button();
            this.replace = new System.Windows.Forms.Button();
            this.searchLoop = new System.Windows.Forms.CheckBox();
            this.replaceAll = new System.Windows.Forms.Button();
            this.caseSensitive = new System.Windows.Forms.CheckBox();
            this.IndexDisplay = new System.Windows.Forms.Label();
            this.maxCount = new System.Windows.Forms.Label();
            this.countNumber = new System.Windows.Forms.Button();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(60, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(234, 21);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "替换为";
            // 
            // replaceBox
            // 
            this.replaceBox.Location = new System.Drawing.Point(60, 39);
            this.replaceBox.Name = "replaceBox";
            this.replaceBox.Size = new System.Drawing.Size(234, 21);
            this.replaceBox.TabIndex = 3;
            // 
            // nextOne
            // 
            this.nextOne.Location = new System.Drawing.Point(333, 12);
            this.nextOne.Name = "nextOne";
            this.nextOne.Size = new System.Drawing.Size(61, 21);
            this.nextOne.TabIndex = 4;
            this.nextOne.Text = "下一个";
            this.nextOne.UseVisualStyleBackColor = true;
            this.nextOne.Click += new System.EventHandler(this.nextOne_Click);
            // 
            // lastOne
            // 
            this.lastOne.Location = new System.Drawing.Point(300, 12);
            this.lastOne.Name = "lastOne";
            this.lastOne.Size = new System.Drawing.Size(27, 21);
            this.lastOne.TabIndex = 5;
            this.lastOne.Text = "▲";
            this.lastOne.UseVisualStyleBackColor = true;
            this.lastOne.Click += new System.EventHandler(this.lastOne_Click);
            // 
            // replace
            // 
            this.replace.Location = new System.Drawing.Point(300, 65);
            this.replace.Name = "replace";
            this.replace.Size = new System.Drawing.Size(94, 21);
            this.replace.TabIndex = 6;
            this.replace.Text = "替换";
            this.replace.UseVisualStyleBackColor = true;
            this.replace.Click += new System.EventHandler(this.replace_Click);
            // 
            // searchLoop
            // 
            this.searchLoop.AutoSize = true;
            this.searchLoop.Location = new System.Drawing.Point(132, 66);
            this.searchLoop.Name = "searchLoop";
            this.searchLoop.Size = new System.Drawing.Size(72, 16);
            this.searchLoop.TabIndex = 7;
            this.searchLoop.Text = "循环查找";
            this.searchLoop.UseVisualStyleBackColor = true;
            // 
            // replaceAll
            // 
            this.replaceAll.Location = new System.Drawing.Point(300, 90);
            this.replaceAll.Name = "replaceAll";
            this.replaceAll.Size = new System.Drawing.Size(94, 21);
            this.replaceAll.TabIndex = 8;
            this.replaceAll.Text = "全部替换";
            this.replaceAll.UseVisualStyleBackColor = true;
            this.replaceAll.Click += new System.EventHandler(this.replaceAll_Click);
            // 
            // caseSensitive
            // 
            this.caseSensitive.AutoSize = true;
            this.caseSensitive.Location = new System.Drawing.Point(210, 66);
            this.caseSensitive.Name = "caseSensitive";
            this.caseSensitive.Size = new System.Drawing.Size(84, 16);
            this.caseSensitive.TabIndex = 9;
            this.caseSensitive.Text = "区分大小写";
            this.caseSensitive.UseVisualStyleBackColor = true;
            this.caseSensitive.CheckedChanged += new System.EventHandler(this.caseSensitive_CheckedChanged);
            // 
            // IndexDisplay
            // 
            this.IndexDisplay.AutoSize = true;
            this.IndexDisplay.Location = new System.Drawing.Point(12, 71);
            this.IndexDisplay.Name = "IndexDisplay";
            this.IndexDisplay.Size = new System.Drawing.Size(47, 12);
            this.IndexDisplay.TabIndex = 10;
            this.IndexDisplay.Text = "(index)";
            // 
            // maxCount
            // 
            this.maxCount.AutoSize = true;
            this.maxCount.Location = new System.Drawing.Point(12, 94);
            this.maxCount.Name = "maxCount";
            this.maxCount.Size = new System.Drawing.Size(35, 12);
            this.maxCount.TabIndex = 11;
            this.maxCount.Text = "(max)";
            // 
            // countNumber
            // 
            this.countNumber.Location = new System.Drawing.Point(200, 90);
            this.countNumber.Name = "countNumber";
            this.countNumber.Size = new System.Drawing.Size(94, 21);
            this.countNumber.TabIndex = 12;
            this.countNumber.Text = "计数";
            this.countNumber.UseVisualStyleBackColor = true;
            this.countNumber.Click += new System.EventHandler(this.countNumber_Click);
            // 
            // tip
            // 
            this.tip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.tip.ToolTipTitle = "警告";
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(300, 39);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(94, 21);
            this.search.TabIndex = 13;
            this.search.Text = "查找";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // ReplaceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 122);
            this.Controls.Add(this.search);
            this.Controls.Add(this.countNumber);
            this.Controls.Add(this.maxCount);
            this.Controls.Add(this.IndexDisplay);
            this.Controls.Add(this.caseSensitive);
            this.Controls.Add(this.replaceAll);
            this.Controls.Add(this.searchLoop);
            this.Controls.Add(this.replace);
            this.Controls.Add(this.lastOne);
            this.Controls.Add(this.nextOne);
            this.Controls.Add(this.replaceBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找和替换";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox replaceBox;
        private System.Windows.Forms.Button nextOne;
        private System.Windows.Forms.Button lastOne;
        private System.Windows.Forms.Button replace;
        private System.Windows.Forms.CheckBox searchLoop;
        private System.Windows.Forms.Button replaceAll;
        private System.Windows.Forms.CheckBox caseSensitive;
        private System.Windows.Forms.Label IndexDisplay;
        private System.Windows.Forms.Label maxCount;
        private System.Windows.Forms.Button countNumber;
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.Button search;
    }
}
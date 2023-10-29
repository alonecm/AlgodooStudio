
namespace AlgodooStudio.ASProject.Dialogs
{
    partial class PropertiesSelect
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
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.selecter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.valueInput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.customProp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.colorSelect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(118, 85);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(34, 85);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 4;
            this.ok.Text = "确认";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // selecter
            // 
            this.selecter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selecter.FormattingEnabled = true;
            this.selecter.Items.AddRange(new object[] {
            "(自定义属性)"});
            this.selecter.Location = new System.Drawing.Point(56, 6);
            this.selecter.Name = "selecter";
            this.selecter.Size = new System.Drawing.Size(158, 20);
            this.selecter.TabIndex = 6;
            this.selecter.SelectionChangeCommitted += new System.EventHandler(this.selecter_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "值";
            // 
            // valueInput
            // 
            this.valueInput.Location = new System.Drawing.Point(56, 58);
            this.valueInput.Name = "valueInput";
            this.valueInput.Size = new System.Drawing.Size(105, 21);
            this.valueInput.TabIndex = 9;
            this.toolTip1.SetToolTip(this.valueInput, "注意！请按照属性对应的值类型进行输入！否则造成的后果请自负！");
            this.valueInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valueInput_KeyDown);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "警告";
            // 
            // customProp
            // 
            this.customProp.Enabled = false;
            this.customProp.Location = new System.Drawing.Point(56, 32);
            this.customProp.Name = "customProp";
            this.customProp.Size = new System.Drawing.Size(158, 21);
            this.customProp.TabIndex = 11;
            this.toolTip2.SetToolTip(this.customProp, "名称请用英文字母或下划线来做开头");
            this.toolTip1.SetToolTip(this.customProp, "注意！如果选择了自定义输入，请务必在其中输入名称！");
            this.customProp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customProp_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "自定义属性名";
            // 
            // toolTip2
            // 
            this.toolTip2.AutomaticDelay = 100;
            this.toolTip2.AutoPopDelay = 10000;
            this.toolTip2.InitialDelay = 100;
            this.toolTip2.IsBalloon = true;
            this.toolTip2.ReshowDelay = 20;
            this.toolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip2.ToolTipTitle = "提示";
            // 
            // colorSelect
            // 
            this.colorSelect.BackColor = System.Drawing.Color.White;
            this.colorSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorSelect.Location = new System.Drawing.Point(167, 58);
            this.colorSelect.Name = "colorSelect";
            this.colorSelect.Size = new System.Drawing.Size(47, 21);
            this.colorSelect.TabIndex = 12;
            this.colorSelect.Text = "选色...";
            this.colorSelect.Visible = false;
            this.colorSelect.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // PropertiesSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 117);
            this.Controls.Add(this.colorSelect);
            this.Controls.Add(this.customProp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.valueInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selecter);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性选择器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ComboBox selecter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox valueInput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox customProp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label colorSelect;
    }
}
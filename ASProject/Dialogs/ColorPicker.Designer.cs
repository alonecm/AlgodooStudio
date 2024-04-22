
namespace AlgodooStudio.ASProject.Dialogs
{
    partial class ColorPicker
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
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.g_color = new System.Windows.Forms.GroupBox();
            this.copyToClipbroad = new System.Windows.Forms.Button();
            this.l_pickingTip = new System.Windows.Forms.Label();
            this.t_rgb = new System.Windows.Forms.TextBox();
            this.l_rgb = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.l_pickingTip2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.g_color.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorBox
            // 
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(237, 30);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(64, 64);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // g_color
            // 
            this.g_color.Controls.Add(this.l_pickingTip2);
            this.g_color.Controls.Add(this.copyToClipbroad);
            this.g_color.Controls.Add(this.l_pickingTip);
            this.g_color.Controls.Add(this.t_rgb);
            this.g_color.Controls.Add(this.l_rgb);
            this.g_color.Controls.Add(this.colorBox);
            this.g_color.Location = new System.Drawing.Point(12, 12);
            this.g_color.Name = "g_color";
            this.g_color.Size = new System.Drawing.Size(321, 116);
            this.g_color.TabIndex = 1;
            this.g_color.TabStop = false;
            this.g_color.Text = "颜色";
            // 
            // copyToClipbroad
            // 
            this.copyToClipbroad.Location = new System.Drawing.Point(51, 78);
            this.copyToClipbroad.Name = "copyToClipbroad";
            this.copyToClipbroad.Size = new System.Drawing.Size(170, 23);
            this.copyToClipbroad.TabIndex = 4;
            this.copyToClipbroad.Text = "复制到剪贴板";
            this.copyToClipbroad.UseVisualStyleBackColor = true;
            this.copyToClipbroad.Click += new System.EventHandler(this.copyToClipbroad_Click);
            // 
            // l_pickingTip
            // 
            this.l_pickingTip.AutoSize = true;
            this.l_pickingTip.Location = new System.Drawing.Point(249, 14);
            this.l_pickingTip.Name = "l_pickingTip";
            this.l_pickingTip.Size = new System.Drawing.Size(41, 12);
            this.l_pickingTip.TabIndex = 3;
            this.l_pickingTip.Text = "取色中";
            this.l_pickingTip.Visible = false;
            // 
            // t_rgb
            // 
            this.t_rgb.Location = new System.Drawing.Point(51, 37);
            this.t_rgb.Name = "t_rgb";
            this.t_rgb.ReadOnly = true;
            this.t_rgb.Size = new System.Drawing.Size(170, 21);
            this.t_rgb.TabIndex = 2;
            // 
            // l_rgb
            // 
            this.l_rgb.AutoSize = true;
            this.l_rgb.Location = new System.Drawing.Point(16, 40);
            this.l_rgb.Name = "l_rgb";
            this.l_rgb.Size = new System.Drawing.Size(29, 12);
            this.l_rgb.TabIndex = 1;
            this.l_rgb.Text = "RGBA";
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // l_pickingTip2
            // 
            this.l_pickingTip2.AutoSize = true;
            this.l_pickingTip2.Location = new System.Drawing.Point(234, 97);
            this.l_pickingTip2.Name = "l_pickingTip2";
            this.l_pickingTip2.Size = new System.Drawing.Size(71, 12);
            this.l_pickingTip2.TabIndex = 5;
            this.l_pickingTip2.Text = "按C停止取色";
            this.l_pickingTip2.Visible = false;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 140);
            this.Controls.Add(this.g_color);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPicker";
            this.ShowIcon = false;
            this.Text = "取色器";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.ColorPicker_Deactivate);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColorPicker_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.g_color.ResumeLayout(false);
            this.g_color.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox colorBox;
        private System.Windows.Forms.GroupBox g_color;
        private System.Windows.Forms.Label l_rgb;
        private System.Windows.Forms.TextBox t_rgb;
        private System.Windows.Forms.Label l_pickingTip;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button copyToClipbroad;
        private System.Windows.Forms.Label l_pickingTip2;
    }
}
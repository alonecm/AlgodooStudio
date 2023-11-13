
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
            this.l_rgb = new System.Windows.Forms.Label();
            this.t_rgb = new System.Windows.Forms.TextBox();
            this.l_pickingTip = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.copyToClipbroad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.g_color.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorBox
            // 
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(238, 37);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(64, 64);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // g_color
            // 
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
            // l_rgb
            // 
            this.l_rgb.AutoSize = true;
            this.l_rgb.Location = new System.Drawing.Point(16, 40);
            this.l_rgb.Name = "l_rgb";
            this.l_rgb.Size = new System.Drawing.Size(29, 12);
            this.l_rgb.TabIndex = 1;
            this.l_rgb.Text = "RGBA";
            // 
            // t_rgb
            // 
            this.t_rgb.Location = new System.Drawing.Point(51, 37);
            this.t_rgb.Name = "t_rgb";
            this.t_rgb.ReadOnly = true;
            this.t_rgb.Size = new System.Drawing.Size(170, 21);
            this.t_rgb.TabIndex = 2;
            // 
            // l_pickingTip
            // 
            this.l_pickingTip.AutoSize = true;
            this.l_pickingTip.Location = new System.Drawing.Point(251, 17);
            this.l_pickingTip.Name = "l_pickingTip";
            this.l_pickingTip.Size = new System.Drawing.Size(41, 12);
            this.l_pickingTip.TabIndex = 3;
            this.l_pickingTip.Text = "取色中";
            this.l_pickingTip.Visible = false;
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
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
    }
}
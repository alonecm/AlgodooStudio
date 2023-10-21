
using Dex.Canvas;

namespace AlgodooStudio.Window
{
    partial class SceneEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneEditorWindow));
            this.sceneEditor1 = new Dex.Canvas.SceneEditor();
            ((System.ComponentModel.ISupportInitialize)(this.sceneEditor1)).BeginInit();
            this.SuspendLayout();
            // 
            // sceneEditor1
            // 
            this.sceneEditor1.CurrentLayer = null;
            this.sceneEditor1.CurrentScene = null;
            this.sceneEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneEditor1.Location = new System.Drawing.Point(0, 0);
            this.sceneEditor1.MousePos = ((System.Drawing.PointF)(resources.GetObject("sceneEditor1.MousePos")));
            this.sceneEditor1.Name = "sceneEditor1";
            this.sceneEditor1.Size = new System.Drawing.Size(1216, 540);
            this.sceneEditor1.TabIndex = 0;
            this.sceneEditor1.TabStop = false;
            // 
            // SceneEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 540);
            this.Controls.Add(this.sceneEditor1);
            this.Name = "SceneEditorWindow";
            this.Text = "SceneEditorWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SceneEditorWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.sceneEditor1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SceneEditor sceneEditor1;
    }
}
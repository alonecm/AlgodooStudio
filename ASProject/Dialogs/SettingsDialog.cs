using AlgodooStudio.ASProject.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    internal partial class SettingsDialog : Form
    {
        internal SettingsDialog()
        {
            InitializeComponent();
            //显示当前设置
            text_scenePath.Text = Program.Setting.ScenePath;
            text_algodooPath.Text = Program.Setting.AlgodooPath;
            check_closeSaveLayout.Checked = Program.Setting.IsSavingLayout;
        }

        /// <summary>
        /// 应用设置
        /// </summary>
        private void Apply()
        {
            Program.Setting.ScenePath = text_scenePath.Text;
            Program.Setting.AlgodooPath = text_algodooPath.Text;
            Program.Setting.IsSavingLayout = check_closeSaveLayout.Checked;
            Program.SaveSetting();
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void b_scenePath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog()==DialogResult.OK)
                {
                    text_scenePath.Text = fbd.SelectedPath;
                    if (Program.Setting.ScenePath != text_scenePath.Text)
                    {
                        apply.Enabled = true;
                    }
                }
            }
        }

        private void b_algodooPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    text_algodooPath.Text = fbd.SelectedPath;
                    if (Program.Setting.AlgodooPath != text_algodooPath.Text)
                    {
                        apply.Enabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_Click(object sender, EventArgs e)
        {
            Apply();
            apply.Enabled = false;
        }

        private void check_closeSaveLayout_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.Setting.IsSavingLayout != check_closeSaveLayout.Checked)
            {
                apply.Enabled = true;
            }
        }
    }
}

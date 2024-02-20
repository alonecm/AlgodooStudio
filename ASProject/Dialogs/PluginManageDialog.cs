using AlgodooStudio.ASProject.Support;
using AlgodooStudio.PluginSystem;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    internal partial class PluginManageDialog : Form
    {
        private Plugin selectedPlugin;

        internal PluginManageDialog()
        {
            InitializeComponent();

            //读取经过检查的插件信息
            foreach (var plugin in Loader.LoadedPlugins.Values)
            {
                list_plugins.Items.Add(new ListViewItem(new string[] { plugin.Name, plugin.Version, plugin.Author }));
            }
        }


        private void b_enabled_Click(object sender, EventArgs e)
        {
            if (selectedPlugin != null)
            {
                if (selectedPlugin.IsEnabled)
                {
                    selectedPlugin.SetDisable();
                    b_enabled.Text = "启用";
                    l_status.Text = "已禁用";
                    l_status.ForeColor = Color.Red;
                }
                else
                {
                    selectedPlugin.SetEnable();
                    b_enabled.Text = "禁用";
                    l_status.Text = "已启用";
                    l_status.ForeColor = Color.Green;
                }
            }
        }

        private void list_plugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_plugins.SelectedItems.Count > 0)
            {
                selectedPlugin = Loader.LoadedPlugins[list_plugins.SelectedItems[0].SubItems[0].Text];
                if (selectedPlugin.IsEnabled)
                {
                    b_enabled.Text = "禁用";
                    l_status.Text = "已启用";
                    l_status.ForeColor = Color.Green;
                }
                else
                {
                    b_enabled.Text = "启用";
                    l_status.Text = "已禁用";
                    l_status.ForeColor = Color.Red;
                }
            }
        }

        private void b_close_Click(object sender, EventArgs e)
        {
            Loader.WriteManageFile();
            MBox.Showlog("更改将在下次启动时改变");
        }
    }
}

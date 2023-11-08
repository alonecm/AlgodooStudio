using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    public partial class AutoExecuteManageWindow : DockContent
    {
        public AutoExecuteManageWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启用VS渲染
        /// </summary>
        /// <param name="version"></param>
        /// <param name="theme"></param>
        public void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            vsToolStripExtender.SetStyle(toolbar, version, theme);
        }

        public void LoadFile()
        {
            var path = Program.Setting.AlgodooPath + "\\autoexec.cfg";
            if (!File.Exists(path))
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 属性窗口
    /// </summary>
    public partial class PropertyWindow : DockContent
    {
        
        public PropertyWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设定被编辑对象
        /// </summary>
        /// <param name="obj"></param>
        public void SetEdit(object obj)
        {
            this.propertyGrid.SelectedObject=obj;
        }
    }
}

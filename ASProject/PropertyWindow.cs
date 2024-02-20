﻿using System;
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
    internal partial class PropertyWindow : DockContent
    {

        internal PropertyWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设定被编辑对象
        /// </summary>
        /// <param name="obj"></param>
        public void SetEdit(object obj)
        {
            this.propertyGrid.SelectedObject = obj;
            this.propertyGrid.Refresh();
        }
        /// <summary>
        /// 设定被编辑对象们的共有属性
        /// </summary>
        /// <param name="obj"></param>
        public void SetEdit(object[] objs)
        {
            this.propertyGrid.SelectedObjects = objs;
            this.propertyGrid.Refresh();
        }
    }
}

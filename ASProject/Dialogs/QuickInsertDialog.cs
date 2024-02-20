using AlgodooStudio.ASProject;
using AlgodooStudio.ASProject.Support;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    public partial class QuickInsertDialog : Form
    {
        /// <summary>
        /// 合并后的脚本代码
        /// </summary>
        private string scriptContent = "";

        /// <summary>
        /// 合并后的脚本代码
        /// </summary>
        public string Content { get => scriptContent; }

        public QuickInsertDialog()
        {
            InitializeComponent();
        }

        #region 项目操作
        /// <summary>
        /// 非几何体类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeSelector3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (typeSelector3.SelectedItem.ToString() != "")
            {
                propsList2.Enabled = addProp2.Enabled = true;
            }
            else
            {
                propsList2.Enabled = addProp2.Enabled = false;
            }
            propsList2.Items.Clear();
        }

        /// <summary>
        /// (基本)类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeSelector1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (typeSelector1.SelectedItem.ToString() != "")
            {
                density.Enabled = restitution.Enabled = attraction.Enabled = friction.Enabled = objColor.Enabled = colorSelect.Enabled = true;
            }
            else
            {
                density.Enabled = restitution.Enabled = attraction.Enabled = friction.Enabled = objColor.Enabled = colorSelect.Enabled = false;
            }
        }

        /// <summary>
        /// (高级)类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeSelector2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (typeSelector2.SelectedItem.ToString() != "")
            {
                propsList1.Enabled = addProp1.Enabled = true;
            }
            else
            {
                propsList1.Enabled = addProp1.Enabled = false;
            }
            propsList1.Items.Clear();
        }

        /// <summary>
        /// 脚本类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeSelector4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //判断是否可以启动名称框
            if (typeSelector4.SelectedItem.ToString() != "")
            {
                scriptName.Enabled = true;
            }
            else
            {
                scriptName.Enabled = false;
            }

            //判断要启用的类型
            switch (typeSelector4.SelectedItem.ToString())
            {
                case "函数":
                    groupBox7.Enabled = true;
                    groupBox8.Enabled = groupBox6.Enabled = false;
                    break;

                case "计时器":
                    groupBox8.Enabled = true;
                    groupBox7.Enabled = groupBox6.Enabled = false;
                    break;

                case "类":
                    groupBox6.Enabled = true;
                    groupBox7.Enabled = groupBox8.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 选择基本项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox4.Enabled = true;
                groupBox5.Enabled = false;
            }
        }

        /// <summary>
        /// 选择高级项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox5.Enabled = true;
                groupBox4.Enabled = false;
            }
        }

        #endregion 项目操作

        #region 内容控制

        /// <summary>
        /// 选择颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                objColor.BackColor = cd.Color;
            }
            cd.Dispose();
            cd = null;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProp1_Click(object sender, EventArgs e)
        {
            PropertiesSelect ps = new PropertiesSelect(SelectionTransform(typeSelector2.Text));
            if (ps.ShowDialog() == DialogResult.OK)
            {
                propsList1.Items.Add(new ListViewItem(new string[] { ps.PropName, ps.Value }));
            }
            ps.Dispose();
            ps = null;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProp2_Click(object sender, EventArgs e)
        {
            PropertiesSelect ps = new PropertiesSelect(SelectionTransform(typeSelector3.Text));
            if (ps.ShowDialog() == DialogResult.OK)
            {
                propsList2.Items.Add(new ListViewItem(new string[] { ps.PropName, ps.Value }));
            }
            ps.Dispose();
            ps = null;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProp3_Click(object sender, EventArgs e)
        {
            PropertiesSelect ps = new PropertiesSelect(EntityType.Null);
            if (ps.ShowDialog() == DialogResult.OK)
            {
                propsList3.Items.Add(new ListViewItem(new string[] { ps.PropName, ps.Value }));
            }
            ps.Dispose();
            ps = null;
        }

        /// <summary>
        /// 移除所选属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeProp1_Click(object sender, EventArgs e)
        {
            propsList1.Items.Remove(propsList1.SelectedItems[0]);
            //propsList.Items.Remove(selectedItem);
        }

        /// <summary>
        /// 移除所选属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeProp2_Click(object sender, EventArgs e)
        {
            propsList2.Items.Remove(propsList2.SelectedItems[0]);
        }

        /// <summary>
        /// 移除所选属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeProp3_Click(object sender, EventArgs e)
        {
            propsList3.Items.Remove(propsList3.SelectedItems[0]);
        }

        /// <summary>
        /// 选中某项时启动删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propsList1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                removeProp1.Enabled = true;
            }
            else
            {
                removeProp1.Enabled = false;
            }
        }

        /// <summary>
        /// 选中某项时启动删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propsList2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                removeProp2.Enabled = true;
            }
            else
            {
                removeProp2.Enabled = false;
            }
        }

        /// <summary>
        /// 选中某项时启动删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propsList3_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                removeProp3.Enabled = true;
            }
            else
            {
                removeProp3.Enabled = false;
            }
        }

        /// <summary>
        /// 字符串转实体类型枚举
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>实体类型枚举</returns>
        private EntityType SelectionTransform(string str)
        {
            switch (str)
            {
                case "圆形":
                    return EntityType.Circle;

                case "矩形":
                    return EntityType.Box;

                case "多边形":
                    return EntityType.Polygon;

                case "平面":
                    return EntityType.Plane;

                case "激光笔":
                    return EntityType.LaserPen;

                case "轨迹追踪器":
                    return EntityType.Pen;

                case "轴承":
                    return EntityType.Hinge;

                case "固定点":
                    return EntityType.Fixjoint;

                case "推进器":
                    return EntityType.Thruster;

                default:
                    return EntityType.Null;
            }
        }

        /// <summary>
        /// 方法含参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void needParam_CheckedChanged(object sender, EventArgs e)
        {
            if (needParam.Checked)
            {
                paramNumber.Enabled = true;
            }
            else
            {
                paramNumber.Enabled = false;
            }
        }

        #endregion 内容控制

        #region 大类操作

        /// <summary>
        /// 选择几何体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geomEntitySelect_CheckedChanged(object sender, EventArgs e)
        {
            if (geomEntitySelect.Enabled)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
        }

        /// <summary>
        /// 选择非几何体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nongeomEntitySelect_CheckedChanged(object sender, EventArgs e)
        {
            if (geomEntitySelect.Enabled)
            {
                groupBox2.Enabled = true;
                groupBox1.Enabled = false;
            }
        }

        #endregion 大类操作

        #region 脚本生成控制

        /// <summary>
        /// 确认并输出代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, EventArgs e)
        {
            OutText();
        }

        /// <summary>
        /// 输出控制，并检查是否是以当前页的指定内容输出的
        /// </summary>
        private void OutText()
        {
            //检查是否以实体输出
            if (tabControl1.SelectedTab.Text == "实体")
            {
                //检查是否以几何实体形式输出
                if (geomEntitySelect.Checked)
                {
                    //是否是以基础部分输出
                    if (radioButton1.Checked)
                    {
                        //判断选择的类型是否存在
                        if (typeSelector1.Text != "")
                        {
                            Color c = objColor.BackColor;
                            scriptContent =
                            "Scene.add" + SelectionTransform(typeSelector1.Text).ToString() + "({\n" +
                               "\tdensity:=" + density.Text + ";\n" +
                               "\trestitution:=" + restitution.Text + ";\n" +
                               "\tattraction:=" + attraction.Text + ";\n" +
                               "\tfriction:=" + friction.Text + ";\n" +
                               "\tcolor:=[" + c.R / 255f + "," + c.G / 255f + "," + c.B / 255f + "," + c.A / 255f + "];\n" +
                               "});";
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MBox.ShowInfo("请选择一个实体类型！");
                        }
                    }
                    else
                    {
                        //判断选择的类型是否存在
                        if (typeSelector2.Text != "")
                        {
                            scriptContent = "Scene.add" + SelectionTransform(typeSelector2.Text).ToString() + "({\n";
                            foreach (ListViewItem item in propsList1.Items)
                            {
                                scriptContent += "\t" + item.SubItems[0].Text + ":=" + item.SubItems[1].Text + ";\n";
                            }
                            scriptContent += "});";
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MBox.ShowInfo("请选择一个实体类型！");
                        }
                    }
                }
                else
                {
                    //判断选择的类型是否存在
                    if (typeSelector3.Text != "")
                    {
                        scriptContent = "Scene.add" + SelectionTransform(typeSelector3.Text).ToString() + "({\n";
                        foreach (ListViewItem item in propsList2.Items)
                        {
                            scriptContent += "\t" + item.SubItems[0].Text + ":=" + item.SubItems[1].Text + ";\n";
                        }
                        scriptContent += "});";
                        //MBox.ShowInfo(scriptContent);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MBox.ShowInfo("请选择一个实体类型！");
                    }
                }
            }
            else
            {
                //检查名称是否合理
                if (IsNameValid(scriptName.Text))
                {
                    //根据选项生成相关代码
                    switch (typeSelector4.Text)
                    {
                        case "函数":
                            GenFunction();
                            break;

                        case "计时器":
                            GenTimer();
                            break;

                        case "类":
                            GenClass();
                            break;
                    }
                    DialogResult = DialogResult.OK;
                    //自动拷贝到剪切板中
                    //Clipboard.SetText(scriptContent);
                }
                else
                {
                    MBox.ShowInfo("请输入正确的名称！");
                }
            }
        }

        /// <summary>
        /// 命名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsNameValid(string name)
        {
            //名称是否为空
            if (name != "")
            {
                if (Regex.Match(name[0].ToString(), "^[A-Za-z]+$").Success || name[0] == '_')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 生成函数
        /// </summary>
        private void GenFunction()
        {
            //检查是否启用参数
            if (needParam.Checked)
            {
                scriptContent = scriptName.Text + "=(";
                for (int i = 1; i <= paramNumber.Value; i++)
                {
                    if (i != paramNumber.Value)
                    {
                        scriptContent += "p" + i + ",";
                    }
                    else
                    {
                        scriptContent += "p" + i;
                    }
                }
                scriptContent += ")=>{\n\n};";
            }
            else
            {
                scriptContent = scriptName.Text + "={\n\n};";
            }
        }

        /// <summary>
        /// 生成计时器
        /// </summary>
        private void GenTimer()
        {
            //是否启用重复计时
            if (repeatTime.Checked)
            {
                scriptContent = $"{scriptName.Text}=0;\n{scriptName.Text}>0?" +
                    "{" +
                    "\n\t{需要定时执行的代码};" +
                    $"\n\t{scriptName.Text}={scriptName.Text}-1/sim.frequency;" +
                    "\n}:{" +
                    "\n\t{需要定时执行的代码};" +
                    $"\n\t{scriptName.Text}={timerCount.Value};" +
                    "\n};";
            }
            else
            {
                scriptContent =
                    $"{scriptName.Text}={timerCount.Value};" +
                    $"\n{scriptName.Text}>0?" +
                    "\n{" +
                    "\n\t{需要定时执行的代码};" +
                    $"\n\t{scriptName.Text}={scriptName.Text}-1/sim.frequency;" +
                    "\n}:{" +
                    "\n\t{需要定时执行的代码};" +
                    $"\n\t{scriptName.Text}=0;" +
                    "\n};";
            }
        }

        /// <summary>
        /// 生成类模型
        /// </summary>
        private void GenClass()
        {
            scriptContent = $"{scriptName.Text}=" +
                "{\n\tbase = alloc;\n";
            foreach (ListViewItem item in propsList3.Items)
            {
                scriptContent += "\tbase." + item.SubItems[0].Text + "=" + item.SubItems[1].Text + ";\n";
            }
            scriptContent += "\tbase->{\n\n\t};" +
                "\n\tbase;\n};";
        }

        #endregion 脚本生成控制
    }
}
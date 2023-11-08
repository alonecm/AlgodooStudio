using AlgodooStudio.ASProject.Support;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Dialogs
{
    /// <summary>
    /// 属性选择器
    /// </summary>
    public partial class PropertiesSelect : Form
    {
        private string propName = "";
        private string value = "";

        /// <summary>
        /// 创建一个属性选择器
        /// </summary>
        /// <param name="type">需要赋值的实体类型</param>
        internal PropertiesSelect(EntityType type)
        {
            InitializeComponent();
            switch (type)
            {
                case EntityType.Circle:
                    selecter.Items.AddRange(Props.circle);
                    break;

                case EntityType.Box:
                    selecter.Items.AddRange(Props.box);
                    break;

                case EntityType.Polygon:
                    selecter.Items.AddRange(Props.polygon);
                    break;

                case EntityType.Plane:
                    selecter.Items.AddRange(Props.plane);
                    break;

                case EntityType.LaserPen:
                    selecter.Items.AddRange(Props.laser);
                    break;

                case EntityType.Pen:
                    selecter.Items.AddRange(Props.pen);
                    break;

                case EntityType.Hinge:
                    selecter.Items.AddRange(Props.hinge);
                    break;

                case EntityType.Fixjoint:
                    selecter.Items.AddRange(Props.fixjoint);
                    break;

                case EntityType.Thruster:
                    selecter.Items.AddRange(Props.thruster);
                    break;

                case EntityType.Water:
                    selecter.Items.AddRange(Props.water);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 属性的值
        /// </summary>
        internal string Value { get => value; }

        /// <summary>
        /// 属性的名称
        /// </summary>
        internal string PropName { get => propName; }

        /// <summary>
        /// 确认键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, EventArgs e)
        {
            OutText();
        }

        /// <summary>
        /// 输出值
        /// </summary>
        private void OutText()
        {
            valueInput.Enabled = false;//防止误输入
            //检查是否选择属性了
            if (selecter.Text != "")
            {
                //检查是否启用了自定义命名
                if (customProp.Enabled)
                {
                    //检查是否有命名完成
                    if (customProp.Text != "")
                    {
                        //检查命名是否合法
                        if (IsNameValid(customProp.Text))
                        {
                            propName = customProp.Text;
                            value = valueInput.Text == "" ? "null" : valueInput.Text;
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MBox.ShowInfo("请输入正确的属性名！");
                            valueInput.Enabled = true;
                        }
                    }
                    else
                    {
                        MBox.ShowInfo("请输入一个属性名！");
                        valueInput.Enabled = true;
                    }
                }
                else
                {
                    propName = selecter.Text;
                    value = valueInput.Text;
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MBox.ShowInfo("请选择一个属性值！");
                valueInput.Enabled = true;
            }
        }

        /// <summary>
        /// 命名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsNameValid(string name)
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

        /// <summary>
        /// 检查是否选择了自定义属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (selecter.SelectedItem.ToString() == "(自定义属性)")
            {
                customProp.Enabled = true;
                colorSelect.Visible = true;
            }
            else
            {
                if (selecter.SelectedItem.ToString() == "color")
                {
                    colorSelect.Visible = true;
                    customProp.Enabled = false;
                }
                else
                {
                    colorSelect.Visible = false;
                    customProp.Enabled = false;
                }
            }
        }

        private void customProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OutText();
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void valueInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OutText();
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// 颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                colorSelect.BackColor = cd.Color;
                valueInput.Text = "[" + cd.Color.R / 255f + "," + cd.Color.B / 255f + "," + cd.Color.G / 255f + "," + cd.Color.A / 255f + "]";
            }
            cd.Dispose();
            cd = null;
        }
    }
}
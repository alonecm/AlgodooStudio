﻿using System;
using System.Windows.Forms;

namespace AlgodooStudio.Window.Dialogs
{
    public partial class TextGetDialog : Form
    {
        private bool isExtraMode = false;
        private string inputText;

        /// <summary>
        /// 是否启用额外模式（启用就是开启全局模式的复选框）
        /// </summary>
        public bool IsExtraMode
        {
            get
            {
                return isExtraMode;
            }
            set
            {
                isExtraMode = value;
                if (isExtraMode)
                {
                    allGet.Visible = true;
                }
                else
                {
                    allGet.Visible = false;
                }
            }
        }

        /// <summary>
        /// 是否启用全局获取模式（修改文件名的时候反馈扩展名也一起修改）
        /// </summary>
        public bool IsGetAll
        {
            get
            {
                return allGet.Checked;
            }
        }

        /// <summary>
        /// 是否启用文件名有效性检查
        /// </summary>
        public bool IsNameValidCheck { get; set; } = false;

        /// <summary>
        /// 输入的文字
        /// </summary>
        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                input.Text = value;
                input.SelectAll();
            }
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title { get => Text; set => Text = value; }

        public TextGetDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, EventArgs e)
        {
            //如果使用了文字检查
            if (IsNameValidCheck)
            {
                if (input.Text != "")
                {
                    if (IsFileNameValid(input.Text))
                    {
                        inputText = input.Text;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MBox.ShowWarning("名称不合法！");
                    }
                }
                else
                {
                    MBox.ShowWarning("输入不能为空！");
                }
            }
            else
            {
                inputText = input.Text;
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 判定文件名是否合法
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns>是否合法</returns>
        private bool IsFileNameValid(string name)
        {
            bool isFilename = true;
            string[] errorStr = new string[] { "/", "\\", ":", ",", "*", "?", "\"", "<", ">", "|" };

            if (string.IsNullOrEmpty(name))
            {
                isFilename = false;
            }
            else
            {
                for (int i = 0; i < errorStr.Length; i++)
                {
                    if (name.Contains(errorStr[i]))
                    {
                        isFilename = false;
                        break;
                    }
                }
            }
            return isFilename;
        }
    }
}
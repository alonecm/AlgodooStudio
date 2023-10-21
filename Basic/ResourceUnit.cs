using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using AlgodooStudio.Forms.Dialogs;
using AlgodooStudio.Forms.Style;

namespace AlgodooStudio.Basic
{
    /// <summary>
    /// 资源管理单元
    /// </summary>
    internal partial class ResourceUnit : UserControl
    {
        /// <summary>
        /// 字符串合并器
        /// </summary>
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 文件
        /// </summary>
        private FileInfo file;
        /// <summary>
        /// 文件夹
        /// </summary>
        private DirectoryInfo directory;
        /// <summary>
        /// 当前是否为垂直 (图标在上名称在下) 格式
        /// </summary>
        private bool isVerticalDisplay = true;


        /// <summary>
        /// 当前是否为垂直 (图标在上名称在下) 格式
        /// </summary>
        internal bool IsVerticalDisplay { get => isVerticalDisplay; }


        /// <summary>
        /// 创建资源管理单元
        /// </summary>
        /// <param name="filePath"></param>
        internal ResourceUnit(string filePath)
        {
            InitializeComponent();
            SetResource(filePath, 60);
            //注册事件
            this.icon.GotFocus += Icon_GotFocus;
            this.icon.LostFocus += Icon_LostFocus;
            //初始化右键菜单主题
            this.icon.ContextMenuStrip.Renderer = ThemeToolStripRenderer.GetRenderer();
        }
        /// <summary>
        /// 创建资源管理单元
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isVertical">是否需要垂直显示</param>
        internal ResourceUnit(string filePath, bool isVertical)
        {
            InitializeComponent();
            SetResource(filePath, 60);
            //切换显示方式
            ChangeDisplay(isVertical);
            //注册事件
            this.icon.GotFocus += Icon_GotFocus;
            this.icon.LostFocus += Icon_LostFocus;
            //初始化右键菜单主题
            this.icon.ContextMenuStrip.Renderer = ThemeToolStripRenderer.GetRenderer();
        }
        /// <summary>
        /// 改变显示方式
        /// </summary>
        /// <param name="isVertical">是否需要垂直显示</param>
        private void ChangeDisplay(bool isVertical)
        {
            //如果当前状态与设定状态不同则准备切换
            if (isVertical!=isVerticalDisplay)
            {
                if (isVertical)
                {
                    this.splitContainer1.Orientation = Orientation.Horizontal;
                    SetUnitSize(60);
                }
                else
                {
                    this.splitContainer1.Orientation = Orientation.Vertical;
                    SetUnitSize(20);
                }
                isVerticalDisplay = isVertical;
            }
        }
        /// <summary>
        /// 设定文件资源(有需要修改的部分)
        /// </summary>
        /// <param name="path">指定文件</param>
        /// <param name="size">图标大小</param>
        private void SetResource(string path, int size)
        {
            //如果此文件是文件夹
            if (File.GetAttributes(path) == FileAttributes.Directory)
            {
                directory = new DirectoryInfo(path);
                this.fileName.Text = directory.Name;
                SetUnitSize(size);
                this.icon.Image = Properties.Resources.folder;
            }
            else
            {
                file = new FileInfo(path);
                this.fileName.Text = file.Name;
                SetUnitSize(size);
                this.icon.Image = Icon.ExtractAssociatedIcon(path).ToBitmap();
            }
        }
        /// <summary>
        /// 设定单元大小
        /// </summary>
        /// <param name="size"></param>
        private void SetUnitSize(int size)
        {
            if (this.splitContainer1.Orientation == Orientation.Horizontal)
            {
                this.Height = size + 30;//框架高度
                this.Width = size;//框架宽度
                this.splitContainer1.SplitterDistance = this.Width;//图标长度
            }
            else
            {
                this.Height = size;//框架高度
                this.Width = size + 90;//框架宽度
                this.splitContainer1.SplitterDistance = this.Height;//图标长度
            }
        }
        /// <summary>
        /// 设定文字颜色
        /// </summary>
        private void SetTextColor()
        {
            this.fileName.ForeColor = ColorTool.GetContrastColor(this.Parent.BackColor);
        }


        /// <summary>
        /// 显示暗色框
        /// </summary>
        private void ShowDarkBox()
        {
            if (BorderStyle == BorderStyle.None)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
            }
            this.BackColor = ColorTool.OffsetColor(this.BackColor, 10);
        }
        /// <summary>
        /// 隐藏暗色框
        /// </summary>
        private void HideDarkBox()
        {
            if (BorderStyle!=BorderStyle.Fixed3D)
            {
                this.BorderStyle = BorderStyle.None;
            }
            this.BackColor = ColorTool.OffsetColor(this.BackColor, -10);
        }

        #region 事件
        /// <summary>
        /// 控件被加载时的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceUnit_Load(object sender, EventArgs e)
        {
            SetTextColor();
        }
        /// <summary>
        /// 鼠标移入PB上显示暗色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icon_MouseEnter(object sender, EventArgs e)
        {
            ShowDarkBox();
        }
        /// <summary>
        /// 鼠标移出PB上隐藏暗色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icon_MouseLeave(object sender, EventArgs e)
        {
            HideDarkBox();
        }
        /// <summary>
        /// 鼠标移入FN上激活暗色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileName_MouseEnter(object sender, EventArgs e)
        {
            ShowDarkBox();
        }
        /// <summary>
        /// 鼠标移出FN上隐藏暗色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileName_MouseLeave(object sender, EventArgs e)
        {
            HideDarkBox();
        }
        /// <summary>
        /// 点击其他图标后可以隐藏亮色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_LostFocus(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = ColorTool.OffsetColor(this.BackColor, -40);
        }
        /// <summary>
        /// 点击图标后可以激活亮色框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_GotFocus(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = ColorTool.OffsetColor(this.BackColor, 40);
        }
        /// <summary>
        /// 单击以便使获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icon_Click(object sender, EventArgs e)
        {
            this.icon.Focus();
        }
        /// <summary>
        /// 单击名称以获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileName_Click(object sender, EventArgs e)
        {
            if (!isVerticalDisplay)
            {
                this.icon.Focus();
            }
        }
        /// <summary>
        /// 文件信息获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewInfo_Popup(object sender, PopupEventArgs e)
        {
            sb.Clear();
            if (file!=null)
            {
                sb.Append("文件名：");
                sb.Append(file.Name);
                sb.Append("\n创建时间：");
                sb.Append(file.CreationTime);
                if (file.Extension == ".phz")
                {
                    sb.Append("\n作者：");
                    //sb.Append(author);
                }
            }
            else
            {
                sb.Append("文件夹名：");
                sb.Append(directory.Name);
                sb.Append("\n创建时间：");
                sb.Append(directory.CreationTime);
            }
            viewInfo.SetToolTip(e.AssociatedControl, sb.ToString());
        }
        /// <summary>
        ///  打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Parent.Cursor = Cursors.WaitCursor;
            //测试解压功能
            Thread th = new Thread(()=> { rrr(file.FullName); });
            th.Start();
        }
        #endregion

        Phun.PhunPackage pp;
        /// <summary>
        /// 测试子线程解压功能
        /// </summary>
        /// <param name="file"></param>
        private void rrr(string file)
        {
            pp = PhunSaveTools.DeCompress(file);
            this.BeginInvoke((Action)delegate
            {
                icon.Image = pp.Thumb;
                this.Parent.Cursor = Cursors.Default;
            });
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Parent.Cursor = Cursors.WaitCursor;
            //测试打包功能
            Thread th = new Thread(() => {
                ddd(file.DirectoryName+"\\"+Path.GetFileNameWithoutExtension(file.Name)+".zip"); 
            });
            th.Start();
        }
        /// <summary>
        /// 测试子线程解压功能
        /// </summary>
        /// <param name="file"></param>
        private void ddd(string file)
        {
            PhunSaveTools.Compress(pp,file);
            this.BeginInvoke((Action)delegate
            {
                icon.Image = pp.Thumb;
                this.Parent.Cursor = Cursors.Default;
            });
        }
    }
}

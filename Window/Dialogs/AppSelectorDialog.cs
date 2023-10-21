using AlgodooStudio.Base;
using System;
using System.Windows.Forms;

namespace AlgodooStudio.Window.Dialogs
{
    /// <summary>
    /// 启动应用选择器，创建时应提供<see cref="App"/>[]数据源
    /// </summary>
    public partial class AppSelectorDialog : Form
    {
        /// <summary>
        /// 应用数据源
        /// </summary>
        public Container<App> LoadSource { get; set; }

        private string appPath;

        /// <summary>
        /// 用于外部启动的应用程序路径
        /// </summary>
        public string AppPath { get => appPath; }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title { get => Text; set => Text = value; }

        public AppSelectorDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动时加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppSelectorDialog_Load(object sender, EventArgs e)
        {
            //加载数据
            foreach (var item in LoadSource)
            {
                AddToList(item);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (appList.SelectedItems.Count > 0)
            {
                appPath = appList.SelectedItems[0].Tag.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// 选择其他应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectb_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择指定的应用程序用于打开";
            ofd.Filter = "可执行文件|*.exe";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Add(new App(ofd.FileName));
                //导入到设置中
                Setting.executeApp = LoadSource;
                //保存设置
                Program.SettingSave();
            }
            ofd.Dispose();
            ofd = null;
        }

        /// <summary>
        /// 向数据源和列表中添加应用程序
        /// </summary>
        /// <param name="content"></param>
        /// <param name="size"></param>
        public void Add(App content)
        {
            //从图标集中检查是否存在多余的内容
            if (!imageList.Images.ContainsKey(content.Path))
            {
                //不存在则添加
                LoadSource.Add(content);
                //添加到列表中
                AddToList(content);
            }
        }

        private void AddToList(App content)
        {
            //如果不存在图标则通过导入路径来检查一遍
            if (content.Icon == null)
            {
                content.Path = content.Path;
            }
            //添加图片
            imageList.Images.Add(content.Path, content.Icon);
            //添加项
            ListViewItem l = new ListViewItem("");
            l.ImageKey = content.Path;
            l.Tag = content.Path;
            l.SubItems.Add(content.Name);
            l.SubItems.Add(content.Path);
            appList.Items.Add(l);
        }

        private void appList_ItemActivate(object sender, EventArgs e)
        {
            appPath = appList.SelectedItems[0].Tag.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
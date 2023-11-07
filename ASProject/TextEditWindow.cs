using AlgodooStudio.ASProject.Dialogs;
using AlgodooStudio.ASProject.Interface;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Search;
using PhunSharp;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    public partial class TextEditWindow : DockContent, IEditable, IReplaceable, ISearchable, ISaveable
    {
        /// <summary>
        /// Avalon文字编辑器
        /// </summary>
        private TextEditor editor = new TextEditor();

        /// <summary>
        /// 是否是文本加载阶段
        /// </summary>
        private bool IsTextLoad;

        /// <summary>
        /// 是否被保存了
        /// </summary>
        private bool IsSaved = true;

        /// <summary>
        /// 文件路径
        /// </summary>
        private string filepath;

        /// <summary>
        /// 文件名
        /// </summary>
        private string fileName;

        /// <summary>
        /// 只读阅读模式
        /// </summary>
        private bool readOnly;

        /// <summary>
        /// 提醒器是否已经显示
        /// </summary>
        private bool IsReminderShow;

        /// <summary>
        /// 提词器
        /// </summary>
        private CompletionWindow reminder;

        /// <summary>
        /// 折叠栏
        /// </summary>
        private FoldingManager foldingManager;

        /// <summary>
        /// Xml折叠策略
        /// </summary>
        private BraceFoldingStrategy foldingStrategy = new BraceFoldingStrategy();

        /// <summary>
        /// 搜索面板
        /// </summary>
        private SearchPanel searchPanel;

        /// <summary>
        /// 查找和替换窗口
        /// </summary>
        private ReplaceWindow replaceWindow;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get => filepath;
            set
            {
                filepath = value;
                fileName = Path.GetFileName(value);

                //证明此处是文字加载阶段
                IsTextLoad = true;
                switch (Path.GetExtension(value))
                {
                    case ".phz":
                        editor.Text = ArchiveTools.GetPhnContent(ArchiveTools.DeCompress(value));
                        break;
                    default:
                        editor.Load(value);
                        break;
                }
                IsTextLoad = false;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                this.readOnly = value;
                this.editor.IsReadOnly = this.readOnly;
                this.快速输入ToolStripMenuItem.Enabled = !this.readOnly;
                SetTitle(this.fileName, false);
            }
        }

        internal TextEditWindow()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 通过标题，内容和读写方式创建文字编辑窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="readOnly"></param>
        internal TextEditWindow(string title = "", string filepath = "", string content = "", bool readOnly = true)
        {
            InitializeComponent();
            Initialize();
            this.fileName = title;//设置文件名
            this.filepath = filepath;//设置文件路径
            this.IsTextLoad = true;
            this.editor.Text = content;//设置内容
            this.IsTextLoad = false;
            this.ReadOnly = readOnly;//设置只读
            //设置标题
            SetTitle(title, false);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            //选中块设定为非圆角
            editor.TextArea.SelectionCornerRadius = 0;
            //允许复制一整行
            editor.Options.CutCopyWholeLine = true;
            //高亮当前行
            editor.Options.HighlightCurrentLine = true;
            //允许滚动到文档下方
            editor.Options.AllowScrollBelowDocument = true;
            //设置字体
            editor.FontFamily = new System.Windows.Media.FontFamily("Console");
            //设置滚动条
            editor.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            editor.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            ////设置颜色
            //BackColor = Setting.theme.BackColor2;
            ////编辑部份背景色
            //editor.Background = new SolidColorBrush(NormalColorToMediaColor(BackColor));
            ////编辑部份前景色
            //editor.Foreground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.VarNameColor));
            ////列号前景色
            //editor.LineNumbersForeground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.StringColor));
            ////当前行背景色
            //editor.TextArea.TextView.CurrentLineBackground = new SolidColorBrush(NormalColorToMediaColor(System.Drawing.Color.FromArgb(50, Setting.theme.KeywordsColor)));
            //editor.TextArea.TextView.CurrentLineBorder = new System.Windows.Media.Pen(editor.TextArea.TextView.CurrentLineBackground, 2);
            //创建状态栏渲染器
            //statusBar.Renderer = StatusBarRenderer.GetRenderer();
            //显示行号
            editor.ShowLineNumbers = true;
            //为编辑器创建事件
            editor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
            editor.TextArea.MouseWheel += TextArea_MouseWheel;
            editor.TextArea.SelectionChanged += TextArea_SelectionChanged;
            editor.TextArea.TextEntered += TextArea_TextEntered;
            editor.TextChanged += Editor_TextChanged;
            //将元素主机作为编辑器创建
            elementHost.Child = editor;
            //初始化底部显示
            DisplayLineAndColAndPos();
            //初始化缩放
            scale.Text = (20 / 20 * 100) + "%";
            editor.FontSize = 20;
            //初始化折叠栏
            foldingManager = FoldingManager.Install(editor.TextArea);
            foldingStrategy.UpdateFoldings(foldingManager, editor.Document);
            //初始化搜索框
            searchPanel = SearchPanel.Install(editor.TextArea);
            ////右键菜单渲染器
            //rightMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
        }

        #region 编辑器

        /// <summary>
        /// 文字变动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_TextChanged(object sender, EventArgs e)
        {
            foldingStrategy.UpdateFoldings(foldingManager, editor.Document);
            //如果当前是文字加载阶段则无需变动
            if (IsTextLoad)
            {
                //变动标题以便于驱动保存功能
                SetTitle(fileName, false);
            }
            else
            {
                //变动标题以便于驱动保存功能
                SetTitle(fileName, !(IsSaved = false));
            }
        }

        /// <summary>
        /// 文字输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_TextEntered(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!readOnly)
            {
                //提词器未显示时检查内容
                if (!IsReminderShow)
                {
                    //如果内容是字母或数字则创建提词器并标注已经启动
                    if (Regex.IsMatch(e.Text, @"\w|\p{P}"))
                    {
                        CreateReminder();
                    }
                }
                else
                {
                    //提词器如果已经显示则检查是否是空格是则关闭提词器并标注已关闭
                    //如果内容是空格则关闭提词器并标注已经关闭
                    if (Regex.IsMatch(e.Text, @"\s"))
                    {
                        //这个样子只是把之前的给替换掉
                        reminder.CompletionList.SelectItem(editor.Document.GetText(reminder.StartOffset, reminder.TextArea.Caret.Offset - reminder.StartOffset));
                        reminder.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 所选内容变动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_SelectionChanged(object sender, EventArgs e)
        {
            //选中后显示选中长度
            if (editor.SelectionLength > 0)
            {
                selectlength.Text = "SelectLen: " + editor.SelectionLength;
                selectlength.Visible = true;
                toolStripSeparator4.Visible = true;
            }
            else
            {
                selectlength.Visible = false;
                toolStripSeparator4.Visible = false;
            }
        }

        /// <summary>
        /// 鼠标滚轮缩放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                //放大
                if (e.Delta > 0)
                {
                    if (editor.FontSize < 200)
                    {
                        editor.FontSize *= 1.1;
                    }
                }
                else
                {
                    //缩小
                    if (editor.FontSize > 10)
                    {
                        editor.FontSize /= 1.1;
                    }
                }
                scale.Text = (int)(editor.FontSize / 20 * 100) + "%";
            }
        }

        /// <summary>
        /// 光标位置更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            DisplayLineAndColAndPos();
        }

        #endregion 编辑器

        #region 窗体

        /// <summary>
        /// 窗体关闭时的检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未保存且窗体名后有星花则保存
            if (!IsSaved && Text.EndsWith("*"))
            {
                DialogResult dr = MBox.ShowWarningYesNoCancel("文件尚未保存，是否保存？");
                switch (dr)
                {
                    case DialogResult.Yes:
                        //如果保存失败则取消关闭窗口
                        if (_Save() == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 窗体关闭后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            editor = null;
            reminder = null;
            foldingManager.Clear();
            foldingManager = null;
            searchPanel = null;
            if (replaceWindow != null)
            {
                replaceWindow.Dispose();
                replaceWindow = null;
            }
            GC.Collect(3);
        }

        #endregion 窗体

        #region 提词器

        private void Reminder_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            IsReminderShow = true;
        }

        private void Reminder_Closed(object sender, EventArgs e)
        {
            IsReminderShow = false;
        }

        #endregion 提词器

        #region 右键菜单

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void 快速输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (QuickInsertDialog dialog = new QuickInsertDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Insert(dialog.Content);
                }
            }
        }
        #endregion 右键菜单

        #region 其他方法

        /// <summary>
        /// 创建提词器
        /// </summary>
        private void CreateReminder()
        {
            reminder = new CompletionWindow(editor.TextArea);
            //reminder.Background = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.BackColor1));
            //reminder.Foreground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.VarNameColor));
            reminder.Closed += Reminder_Closed;
            reminder.Loaded += Reminder_Loaded;
            AddReminderItem(reminder);
            reminder.Show();
        }

        /// <summary>
        /// 通过给定的字符串搜索并添加提示条目
        /// </summary>
        /// <param name="reminder">提词器</param>
        private void AddReminderItem(CompletionWindow reminder)
        {
            //清空
            reminder.CompletionList.CompletionData.Clear();
            //拆分关键词列表
            string[] words = ReminderList.keywords.Split(',');
            //匹配
            foreach (var item in words)
            {
                reminder.CompletionList.CompletionData.Add(new ReminderItem(item));
            }
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="needToSave">需要被保存</param>
        private void SetTitle(string title, bool needToSave)
        {
            if (!this.ReadOnly)
            {
                if (needToSave)
                {
                    this.Text = title + "*";
                }
                else
                {
                    this.Text = title;
                }
                return;
            }
            this.Text = "仅查看：" + title;
        }

        /// <summary>
        /// 将普通颜色转换至媒体色彩
        /// </summary>
        /// <param name="color">普通颜色</param>
        /// <returns>媒体色彩</returns>
        private System.Windows.Media.Color NormalColorToMediaColor(System.Drawing.Color color)
        {
            var media = new System.Windows.Media.Color();
            media.A = color.A;
            media.R = color.R;
            media.G = color.G;
            media.B = color.B;
            return media;
        }

        /// <summary>
        /// 显示行列和全文位置
        /// </summary>
        /// <param name="index">位置索引</param>
        /// <returns>位置索引</returns>
        private int DisplayLineAndColAndPos()
        {
            pos.Text = "Pos: " + editor.TextArea.Caret.Offset;
            line.Text = "Line: " + editor.TextArea.Caret.Line;
            col.Text = "Col: " + editor.TextArea.Caret.Column;
            return editor.TextArea.Caret.Offset;
        }

        /// <summary>
        /// 仅在此窗口内使用
        /// </summary>
        /// <returns>成功返回OK</returns>
        private DialogResult _Save()
        {
            //如果没保存
            if (!IsSaved)
            {
                //并且文件还不存在则使用另存为
                if (!File.Exists(filepath))
                {
                    return _SaveAs();
                }
                else
                {
                    //文件存在则
                    editor.Save(filepath);
                    SetTitle(this.fileName, !(IsSaved = true));
                    return DialogResult.OK;
                }
            }
            else
            {
                return DialogResult.Cancel;
            }
        }

        /// <summary>
        /// 仅在此窗口内使用
        /// </summary>
        /// <returns>成功返回OK</returns>
        private DialogResult _SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = fileName + " 另存为";
            sfd.Filter = "Thyme脚本|*.thm|cfg配置文件|*.cfg|其他文件|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                editor.Save(sfd.FileName);
                SetTitle(Path.GetFileName(sfd.FileName), !(IsSaved = true));
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        private void _Search()
        {
            searchPanel.Open();
        }

        /// <summary>
        /// 替换
        /// </summary>
        private void _Replace()
        {
            //检查窗口是否未创建或已释放，是则创建窗口并显示
            if (replaceWindow == null || replaceWindow.IsDisposed)
            {
                replaceWindow = new ReplaceWindow(editor.TextArea);
                replaceWindow.Show();
            }
        }

        #endregion 其他方法

        public void Save()
        {
            _Save();
        }

        public void SaveAs()
        {
            _SaveAs();
        }

        public void Copy()
        {
            editor.Copy();
        }

        public void Cut()
        {
            editor.Cut();
        }

        public void Paste()
        {
            editor.Paste();
        }

        public void Undo()
        {
            editor.Undo();
        }

        public void Redo()
        {
            editor.Redo();
        }

        public void Delete()
        {
            editor.Delete();
        }

        public void SelectAll()
        {
            editor.SelectAll();
        }

        public void Replace()
        {
            _Replace();
        }

        public void Search()
        {
            _Search();
        }

        /// <summary>
        /// 获取当前窗口的保存字符串
        /// </summary>
        /// <returns></returns>
        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + FilePath + "," + readOnly;
        }

        public void Insert(string str, int pos = -1)
        {
            if (pos == -1)
            {
                editor.Document.Insert(editor.CaretOffset, str);
            }
            else
            {
                editor.Document.Insert(pos, str);
            }
        }
    }
}
using AlgodooStudio.Interface;
using AlgodooStudio.Interface.Edit;
using AlgodooStudio.Window.Dialogs;
using AlgodooStudio.Window.Style;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Media;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Window
{
    /// <summary>
    /// 脚本编辑器，用于编辑文本文件
    /// </summary>
    internal partial class ScriptEditorWindow : DockContent, ISave, IManage, IOperateControl, ISelectAll, ISearch, IReplace, IQuickInsertable
    {
        /// <summary>
        /// Avalon文字编辑器
        /// </summary>
        private TextEditor editor = new TextEditor();

        /// <summary>
        /// 是否被保存了
        /// </summary>
        private bool IsSaved = false;

        /// <summary>
        /// 文件路径
        /// </summary>
        private string filePath;

        /// <summary>
        /// 文件名
        /// </summary>
        private string fileName;

        /// <summary>
        /// 提醒器是否已经显示
        /// </summary>
        private bool IsReminderShow = false;

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

        internal ScriptEditorWindow()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 通过文本创建一个文本编辑器
        /// </summary>
        /// <param name="content">文本</param>
        internal ScriptEditorWindow(string content)
        {
            InitializeComponent();
            Initialize();
            editor.Text = content;
        }

        /// <summary>
        /// 通过文本创建一个文本编辑器
        /// </summary>
        /// <param name="content">文本</param>
        /// <param name="cap">标签名</param>
        internal ScriptEditorWindow(string content, string cap)
        {
            InitializeComponent();
            Initialize();
            editor.Text = content;
            this.Text = cap;
            this.fileName = cap;
        }

        /// <summary>
        /// 通过文件创建一个文本编辑器
        /// </summary>
        /// <param name="file">文本文件</param>
        internal ScriptEditorWindow(FileInfo file)
        {
            InitializeComponent();
            Initialize();
            StringBuilder sb = new StringBuilder();
            fileName = this.Text = file.Name;
            filePath = file.FullName;
            using (StreamReader sr = new StreamReader(file.FullName))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine() + "\r\n");
                }
            }
            sb.Append("\r\n");//多向下一行以便于看见下方
            editor.Text = sb.ToString();
            SetTitle(fileName, !(IsSaved = true));
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
            //变动标题以便于驱动保存功能
            SetTitle(fileName, !(IsSaved = false));
        }

        /// <summary>
        /// 文字输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TextArea_TextEntered(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //提词器未显示时检查内容
            if (!IsReminderShow)
            {
                //如果内容是字母或数字则创建提词器并标注已经启动
                if (Regex.IsMatch(e.Text, @"\p{P}|\w"))
                {
                    CreateReminder();
                }
            }
            else
            {
                //提词器如果已经显示则检查是否是空格是则关闭提词器并标注已关闭
                //如果内容是空格则关闭提词器并标注已经关闭
                if (Regex.IsMatch(e.Text, @"\p{P}|\s"))
                {
                    //这个样子只是把之前的给替换掉
                    reminder.CompletionList.SelectItem(editor.Document.GetText(reminder.StartOffset, reminder.TextArea.Caret.Offset - reminder.StartOffset));
                    reminder.Close();
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

        #endregion 右键菜单

        #region 其他方法

        /// <summary>
        /// 编辑器初始化
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
            //设置颜色
            BackColor = Setting.theme.BackColor2;
            //编辑部份背景色
            editor.Background = new SolidColorBrush(NormalColorToMediaColor(BackColor));
            //编辑部份前景色
            editor.Foreground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.VarNameColor));
            //列号前景色
            editor.LineNumbersForeground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.StringColor));
            //当前行背景色
            editor.TextArea.TextView.CurrentLineBackground = new SolidColorBrush(NormalColorToMediaColor(System.Drawing.Color.FromArgb(50, Setting.theme.KeywordsColor)));
            editor.TextArea.TextView.CurrentLineBorder = new System.Windows.Media.Pen(editor.TextArea.TextView.CurrentLineBackground, 2);
            //创建状态栏渲染器
            statusBar.Renderer = StatusBarRenderer.GetRenderer();
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
            //右键菜单渲染器
            rightMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
        }

        /// <summary>
        /// 创建提词器
        /// </summary>
        private void CreateReminder()
        {
            reminder = new CompletionWindow(editor.TextArea);
            reminder.Background = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.BackColor1));
            reminder.Foreground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.VarNameColor));
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
        /// <param name="special">是否需要特殊符号</param>
        private void SetTitle(string title, bool special)
        {
            if (special)
            {
                this.Text = title + "*";
            }
            else
            {
                this.Text = title;
            }
        }

        /// <summary>
        /// 将普通颜色转换至媒体色彩
        /// </summary>
        /// <param name="color">普通颜色</param>
        /// <returns>媒体色彩</returns>
        private Color NormalColorToMediaColor(System.Drawing.Color color)
        {
            Color media = new Color();
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
        /// 保存文件到指定路径
        /// </summary>
        /// <param name="path">文件的路径</param>
        private void SaveFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(editor.Text);
            }
        }

        /// <summary>
        /// 仅在此窗口内使用
        /// </summary>
        /// <returns>成功返回OK</returns>
        private DialogResult _Save()
        {
            if (!IsSaved)
            {
                if (!File.Exists(this.filePath))
                {
                    return _SaveAs();
                }
                else
                {
                    SaveFile(this.filePath);
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
                SaveFile(sfd.FileName);
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
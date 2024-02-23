using AlgodooStudio.ASProject.Support;
using Dex.IO;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Xml.Linq;

namespace AlgodooStudio.ASProject.Dialogs
{
    internal partial class ClipEditDialog : Form
    {
        /// <summary>
        /// 编辑中的片段
        /// </summary>
        private ScriptClip editingScriptClip;
        /// <summary>
        /// Avalon文字编辑器
        /// </summary>
        private TextEditor _editor = new TextEditor();
        /// <summary>
        /// 提词器
        /// </summary>
        private CompletionWindow _reminder;
        /// <summary>
        /// 折叠栏
        /// </summary>
        private FoldingManager _foldingManager;
        /// <summary>
        /// Xml折叠策略
        /// </summary>
        private BraceFoldingStrategy _foldingStrategy = new BraceFoldingStrategy();
        /// <summary>
        /// 搜索面板
        /// </summary>
        private SearchPanel _searchPanel;
        /// <summary>
        /// 查找和替换窗口
        /// </summary>
        private ReplaceWindow _replaceWindow;


        /// <summary>
        /// 片段位置
        /// </summary>
        private string path;
        /// <summary>
        /// 提醒器是否已经显示
        /// </summary>
        private bool _isReminderShow;


        /// <summary>
        /// 初始文字
        /// </summary>
        public string InitialText
        {
            set
            {
                _editor.Text = value;
            }
        }
        /// <summary>
        /// 指示当前是编辑状态
        /// </summary>
        public bool IsEdit { get; set; } = false;
        /// <summary>
        /// 编辑中的片段
        /// </summary>
        public ScriptClip EditingScriptClip { get => editingScriptClip; }
      

        /// <summary>
        /// 从路径打开切片
        /// </summary>
        /// <param name="path"></param>
        public ClipEditDialog(string path)
        {
            InitializeComponent();
            Initialize(path);
        }


        private void Initialize(string path)
        {
            //记录片段路径
            this.path = path;
            //选中块设定为非圆角
            _editor.TextArea.SelectionCornerRadius = 0;
            //允许复制一整行
            _editor.Options.CutCopyWholeLine = true;
            //高亮当前行
            _editor.Options.HighlightCurrentLine = true;
            //允许滚动到文档下方
            _editor.Options.AllowScrollBelowDocument = true;
            //设置字体
            _editor.FontFamily = new System.Windows.Media.FontFamily("Console");
            //设置滚动条
            _editor.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            _editor.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;

            //显示行号
            _editor.ShowLineNumbers = true;
            //为编辑器创建事件
            _editor.TextArea.MouseWheel += TextArea_MouseWheel;
            _editor.TextArea.TextEntered += TextArea_TextEntered;
            _editor.TextChanged += Editor_TextChanged;
            //将元素主机作为编辑器创建
            elementHost.Child = _editor;
            _editor.FontSize = 20;
            //初始化折叠栏
            _foldingManager = FoldingManager.Install(_editor.TextArea);
            _foldingStrategy.UpdateFoldings(_foldingManager, _editor.Document);
            //初始化搜索框
            _searchPanel = SearchPanel.Install(_editor.TextArea);

            //初始化片段
            if (File.Exists(path))
            {
                editingScriptClip = SimpleSerializer.GetObjectFromBinaryFile<ScriptClip>(path);
            }
            else
            {
                editingScriptClip = new ScriptClip();
            }

            //提取片段信息
            this.Text = "编辑:" + Path.GetFileNameWithoutExtension(path);
            t_description.Text = editingScriptClip.Description;
            _editor.Text = editingScriptClip.Script;
            t_description.Focus();//聚焦
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
        /// 创建提词器
        /// </summary>
        private void CreateReminder()
        {
            _reminder = new CompletionWindow(_editor.TextArea);
            _reminder.Closed += Reminder_Closed;
            _reminder.Loaded += Reminder_Loaded;
            AddReminderItem(_reminder);
            _reminder.Show();
        }
        /// <summary>
        /// 在指定位置插入内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pos"></param>
        public void Insert(string str, int pos = -1)
        {
            if (pos == -1)
            {
                _editor.Document.Insert(_editor.CaretOffset, str);
            }
            else
            {
                _editor.Document.Insert(pos, str);
            }
        }


        private void Editor_TextChanged(object sender, EventArgs e)
        {
            _foldingStrategy.UpdateFoldings(_foldingManager, _editor.Document);
        }
        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            //提词器未显示时检查内容
            if (!_isReminderShow)
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
                    _reminder.CompletionList.SelectItem(_editor.Document.GetText(_reminder.StartOffset, _reminder.TextArea.Caret.Offset - _reminder.StartOffset));
                    _reminder.Close();
                }
            }
        }
        private void TextArea_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                //放大
                if (e.Delta > 0)
                {
                    if (_editor.FontSize < 200)
                    {
                        _editor.FontSize *= 1.1;
                    }
                }
                else
                {
                    //缩小
                    if (_editor.FontSize > 10)
                    {
                        _editor.FontSize /= 1.1;
                    }
                }
            }
        }
        private void ClipEditDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //保存到路径
            editingScriptClip.Description = t_description.Text;
            editingScriptClip.Script = _editor.Text;
            editingScriptClip.WriteObjectToBinaryFile(path);

            _editor = null;
            _reminder = null;
            _foldingManager.Clear();
            _foldingManager = null;
            _searchPanel = null;
            if (_replaceWindow != null)
            {
                _replaceWindow.Dispose();
                _replaceWindow = null;
            }
            GC.Collect(2);
        }


        private void Reminder_Loaded(object sender, RoutedEventArgs e)
        {
            _isReminderShow = true;
        }
        private void Reminder_Closed(object sender, EventArgs e)
        {
            _isReminderShow = false;
        }

      
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _searchPanel.Open();
        }
        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //检查窗口是否未创建或已释放，是则创建窗口并显示
            if (_replaceWindow == null || _replaceWindow.IsDisposed)
            {
                _replaceWindow = new ReplaceWindow(_editor.TextArea);
                _replaceWindow.Show();
            }
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editor.Copy();
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editor.Cut();
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _editor.Paste();
        }
        private void 快速输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (QuickInsertDialog dialog = new QuickInsertDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Insert(dialog.CombineContent);
                }
            }
        }
        private void rightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (System.Windows.Forms.Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Visible = true;
            }
            if (_editor.SelectionLength==0)
            {
                复制ToolStripMenuItem.Visible = 剪切ToolStripMenuItem.Visible = false;
            }
            else
            {
                复制ToolStripMenuItem.Visible = 剪切ToolStripMenuItem.Visible = true;
            }
        }
        

        private void b_clear_Click(object sender, EventArgs e)
        {
            if (MBox.ShowWarningOKCancel("无法恢复，确定清除吗？") == DialogResult.OK)
            {
                _editor.Clear();
            }
        }
        private void b_saveAndReturn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}

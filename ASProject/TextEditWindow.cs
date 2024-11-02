using AlgodooStudio.ASProject.Dialogs;
using AlgodooStudio.ASProject.Interface;
using AlgodooStudio.ASProject.Script.Parse;
using Dex.Common;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Search;
using PhunSharp;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.ASProject
{
    internal partial class TextEditWindow : DockContent, IEditable, IReplaceable, ISearchable, ISaveable
    {
        /// <summary>
        /// Avalon文字编辑器
        /// </summary>
        private TextEditor _editor = new TextEditor();
        /// <summary>
        /// 是否是文本加载阶段
        /// </summary>
        private bool _isLoad;
        /// <summary>
        /// 是否被保存了
        /// </summary>
        private bool _isSaved = true;
        /// <summary>
        /// 文件路径
        /// </summary>
        private string _filepath;
        /// <summary>
        /// 文件名
        /// </summary>
        private string _title;
        /// <summary>
        /// 提醒器是否已经显示
        /// </summary>
        private bool _isReminderShow = false;
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
        /// 词法解析器
        /// </summary>
        private ThymeTokenizer _tokenizer = new ThymeTokenizer();

        /// <summary>
        /// 获取和设置文件路径
        /// </summary>
        public string FilePath
        {
            get => _filepath;
            set
            {
                _filepath = value;
                _title = Path.GetFileName(value);
                //证明此处是文字加载阶段
                _isLoad = true;
                switch (Path.GetExtension(value))
                {
                    case ".phz":
                        _editor.Text = ArchiveTools.GetPhnContent(ArchiveTools.DeCompress(value));
                        break;
                    default:
                        _editor.Load(value);
                        break;
                }
                _isLoad = false;
                SetWindowTitle();//展示标题
            }
        }
        /// <summary>
        /// 获取和设置只读状态
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this._editor.IsReadOnly;
            }
            set
            {
                this._editor.IsReadOnly = value;
                this.粘贴ToolStripMenuItem.Enabled = !value;
                this.快速输入ToolStripMenuItem.Enabled = !value;
                SetWindowTitle();//展示标题
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
            this._isLoad = true;//标记当前是加载过程
            this._title = title;//设置标题
            this._filepath = filepath;//设置文件路径
            this._editor.Text = content;//设置内容
            this._isLoad = false;//结束标记加载过程
            this.ReadOnly = readOnly;//设置只读
            //SetWindowTitle(); 因为已经有了Readonly里的部分所以就不加了
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
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
            _editor.ShowLineNumbers = true;
            //为编辑器创建事件
            _editor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
            _editor.TextArea.MouseWheel += TextArea_MouseWheel;
            _editor.TextArea.SelectionChanged += TextArea_SelectionChanged;
            _editor.TextArea.TextEntering += TextArea_TextEntering;
            _editor.TextChanged += Editor_TextChanged;
            //将元素主机作为编辑器创建
            elementHost.Child = _editor;
            //初始化底部显示
            DisplayLineAndColAndPos();
            //初始化缩放
            scale.Text = (20 / 20 * 100) + "%";
            _editor.FontSize = 20;
            //初始化折叠栏
            _foldingManager = FoldingManager.Install(_editor.TextArea);
            _foldingStrategy.UpdateFoldings(_foldingManager, _editor.Document);
            //初始化搜索框
            _searchPanel = SearchPanel.Install(_editor.TextArea);
            ////右键菜单渲染器
            //rightMenu.Renderer = ThemeToolStripRenderer.GetRenderer();
        }

        private void Caret_PositionChanged1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        #region 编辑器
        /// <summary>
        /// 文字变动驱动错误检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_TextChanged(object sender, EventArgs e)
        {
            _foldingStrategy.UpdateFoldings(_foldingManager, _editor.Document);
            //如果当前是文字加载阶段则无需变动
            if (!_isLoad)
            {
                _isSaved = false;//标记未保存
                SetWindowTitle(true);//展示标题到窗口
            }
            errorCheckTimer.Enabled = true;
            
        }
        /// <summary>
        /// 文字输入前创建提词器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_TextEntering(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!ReadOnly)//可编辑时才显示
            {
                //提词器未显示时检查内容
                if (!_isReminderShow)
                {
                    //如果内容是字母或数字则创建提词器
                    if (Regex.IsMatch(e.Text, @"\w"))
                    {
                        CreateReminder();
                    }
                }
            }
        }
        /// <summary>
        /// 所选内容变动驱动选择长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_SelectionChanged(object sender, EventArgs e)
        {
            //选中后显示选中长度
            if (_editor.SelectionLength > 0)
            {
                selectlength.Text = "SelectLen: " + _editor.SelectionLength;
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
                scale.Text = (int)(_editor.FontSize / 20 * 100) + "%";
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

        #region 提词器
        private void _reminder_Closed(object sender, EventArgs e)
        {
            _isReminderShow = false;
        }
        private void _reminder_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _isReminderShow = true;
        }
        /// <summary>
        /// 选定内容如果为空则关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_reminder.CompletionList.ListBox.SelectedItems.Count == 0)
            {
                _reminder.Close();//匹配不到东西就关闭
            }
        }
        #endregion

        #region 窗体
        /// <summary>
        /// 窗体关闭时的检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未保存且窗体名后有星花则保存
            if (!_isSaved && Text.EndsWith("*"))
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
            GC.Collect(3);
        }
        #endregion 窗体

        #region 右键菜单
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
        #endregion 右键菜单

        #region 其他方法
        /// <summary>
        /// 创建提词器
        /// </summary>
        private void CreateReminder()
        {
            _reminder = new CompletionWindow(_editor.TextArea);
            //reminder.Background = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.BackColor1));
            //reminder.Foreground = new SolidColorBrush(NormalColorToMediaColor(Setting.theme.VarNameColor));
            _reminder.CompletionList.IsFiltering = true;
            _reminder.Loaded += _reminder_Loaded;
            _reminder.Closed += _reminder_Closed;
            _reminder.CompletionList.ListBox.SelectionChanged += ListBox_SelectionChanged;
            AddReminderItem(_reminder);
            _reminder.Show();
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
        /// 设置窗口的标题
        /// </summary>
        /// <param name="needSave">需要被保存</param>
        private void SetWindowTitle(bool needSave = false)
        {
            string str = "";
            //如果只读
            if (ReadOnly)
            {
                str+="[只读] ";
            }
            //记录标题
            str += this._title;
            //需要保存
            if (needSave)
            {
                str += "*";
            }
            this.Text = str;
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
            pos.Text = "Pos: " + _editor.TextArea.Caret.Offset;
            line.Text = "Line: " + _editor.TextArea.Caret.Line;
            col.Text = "Col: " + _editor.TextArea.Caret.Column;
            return _editor.TextArea.Caret.Offset;
        }
        /// <summary>
        /// 仅在此窗口内使用
        /// </summary>
        /// <returns>成功返回OK</returns>
        private DialogResult _Save()
        {
            //如果没保存
            if (!_isSaved)
            {
                //并且文件还不存在则使用另存为
                if (!File.Exists(_filepath))
                {
                    return _SaveAs();
                }
                else
                {
                    //文件存在则保存
                    _editor.Save(_filepath);
                    _isSaved = true; //标记已保存
                    SetWindowTitle();//展示标题到窗口
                    ReCheck();//重新检查异常
                    return DialogResult.OK;
                }
            }
            else
            {
                ReCheck();//重新检查异常
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
            sfd.Title = _title + " 另存为";
            sfd.Filter = "Thyme脚本|*.thm|cfg配置文件|*.cfg|其他文件|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _editor.Save(_filepath = sfd.FileName);//将另存为的路径作为当前文件的路径
                _isSaved = true;//标记已保存
                this._title = Path.GetFileName(sfd.FileName);//设置标题
                SetWindowTitle();//展示标题到窗口
                ReCheck();//重新检查异常
                return DialogResult.OK;
            }
            else
            {
                ReCheck();//重新检查异常
                return DialogResult.Cancel;
            }
        }
        /// <summary>
        /// 查找
        /// </summary>
        private void _Search()
        {
            _searchPanel.Open();
        }
        /// <summary>
        /// 替换
        /// </summary>
        private void _Replace()
        {
            //检查窗口是否未创建或已释放，是则创建窗口并显示
            if (_replaceWindow == null || _replaceWindow.IsDisposed)
            {
                _replaceWindow = new ReplaceWindow(_editor.TextArea);
                _replaceWindow.Show();
            }
        }
        /// <summary>
        /// 获取当前窗口的保存字符串
        /// </summary>
        /// <returns></returns>
        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + FilePath + "," + ReadOnly;
        }
        /// <summary>
        /// 在指定位置插入文字
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
        /// <summary>
        /// 右键菜单打开时的相关项目的显示控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool selectNotZero = _editor.SelectionLength != 0;
            bool clipboardContainedText = Clipboard.ContainsText();
            将选定文字保存为片段ToolStripMenuItem.Visible = selectNotZero;

            toolStripSeparator5.Visible = selectNotZero;

            复制ToolStripMenuItem.Visible = selectNotZero;
            剪切ToolStripMenuItem.Visible = selectNotZero;
            粘贴ToolStripMenuItem.Visible = clipboardContainedText;

            toolStripSeparator3.Visible = (clipboardContainedText || selectNotZero);
        }
        /// <summary>
        /// 选定文字并允许保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 将选定文字保存为片段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextGetDialog tgd = new TextGetDialog())
            {
                tgd.Title = "创建片段";
                tgd.InputText = "NewClip";
                tgd.IsNameValidCheck = true;
                //一直加载
                while (tgd.ShowDialog() == DialogResult.OK)
                {
                    //确保片段不存在
                    if (!File.Exists($".\\Clips\\{tgd.InputText}.clip"))
                    {
                        using (ClipEditDialog ced = new ClipEditDialog($".\\Clips\\{tgd.InputText}.clip"))
                        {
                            ced.InitialText = _editor.SelectedText;
                            ced.ShowDialog();
                        }
                        break;
                    }
                    else
                    {
                        MBox.Showlog($"片段{tgd.InputText}已存在！");
                        tgd.InputText = tgd.InputText;//用此方式重新选中那些文字
                    }
                }
            }
        }
        /// <summary>
        /// 异常检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void errorCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!this.ReadOnly)//只解析可编辑文档
            {
                _tokenizer.Diagnostics.Clear();
                var tos = _tokenizer.Tokenize(_editor.Text);
                var parser = new ThymeParser(tos);
                var result = parser.Parse();
                parser.Diagnostics.AddRange(_tokenizer.Diagnostics);//合并异常
                //如果文件存在则按照文件名设置
                if (File.Exists(_filepath))
                {
                    Program.UpdateErrors(_filepath, parser.Diagnostics);
                }
                else
                {
                    Program.UpdateErrors(_title, parser.Diagnostics);
                }
            }
            //经过一次检查后自动结束
            errorCheckTimer.Enabled = false;
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
            _editor.Copy();
        }
        public void Cut()
        {
            _editor.Cut();
        }
        public void Paste()
        {
            _editor.Paste();
        }
        public void Undo()
        {
            _editor.Undo();
        }
        public void Redo()
        {
            _editor.Redo();
        }
        public void Delete()
        {
            _editor.Delete();
        }
        public void SelectAll()
        {
            _editor.SelectAll();
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
        /// 选中错误位置
        /// </summary>
        /// <param name="range"></param>
        public void SelectErrorPos(Range range)
        {
            _editor.Select((int)range.Min, (int)(range.Max - range.Min));
            _editor.ScrollTo(_editor.TextArea.Caret.Line, _editor.TextArea.Caret.Column);
        }
        /// <summary>
        /// 重新检查
        /// </summary>
        public void ReCheck()
        {
            errorCheckTimer.Enabled = true;
        }
    }
}
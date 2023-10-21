using AlgodooStudio.Window.Dialogs;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AlgodooStudio.Window
{
    /// <summary>
    /// 替换窗口(只给文本编辑器提供替换操作)
    /// </summary>
    internal partial class ReplaceWindow : Form
    {
        /*找到后高亮即可，无需全部高亮emmmm，如果能全部高亮最好*/

        /// <summary>
        /// 搜索是否就绪
        /// </summary>
        private bool IsSearchReady = true;

        /// <summary>
        /// 定位用的匹配项索引
        /// </summary>
        private int locationIndex = 0;

        /// <summary>
        /// 匹配到的文本集合
        /// </summary>
        private MatchCollection matches;

        /// <summary>
        /// 要进行操作的文本区域
        /// </summary>
        private TextArea area;

        /// <summary>
        /// 使用给定的文本区域创建替换窗口
        /// </summary>
        /// <param name="area"></param>
        internal ReplaceWindow(TextArea area)
        {
            InitializeComponent();
            Initalize(area);
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        private void Initalize(TextArea area)
        {
            this.area = area;
            //利用文字更改事件驱动匹配项的更改
            area.Document.TextChanged += Document_TextChanged;
            //标签显示
            ShowLabels();
        }

        /// <summary>
        /// 获取与<paramref name="searchText"/>一致的全部的匹配项
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        private void GetAllMatchItems(string searchText)
        {
            //如果搜索文字存在则启动搜索
            if (searchText.Length > 0)
            {
                //大小写不敏感(忽略大小写)
                if (!caseSensitive.Checked)
                {
                    matches = Regex.Matches(area.Document.Text, searchText, RegexOptions.IgnoreCase);
                }
                else
                {
                    matches = Regex.Matches(area.Document.Text, searchText);
                }
                //每次匹配都要重置索引
                locationIndex = 0;
            }
        }

        /// <summary>
        /// 选中匹配项
        /// </summary>
        /// <param name="match">需要给定的匹配项</param>
        private void SelectMatchItem(Match match)
        {
            area.Selection = Selection.Create(area, match.Index, match.Index + match.Length);
            area.Caret.Position = area.Selection.EndPosition;
            area.Caret.BringCaretToView();
        }

        /// <summary>
        /// 获取所有匹配项
        /// </summary>
        private void GetMatches()
        {
            //搜索准备就绪后允许重新匹配
            if (IsSearchReady)
            {
                GetAllMatchItems(searchBox.Text);
                IsSearchReady = false;
            }
        }

        /// <summary>
        /// 索引后退
        /// </summary>
        private void IndexBack()
        {
            //如果开启了循环查找
            if (searchLoop.Checked)
            {
                if (locationIndex == 0)
                {
                    locationIndex = matches.Count;
                }
                locationIndex--;
            }
            else
            {
                if (locationIndex > 0)
                {
                    locationIndex--;
                }
            }
        }

        /// <summary>
        /// 索引前进
        /// </summary>
        private void IndexForward()
        {
            //如果开启了循环查找
            if (searchLoop.Checked)
            {
                //目前是最后的时候设定为0
                if (locationIndex == matches.Count - 1)
                {
                    locationIndex = -1;
                }
                //索引向前移动一位
                locationIndex++;
            }
            else
            {
                //不为循环查找时如果可以向前移动就前移
                if (locationIndex < matches.Count - 1)
                {
                    locationIndex++;
                }
            }
        }

        /// <summary>
        /// 索引显示
        /// </summary>
        private void ShowIndex()
        {
            if (matches != null)
            {
                if (matches.Count > 0)
                {
                    IndexDisplay.Text = $"当前：第{locationIndex + 1}个";
                }
                else
                {
                    IndexDisplay.Text = "当前：第0个";
                }
            }
            else
            {
                IndexDisplay.Text = "尚未进行查找";
            }
        }

        /// <summary>
        /// 匹配数量显示
        /// </summary>
        private void ShowMax()
        {
            if (matches != null)
            {
                maxCount.Text = $"共{matches.Count}个";
            }
            else
            {
                maxCount.Text = "尚未进行查找";
            }
        }

        /// <summary>
        /// 显示标签集
        /// </summary>
        private void ShowLabels()
        {
            ShowIndex();
            ShowMax();
        }

        /// <summary>
        /// 安全选中匹配项
        /// </summary>
        private void SelectMatchSafety()
        {
            //确保匹配到内容了
            if (matches.Count > 0)
            {
                SelectMatchItem(matches[locationIndex]);
            }
        }

        /// <summary>
        /// 向上查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastOne_Click(object sender, EventArgs e)
        {
            GetMatches();
            if (matches != null)
            {
                IndexBack();
                ShowLabels();
                SelectMatchSafety();
            }
            else
            {
                tip.Show("请输入要查找的文本", this.searchBox);
            }
        }

        /// <summary>
        /// 向下查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextOne_Click(object sender, EventArgs e)
        {
            GetMatches();
            if (matches != null)
            {
                IndexForward();
                ShowLabels();
                SelectMatchSafety();
            }
            else
            {
                tip.Show("请输入要查找的文本", this.searchBox);
            }
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void replace_Click(object sender, EventArgs e)
        {
            //有内容才能匹配，匹配到了才能选中，确保选中了才能替换
            if (area.Selection.SurroundingSegment != null)
            {
                //替换完毕后匹配项索引变动又需要重新匹配
                area.Document.Replace(matches[locationIndex].Index, matches[locationIndex].Length, replaceBox.Text);
                //重新匹配
                GetAllMatchItems(searchBox.Text);
                //显示标签
                ShowLabels();
                //安全选中
                SelectMatchSafety();
            }
        }

        /// <summary>
        /// 全部替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void replaceAll_Click(object sender, EventArgs e)
        {
            if (matches != null)
            {
                if (MBox.ShowWarningOKCancel("全部替换？") == DialogResult.OK)
                {
                    for (int i = matches.Count - 1; i >= 0; i--)
                    {
                        area.Document.Replace(matches[i].Index, matches[i].Length, replaceBox.Text);
                    }
                    MBox.ShowInfo("已完成" + matches.Count + "处替换");
                    GetAllMatchItems(searchBox.Text);
                    ShowLabels();
                }
            }
            else
            {
                tip.Show("请输入要查找的文本", this.searchBox);
            }
        }

        /// <summary>
        /// 计数(无视条件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countNumber_Click(object sender, EventArgs e)
        {
            GetAllMatchItems(searchBox.Text);
            if (matches != null)
            {
                MBox.Showlog($"共找到 {matches.Count} 处");
                ShowLabels();
            }
            else
            {
                tip.Show("请输入要查找的文本", this.searchBox);
            }
        }

        /// <summary>
        /// 文字变动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Document_TextChanged(object sender, EventArgs e)
        {
            //文字变动后再次执行匹配，以防止用空字符进行匹配
            IsSearchReady = true;
        }

        /// <summary>
        /// 大小写敏感变动时都要再次匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void caseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            IsSearchReady = true;
        }

        /// <summary>
        /// 搜索文字变动后允许搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text.Length > 0)
            {
                tip.Hide(searchBox);
            }
            else
            {
                tip.Show("内容不能为空", searchBox);
            }
            IsSearchReady = true;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Click(object sender, EventArgs e)
        {
            GetMatches();
            if (matches != null)
            {
                ShowLabels();
            }
            else
            {
                tip.Show("请输入要查找的文本", this.searchBox);
            }
        }
    }
}
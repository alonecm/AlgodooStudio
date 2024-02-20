using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 大括号折叠
    /// </summary>
    internal class BraceFoldingStrategy
    {
        /// <summary>
        ///  指示是否应在折叠元素上显示属性的标志
        /// </summary>
        public bool ShowAttributesWhenFolded { get; set; }

        /// <summary>
        /// 为指定的文档创建折叠组，并用它们更新折叠管理器。
        /// </summary>
        /// <param name="manager">折叠管理器</param>
        /// <param name="document">文档</param>
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            IEnumerable<NewFolding> newFoldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(newFoldings, firstErrorOffset);
        }

        /// <summary>
        /// 为指定的文档创建折叠组
        /// </summary>
        /// <param name="document">文档</param>
        /// <param name="firstErrorOffset">第一个错误的偏移</param>
        /// <returns>折叠组集合</returns>
        private IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            List<NewFolding> foldlist = new List<NewFolding>();
            Stack<Match> startBraces = new Stack<Match>();//创建一个栈用于存放起始大括号
            MatchCollection m = Regex.Matches(document.Text, "{|}"); //从文中找括号
            int errorIndex = -1;//异常的索引
            foreach (Match br in m)
            {
                //是起始则入栈
                if (br.Value == "{")
                {
                    startBraces.Push(br);
                }
                else
                {
                    //是结束则看栈中数量是否不为零
                    if (startBraces.Count > 0)
                    {
                        Match tmp = startBraces.Pop();
                        //如果两个括号都不在同一行则折叠
                        if (document.GetLineByOffset(tmp.Index).LineNumber < document.GetLineByOffset(br.Index).LineNumber)
                        {
                            NewFolding e = new NewFolding();
                            foldlist.Add(new NewFolding(tmp.Index, br.Index + 1));
                        }
                    }
                    else
                    {
                        //为0则认为是有异常的
                        errorIndex = br.Index + 1;
                    }
                }
            }
            firstErrorOffset = errorIndex;
            foldlist.Sort((NewFolding a, NewFolding b) => a.StartOffset.CompareTo(b.StartOffset));
            return foldlist;
        }
    }
}
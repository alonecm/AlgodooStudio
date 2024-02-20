using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Windows.Media;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 提示器数据项，用来存放提示器的内容
    /// </summary>
    internal class ReminderItem : ICompletionData
    {
        /// <summary>
        /// 用指定的文字创建提示器数据项
        /// </summary>
        /// <param name="text"></param>
        public ReminderItem(string text)
        {
            Text = text;
            Content = text;
            Description = text + " 未知用途";
        }

        public ReminderItem(string text, object description)
        {
            Text = text;
            Content = text;
            Description = description;
        }

        /// <summary>
        /// 数据项的图像源
        /// </summary>
        public ImageSource Image { get; }

        /// <summary>
        /// 用来筛选到当前数据项的文字
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 数据项的实际内容
        /// </summary>
        public object Content { get; }

        /// <summary>
        /// 数据项的描述
        /// </summary>
        public object Description { get; }

        /// <summary>
        /// 用来筛选到当前数据项的优先级
        /// </summary>
        public double Priority { get; }

        /// <summary>
        /// 执行完成时的动作
        /// </summary>
        /// <param name="textArea">文本区域</param>
        /// <param name="completionSegment">数据项使用的文本段数据对</param>
        /// <param name="insertionRequestEventArgs">用于执行插入请求的事件</param>
        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            //替换掉文本
            textArea.Document.Replace(completionSegment.Offset, completionSegment.Length, Text);
        }
    }
}
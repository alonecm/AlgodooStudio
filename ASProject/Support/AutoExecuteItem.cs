﻿using Dex.Common;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 自启动项
    /// </summary>
    public class AutoExecuteItem
    {
        /// <summary>
        /// 创建自启动项
        /// </summary>
        /// <param name="isEnabled">项是否启用</param>
        /// <param name="type">项类型</param>
        /// <param name="content">项内容</param>
        public AutoExecuteItem(bool isEnabled, AutoExecuteItemType type, string content, Range range)
        {
            IsEnabled = isEnabled;
            Type = type;
            Content = content;
            Range = range;
        }
        public bool IsEnabled { get; set; }
        public AutoExecuteItemType Type { get; }
        public string Content { get; }
        public Range Range { get; }

        public override string ToString()
        {
            return Content;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 自启动项集合
    /// </summary>
    internal class AutoExecuteItemCollection
    {
        private List<AutoExecuteItem> items;

        public AutoExecuteItemCollection()
        {
            items = new List<AutoExecuteItem>();
        }

        /// <summary>
        /// 添加自启动项
        /// </summary>
        /// <param name="items"></param>
        public void Add(params AutoExecuteItem[] items)
        {
            if (items.Length>0)
                this.items.AddRange(items);
        }
        /// <summary>
        /// 移除指定位置的项
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        /// <summary>
        /// 切换指定项的状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="status"></param>
        public void SetStatus(int index, bool status)
        {
            if (index >= 0 && index < items.Count)
                items[index].IsEnabled = status;
        }
    }
}

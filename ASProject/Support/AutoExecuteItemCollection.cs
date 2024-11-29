using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 自启动项集合
    /// </summary>
    public class AutoExecuteItemCollection : IEnumerable<AutoExecuteItem>
    {
        private List<AutoExecuteItem> items = new List<AutoExecuteItem>();

        public AutoExecuteItem this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public AutoExecuteItemCollection()
        {
           
        }

        /// <summary>
        /// 切换指定项的状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="status"></param>
        public void SetStatus(int index, bool status)
        {
            if (index >= 0 && index < items.Count)
                this[index].IsEnabled = status;
        }

        /// <summary>
        /// 添加启动项
        /// </summary>
        /// <param name="items"></param>
        public void Add(AutoExecuteItem item)
        {
            this.items.Add(item);
        }

        /// <summary>
        /// 移除指定索引项
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public IEnumerator<AutoExecuteItem> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

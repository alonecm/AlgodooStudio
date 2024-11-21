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
    internal class AutoExecuteItemCollection : List<AutoExecuteItem>
    {

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
            if (index >= 0 && index < this.Count)
                this[index].IsEnabled = status;
        }
    }
}

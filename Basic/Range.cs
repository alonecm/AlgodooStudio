using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Basic
{
    /// <summary>
    /// 表示一个范围，其范围是闭区间，能取到两侧值
    /// </summary>
    public struct Range
    {
        /// <summary>
        /// 范围最小值
        /// </summary>
        public int min;
        /// <summary>
        /// 范围最大值
        /// </summary>
        public int max;

        /// <summary>
        /// 创建一个最小值为0，最大值给定的范围
        /// </summary>
        /// <param name="max">最大值</param>
        public Range(int max)
        {
            min = 0;
            this.max = max;
        }
        /// <summary>
        /// 创建一个最小值给定，最大值给定的范围
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public Range(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override string ToString()
        {
            return $"[{min}, {max}]";
        }
    }
}

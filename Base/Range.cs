namespace AlgodooStudio.Base
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
        /// 创建一个最小值为0，最大值给定的范围, 其范围是闭区间，能取到两侧值
        /// </summary>
        /// <param name="max">最大值</param>
        public Range(int max)
        {
            min = 0;
            this.max = max;
        }

        /// <summary>
        /// 创建一个最小值给定，最大值给定的范围, 其范围是闭区间，能取到两侧值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public Range(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// 判断<paramref name="n"/>是否在当前范围内
        /// </summary>
        /// <param name="n">需要判断的值</param>
        /// <returns>在范围内返回真，否则返回假</returns>
        public bool InRange(int n)
        {
            return InRange(n, this);
        }

        /// <summary>
        /// 判断两个范围是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>是则返回真</returns>
        public static bool operator ==(Range a, Range b)
        {
            return a.min == b.min && a.max == b.max;
        }

        /// <summary>
        /// 判断两个范围是否不等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>是则返回真</returns>
        public static bool operator !=(Range a, Range b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 判断<paramref name="n"/>是否在<paramref name="range"/>内
        /// </summary>
        /// <param name="n">需要判断的值</param>
        /// <param name="range">范围</param>
        /// <returns>在范围内返回真，否则返回假</returns>
        public static bool InRange(int n, Range range)
        {
            if (n >= range.min && n <= range.max)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[{min}, {max}]";
        }
    }
}
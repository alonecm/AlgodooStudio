using AlgodooStudio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// 存档单元
    /// </summary>
    internal sealed class PhnUnit
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal readonly string name;
        /// <summary>
        /// 类型
        /// </summary>
        internal readonly UnitType type;
        /// <summary>
        /// 项集合
        /// </summary>
        internal readonly UnitItem[] items;
        /// <summary>
        /// 创建存档单元
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="items">项集合</param>
        public PhnUnit(string name, UnitType type, params UnitItem[] items)
        {
            this.name = name;
            this.type = type;
            this.items = items;
        }

        public static bool operator ==(PhnUnit left,PhnUnit right)
        {
            bool nullLeft = (object)left == null;
            bool nullRight = (object)right == null;
            //如果两侧都不是空的则比较内部
            if (!nullLeft && !nullRight)
            {
                if (left.items.Length!=right.items.Length)
                {
                    return false;
                }
                else
                {
                    //查看是否存在不同的项
                    bool isDifferent = false;
                    for (int i = 0; i < left.items.Length; i++)
                    {
                        //如果项的名与值不相同则不是一类
                        if (left.items[i].name != right.items[i].name||
                            left.items[i].content != right.items[i].content)
                        {
                            isDifferent = true;//存在不同
                            break;
                        }
                    }
                    //如果存在不同的部分则不同
                    if (isDifferent)
                    {
                        return false;
                    }
                    //最后查看是否相同
                    return left.name == right.name && left.type == right.type;
                }
            }
            else
            {
                //如果两侧都是空的则返回真
                if (nullLeft && nullRight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool operator !=(PhnUnit left, PhnUnit right)
        {
            return !(left == right);
        }
    }
}

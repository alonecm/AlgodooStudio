using AlgodooStudio.Base;
using System.Text.RegularExpressions;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 设置项，用于存放设置名称和值
    /// </summary>
    public sealed class SettingItem : Item<ValueType>
    {
        /// <summary>
        /// 创建设置项
        /// </summary>
        /// <param name="name">设置名</param>
        /// <param name="value">设置值</param>
        public SettingItem(string name, string value) : base(name, value)
        {
        }

        public override ValueType GetType(string value)
        {
            //看是否为数组
            if (value.StartsWith("["))
            {
                return ValueType.Array;
            }
            else
            {
                //不为数组看是否为布尔值
                if (value == "true" || value == "false")
                {
                    return ValueType.Boolean;
                }
                else
                {
                    //不为布尔值看是否是整数
                    if (Regex.IsMatch(value, @"^[0-9]+$"))
                    {
                        return ValueType.Integer;
                    }
                    else if (Regex.IsMatch(value, @"^\d+\.\d+$"))//是否为浮点数
                    {
                        return ValueType.Float;
                    }
                    else
                    {
                        return ValueType.String;
                    }
                }
            }
        }
    }
}
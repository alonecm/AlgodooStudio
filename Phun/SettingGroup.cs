using AlgodooStudio.Base;
using AlgodooStudio.Interface;
using System;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 设置组，用户存放设置项
    /// </summary>
    public sealed class SettingGroup : Container<SettingItem>, ITreeGroup<SettingGroup>
    {
        /// <summary>
        /// 子设置组
        /// </summary>
        public SettingGroup[] ChildGroup { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; } = "Default";

        /// <summary>
        /// 创建设置组
        /// </summary>
        /// <param name="childGroup">子组集合</param>
        /// <param name="groupName">组名</param>
        /// <param name="contents">组项</param>
        public SettingGroup(SettingGroup[] childGroup, string groupName, params SettingItem[] contents) : base(contents)
        {
            ChildGroup = childGroup;
            GroupName = groupName;
            Contents = contents;
        }

        /// <summary>
        /// 创建设置组
        /// </summary>
        /// <param name="childGroup">子组</param>
        /// <param name="groupName">组名</param>
        /// <param name="contents">组项</param>
        public SettingGroup(SettingGroup childGroup, string groupName, params SettingItem[] contents) : base(contents)
        {
            ChildGroup = new SettingGroup[] { childGroup };
            GroupName = groupName;
            Contents = contents;
        }

        /// <summary>
        /// 创建设置组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="contents">组项</param>
        public SettingGroup(string groupName, params SettingItem[] contents) : base(contents)
        {
            GroupName = groupName;
            Contents = contents;
        }

        /// <summary>
        /// 创建设置组
        /// </summary>
        /// <param name="contents">组项</param>
        public SettingGroup(params SettingItem[] contents) : base(contents)
        {
            Contents = contents;
        }

        public void AddGroup(SettingGroup group)
        {
            if (ChildGroup == null)
            {
                ClearGroup();
            }
            SettingGroup[] tmp = new SettingGroup[ChildGroup.Length + 1];
            Array.Copy(ChildGroup, 0, tmp, 0, ChildGroup.Length);
            tmp[tmp.Length - 1] = group;
            ChildGroup = tmp;
        }

        public void ClearGroup()
        {
            ChildGroup = new SettingGroup[0];
        }

        public void RemoveGroupAt(int index)
        {
            if (index < ChildGroup.Length && index > -1)
            {
                SettingGroup[] tmp = new SettingGroup[ChildGroup.Length - 1];
                Array.Copy(ChildGroup, 0, tmp, 0, index);
                //判断不是最后一位
                if (index + 1 < ChildGroup.Length)
                {
                    //复制内容
                    Array.Copy(ChildGroup, index + 1, tmp, index, ChildGroup.Length - index - 1);
                }
                ChildGroup = tmp;
            }
            else
            {
                throw new IndexOutOfRangeException("给定的索引值不在子节点数组的索引范围内");
            }
        }

        /// <summary>
        /// 将B组中的所有子组添加到A组的子组后
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>组合之后的A组</returns>
        public static SettingGroup operator +(SettingGroup a, SettingGroup b)
        {
            foreach (var item in b.ChildGroup)
            {
                a.AddGroup(item);
            }
            return a;
        }

        /// <summary>
        /// 以字符串形式返回设置组
        /// </summary>
        /// <returns>设置组的字符串形式</returns>
        public override string ToString()
        {
            string tmp = GroupName + " -> {";
            foreach (var item in this)
            {
                tmp += "\n    " + item.ToString(" = ") + ";";
            }
            tmp += "\n};";
            return tmp;
        }
    }
}
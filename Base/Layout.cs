using AlgodooStudio.Interface;
using System.Collections.Generic;

namespace AlgodooStudio.Base
{
    /// <summary>
    /// 布局基类
    /// </summary>
    public sealed class Layout : IBasicInformation
    {
        /// <summary>
        /// 布局编号
        /// </summary>
        private int id;

        /// <summary>
        /// 布局名称
        /// </summary>
        private string name;

        /// <summary>
        /// 窗体布局
        /// </summary>
        private Dictionary<int, string> forms = new Dictionary<int, string>();

        public string this[int index]
        {
            get
            {
                return forms[index];
            }
        }

        /// <summary>
        /// 当前布局的全部窗体
        /// </summary>
        public Dictionary<int, string> Forms
        {
            get
            {
                return forms;
            }
        }

        /// <summary>
        /// 布局中的窗体数量
        /// </summary>
        public int Count
        {
            get
            {
                return forms.Count;
            }
        }

        /// <summary>
        /// 布局名称
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// 布局编号
        /// </summary>
        public int ID { get => id; }

        /// <summary>
        /// 新建一个基本布局
        /// </summary>
        /// <param name="id">布局编号</param>
        public Layout(int id)
        {
            this.id = id;
            this.name = "Layout" + id;
        }

        /// <summary>
        /// 新建一个基本布局
        /// </summary>
        /// <param name="id">布局编号</param>
        /// <param name="name">布局名称</param>
        public Layout(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// 通过ID修改窗体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="form">需要修改的窗体</param>
        /// <returns>是否修改成功</returns>
        public bool ModifyFormByID(int id, string form)
        {
            //看新的窗体是否与其他的窗体相同
            foreach (var item in forms.Values)
            {
                if (form == item)
                {
                    return false;
                }
            }
            forms[id] = form;
            return true;
        }

        /// <summary>
        /// 通过ID获取窗体
        /// </summary>
        /// <param name="id">窗体编号</param>
        /// <returns>指定窗体</returns>
        public string GetFormByID(int id)
        {
            return forms[id];
        }

        /// <summary>
        /// 添加窗体到窗体布局中
        /// </summary>
        /// <param name="id">窗体编号</param>
        /// <param name="form">需要添加的窗体</param>
        /// <returns>检查是否添加成功</returns>
        public bool AddForm(int id, string form)
        {
            return AddForm(id, form, false);
        }

        /// <summary>
        /// 添加窗体到窗体布局中
        /// </summary>
        /// <param name="id">窗体编号</param>
        /// <param name="form">需要添加的窗体</param>
        /// <param name="isCheckSame">是否需要检查相同项</param>
        /// <returns>检查是否添加成功</returns>
        public bool AddForm(int id, string form, bool isCheckSame)
        {
            if (isCheckSame)
            {
                //检查布局中是否有相同的窗体存在
                foreach (var item in forms.Values)
                {
                    if (item == form)
                    {
                        return false;
                    }
                }
            }
            forms.Add(id, form);
            return true;
        }

        /// <summary>
        /// 通过编号移除指定窗体
        /// </summary>
        /// <param name="id">窗体编号</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveFormByID(int id)
        {
            return forms.Remove(id);
        }
    }
}
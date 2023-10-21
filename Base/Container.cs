using AlgodooStudio.Attribute;
using AlgodooStudio.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgodooStudio.Base
{
    /// <summary>
    /// 一个可用于存放<typeparamref name="Object"/>的简易容器基类，可以被继承
    /// </summary>
    [XmlSerialize]
    public class Container<Object> : IContainer<Object>, IEnumerable<Object>
    {
        [XmlSerialize]
        protected Object[] contents;

        /// <summary>
        /// 当前容量
        /// </summary>
        private int capacity;

        /// <summary>
        /// 新项索引
        /// </summary>
        private int newItemIndex;

        /// <summary>
        /// 创建一个用于放置<typeparamref name="Object"/>的容器
        /// </summary>
        /// <param name="contents">需要放进去的<typeparamref name="Object"/></param>
        public Container(params Object[] contents)
        {
            Contents = contents;
        }

        public Object[] Contents { get => contents; set => contents = value; }
        public int Count { get => Contents.Length; }

        public Object this[int index]
        {
            get
            {
                return Contents[index];
            }
            set
            {
                Contents[index] = value;
            }
        }

        public virtual void Add(Object content, int size = 0)
        {
            if (size <= 0)
            {
                Object[] tmp = new Object[Contents.Length + 1];
                Array.Copy(Contents, 0, tmp, 0, Contents.Length);
                tmp[tmp.Length - 1] = content;
                Contents = tmp;
                capacity++;
            }
            else
            {
                //如果已被填满则扩容
                if (newItemIndex == capacity)
                {
                    Object[] tmp = new Object[Contents.Length + size];
                    Array.Copy(Contents, 0, tmp, 0, Contents.Length);
                    tmp[Contents.Length] = content;
                    Contents = tmp;
                    capacity += size;
                }
                else
                {
                    Contents[newItemIndex] = content;
                }
            }
            newItemIndex++;
        }

        public virtual void AddRange(Object[] source)
        {
            Object[] tmp = new Object[Contents.Length + source.Length];
            Array.Copy(Contents, 0, tmp, 0, Contents.Length);
            Array.Copy(source, 0, tmp, Contents.Length, source.Length);
            Contents = tmp;
            newItemIndex += source.Length;
            capacity += source.Length;
        }

        public virtual void Clear()
        {
            Contents = new Object[0];
            newItemIndex = 0;
            capacity = 0;
        }

        public virtual void RemoveAt(int index)
        {
            if (index < Contents.Length && index > -1)
            {
                Object[] tmp = new Object[Contents.Length - 1];
                Array.Copy(Contents, 0, tmp, 0, index);
                //判断不是最后一位
                if (index + 1 < Contents.Length)
                {
                    //复制内容
                    Array.Copy(Contents, index + 1, tmp, index, Contents.Length - index - 1);
                }
                Contents = tmp;
            }
            else
            {
                throw new IndexOutOfRangeException("给定的索引值不在对象组的索引范围内");
            }
            newItemIndex--;
            capacity--;
        }

        public IEnumerator<Object> GetEnumerator()
        {
            return ((IEnumerable<Object>)Contents).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        /// <summary>
        /// 将装有<typeparamref name="Object"/>的容器转换成用<typeparamref name="Object"/>表示的数组
        /// </summary>
        /// <param name="objects">需要转换的容器</param>
        public static implicit operator Object[](Container<Object> objects)
        {
            return objects.Contents;
        }

        /// <summary>
        /// 将<typeparamref name="Object"/>表示的数组转换成装有<typeparamref name="Object"/>的容器
        /// </summary>
        /// <param name="objects">需要转换的数组</param>
        public static explicit operator Container<Object>(Object[] objects)
        {
            return new Container<Object>(objects);
        }
    }
}
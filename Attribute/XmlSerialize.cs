using AlgodooStudio.Base;
using System;

namespace AlgodooStudio.Attribute
{
    /// <summary>
    /// 用于描述一个需要被保存为XML文件的类型的特性,被标识的字段的值可以被保存为XML文件<br/><br/>
    /// 字段只能使用引用类型不能使用值类型！
    /// </summary>
    [AttributeUsage(AttributeTargets.Field |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Property)]
    public class XmlSerialize : ASAttribute
    {
        /// <summary>
        /// 是否只序列化成文件
        /// </summary>
        private bool writeOnly = false;

        /// <summary>
        /// 创建一个Xml序列化特性
        /// </summary>
        /// <param name="writeOnly">是否只序列化成文件</param>
        public XmlSerialize(bool writeOnly)
        {
            this.writeOnly = writeOnly;
        }

        public XmlSerialize()
        {
        }

        /// <summary>
        /// 是否只序列化成文件
        /// </summary>
        public bool WriteOnly { get => writeOnly; set => writeOnly = value; }
    }
}
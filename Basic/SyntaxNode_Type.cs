using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Basic
{
    /// <summary>
    /// 用于表示一个语法节点的类型
    /// </summary>
    internal enum SyntaxNode_Type
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        Error,
        /// <summary>
        /// 未知
        /// </summary>
        None,//√
        /// <summary>
        /// 脚本
        /// </summary>
        Thyme,//√
        /// <summary>
        /// 设置
        /// </summary>
        Setting,//√
        /// <summary>
        /// 类
        /// </summary>
        Class,//√
        /// <summary>
        /// 方法
        /// </summary>
        Method,//√
        /// <summary>
        /// 参数
        /// </summary>
        Parameter,//√
        /// <summary>
        /// 判断
        /// </summary>
        Judge,//√
        /// <summary>
        /// 循环
        /// </summary>
        Loop,//√
        /// <summary>
        /// 数组
        /// </summary>
        Array,//√
        /// <summary>
        /// 场景对象
        /// </summary>
        Object//√
    }
}

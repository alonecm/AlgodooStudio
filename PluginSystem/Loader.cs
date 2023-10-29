using AlgodooStudio.ASProject.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlgodooStudio.PluginSystem
{
    /// <summary>
    /// 插件加载器，用于将Plugins中的插件(一个一个的文件夹)读取成一个一个的插件对象实例
    /// </summary>
    internal static class Loader
    {
        /// <summary>
        /// 加载指定路径下的DLL插件
        /// </summary>
        /// <param name="DllPath">插件路径</param>
        internal static Plugin Load(string DllPath)
        {
            //获取插件中所有已经存在的类型
            List<TypeInfo> types = Assembly.LoadFrom(DllPath).DefinedTypes.ToList();
            foreach (var item in types)
            {
                //判断是否为主类并输出由这个类型生成的实例
                if (item.Name == "Main")
                {
                    //能输出则证明主类存在
                    return (Plugin)Activator.CreateInstance(item.AsType());
                }
            }
            return null;
        }
    }
}
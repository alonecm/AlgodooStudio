using AlgodooStudio.ASProject.Support;
using Dex.Common;
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
        /// 已注册的插件
        /// </summary>
        public static Container<Plugin> RegisteredPlugins { get; } = new Container<Plugin>();
        /// <summary>
        /// 已启用的插件
        /// </summary>
        public static Container<Plugin> EnabledPlugins { get; } = new Container<Plugin>();

        /// <summary>
        /// 注册指定路径下的DLL插件
        /// </summary>
        /// <param name="DllPath">插件路径</param>
        public static void Regist(string DllPath)
        {
            //获取插件中所有已经存在的类型
            List<TypeInfo> types = Assembly.LoadFrom(DllPath).DefinedTypes.ToList();
            foreach (var item in types)
            {
                //判断是否为主类并输出由这个类型生成的实例
                if (item.Name == "Main")
                {
                    var plugin = (Plugin)Activator.CreateInstance(item.AsType());
                    plugin.OnLoad();
                    RegisteredPlugins.Add(plugin);
                }
            }
        }

        /// <summary>
        /// 启用所有插件
        /// </summary>
        public static void EnabledAllPlugin()
        {
            //依次启用所有注册的插件
            foreach (var item in RegisteredPlugins)
            {
                try
                {
                    //尝试启用如果成功则加入到已启用插件中
                    item.OnEnabled();
                    EnabledPlugins.Add(item);
                }
                catch{ }
            }
        }
    }
}
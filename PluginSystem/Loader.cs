using AlgodooStudio.ASProject;
using AlgodooStudio.ASProject.Support;
using Dex.IO;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static Dictionary<string, Plugin> LoadedPlugins { get; } = new Dictionary<string, Plugin>();

        /// <summary>
        /// 加载指定路径下的DLL插件
        /// </summary>
        /// <param name="DllPath">插件路径</param>
        public static void Load(string DllPath)
        {
            //获取插件中所有已经存在的类型
            List<TypeInfo> types = Assembly.LoadFrom(DllPath).DefinedTypes.ToList();
            foreach (var item in types)
            {
                //判断是否为主类并输出由这个类型生成的实例
                if (item.Name == "Main")
                {
                    var plugin = (Plugin)Activator.CreateInstance(item.AsType());
                    try
                    {
                        if (!LoadedPlugins.ContainsKey(plugin.Name))
                        {
                            plugin.OnLoad();
                            LoadedPlugins.Add(plugin.Name, plugin);
                        }
                    }
                    catch (Exception e)
                    {
                        LogWriter.Write(e.Message);
                        MBox.ShowError($"启用插件 \"{plugin.Name}\" 时出现了异常，异常信息已经输出到Logs文件中，请查看");
                    }
                }
            }
        }

        /// <summary>
        /// 启动插件(跟随记录启动)
        /// </summary>
        public static void EnablePlugins()
        {
            //不存在管理文件则创建
            if (!File.Exists(".\\plgm.bin"))
            {
                WriteManageFile();
            }
            //加载管理文件
            var plgm = SimpleSerializer.GetObjectFromBinaryFile<bool[]>(".\\plgm.bin");
            int pi = 0;
            //依次启用所有注册的插件
            foreach (var plugin in LoadedPlugins.Values)
            {
                if (plgm[pi])
                {
                    try
                    {
                        plugin.SetEnable();
                    }
                    catch (Exception e)
                    {
                        LogWriter.Write(e.Message);
                        MBox.ShowError($"启用插件 \"{plugin.Name}\" 时出现了异常，异常信息已经输出到Logs文件中，请查看");
                    }
                }
                else
                {
                    try
                    {
                        plugin.SetDisable();
                    }
                    catch (Exception e)
                    {
                        LogWriter.Write(e.Message);
                        MBox.ShowError($"禁用插件 \"{plugin.Name}\" 时出现了异常，异常信息已经输出到Logs文件中，请查看");
                    }
                }
                pi++;
            }
        }
        /// <summary>
        /// 写管理文件
        /// </summary>
        public static void WriteManageFile()
        {
            bool[] pluginStatus = new bool[LoadedPlugins.Count];
            int i = 0;
            foreach (var plugin in LoadedPlugins.Values)
            {
                pluginStatus[i] = plugin.IsEnabled; i++;
            }
            pluginStatus.WriteObjectToBinaryFile(".\\plgm.bin");
        }
    }
}
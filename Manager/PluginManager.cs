using AlgodooStudio.Base;
using AlgodooStudio.PluginSystem;
using System.Collections.Generic;
using System.IO;
using Zero.Core.FileAndDirectory.FileTools;
using Zero.Core.XML;

namespace AlgodooStudio.Manager
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    internal static class PluginManager
    {
        /// <summary>
        /// 插件文件夹(自带斜杠哈)
        /// </summary>
        private const string pluginFolder = pluginFolderNo + "\\";

        /// <summary>
        /// 插件文件夹(不带斜杠哈)
        /// </summary>
        private const string pluginFolderNo = "Plugins";

        /// <summary>
        /// 文件查询器
        /// </summary>
        private static FileSearcher fs = new FileSearcher(pluginFolderNo);

        /// <summary>
        /// 加载进来的全部插件
        /// </summary>
        private static Dictionary<string, Plugin> plugins = new Dictionary<string, Plugin>();

        /// <summary>
        /// 注册文件
        /// </summary>
        private static EasyXml registFile;

        /*
         1.由Loader将dll文件组成的插件文件变成一个一个的插件对象实例
         2.将这些插件实例载入到这个plugins中
         3.将plugins中的插件实例在这个插件管理器中执行出来
         */

        /// <summary>
        /// 注册已加载插件启用状态
        /// </summary>
        internal static void RegEnablePlugins()
        {
            string path = pluginFolder + "reg.pm";
            //检查注册文件是否存在
            if (File.Exists(path))
            {
                //存在则读取注册文件
                registFile = new EasyXml(path, false);
                registFile.RootNode.Content = "";//清空根节点的内容
                //检查先前的注册文件与现在的注册文件在插件数量上是否相同
                List<Xml_Node> nodes = registFile.RootNode.ChildNodes;
                if (nodes.Count == plugins.Count)
                {
                    //相同则直接加载启用状态，如果是true则需要激活一次方法
                    foreach (var plugin in nodes)
                    {
                        switch (plugin.Attribute[0].Content)
                        {
                            case "true":
                                plugins[plugin.Content].SetEnable();
                                plugins[plugin.Content].OnEnabled();
                                break;

                            case "false":
                                plugins[plugin.Content].SetDisable();
                                break;
                        }
                    }
                }
                else
                {
                    //不同则将已存在的插件状态先读取出来
                    foreach (var plugin in nodes)
                    {
                        //检查文件中的插件是否不存在
                        if (!plugins.ContainsKey(plugin.Content))
                        {
                            continue;
                        }
                        else
                        {
                            //存在则直接读取其状态，如果是true则需要激活一次方法
                            switch (plugin.Attribute[0].Content)
                            {
                                case "true":
                                    plugins[plugin.Content].SetEnable();
                                    plugins[plugin.Content].OnEnabled();
                                    break;

                                case "false":
                                    plugins[plugin.Content].SetDisable();
                                    break;
                            }
                        }
                    }
                    //再将插件现有状态写入文件中
                    registFile.RootNode.ChildNodes.Clear();//清空
                    //写入
                    foreach (var item in plugins)
                    {
                        registFile.RootNode.ChildNodes.Add(new Xml_Node("Plugin", item.Value.Name, new Xml_Attb("status", item.Value.IsEnabled.ToString())));
                    }
                    registFile.Save();
                    registFile.Dispose();
                }
            }
            else
            {
                //不存在则创建一个注册文件并导入现在导入的全部插件启用状态
                registFile = EasyXml.Create(path, "Plugins");
                foreach (var item in plugins)
                {
                    registFile.RootNode.ChildNodes.Add(new Xml_Node("Plugin", item.Value.Name, new Xml_Attb("status", item.Value.IsEnabled.ToString())));
                    //如果插件是开启的默认直接激活
                    if (item.Value.IsEnabled)
                    {
                        item.Value.OnEnabled();
                    }
                }
                registFile.Save();
                registFile.Dispose();
            }
        }

        /// <summary>
        /// 加载Plugins文件夹内的所有插件
        /// </summary>
        internal static void Load()
        {
            FileInfo[] dllFiles = fs.GetFilesFromThisFolder();
            //读取所有已存在插件
            foreach (var item in dllFiles)
            {
                if (item.Extension == ".dll")
                {
                    Load(item);
                }
            }
            RegEnablePlugins();
        }

        /// <summary>
        /// 加载指定的插件
        /// </summary>
        /// <param name="pluginFile">插件文件</param>
        internal static void Load(FileInfo pluginFile)
        {
            //读取插件
            Plugin p = Loader.Load(pluginFile.FullName);
            //如果插件加载成功
            if (p != null)
            {
                //执行一次加载时的操作
                p.OnLoad();
                //将插件放到列表中
                plugins.Add(p.Name, p);
            }
            else
            {
                //获取插件名
                string pluginName = Path.GetFileNameWithoutExtension(pluginFile.Name);
                //产生一个错误的，未启用的插件
                Plugin errPlugin = new Plugin(-1, pluginName);
                //标记禁用此错误插件
                errPlugin.SetDisable();
                //设置为有错误的插件
                errPlugin.SetError("无法找到入口点");
                plugins.Add(pluginName, errPlugin);
                //MBox.ShowError(pluginFile.Name + errPlugin.ErrorDescription);
            }
        }

        /// <summary>
        /// 卸载加载过的指定的插件
        /// </summary>
        internal static void Unload(string pluginName)
        {
            //调用一次Onload
            plugins[pluginName].OnUnload();
            //将指定的插件从插件列表里移除
            plugins.Remove(pluginName);
        }

        /// <summary>
        /// 卸载全部插件
        /// </summary>
        internal static void Unload()
        {
            foreach (var item in plugins.Values)
            {
                item.OnUnload();
            }
            plugins.Clear();
        }

        /// <summary>
        /// 启用加载过的指定的插件
        /// </summary>
        /// <param name="pluginName">插件名</param>
        internal static void Enable(string pluginName)
        {
            //设定启用
            plugins[pluginName].SetEnable();
            //执行启用方法
            plugins[pluginName].OnEnabled();
        }

        /// <summary>
        /// 启用所有插件
        /// </summary>
        /// <param name="pluginName">插件名</param>
        internal static void Enable()
        {
            foreach (var item in plugins.Values)
            {
                //如果这个插件不是出错的就启用
                if (!item.IsError)
                {
                    //设定启用
                    item.SetEnable();
                    //执行启用方法
                    item.OnEnabled();
                }
            }
        }

        /// <summary>
        /// 禁用加载过的指定的插件
        /// </summary>
        /// <param name="pluginName">插件名</param>
        internal static void Disable(string pluginName)
        {
            //设定禁用
            plugins[pluginName].SetDisable();
            //执行禁用方法
            plugins[pluginName].OnDisabled();
        }

        /// <summary>
        /// 禁用所有插件
        /// </summary>
        internal static void Disable()
        {
            foreach (var item in plugins.Values)
            {
                item.SetDisable();
                item.OnDisabled();
            }
        }
    }
}
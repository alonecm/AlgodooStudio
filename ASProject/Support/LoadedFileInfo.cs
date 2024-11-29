using PhunSharp.Archive;
using System.ComponentModel;

namespace AlgodooStudio.ASProject.Support
{
    /// <summary>
    /// 已加载文件信息
    /// </summary>
    public class LoadedFileInfo
    {
        public LoadedFileInfo(ArchiveFile phn)
        {
            var props = phn.Settings["FileInfo"].Properties;
            foreach (var item in props)
            {
                switch (item.Key)
                {
                    case "title":
                        Title = (string)item.Value;
                        break;
                    case "version":
                        Version = (string)item.Value;
                        break;
                    case "author":
                        Author = (string)item.Value;
                        break;
                    case "description":
                        Describe = (string)item.Value;
                        break;
                    default:
                        break;
                }
            }
            SettingCount = phn.Settings.Count;
            EntityCount = phn.Objects.Count;
            GlobalVarsCount = phn.Variables.PropsCount;
            SceneVarsCount = phn.SceneVariables.PropsCount;
        }
        [Category("存档信息")]
        [Description("标题")]
        public string Title { get; }

        [Category("存档信息")]
        [Description("存档文件版本")]
        public string Version { get; }

        [Category("存档信息")]
        [Description("作者")]
        public string Author { get; }

        [Category("存档信息")]
        [Description("描述")]
        public string Describe { get; }

        [Category("场景信息")]
        [Description("设置数")]
        public int SettingCount { get; }
        [Category("场景信息")]
        [Description("实体数量")]
        public int EntityCount { get; }
        [Category("场景信息")]
        [Description("全局变量数")]
        public int GlobalVarsCount { get; }
        [Category("场景信息")]
        [Description("场景变量数")]
        public int SceneVarsCount { get; }
    }
}
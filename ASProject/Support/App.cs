using System.Drawing;
using System.IO;

namespace AlgodooStudio.ASProject.Support
{
    public sealed class App
    {
        private string name;

        private Icon icon;

        private string path;

        public App(string path)
        {
            name = System.IO.Path.GetFileNameWithoutExtension(path);
            Path = path;
        }

        public App(string path, string name)
        {
            this.name = name;
            Path = path;
        }

        public App(string path, string name, Icon icon)
        {
            this.name = name;
            this.icon = icon;
            this.path = path;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 图标
        /// </summary>
        public Icon Icon { get => icon; }

        /// <summary>
        /// 所在路径
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (File.Exists(value))
                {
                    icon = Icon.ExtractAssociatedIcon(value);
                }
                else
                {
                    icon = null;
                }
                path = value;
            }
        }
    }
}
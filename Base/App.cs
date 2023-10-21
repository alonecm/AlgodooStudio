using AlgodooStudio.Attribute;
using System.Drawing;
using System.IO;

namespace AlgodooStudio.Base
{
    [XmlSerialize]
    public sealed class App
    {
        [XmlSerialize]
        private string name;

        private Icon icon;

        [XmlSerialize]
        private string path;

        public App(string path)
        {
            this.name = System.IO.Path.GetFileNameWithoutExtension(path);
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
                    this.icon = Icon.ExtractAssociatedIcon(value);
                }
                else
                {
                    this.icon = null;
                }
                this.path = value;
            }
        }
    }
}
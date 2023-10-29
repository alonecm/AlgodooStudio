using Dex.Attribute;
using System.IO;

namespace AlgodooStudio.ASProject.Support
{
    [ConfigObject]
    internal class Setting
    {
        /// <summary>
        /// Algodoo所在路径
        /// </summary>
        public string AlgodooPath { get; set; }
        /// <summary>
        /// AS所在目录
        /// </summary>
        public string StudioPath { get; internal set; } = Directory.GetCurrentDirectory();


        /// <summary>
        /// 操作是否为剪切
        /// </summary>
        internal bool IsCutting { get; set; }
        /// <summary>
        /// 剪贴操作完成
        /// </summary>
        internal bool IsCopyFinished { get; set; }
    }
}
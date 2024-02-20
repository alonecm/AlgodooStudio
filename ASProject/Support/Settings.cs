using Dex.Attribute;
using System.IO;
using System.Numerics;

namespace AlgodooStudio.ASProject.Support
{
    [ConfigObject]
    internal class Settings
    {
        /// <summary>
        /// Algodoo文档根目录
        /// </summary>
        public string ScenePath { get; set; }
        /// <summary>
        /// Algodoo所在根目录
        /// </summary>
        public string AlgodooPath { get; set; }
        /// <summary>
        /// 是否保存当前布局
        /// </summary>
        public bool IsSavingLayout { get; set; } = true;

        /// <summary>
        /// 工作室所在目录
        /// </summary>
        internal string StudioPath { get; } = ".\\";


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
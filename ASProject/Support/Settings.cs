using Dex.Common;

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
        /// Algodoo可执行文件路径
        /// </summary>
        internal string AlgodooExecuteFilePath => AlgodooPath + "\\algodoo.exe";
        /// <summary>
        /// Algodoo自启动文件路径
        /// </summary>
        internal string AlgodooAutoExecuteFilePath => AlgodooPath + "\\autoexec.cfg";
    }
}
using System.IO;

namespace AlgodooStudio.Window
{
    /// <summary>
    /// 脚本管理项，用于存放脚本文件的信息和启用状态
    /// </summary>
    internal class ScriptManageItem
    {
        private FileInfo scriptFile;
        private bool isEnabled = false;

        internal ScriptManageItem(FileInfo scriptFile)
        {
            this.scriptFile = scriptFile;
        }

        internal ScriptManageItem(FileInfo scriptFile, bool isEnabled)
        {
            this.scriptFile = scriptFile;
            this.isEnabled = isEnabled;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

        /// <summary>
        /// 脚本路径
        /// </summary>
        internal string Path { get => scriptFile.FullName; }

        /// <summary>
        /// 脚本文件名
        /// </summary>
        internal string FileName { get => scriptFile.Name; }
    }
}
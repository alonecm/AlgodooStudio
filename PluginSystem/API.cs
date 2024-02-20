
using AlgodooStudio.ASProject;
using System.Windows.Forms;

namespace AlgodooStudio.PluginSystem
{
    /// <summary>
    /// 向程序外部提供必要的插件API接口用于插件开发
    /// </summary>
    public static class API
    {
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message"></param>
        public static DialogResult ShowLog(string message)
        {
            return MBox.Showlog(message);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message"></param>
        public static DialogResult ShowWarning(string message)
        {
            return MBox.ShowWarning(message);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message"></param>
        public static DialogResult ShowError(string message)
        {
            return MBox.ShowError(message);
        }
    }
}
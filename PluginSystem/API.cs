using AlgodooStudio.ASProject;

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
        public static void ShowLog(string message)
        {
            MBox.Showlog(message);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowWarning(string message)
        {
            MBox.ShowWarning(message);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowError(string message)
        {
            MBox.ShowError(message);
        }
    }
}
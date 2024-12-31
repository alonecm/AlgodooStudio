using System.Windows.Forms;

namespace AlgodooStudio.ASProject
{
    /// <summary>
    /// 信息框
    /// </summary>
    internal static class MBox
    {
        /// <summary>
        /// 显示信息框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult Showlog(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio");
        }

        /// <summary>
        /// 显示信息框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowInfo(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示信息框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowWarning(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示警告提示选择框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowWarningYesNoCancel(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示警告提示选择框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowWarningOKCancel(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误提示选择框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowErrorOKCancel(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示信息框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowError(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示询问提示选择框
        /// </summary>
        /// <param name="content">需要显示的内容</param>
        /// <returns>结果</returns>
        public static DialogResult ShowQuestionYesNo(string content)
        {
            return MessageBox.Show(content, "AlgodooStudio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
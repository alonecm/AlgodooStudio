using AlgodooStudio.Analyzer.Syntax;
using System.Text.RegularExpressions;

namespace AlgodooStudio.Analyzer
{
    /// <summary>
    /// 正则表达式匹配工具集
    /// </summary>
    internal static class RegexTools
    {
        /// <summary>
        /// 移除原文中的注释
        /// </summary>
        /// <param name="content">原文</param>
        /// <returns>去除注释后的原文</returns>
        internal static string RemoveNote(string content)
        {
            string ct = content;
            //移除 行 注释
            while (true)
            {
                Match line = Regex.Match(ct, RegexFormatStrings.lineNote, RegexOptions.Multiline);
                if (!line.Success)
                {
                    break;
                }
                ct = ct.Remove(line.Index, line.Value.Length);
            }
            //移除 块 注释
            while (true)
            {
                Match block = Regex.Match(ct, RegexFormatStrings.blockNote, RegexOptions.Multiline);
                if (!block.Success)
                {
                    break;
                }
                ct = ct.Remove(block.Index, block.Value.Length);
            }
            return ct;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace AlgodooStudio.Analyzer.Syntax
{
    /// <summary>
    /// 用于保存给<see cref="Regex"/>使用的格式字符串
    /// </summary>
    public static class RegexFormatStrings
    {
        //正则表达式的格式控制符已经内置到C#中了，无需写入
        /// <summary>
        /// 空格字符
        /// </summary>
        public const string space = "\\s*";
        /// <summary>
        /// 词汇
        /// </summary>
        public const string word = "\\w+";
        /// <summary>
        /// 全文
        /// </summary>
        public const string all = "(.|[\r\n])+";
        /// <summary>
        /// 非数字文字
        /// </summary>
        public const string noNumberWord = "[a-zA-Z_]";
        /// <summary>
        /// 特殊字符
        /// </summary>
        public const string specialCode= "[~`!@#$%^&*()=+[\\]|:;\"\'<>,.?/\\\\-]";
        /// <summary>
        /// 赋值号
        /// </summary>
        public const string assignment = space + "=";
        /// <summary>
        /// 大括号开始语句
        /// </summary>
        public const string startBraces = space + "{";
        /// <summary>
        /// 转移语句
        /// </summary>
        public const string transform = space + "->";
        /// <summary>
        /// 分号
        /// </summary>
        public const string colons = space + ";+";
        /// <summary>
        /// 大括号结束语句
        /// </summary>
        public const string endBraces = space + "}" + colons;
        /// <summary>
        /// 所有符合命名规则的词
        /// </summary>
        public const string inRuleWords = "[^\\d.]" + noNumberWord + "\\w*[^=]";//[^\d.][a-zA-Z_]\w*
        /// <summary>
        /// 实例化语句
        /// </summary>
        public const string instance = inRuleWords + assignment + space + "alloc" + colons;
        /// <summary>
        /// scene.addXXX相关方法
        /// </summary>
        public const string addSceneObject = "scene.add" + noNumberWord + "+";
        /// <summary>
        /// 变量定义
        /// </summary>
        public const string variableDefine = "[^\\d.][a-zA-Z_]\\w*\\s*(:=|=)\\s*(\"\\w*\"|\\d*);";

        /// <summary>
        /// 方法分支
        /// </summary>
        public const string method_startBranches = @"(\w*\s*=\s*\(((\w|\.)*\s*|,)*\)\s*=>\s*{)";//取最后
        /// <summary>
        /// 转移分支
        /// </summary>
        public const string trans_startBranches = @"(->\s*{)";//取最后
        /// <summary>
        /// 定义分支
        /// </summary>
        public const string define_startBranches = @"(=\s*{)";//取最后
        /// <summary>
        /// 起始分支
        /// </summary>
        public const string startBranches = "{";
        /// <summary>
        /// 结束分支
        /// </summary>
        public const string endBranches = "}";
        /// <summary>
        /// 转移分支结束
        /// </summary>
        public const string trans_endBranches = @"(}\s*;\s*})";//取最后
        public const string define_endBranches = @"(}\s*;\s*\w*\s*=)";//取首
        public const string class_endBranches = @"(}\s*;\s*\w*\s*;\s*})";//取最后
    }
}

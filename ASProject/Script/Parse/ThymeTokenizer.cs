using AlgodooStudio.ASProject.Script.Parse.Expr;
using Dex.Analysis;
using Dex.Analysis.Parse;
using Dex.Common;
using System;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public sealed class ThymeTokenizer : Tokenizer<ThymeTokenCollection>
    {
        private bool removeComment;

        /// <summary>
        /// 创建thyme的标注器
        /// </summary>
        /// <param name="content">解析内容</param>
        /// <param name="removeComment">移除注释</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ThymeTokenizer(string content, bool removeComment = false) : base(content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            this.removeComment = removeComment;

            //移除注释
            if (removeComment)
            {
                this.content = CommentRemover.DefaultRemover.RemoveComments(this.content);
            }

            //初始化位置
            this.pos = 0;

            //单符号
            RegistSingleSymbol(
                '+', '-', '*', '/', '%', '^', '>', '<', '!',
                '=', '.', ',', ':', ';', '?',
                '(', ')', '[', ']', '{', '}',
                '∞');

            //多字符
            RegistMultipleSymbol(
                "!=", ">=", "<=", "==",
                "||", "&&",
                "=>", ":=", "++", "->");

            //关键词
            RegistKeywords("null", "NaN");

            //特殊词
            RegistSpecial("alloc", "alloc");
            RegistSpecial("inf", "inf");
        }

        public override ThymeTokenCollection Tokenize()
        {
            this.pos = 0;

            //多重注释标记
            var mutipleComment = false;
            //行注释
            var singleComment = false;

            //创建标记列表
            List<ThymeToken> tokens = new List<ThymeToken>();

            //初始化位置
            pos = 0;

            //读取代码
            while (IsNotEnd)
            {
                var c = Peek(0);

                if (!removeComment)
                {
                    //结束多行注释
                    if (pos + 1 < content.Length && c == '*' && Peek(1) == '/')
                    {
                        mutipleComment = false;
                        pos++;
                        pos++;
                        continue;
                    }

                    //执行多重注释
                    if (mutipleComment)
                    {
                        pos++;
                        continue;
                    }

                    //启动多重注释
                    if (pos + 1 < content.Length && c == '/' && Peek(1) == '*')
                    {
                        mutipleComment = true;
                        pos++;
                        pos++;
                        continue;
                    }

                    //结束单行注释
                    if (singleComment && (c == '\r' || c == '\n'))
                    {
                        singleComment = false;
                        pos++;
                        continue;
                    }

                    //执行单行注释
                    if (singleComment)
                    {
                        pos++;
                        continue;
                    }

                    //启动单行注释
                    if (pos + 1 < content.Length && c == '/' && Peek(1) == '/')
                    {
                        singleComment = true;
                        pos++;
                        pos++;
                        continue;
                    }
                }

                //越过空白
                if (IsWhiteSpace(c)) pos++;

                //是否为标识符
                else if (char.IsLetter(c) || c == '_')
                {
                    var start = pos;
                    var id = this.ReadIdentifier();
                    var lower = id.ToLower();

                    if (this.keywords.Contains(id)) //关键词，大小写影响
                    {
                        tokens.Add(new ThymeToken("keyword", id, new Range(start, pos)));
                    }
                    else if (this.special.ContainsKey(lower)) //特殊词，大小写不影响
                    {
                        tokens.Add(new ThymeToken(this.special[lower], lower, new Range(start, pos)));
                    }
                    else if (lower == "true" || lower == "false") //布尔值，大小写不影响
                    {
                        tokens.Add(new ThymeToken("bool", lower, new Range(start, pos)));
                    }
                    else //标识符，大小写不影响
                    {
                        tokens.Add(new ThymeToken("identifier", lower, new Range(start, pos)));
                    }
                }

                //字符串
                else if (c == '\"')
                {
                    var start = pos;
                    string str = ReadString();
                    tokens.Add(new ThymeToken("string", str, new Range(start, pos)));
                }

                //数字
                else if (char.IsDigit(c) || c == '.' && char.IsDigit(Peek(1)))
                {
                    var start = pos;
                    string number = ReadNumber();
                    tokens.Add(new ThymeToken("number", number, new Range(start, pos)));
                }

                //是其他字符
                else
                {
                    var start = pos;
                    //尝试获取多字符符号
                    string symbol = ReadMultipleSymbol();
                    if (symbol != null)
                    {
                        tokens.Add(new ThymeToken("m_symbol", symbol, new Range(start, pos)));
                        Next();
                    }
                    else if (singleSymbols.Contains(c))
                    {
                        tokens.Add(new ThymeToken("s_symbol", $"{Next()}", new Range(start, pos)));
                        continue;
                    }
                    else
                    {
                        ReportError(new ThymeDiagnostic($"不支持的符号 '{c}'", new Range(start, pos + 1)));
                        tokens.Add(new ThymeToken("UnsupportSymbol", $"{c}", new Range(start, pos + 1)));
                        Next();
                    }
                }
            }
            return new ThymeTokenCollection(tokens);
        }

        public static ThymeTokenizer GetThymeTokenizer(string content, bool removeComment = false)
        {
            return new ThymeTokenizer(content, removeComment);
        }

        public static Tuple<ThymeTokenCollection, DiagnosticsCollection> GetTokens(string content)
        {
            var tkiz = GetThymeTokenizer(content);
            return new Tuple<ThymeTokenCollection, DiagnosticsCollection>(tkiz.Tokenize(), tkiz.Diagnostics);
        }
    }
}
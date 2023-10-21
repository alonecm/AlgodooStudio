using AlgodooStudio.Analyzer.Syntax.Exception;
using AlgodooStudio.Base;
using System.Text.RegularExpressions;

namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// 词法解析流，用于生成词组流(Token)
    /// </summary>
    internal sealed class TokenStream
    {
        /// <summary>
        /// 当前字符集所在的索引
        /// </summary>
        internal int index = 0;

        /// <summary>
        /// 从流中获取到的全部字符集
        /// </summary>
        internal Container<Token> tokens = new Container<Token>();

        /// <summary>
        /// 异常集
        /// </summary>
        private Container<SyntaxException> exceptions = new Container<SyntaxException>();

        /// <summary>
        /// 字符流
        /// </summary>
        private StringStream stream;

        /// <summary>
        /// 编写代码用的部分关键词
        /// </summary>
        //private const string keywords = "if for eval geval if_then_else print readable";
        /// <summary>
        /// 编写代码用的部分关键词
        /// </summary>
        private const string keywords = "alloc Widgets";

        /// <summary>
        /// 操作符(左右两侧是内容)
        /// </summary>
        private const string Operator = "+-*/%^&|<>=!?:.";

        /// <summary>
        /// 是否开启Debug模式
        /// </summary>
        internal bool IsDebug { get; set; } = false;

        /// <summary>
        /// 异常集
        /// </summary>
        public Container<SyntaxException> Exceptions { get => exceptions; }

        /// <summary>
        /// 创建字符集流
        /// </summary>
        /// <param name="content">需要创建字符集的内容</param>
        internal TokenStream(string content)
        {
            stream = new StringStream(content);
            IsDebug = false;
            //全部解析
            GetTokens();
        }

        /// <summary>
        /// 读取上一个字符集但不向上移动
        /// </summary>
        /// <returns>读取到的字符集</returns>
        public Token Review()
        {
            //如果没超过范围就是返回值否则就是返回空
            if (index > 0)
            {
                return tokens[index - 1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取当前字符集并向下移动
        /// </summary>
        /// <returns>读取到的字符集</returns>
        public Token Next()
        {
            //如果没超过范围就是返回值否则就是返回空
            if (index < tokens.Count - 1)
            {
                return tokens[index++];
            }
            else
            {
                return null;
            }
            ////获取由Peek读取的下一项
            //var tmp = nextToken;
            ////倒空以便于再次Peek
            //nextToken = null;
            ////如果下一项不是空的则优先返回下一项
            //if (tmp!=null)
            //{
            //    return tmp;
            //}
            //else
            //{
            //    //如果下一项依然为空则向下读取
            //    return ReadNextFromStream();
            //}
        }

        /// <summary>
        /// 读取下一个字符集但不向下移动
        /// </summary>
        /// <returns>字符集</returns>
        public Token Peek()
        {
            int newIndex = index + 1;
            //如果超出了索引就返回空
            if (newIndex < tokens.Count - 1)
            {
                return tokens[newIndex];
            }
            else
            {
                return null;
            }
            ////读取下一个但是流不可能反向流动所以要暂存到nextToken中
            //if (nextToken==null)
            //{
            //    nextToken = ReadNextFromStream();
            //}
            //return nextToken;
        }

        /// <summary>
        /// 获取当前的字符集但不向下移动
        /// </summary>
        /// <returns>字符集</returns>
        public Token Now()
        {
            return tokens[index];
        }

        /// <summary>
        /// 是否结束读取了
        /// </summary>
        /// <returns>结束则返回真</returns>
        public bool EndOfRead()
        {
            return Now() == null;
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="msg">异常描述</param>
        /// <param name="errorCode">异常代码</param>
        /// <returns>语法异常</returns>
        public SyntaxException ThrowException(string msg, string errorCode)
        {
            return new SyntaxException(new Range(index, index), errorCode, msg);
        }

        /// <summary>
        /// 获取内容对应的全部字符集
        /// </summary>
        private void GetTokens()
        {
            tokens.Clear();
            while (true)
            {
                Token tmp = ReadNextFromStream();
                if (tmp == null)
                {
                    break;
                }
                tokens.Add(tmp);
            }
        }

        /// <summary>
        /// 从字符流中读取下一个字符集
        /// </summary>
        /// <returns>读到的字符</returns>
        private Token ReadNextFromStream()
        {
            //跳过空白
            JumpBlank();
            //获取当前的字符
            char ch = stream.Now();
            //如过流结束了则返回空字符集(只要当前不是空就行)
            if (ch == '\0') return null;
            if (ch == '/' && stream.Peek() == '/' || ch == '/' && stream.Peek() == '*')
            {
                //跳过注释
                SkipComment(ch);
                return ReadNextFromStream();
            }
            //获取新跳过后的字符
            char newCh = stream.Now();
            //如果当前的字符是双引号则读取字符串
            if (newCh == '"') return GetString();
            //如果当前的字符是数字则读取数字组
            if (IsDigit(newCh)) return GetNumber();
            //如果当前的字符是满足命名规则的则返回词组
            if (IsNameRule(newCh)) return GetWord();
            if (IsOperator(newCh)) return GetOperator();
            int p = stream.Pos;
            stream.Next();//确保这些特殊字符在弄完后也能向下移动一次
            if (newCh == '{')
            {
                return new Token(new Range(p, p + 1), "branchStart", "{");
            }
            if (newCh == '}')
            {
                return new Token(new Range(p, p + 1), "branchEnd", "}");
            }
            if (newCh == '(')
            {
                return new Token(new Range(p, p + 1), "bracesStart", "(");
            }
            if (newCh == ')')
            {
                return new Token(new Range(p, p + 1), "bracesEnd", ")");
            }
            if (newCh == '[')
            {
                return new Token(new Range(p, p + 1), "arrayStart", "[");
            }
            if (newCh == ']')
            {
                return new Token(new Range(p, p + 1), "arrayEnd", "]");
            }
            if (newCh == ';')
            {
                return new Token(new Range(p, p + 1), "punc", ";");
            }
            if (newCh == ',')
            {
                return new Token(new Range(p, p + 1), "elementCut", ",");
            }
            if (IsDebug)
            {
                throw new System.Exception("无法处理字符：" + newCh);
            }
            else
            {
                Exceptions.Add(stream.ThrowException("无法处理字符：" + newCh, ErrorCode.charhandleError));
                return new Token(new Range(stream.Pos), "error", newCh);
            }
        }

        /// <summary>
        /// 获取操作符字符集
        /// </summary>
        /// <returns>操作符字符集</returns>
        private Token GetOperator()
        {
            //因当前是操作符
            string op = stream.Now() + "";
            int start = stream.Pos;
            stream.Next();//从下一个开始检查
            while (!stream.EndOfRead())
            {
                //是操作符则添加进去
                var ch = stream.Now();
                if (IsOperator(ch))
                {
                    op += ch;
                }
                else
                {
                    //移动了判断不通过
                    break;
                }
                stream.Next();
            }
            return new Token(new Range(start, stream.Pos), "operator", op);
        }

        /// <summary>
        /// 是否为操作符
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是返回真</returns>
        private bool IsOperator(char ch)
        {
            return Operator.IndexOf(ch) >= 0;
        }

        /// <summary>
        /// 获取单词字符集
        /// </summary>
        /// <returns>单词字符集</returns>
        private Token GetWord()
        {
            //加入当前字符
            string word = "";
            int start = stream.Pos;
            while (!stream.EndOfRead())
            {
                var ch = stream.Now();
                //如果后一个字符是符合命名规则的
                if (IsNameRule2(ch))
                {
                    word += ch;//加入
                    stream.Next();
                }
                else
                {
                    break;
                }
            }
            if (IsKeywords(word))
            {
                return new Token(new Range(start, stream.Pos), "keyword", word);
            }
            else
            {
                switch (word)
                {
                    case "null":
                    case "NaN":
                        return new Token(new Range(start, stream.Pos), "spValue", word);

                    case "true":
                    case "false":
                        return new Token(new Range(start, stream.Pos), "bool", word);

                    default:
                        return new Token(new Range(start, stream.Pos), "var", word);
                }
            }
        }

        /// <summary>
        /// 指定单词是否为关键词
        /// </summary>
        /// <param name="word">需要比对的单词</param>
        /// <returns>是则返回真</returns>
        private bool IsKeywords(string word)
        {
            return Regex.IsMatch(keywords, "\\b" + word + "\\b");
        }

        /// <summary>
        /// 是否是命名规则的字符
        /// </summary>
        /// <param name="ch">需要检查的字符</param>
        /// <returns>符合命名规则返回真</returns>
        private bool IsNameRule2(char ch)
        {
            return Regex.IsMatch(ch + "", "[_a-zA-Z0-9]");
        }

        /// <summary>
        /// 是否是命名规则的字符
        /// </summary>
        /// <param name="ch">需要检查的字符</param>
        /// <returns>符合命名规则返回真</returns>
        private bool IsNameRule(char ch)
        {
            return Regex.IsMatch(ch + "", "[_a-zA-Z]");
        }

        /// <summary>
        /// 获取数字组字符集
        /// </summary>
        /// <returns>数字组字符集</returns>
        private Token GetNumber()
        {
            //读到的数字
            string num = "";
            int start = stream.Pos;
            //是否存在小数点
            bool point = false;
            while (!stream.EndOfRead())
            {
                var ch = stream.Now();
                //检查下一项是否是数字
                if (IsDigit(ch))
                {
                    num += ch;
                }
                else
                {
                    //如果当前是点且下一个字符是数字则证明是小数点
                    if (ch == '.' && IsDigit(stream.Peek()))
                    {
                        num += ".";
                        point = true;
                    }
                    else
                    {
                        //啥也不是则退出
                        break;
                    }
                }
                stream.Next();
            }
            if (point)
            {
                return new Token(new Range(start, stream.Pos), "float", num);
            }
            else
            {
                return new Token(new Range(start, stream.Pos), "int", num);
            }
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="ch">需要判断的字符</param>
        /// <returns>是数字则返回真</returns>
        private bool IsDigit(char ch)
        {
            return "0123456789".IndexOf(ch) >= 0;
        }

        /// <summary>
        /// 获取字符串字符集
        /// </summary>
        /// <returns>字符串字符集</returns>
        private Token GetString()
        {
            string str = "";
            int start = stream.Pos + 1;
            while (!stream.EndOfRead())
            {
                stream.Next();//优先下移确保不是双引号
                //如果当前的字符是被转义的双引号则不结束
                if (stream.Now() == '\\' && stream.Peek() == '"')
                {
                    str += stream.Now() + stream.Peek();
                    stream.Next();
                    stream.Next();
                }
                if (stream.Now() == '"')//如果是普通双引号则结束读取
                {
                    stream.Next();//退出前一定要下移
                    break;
                }
                else
                {
                    str += stream.Now();
                }
            }
            return new Token(new Range(start, stream.Pos), "string", str);
        }

        /// <summary>
        /// 跳过注释
        /// </summary>
        /// <param name="ch">需要检查的字符</param>
        private void SkipComment(char ch)
        {
            //当前是"/"
            //如果是单行注释
            if (ch == '/' && stream.Peek() == '/')
            {
                //不断做循环读取
                while (!stream.EndOfRead())
                {
                    //检查当前是否为换行符，如果为换行符则结束
                    if (stream.Now() == '\n')
                    {
                        break;
                    }
                    stream.Next();
                }
                //因为当前是\n所以要继续向下移动一次
                stream.Next();
                //JumpBlank();
            }
            else
            {
                //如果是多行注释
                if (ch == '/' && stream.Peek() == '*')
                {
                    //不断做循环读取
                    while (!stream.EndOfRead())
                    {
                        //检查当前是否为*/
                        if (stream.Now() == '*' && stream.Peek() == '/')
                        {
                            break;
                        }
                        stream.Next();
                    }
                    //因为当前是*且下一个是/所以要跳过继续判断下一个字符
                    stream.Next();
                    //JumpBlank();
                }
            }
        }

        /// <summary>
        /// 跳过空白字符(换行，制表，空格)
        /// </summary>
        private void JumpBlank()
        {
            //循环检查
            while (!stream.EndOfRead())
            {
                //如果当前是换行符则向下移动
                if (" \t\r\n".IndexOf(stream.Now()) >= 0)
                {
                    stream.Next();
                }
                else
                {
                    break;
                }
            }
        }

        ///// <summary>
        ///// 读取下一个字符
        ///// </summary>
        ///// <returns>读取到的字符集</returns>
        //private Token ReadNext()
        //{
        //    //判断是否为空白字符
        //    ReadWhile(IsWhiteSpace);
        //    //如果流是空的则结束读取返回空
        //    if (stream.EndOfRead()) return null;
        //    //检查后方的字符的字符
        //    var ch = stream.Peek();
        //    //如果后方的字符是注释则跳过注释
        //    if (ch == '/')
        //    {
        //        SkipComment();
        //        return ReadNext();
        //    }
        //    //如果后方的字符是双引号则读取字符串
        //    if (ch == '"') return ReadString();
        //    //如果后方的字符是数字则读取数字组
        //    if (IsDigit(ch)) return ReadNumber();
        //    //如果后方是满足命名规则的则返回词组
        //    if (IsNameRule(ch)) return ReadWord();
        //    //如果后方满足块组的返回块标识符
        //    if (is_punc(ch))
        //    {
        //        return new Token(new Range(stream.Pos,stream.Pos),"punc", stream.Next());
        //    }
        //    if (is_op_char(ch))
        //    {
        //        return new Token(new Range(stream.Pos, stream.Pos), "op", ReadWhile(is_op_char));
        //    }
        //    //throw new System.Exception("无法处理字符：" + ch);
        //    exceptions.Add(stream.ThrowException("Can't handle character: " + ch, ErrorCode.charhandleError));
        //    return new Token(new Range(stream.Pos, stream.Pos), "error", ch);
        //}
        ///// <summary>
        ///// 跳过注释
        ///// </summary>
        //private void SkipComment()
        //{
        //    ReadWhile(new Predicate<char>(delegate (char ch) { return ch != '\n'; }));
        //    stream.Next();
        //}
        ///// <summary>
        ///// 是否为操作符
        ///// </summary>
        ///// <param name="ch"></param>
        ///// <returns></returns>
        //private bool is_op_char(char ch)
        //{
        //    return Operator.IndexOf(ch) >= 0;
        //}
        ///// <summary>
        ///// 是否为括号分号和逗号
        ///// </summary>
        ///// <param name="ch"></param>
        ///// <returns></returns>
        //private bool is_punc(char ch)
        //{
        //    return ",;(){}[]".IndexOf(ch) >= 0;
        //}
        ///// <summary>
        ///// 读取单词
        ///// </summary>
        ///// <returns>单词的字符集</returns>
        //private Token ReadWord()
        //{
        //    var id = ReadWhile(IsNameRule);
        //    return new Token(new Range(stream.Pos, stream.Pos), IsKeywordsK(id) ? "keyword" : "var", id);
        //}
        ///// <summary>
        ///// 判断当前词语是否为关键词
        ///// </summary>
        ///// <param name="word"></param>
        ///// <returns></returns>
        //private bool IsKeywordsK(string word)
        //{
        //    return keywords.IndexOf(" " + word + " ") >= 0;
        //}
        ///// <summary>
        ///// 不知道是啥
        ///// </summary>
        ///// <param name="ch"></param>
        ///// <returns></returns>
        //private bool is_id(char ch)
        //{
        //    return IsNameRule(ch);//|| "?!-<>=0123456789".IndexOf(ch) >= 0;
        //}
        ///// <summary>
        ///// 是否为单词命名规则的开头
        ///// </summary>
        ///// <param name="ch">需要判断的字符</param>
        ///// <returns>是则返回真</returns>
        //private bool IsNameRuleK(char ch)
        //{
        //    return Regex.IsMatch(ch + "", "[a-zA-Z_]");
        //}
        ///// <summary>
        ///// 检查是否为空白字符
        ///// </summary>
        ///// <param name="ch">当前字符</param>
        ///// <returns>存在则返回真</returns>
        //private bool IsWhiteSpace(char ch)
        //{
        //    return " \t\r\n".IndexOf(ch) >= 0;
        //}
        ///// <summary>
        ///// 读取循环，将需要在循环读取字符串的过程中用
        ///// 于判断的条件的<br/>返回值为布尔值的函数放入其中即可
        ///// </summary>
        ///// <param name="function">需要用来做循环的布尔值方法</param>
        //private string ReadWhile(Predicate<char> function)
        //{
        //    var str = "";
        //    //通过传入的用于判断的函数将下一个字符进行预检查如果符合条件就向下读取并记录
        //    while (!stream.EndOfRead() && function(stream.Next()))
        //    {
        //        str += stream.Next();
        //    }
        //    return str;
        //}
        ///// <summary>
        ///// 读取数字字符集
        ///// </summary>
        ///// <returns>数字字符集</returns>
        //private Token ReadNumber()
        //{
        //    var has_dot = false;
        //    var number = ReadWhile(new Predicate<char>(
        //    delegate (char ch)
        //    {
        //        if (ch == '.')
        //        {
        //            if (has_dot) return false;
        //            has_dot = true;
        //            return true;
        //        }
        //        return IsDigit(ch);
        //    }
        //    ));
        //    return new Token(new Range(stream.Pos, stream.Pos), "num", number);
        //}
        ///// <summary>
        ///// 是否为数字
        ///// </summary>
        ///// <param name="ch">需要判断的字符</param>
        ///// <returns>是数字则返回真</returns>
        //private bool IsDigitK(char ch)
        //{
        //    return Regex.IsMatch(ch + "", "[0-9]");
        //}
        ///// <summary>
        ///// 读取字符串
        ///// </summary>
        ///// <returns>字符串的字符集</returns>
        //private Token ReadString()
        //{
        //    return new Token(new Range(stream.Pos, stream.Pos), "str", read_escaped('\"'));
        //}
        ///// <summary>
        ///// 是否读取到了结束字符
        ///// </summary>
        ///// <param name="end">结束字符</param>
        //private string read_escaped(char end)
        //{
        //    bool escaped = false;
        //    string str = "";
        //    stream.Next();
        //    while (!stream.EndOfRead())
        //    {
        //        var ch = stream.Next();
        //        if (escaped)
        //        {
        //            str += ch;
        //            escaped = false;
        //        }
        //        else if (ch == '\\')
        //        {
        //            escaped = true;
        //        }
        //        else if (ch == end)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            str += ch;
        //        }
        //    }
        //    return str;
        //}
    }
}
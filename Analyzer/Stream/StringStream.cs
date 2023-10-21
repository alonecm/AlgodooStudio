using AlgodooStudio.Analyzer.Syntax.Exception;
using AlgodooStudio.Base;

namespace AlgodooStudio.Analyzer.Stream
{
    /// <summary>
    /// 从给定的字符串中按字符流形式读取字符
    /// </summary>
    internal sealed class StringStream
    {
        /// <summary>
        /// 字符所在位置
        /// </summary>
        private int pos = 0;

        /// <summary>
        /// 行号
        /// </summary>
        private int line = 1;

        /// <summary>
        /// 列号
        /// </summary>
        private int col = 0;

        /// <summary>
        /// 需要被解析的字符串
        /// </summary>
        private string content;

        /// <summary>
        /// 流运行到的位置
        /// </summary>
        public int Pos { get => pos; }

        /// <summary>
        /// 流当前的字符
        /// </summary>
        public char Char { get => content[pos]; }

        internal StringStream(string content)
        {
            this.content = content;
        }

        /// <summary>
        /// 通过给定的索引获取内容的字符
        /// </summary>
        /// <param name="index">提供的索引</param>
        /// <returns>索引对应的字符</returns>
        public char GetChar(int index)
        {
            return content[index];
        }

        /// <summary>
        /// 读取当前字符并移动到下一个位置
        /// </summary>
        /// <returns>读取到的字符</returns>
        public char Next()
        {
            var ch = content[pos++];
            if (ch == '\n')
            {
                line++;
                col = 0;
            }
            else
            {
                col++;
            }
            return ch;
        }

        /// <summary>
        /// 判断是否结束了流的读取
        /// </summary>
        /// <returns>如果到了结束部分则返回真</returns>
        public bool EndOfRead()
        {
            return Now() == '\0';
        }

        /// <summary>
        /// 读取下一个位置的字符
        /// </summary>
        /// <returns>读取到的字符</returns>
        public char Peek()
        {
            return content[pos + 1];
        }

        /// <summary>
        /// 读取当前位置的字符
        /// </summary>
        /// <returns>读取到的字符</returns>
        public char Now()
        {
            if (pos<content.Length)
            {
                return content[pos];
            }
            else
            {
                return '\0';
            }
            
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="msg">异常描述</param>
        /// <param name="errorCode">异常代码</param>
        /// <returns>语法异常</returns>
        public SyntaxException ThrowException(string msg, string errorCode)
        {
            return new SyntaxException(new Range(line, col), errorCode, msg);
        }
    }
}
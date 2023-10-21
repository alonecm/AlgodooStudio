using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlgodooStudio.Analyzer.Syntax.Exception;
using AlgodooStudio.Analyzer.Syntax.TreeNode;
using AlgodooStudio.Attribute;
using AlgodooStudio.Basic;
using AlgodooStudio.Forms.Dialogs;

namespace AlgodooStudio.Analyzer.Syntax
{
    /// <summary>
    /// 语法树分析器，用于生成语法树并对语法树进行完整性检查<br/>
    /// 不是真正编译，只检查运行过程中的错误并记录其位置就行<br/>
    /// 一般在按下检查按钮时执行
    /// </summary>
    internal sealed class SyntaxTreeAnalyzer
    {
        /// <summary>
        /// 语法树根节点
        /// </summary>
        private SyntaxTreeNode rootNode;
        /// <summary>
        /// 字符串构造器
        /// </summary>
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 脚本内容
        /// </summary>
        private string content;
        /// <summary>
        /// 脚本文件内容
        /// </summary>
        private string fileName;
        /// <summary>
        /// 语法树根节点
        /// </summary>
        internal SyntaxTreeNode RootNode { get => rootNode; }
        /// <summary>
        /// 脚本内容
        /// </summary>
        public string Content { get => content; set => content = value; }


        /// <summary>
        /// 创建一个语法分析器
        /// </summary>
        /// <param name="file">脚本文件</param>
        internal SyntaxTreeAnalyzer(FileInfo file)
        {
            sb.Clear();
            using (StreamReader sr=new StreamReader(file.FullName))
            {
                while (sr.Peek()!=-1)
                {
                    sb.Append(sr.ReadLine());
                }
            }
            fileName = Path.GetFileNameWithoutExtension(file.Name);
            Content = sb.ToString();
        }
        /// <summary>
        /// 创建一个语法分析器
        /// </summary>
        /// <param name="content">脚本内容</param>
        internal SyntaxTreeAnalyzer(string fileName,string content)
        {
            this.fileName = fileName;
            this.Content = content;
        }

        /*
         1.生成指定的语法树
         2.生成过程中检查是否存在“大括号”缺少的位置、“小括号”缺少的位置、“方括号”缺少的位置
         3.生成过程中检查是否存在左右缺少内容的“运算符”的位置
         3.生成过程中检查是否存在“不标准运算符”的位置
         */
        
        /// <summary>
        /// 生成给定内容的语法树
        /// </summary>
        /// <param name="scriptContents">脚本内容</param>
        internal void GenerateTree()
        {
            SyntaxException[] e = GetGroupExceptions();
            string ea = "";
            foreach (var item in e)
            {
                ea += item.ToString()+"\r\n";
            }
            MBox.Showlog(ea);
        }
        /// <summary>
        /// 获取全文大括号凑组的异常集
        /// </summary>
        /// <returns>凑组的异常集</returns>
        internal SyntaxException[] GetGroupExceptions()
        {
            MatchCollection branches = Regex.Matches(content, @"{|}");
            //等待凑组的括号
            Stack<Tuple<string, int>> waitingGroup = new Stack<Tuple<string, int>>();
            bool flag = false;//组队异常
            //凑组开始
            foreach (Match v in branches)
            {
                if (v.Value == "{")
                {
                    waitingGroup.Push(new Tuple<string, int>("{", v.Index));
                }
                else
                {
                    //检查是否存在凑组异常的问题
                    if (waitingGroup.Count > 0)
                    {
                        if (waitingGroup.Peek().Item1 == "{")
                        {
                            waitingGroup.Pop();
                        }
                        else
                        {
                            waitingGroup.Push(new Tuple<string, int>("}", v.Index));
                        }
                    }
                    else
                    {
                        flag = true;
                        waitingGroup.Push(new Tuple<string, int>("}", v.Index));
                        break;
                    }
                }
            }
            //组队异常
            if (flag)
            {
                return new SyntaxException[] { new SyntaxException(waitingGroup.Peek().Item2, "{","缺失") };
            }
            else
            {
                if (waitingGroup.Count > 0)
                {
                    int i = waitingGroup.Count-1;
                    SyntaxException[] e = new SyntaxException[waitingGroup.Count];
                    foreach (var item in waitingGroup)
                    {
                        e[i] = new SyntaxException(item.Item2,"}", "字符无法组队");
                        i--;
                    }
                    return e;
                }
                else
                {
                    return new SyntaxException[0];
                }
            }
        }
        /*
          语法标准：
          方法: MMM = (X,X) => {}; √ | MMM = {};  √   
          类:   CCC = {_=alloc;_->{}}; √
          结构: SSS = {_=alloc;}; √
          数组：AAA = []; √
          判断：JJJ ? {}:{}; √
          循环：for (n,(x)=>{}); √
          设置：TTT -> {}; √
          对象: OOO{}; √ (scene|Scene).add[a-zA-Z]+

          "无参方法|类|结构|判断" 使用={    
          "有参方法|设置" 使用>{
          "判断" 使用?{
          "对象" 使用{

          "循环" 使用for(
          "数组" 使用=[
        */

        //private MatchCollection 
    }
}

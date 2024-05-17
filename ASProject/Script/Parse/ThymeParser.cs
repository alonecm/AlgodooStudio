using AlgodooStudio.ASProject.Script.Parse.Expr;
using Dex.Analysis.Parse;
using Dex.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlgodooStudio.ASProject.Script.Parse
{
    /// <summary>
    /// Thyme语法提取器
    /// </summary>
    public sealed class ThymeParser : Parser<ThymeToken, ThymeSyntaxNode>
    {
        public ThymeParser(ThymeTokenCollection tokens) : base(tokens)
        {
            
        }

        public override ThymeSyntaxNode Parse()
        {
            var nodes = new List<ThymeSyntaxNode>();
            var currentTokenCount = 0;
            while (!IsEnd)
            {
                var node = ParseMain(ref currentTokenCount);
                
                //出现空表达式则报错
                if (node is EmptyExpression e)
                {
                    ReportEmptyExpression(e.Token);
                    GoEnd();
                    return null;
                }

                //未停止则继续执行
                if (!IsEnd)
                {
                    //当前是分号创建行并继续
                    if (Current.Value == ";")
                    {
                        var jump = Next(ref currentTokenCount);
                        nodes.Add(new Line(node));
                        currentTokenCount = 0;
                    }
                    else
                    {
                        //下一个字符如果不是分号且后续不为空则报错
                        if (!(Preview is null) && Preview.Value != ";")
                        {
                            ReportMissing(";", Preview.Range);
                            GoEnd();
                            break;
                        }
                    }
                }
                else
                {
                    //停止则直接保存并结束
                    nodes.Add(new Line(node));
                    currentTokenCount = 0;
                    break;
                }
            }
            return new Root(nodes.ToArray());
        }

        protected new ThymeToken Next(ref int currentTokenCount)
        {
            currentTokenCount++;
            return base.Next();
        }

        /// <summary>
        /// 主要转换
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseMain(ref int currentTokenCount)
        {
            return ParseAssignment(ref currentTokenCount);
        }

        /// <summary>
        /// 转换赋值
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseAssignment(ref int currentTokenCount)
        {
            //解析左侧
            var left = ParseIfStatement(ref currentTokenCount);

            //左侧为标识符且当前为赋值或重定向则进行解析
            if (!IsEnd && (Current.Value == "=" || Current.Value == ":=" || Current.Value == "->"))
            {
                if (currentTokenCount > 1)
                {
                    ReportError(new ThymeDiagnostic($"左侧令牌太多！(应为1个，现为{currentTokenCount}个)", Review.Range));
                    GoEnd();
                    return null;
                }

                if (left is Identifier)
                {
                    switch (Current.Value)
                    {
                        case "=":
                            var eq = Next(ref currentTokenCount);
                            return new Assign(left, ParseAssignment(ref currentTokenCount));
                        case ":=":
                            var point = Next(ref currentTokenCount);
                            return new NewAssign(left, ParseAssignment(ref currentTokenCount));
                        case "->":
                            var re = Next(ref currentTokenCount);
                            if (IsEnd || Current.Value != "{")
                            {
                                ReportMissing("{", Review.Range);
                                GoEnd();
                                return null;
                            }
                            return new Redirect(left, ParseBlock(ref currentTokenCount));
                    }
                }
                else
                {
                    ReportError(new ThymeDiagnostic($"不能对 '{left.Type}' 赋值", Preview.Range));
                    GoEnd();
                    return null;
                }
            }
            return left;
        }

        /// <summary>
        /// 转换条件表达式
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseIfStatement(ref int currentTokenCount)
        {
            //TODO: 需要不允许冒号间的节点深度超过1
            var expr1 = ParseExpression(ref currentTokenCount);
            if (!IsEnd && Current.Value == "?")//如果表达式后有问号则证明是三元表达式
            {
                var question = Next(ref currentTokenCount);

                if (IsEnd)//缺失表达式2
                {
                    ReportMissing("expression2", Review.Range);
                    GoEnd();
                    return null;
                }
                var expr2 = ParseAssignment(ref currentTokenCount);

                if (IsEnd || Current.Value != ":")//缺少冒号或结束了则报错
                {
                    ReportMissing(":", Review.Range);
                    GoEnd();
                    return null;
                }
                var colon = Next(ref currentTokenCount);

                if (IsEnd)//缺失表达式3
                {
                    ReportMissing("expression3", Review.Range);
                    GoEnd();
                    return null;
                }
                var expr3 = ParseAssignment(ref currentTokenCount);

                return new Ifstatement(expr1, expr2, expr3);
            }

            //后面啥也不是则按表达式处理
            return expr1;
        }

        /// <summary>
        /// 转换表达式
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseExpression(ref int currentTokenCount, int p = 0)
        {
            ThymeSyntaxNode left;
            var unary = Current.GetUnaryPriority();
            if (unary != 0 && p <= unary)//让高优先级字符参与，同时确保运算符是存在的
            {
                var op = Next(ref currentTokenCount);//获取运算符
                var expr = ParseExpression(ref currentTokenCount);//获取元素
                left = new UnaryExpression(op, expr);
            }
            else
            {
                left = ParsePrimary(ref currentTokenCount);//如果不是一元运算符则直接获取元素
            }


            //如果给出的左侧是数组且后方有追加符，则是数组合并
            if (!IsEnd && Current.Value == "++")
            {
                var pp = Next(ref currentTokenCount);
                var right = ParseExpression(ref currentTokenCount);
                return new ArrayCombine(left, right);
            }


            //成员调用，检查是否满足成员调用的要求（左侧是小括号或标识符，未结束，当前是点号）
            if ((left is Identifier || left is BraceExpression || left is Call) && !IsEnd && Current.Value == ".")
            {
                //右侧非空，是标识符
                if (!(Preview is null) && (Preview.Type == "identifier"))
                {
                    currentTokenCount--;//这两个是为了保证成员调用只识别为1个令牌而不是多个
                    currentTokenCount--;
                    var point = Next(ref currentTokenCount);
                    return new MemberCall(left, ParseAssignment(ref currentTokenCount));
                }
                else
                {
                    ReportError(new ThymeDiagnostic("成员调用异常！", Current.Range));
                    GoEnd();
                    return null;
                }
            }


            while (!IsEnd)//在保证未结束的情况下继续执行这个循环
            {
                var binary = Current.GetBinaryPriority();
                if (binary == 0 || binary <= p)//非二元运算符或这个二元运算符优先级比父运算符优先级低则结束
                    break;
                var op = Next(ref currentTokenCount);//记录运算符
                var right = ParseExpression(ref currentTokenCount, binary);
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        /// <summary>
        /// 转换函数
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseFunction(ref int currentTokenCount)
        {
            var brace = ParseBrace(ref currentTokenCount);
            //未结束且看是否符合函数的定义格式
            if (!IsEnd && Current.Value == "=>")
            {
                //参数数组检查
                var allowBuildFlag = true;//符合函数构建条件变为true，默认是符合
                if (brace is BraceExpression b && !(b.Node is Identifier))
                {
                    allowBuildFlag = false;//如果括号表达式中的节点不是标识符则不允许构建
                }
                else
                {
                    //如果是数组则挨着看，否则认为允许构建
                    if (brace is Expr.Array arr)
                    {
                        foreach (var item in arr.Nodes)
                        {
                            //如果数组中的元素存在非标识符则不允许构建函数
                            if (!(item is Identifier))
                            {
                                allowBuildFlag = false;
                            }
                        }
                    }
                }
                //允许构建函数
                if (allowBuildFlag)
                {
                    var def = Next(ref currentTokenCount);
                    //看是否缺少括号
                    if (!IsEnd && Current.Value == "{")
                    {
                        var block = ParseBlock(ref currentTokenCount);
                        Params @params = null;
                        switch (brace)
                        {
                            case Expr.Array a: @params = new Params(a.Nodes); break;
                            case BraceExpression be: @params = new Params(new ThymeSyntaxNode[] { be.Node }); break;
                            default: break;
                        }
                        return new Function(@params, block);
                    }
                    else
                    {
                        ReportMissing("{", Review.Range);
                        GoEnd();
                        return null;
                    }
                }
                else
                {
                    //不允许则证明存在非标识符
                    ReportError(new ThymeDiagnostic("参数列表中存在非标识符！", Review.Range));
                    GoEnd();
                    return null;
                }
            }
            return brace;
        }

        /// <summary>
        /// 转换大括号
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseBlock(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var block_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();
            //如果直接结束了则报错
            if (IsEnd)
            {
                ReportMissing("}", Review.Range);
                GoEnd();
                return null;
            }

            while (true)
            {
                //如果直接结束了则结束
                if (Current.Value == "}")
                {
                    var end = Next(ref block_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseMain(ref block_CurrentTokenCount);

                //出现空表达式则报错
                if (node is EmptyExpression e)
                {
                    ReportEmptyExpression(e.Token);
                    GoEnd();
                    return null;
                }

                //未停止则持续读取
                if (!IsEnd)
                {
                    //碰上分号则换行
                    if (Current.Value == ";")
                    {
                        var jump = Next(ref block_CurrentTokenCount);//略过
                        nodes.Add(new Line(node));
                        block_CurrentTokenCount = 0;
                    }
                    //碰上大括号则结束
                    else if (Current.Value == "}")
                    {
                        var end = Next(ref block_CurrentTokenCount);//略过
                        nodes.Add(new Line(node));
                        break;
                    }
                    //碰上其他则报错
                    else
                    {
                        ReportMissing(";", Review.Range);
                        GoEnd();
                        return null;
                    }
                }
                else
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing("}", Review.Range);
                    GoEnd();
                    return null;
                }
            }

            //如果出现了函数定义符号则报错
            if (!IsEnd && Current.Value == "=>")
            {
                ReportError(new ThymeDiagnostic($"形参异常！", Review.Range));
                GoEnd();
                return null;
            }

            return new Block(nodes.ToArray());
        }

        /// <summary>
        /// 转换数组
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseArray(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var array_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();

            //如果直接结束了则报错
            if (IsEnd)
            {
                ReportMissing("]", Review.Range);
                GoEnd();
                return null;
            }

            while (true)
            {
                //如果直接结束了则结束
                if (Current.Value == "]")
                {
                    var end = Next(ref array_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseMain(ref array_CurrentTokenCount);

                //出现空表达式则报错
                if (node is EmptyExpression e)
                {
                    ReportEmptyExpression(e.Token);
                    GoEnd();
                    return null;
                }

                //未停止则持续读取
                if (!IsEnd)
                {
                    //碰上逗号则略过
                    if (Current.Value == ",")
                    {
                        var jump = Next(ref array_CurrentTokenCount);//略过
                        nodes.Add(node);
                    }
                    //碰上方括号说明是结束了
                    else if (Current.Value == "]")
                    {
                        var end = Next(ref array_CurrentTokenCount);//略过
                        nodes.Add(node);
                        break;
                    }
                }
                else
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing("]", Review.Range);
                    GoEnd();
                    return null;
                }
            }

            var arr = new Expr.Array(nodes.ToArray());
            //如果后方未结束且有括号则按照调用来表示
            if (!IsEnd)
            {
                if (Current.Value == "=>") //如果出现了函数定义符号则报错
                {
                    ReportError(new ThymeDiagnostic($"形参异常！", Review.Range));
                    GoEnd();
                    return null;
                }
                if (Current.Value == "[") //如果当前还有数组则按照数组群组调用来表示
                {
                    var arrCall = ParseArray(ref currentTokenCount);
                    return new ArrayCall(arr, arrCall);
                }
                if (Current.Value=="(")
                {
                    var idx = ParseBrace(ref currentTokenCount);
                    return new Call(arr, idx);
                }
            }
            //如果都不满足则按普通数组表示
            return arr;
        }

        /// <summary>
        /// 转换小括号（包圆全部的小括号相关方面）
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseBrace(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var brace_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();

            //如果直接结束了则报错
            if (IsEnd)
            {
                ReportMissing(")", Review.Range);
                GoEnd();
                return null;
            }

            while (true)
            {
                //如果直接结束了则结束
                if (Current.Value == ")")
                {
                    var end = Next(ref brace_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseMain(ref brace_CurrentTokenCount);

                //出现空表达式则报错
                if (node is EmptyExpression e)
                {
                    ReportEmptyExpression(e.Token);
                    GoEnd();
                    return null;
                }
              
                //未停止则持续读取
                if (!IsEnd)
                {
                    //碰上逗号则是参数或数组
                    if (Current.Value == "," )
                    {
                        var jump = Next(ref brace_CurrentTokenCount);//略过
                        nodes.Add(node);
                    }
                    //碰上小括号说明是结束了
                    else if (Current.Value == ")")
                    {
                        var end = Next(ref brace_CurrentTokenCount);//略过
                        nodes.Add(node);
                        break;
                    }
                }
                else
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing(")", Review.Range);
                    GoEnd();
                    return null;
                }
            }

            ThymeSyntaxNode temp;
            //元素为0或大于1则是数组否则为小括号
            if (nodes.Count == 0 || nodes.Count > 1)
            {
                //输出数组
                temp = new Expr.Array(nodes.ToArray());
            }
            else
            {
                //输出小括号
                temp = new BraceExpression(nodes[0]);
            }

            //如果后方未结束且有括号则按照调用来表示
            if (!IsEnd)
            {
                if (temp is Expr.Array)//小括号的数组用方括号有效
                {
                    if (Current.Value == "[")
                    {
                        var idx = ParseArray(ref currentTokenCount);
                        return new ArrayCall(temp, idx);
                    }
                    if (Current.Value == "(")
                    {
                        var idx = ParseBrace(ref currentTokenCount);
                        return new Call(temp, idx);
                    }
                }
                else
                {
                    if (Current.Value == "(")
                    {
                        var idx = ParseBrace(ref currentTokenCount);
                        return new Call(temp, idx);
                    }
                }
            }

            return temp;
        }

        /// <summary>
        /// 转换基本
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParsePrimary(ref int currentTokenCount)
        {
            switch (Current.Type)
            {
                //符号
                case "s_symbol":
                case "m_symbol":
                    switch (Current.Value)
                    {
                        case "(": return ParseFunction(ref currentTokenCount);
                        case "[": return ParseArray(ref currentTokenCount);
                        case "{": return ParseBlock(ref currentTokenCount);
                        case "∞": return new Inf(Next(ref currentTokenCount));
                        case ",": case ";": return new EmptyExpression(Next(ref currentTokenCount));
                        default:
                            ReportErrorSymbol(Current);
                            GoEnd(); 
                            return null;
                    }

                //字面量
                case "number":
                case "bool":
                case "string":
                    return new Literal(Next(ref currentTokenCount));

                //标识符
                case "identifier":
                    var id = new Identifier(Next(ref currentTokenCount));
                    if (!IsEnd)
                    {
                        //函数调用
                        switch (Current.Value)
                        {
                            case "(":
                            case "{":
                            case "[":
                                return new Call(id, ParsePrimary(ref currentTokenCount));
                        }
                        //函数调用
                        switch (Current.Type)
                        {
                            case "alloc":
                            case "keyword":
                            case "number":
                            case "bool":
                            case "string":
                            case "identifier":
                                return new Call(id, ParsePrimary(ref currentTokenCount));
                        }
                    }
                    return id;

                //关键字
                case "keyword":
                    switch (Current.Value)
                    {
                        case "null": return new Null(Next(ref currentTokenCount));
                        case "NaN": return new NaN(Next(ref currentTokenCount));
                        default: throw new Exception($"未定义关键词 '{Current.Value}'");
                    }

                //特殊词
                case "alloc": return new Alloc(Next(ref currentTokenCount));
                case "inf": return new Inf(Next(ref currentTokenCount));


                //不支持的格式
                case "UnsupportSymbol":
                    return new UnsupportSymbol(Next(ref currentTokenCount));

                default:
                    throw new Exception($"未定义类型 '{Current.Type}'");
            }
        }

        /// <summary>
        /// 汇报期待值
        /// </summary>
        /// <param name="value"></param>
        private void ReportExpected(string value)
        {
            ReportError(new ThymeDiagnostic($"期待 '{value}', 不期待 '{Current.Value}'", Current.Range));
        }

        /// <summary>
        /// 汇报缺失值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="range"></param>
        private void ReportMissing(string value, Range range)
        {
            ReportError(new ThymeDiagnostic($"缺少 '{value}'", range));
        }

        /// <summary>
        /// 汇报不正确使用字符
        /// </summary>
        /// <param name="token"></param>
        private void ReportErrorSymbol(ThymeToken token)
        {
            ReportError(new ThymeDiagnostic($"不正确使用字符 '{token.Value}' ", token.Range));
        }

        /// <summary>
        /// 汇报空表达式
        /// </summary>
        /// <param name="token"></param>
        private void ReportEmptyExpression(ThymeToken token)
        {
            ReportError(new ThymeDiagnostic($"'{token.Value}' 前的表达式为空！", token.Range));
        }
    }
}

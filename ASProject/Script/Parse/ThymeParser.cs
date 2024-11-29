using AlgodooStudio.ASProject.Script.Parse.Expr;
using Dex.Analysis.Parse;
using Dex.Common;
using Dex.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Array = AlgodooStudio.ASProject.Script.Parse.Expr.Array;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public sealed class ThymeParser : Parser<ThymeToken, ThymeSyntaxNode>
    {
        public ThymeParser(ThymeTokenCollection tokens) : base(tokens)
        {
        }

        public override ThymeSyntaxNode Parse()
        {
            return ParseRoot();
        }

        /// <summary>
        /// 转换根节点
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseRoot()
        {
            var start = Current;
            var nodes = new List<ThymeSyntaxNode>();
            var currentTokenCount = 0;
            while (!IsEnd)
            {
                var node = ParseAssignment(ref currentTokenCount);

                //未停止则继续执行
                if (IsEnd)
                {
                    //停止则直接保存并结束
                    nodes.Add(node);
                    break;
                }
                else
                {
                    //当前是分号创建行并继续
                    if (Current.Value == ";")
                    {
                        var jump = Next(ref currentTokenCount);
                        nodes.Add(node);
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
            }
            if (this.Pos!=0)
            {
                return new Root(nodes.ToArray(), new Range(0, Review.Range.Max));
            }
            else
            {
                return new Root(nodes.ToArray(), new Range(0, 0));
            }
        }

        /// <summary>
        /// 转换元素
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseElement(ref int currentTokenCount)
        {
            switch (Current.Type)
            {
                //基本元素
                case "number":
                case "bool":
                case "string":
                    return new Literal(Next(ref currentTokenCount));

                //符号
                case "s_symbol":
                case "m_symbol":
                    switch (Current.Value)
                    {
                        case "(": return ParseFunction(ref currentTokenCount);
                        case "[": return ParseArray(ref currentTokenCount);
                        case "{": return ParseBlock(ref currentTokenCount);
                        case "!": return ParseExpression(ref currentTokenCount);

                        case "∞": return new Inf(Next(ref currentTokenCount));

                        case ",":
                        case ";":
                            var tk = Current;
                            ReportEmptyExpression(tk);
                            GoEnd();
                            return new Symbol(tk); ;

                        default:
                            tk = Current;
                            ReportErrorSymbol(tk);
                            GoEnd();
                            return new Symbol(tk);
                    }

                case "keyword":
                    switch (Current.Value)
                    {
                        case "null": return new Null(Next(ref currentTokenCount));
                        case "NaN": return new NaN(Next(ref currentTokenCount));
                        default: throw new NotImplementedException($"关键字 {Current.Value} 未在 {this.GetType().Name} 中定义");
                    }

                case "identifier":
                    var identifier = new Identifier(Next(ref currentTokenCount));
                    if (!IsEnd)
                    {
                        //标识符调用式
                        switch (Current.Value)
                        {
                            case "(":
                            case "[":
                                var es = ParseElement(ref currentTokenCount);
                                if (es is Array array)
                                {
                                    return new IdentifierCall(identifier, new RealParams(array.Range, array.Nodes), new Range(identifier.Range.Min, array.Range.Max));
                                }
                                else if (es is BraceExpression braceExpression)
                                {
                                    return new IdentifierCall(identifier, new RealParams(braceExpression.Range,braceExpression.Node),new Range(identifier.Range.Min,braceExpression.Range.Max));
                                }
                                break;
                        }
                    }
                    return identifier;

                case "alloc": return new Alloc(Next(ref currentTokenCount));
                case "inf": return new Inf(Next(ref currentTokenCount));
                case "UnsupportSymbol": return new UnsupportSymbol(Next(ref currentTokenCount));

                default:
                    throw new NotImplementedException($"元素类型 {Current.Type} 未在{this.GetType().Name}中定义");
            }
        }

        /// <summary>
        /// 转换表达式
        /// </summary>
        /// <param name="currentTokenCount"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private ThymeSyntaxNode ParseExpression(ref int currentTokenCount, int p = 0)
        {
            ThymeSyntaxNode left;
            var unary = Current.GetUnaryPriority();
            if (unary != 0 && p <= unary)//让高优先级字符参与，同时确保运算符是存在的
            {
                var op = Next(ref currentTokenCount);//获取运算符
                if (IsEnd)//此处结束报错
                {
                    ReportMissing("右侧表达式", op.Range);
                    return null;
                }
                var expr = ParseExpression(ref currentTokenCount);//获取元素
                left = new UnaryExpression(op, expr, new Range(op.Range.Min, expr.Range.Max));
            }
            else
            {
                left = ParseElement(ref currentTokenCount);//如果不是一元运算符则直接获取元素
            }

            //如果就这么结束了则返回
            if (IsEnd)
            {
                return left;
            }

            switch (Current.Value)
            {
                case "++":
                    //如果给出的左侧是数组且后方有追加符，则是数组合并
                    var pp = Next(ref currentTokenCount);
                    if (IsEnd)
                    {
                        ReportMissing("右侧表达式", pp.Range);
                        return null;
                    }
                    var right = ParseAssignment(ref currentTokenCount);
                    return new ArrayCombine(left, right, new Range(left.Range.Min, right.Range.Max));
                case ".":
                    currentTokenCount--;
                    currentTokenCount--;
                    var point = Next(ref currentTokenCount);
                    if (IsEnd)//缺失表达式2
                    {
                        ReportMissing("子成员", point.Range);
                        return null;
                    }
                    var member = ParseAssignment(ref currentTokenCount);
                    return new MemberCall(left, member, new Range(left.Range.Min, member.Range.Max));
                case "?":
                    //三元表达式
                    var question = Next(ref currentTokenCount);
                    if (IsEnd)//缺失表达式2
                    {
                        ReportMissing("expression2", question.Range);
                        return null;
                    }
                    var expr2 = ParseAssignment(ref currentTokenCount);

                    //结束了则报错
                    if (IsEnd)
                    {
                        ReportMissing(":", Review.Range);
                        return null;
                    }
                    //缺少冒号则报错
                    if (Current.Value != ":")
                    {
                        ReportMissing(":", Review.Range);
                        GoEnd();
                        return null;
                    }

                    var colon = Next(ref currentTokenCount);
                    if (IsEnd)//缺失表达式3
                    {
                        ReportMissing("expression3", colon.Range);
                        return null;
                    }
                    var expr3 = ParseAssignment(ref currentTokenCount);
                    return new Ifstatement(left, expr2, expr3, new Range(left.Range.Min, expr3.Range.Max));
                default:
                    break;
            }

            //如果就这么结束了则返回
            if (IsEnd)
            {
                return left;
            }

            while (true)//在保证未结束的情况下继续执行这个循环
            {
                var binary = Current.GetBinaryPriority();
                if (binary == 0 || binary <= p)//非二元运算符或这个二元运算符优先级比父运算符优先级低则结束
                    break;
                var op = Next(ref currentTokenCount);//记录运算符
                if (IsEnd)//此处结束报错
                {
                    ReportMissing("右侧表达式", op.Range);
                    return null;
                }
                var right = ParseExpression(ref currentTokenCount, binary);
                left = new BinaryExpression(left, op, right, new Range(left.Range.Min, right.Range.Max));
            }

            return left;
        }

        /// <summary>
        /// 转换赋值
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseAssignment(ref int currentTokenCount)
        {
            //解析左侧
            var left = ParseExpression(ref currentTokenCount);

            //停止则返回
            if (IsEnd)
            {
                return left;
            }

            //左侧为标识符且当前为赋值或重定向则进行解析
            if (Current.Value == "=" || Current.Value == ":=" || Current.Value == "->")
            {
                //左侧不是标识符则报错
                if (!(left is Identifier))
                {
                    ReportError(new ThymeDiagnostic($"不能对 '{left.Type}' 赋值", Current.Range));
                    GoEnd();
                    return null;
                }

                if (currentTokenCount > 1)
                {
                    ReportError(new ThymeDiagnostic($"'{Current.Value}'左侧令牌太多！(应为1个，现为{currentTokenCount}个)", Current.Range));
                    GoEnd();
                    return null;
                }

                switch (Current.Value)
                {
                    case "=":
                        var eq = Next(ref currentTokenCount);
                        if (IsEnd)
                        {
                            ReportMissing("右侧的值", eq.Range);
                            return null;
                        }
                        var assign = ParseAssignment(ref currentTokenCount);
                        return new Assign(left, assign, new Range(left.Range.Min, assign.Range.Max));
                    case ":=":
                        var point = Next(ref currentTokenCount);
                        if (IsEnd)
                        {
                            ReportMissing("右侧的值", point.Range);
                            return null;
                        }
                        assign = ParseAssignment(ref currentTokenCount);
                        return new NewAssign(left, assign, new Range(left.Range.Min, assign.Range.Max));
                    case "->":
                        var re = Next(ref currentTokenCount);
                        if (IsEnd || Current.Value != "{")
                        {
                            ReportMissing("{", Review.Range);
                            GoEnd();
                            return null;
                        }
                        var block = ParseBlock(ref currentTokenCount);
                        return new Redirect(left, block, new Range(left.Range.Min, block.Range.Max));
                }
            }
            return left;
        }

        /// <summary>
        /// 转换块
        /// </summary>
        /// <param name="currentTokenCount"></param>
        /// <returns></returns>
        private ThymeSyntaxNode ParseBlock(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var block_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();
            ThymeToken end;
            while (true)
            {
                //如果直接结束了则报错
                if (IsEnd)
                {
                    ReportMissing("}", Review.Range);
                    return null;
                }

                //找到结尾大括号则结束块解析
                if (Current.Value == "}")
                {
                    end = Next(ref block_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseAssignment(ref block_CurrentTokenCount);

                //未停止则持续读取
                if (IsEnd)
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing("}", Review.Range);
                    return null;
                }

                //碰上分号则换行
                if (Current.Value == ";")
                {
                    var jump = Next(ref block_CurrentTokenCount);//略过
                    nodes.Add(node);
                    block_CurrentTokenCount = 0;
                }
                //碰上大括号则结束
                else if (Current.Value == "}")
                {
                    end = Next(ref block_CurrentTokenCount);//略过
                    nodes.Add(node);
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

            return new Block(nodes.ToArray(), new Range(start.Range.Min, end.Range.Max));
        }

        /// <summary>
        /// 转换数组
        /// </summary>
        /// <param name="currentTokenCount"></param>
        /// <returns></returns>
        private ThymeSyntaxNode ParseArray(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var array_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();
            ThymeToken end;

            while (true)
            {
                //如果直接结束了则报错
                if (IsEnd)
                {
                    ReportMissing("]", Review.Range);
                    return null;
                }

                //如果直接结束了则结束
                if (Current.Value == "]")
                {
                    end = Next(ref array_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseAssignment(ref array_CurrentTokenCount);

                //停止则报错
                if (IsEnd)
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing("]", Review.Range);
                    return null;
                }

                //碰上逗号则略过
                if (Current.Value == ",")
                {
                    var jump = Next(ref array_CurrentTokenCount);//略过
                    nodes.Add(node);
                    continue;
                }

                //碰上方括号说明是结束了
                if (Current.Value == "]")
                {
                    end = Next(ref array_CurrentTokenCount);//略过
                    nodes.Add(node);
                    break;
                }
            }

            var arr = new Array(nodes.ToArray(), new Range(start.Range.Min, end.Range.Max));

            //如果后方未结束且有括号则按照调用来表示
            if (IsEnd)
            {
                return arr;
            }

            switch (Current.Value)
            {
                case "[":
                    var followArray = ParseArray(ref currentTokenCount);
                    return new ArrayWithArrayCall(arr, followArray, new Range(arr.Range.Min, followArray.Range.Max));
                case "(":
                    var followBrace = ParseBrace(ref currentTokenCount);
                    return new ArrayWithBraceCall(arr, followBrace, new Range(arr.Range.Min, followBrace.Range.Max));
                default:
                    return arr;
            }
        }

        /// <summary>
        /// 转换小括号
        /// </summary>
        /// <param name="currentTokenCount"></param>
        /// <returns></returns>
        private ThymeSyntaxNode ParseBrace(ref int currentTokenCount)
        {
            var start = Next(ref currentTokenCount);//略过首括号
            var brace_CurrentTokenCount = 0;
            var nodes = new List<ThymeSyntaxNode>();
            ThymeToken end;

            while (true)
            {
                //如果直接结束了则报错
                if (IsEnd)
                {
                    ReportMissing(")", Review.Range);
                    return null;
                }

                //如果直接结束了则结束
                if (Current.Value == ")")
                {
                    end = Next(ref brace_CurrentTokenCount);//略过
                    break;
                }

                var node = ParseAssignment(ref brace_CurrentTokenCount);

                if (IsEnd)
                {
                    //如果未退出还结束了则缺少内容
                    ReportMissing(")", Review.Range);
                    return null;
                }

                //碰上逗号则是参数或数组
                if (Current.Value == ",")
                {
                    var jump = Next(ref brace_CurrentTokenCount);//略过
                    nodes.Add(node);
                    continue;
                }
                //碰上小括号说明是结束了
                if (Current.Value == ")")
                {
                    end = Next(ref brace_CurrentTokenCount);//略过
                    nodes.Add(node);
                    break;
                }
            }

            ThymeSyntaxNode brace;

            if (nodes.Count != 1)
            {
                brace = new Array(nodes.ToArray(), new Range(start.Range.Min, end.Range.Max));//输出为小括号数组
            }
            else
            {
                brace = new BraceExpression(nodes[0], new Range(start.Range.Min, end.Range.Max));
            }

            if (IsEnd)
            {
                return brace;
            }

            switch (Current.Value)
            {
                case "[":
                    var followArray = ParseArray(ref currentTokenCount);
                    return new BraceWithArrayCall(brace, followArray, new Range(brace.Range.Min, followArray.Range.Max));
                case "(":
                    var followBrace = ParseBrace(ref currentTokenCount);
                    return new BraceWithBraceCall(brace, followBrace, new Range(brace.Range.Min, followBrace.Range.Max));
                default:
                    return brace;
            }
        }

        /// <summary>
        /// 转换函数
        /// </summary>
        /// <returns></returns>
        private ThymeSyntaxNode ParseFunction(ref int currentTokenCount)
        {
            var brace = ParseBrace(ref currentTokenCount);

            if (IsEnd)
            {
                return brace;
            }

            //未结束且看是否符合函数的定义格式
            if (Current.Value == "=>")
            {
                switch (brace)
                {
                    case Array array:
                        //挨着看，否则认为允许构建
                        foreach (var item in array.Nodes)
                        {
                            //如果数组中的元素存在非标识符则不允许构建函数
                            if (item is Identifier)
                            {
                                continue;
                            }
                            //不允许则证明存在非标识符
                            ReportError(new ThymeDiagnostic("参数列表中存在非标识符！", Review.Range));
                            GoEnd();
                            return null;
                        }
                        break;
                    case BraceExpression braceExpression:
                        if (!(braceExpression.Node is Identifier))
                        {
                            //不允许则证明存在非标识符
                            ReportError(new ThymeDiagnostic("参数列表中存在非标识符！", Review.Range));
                            GoEnd();
                            return null;
                        }
                        break;
                }
                //允许构建函数
                var def = Next(ref currentTokenCount);
                //看是否缺少括号
                if (IsEnd || Current.Value != "{")
                {
                    ReportMissing("{", Review.Range);
                    GoEnd();
                    return null;
                }
                else
                {
                    Params @params = null;
                    switch (brace)
                    {
                        case Array array:
                            @params = new Params(array.Range, array.Nodes);
                            break;
                        case BraceExpression braceExpression:
                            @params = new Params(braceExpression.Range, braceExpression.Node);
                            break;
                        default:
                            break;
                    }
                    var block = ParseBlock(ref currentTokenCount);
                    return new Function(@params, block, new Range(@params.Range.Min, block.Range.Max));
                }
            }
            return brace;
        }

        /// <summary>
        /// 返回当前令牌并向下移动，令牌数+1
        /// </summary>
        /// <param name="currentTokenCount"></param>
        /// <returns></returns>
        protected ThymeToken Next(ref int currentTokenCount)
        {
            currentTokenCount++;
            return base.Next();
        }

        /// <summary>
        /// 汇报期待值
        /// </summary>
        /// <param name="value"></param>
        private void ReportExpected(string value)
        {
            ReportError(new ThymeDiagnostic($"期待 '{value}', 不期待 '{Current.Value}'", Current.Range, DiagnosticType.Error));
        }

        /// <summary>
        /// 汇报缺失值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="range"></param>
        private void ReportMissing(string value, Range range)
        {
            ReportError(new ThymeDiagnostic($"缺少 '{value}'", range, DiagnosticType.Error));
        }

        /// <summary>
        /// 汇报不正确使用字符
        /// </summary>
        /// <param name="token"></param>
        private void ReportErrorSymbol(ThymeToken token)
        {
            ReportError(new ThymeDiagnostic($"不正确使用字符 '{token.Value}' ", token.Range, DiagnosticType.Error));
        }

        /// <summary>
        /// 汇报空表达式
        /// </summary>
        /// <param name="token"></param>
        private void ReportEmptyExpression(ThymeToken token)
        {
            ReportError(new ThymeDiagnostic($"'{token.Value}' 前的表达式为空！", token.Range, DiagnosticType.Error));
        }

        protected override void GoEnd()
        {
            int tokencount = 0;
            while (!IsEnd)
            {
                Next(ref tokencount);
                if (!(Current is null))
                {
                    switch (Current.Value)
                    {
                        case ";":
                        case ",":
                        case "}":
                        case "]":
                        case ")":
                        case "?":
                        case ":":
                        default:
                            return;
                    }
                }
            }
        }


        /// <summary>
        /// 获取parser对象和由Tokenizer捕获的异常
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Tuple<ThymeParser, DiagnosticsCollection> GetParser(string content)
        {
            var tkiz = ThymeTokenizer.GetTokens(content);
            return new Tuple<ThymeParser, DiagnosticsCollection>(new ThymeParser(tkiz.Item1), tkiz.Item2);
        }

        /// <summary>
        /// 获取AST树和由Parser捕获的异常
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Tuple<Root, DiagnosticsCollection> GetAST(string content)
        {
            var parser = GetParser(content);
            parser.Item1.Diagnostics.AddRange(parser.Item2);//转移异常
            return new Tuple<Root, DiagnosticsCollection>(parser.Item1.Parse() as Root, parser.Item1.Diagnostics);
        }
    }
}
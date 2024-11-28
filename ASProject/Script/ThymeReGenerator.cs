using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Script.Parse.Expr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Array = AlgodooStudio.ASProject.Script.Parse.Expr.Array;
using Block = AlgodooStudio.ASProject.Script.Parse.Expr.Block;

namespace AlgodooStudio.ASProject.Script
{
    //TODO:完善脚本重生成器
    /// <summary>
    /// 脚本重生成
    /// </summary>
    public static class ThymeReGenerator
    {
        /// <summary>
        /// 重生成脚本
        /// </summary>
        /// <param name="node">需要转换的语法节点</param>
        /// <returns></returns>
        public static string ReGenerate(ThymeSyntaxNode node)
        {
            switch (node)
            {
                case Alloc alloc: return "alloc";
                case Array array: return GenerateArray(array);
                case ArrayCombine arrayCombine: return GenerateArrayCombine(arrayCombine);
                case ArrayIndexCall arrayIndexCall: return GenerateArrayIndexCall(arrayIndexCall);
                case ArrayIndexGroupCall arrayIndexGroupCall: return GenerateArrayIndexGroupCall(arrayIndexGroupCall);
                case Assign assign: return GenerateAssign(assign);
                case BinaryExpression binaryExpression: return GenerateBinaryExpression(binaryExpression);
                case Block block: return GenerateBlock(block);
                case BraceArray braceArray: return GenerateBraceArray(braceArray);
                case BraceExpression braceExpression: return GenerateBraceExpression(braceExpression);
                case Function function: return GenerateFunction(function);
                case Identifier identifier: return identifier.Value.Value;
                case IdentifierCall IdentifierCall: return GenerateIdentifierCall(IdentifierCall);
                case Ifstatement ifstatement: return GenerateIFstatement(ifstatement);
                case Inf inf: return "inf";
                case Literal literal: return literal.Value.Value;
                case MemberCall memberCall: return GenerateMemberCall(memberCall);
                case NaN naN: return "NaN";
                case NewAssign newAssign: return GenerateNewAssign(newAssign);
                case Null @null: return "null";
                case Params @params: return GenerateParams(@params);
                case Placeholders placeholders: return placeholders.Type;
                case Redirect redirect: return GenerateRedirect(redirect);
                case Root root: return GenerateRoot(root);
                case UnaryExpression unaryExpression: return GenerateUnaryExpression(unaryExpression);
                case UnsupportSymbol unsupportSymbol: return "[ERROR SYMBOL]";
                default: throw new NotImplementedException($" {node.Type} 未找到能转换的语法节点");
            }
        }

        private static string GenerateBraceArray(BraceArray braceArray)
        {
            var builder = new StringBuilder();
            if (braceArray.Nodes.Length > 0)
            {
                if (braceArray.Nodes.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < braceArray.Nodes.Length - 1; i++)
                    {
                        builder.Append(ReGenerate(braceArray.Nodes[i]) + ",");
                    }
                }
                //添加最后一项
                builder.Append(ReGenerate(braceArray.Nodes[braceArray.Nodes.Length - 1]));
            }
            return builder.ToString();
        }

        private static string GenerateIdentifierCall(IdentifierCall identifier)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(identifier.Identifier));
            builder.Append(identifier.StartToken.Value);
            builder.Append(ReGenerate(identifier.RealParams));
            builder.Append(identifier.EndToken.Value);
            return builder.ToString();
        }

        private static string GenerateArrayIndexCall(ArrayIndexCall arrayIndexCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayIndexCall.Array));
            builder.Append("(");
            builder.Append(ReGenerate(arrayIndexCall.Index));
            builder.Append(")");
            return builder.ToString();
        }

        private static string GenerateUnaryExpression(UnaryExpression unaryExpression)
        {
            var builder = new StringBuilder();
            builder.Append(unaryExpression.Op.Value);
            builder.Append(ReGenerate(unaryExpression.Right));
            return builder.ToString();
        }

        private static string GenerateRoot(Root root)
        {
            if (root.Nodes.Length == 0) return "";

            var builder = new StringBuilder();
            if (root.Nodes.Length > 1)
            {
                //添加前面的项
                for (int i = 0; i < root.Nodes.Length - 1; i++)
                {
                    builder.Append(ReGenerate(root.Nodes[i]) + ";\r\n");
                }
            }
            //添加最后一项
            builder.Append(ReGenerate(root.Nodes[root.Nodes.Length - 1]));
            return builder.ToString();
        }

        private static string GenerateRedirect(Redirect redirect)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(redirect.Name));
            builder.Append(" -> ");
            builder.Append(ReGenerate(redirect.Node));
            return builder.ToString();
        }

        private static string GenerateParams(Params @params)
        {
            var builder = new StringBuilder();
            builder.Append('(');
            if (@params.Tokens.Length > 0)
            {
                if (@params.Tokens.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < @params.Tokens.Length - 1; i++)
                    {
                        builder.Append(ReGenerate(@params.Tokens[i]) + ",");
                    }
                }
                //添加最后一项
                builder.Append(ReGenerate(@params.Tokens[@params.Tokens.Length - 1]));
            }
            builder.Append(')');
            return builder.ToString();
        }

        private static string GenerateMemberCall(MemberCall memberCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(memberCall.Name));
            builder.Append(".");
            builder.Append(ReGenerate(memberCall.Member));
            return builder.ToString();
        }

        private static string GenerateNewAssign(NewAssign newAssign)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(newAssign.Name));
            builder.Append(":=");
            builder.Append(ReGenerate(newAssign.Node));
            return builder.ToString();
        }


        private static string GenerateIFstatement(Ifstatement ifstatement)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(ifstatement.Expr1));
            builder.Append("?");
            builder.Append(ReGenerate(ifstatement.Expr2));
            builder.Append(":");
            builder.Append(ReGenerate(ifstatement.Expr3));
            return builder.ToString();
        }

        private static string GenerateFunction(Function function)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(function.Params));
            builder.Append("=>");
            builder.Append(ReGenerate(function.Block));
            return builder.ToString();
        }


        private static string GenerateBraceExpression(BraceExpression braceExpression)
        {
            var builder = new StringBuilder();
            builder.Append("(");
            builder.Append(ReGenerate(braceExpression.Node));
            builder.Append(")");
            return builder.ToString();
        }

        private static string GenerateBlock(Block block)
        {
            var builder = new StringBuilder();
            builder.Append("{");
            if (block.Nodes.Length > 0)
            {
                if (block.Nodes.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < block.Nodes.Length - 1; i++)
                    {
                        builder.Append(ReGenerate(block.Nodes[i]) + ";");
                    }
                }
                //添加最后一项
                builder.Append(ReGenerate(block.Nodes[block.Nodes.Length - 1]));
            }
            builder.Append("}");
            return builder.ToString();
        }

        private static string GenerateBinaryExpression(BinaryExpression binaryExpression)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(binaryExpression.Left));
            builder.Append(binaryExpression.Op.Value);
            builder.Append(ReGenerate(binaryExpression.Right));
            return builder.ToString();
        }

        private static string GenerateAssign(Assign assign)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(assign.Name));
            builder.Append("=");
            builder.Append(ReGenerate(assign.Node));
            return builder.ToString();
        }

        private static string GenerateArrayCombine(ArrayCombine arrayCombine)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayCombine.Array1));
            builder.Append("++");
            builder.Append(ReGenerate(arrayCombine.Array2));
            return builder.ToString();
        }

        private static string GenerateArrayIndexGroupCall(ArrayIndexGroupCall arrayCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayCall.Array));
            builder.Append('[');
            builder.Append(ReGenerate(arrayCall.IndexGroup));
            builder.Append(']');
            return builder.ToString();
        }

        private static string GenerateArray(Array array)
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (array.Nodes.Length>0)
            {
                if (array.Nodes.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < array.Nodes.Length - 1; i++)
                    {
                        builder.Append(ReGenerate(array.Nodes[i]) + ",");
                    }
                }
                //添加最后一项
                builder.Append(ReGenerate(array.Nodes[array.Nodes.Length - 1]));
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}

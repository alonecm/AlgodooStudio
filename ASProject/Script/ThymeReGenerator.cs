using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Script.Parse.Expr;
using System;
using System.Text;
using Array = AlgodooStudio.ASProject.Script.Parse.Expr.Array;
using Block = AlgodooStudio.ASProject.Script.Parse.Expr.Block;

namespace AlgodooStudio.ASProject.Script
{
    /// <summary>
    /// 脚本重生成
    /// </summary>
    public sealed class ThymeReGenerator
    {
        private int blockLayer = 0;
        /// <summary>
        /// 重生成脚本
        /// </summary>
        /// <param name="node">需要转换的语法节点</param>
        /// <returns></returns>
        public string ReGenerate(ThymeSyntaxNode node)
        {
            switch (node)
            {
                case Alloc alloc: return "alloc";
                case Array array: return GenerateArray(array);
                case ArrayCombine arrayCombine: return GenerateArrayCombine(arrayCombine);
                case ArrayWithBraceCall arrayWithBraceCall: return GenerateArrayWithBraceCall(arrayWithBraceCall);
                case ArrayWithArrayCall arrayWithArrayCall: return GenerateArrayWithArrayCall(arrayWithArrayCall);
                case Assign assign: return GenerateAssign(assign);
                case BinaryExpression binaryExpression: return GenerateBinaryExpression(binaryExpression);
                case Block block: return GenerateBlock(block);
                case BraceExpression braceExpression: return GenerateBraceExpression(braceExpression);
                case BraceWithArrayCall braceWithArrayCall: return GenerateBraceWithArrayCall(braceWithArrayCall);
                case BraceWithBraceCall braceWithBraceCall: return GenerateBraceWithBraceCall(braceWithBraceCall);
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
                case RealParams realParams: return GenerateRealParams(realParams);
                case Redirect redirect: return GenerateRedirect(redirect);
                case Root root: return GenerateRoot(root);
                case UnaryExpression unaryExpression: return GenerateUnaryExpression(unaryExpression);
                case UnsupportSymbol unsupportSymbol: return "[ERROR SYMBOL]";
                default: throw new NotImplementedException($" {node.Type} 未找到能转换的语法节点");
            }
        }

        private string GenerateBraceWithBraceCall(BraceWithBraceCall braceWithBraceCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(braceWithBraceCall.Brace1));
            builder.Append(ReGenerate(braceWithBraceCall.Brace2));
            return builder.ToString();
        }

        private string GenerateBraceWithArrayCall(BraceWithArrayCall braceWithArrayCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(braceWithArrayCall.Brace));
            builder.Append(ReGenerate(braceWithArrayCall.Array));
            return builder.ToString();
        }

        private string GenerateRealParams(RealParams realParams)
        {
            var builder = new StringBuilder();
            if (realParams.Nodes.Length > 0)
            {
                if (realParams.Nodes.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < realParams.Nodes.Length - 1; i++)
                    {
                        builder.Append(ReGenerate(realParams.Nodes[i]) + ",");
                    }
                }
                //添加最后一项
                builder.Append(ReGenerate(realParams.Nodes[realParams.Nodes.Length - 1]));
            }
            return builder.ToString();
        }

        private string GenerateIdentifierCall(IdentifierCall identifier)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(identifier.Identifier));
            builder.Append("(");
            builder.Append(ReGenerate(identifier.RealParams));
            builder.Append(")");
            return builder.ToString();
        }

        private string GenerateArrayWithBraceCall(ArrayWithBraceCall arrayWithBraceCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayWithBraceCall.Array));
            builder.Append(ReGenerate(arrayWithBraceCall.Brace));
            return builder.ToString();
        }

        private string GenerateUnaryExpression(UnaryExpression unaryExpression)
        {
            var builder = new StringBuilder();
            builder.Append(unaryExpression.Op.Value);
            builder.Append(ReGenerate(unaryExpression.Right));
            return builder.ToString();
        }

        private string GenerateRoot(Root root)
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

        private string GenerateRedirect(Redirect redirect)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(redirect.Name));
            builder.Append(" -> ");
            builder.Append(ReGenerate(redirect.Node));
            return builder.ToString();
        }

        private string GenerateParams(Params @params)
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

        private string GenerateMemberCall(MemberCall memberCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(memberCall.Name));
            builder.Append(".");
            builder.Append(ReGenerate(memberCall.Member));
            return builder.ToString();
        }

        private string GenerateNewAssign(NewAssign newAssign)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(newAssign.Name));
            builder.Append(":=");
            builder.Append(ReGenerate(newAssign.Node));
            return builder.ToString();
        }

        private string GenerateIFstatement(Ifstatement ifstatement)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(ifstatement.Expr1));
            builder.Append("?");
            builder.Append(ReGenerate(ifstatement.Expr2));
            builder.Append(":");
            builder.Append(ReGenerate(ifstatement.Expr3));
            return builder.ToString();
        }

        private string GenerateFunction(Function function)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(function.Params));
            builder.Append("=>");
            builder.Append(ReGenerate(function.Block));
            return builder.ToString();
        }

        private string GenerateBraceExpression(BraceExpression braceExpression)
        {
            var builder = new StringBuilder();
            builder.Append("(");
            builder.Append(ReGenerate(braceExpression.Node));
            builder.Append(")");
            return builder.ToString();
        }

        private string GenerateBlock(Block block)
        {
            var builder = new StringBuilder();
            builder.Append("{\r\n");
            blockLayer++;
            if (block.Nodes.Length > 0)
            {
                if (block.Nodes.Length > 1)
                {
                    //添加前面的项
                    for (int i = 0; i < block.Nodes.Length - 1; i++)
                    {
                        builder.Append("".PadLeft(blockLayer, '\t') + ReGenerate(block.Nodes[i]) + ";\r\n");
                    }
                }
                //添加最后一项
                builder.Append("".PadLeft(blockLayer, '\t') + ReGenerate(block.Nodes[block.Nodes.Length - 1]) + "\r\n");
            }
            blockLayer--;
            builder.Append("".PadLeft(blockLayer, '\t') + "}");
            return builder.ToString();
        }

        private string GenerateBinaryExpression(BinaryExpression binaryExpression)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(binaryExpression.Left));
            builder.Append(binaryExpression.Op.Value);
            builder.Append(ReGenerate(binaryExpression.Right));
            return builder.ToString();
        }

        private string GenerateAssign(Assign assign)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(assign.Name));
            builder.Append("=");
            builder.Append(ReGenerate(assign.Node));
            return builder.ToString();
        }

        private string GenerateArrayCombine(ArrayCombine arrayCombine)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayCombine.Array1));
            builder.Append("++");
            builder.Append(ReGenerate(arrayCombine.Array2));
            return builder.ToString();
        }

        private string GenerateArrayWithArrayCall(ArrayWithArrayCall arrayCall)
        {
            var builder = new StringBuilder();
            builder.Append(ReGenerate(arrayCall.Array1));
            builder.Append(ReGenerate(arrayCall.Array2));
            return builder.ToString();
        }

        private string GenerateArray(Array array)
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (array.Nodes.Length > 0)
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
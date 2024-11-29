using AlgodooStudio.ASProject.Script.Parse;
using AlgodooStudio.ASProject.Script.Parse.Expr;
using Dex.Analysis.Parse;
using System;

namespace AlgodooStudio.ASProject.Script
{
    //TODO:需要完善评估器
    /// <summary>
    /// Thyme评估器
    /// </summary>
    public sealed class ThymeEvaluator : IEvaluable<object, ThymeSyntaxNode>
    {
        public object Evaluate(ThymeSyntaxNode node)
        {
            return EvaluateRoot(node);
        }

        private object EvaluateRoot(ThymeSyntaxNode node)
        {
            switch (node)
            {
                case Root root:
                    object rootResult = null;
                    foreach (var item in root.Nodes)
                    {
                        //rootResult = EvaluateLine((item as Root));
                    }
                    return rootResult;
                default:
                    throw new Exception($"[{node.Type}]节点目前无法评估");
            }
        }

        /// <summary>
        /// 评估一行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private object EvaluateLine(ThymeSyntaxNode node)
        {
            switch (node)
            {
                case MemberCall memberCall:
                    return memberCall;
                default:
                    throw new Exception($"[{node.Type}]节点目前无法评估");
            }
        }

        private object EvaluatePrimary(ThymeSyntaxNode node)
        {
            throw new NotImplementedException();
        }
    }
}
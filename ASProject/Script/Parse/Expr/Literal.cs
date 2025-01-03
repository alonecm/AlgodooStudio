﻿using Dex.Analysis.Parse;
using Dex.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlgodooStudio.ASProject.Script.Parse.Expr
{
    public sealed class Literal : ThymeSyntaxNode
    {
        public override string Type => "Literal " + Value.Value;

        public ThymeToken Value { get; }

        public Literal(ThymeToken value)
        {
            Value = value;
            Range = value.Range;
        }
        public override Range Range { get; }
        public override IEnumerable<ISyntaxNode> GetChildren()
        {
            return Enumerable.Empty<ISyntaxNode>();
        }
    }
}
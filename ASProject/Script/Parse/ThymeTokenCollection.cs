using Dex.Analysis.Parse;
using System.Collections.Generic;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public sealed class ThymeTokenCollection : TokenCollection<ThymeToken>
    {
        public ThymeTokenCollection(ICollection<ThymeToken> tokens) : base(tokens)
        {
        }
    }
}
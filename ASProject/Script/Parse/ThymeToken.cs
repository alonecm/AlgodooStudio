using Dex.Analysis.Parse;
using Dex.Common;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public sealed class ThymeToken : IToken
    {
        public ThymeToken(string type, string value, Range range)
        {
            Type = type;
            Value = value;
            Range = range;
        }

        public string Type { get; }
        public string Value { get; }
        public Range Range { get; }

        public override string ToString()
        {
            return $"{Range} <{Type}> {Value}";
        }
    }
}
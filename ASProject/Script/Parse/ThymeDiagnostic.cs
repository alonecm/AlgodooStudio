using Dex.Analysis.Parse;
using Dex.Common;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public class ThymeDiagnostic : IDiagnosable
    {
        public ThymeDiagnostic(string message, Range range)
        {
            Range = range;
            Message = message;
        }

        public Range Range { get; }
        public string Message { get; }
    }
}
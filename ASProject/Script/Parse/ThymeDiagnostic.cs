using Dex.Analysis.Parse;
using Dex.Common;

namespace AlgodooStudio.ASProject.Script.Parse
{
    public class ThymeDiagnostic : IDiagnosable
    {
        public ThymeDiagnostic(string message, Range range, DiagnosticType type = DiagnosticType.Warning)
        {
            Range = range;
            Message = message;
            Type = type;
        }

        public Range Range { get; }
        public string Message { get; }

        public DiagnosticType Type { get; }
    }
}
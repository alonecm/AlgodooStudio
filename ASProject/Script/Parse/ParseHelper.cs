using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.ASProject.Script.Parse
{
    internal static class ParseHelper
    {
        public static int GetBinaryPriority(this ThymeToken op)
        {
            if (op.Type=="s_symbol" || op.Type == "m_symbol")
            {
                switch (op.Value)
                {
                    case "||":
                        return 1;

                    case "&&":
                        return 2;

                    case "==":
                    case "!=":
                        return 3;

                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                        return 4;

                    case "+":
                    case "-":
                        return 5;

                    case "*":
                    case "/":
                    case "%":
                        return 6;

                    case "^":
                        return 7;

                    default:
                        return 0;
                }
            }
            return 0;
        }

        public static int GetUnaryPriority(this ThymeToken op)
        {
            if (op.Type == "s_symbol" || op.Type == "m_symbol")
            {
                switch (op.Value)
                {
                    case "!":
                    case "+":
                    case "-":
                        return 8;

                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}

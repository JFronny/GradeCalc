using System;

namespace GradeCalc
{
    internal static class NCalcDoubleParser
    {
        public static double Parse(object nCalcOutput)
        {
            return nCalcOutput switch
            {
                bool n => (n ? 1 : 0),
                byte n => n,
                sbyte n => n,
                short n => n,
                ushort n => n,
                int n => n,
                uint n => n,
                long n => n,
                ulong n => n,
                float n => n,
                double n => n,
                decimal n => (double) n,
                _ => throw new ArgumentException("Type mismatch! (" + nCalcOutput.GetType() + ")")
            };
        }
    }
}
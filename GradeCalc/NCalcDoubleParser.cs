using System;

namespace GradeCalc
{
    internal static class NCalcDoubleParser
    {
        public static double Parse(object NCalcOutput)
        {
            if (NCalcOutput.GetType() == typeof(bool))
                return (bool) NCalcOutput ? 1 : 0;
            if (NCalcOutput.GetType() == typeof(byte))
                return (byte) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(sbyte))
                return (sbyte) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(short))
                return (short) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(ushort))
                return (ushort) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(int))
                return (int) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(uint))
                return (uint) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(long))
                return (long) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(ulong))
                return (ulong) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(float))
                return (float) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(double))
                return (double) NCalcOutput;
            if (NCalcOutput.GetType() == typeof(decimal))
                return (double) (decimal) NCalcOutput;
            throw new ArgumentException("Type mismatch! (" + NCalcOutput.GetType() + ")");
        }
    }
}
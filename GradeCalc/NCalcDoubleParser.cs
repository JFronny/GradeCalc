using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeCalc
{
    static class NCalcDoubleParser
    {
        public static double Parse(object NCalcOutput)
        {
            if (NCalcOutput.GetType() == typeof(bool))
                return (bool)NCalcOutput ? 1 : 0;
            else if (NCalcOutput.GetType() == typeof(byte))
                return (byte)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(sbyte))
                return (sbyte)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(short))
                return (short)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(ushort))
                return (ushort)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(int))
                return (int)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(uint))
                return (uint)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(long))
                return (long)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(ulong))
                return (ulong)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(float))
                return (float)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(double))
                return (double)NCalcOutput;
            else if (NCalcOutput.GetType() == typeof(decimal))
                return (double)(decimal)NCalcOutput;
            throw new ArgumentException("Type mismatch! (" + NCalcOutput.GetType().ToString() + ")");
        }
    }
}

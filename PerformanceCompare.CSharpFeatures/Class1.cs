using System;

namespace PerformanceCompare.CSharpFeatures
{
    public class SpanHelper
    {
        public static char ForEachTest(ReadOnlySpan<char> data, int LoopCount)
        {
            char ch = default;
            for (var i = 0; i < LoopCount; i++)
            {
                for (var j = 1; j <= 4; j++)
                    ch = data[j];
            }
            return ch;
        }
    }
}

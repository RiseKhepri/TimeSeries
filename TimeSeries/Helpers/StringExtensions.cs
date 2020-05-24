using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSeries.Helpers
{
    public static class StringExtensions
    {
        public static int CountStringOccurrences(this string text, string pattern)
        {
            int count = 0;

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                var sub = text.Substring(i, pattern.Length);

                if (sub == pattern)
                {
                    count++;
                }
            }

            return count;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public class AlphabetMapper
    {
        public string MapDifferentialSeries(List<decimal> series, string alphabet,
            List<Interval> negativeIntervals,
            List<Interval> positiveIntervals)
        {
            int n = alphabet.Length / 2;

            var charArray = series.Select(diff =>
            {
                var negative = negativeIntervals.FirstOrDefault(x => x.IsValueInside(diff));

                if (negative != null)
                {
                    return alphabet[negative.Index];
                }

                var positive = positiveIntervals.First(x => x.IsValueInside(diff));

                return alphabet[positive.Index + n];
            })
                .ToArray();

            return new string(charArray);
        }
    }
}

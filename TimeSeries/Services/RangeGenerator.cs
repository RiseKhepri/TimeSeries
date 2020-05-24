using System.Collections.Generic;
using System.Linq;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public class RangeGenerator
    {
        public List<Interval> GetIntervals(decimal min, decimal max, string alphabet,
            bool leftIncluded = false,
            bool rightIncluded = false)
        {
            var n = alphabet.Length / 2;
            var intervalLength = (max - min) / n;

            var startInterval = new Interval
            {
                Left = min,
                Right = min + intervalLength,
                LeftIncluded = true,
                RightIncluded = rightIncluded,
                Index = 0
            };

            return Enumerable.Range(0, n - 1)
                .Aggregate(new List<Interval> { startInterval }, (acc, i) =>
                {
                    var prev = acc.Last().Right;
                    acc.Add(new Interval
                    {
                        Left = prev,
                        Right = prev + intervalLength,
                        LeftIncluded = leftIncluded,
                        RightIncluded = rightIncluded,
                        Index = i + 1
                    });
                    return acc;
                });
        }
    }
}

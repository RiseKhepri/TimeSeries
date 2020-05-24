using System.Collections.Generic;
using System.Linq;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public class DifferentialSeriesCalculator
    {
        private readonly List<RawSeries> _data;

        public DifferentialSeriesCalculator(List<RawSeries> data)
        {
            _data = data;
        }

        public List<decimal> GetDifferentialSeries()
        {
            return Enumerable.Range(1, _data.Count - 1)
                .Select(i => _data[i].Value - _data[i - 1].Value)
                .ToList();
        }
    }
}

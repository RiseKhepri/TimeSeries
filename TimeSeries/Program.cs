using System.Collections.Generic;
using System.IO;
using System.Linq;
using TimeSeries.Models;
using TimeSeries.Services;

namespace TimeSeries
{
    class Program
    {
        static void Main(FileInfo series, FileInfo alphabet)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            // read data - series and alphabet
            var data = series.ReadSeries();
            var alphabetString = alphabet.ReadAlphabet();

            var wholePeriod = ProcessSeries(data, alphabetString, "whole");

            var byYears = data.GroupBy(x => x.DateTime.Year);

            var diffs = new List<(decimal, decimal, decimal)>();

            foreach (var yearSeries in byYears)
            {
                var byYear = ProcessSeries(yearSeries.ToList(), alphabetString, yearSeries.Key.ToString());

                var diff1 = MatrixComparer.Compare(wholePeriod.matrix1, byYear.matrix1);
                var diff2 = MatrixComparer.Compare(wholePeriod.matrix2, byYear.matrix2);
                var diff3 = MatrixComparer.Compare(wholePeriod.matrix3, byYear.matrix3);

                diffs.Add((diff1, diff2, diff3));
            }

            File.WriteAllLines("res/diffs.txt", diffs.Select(x => $"{x.Item1};{x.Item2};{x.Item3}"));
        }

        static (List<MatrixElement> matrix1, List<MatrixElement> matrix2, List<MatrixElement> matrix3)
            ProcessSeries(List<RawSeries> series, string alphabet, string name)
        {
            // calculate differential series
            var differentialCalculator = new DifferentialSeriesCalculator(series);
            var differentialSeries = differentialCalculator.GetDifferentialSeries();

            // calculate intervals
            var min = differentialSeries.Min();
            var max = differentialSeries.Max();
            var intervalsCalculator = new RangeGenerator();

            var negativeIntervals = intervalsCalculator.GetIntervals(min, 0, alphabet, leftIncluded: true);
            var positiveIntervals = intervalsCalculator.GetIntervals(0, max, alphabet, rightIncluded: true);

            // map differential series to linguistic series
            var mapper = new AlphabetMapper();
            var linguisticString = mapper.MapDifferentialSeries(differentialSeries, alphabet, negativeIntervals, positiveIntervals);

            // calculate matrix
            var builder = new MatrixBuilder(linguisticString, alphabet);
            var matrix1 = builder.CalculateMatrix(1);
            var matrix2 = builder.CalculateMatrix(2);
            var matrix3 = builder.CalculateMatrix(3);

            ResultSaver.Save(name, differentialSeries, linguisticString, matrix1, matrix2, matrix3);

            return (matrix1, matrix2, matrix3);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public static class MatrixComparer
    {
        public static decimal Compare(List<MatrixElement> matrix1, List<MatrixElement> matrix2)
        {
            var rows = matrix1.Select(x => x.Row).Union(matrix2.Select(x => x.Row)).Distinct().ToList();
            var columns = matrix1.Select(x => x.Column).Union(matrix2.Select(x => x.Column)).Distinct().ToList();

            double acc = 0;

            foreach (var row in rows)
            {
                foreach (var column in columns)
                {
                    var value1 = matrix1.FirstOrDefault(x => x.Row == row && x.Column == column)?.Value ?? 0;
                    var value2 = matrix2.FirstOrDefault(x => x.Row == row && x.Column == column)?.Value ?? 0;

                    acc += Math.Pow((double)(value1 - value2), 2);
                }
            }

            return (decimal)(Math.Sqrt(acc));
        }
    }
}

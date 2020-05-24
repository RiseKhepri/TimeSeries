using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public static class ResultSaver
    {
        public static void Save(string name, List<decimal> differential, string linguistucString, params List<MatrixElement>[] matrixes)
        {
            Directory.CreateDirectory("res");
            File.WriteAllText($"res/{name}-series.txt", linguistucString);
            File.WriteAllLines($"res/{name}-diff.txt", differential.Select(x => x.ToString("N4")));

            for (int i = 0; i < matrixes.Length; i++)
            {
                var matrix = matrixes[i];

                var rows = matrix.Select(x => x.Row).Distinct().OrderBy(x => x).ToArray();
                var columns = matrix.Select(x => x.Column).Distinct().ToArray();

                var lines = new List<string>();
                lines.Add("x " + string.Join(" ", columns));

                foreach (var row in rows)
                {
                    var values = columns.Select(c => matrix.First(m => m.Row == row && m.Column == c).Value.ToString("N4"));

                    lines.Add(row + " " + string.Join(" ", values));
                }

                File.WriteAllLines($"res/{name}-matrix-{i}.txt", lines);
            }
        }
    }
}

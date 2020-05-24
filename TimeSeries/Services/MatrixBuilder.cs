using System.Collections.Generic;
using TimeSeries.Helpers;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public class MatrixBuilder
    {
        private string _series;
        private string _alphabet;

        public MatrixBuilder(string series, string alphabet)
        {
            _series = series;
            _alphabet = alphabet;
        }

        public List<MatrixElement> CalculateMatrix(int stateCount)
        {
            var leftStates = new HashSet<string>();

            for (int i = 0; i <= _series.Length - stateCount; i++)
            {
                var state = _series.Substring(i, stateCount);
                leftStates.Add(state);
            }

            var result = new List<MatrixElement>();

            foreach (var leftState in leftStates)
            {
                foreach (var rightState in _alphabet)
                {
                    var total = _series.CountStringOccurrences(leftState);
                    var tran = _series.CountStringOccurrences(leftState + rightState);
                    var element = new MatrixElement
                    {
                        Row = leftState,
                        Column = rightState.ToString(),
                        Value = (decimal)tran / total
                    };

                    result.Add(element);
                }
            }

            return result;
        }
    }
}

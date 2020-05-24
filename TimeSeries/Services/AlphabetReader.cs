using System;
using System.IO;
using System.Linq;

namespace TimeSeries.Services
{
    public static class AlphabetReader
    {
        public static string ReadAlphabet(this FileInfo file)
        {
            using (var reader = file.OpenText())
            {
                var rawLine = reader.ReadLine();

                return new string(rawLine.ToCharArray().Reverse().ToArray()) + rawLine.ToUpper();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TimeSeries.Models;

namespace TimeSeries.Services
{
    public static class CsvReader
    {
        public static List<RawSeries> ReadSeries(this FileInfo file)
        {
            var result = new List<RawSeries>();

            using (var csvReader = new CsvHelper.CsvReader(file.OpenText(), CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.Delimiter = ",";

                while (csvReader.Read())
                {
                    try
                    {
                        var item = new RawSeries
                        {
                            DateTime = csvReader.GetField<DateTime>(0),
                            Value = csvReader.GetField<decimal>(1)
                        };

                        result.Add(item);
                    }
                    catch(Exception)
                    {

                    }
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;

namespace Anomalies
{
    internal static class WaitTimeLoader
    {
        public static TimeSeries[] Load()
        {
            // Create an ML.NET machine learning context.
            var context = new MLContext();

            // Get the path to the CSV files.
            string rootFolder = Directory.GetParent(Environment.CurrentDirectory).FullName;
            string dataFolder = Path.Combine(rootFolder, "data");
            string[] csvFiles = Directory.GetFiles(dataFolder, "Wait_Time_Sample_*.csv");

            // Load data from the CSV files.
            var timeSeriesList = new List<TimeSeries>();
            foreach (string csvFile in csvFiles)
            {
                Console.WriteLine($"Loading database wait times from '{csvFile}'...");
                IDataView dataView = context.Data.LoadFromTextFile<WaitTime>(path: csvFile, hasHeader: true, separatorChar: ',');
                WaitTime[] waitTimes = context.Data.CreateEnumerable<WaitTime>(dataView, reuseRowObject: false).ToArray();

                // Convert to time series.
                var timeSeries = new TimeSeries(
                    name: Path.GetFileName(csvFile),
                    observations: waitTimes.Select(s => s.ToObservation()),
                    interval: TimeSpan.FromHours(1),
                    group: "Database Wait Times");

                timeSeriesList.Add(timeSeries);
            }

            return timeSeriesList.ToArray();
        }
    }
}

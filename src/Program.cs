using System;
using System.Collections.Generic;
using XPlot.Plotly;

namespace Anomalies
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSeries[] stockSeries = StockLoader.Load();
            IEnumerable<TimeSeriesAnalysis> analysisResults = Analyze(stockSeries);
            ShowCharts(analysisResults);
            Console.WriteLine("Finished!");
        }

        private static IEnumerable<TimeSeriesAnalysis> Analyze(IEnumerable<TimeSeries> timeSeriesList)
        {
            foreach (TimeSeries timeSeries in timeSeriesList)
            {
                var analysis = new TimeSeriesAnalysis(timeSeries);
                yield return analysis;
            }
        }

        private static void ShowCharts(IEnumerable<TimeSeriesAnalysis> analysisResults)
        {
            var charts = new List<PlotlyChart>();

            foreach (TimeSeriesAnalysis analysis in analysisResults)
            {
                PlotlyChart chart = ChartBuilder.BuildChart(analysis);
                charts.Add(chart);
            }

            Chart.ShowAll(charts);
        }
    }
}

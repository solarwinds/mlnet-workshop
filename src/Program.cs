using System;
using System.Collections.Generic;
using System.Linq;
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
            const int horizon = 100;

            foreach (TimeSeries timeSeries in timeSeriesList)
            {
                Observation[] observations = GapFiller.FillGaps(timeSeries.Observations, timeSeries.Interval).ToArray();
                Observation[] historical = observations.Take(observations.Length - horizon).ToArray();
                Observation[] actual = observations.Skip(observations.Length - horizon).ToArray();

                var forecasts = new List<ForecastDetails>();
                Observation[] linearRegressionForecast = LinearRegressionForecaster
                    .Forecast(historical, horizon, timeSeries.Interval);
                forecasts.Add(new ForecastDetails("Linear Regression", linearRegressionForecast));

                var analysis = new TimeSeriesAnalysis(timeSeries, historical, actual, forecasts);
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

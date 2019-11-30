using System.Collections.Generic;
using System.Linq;
using XPlot.Plotly;

namespace Anomalies
{
    internal static class HistogramBuilder
    {
        public static PlotlyChart BuildHistogram(string groupName, ICollection<TimeSeriesAnalysis> analysisResults)
        {
            IEnumerable<Graph.Histogram> traces = BuildTraces(analysisResults);
            PlotlyChart chart = BuildChart(groupName, traces);
            return chart;
        }

        private static IEnumerable<Graph.Histogram> BuildTraces(ICollection<TimeSeriesAnalysis> analysisResults)
        {
            ILookup<string, ForecastDetails> forecastsByAlgorithm =
                analysisResults.SelectMany(a => a.Forecasts)
                .ToLookup(o => o.AlgorithmName);

            double max = analysisResults.Max(a => a.Forecasts.Max(f => f.RegressionMetrics.RootMeanSquaredError));
            int binSize = ((int)((max + 1) / 100)) * 10;

            foreach (IGrouping<string, ForecastDetails> forecastGrouping in forecastsByAlgorithm)
            {
                var trace = new Graph.Histogram
                {
                    name = forecastGrouping.Key,
                    x = forecastGrouping.Select(f => f.RegressionMetrics.RootMeanSquaredError),
                    opacity = 0.75,
                    autobinx = false,
                    xbins = new Graph.Xbins
                    {
                        start = 0,
                        end = binSize * 10,
                        size = binSize
                    }
                };

                yield return trace;
            }
        }

        private static PlotlyChart BuildChart(string groupName, IEnumerable<Graph.Histogram> traces)
        {
            PlotlyChart chart = Chart.Plot(traces);

            var layout = new Layout.Layout
            {
                title = $"{groupName} RMSE",
                barmode = "overlay"
            };

            chart.WithLayout(layout);
            chart.Width = 800;
            chart.Height = 500;
            chart.WithLegend(true);

            return chart;
        }
    }
}

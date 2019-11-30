using System;
using System.Collections.Generic;
using System.Linq;
using XPlot.Plotly;

namespace Anomalies
{
    internal static class ChartBuilder
    {
        public static PlotlyChart BuildChart(TimeSeriesAnalysis analysis)
        {
            IEnumerable<Graph.Trace> traces = BuildTraces(analysis);
            PlotlyChart chart = BuildPlotlyChart(analysis.TimeSeries.Name, traces);
            return chart;
        }

        private static IEnumerable<Graph.Trace> BuildTraces(TimeSeriesAnalysis analysis)
        {
            yield return BuildTrace("Historical", analysis.Historical);
            yield return BuildTrace("Actual", analysis.Actual);

            foreach (ForecastDetails forecastDetails in analysis.Forecasts)
            {
                string name = $"{forecastDetails.AlgorithmName} Forecast";
                yield return BuildTrace(name, forecastDetails.Forecast);
            }
        }

        private static Graph.Trace BuildTrace(string name, IEnumerable<Observation> observations)
        {
            DateTime[] dates = observations.Select(s => s.Date).ToArray();
            float[] values = observations.Select(s => s.Value).ToArray();

            return new Graph.Scatter()
            {
                name = name,
                x = dates,
                y = values
            };
        }

        private static PlotlyChart BuildPlotlyChart(string chartTitle, IEnumerable<Graph.Trace> traces)
        {
            PlotlyChart chart = Chart.Plot(traces);

            var layout = new Layout.Layout { title = chartTitle };
            chart.WithLayout(layout);
            chart.Width = 800;
            chart.Height = 500;
            chart.WithXTitle("Date");
            chart.WithYTitle("Value");
            chart.WithLegend(true);

            return chart;
        }
    }
}

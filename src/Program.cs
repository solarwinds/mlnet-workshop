﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Data;
using XPlot.Plotly;

namespace Anomalies
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSeries[] stockSeries = StockLoader.Load();
            TimeSeries[] waitTimeSeries = WaitTimeLoader.Load();
            TimeSeries[] timeSeriesList = waitTimeSeries.Concat(stockSeries).ToArray();
            TimeSeriesAnalysis[] analysisResults = Analyze(timeSeriesList).ToArray();
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
                RegressionMetrics linearRegressionMetrics = ForecastScorer.Evaluate(actual, linearRegressionForecast);
                forecasts.Add(new ForecastDetails("Linear Regression", linearRegressionForecast, linearRegressionMetrics));

                Observation[] ssaForecast = SsaForecaster.Forecast(historical, horizon, timeSeries.Interval);
                RegressionMetrics ssaMetrics = ForecastScorer.Evaluate(actual, ssaForecast);
                forecasts.Add(new ForecastDetails("SSA", ssaForecast, ssaMetrics));

                var analysis = new TimeSeriesAnalysis(timeSeries, historical, actual, forecasts);
                yield return analysis;
            }
        }

        private static void ShowCharts(TimeSeriesAnalysis[] analysisResults)
        {
            var charts = new List<PlotlyChart>();

            foreach (TimeSeriesAnalysis analysis in analysisResults)
            {
                PlotlyChart chart = ChartBuilder.BuildChart(analysis);
                charts.Add(chart);
            }

            foreach (IGrouping<string, TimeSeriesAnalysis> analysisGroup in analysisResults.ToLookup(a => a.TimeSeries.Group))
            {
                PlotlyChart histogram = HistogramBuilder.BuildHistogram(analysisGroup.Key, analysisGroup.ToList());
                charts.Add(histogram);
            }

            Chart.ShowAll(charts);
        }
    }
}

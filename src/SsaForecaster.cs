using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace Anomalies
{
    internal class SsaForecaster
    {
        public static Observation[] Forecast(ICollection<Observation> observations, int horizon, TimeSpan interval)
        {
            // Create a new ML context.
            var ml = new MLContext();

            // Convert data to IDataView.
            IDataView dataView = ml.Data.LoadFromEnumerable(observations);

            // Setup arguments.
            var inputColumnName = nameof(Observation.Value);
            var outputColumnName = nameof(ForecastResult.Forecast);

            int windowSize = Math.Min(50, observations.Count / 2 - 1);
            int seriesLength = Math.Min(110, observations.Count);

            // Instantiate the forecasting model.
            var model = ml.Forecasting.ForecastBySsa(
                outputColumnName: outputColumnName,
                inputColumnName: inputColumnName,
                windowSize: windowSize,
                seriesLength: seriesLength,
                trainSize: observations.Count,
                horizon: horizon);

            // Train.
            var transformer = model.Fit(dataView);

            // Forecast the next set of values.
            using var forecastEngine = transformer.CreateTimeSeriesEngine<Observation, ForecastResult>(ml);
            var forecast = forecastEngine.Predict();

            var predictions = new List<Observation>();
            DateTime currentTime = observations.Last().Date;

            foreach (var predictedValue in forecast.Forecast)
            {
                currentTime = currentTime.Add(interval);
                predictions.Add(new Observation { Date = currentTime, Value = predictedValue });
            }

            return predictions.ToArray();
        }

        private class ForecastResult
        {
            public float[] Forecast { get; set; }
        }
    }
}

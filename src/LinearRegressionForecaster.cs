using System;
using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class LinearRegressionForecaster
    {
        public static Observation[] Forecast(Observation[] observations, int horizon, TimeSpan interval)
        {
            // Convert the observations into (x,y) points.
            float[] x = observations.Select(o => (float)o.Date.Ticks).ToArray();
            float[] y = observations.Select(o => o.Value).ToArray();

            // Use linear regression to fit a line to the points.
            (float intercept, float slope) = FitLine(x, y);

            // Use the slope/intercept definition of the line to predict future values.
            var predictions = new List<Observation>();
            DateTime currentTime = observations.Last().Date;

            for (int i = 0; i < horizon; i++)
            {
                currentTime = currentTime.Add(interval);
                float currentX = (float)currentTime.Ticks;
                float predictedValue = slope * currentX + intercept;
                var prediction = new Observation { Date = currentTime, Value = predictedValue };
                predictions.Add(prediction);
            }

            return predictions.ToArray();
        }

        private static (float intercept, float slope) FitLine(float[] x, float[] y)
        {
            float meanX = x.Sum() / x.Length;
            float meanY = y.Sum() / y.Length;

            float covariance = 0.0f;
            float variance = 0.0f;

            for (int i = 0; i < x.Length; i++)
            {
                float diff = x[i] - meanX;
                variance += diff * diff;
                covariance += diff * (y[i] - meanY);
            }

            float slope = covariance / variance;
            return (meanY - slope * meanX, slope);
        }
    }
}

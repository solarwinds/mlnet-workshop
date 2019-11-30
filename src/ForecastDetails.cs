using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Data;

namespace Anomalies
{
    internal class ForecastDetails
    {
        public string AlgorithmName { get; }
        public Observation[] Forecast { get; }
        public RegressionMetrics RegressionMetrics { get; }

        public ForecastDetails(
            string algorithmName,
            IEnumerable<Observation> forecast,
            RegressionMetrics regressionMetrics)
        {
            AlgorithmName = algorithmName;
            Forecast = forecast.ToArray();
            RegressionMetrics = regressionMetrics;
        }
    }
}

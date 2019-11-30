using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class ForecastDetails
    {
        public string AlgorithmName { get; }
        public Observation[] Forecast { get; }

        public ForecastDetails(string algorithmName, IEnumerable<Observation> forecast)
        {
            AlgorithmName = algorithmName;
            Forecast = forecast.ToArray();
        }
    }
}

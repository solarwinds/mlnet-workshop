using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class TimeSeriesAnalysis
    {
        public TimeSeries TimeSeries { get; }
        public Observation[] Historical { get; }
        public Observation[] Actual { get; }
        public ForecastDetails[] Forecasts { get; }

        public TimeSeriesAnalysis(
            TimeSeries timeSeries,
            IEnumerable<Observation> historical,
            IEnumerable<Observation> actual,
            IEnumerable<ForecastDetails> forecasts)
        {
            TimeSeries = timeSeries;
            Historical = historical.ToArray();
            Actual = actual.ToArray();
            Forecasts = forecasts.ToArray();
        }
    }
}

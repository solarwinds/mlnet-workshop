using System;
using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class TimeSeries
    {
        public string Name { get; }
        public Observation[] Observations { get; }
        public TimeSpan Interval { get; }

        public TimeSeries(
            string name,
            IEnumerable<Observation> observations,
            TimeSpan interval)
        {
            Name = name;
            Observations = observations.ToArray();
            Interval = interval;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Anomalies
{
    internal class TimeSeries
    {
        public string Name { get; }
        public Observation[] Observations { get; }

        public TimeSeries(string name, IEnumerable<Observation> observations)
        {
            Name = name;
            Observations = observations.ToArray();
        }
    }
}
